using dBosque.Stub.Interfaces;
using dBosque.Stub.Server.WebApi;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddWebApiModule(this IServiceCollection service, IConfiguration configuration, string sectionName = "WebApi")
        {
            service.AddTransient<IServiceRegister, ServiceRegister>()
                   .Configure<dBosque.Stub.Server.WebApi.Configuration.Hosting>(configuration.GetSection(sectionName));
            return service;
        }
    }
}
