using dBosque.Stub.AspNet.Host;
using dBosque.Stub.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace dBosque.Stub.WebApi.Configuration
{
    /// <summary>
    /// Service Register for webapi
    /// </summary>
    public class ServiceRegister : ServiceRegisterBase<Startup>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceRegister(IConfigurationRepository config, ILogger<ServiceRegister> logger)
            : base(logger, "configuration", config.Get("Hosting.ConfigurationUrl") ?? "http://*:8082")
        {}
     }
}
