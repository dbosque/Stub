using dBosque.Stub.Interfaces;
using dBosque.Stub.Services;
using dBosque.Stub.Server.Sockets;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddSocketModule(this IServiceCollection service, IConfiguration configuration, string sectionName = "Socket")
        {
            service.AddTransient<IStubHandler<string>, GenericStubHandler<string>>()
                   .AddTransient<IServiceRegister, SocketService>()
                   .Configure<dBosque.Stub.Server.Sockets.Configuration.Hosting>(configuration.GetSection(sectionName));
            return service;
        }
    }
}
