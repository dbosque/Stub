using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class Xpath
    {
        public Xpath()
        {
            CombinationXpath = new HashSet<CombinationXpath>();
            TemplateXpath = new HashSet<TemplateXpath>();
        }

        public long XpathId { get; set; }
        public string Expression { get; set; }
        public string Description { get; set; }
        public long? Type { get; set; }

        public virtual ICollection<CombinationXpath> CombinationXpath { get; set; }
        public virtual ICollection<TemplateXpath> TemplateXpath { get; set; }
    }
}
