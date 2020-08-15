using dBosque.Stub.Interfaces;
using dBosque.Stub.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace dBosque.Stub.Server.WebApi
{
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
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
        public void Configure(IApplicationBuilder app)
        {
#if NETCOREAPP
            app.UseRouting()
               .UseSerilogRequestLogging()
               .UseEndpoints(routes =>
            {

                routes.MapControllerRoute(
                    name: "passthroughTenantSpecific",
                    pattern: "passthrough/private/{tenant}/{protocol}/{pass}/{*uri}",
                    defaults: new { controller = "GenericApi", action = "Passthrough" });

                routes.MapControllerRoute(
                    name: "passthrougSpecific",
                    pattern: "passthrough/{protocol}/{pass}/{*uri}",
                    defaults: new { controller = "GenericApi", action = "PassthroughDefault" });

                routes.MapControllerRoute(
                    name: "TenantSpecific",
                    pattern: "private/{tenant}/{*uri}",
                    defaults: new { controller = "GenericApi", action = "Execute" });

                routes.MapControllerRoute(
                    name: "Generic",
                    pattern: "{*uri}",
                    defaults: new { controller = "GenericApi", action = "ExecuteDefault" });
            });
#else
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
#endif
        }
    }
}
