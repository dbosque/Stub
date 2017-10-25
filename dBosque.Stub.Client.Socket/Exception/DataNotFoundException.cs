using System;

namespace dBosque.Stub.Client.Socket.Exception
{
    /// <summary>
    /// No result found in the stub and passthrough is not enabled.
    /// </summary>
    public class DataNotFoundException : System.Exception
    {
        public DataNotFoundException(string message) 
            : base(message)
        { }
    }
}
