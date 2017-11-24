using dBosque.Stub.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace dBosque.Stub.Services.Logging
{
    public static class LogMessageExtensions
    {
        public static LogMessage<TResult> Success<TResult>(this IStubMessage<TResult> msg, long elapsed )
        {
            return new SuccessMessage<TResult>(msg, elapsed);
        }

        public static LogMessage<TResult> Failure<TResult>(this IStubMessage<TResult> msg, long elapsed)
        {
            return new FailureMessage<TResult>(msg, elapsed);
        }

        public static LogMessage<TResult> Info<TResult>(this IStubMessage<TResult> msg)
        {
            return new LogMessage<TResult>(msg);
        }
    }
}
