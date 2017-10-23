using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace dBosque.Stub.Services.Extensions
{
    public static class XmlDocumentExtensions
    {
        /// <summary>
        /// Get all valid xpaths for a given document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        /// <param name="stripSoapEnvelope"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllValidFor<T>(this System.Xml.XPath.IXPathNavigable document, IEnumerable<T> collection, Func<T, string> filter, bool stripSoapEnvelope = true)
        {
            // Als er een document geladen is, filteren, anders gewoon zo laten
            if (document != null)
            {
                System.Xml.XPath.XPathNavigator navigator = document.CreateNavigator();
                foreach (var p in collection)
                {
                    System.Xml.XPath.XPathNavigator n = null;
                    try
                    {
                        var exp = filter(p);
                        if (!stripSoapEnvelope && !exp.StartsWith("/*[local-name()='Envelope']", StringComparison.Ordinal))
                            exp = @"/" + exp;
                        n = navigator.SelectSingleNode(exp);
                    }
                    catch
                    {
                        // Geen catch,
                        // Exception betekend dat de Expression niet gevonden kon worden.
                        // Dus niet opnemen in de lijst.
                    }

                    // Indien de Expression gevonden kan worden, toevoegen aan de lijst.
                    if (n != null)
                        yield return p;
                }
            }
        }

        /// <summary>
        /// Get the documentinfo from a MessageType based on wsdl or Swagger
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DocumentInfo GetDocumentInfo(this ExternalReferenceResolvers.ExternalMessageType info)
        {
            DocumentInfo i = new DocumentInfo();
            if (!string.IsNullOrEmpty(info.Request))
                i = info.Request.GetDocumentInfo();
            if (!string.IsNullOrEmpty(info.RegEx))
            {
                i.Namespace = info.RegEx;
                i.RootNode = string.Empty;
            }

            return i;
        }

        /// <summary>
        /// Get the documentinfo from a string message content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="strip"></param>
        /// <returns></returns>
        public static DocumentInfo GetDocumentInfo(this string content, bool strip = true)
        {
            try
            {
                var doc = content.CreateDocument(strip);
                return new DocumentInfo()
                {Document = doc, Namespace = doc.DocumentElement.NamespaceURI, RootNode = doc.DocumentElement.LocalName};
            }
            catch (Exception)
            {
                return new DocumentInfo();
            }
        }

        /// <summary>
        /// Convert an XDocument to a XMLDocument
        /// </summary>
        /// <param name = "xDocument"></param>
        /// <returns></returns>
        private static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }

            return xmlDocument;
        }

        /// <summary>
        /// Create a xmlDocument from a string. The string can be xml or json
        /// </summary>
        /// <param name = "content"></param>
        /// <returns></returns>
        public static XmlDocument CreateDocument(this string content, bool stripEnvelope = true)
        {
            if (string.IsNullOrEmpty(content))
                content = null;
            // Load the XML file. 
            var dom = new XmlDocument();
            try
            {
                // First XML
                dom.LoadXml(content);
            }
            catch (Exception)
            {
                try
                {
                    // Json?
                    dom = JsonConvert.DeserializeXNode(content).ToXmlDocument();
                }
                catch (JsonException)
                {
                    try
                    {
                        // Json with an unkown root?
                        dom = JsonConvert.DeserializeXNode(content, "root", true).ToXmlDocument();
                    }
                    catch (JsonException)
                    {
                        try
                        {
                            // Json with an unknown root but another way of checking
                            dom = JsonConvert.DeserializeXNode("{\"root\":" + content + "}").ToXmlDocument();
                        }
                        catch (Exception)
                        {
                            throw new DataMisalignedException($"Invalid data {content}");
                        }
                    }
                }
                catch (Exception)
                {
                    throw new DataMisalignedException($"Invalid data {content}");
                }
            }

            // Strip soap envelope if any
            if (stripEnvelope)
                dom.StripSoapEnvelope();
            return dom;
        }

        /// <summary>
        /// Strip the soapenvelope
        /// </summary>
        /// <param name = "document"></param>
        /// <returns></returns>
        private static XmlDocument StripSoapEnvelope(this XmlDocument document)
        {
            var soapbody = document.SelectSingleNode("/*[local-name()='Envelope']/*[local-name()='Body']");
            if (soapbody != null)
                document.LoadXml(soapbody.FirstChild.OuterXml);
            return document;
        }

        /// <summary>
        /// Retrieve the Xpath value from an XMLNode
        /// </summary>
        /// <param name = "node"></param>
        /// <returns></returns>
        public static string FindXPath(this XmlNode node)
        {
            StringBuilder builder = new StringBuilder();
            while (node != null)
            {
                string name = GetNodeName(node);
                switch (node.NodeType)
                {
                    case XmlNodeType.Attribute:
                        builder.Insert(0, "/@" + name);
                        node = ((XmlAttribute)node).OwnerElement;
                        break;
                    case XmlNodeType.Element:
                        int index = GetIndex(node);
                        if (index > 0)
                            builder.Insert(0, "/*[local-name()='" + name + "'][" + index + "]");
                        else
                            builder.Insert(0, "/*[local-name()='" + name + "']");
                        node = node.ParentNode;
                        break;
                    case XmlNodeType.Document:
                        return builder.ToString();
                    default:
                        throw new ArgumentException("Only elements and attributes are supported");
                }
            }

            throw new ArgumentException("Node was not in a document");
        }

        /// <summary>
        /// Get the index count of a specific node in an array
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static int GetIndex(XmlNode node)
        {
            bool hasSiblings = (node.PreviousSibling != null || node.NextSibling != null);
            int index = 0;
            if (hasSiblings)
            {
                XmlNode currNode = node;
                while (currNode != null)
                {
                    if (currNode.PreviousSibling != null)
                    {
                        if (GetNodeName(currNode.PreviousSibling) == GetNodeName(currNode))
                        {
                            index++;
                        }
                    }

                    currNode = currNode.PreviousSibling;
                }
            }

            return index;
        }

        /// <summary>
        /// Get the nodename of a xmlnode
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetNodeName(XmlNode node)
        {
            string name = node.Name;
            // Meer dan 1 :
            string[] splitted = name.Split(':');
            if (splitted.Length > 1)
            {
                name = splitted[splitted.Length - 1];
            }

            return name;
        }
    }
}