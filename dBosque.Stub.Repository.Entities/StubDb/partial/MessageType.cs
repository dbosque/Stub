using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class MessageType
    {
        public bool IsRegEx => string.IsNullOrEmpty(Rootnode);
    }
}
