using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.ConfigurationDb.Entities
{
    public partial class Tenant
    {
        public Tenant()
        {
            Settings = new HashSet<Settings>();
            TenantSecurity = new HashSet<TenantSecurity>();
        }

        public long TenantId { get; set; }
        public string Name { get; set; }
        public string Connectionstring { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Settings> Settings { get; set; }
        public virtual ICollection<TenantSecurity> TenantSecurity { get; set; }
    }
}
