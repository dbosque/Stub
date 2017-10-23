using dBosque.Stub.Services.LoggerService;

// Different namepsace!!
namespace Microsoft.Extensions.Logging
{
    public static class EventLoggerExtensions
    {
        /// <summary>
        /// Add event logging
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static ILoggerFactory AddEventLogging(this ILoggerFactory factory)
        {
            factory.AddProvider(new EventLoggerProvider());
            return factory;
        }
    }
}
