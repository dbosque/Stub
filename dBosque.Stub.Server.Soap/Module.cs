using dBosque.Stub.Interfaces;
using dBosque.Stub.Server.Soap;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddSoapModule(this IServiceCollection service, IConfiguration configuration, string sectionName = "Soap")
        {
            service.AddTransient<IServiceRegister, ServiceRegister>()
                   .Configure<dBosque.Stub.Server.Soap.Configuration.Hosting>(configuration.GetSection(sectionName));
            return service;
        }
    }
}
