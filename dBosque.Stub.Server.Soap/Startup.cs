using dBosque.Stub.Interfaces;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Repository.Interfaces.Configuration;
using dBosque.Stub.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace dBosque.Stub.Server.Soap
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
                .AddTransient<IStubHandler<Message>, GenericStubHandler<Message>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSOAPEndpoint( new BasicHttpBinding());
        }
    }
}
