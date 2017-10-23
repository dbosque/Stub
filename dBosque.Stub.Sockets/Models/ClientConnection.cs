using System;
using System.Net.Sockets;
using System.Text;

namespace dBosque.Stub.Sockets.Models
{
    /// <summary>
    /// Client socket connection
    /// </summary>
#pragma warning disable S3881 // "IDisposable" should be implemented correctly    
    public class ClientConnection : IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly

    {

        private class StateObject
        {
            public const int BufferSize = 4096;
            public byte[] Buffer = new byte[BufferSize];
            public StringBuilder Builder = new StringBuilder();


            
        }


        public ClientConnection(Socket s)
        {
            Socket = s;
            Attempts = 0;
            Id = s.RemoteEndPoint.ToString();
            State = new StateObject();
        }

        public IAsyncResult BeginReceive(AsyncCallback callback)
        {
            State = new StateObject();
            return Socket.BeginReceive(State.Buffer, 0, State.Buffer.Length, SocketFlags.None, callback, this);
        }

        public string ReceivedData(int read)
        {
            State.Builder.Append(Encoding.UTF8.GetString(State.Buffer, 0, read));
            return State.Builder.ToString();
        }

        private StateObject State { get; set; }
        public string Id { get; set; }

        public Socket Socket { get; private set; }
        public int Attempts { get; set; }

        ///<summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Socket.Close();
            //Socket.Disconnect(true);
            //Socket.Dispose();
            Socket = null;
        }
    }
}
