using dBosque.Stub.Server;
using dBosque.Stub.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddHostingModule(this IServiceCollection service)
        {
            service.AddTransient<IServiceRegister, OneEndpointServiceRegister>();
            return service;
        }
    }
}
