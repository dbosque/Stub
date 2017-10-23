using System;
using System.Collections.Generic;

namespace dBosque.Stub.Repository.Interfaces
{
    public interface IConfigurationRepository
    {

        /// <summary>
        /// The connectionstring used to create the repository
        /// </summary>
        ConnectionStringSetting Current { get; }

        /// <summary>
        /// Check if any connectionstring are available
        /// </summary>
        bool HasConnectionStrings { get; }

        /// <summary>
        /// Get the value of a configuration key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// Get a provider for the specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetProvider(string key);

        /// <summary>
        /// Get the connectionstring for the specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetConnectionString(string key);

        /// <summary>
        /// Get the connectionstring for the specific key.
        /// </summary>
        /// <param name="key"></param>
        ConnectionStringSetting GetConnection(string key);

        /// <summary>
        /// Set a specific setting
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, string value);


        /// <summary>
        /// Update the connectionstring for the specific key.
        /// </summary>
        /// <param name="con"></param>
        void SetConnection(ConnectionStringSetting con);     

        /// <summary>
        /// Get all possible keys
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllKeys();

        /// <summary>
        /// Get all connectionstring keys
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllConnectionStringKeys();

        /// <summary>
        /// Update a tenant key
        /// </summary>
        /// <param name="key"></param>
        void UpdateKey(Guid key);

        /// <summary>
        /// Get the tenantId for which this configuration is valid.
        /// </summary>
         long? Id { get; }

        /// <summary>
        /// Get the default data connection instance name
        /// </summary>
        /// <param name="configured"></param>
        /// <returns></returns>
        string DefaultDataConnectionId(string configured);
    }
}
