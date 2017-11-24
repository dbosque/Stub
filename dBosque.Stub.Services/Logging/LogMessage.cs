using dBosque.Stub.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace dBosque.Stub.Services.Logging
{
    public class LogMessage<TResult>
    {
        public LogMessage(IStubMessage<TResult> msg)
        {
            Sender = msg.Sender;
            Rootnode = $"{msg.RootNameSpace}/{msg.RootNode}";
            Uri = msg.Uri;
            Direction = "In";
        }

        public string Direction { get; set; }
        public string Sender { get; set; }
        public string Rootnode { get; set; }
        public string Uri { get; set; }
    }
}
