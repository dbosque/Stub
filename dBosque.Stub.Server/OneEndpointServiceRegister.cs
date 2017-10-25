using dBosque.Stub.Server.AspNetCore.Host;
using dBosque.Stub.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dBosque.Stub.Server
{
    public class OneEndpointServiceRegister : ServiceRegisterBase<OneEndpointStartup>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public OneEndpointServiceRegister(IConfigurationRepository config, IOptions<Configuration.Hosting> hosting, ILogger<OneEndpointServiceRegister> logger)
            : base(logger, "all", $"{hosting?.Value.Uri ?? "http://*:8081"}")
        {
            Enabled = true;
        }

    }
}
