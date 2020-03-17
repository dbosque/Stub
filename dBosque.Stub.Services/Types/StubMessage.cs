using dBosque.Stub.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace dBosque.Stub.Services
{
    /// <summary>
    /// A generic base implementation of a stubmessage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class StubMessage<T> : IStubMessage<T>
    {

        /// <summary>
        /// The error description of the message
        /// </summary>
        public string Description
        {
            get
            {
                List<string> items = new List<string>();
          
                if (!string.IsNullOrEmpty(RootNameSpace))
                    items.Add($"namespace : '{RootNameSpace}'");
                if (!string.IsNullOrEmpty(RootNode))
                    items.Add($"rootnode : '{RootNode}'");
                if (!string.IsNullOrEmpty(Uri))
                    items.Add($"uri : '{Uri}'");

                return $"{string.Join(",", items)}";
            }
        }

        ///<summary>
        ///The original sender of the message
        ///</summary>
        public string Sender { get; set; }

        ///<summary>
        ///The (log) id of this message
        ///</summary>
        public MessageId Id => new MessageId() { Sender = Sender, Namespace = RootNameSpace, Rootnode = RootNode, Uri = Uri };

        /// <summary>
        /// The action that was performed
        /// </summary>
        public string Action            { get; set; }
      
        ///<summary>
        ///The actual request received
        ///</summary>
        public string Request           { get; set; }
        
        ///<summary>
        ///The configured response to return (if any)
        ///</summary>
        public string Response          { get; set; }

        public Dictionary<string, string[]> ResponseHeaders { get; set; }
        
        ///<summary>
        ///The rootnode extracted from the request
        ///</summary>
        public string RootNode          { get; set; }
       
        ///<summary>
        ///The root namespace extracted from the request.
        ///</summary>
        public string RootNameSpace     { get; set; }
        
        ///<summary>
        ///A list of found matches
        ///</summary>
        public StubMatchList Matches    { get; set; }
        
        ///<summary>
        ///The statuscode to return
        ///</summary>      
        public int HttpStatusCode       { get; set; }

        ///<summary>
        ///The contenttype to return
        ///</summary>
        public string ContentType       { get; set; }

        ///<summary>
        ///The ID of the tenant on which the request was received.
        ///</summary>
        public long? TenantId            { get; set; }

        ///<summary>
        ///The Raw request
        ///</summary>
        public string RawRequest         { get; set; }

        ///<summary>
        ///Indicator if for this message(type) a passthrough is configured
        ///</summary>
        public bool IsPassTrough { get; set; }

        ///<summary>
        ///The URL to pass the request forward to.
        ///</summary>
        public string PassthroughUri { get; set; }

        ///<summary>
        ///Indicator to signal if any match was found or not.
        ///</summary>
        public bool HasMatch => Matches.HasMatch;

        ///<summary>
        ///Indicator to signal if there are multiple matches found.
        ///</summary>
        public bool HasMultipleMatches => Matches.Count > 1;

        ///<summary>
        ///The URI on which the request was received
        ///</summary>
        public string Uri { get; set; }

        ///<summary>
        ///The tenant on which the request was received
        ///</summary>
        public string Tenant { get; set; }

        ///<summary>
        ///Relay the message to the specific uri.
        ///</summary>
        ///<param name="uri"></param>
        public abstract void Relay(string uri);        

        ///<summary>
        ///Localize a xpath
        ///</summary>
        ///<param name="xpath"></param>
        ///<returns></returns>
        public virtual string LocalizeXpath(string xpath)
        {
            // Default do nothing
            return xpath;
        }

        /// <summary>
        /// Indicator to signal a soapmessage or not
        /// </summary>
        private bool IsSoapMessage
            =>
                string.Compare(RootNameSpace, "http://schemas.xmlsoap.org/soap/envelope/",
                    StringComparison.OrdinalIgnoreCase) == 0 ||
            ContentType.IndexOf("soap", 0, StringComparison.OrdinalIgnoreCase) != -1;

        ///<summary>
        ///Should any postprocessing be done.
        ///</summary>
        public bool hasPostProcessing => Response?.Contains("xsl:stylesheet")??false;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="tenant"></param>
        protected StubMessage(string tenant, int defaultStatusCode, string defaultContentType)
        {
            Tenant = tenant;
            RootNameSpace = string.Empty;
            RootNode = string.Empty;
            RawRequest = string.Empty;
            Request = string.Empty;
            Matches = new StubMatchList();
            HttpStatusCode = defaultStatusCode;
            ContentType = defaultContentType;
        }
       
        /// <summary>
        /// Apply the stream to the message.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="message"></param>
        protected static void ApplyDocument(MemoryStream stream, IStubMessage<T> message)
        {
            var xrdr = XmlDictionaryReader.CreateDictionaryReader(XmlReader.Create(stream, new XmlReaderSettings() { XmlResolver = null, DtdProcessing = DtdProcessing.Prohibit }));
            xrdr.MoveToContent();
            message.RootNameSpace = xrdr.NamespaceURI;
            message.RootNode = xrdr.LocalName;
            message.Request = xrdr.ReadOuterXml();
        }


        /// <summary>
        /// Return a valid result message
        /// </summary>
        /// <returns></returns>
        public abstract T AsResult();

        /// <summary>
        /// Return a valid UnAuthorized message
        /// </summary>
        /// <returns></returns>
        public abstract T AsUnauthorized();


        /// <summary>
        /// Convert the statuscode integer to a real statuscode
        /// </summary>
        /// <returns></returns>
        public HttpStatusCode Convert()
        {
            switch (HttpStatusCode)
            {
                case 200:
                    return System.Net.HttpStatusCode.OK;
                case 302:
                    return System.Net.HttpStatusCode.Redirect;
                case 201:
                    return System.Net.HttpStatusCode.Created;
                case 401:
                    return System.Net.HttpStatusCode.Unauthorized;
                case 409:
                    return System.Net.HttpStatusCode.Conflict;
                case 500:
                    return System.Net.HttpStatusCode.InternalServerError;
                default:
                    return (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), HttpStatusCode.ToString());

            }
        }
    }    
}