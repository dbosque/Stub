using dBosque.Stub.Interfaces;
using dBosque.Stub.WCF;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddWCFModule(this IServiceCollection service)
        {
            service.AddTransient<IServiceRegister, ServiceRegister>();
            return service;
        }
    }
}
