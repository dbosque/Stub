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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{          
        //    optionsBuilder.UseSqlite(@"Data Source=C:\Source\core\stub\dbstub.db");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Combination>(entity =>
        //    //{
        //    //    entity.Property(e => e.CombinationId)
        //    //        .HasColumnName("CombinationID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.Description).HasColumnType("varchar(250)");

        //    //    entity.Property(e => e.MessageTypeId)
        //    //        .HasColumnName("MessageTypeID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.ResponseId)
        //    //        .HasColumnName("ResponseID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.TemplateId)
        //    //        .HasColumnName("TemplateID")
        //    //        .HasColumnType("integer");

        //    //    entity.HasOne(d => d.MessageType)
        //    //        .WithMany(p => p.Combination)
        //    //        .HasForeignKey(d => d.MessageTypeId)
        //    //        .OnDelete(DeleteBehavior.Restrict);

        //    //    entity.HasOne(d => d.Response)
        //    //        .WithMany(p => p.Combination)
        //    //        .HasForeignKey(d => d.ResponseId)
        //    //        .OnDelete(DeleteBehavior.Restrict);

        //    //    entity.HasOne(d => d.Template)
        //    //        .WithMany(p => p.Combination)
        //    //        .HasForeignKey(d => d.TemplateId)
        //    //        .OnDelete(DeleteBehavior.Restrict);
        //    //});

        //    //modelBuilder.Entity<CombinationXpath>(entity =>
        //    //{
        //    //    entity.Property(e => e.CombinationXpathId)
        //    //        .HasColumnName("CombinationXpathID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.CombinationId)
        //    //        .HasColumnName("CombinationID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.XpathId)
        //    //        .HasColumnName("XpathID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.XpathValue).HasColumnType("varchar(250)");

        //    //    entity.HasOne(d => d.Combination)
        //    //        .WithMany(p => p.CombinationXpath)
        //    //        .HasForeignKey(d => d.CombinationId)
        //    //        .OnDelete(DeleteBehavior.Restrict);

        //    //    entity.HasOne(d => d.Xpath)
        //    //        .WithMany(p => p.CombinationXpath)
        //    //        .HasForeignKey(d => d.XpathId)
        //    //        .OnDelete(DeleteBehavior.Restrict);
        //    //});

        //    //modelBuilder.Entity<MessageType>(entity =>
        //    //{
        //    //    entity.Property(e => e.MessageTypeId)
        //    //        .HasColumnName("MessageTypeID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.Description).HasColumnType("varchar(250)");

        //    //    entity.Property(e => e.Namespace)
        //    //        .IsRequired()
        //    //        .HasColumnType("varchar(250)");

        //    //    entity.Property(e => e.PassthroughUrl).HasColumnType("varchar(1024)");

        //    //    entity.Property(e => e.Rootnode)
        //    //        .IsRequired()
        //    //        .HasColumnType("varchar(250)");

        //    //    entity.Property(e => e.Sample).HasColumnType("varchar(1024)");
        //    //});

        //    //modelBuilder.Entity<Request>(entity =>
        //    //{
        //    //    entity.Property(e => e.RequestId)
        //    //        .HasColumnName("RequestID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.Description).HasColumnType("varchar(250)");

        //    //    entity.Property(e => e.Request1)
        //    //        .IsRequired()
        //    //        .HasColumnName("Request")
        //    //        .HasColumnType("varchar");
        //    //});

        //    //modelBuilder.Entity<RequestThumbprint>(entity =>
        //    //{
        //    //    entity.HasIndex(e => e.Thumbprint)
        //    //        .HasName("RequestThumbprint_IX_RequestThumbprint")
        //    //        .IsUnique();

        //    //    entity.Property(e => e.RequestThumbPrintId)
        //    //        .HasColumnName("RequestThumbPrintID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.RequestId)
        //    //        .HasColumnName("RequestID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.ResponseId)
        //    //        .HasColumnName("ResponseID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.Thumbprint)
        //    //        .IsRequired()
        //    //        .HasColumnType("char(40)");

        //    //    entity.HasOne(d => d.Request)
        //    //        .WithMany(p => p.RequestThumbprint)
        //    //        .HasForeignKey(d => d.RequestId)
        //    //        .OnDelete(DeleteBehavior.Restrict);

        //    //    entity.HasOne(d => d.Response)
        //    //        .WithMany(p => p.RequestThumbprint)
        //    //        .HasForeignKey(d => d.ResponseId)
        //    //        .OnDelete(DeleteBehavior.Restrict);
        //    //});

        //    //modelBuilder.Entity<Response>(entity =>
        //    //{
        //    //    entity.Property(e => e.ResponseId)
        //    //        .HasColumnName("ResponseID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.ContentType).HasColumnType("varchar(250)");

        //    //    entity.Property(e => e.Description).HasColumnType("varchar(250)");

        //    //    entity.Property(e => e.ResponseText)
        //    //        .IsRequired()
        //    //        .HasColumnType("varchar");

        //    //    entity.Property(e => e.StatusCode).HasColumnType("integer");
        //    //});

        //    //modelBuilder.Entity<StubLog>(entity =>
        //    //{
        //    //    entity.Property(e => e.StubLogId)
        //    //        .HasColumnName("StubLogID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.CombinationId)
        //    //        .HasColumnName("CombinationID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.MessageTypeId).HasColumnType("integer");

        //    //    entity.Property(e => e.Request)
        //    //        .IsRequired()
        //    //        .HasColumnType("nvarchar");

        //    //    entity.Property(e => e.ResponseDatumTijd)
        //    //        .IsRequired()
        //    //        .HasColumnType("datetime");

        //    //    entity.Property(e => e.TenantId)
        //    //        .HasColumnName("TenantID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.Uri).HasColumnType("nvarchar");
        //    //});

        //    //modelBuilder.Entity<Template>(entity =>
        //    //{
        //    //    entity.Property(e => e.TemplateId)
        //    //        .HasColumnName("TemplateID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.Description).HasColumnType("varchar(250)");

        //    //    entity.Property(e => e.MessageTypeId)
        //    //        .HasColumnName("MessageTypeID")
        //    //        .HasColumnType("integer");

        //    //    entity.HasOne(d => d.MessageType)
        //    //        .WithMany(p => p.Template)
        //    //        .HasForeignKey(d => d.MessageTypeId)
        //    //        .OnDelete(DeleteBehavior.Restrict);
        //    //});

        //    //modelBuilder.Entity<TemplateXpath>(entity =>
        //    //{
        //    //    entity.Property(e => e.TemplateXpathId)
        //    //        .HasColumnName("TemplateXpathID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.TemplateId)
        //    //        .HasColumnName("TemplateID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.XpathId)
        //    //        .HasColumnName("XpathID")
        //    //        .HasColumnType("integer");

        //    //    entity.HasOne(d => d.Template)
        //    //        .WithMany(p => p.TemplateXpath)
        //    //        .HasForeignKey(d => d.TemplateId)
        //    //        .OnDelete(DeleteBehavior.Restrict);

        //    //    entity.HasOne(d => d.Xpath)
        //    //        .WithMany(p => p.TemplateXpath)
        //    //        .HasForeignKey(d => d.XpathId)
        //    //        .OnDelete(DeleteBehavior.Restrict);
        //    //});

        //    //modelBuilder.Entity<Xpath>(entity =>
        //    //{
        //    //    entity.Property(e => e.XpathId)
        //    //        .HasColumnName("XpathID")
        //    //        .HasColumnType("integer");

        //    //    entity.Property(e => e.Description).HasColumnType("varchar(250)");

        //    //    entity.Property(e => e.Expression)
        //    //        .IsRequired()
        //    //        .HasColumnType("varchar(1000)");

        //    //    entity.Property(e => e.Type)
        //    //        .HasColumnType("integer")
        //    //        .HasDefaultValueSql("0");
        //    //});
        //}
    }
}