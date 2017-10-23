using System;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class stp_selectLog_Result
    {
        public string ResponseDatumTijd { get; set; }
        public string Request { get; set; }
        public string Tenant { get; set; }
        public string Template { get; set; }
        public string Combination { get; set; }
        public string Namespace { get; set; }
        public string Rootnode { get; set; }
        public long Id { get; set; }
        public string ResponseText { get; set; }
        public string Uri { get; set; }
    }
}
