using dBosque.Stub.Interfaces;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Linq;
using System.Reflection;

namespace dBosque.Stub.Server
{
    class Program
    {

        private static IConfiguration _configuration;
        private static CommandOption _onePort;
        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
           
            try
            {
                var app = SetUpApp();
                app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                // You'll always want to catch this exception, otherwise it will generate a messy and confusing error for the end user.
                // the message will usually be something like:
                // "Unrecognized command or argument '<invalid-command>'"
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to execute application: {0}", ex.Message);
            }

        }

      
        /// <summary>
        /// Setup the console app
        /// </summary>
        /// <returns></returns>
        private static CommandLineApplication SetUpApp()
        {
            var app = new CommandLineApplication
            {
                Name = "dBosque.Stub.Server",
                Description = "Standalone stub hosting for SOAP, REST and sockets.",
                ExtendedHelpText = Environment.NewLine +
                                   "Standalone stub hosting for SOAP, REST and sockets." + Environment.NewLine +
                                   "Default configuration is stored in the appsettings.json file." + Environment.NewLine + Environment.NewLine +
                                   "Supports processing of input messages based on the URI and messagebody and returns a specific (configured) response." + Environment.NewLine +
                                   "If no specific response is available but a passthourgh URL is provided or configured, the message will be forwarded." + Environment.NewLine +
                                   "Messages can be added through the configuration API or the additonal Editor application." + Environment.NewLine
            };
            app.HelpOption("-?|-h|--help");
            app.VersionOption("-v|--version", () => {
                return string.Format("Version {0}", Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            });
            _onePort = app.Option("-o|--oneport", "Run all SOAP and REST services on the same port. ('/soap', '/http', '/configuration')", CommandOptionType.NoValue);

            app.OnExecute(() =>
            {
                Run();
                return 0;
            });

            return app;
        }

        /// <summary>
        /// Run the actual program
        /// </summary>
        private static void Run()
        {
            var provider = Configure().BuildServiceProvider();

            // Setup logging
            ConfigureLogging(provider);

            // Setup database
            AssertDatabaseAvailability();

            // Get all services
            var services = provider.GetServices<IServiceRegister>().Where(s => s.Enabled).Select(s => s.Service).ToList();
            var configuration = provider.GetService<IConfiguration>();
            // Start all
            services.ForEach(s => s.Start(ConfigureGeneric, configuration));
            services.ForEach(s => Log.Logger.Information(s.Description));

            Console.WriteLine("Press any key to shutdown.");
            Console.ReadLine();
            services.ForEach(s => s?.Stop());
        }

        /// <summary>
        /// Configure the loggingframework
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private static void ConfigureLogging(IServiceProvider provider)
        {        
            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                   .SetupSeriLog(_configuration)
                   .WriteTo.RollingFile("./data/logs/log-{Date}.txt")
                   .WriteTo.Console()
                   .CreateLogger();

            var fact = provider.GetService<ILoggerFactory>()
                            .AddSerilog();
        }


        /// <summary>
        /// Make sure the correct database file is available
        /// </summary>
        /// <param name="logger"></param>
        private static void AssertDatabaseAvailability()
        {
            if (!System.IO.File.Exists("./data/dbstub.db"))
            {
                Log.Logger.Warning($"No database at ./data");
                if (!System.IO.Directory.Exists("./data") && System.IO.File.Exists("dbstub.db"))
                    System.IO.Directory.CreateDirectory("./data");
                System.IO.File.Copy("dbstub.db", "./data/dbstub.db");
                Log.Logger.Information("Default database copied to ./data");
            }
        }
        
        /// <summary>
        /// Configure generic services, will also be called by the webApiHost
        /// </summary>
        /// <param name="collection"></param>
        private static void ConfigureGeneric(IServiceCollection collection)
        {
            collection
                .AddScoped(s => _configuration)
                .AddRepositoryModule(_configuration)
                .Configure<Configuration.Hosting>(_configuration.GetSection("Server"));
        }


        /// <summary>
        /// Configure Dependencyinjection
        /// </summary>
        /// <returns></returns>
        private static IServiceCollection Configure()
        {
            _configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .Build();

            var serviceProvider = new ServiceCollection();
            ConfigureGeneric(serviceProvider);

            serviceProvider.AddLogging()
                           .AddSocketModule(_configuration)
                           .AddHostingModule()
                           .AddWebApiModule(_configuration)
                           .AddWebApiConfigurationModule(_configuration)
                           .AddSoapModule(_configuration);

            return serviceProvider;
        }
    }
}
