using dBosque.Stub.Interfaces;
using dBosque.Stub.Services.Types;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace dBosque.Stub.Services.XSLT
{
    public class XSLTPostProcessor
    {

        /// <summary>
        /// Execute the XSLT post processing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message to process</param>
        public void Execute<T>(IStubMessage<T> message)
        {
            try
            {
                XslCompiledTransform transform = new XslCompiledTransform();
                using (XmlReader reader = XmlReader.Create(new StringReader(message.Response)))
                {
                    transform.Load(reader);
                }
                using (StringWriter results = new StringWriter())
                {
                    using (XmlReader reader = XmlReader.Create(new StringReader(message.Request)))
                    {
                        transform.Transform(reader, null, results);
                    }
                    message.Response = results.ToString();
                }
                if (message.ContentType == ContentTypes.ApplicationJson)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(message.Response);
                    message.Response = JsonConvert.SerializeXmlNode(doc);
                }
            }
            catch (Exception ex)
            {
                message.Matches = new StubMatchList()
                {
                    Error = ex.Message
                };
            }
        }
    }
}
