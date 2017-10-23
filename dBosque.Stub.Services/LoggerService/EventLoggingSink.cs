using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using System;
using System.IO;

namespace dBosque.Stub.Services.LoggerService
{
    class EventLoggingSink : ILogEventSink
    {
        readonly ITextFormatter _formatter;

        public EventLoggingSink(ITextFormatter formatter)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        ///<summary>
        ///Emit the provided log event to the sink.
        ///</summary>
        ///<param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            using (var buffer = new StringWriter())
            {
                _formatter.Format(logEvent, buffer);
                LogWatcher.Notify(new LogEventArgs((LogLevel)logEvent.Level, buffer.ToString()));
            }
        }
    }
}
