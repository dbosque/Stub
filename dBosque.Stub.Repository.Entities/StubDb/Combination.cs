using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class Combination
    {
        public Combination()
        {
            CombinationXpath = new HashSet<CombinationXpath>();
        }

        public long CombinationId { get; set; }
        public long MessageTypeId { get; set; }
        public long TemplateId { get; set; }
        public long ResponseId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CombinationXpath> CombinationXpath { get; set; }
        public virtual MessageType MessageType { get; set; }
        public virtual Response Response { get; set; }
        public virtual Template Template { get; set; }
    }
}
