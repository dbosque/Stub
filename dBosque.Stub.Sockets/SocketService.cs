using dBosque.Stub.Interfaces;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Sockets.Models;
using dBosque.Stub.Sockets.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace dBosque.Stub.Sockets
{

    /// <summary>
    /// Socket service, allows for connection through a port
    /// </summary>
#pragma warning disable S3881 // "IDisposable" should be implemented correctly
    public class SocketService : IStubService, IServiceRegister, IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
    {
        private readonly int portNumber = 2201;
        private Socket serverSocket;
        private readonly ClientConnectionList clientSockets = new ClientConnectionList();
        const int MAX_RECEIVE_ATTEMPT = 10;

        private IStubHandler<string> _handler;

        private ILogger _logger;
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="config"></param>
        public SocketService(IStubHandler<string> handler, IConfigurationRepository config, IConfiguration configuration, ILogger<SocketService> logger)
        {
            _handler = handler;
            _logger = logger;
            var port = configuration.GetValue<string>("Hosting:SocketPort");
            int.TryParse(port, out portNumber);
        }

        /// <summary>
        /// A description of the runtime environment
        /// </summary>
        string IStubService.Description
        {
            get
            {
                return $"Running socketService on {serverSocket?.LocalEndPoint.ToString()}.";
            }
        }

        public IStubService Service => this;

        /// <summary>
        /// Start the service
        /// </summary>
        /// <returns></returns>
        IStubService IStubService.Start(Action<IServiceCollection> collectionAction, IConfiguration configuration)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, portNumber));
            serverSocket.Listen(4); //the maximum pending client, define as you wish
            serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
            return this;
        }

        /// <summary>
        /// Stop the service
        /// </summary>
        /// <returns></returns>
        IStubService IStubService.Stop()
        {
            clientSockets.CloseAll();
            if (serverSocket.Connected)
                serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Dispose();
            serverSocket = null;
            return this;
        }

        /// <summary>
        /// Event on connection accept
        /// </summary>
        /// <param name="result"></param>
        private void acceptCallback(IAsyncResult result)
        {
            try
            {
                if (!result.IsCompleted)
                    return;
                if (serverSocket == null)
                    return;

                var conn = new ClientConnection(serverSocket.EndAccept(result));           
                _logger.LogInformation($"Socket connected on {conn.Id}");
                conn.BeginReceive(new AsyncCallback(receiveCallback));
                clientSockets.AddConnection(conn);
                serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null); //to receive another client
            }
            catch (ObjectDisposedException)
            {
                // When a connection is closed this will be thrown because the callback will always execute.
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
        }

        public void HandleEndReceive(ClientConnection con, IAsyncResult result)
        {
            int received = con.Socket.EndReceive(result, out SocketError errorCode);
            if (received > 0 && errorCode == SocketError.Success)
            {
                string msg = con.ReceivedData(received);
                try
                {
                    msg = _handler.HandleMessage(new SocketMessage(con.Id, msg));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    msg = ex.Message;
                }

                con.Socket.Send(Encoding.UTF8.GetBytes(msg)); //Note that you actually send data in byte[]
                con.Attempts = 0; //reset receive attempt
                con.BeginReceive(new AsyncCallback(receiveCallback));//repeat beginReceive
            }
            else if (errorCode == SocketError.ConnectionReset)
            {
                con.Attempts = 0;
                _logger.LogDebug($"Connectionreset for {con.Id}.");
                clientSockets.Disconnect(con);
            }
            else if (con.Attempts < MAX_RECEIVE_ATTEMPT)
            {
                ++con.Attempts;
                con.BeginReceive(new AsyncCallback(receiveCallback));//repeat beginReceive
            }
            else
            {
                con.Attempts = 0; //reset this for the next connection
                clientSockets.Disconnect(con);
            }
        }
 
        /// <summary>
        /// Event on message received
        /// </summary>
        /// <param name="result"></param>
        private void receiveCallback(IAsyncResult result)
        {
            ClientConnection con = null;
            try
            {
                con = (ClientConnection)result.AsyncState; //this is to get the sender
                if (con.Socket.Connected)
                {
                    HandleEndReceive(con, result);
                }
            }
            catch (SocketException se)
            {
                _logger.LogError(se.ToString());
                clientSockets.Disconnect(con);
            }
            catch (ObjectDisposedException)
            {
                // When a connection is closed this will be thrown because the callback will always execute.
            }
            catch (Exception e)
            { 
                _logger.LogError("receiveCallback fails with exception! " + e.ToString());
                clientSockets.Disconnect(con);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (serverSocket != null)
                    (this as IStubService).Stop();

                disposedValue = true;
            }
        }      

        /// <summary>
        /// Dispose the service
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion


    }
}
