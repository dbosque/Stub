using dBosque.Stub.Repository.ConfigurationDb.Entities;
using dBosque.Stub.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dBosque.Stub.Repository
{
    /// <summary>
    /// Configuration Repository
    /// </summary>
    public class ConfigurationDbRepository : IConfigurationRepository, IStubConfigurationRepository
    {
        ///<summary>
        ///The connectionstring used to create the repository
        ///</summary>
        public ConnectionStringSetting Current { get; private set; }

        private ConfigurationDbEntities database;
        private long tenantId = 0;

        internal ConfigurationDbRepository(IEnumerable<IDbContextBuilder> builders, ConnectionStringSetting setting, ILogger logger) :
            this(builders, 1, setting, logger)
        { }

        internal ConfigurationDbRepository(IEnumerable<IDbContextBuilder> builders, long id, ConnectionStringSetting setting, ILogger logger)
        {
            // Save the connectionstring we used to access the configurationdb.
            Current = setting;
            var builder = builders.FirstOrDefault(b => b.CanHandle(setting.ProviderName));
            database = builder.CreateDbContext<ConfigurationDbEntities>(setting.ConnectionString);
            tenantId = id;
            logger.LogTrace($"Created configuration from {setting.ConnectionString}");
            
        }
        
        ///<summary>
        ///Check if any connectionstring are available
        ///</summary>        
        bool IConfigurationRepository.HasConnectionStrings => ((IConfigurationRepository)this).GetAllConnectionStringKeys().Any();

        ///<summary>
        ///Get the tenantId for which this configuration is valid.
        ///</summary>
        long? IConfigurationRepository.Id => tenantId;

        ///<summary>
        ///Get the value of a configuration key
        ///</summary>
        ///<param name="key"></param>
        ///<returns></returns>
        string IConfigurationRepository.Get(string key)
        {
            return GetSettingWhere(t => t.Name == key, t => t.Value).FirstOrDefault();
        }

        private void UpdateSettingWhere(Func<Settings, bool> clause, string name, string value)
        {
            var item = database.Settings.FirstOrDefault(clause ?? ((a) => a.Name == name));
            if (item == null)
            {
                item = new Settings() { Name = name, TenantId = tenantId, Value = value };
                database.Settings.Add(item);
            }
            else
                item.Value = value;
            database.SaveChanges();
        }

        private IEnumerable<string> GetSettingWhere(Func<Settings, bool> clause, Func<Settings, string> select)
        {
            return database.Settings
                .OrderByDescending(t => t.Name)
                .Where(clause ?? ((a) => true))
                .Select(select);
        }

        ///<summary>
        ///Get all connectionstring keys
        ///</summary>
        ///<returns></returns>
        IEnumerable<string> IConfigurationRepository.GetAllConnectionStringKeys()
        {
            return GetSettingWhere(t => t.Name.EndsWith(".ConnectionString", StringComparison.Ordinal), s => s.Name?.Replace(".ConnectionString", "")).OrderBy(a => a);
        }

        ///<summary>
        ///Get all possible keys
        ///</summary>
        ///<returns></returns>
        IEnumerable<string> IConfigurationRepository.GetAllKeys()
        {
            return GetSettingWhere(null, s => s.Name);
        }

        ///<summary>
        ///Get the connectionstring for the specific key.
        ///</summary>
        ///<param name="key"></param>
        ConnectionStringSetting IConfigurationRepository.GetConnection(string key)
        {
            var conn = GetSettingWhere(t => t.Name == $"{key}.ConnectionString", t => t.Value).FirstOrDefault();
            var prov = GetSettingWhere(t => t.Name == $"{key}.Provider", t => t.Value).FirstOrDefault();
            return new ConnectionStringSetting(key, conn, prov);
        }

        ///<summary>
        ///Get the connectionstring for the specific key
        ///</summary>
        ///<param name="key"></param>
        ///<returns></returns>
        string IConfigurationRepository.GetConnectionString(string key)
        {
            var x = GetSettingWhere(t => t.Name == $"{key}.ConnectionString", t => t.Value).FirstOrDefault();
            return x;
        }

        ///<summary>
        ///Get a provider for the specific key
        ///</summary>
        ///<param name="key"></param>
        ///<returns></returns>
        string IConfigurationRepository.GetProvider(string key)
        {
            var x = GetSettingWhere(t => t.Name == $"{key}.Provider", t => t.Value).FirstOrDefault();
            return x;
        }

        ///<summary>
        ///Set a specific setting
        ///</summary>
        ///<param name="key"></param>
        ///<param name="value"></param>
        void IConfigurationRepository.Set(string key, string value)
        {
            UpdateSettingWhere(null, key, value);
        }

        ///<summary>
        ///Update the connectionstring for the specific key.
        ///</summary>
        ///<param name="con"></param>
        void IConfigurationRepository.SetConnection(ConnectionStringSetting con)
        {
            UpdateSettingWhere(null, $"{con.Name}.ConnectionString", con.ConnectionString);
            UpdateSettingWhere(null, $"{con.Name}.Provider", con.ProviderName);
        }     

        ///<summary>
        ///Update a tenant key
        ///</summary>
        ///<param name="key"></param>
        void IConfigurationRepository.UpdateKey(Guid key)
        {
            database.TenantSecurity.FirstOrDefault(t => t.TenantId == tenantId && t.Active).SecurityCode = key;
        }

        ///<summary>
        ///Update a tenant
        ///</summary>
        ///<param name="id"></param>
        ///<param name="updateAction"></param>
        ///<returns></returns>
        Tenant IStubConfigurationRepository.UpdateTenant(long? id, Action<Tenant> updateAction)
        {
            var item = new Tenant();
            if (id.HasValue)
                item = database.Tenant.FirstOrDefault(r => r.TenantId == id);
            else
                database.Tenant.Add(item);

            updateAction(item);
            database.SaveChanges();
            return item;
        }

        ///<summary>
        ///Get a tenant by (one of) its security keys
        ///</summary>
        ///<param name="key"></param>
        ///<returns></returns>
        Tenant IStubConfigurationRepository.GetBySecurityKey(Guid key)
        {
            return database.TenantSecurity.FirstOrDefault(ts => ts.SecurityCode == key && ts.Active)?.Tenant;
        }

        /// <summary>
        /// Get a tenant by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Tenant IStubConfigurationRepository.GetById(long id)
        {
            return database.Tenant.FirstOrDefault(ts => ts.TenantId == id && ts.Active);
        }

        ///<summary>
        ///Retrieve all possible tenants
        ///</summary>
        ///<returns></returns>
        IEnumerable<Tenant> IStubConfigurationRepository.GetAllTenants()
        {
            return database.Tenant.AsEnumerable();
        }

        ///<summary>
        ///Get all security codes for a tenant
        ///</summary>
        ///<param name="tenantId"></param>
        ///<returns></returns>
        IEnumerable<TenantSecurity> IStubConfigurationRepository.GetAllSecurityCodes(long tenantId)
        {
            return database.TenantSecurity.Where(t => t.TenantId == tenantId);
        }

        ///<summary>
        ///Delete a tenant
        ///</summary>
        ///<param name="id"></param>
        void IStubConfigurationRepository.DeleteTenant(long? id)
        {
            database.TenantSecurity.RemoveRange(database.TenantSecurity.Where(t => t.TenantId == id.Value));
            database.Tenant.Remove(database.Tenant.FirstOrDefault(t => t.TenantId == id.Value));
            database.SaveChanges();
        }
        ///<summary>
        ///Create a new security code
        ///</summary>
        ///<param name="tenantId"></param>
        ///<returns></returns>
        TenantSecurity IStubConfigurationRepository.AddNewSecurityCode(long tenantId)
        {
            database.TenantSecurity.Where(t => t.TenantId == tenantId).ToList().ForEach(a => a.Active = false);
            var sec = new TenantSecurity() { Active = true, SecurityCode = Guid.NewGuid(), TenantId = tenantId };
            database.TenantSecurity.Remove(sec);
            database.SaveChanges();
            return sec;
        }

        ///<summary>
        ///Get all settings
        ///</summary>
        ///<param name="tenantId"></param>
        ///<returns></returns>
        IEnumerable<Settings> IStubConfigurationRepository.GetAllSettings(long tenantId)
        {
            return database.Settings.Where(t => t.TenantId == tenantId);
        }

        ///<summary>
        ///Update a specific setting
        ///</summary>
        ///<param name="id"></param>
        ///<param name="updateAction"></param>
        ///<returns></returns>
        Settings IStubConfigurationRepository.UpdateSetting(long? id, Action<Settings> updateAction)
        {
            var item = new Settings();
            if (id.HasValue)
                item = database.Settings.FirstOrDefault(r => r.TenantId == tenantId);
            else
                database.Settings.Add(item);

            updateAction(item);
            database.SaveChanges();
            return item;
        }


     

        ///<summary>
        ///Activate a specific securitycode
        ///</summary>
        ///<param name="id"></param>
        void IStubConfigurationRepository.ActivateSecurtyCode(long? id)
        {
            database.TenantSecurity.Where(t => t.TenantId == tenantId).ToList().ForEach(a => a.Active = false);
            database.TenantSecurity.FirstOrDefault(t => t.TenantSecurityId == id.Value).Active = true;
            database.SaveChanges();
        }

        ///<summary>
        ///Get the default data connection instance name
        ///</summary>
        ///<param name="configured"></param>
        ///<returns></returns>
        string IConfigurationRepository.DefaultDataConnectionId(string configured)
        {
            return string.IsNullOrEmpty(configured) ? ((IConfigurationRepository)this).GetAllConnectionStringKeys().First() : configured;
        }
    }
}
