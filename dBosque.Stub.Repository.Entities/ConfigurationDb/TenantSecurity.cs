using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.ConfigurationDb.Entities
{
    public partial class TenantSecurity
    {
        public long TenantSecurityId { get; set; }
        public long TenantId { get; set; }
        public Guid SecurityCode { get; set; }
        public bool Active { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
