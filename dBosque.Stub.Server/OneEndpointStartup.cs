using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace dBosque.Stub.Server
{
    public class OneEndpointStartup
    {
        private WebApi.Startup _webApi;
        private WebApi.Configuration.Startup _configApi;
        private Soap.Startup _wcfApi;

        public OneEndpointStartup(IConfiguration configuration)
        {
            Configuration = configuration;
            _wcfApi = new Soap.Startup(configuration);
            _webApi = new WebApi.Startup(configuration);
            _configApi = new WebApi.Configuration.Startup(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _wcfApi.ConfigureServices(services);
            _webApi.ConfigureServices(services);
            _configApi.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // _webApi.Configure(app, env);
            app.Map("/http", main => _webApi.Configure(main, env));
            app.Map("/soap", main => _wcfApi.Configure(main, env));
            app.Map("/configuration", main => _configApi.Configure(main, env));

        }
    }
}
