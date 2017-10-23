using dBosque.Stub.Interfaces;
using dBosque.Stub.Services;
using dBosque.Stub.Sockets;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddSocketModule(this IServiceCollection service)
        {
            service.AddTransient<IStubHandler<string>, GenericStubHandler<string>>()
                   .AddTransient<IServiceRegister, SocketService>();
            return service;
        }
    }
}
