using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class RequestThumbprint
    {
        public long RequestThumbPrintId { get; set; }
        public long ResponseId { get; set; }
        public long RequestId { get; set; }
        public string Thumbprint { get; set; }

        public virtual Request Request { get; set; }
        public virtual Response Response { get; set; }
    }
}
