using dBosque.Stub.Interfaces;
using dBosque.Stub.WebApi;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddWebApiModule(this IServiceCollection service)
        {
            service.AddTransient<IServiceRegister, ServiceRegister>();
            return service;
        }
    }
}
