using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace dBosque.Stub.Services.LoggerService
{
    public class EventLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, EventLogger> _loggers = new ConcurrentDictionary<string, EventLogger>();

        public EventLoggerProvider()
        {            
        }

        ///<summary>
        ///Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.
        ///</summary>
        ///<param name="categoryName">The category name for messages produced by the logger.</param>
        ///<returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new EventLogger(name));
        }

        ///<summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
