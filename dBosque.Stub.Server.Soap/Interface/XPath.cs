using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
namespace dBosque.Stub.Server.Soap.Interface
{
    [DataContract(Namespace = "http://schemas.stubber/XPath")]
    public class XPath
    {
        public XPath()
        {
        }

        public XPath(string exp, string val)
        {
            Expression = exp;
            Value = val;
        }

        [DataMember]
        public string Expression
        {
            get;
            set;
        }

        [DataMember]
        public string Value
        {
            get;
            set;
        }
    }
}