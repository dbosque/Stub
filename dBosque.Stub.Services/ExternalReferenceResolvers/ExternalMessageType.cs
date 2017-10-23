using System.Xml;

namespace dBosque.Stub.Services.ExternalReferenceResolvers
{
    /// <summary>
    /// An info object for the retrieved ExternalMessageType
    /// </summary>
    public class ExternalMessageType
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ExternalMessageType()
        { }

        /// <summary>
        /// Overloaded constructor to take a different externalmessagetype instance
        /// </summary>
        /// <param name="copy"></param>
        protected ExternalMessageType(ExternalMessageType copy)
        {
            Uri = copy.Uri;         
            Request = copy.Request;
            Description = copy.Description;
            RegEx = copy.RegEx;          
        }

        /// <summary>
        /// The uri this message is taken from
        /// </summary>
        public string Uri { get; set; }
      
        /// <summary>
        /// A request example 
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// A nice description, taken from the documentation
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A possible regex (for restapi's)
        /// </summary>
        public string RegEx { get; set; }        
    }
}
