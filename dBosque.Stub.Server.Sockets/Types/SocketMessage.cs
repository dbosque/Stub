using dBosque.Stub.Services;
using dBosque.Stub.Services.Extensions;
using dBosque.Stub.Services.Types;
using System.Text;

namespace dBosque.Stub.Server.Sockets.Types
{
    public class SocketMessage : StubMessage<string>
    {
        public SocketMessage(string sender, string msg) 
            : base("__default", 200, ContentTypes.ApplicationXml)
        {
            Sender = sender;
            RawRequest = msg;
            var doc = msg.CreateDocument(true);
            if (doc != null)
            {
                using (var memStream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(doc.OuterXml)))
                {
                    ApplyDocument(memStream, this);
                }
            }
            Action = "socket";
        }

        ///<summary>
        ///Return a valid result message
        ///</summary>
        ///<returns></returns>
        public override string AsResult()
        {
            if (HasMultipleMatches)
                return "";

            if (HasMatch)
            {
                if (Convert() != System.Net.HttpStatusCode.OK)
                {
                    return HttpStatusCode.ToString();
                }
                return Response;
            }
            return "404";
        }

        ///<summary>
        ///Return a valid UnAuthorized message
        ///</summary>
        ///<returns></returns>
        public override string AsUnauthorized()
        {
           return  "409";
        }

        ///<summary>
        ///Relay the message to the specific uri.
        ///</summary>
        ///<param name="uri"></param>
        public override void Relay(string uri)
        {
            // nothing, we can not relay a socker message
        }
    }
}
