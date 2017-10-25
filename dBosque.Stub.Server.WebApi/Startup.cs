using dBosque.Stub.Interfaces;
using dBosque.Stub.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dBosque.Stub.Server.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTransient<IStubHandler<IActionResult>, GenericStubHandler<IActionResult>>()
                .AddMvc()
                .AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "passthroughTenantSpecific",
                    template: "passthrough/private/{tenant}/{protocol}/{pass}/{*uri}",
                    defaults: new { controller = "GenericApi", action = "Passthrough" });

                routes.MapRoute(
                    name: "passthrougSpecific",
                    template: "passthrough/{protocol}/{pass}/{*uri}",
                    defaults: new { controller = "GenericApi", action = "PassthroughDefault" });

                routes.MapRoute(
                    name: "TenantSpecific",
                    template: "private/{tenant}/{*uri}",
                    defaults: new { controller = "GenericApi", action = "Execute" });

                routes.MapRoute(
                    name: "Generic",
                    template: "{*uri}",
                    defaults: new { controller = "GenericApi", action = "ExecuteDefault" });

            });
          
        }
    }
}
