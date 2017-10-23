using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class StubLog
    {
        public long StubLogId { get; set; }
        public long? CombinationId { get; set; }
        public DateTime ResponseDatumTijd { get; set; }
        public long? TenantId { get; set; }
        public string Request { get; set; }
        public string Uri { get; set; }
        public long? MessageTypeId { get; set; }
    }
}
