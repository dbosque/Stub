using dBosque.Stub.Interfaces;
using dBosque.Stub.Services;
using dBosque.Stub.Services.Extensions;
using dBosque.Stub.Services.Types;
using dBosque.Stub.Server.WebApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace dBosque.Stub.Server.WebApi.Types
{
    public class ApiStubMessage : StubMessage<IActionResult>
    {
        private readonly HttpRequest _httpRequest;
        private readonly Controller _controller;
        public ApiStubMessage(HttpRequest httpRequest, string uri, Controller controller, string tenant)
            : base(tenant, 200, ContentTypes.ApplicationJson)
        {
            _controller = controller;
            _httpRequest = httpRequest;     
            Uri = uri;
            var connectionInfo = httpRequest?.HttpContext?.Connection;
            Sender = $"{connectionInfo?.RemoteIpAddress}:{connectionInfo?.RemotePort}";

            // Override with content if exists.
            ParseContentAndHeaders();

            Action = uri;                      
        }

        /// <summary>
        /// Parse the message content and headers
        /// </summary>
        private void ParseContentAndHeaders()
        {
            var headers = _httpRequest.GetTypedHeaders();
            // If accept supports application/xml, let that one be the default.
            if (headers.Accept != null && headers.Accept.Any())
                ContentType = headers.Accept.Any(a => a.MediaType.Value == ContentTypes.ApplicationXml) 
                    ? ContentTypes.ApplicationXml: headers.Accept.FirstOrDefault()?.MediaType.Value;
            if (ContentType == ContentTypes.All)
                ContentType = ContentTypes.ApplicationJson;

            if (headers.ContentLength <= 0)
                return;

            var content = _httpRequest.GetBody();
            ContentType = headers.ContentType?.MediaType.Value ?? ContentType;
            RawRequest = content;
            var doc = content.CreateDocument(true);
            if (doc != null)
            {
                using (var memStream = new MemoryStream(Encoding.UTF8.GetBytes(doc.OuterXml)))
                {
                    ApplyDocument(memStream, this);
                }
            }
        }


        ///<summary>
        ///Return a valid UnAuthorized message
        ///</summary>
        ///<returns></returns>
        public override IActionResult AsUnauthorized()
        {
            return new UnauthorizedResult();
        }

        ///<summary>
        ///Return a valid result message
        ///</summary>
        ///<returns></returns>
        public override IActionResult AsResult()
        {
            if (HasMultipleMatches)
                return new StatusCodeResult(409);

            if (HasMatch || IsPassTrough)
            {
                return new ContentResult()
                {
                    Content = Response,
                    ContentType = ContentType,
                    StatusCode = HttpStatusCode
                };              
            }
            if (string.IsNullOrEmpty(Matches.Error))
                return new NotFoundResult();
            else
                return new ContentResult()
                {
                    Content = Matches.Error,
                    ContentType = ContentTypes.TextPlain,
                    StatusCode = 500
                };
        }

        ///<summary>
        ///Relay the message to the specific uri.
        ///</summary>
        ///<param name="uri"></param>
        public override void Relay(string uri)
        {
            HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create(uri + Uri);

            newRequest.ContentType = ContentType;
            newRequest.Method = _httpRequest.Method;
            newRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            foreach (var headerKey in _httpRequest.Headers.Where(a => !WebHeaderCollection.IsRestricted(a.Key)).Select(a => a.Key))
                newRequest.Headers[headerKey] = string.Join(",",_httpRequest.Headers.Where(a => a.Key == headerKey).SelectMany(a => a.Value).ToArray());
            var headers = _httpRequest.GetTypedHeaders();
            if (headers.ContentLength > 0)
            {
                byte[] originalStream = Encoding.UTF8.GetBytes(_httpRequest.GetBody());
                using (Stream reqStream = newRequest.GetRequestStream())
                {
                    reqStream.Write(originalStream, 0, originalStream.Length);
                    reqStream.Close();
                }
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)newRequest.GetResponse();
                ContentType = ContentTypes.First(response.ContentType);
                HttpStatusCode = (int)response.StatusCode;
                var reader = new StreamReader(response.GetResponseStream());
               
                ResponseHeaders = new Dictionary<string, string[]>();
                foreach (var headerKey in response.Headers.AllKeys.Where(a => !WebHeaderCollection.IsRestricted(a)).Select(a => a))
                    ResponseHeaders.Add(headerKey, response.Headers.GetValues(headerKey));

                Response = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                ContentType = ContentTypes.First(response.ContentType);
                HttpStatusCode = (int)response.StatusCode;
                var reader = new StreamReader(response.GetResponseStream());
                Response = reader.ReadToEnd();
            }
            Matches = new StubMatchList();
        }
    }
}
