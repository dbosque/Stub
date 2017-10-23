using dBosque.Stub.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace dBosque.Stub.Repository.ConfigurationDb.Entities
{
    public partial class ConfigurationDbEntities
    {
        public ConfigurationDbEntities()
        { }
        public ConfigurationDbEntities(DbContextOptions options) :
            base(options)
        { }
        //public static ConfigurationDbEntities Create(ConnectionStringSetting setting)
        //{


        //    System.Data.Common.DbConnection connect;
        //    var factory = System.Data.Common.DbProviderFactories.GetFactory(setting.ProviderName);
        //    if (factory != null)
        //    {
        //        connect = factory.CreateConnection();
        //        connect.ConnectionString = setting.ConnectionString;
        //        return new ConfigurationDbEntities(connect);
        //    }
        //    return null;
        //}
      
        //public static ConfigurationDbEntities Create(string name)
        //{
        //    System.Data.Common.DbConnection connect;
        //    var factory = System.Data.Common.DbProviderFactories.GetFactory(System.Configuration.ConfigurationManager.ConnectionStrings[name].ProviderName);
        //    if (factory != null)
        //    {
        //        connect = factory.CreateConnection();
        //        connect.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
        //        return new ConfigurationDbEntities(connect);
        //    }
        //    return null;            
        //}

        //protected ConfigurationDbEntities(System.Data.Common.DbConnection connection) :
        //    base(connection, true)
        //{
        //    Database.SetInitializer<ConfigurationDbEntities>(null);
        //}



        ///<summary>
        ///Override this method to further configure the model that was discovered by convention from the entity types
        ///exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        ///and re-used for subsequent instances of your derived context.
        ///</summary>
        ///<remarks>
        ///If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        ///then this method will not be run.
        ///</remarks>
        ///<param name="modelBuilder">
        ///The builder being used to construct the model for this context. Databases (and other extensions) typically
        ///define extension methods on this object that allow you to configure aspects of the model that are specific
        ///to a given database.
        ///</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

         //   modelBuilder.HasDefaultSchema("configuration");
        
            base.OnModelCreating(modelBuilder);
        }
    }
}
