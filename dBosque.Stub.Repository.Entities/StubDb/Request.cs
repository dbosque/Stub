using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class Request
    {
        public Request()
        {
            RequestThumbprint = new HashSet<RequestThumbprint>();
        }

        public long RequestId { get; set; }
        public string Request1 { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RequestThumbprint> RequestThumbprint { get; set; }
    }
}
