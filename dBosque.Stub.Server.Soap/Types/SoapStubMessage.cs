using dBosque.Stub.Interfaces;
using dBosque.Stub.Services;
using dBosque.Stub.Services.Extensions;
using dBosque.Stub.Services.Types;
using dBosque.Stub.Server.Soap.Interface;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace dBosque.Stub.Server.Soap.Types
{
    public class SoapStubMessage : StubMessage<Message>
    {
        private MessageVersion Version { get; set; }
        
        private Message Message { get; set; }

        private MessageBuffer Buffer { get; set; }

        /// <summary>
        /// Create a new StubMessage class based on a WCF message
        /// </summary>
        /// <param name="msg"></param>
        public SoapStubMessage(Message msg, HttpContext context, string tenant ) 
            : base(tenant, 200, ContentTypes.ApplicationXml)
        {
            var connectionInfo = context?.Connection;
            Sender = $"{connectionInfo?.RemoteIpAddress}:{connectionInfo?.RemotePort}";

            Buffer = msg.CreateBufferedCopy(8192);
            Message = Buffer.CreateMessage();
            ParseMessage();
        }

        private void ParseMessage()
        {
            var xrdr = Message.GetReaderAtBodyContents();
            Action = Message.Headers.Action;
            RootNameSpace = xrdr.NamespaceURI;
            RootNode = xrdr.LocalName;
            var body = Request = RawRequest = xrdr.ReadOuterXml();
            Request = Message.ToString();
            Request = Request.Replace("... stream ...", body);
            Version = Message.Version;
        }

        ///<summary>
        ///Return a valid UnAuthorized message
        ///</summary>
        ///<returns></returns>
        public override Message AsUnauthorized()
        {
            throw new StubErrorException(System.Net.HttpStatusCode.Unauthorized);
        }

        ///<summary>
        ///Return a valid result message
        ///</summary>
        ///<returns></returns>
        public override Message AsResult()
        {
            string messageStream = string.IsNullOrEmpty(Response)?"<empty/>":Response;
            if (!HasMatch && !IsPassTrough)
                messageStream = SerializeToString(AsFault());

            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(messageStream));
            var reader = XmlReader.Create(memoryStream, new XmlReaderSettings() { XmlResolver = null, DtdProcessing = DtdProcessing.Prohibit });
            if (HttpStatusCode >= 300)
                throw new StubErrorException(Convert(), Message.CreateMessage(Version, Action, reader));

            return Message.CreateMessage(Version, Action, reader);
        }
        /// <summary>
        /// Generate a MatchList based on all matches
        /// </summary>
        /// <returns></returns>
        private List<Match> ToMatchList()
        {
            var matches = new List<Match>();
            foreach (var m in Matches)
            {
                var match = new Match(m.Description);
                m.Items.ForEach(x => match.XPath.Add(new XPath(x.Expression, x.Value)));
                matches.Add(match);
            }
            return matches;
        }

        private static XmlElement GetElement(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc.DocumentElement;
        }

        /// <summary>
        /// Create a faultcontract
        /// </summary>
        /// <returns></returns>
        private StubFaultContract AsFault()
        {
            StubFaultContract result = null;

            if (!HasMatch && Matches.Count == 0)
                result = new StubFaultContract()
                {
                    Request = Request == null ? null : GetElement(Request),
                    Message = Matches.Error
                };

            else if (!HasMatch && Matches.Count > 0)
                result = new StubFaultContract()
                {
                    Message = "Multiple matches found.",
                    Request = GetElement(Request),
                    Matches = ToMatchList()
                };

            else if (HasMatch)
                result = new StubFaultContract()
                {
                    Message = "One match found.",
                    Request = GetElement(Request),
                    Matches = ToMatchList()
                };

            return result;
        }

        ///<summary>
        ///Localize a xpath
        ///</summary>
        ///<param name="xpath"></param>
        ///<returns></returns>
        public override string LocalizeXpath(string xpath)
        {
            return xpath.AppendSoapEnvelope();
        }

        private string SerializeToString<T>(T obj, string request = null)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            MemoryStream memoryStream = new MemoryStream();
            serializer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;
            StreamReader r = new StreamReader(memoryStream);
            string reader = r.ReadToEnd();
            if (request != null)
                reader = reader.Replace("<Request i:nil=\"true\"/>", $"<Request>{request}</Request");
            
            return reader;
        }

        ///<summary>
        ///Relay the message to the specific uri.
        ///</summary>
        ///<param name="uri"></param>
        public override void Relay(string uri)
        {
            Message = Buffer.CreateMessage();
            // Remove all headers.
            Message.Headers.ToList().ForEach(a => Message.Headers.RemoveAll(a.Name, a.Namespace));
            Message.Headers.RemoveAll("To", "http://schemas.microsoft.com/ws/2005/05/addressing/none");

            ParseMessage();
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            UpdateHeaderProperties(webRequest);
            webRequest.Method = "POST";
            webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(Request);
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
            try
            {
                using (WebResponse response = webRequest.GetResponse())
                {
                    Response = ParseResponse(response);
                }
            }
            catch (WebException ex)
            {
                Response = ParseResponse(ex.Response);
            }

            Matches = new StubMatchList();
        }

        private string ParseResponse(WebResponse response)
        {
            using (StreamReader rd = new StreamReader(response.GetResponseStream()))
            {
                string soapResult = rd.ReadToEnd();
                XDocument xDoc = XDocument.Load(new StringReader(soapResult));

                var unwrappedResponse = xDoc.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body")
                    .First()
                    .FirstNode;
                return unwrappedResponse.ToString();
            }
        }

        private string GetHeaderValue(WebHeaderCollection headers, string key, string def = null)
        {
            return headers.AllKeys.Contains(key) ? headers[key] : def;
        }

        private void UpdateHeaderProperties(HttpWebRequest request)
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            if (Message.Properties.ContainsKey(HttpRequestMessageProperty.Name))
            {
                headers = (Message.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty).Headers;
                foreach (var header in headers.AllKeys)
                {
                    try
                    {
                        request.Headers.Add(header, headers[header]);
                    }
                    catch
                    {
                        // catch all
                    }
                }
            }

            request.ContentType = GetHeaderValue(headers, "ContentType", $"{ContentTypes.TextXml};charset=\"utf-8\"");
            request.Accept = GetHeaderValue(headers, "Accept", ContentTypes.TextXml);           
        }
      

    }
}
