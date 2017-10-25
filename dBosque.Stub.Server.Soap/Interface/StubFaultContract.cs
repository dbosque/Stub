using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace dBosque.Stub.Server.Soap.Interface
{
    [DataContract(Namespace = "http://schemas.stubber/Fault")]
    public class StubFaultContract
    {
        [DataMember]
        public string Message
        {
            get;
            set;
        }

        [DataMember]
        public List<Match> Matches
        {
            get;
            set;
        }

        [DataMember]
        public XmlElement Request
        {
            get;
            set;
        }
    }
}