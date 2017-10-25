using dBosque.Stub.Server;
using dBosque.Stub.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddHostingModule(this IServiceCollection service, IConfiguration configuration, string sectionName = "Server")
        {
            service.AddTransient<IServiceRegister, OneEndpointServiceRegister>()
                    .Configure<dBosque.Stub.Server.Configuration.Hosting>(configuration.GetSection(sectionName));
            return service;
        }
    }
}
