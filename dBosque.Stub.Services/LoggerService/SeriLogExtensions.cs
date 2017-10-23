using dBosque.Stub.Services.LoggerService;
using Serilog;

namespace Microsoft.Extensions.Configuration
{
    public static class SeriLogExtensions
    {

        public static void SetupSeriLog(this IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                            .SetupSeriLog(configuration)
                            .CreateLogger();
        }
        public static LoggerConfiguration SetupSeriLog(this LoggerConfiguration c, IConfiguration configuration)
        {
            Newtonsoft.Json.JsonConvert.DefaultSettings = () => { return new Newtonsoft.Json.JsonSerializerSettings() { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore }; };

            c.ReadFrom.Configuration(configuration)
             .Enrich.FromLogContext()
             .WriteTo.Event();
            return c;
        }
    }
}
