using dBosque.Stub.Server.AspNetCore.Host;
using dBosque.Stub.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// Service Register for webapi
    /// </summary>
    public class ServiceRegister : ServiceRegisterBase<Startup>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceRegister(IConfigurationRepository config, ILogger<ServiceRegister> logger, IOptions<Hosting> hosting)
            : base(logger, "configuration", $"{hosting?.Value.Uri ?? "http://*:8082"}")
        {}
     }
}
