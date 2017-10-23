using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class CombinationXpath
    {
        public long CombinationXpathId { get; set; }
        public long XpathId { get; set; }
        public long CombinationId { get; set; }
        public string XpathValue { get; set; }

        public virtual Combination Combination { get; set; }
        public virtual Xpath Xpath { get; set; }
    }
}
