using dBosque.Stub.Repository.Interfaces.Configuration;
using Microsoft.Extensions.Options;

namespace dBosque.Stub.Repository.Interfaces
{
    public class ConnectionStringSetting
    {
        public ConnectionStringSetting(IOptions<Datastore> store)
        {
            Name = "ConfigurationRepository";
            ConnectionString = store.Value.Connection?.Connectionstring;
            ProviderName = store.Value.Connection?.Provider;
        }

        public ConnectionStringSetting() :
            this("ConfigurationRepository", @"Data Source='.\dbstub.db'", "SQLite")
        { }            

        public ConnectionStringSetting(string name, string connectionString, string providerName)
        {
            Name = name;
            ConnectionString = connectionString;
            ProviderName = providerName;
        }

        public ConnectionStringSetting With(string connection, string provider = null)
        {
            return new ConnectionStringSetting()
            {
                ConnectionString = connection,
                ProviderName = provider ?? ProviderName
            };
        }

        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
    }
}
