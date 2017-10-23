using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class Template
    {
        public Template()
        {
            Combination = new HashSet<Combination>();
            TemplateXpath = new HashSet<TemplateXpath>();
        }

        public long TemplateId { get; set; }
        public long MessageTypeId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Combination> Combination { get; set; }
        public virtual ICollection<TemplateXpath> TemplateXpath { get; set; }
        public virtual MessageType MessageType { get; set; }
    }
}
