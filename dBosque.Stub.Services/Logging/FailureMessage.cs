using dBosque.Stub.Interfaces;

namespace dBosque.Stub.Services.Logging
{
    public class FailureMessage<TResult> : ResultMessage<TResult>
    {
        public FailureMessage(IStubMessage<TResult> msg, long elapsed)
            : base(msg, elapsed)
        {
            Raw = msg.RawRequest;
            ErrorMessage = msg.Matches.Error;
        }
        public string ErrorMessage { get; set; }
        public string Raw { get; set; }
    }
}
