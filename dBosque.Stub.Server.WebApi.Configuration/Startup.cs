using dBosque.Stub.Server.WebApi.Configuration.Raw;
using dBosque.Stub.Server.WebApi.Configuration.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;

namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// Startyp configuration
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        /// <summary>
        /// The passed configuration
        /// </summary>
        public IConfiguration Configuration { get; }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.       
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {            
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "dBosque.Stub Service Configuration",
                        Description = "Standalone stub hosting for SOAP, REST and sockets.",
                        Version = "v1"
                    }
                 );
                options.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "dBosque.Stub.Server.WebApi.Configuration.xml"), true);
                //options.DescribeAllEnumsAsStrings();
                options.DocumentFilter<SwaggerOrderControllers>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("corspolicy",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost", "https://localhost:44318", "https://localhost").AllowAnyMethod();
                                  });
            });
#if NETCOREAPP
            services.AddControllersWithViews();
            services.AddRazorPages();
#endif

            services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()))
#if NETCOREAPP
                .AddJsonOptions(j =>
                {
                    j.JsonSerializerOptions.IgnoreNullValues = true;          
                });
#else
;
#endif

        }


        /// <summary>
        /// // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
      
            app.UseStaticFiles();
            app.UseCors("corspolicy");
            app.UseSwagger(c =>
            {               
               // c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Expression);
            });

            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "dBosque.Stub Service Configuration";
                c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
                c.RoutePrefix = "info";
                // Relative to the UI path
                c.SwaggerEndpoint("swagger/v1/swagger.json", "V1 Docs");
               
            });
#if NETCOREAPP
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
            app.UseBlazorFrameworkFiles();
            app.UseRouting();
            app.UseEndpoints(r => {
                r.MapRazorPages();
                r.MapControllers();
            });
#else
            app.UseMvc();
#endif
        }
    }
}
