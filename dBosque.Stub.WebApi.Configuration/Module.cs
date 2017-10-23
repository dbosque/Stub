using dBosque.Stub.Interfaces;
using dBosque.Stub.WebApi.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// DependencyInjection module
    /// </summary>
    public static class Module
    {
        /// <summary>
        /// DependencyInjection entry
        /// </summary>
        public static IServiceCollection AddWebApiConfigurationModule(this IServiceCollection service)
        {
            service.AddTransient<IServiceRegister, ServiceRegister>();
            return service;
        }
    }
}
