using System;

namespace dBosque.Stub.Repository.Interfaces
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// The default connection string as specified in the appsettings
        /// </summary>
        ConnectionStringSetting Default { get; }

        /// <summary>
        /// Placeholder to force a specific configuration
        /// </summary>
        ConnectionStringSetting SelectedConfiguration { set; }

        /// <summary>
        /// Create a configuration repository for a specific tenant security key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IConfigurationRepository CreateConfigurationRepositoryFor(Guid key);

        /// <summary>
        /// Create a configuration repository for a specific tenant.
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        IConfigurationRepository CreateConfigurationRepositoryFor(int tenant);

        /// <summary>
        /// Create a configuration repository from a reference connection setting in the database
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IConfigurationRepository CreateConfigurationFromDb(string key);

        /// <summary>
        /// Create the configuration repository from the current (default) connectionstring.
        /// </summary>
        /// <returns></returns>
        IConfigurationRepository CreateConfiguration();

        /// <summary>
        /// Create a data repository based on the given configuration, or the default is none specified.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        IStubDataRepository CreateDataRepository(IConfigurationRepository config = null);
    }
}
