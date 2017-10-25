using dBosque.Stub.Client.Socket.Exception;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace dBosque.Stub.Client.Socket.Process
{
    /// <summary>
    /// Connection to server
    /// </summary>
    public sealed class StubServerConnection : IDisposable
    {
        private const int BUFFER_SIZE = 4096;
        private const int MAX_RECEIVE_ATTEMPT = 10;
        private readonly byte[] _buffer = new byte[BUFFER_SIZE];

        private int _receiveAttempt = 0;
        private System.Net.Sockets.Socket _clientSocket;
        private string _response = string.Empty;

        private readonly EventHandler<MessageEventArgs> OnError;
        private readonly ManualResetEvent _mre = new ManualResetEvent(false);

        private readonly string _server;
        private readonly int _port;

        public StubServerConnection(string server, int port)
        {
            _server = server;
            _port = port;

            OnError += onErrorReport;
        }

        private class MessageEventArgs : EventArgs
        {
            public MessageEventArgs(string msg)
            {
                Message = msg;
            }

            public string Message { get; private set; }
        }

        /// <summary>
        /// Reset the socket in case of an error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void onErrorReport(object sender, MessageEventArgs args)
        {
            _response = args.Message;           
            _mre.Set();
        }

        /// <summary>
        /// Check if an errorcode was returned
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int? DeductErrorCode(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                return null;
            if (!int.TryParse(msg, out int val))
                return null;
            return val;
        }

        /// <summary>
        /// Send a message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public string Send(string msg, out int? errorCode)
        {
            errorCode = null;
            try
            {
                // Do not send empty messages.
                if (string.IsNullOrEmpty(msg))
                    return msg;

                if (!(_clientSocket?.Connected ?? false))
                    loopConnect(3, 3);

                System.Diagnostics.Trace.TraceInformation(msg);

                byte[] bytes = Encoding.UTF8.GetBytes(msg);
               
                _clientSocket.Send(bytes);
                // Wait for the response
                _mre.WaitOne();
                // Rest for the next
                _mre.Reset();
                errorCode = DeductErrorCode(_response);
                System.Diagnostics.Trace.TraceInformation(_response);
                return _response;
            }
            catch (System.Exception)
            {
                throw new StubNotAvailableException(_server, _port);
            }
        }

        /// <summary>
        /// Try to connect a couple of times
        /// </summary>
        /// <param name="noOfRetry"></param>
        /// <param name="attemptPeriodInSeconds"></param>
        private void loopConnect(int noOfRetry, int attemptPeriodInSeconds)
        {
            _clientSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int attempts = 0;
            while (!_clientSocket.Connected && attempts < noOfRetry)
            {
                try
                {
                    ++attempts;
                    IAsyncResult result = _clientSocket.BeginConnect(_server, _port, endConnectCallback, null);
                    result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(attemptPeriodInSeconds));
                    Thread.Sleep(attemptPeriodInSeconds * 1000);
                }
                catch 
                {
                    // Ignore for now                         
                }
            }            
        }


        /// <summary>
        /// Callback to signal a (un)successfull connect
        /// </summary>
        /// <param name="ar"></param>
        private void endConnectCallback(IAsyncResult ar)
        {
            try
            {
                _clientSocket.EndConnect(ar);
                if (_clientSocket.Connected)
                {
                    _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), _clientSocket);
                }
                else
                {
                    OnError?.Invoke(this, new MessageEventArgs("End of connection attempt, fail to connect..."));
                }
            }
            catch (System.Exception e)
            {
                OnError?.Invoke(this, new MessageEventArgs("End-connection attempt is unsuccessful! " + e.ToString()));
            }
        }

        /// <summary>
        /// Callback to signal received data.
        /// </summary>
        /// <param name="result"></param>
        private void receiveCallback(IAsyncResult result)
        {
            System.Net.Sockets.Socket socket = null;
            try
            {
                socket = (System.Net.Sockets.Socket)result.AsyncState;
                if (socket.Connected)
                {
                    int received = socket.EndReceive(result);
                    if (received > 0)
                    {
                        _receiveAttempt = 0;
                        byte[] data = new byte[received];
                        Buffer.BlockCopy(_buffer, 0, data, 0, data.Length);
                        _response = Encoding.UTF8.GetString(data);

                        // Signal response
                        _mre.Set();
                        socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket);
                    }
                    else if (_receiveAttempt < MAX_RECEIVE_ATTEMPT)
                    { //not exceeding the max attempt, try again
                        ++_receiveAttempt;
                        socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket);
                    }
                    else
                    {
                        OnError?.Invoke(this, new MessageEventArgs("receiveCallback is failed!"));
                        _receiveAttempt = 0;
                        _clientSocket.Close();
                    }
                }
            }
            catch (System.Exception e)
            {
                OnError?.Invoke(this, new MessageEventArgs("receiveCallback is failed! " + e.ToString()));
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _clientSocket?.Dispose();
                    _clientSocket = null;
                    _mre?.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose sockets
        /// </summary>
        ~StubServerConnection()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        ///<summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


    }

}
