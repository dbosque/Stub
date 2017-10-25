using System;


namespace dBosque.Stub.Client.Socket.Exception
{
    /// <summary>
    /// Stub server could not be found.
    /// </summary>
    public class StubNotAvailableException : System.Exception
    {
        public StubNotAvailableException(string host, int port) 
            : base($"No Stub available at {host}:{port}.")
        { }

        public StubNotAvailableException(string server)
           : base($"No Stub available at {server}.")
        { }

        public StubNotAvailableException(StubNotAvailableException original)
            : base(original.Message)
        { }
    }
}
