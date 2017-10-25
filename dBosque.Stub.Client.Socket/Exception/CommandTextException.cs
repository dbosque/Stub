using System;

namespace dBosque.Stub.Client.Socket.Exception
{
    /// <summary>
    /// Exception on extracting the commandtext
    /// </summary>
    public class CommandTextException : System.Exception
    {
        public CommandTextException(string message)
            : base(message)
        { }
    }
}
