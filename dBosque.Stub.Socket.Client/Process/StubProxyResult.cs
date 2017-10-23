namespace dBosque.Stub.Socket.Client.Process
{
    /// <summary>
    /// The result of a proxy call
    /// </summary>
    /// <typeparam name="T">The datatype to return</typeparam>
    public class StubProxyResult<T>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        internal StubProxyResult(T data, string message = null, int? code = null)
        {
            Data = data;
            Success = string.IsNullOrEmpty(message);
            ErrorMessage = message;
            ErrorCode = code;

            // If the errorcode equals the errormessage, append some extra info
            if (ErrorCode?.ToString() == ErrorMessage)
                ErrorMessage = $"Stub returned errorcode : {ErrorCode}";
        }

        /// <summary>
        /// The actual returned data.
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// Stub has been successfull
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// An errormessage in case of an error
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// ErrorCode if any
        /// </summary>
        public int? ErrorCode { get; set; }
    }
}
