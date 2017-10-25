using dBosque.Stub.Server.AspNetCore.Host;
using dBosque.Stub.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace dBosque.Stub.Server.WebApi
{
    /// <summary>
    /// Service Register for webapi
    /// </summary>
    public class ServiceRegister : ServiceRegisterBase<Startup>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceRegister(IConfigurationRepository config, ILogger<ServiceRegister> logger, IOptions<Configuration.Hosting> hosting)
            : base(logger, "http", $"{hosting?.Value.Uri ?? "http://*:8081"}")
        { }                  
    }
}
