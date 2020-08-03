using System;
using System.Collections.Generic;
using System.Linq;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class Response
    {
        public Response()
        {
            Combination = new HashSet<Combination>();
            RequestThumbprint = new HashSet<RequestThumbprint>();
        }

        public long ResponseId { get; set; }
        public string ResponseText { get; set; }
        public string Description { get; set; }
        public long? StatusCode { get; set; }
        public string ContentType { get; set; }
        public string Headers { get; set; }
        public virtual ICollection<Combination> Combination { get; set; }
        public virtual ICollection<RequestThumbprint> RequestThumbprint { get; set; }
    }
}
