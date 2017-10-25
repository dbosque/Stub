using System;
using System.Net;
using System.ServiceModel.Channels;

namespace dBosque.Stub.Server.Soap.Interface
{
    public class StubErrorException : ApplicationException
    {        
        public HttpStatusCode Code { get; private set; }
        public Message SoapMessage { get; private set; }
        public bool SuppressBody { get; private set; }
        public StubErrorException(HttpStatusCode code, Message message = null) : base("")
        {
            Code = code;
            if (Code == HttpStatusCode.Unauthorized)
                SuppressBody = true;

            SoapMessage = message;
        }
    }
}
