using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
namespace dBosque.Stub.Server.Soap.Interface
{
    [DataContract(Namespace = "http://schemas.stubber/Match")]
    public class Match
    {
        public Match()
        {
        }

        public Match(string description)
        {
            Description = description;
            XPath = new List<XPath>();
        }

        [DataMember]
        public string Description
        {
            get;
            set;
        }

        [DataMember]
        public List<XPath> XPath
        {
            get;
            set;
        }
    }
}