using dBosque.Stub.Interfaces;

namespace dBosque.Stub.Services.Logging
{
    public class SuccessMessage<TResult> : ResultMessage<TResult>
    {
        public SuccessMessage(IStubMessage<TResult> msg, long elapsed)
            : base(msg, elapsed)
        {
            ContentType = msg.ContentType;
            StatusCode = msg.HttpStatusCode;
        }
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
    }
}
