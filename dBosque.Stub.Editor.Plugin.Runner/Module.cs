using dBosque.Stub.Editor.Interfaces;
using dBosque.Stub.Repository.Interfaces.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dBosque.Stub.Editor.Plugin.Runner
{
    public static class Module
    {
        public static IServiceCollection AddConfigurationOverride(this IServiceCollection service, RuntimePluginConfiguration configuration = null)
        {
            if (configuration != null)
                service.Configure<Datastore>(a =>
                {
                    a.Connection.Connectionstring = configuration.Connection;
                    a.Connection.Provider = configuration.Provider;
                });
            return service;
        }
    }
}
