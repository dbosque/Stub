using System;
using Microsoft.EntityFrameworkCore;

namespace dBosque.Stub.Repository.ConfigurationDb.Entities
{
    public partial class ConfigurationDbEntities : DbContext
    {
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Tenant> Tenant { get; set; }
        public virtual DbSet<TenantSecurity> TenantSecurity { get; set; }

        ///<summary>
        ///<para>
        ///Override this method to configure the database (and other options) to be used for this context.
        ///This method is called for each instance of the context that is created.
        ///</para>
        ///<para>
        ///In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        ///to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        ///the options have already been set, and skip some or all of the logic in
        ///<see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        ///</para>
        ///</summary>
        ///<param name="optionsBuilder">
        ///A builder used to create or modify options for this context. Databases (and other extensions)
        ///typically define extension methods on this object that allow you to configure the context.
        ///</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
      
    }
}