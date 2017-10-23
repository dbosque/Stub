using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.ConfigurationDb.Entities
{
    public partial class Settings
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
