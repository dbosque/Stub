using dBosque.Stub.WebApi.Configuration.Raw;
using dBosque.Stub.WebApi.Configuration.Swagger;
using dBosque.Stub.WebApi.Json.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace dBosque.Stub.WebApi.Configuration
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
                    new Info
                    {
                        Title = "Configuration",
                        Version = "v1",
                        TermsOfService = "None"
                    }
                 );
                var filePath = System.IO.Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "dBosque.Stub.WebApi.Configuration.xml");
                options.IncludeXmlComments(filePath);
                options.DescribeAllEnumsAsStrings();
                options.DocumentFilter<SwaggerOrderControllers>();
            });

            services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()))
                .AddJsonOptions(j =>
                {
                    j.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    j.SerializerSettings.ContractResolver = BaseTypeFirstContractResolver.Instance;
                });


        }


        /// <summary>
        /// // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
      
            app.UseStaticFiles();

            app.UseSwagger(c =>
            {               
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });

            app.UseSwaggerUI(c =>
            {                
                // Relative to the UI path
                c.SwaggerEndpoint("v1/swagger.json", "V1 Docs");
               
            });

            app.UseMvc();
        }
    }
}
