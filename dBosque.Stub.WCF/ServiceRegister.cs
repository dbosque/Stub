using dBosque.Stub.AspNet.Host;
using dBosque.Stub.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace dBosque.Stub.WCF
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
            : base(logger, "soap", config.Get("Hosting.WCFBaseUrl") ?? "http://*:8083")
        { }
    }
}
