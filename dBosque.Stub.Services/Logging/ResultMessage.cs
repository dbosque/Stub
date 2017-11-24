using dBosque.Stub.Interfaces;

namespace dBosque.Stub.Services.Logging
{
    public class ResultMessage<TResult> : LogMessage<TResult>
    {

        public ResultMessage(IStubMessage<TResult> msg, long elapsed)
            : base(msg)
        {

            Duration = elapsed;
            Direction = (msg.IsPassTrough ? "Out Passthrough" : "Out");
        }

        public long Duration { get; set; }
    }
}
