using Microsoft.EntityFrameworkCore;


namespace dBosque.Stub.Repository.StubDb.Entities
{

    public partial class StubDbEntities : DbContext
    {
        public virtual DbSet<Combination> Combination { get; set; }
        public virtual DbSet<CombinationXpath> CombinationXpath { get; set; }
        public virtual DbSet<MessageType> MessageType { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<RequestThumbprint> RequestThumbprint { get; set; }
        public virtual DbSet<Response> Response { get; set; }
        public virtual DbSet<StubLog> StubLog { get; set; }
        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<TemplateXpath> TemplateXpath { get; set; }
        public virtual DbSet<Xpath> Xpath { get; set; }
    }


}