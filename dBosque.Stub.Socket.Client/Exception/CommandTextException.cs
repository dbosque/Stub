using System;

namespace dBosque.Stub.Socket.Client.Exception
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
