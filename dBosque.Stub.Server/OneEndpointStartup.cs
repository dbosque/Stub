using dBosque.Stub.Server.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace dBosque.Stub.Server
{
    public class OneEndpointStartup
    {
        private WebApi.Startup _webApi;
        private WebApi.Configuration.Startup _configApi;
        private Soap.Startup _soapApi;

        public OneEndpointStartup(IConfiguration configuration)
        {
            Configuration = configuration;
            _soapApi = new Soap.Startup(configuration);
            _webApi = new WebApi.Startup(configuration);
            _configApi = new WebApi.Configuration.Startup(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _soapApi.ConfigureServices(services);   
            _webApi.ConfigureServices(services);
            _configApi.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<Configuration.Hosting> hosting, ILogger<OneEndpointStartup> logger)
        {
            string basePath = hosting.Value?.Uri ?? "http://*:8081";
            Configure(app, logger, "http", basePath, hosting.Value?.WebApi, (a, path) => a.Map(path, main => _webApi.Configure(main)));
            Configure(app, logger, "soap", basePath, hosting.Value?.SoapApi, (a, path) => a.Map(path, main => _soapApi.Configure(main)));
            Configure(app, logger, "configuration", basePath, hosting.Value?.ConfigurationApi, (a, path) => a.Map(path, main => _configApi.Configure(main)));
        }

        private void Configure(IApplicationBuilder app, ILogger<OneEndpointStartup> logger, string name, string basePath, Endpoint endpoint , Action<IApplicationBuilder, string> action)
        {
            if (endpoint?.Enabled ?? true)
            {
                action(app, $"/{endpoint.Path ?? name}");
                logger.LogInformation($"Running {name} on url {basePath}/{endpoint.Path ?? name}");
            }
        }
    }
}
