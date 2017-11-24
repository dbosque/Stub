using System;

namespace dBosque.Stub.Services
{
    /// <summary>
    /// Disposable timer
    /// </summary>
    internal class Timer : System.Diagnostics.Stopwatch, IDisposable
    {
        public Timer()
            : base()
        {
            Start();
        }

        ///<summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            if (IsRunning)
                Stop();
        }
    }
}
