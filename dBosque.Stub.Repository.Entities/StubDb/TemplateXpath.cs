using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class TemplateXpath
    {
        public long TemplateXpathId { get; set; }
        public long TemplateId { get; set; }
        public long XpathId { get; set; }

        public virtual Template Template { get; set; }
        public virtual Xpath Xpath { get; set; }
    }
}
