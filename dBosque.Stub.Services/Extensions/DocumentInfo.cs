using System.Xml;
namespace dBosque.Stub.Services.Extensions
{
    /// <summary>
    /// XML info about a send document
    /// </summary>
    public class DocumentInfo
    {
        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        /// <value>
        /// The document.
        /// </value>
        public XmlDocument Document { get; set; }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        public string Namespace { get; set; }
        /// <summary>
        /// Gets or sets the root node.
        /// </summary>
        /// <value>
        /// The root node.
        /// </value>
        public string RootNode { get; set; }
    }
}