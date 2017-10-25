using dBosque.Stub.Interfaces;
using dBosque.Stub.Server.Soap.Interface;
using dBosque.Stub.Server.Soap.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace dBosque.Stub.Server.Soap.Middleware
{
    public class SoapEndpointMiddleware
    {
        // The middleware delegate to call after this one finishes processing
        private readonly RequestDelegate _next;
        private readonly MessageEncoder _messageEncoder;
        private readonly IStubHandler<Message> _handler;

        public SoapEndpointMiddleware(IStubHandler<Message> handler,  RequestDelegate next,  MessageEncoder encoder)
        {
            _messageEncoder = encoder;
            _next = next;
            _handler = handler;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.ContentLength > 0)
            {
                try
                {
                    var requestMessage = _messageEncoder.ReadMessage(httpContext.Request.Body, 0x10000, httpContext.Request.ContentType);

                    var tenant = ValOrDefault(httpContext.Request.Query, "tenant", "__default");
                    var passthroughUrl = ExtractPassthroughUrl(httpContext);

                    Message responseMessage;

                    if (string.IsNullOrEmpty(passthroughUrl))
                        responseMessage = _handler.HandleMessage(new SoapStubMessage(requestMessage, httpContext, tenant));
                    else
                        responseMessage = _handler.HandlePassthrough(new SoapStubMessage(requestMessage, httpContext, tenant), passthroughUrl);

                    WriteMessage(httpContext, responseMessage);
                }
                catch (StubErrorException ex)
                {
                    WriteMessage(httpContext, ex.SoapMessage, (int)ex.Code);
                }
                catch (Exception ex)
                {                    
                    WriteMessage(httpContext, ReturnAsError(ex.Message), 500);
                }
            }
            else
                // Call the next middleware delegate in the pipeline 
                await _next.Invoke(httpContext);
        }

        private void WriteMessage(HttpContext context, Message message, int statusCode = 200)
        {
            context.Response.ContentType = context.Request.ContentType;
            context.Response.Headers["SOAPAction"] = context.Request.Headers["SOAPAction"];
            context.Response.StatusCode = statusCode;
            _messageEncoder.WriteMessage(message, context.Response.Body);
        }

        private string ExtractPassthroughUrl(HttpContext httpContext)
        {
            var protocol = ValOrDefault(httpContext.Request.Query, "protocol", null);
            var passthroughUrl = ValOrDefault(httpContext.Request.Query, "passthrough", string.Empty);
            if (!string.IsNullOrEmpty(protocol))
                passthroughUrl = $"{protocol ?? ""}://{passthroughUrl.Replace(":", "/")}";
            return passthroughUrl;
        }
        private Message ReturnAsError(string message)
        {
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes($"<error>{message}</error>"));
            var reader = XmlReader.Create(memoryStream, new XmlReaderSettings() { XmlResolver = null, DtdProcessing = DtdProcessing.Prohibit });
         
            return Message.CreateMessage(_messageEncoder.MessageVersion, "error", reader);
        }


        private string ValOrDefault(IQueryCollection query, string key, string def)
        {
            if (query.ContainsKey(key))
                return query[key];
            return def;
        }
    }
}
