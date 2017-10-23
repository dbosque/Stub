using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class MessageType
    {
        public MessageType()
        {
            Combination = new HashSet<Combination>();
            Template = new HashSet<Template>();
        }

        public long MessageTypeId { get; set; }
        public string Namespace { get; set; }
        public string Rootnode { get; set; }
        public string Description { get; set; }
        public bool PassthroughEnabled { get; set; }
        public string PassthroughUrl { get; set; }
        public string Sample { get; set; }

        public virtual ICollection<Combination> Combination { get; set; }
        public virtual ICollection<Template> Template { get; set; }
    }
}
