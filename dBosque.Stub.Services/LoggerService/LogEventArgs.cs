using dBosque.Stub.Services.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace dBosque.Stub.Services.LoggerService
{
    /// <summary>
    /// Event args to intercept the logging messages
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        internal LogEventArgs(LogLevel sev, string msg)
        {
            Severity = sev;
            Message = msg.Pretty();
        }

        public LogLevel Severity { get; private set; }
        public string Message { get; private set; }


        ///<summary>Returns a string that represents the current object.</summary>
        ///<returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Severity.ToString()} : {Message}";
        }
    }
}
