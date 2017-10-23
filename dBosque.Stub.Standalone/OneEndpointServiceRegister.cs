using dBosque.Stub.AspNet.Host;
using dBosque.Stub.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dBosque.Stub.AspNet.Combined
{
    public class OneEndpointServiceRegister : ServiceRegisterBase<OneEndpointStartup>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public OneEndpointServiceRegister(IConfigurationRepository config, IOptions<Configuration.Hosting> hosting, ILogger<OneEndpointServiceRegister> logger)
            : base(logger, "all", $"http://*:{hosting?.Value.ApiPort ?? "8081"}")
        { }

    }
}
