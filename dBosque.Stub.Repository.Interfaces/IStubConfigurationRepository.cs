using System;
using System.Collections.Generic;
using dBosque.Stub.Repository.ConfigurationDb.Entities;
namespace dBosque.Stub.Repository.Interfaces
{
    public interface IStubConfigurationRepository
    {
        /// <summary>
        /// Get a tenant by (one of) its security keys
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Tenant GetBySecurityKey(Guid key);

        /// <summary>
        /// Get a tenant by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Tenant GetById(long id);
        /// <summary>
        /// Delete a tenant
        /// </summary>
        /// <param name="id"></param>
        void DeleteTenant(long? id);

        /// <summary>
        /// Update a tenant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        Tenant UpdateTenant(long? id, Action<Tenant> updateAction);

        /// <summary>
        /// Retrieve all possible tenants
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tenant> GetAllTenants();

        /// <summary>
        /// Get all security codes for a tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        IEnumerable<TenantSecurity> GetAllSecurityCodes(long tenantId);

        /// <summary>
        /// Create a new security code
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        TenantSecurity AddNewSecurityCode(long tenantId);

        /// <summary>
        /// Activate a specific securitycode
        /// </summary>
        /// <param name="id"></param>
        void ActivateSecurtyCode(long? id);

        /// <summary>
        /// Get all settings
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        IEnumerable<Settings> GetAllSettings(long tenantId);

        /// <summary>
        /// Update a specific setting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        Settings UpdateSetting(long? id, Action<Settings> updateAction);
    }
}
