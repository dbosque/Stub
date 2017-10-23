using Microsoft.Xml.XMLGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace dBosque.Stub.Services.ExternalReferenceResolvers
{
    public class WSDLResolver : ExternalResolverBase
    {
        const string WSDLns = "http://schemas.xmlsoap.org/wsdl/";
        const string SOAPns = "http://schemas.xmlsoap.org/wsdl/soap/";

        XName serviceName   = XName.Get("service", WSDLns);
        XName portName      = XName.Get("port", WSDLns);
        XName portTypeName  = XName.Get("portType", WSDLns);
        XName operationName = XName.Get("operation", WSDLns);
        XName inputName     = XName.Get("input", WSDLns);
        XName messageName   = XName.Get("message", WSDLns);
        XName partName      = XName.Get("part", WSDLns);
        XName documentName  = XName.Get("documentation", WSDLns);
        XName addressName   = XName.Get("address", SOAPns);

        ///<summary>
        ///Extract the messageTypes from the given content
        ///</summary>
        ///<param name="content">The content to parse</param>
        ///<returns></returns>
        protected override IEnumerable<ExternalMessageType> FromContent(string content)
        {
            var doc = XDocument.Parse(content);
            return ExtractInfo(doc).Distinct();
        }

        ///<summary>
        ///Extract the messageTypes from the given uri
        ///</summary>
        ///<param name="uri">The url to retrieve</param>
        ///<returns></returns>
        protected override IEnumerable<ExternalMessageType> FromUri(string uri)
        {
            var doc = XDocument.Load(uri);
            return ExtractInfo(doc);
        }

        private IEnumerable<ExternalMessageType> ExtractInfo(XDocument doc)
        {            
            Dictionary<string, string> namespaces = new Dictionary<string, string>();
            Dictionary<XmlQualifiedName, string> samples = new Dictionary<XmlQualifiedName, string>();

            // Fill namespace table
            (doc.FirstNode as XElement).Attributes()
                    .Where(a => a.IsNamespaceDeclaration)
                    .ToList()
                    .ForEach(a => namespaces.Add(a.Name.LocalName, a.Value));

            XmlSchemaSet schemaset = ExtractSchemaSet(doc, namespaces);
            foreach (XmlSchema schema in schemaset.Schemas())
                ExtractSamples(schema, samples);

            // Get url
            var url = doc.Descendants(serviceName)
                            .Descendants(portName)
                                .Descendants(addressName)
                                    .Select(a => a.Attribute("location").Value)
                                        .FirstOrDefault();

            // Get operations
            var operations = doc.Descendants(portTypeName)
                                    .Descendants(operationName)
                                        .Select(a => a)
                                            .ToList();

            foreach (XElement op in operations)
            {
                var message = new ExternalMessageType();
                // Operation name
                message.Description = op.Attribute("name").Value;
                message.Uri = url;

                // Get the optional documentation field
                message.Description = op.Descendants()
                                        .FirstOrDefault()?
                                        .Value ?? message.Description;
                
                // Get input parameter object
                var input = ToXName(op.Descendants(inputName)
                                .Attributes("message")
                                    .FirstOrDefault()?
                                        .Value, namespaces);

                 // Get the input definition part
                 var part = doc.Descendants(messageName)
                                .FirstOrDefault(a => a.Attribute("name").Value == input.Name)
                                    .Descendants(partName)
                                        // It could also be a simple type
                                        .Select(a => a.Attribute("element")?.Value)
                                            .FirstOrDefault();

                // Part found? => Valid operation found
                if (!string.IsNullOrEmpty(part))
                {
                    var x = ToXName(part, namespaces);
                    message.Request = samples.ContainsKey(x) ? samples[x] : "";
                    yield return message;                                    
                }
            }


        }

        private XmlSchemaSet ExtractSchemaSet(XDocument doc, Dictionary<string, string> namespaces)
        {

            // Get schemas
            var schemas = doc.Descendants(XName.Get("types", "http://schemas.xmlsoap.org/wsdl/"))
                            .Descendants(XName.Get("schema", "http://www.w3.org/2001/XMLSchema"));

            XmlSchemaSet s = new XmlSchemaSet();
            // Import redirections
            schemas.Descendants(XName.Get("import", "http://www.w3.org/2001/XMLSchema"))
                                .ToList()
                                .ForEach(a => s.Add(a.Attribute("namespace").Value, a.Attribute("schemaLocation").Value));

            // Read inline xmlschema
            NameTable nt = new NameTable();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
            foreach (var n in namespaces)
                nsmgr.AddNamespace(n.Key, n.Value);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            // Create the XmlParserContext.
            XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);
            XmlReader r = XmlReader.Create(new System.IO.StringReader(schemas.FirstOrDefault().ToString()), settings, context);
            s.Add(XmlSchema.Read(r, (se, e) => {
                // We do not care about error
            }));

            return s;
        }

        private void ExtractSamples(XmlSchemaObject schema, Dictionary<XmlQualifiedName, string> samples)
        {
            XmlSampleGenerator g = null;
            if (schema is XmlSchemaImport)
            {
                g = new XmlSampleGenerator((schema as XmlSchemaImport).SchemaLocation);
            }
            else if (schema is XmlSchema)
            {
                g = new XmlSampleGenerator(schema as XmlSchema);
            }
            if (g != null)
            {
                foreach (var sample in g.GenerateAll())
                    if (!samples.ContainsKey(sample.Key))
                        samples.Add(sample.Key, sample.Value);
            }
        }

        private XmlQualifiedName ToXName(string id, Dictionary<string, string> namespaces)
        {
            var parts = id.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            return new XmlQualifiedName(parts[1], namespaces[parts[0]]);
        }
    }
}
