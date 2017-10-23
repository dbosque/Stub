
namespace dBosque.Stub.Interfaces
{
    public interface IStubHandler<TResult>
    {
        /// <summary>
        /// Handle a message that has been marked as passthrough
        /// </summary>
        /// <param name="message">The message to handle</param>
        /// <param name="passthroughUrl">The passthroughUri</param>
        /// <returns>The found result</returns>
        TResult HandlePassthrough(IStubMessage<TResult> message, string passthroughUrl);

        /// <summary>
        /// Handle a message 
        /// </summary>
        /// <param name="message">The message to nahdle </param>
        /// <returns>The mocked result</returns>
        TResult HandleMessage(IStubMessage<TResult> message);
    }
}
