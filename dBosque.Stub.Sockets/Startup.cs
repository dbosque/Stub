using dBosque.Stub.Interfaces;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dBosque.Stub.Sockets
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
            services.AddTransient<IStubHandler<string>, GenericStubHandler<string>>();
        }
    }
}
