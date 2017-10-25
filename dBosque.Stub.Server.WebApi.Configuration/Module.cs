using dBosque.Stub.Interfaces;
using dBosque.Stub.Server.WebApi.Configuration;
using Microsoft.Extensions.Configuration;

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
        public static IServiceCollection AddWebApiConfigurationModule(this IServiceCollection service, IConfiguration configuration, string sectionName = "WebApiConfiguration")
        {
            service.AddTransient<IServiceRegister, ServiceRegister>()
                   .Configure<dBosque.Stub.Server.WebApi.Configuration.Hosting>(configuration.GetSection(sectionName));
            return service;
        }
    }
}
