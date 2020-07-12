using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Repository.Interfaces.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private static IEnumerable<IDbContextBuilder> _builders = new List<IDbContextBuilder>()
        {
            new SQLiteDbContextBuilder(),
            new SQLServerDbContextBuilder(),
            new MySqlDbContextBuilder()
        };

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public RepositoryFactory(IConfiguration configuration, IOptions<Datastore> options, ILogger<RepositoryFactory> logger)
        {
            _logger = logger;
            Default = new ConnectionStringSetting(options);
        }

        private ILogger<RepositoryFactory> _logger;

        ///<summary>
        ///The default connection string as specified in the appsettings
        ///</summary>
        public ConnectionStringSetting Default { get; private set; }

        /// <summary>
        /// Placeholder to force a specific configuration
        /// </summary>
        public ConnectionStringSetting SelectedConfiguration { private get; set; }

        ///<summary>
        ///Create a configuration repository for a specific tenant security key
        ///</summary>
        ///<param name="key"></param>
        ///<returns></returns>
        public IConfigurationRepository CreateConfigurationRepositoryFor(Guid key)
        {
            IStubConfigurationRepository db = CreateConfiguration() as IStubConfigurationRepository;

            var tenant = db.GetBySecurityKey(key);
            if (tenant != null)
            {
                _logger.LogInformation($"Configuration created for tenant : {tenant.Name}.");
                return new ConfigurationDbRepository(_builders, tenant.TenantId, Default.With(tenant.Connectionstring), _logger);
            }
            return null;
        }

        ///<summary>
        ///Create a configuration repository for a specific tenant.
        ///</summary>
        ///<param name="tenant"></param>
        ///<returns></returns>
        public IConfigurationRepository CreateConfigurationRepositoryFor(int tenant)
        {
            IStubConfigurationRepository db = CreateConfiguration() as IStubConfigurationRepository;
            var tenantd = db.GetById(tenant);
            if (tenantd != null)
            {
                _logger.LogInformation($"Configuration created for tenant : {tenantd.Name}.");
                return new ConfigurationDbRepository(_builders, tenantd.TenantId, Default.With(tenantd.Connectionstring), _logger);
            }
            return null;           
        }

        ///<summary>
        ///Create a configuration repository from a reference connection setting in the database
        ///</summary>
        ///<param name="key"></param>
        ///<returns></returns>
        public IConfigurationRepository CreateConfigurationFromDb(string key)
        {
            var config = CreateConfiguration();
            return new ConfigurationDbRepository(_builders, config.GetConnection(key)??Default, _logger);
        }

        ///<summary>
        ///Create the configuration repository from the current (default) connectionstring.
        ///</summary>
        ///<returns></returns>
        public IConfigurationRepository CreateConfiguration()
        {
            return new ConfigurationDbRepository(_builders, SelectedConfiguration??Default, _logger);
        }

        ///<summary>
        ///Create a data repository based on the given configuration, or the default is none specified.
        ///</summary>
        ///<param name="config"></param>
        ///<returns></returns>
        public IStubDataRepository CreateDataRepository(IConfigurationRepository config = null)
        {
            return new LinqStubRepository(_builders, config?? CreateConfiguration(), _logger) as IStubDataRepository;
        }
    }
}
