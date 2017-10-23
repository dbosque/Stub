using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using System;

namespace dBosque.Stub.Services.LoggerService
{
    public static class EventLoggerConfigurationExtensions
    {
        const string DefaultDebugOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

        public static LoggerConfiguration Event(
           this LoggerSinkConfiguration sinkConfiguration,
           LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
           string outputTemplate = DefaultDebugOutputTemplate,
           IFormatProvider formatProvider = null,
           LoggingLevelSwitch levelSwitch = null)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException(nameof(sinkConfiguration));
            if (outputTemplate == null) throw new ArgumentNullException(nameof(outputTemplate));

            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            return sinkConfiguration.Event(formatter, restrictedToMinimumLevel, levelSwitch);
        }

        public static LoggerConfiguration Event(
         this LoggerSinkConfiguration sinkConfiguration,
         ITextFormatter formatter,
         LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
         LoggingLevelSwitch levelSwitch = null)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException(nameof(sinkConfiguration));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            return sinkConfiguration.Sink(new EventLoggingSink(formatter), restrictedToMinimumLevel, levelSwitch);
        }
    }
}
