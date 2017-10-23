using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
namespace dBosque.Stub.Services.Extensions
{
    public class DocumentInfo
    {
        public XmlDocument Document
        {
            get;
            set;
        }

        public string Namespace
        {
            get;
            set;
        }

        public string RootNode
        {
            get;
            set;
        }
    }
}