using dBosque.Stub.Repository;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Repository.Interfaces.Configuration;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        /// <summary>
        /// Add the repository module
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositoryModule(this IServiceCollection service, IConfiguration configuration, string sectionName = "Datastore")
        {
            service.AddOptions()
                .Configure<Datastore>(configuration.GetSection(sectionName))
                .AddSingleton<IRepositoryFactory, RepositoryFactory>()
                .AddSingleton(s => s.GetService<IRepositoryFactory>().CreateConfiguration());

            return service;

        }
    }
}
