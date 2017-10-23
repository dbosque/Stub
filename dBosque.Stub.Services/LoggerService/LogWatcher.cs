using System;

namespace dBosque.Stub.Services.LoggerService
{
    public static class LogWatcher
    {
        /// <summary>
        /// Intercept the messages
        /// </summary>
        public static event Action<LogEventArgs> OnEvent;

        /// <summary>
        /// Notify all interested parties.
        /// </summary>
        /// <param name="args"></param>
        internal static void Notify(LogEventArgs args)
        {
            OnEvent?.Invoke( args);
        }
    }

}
