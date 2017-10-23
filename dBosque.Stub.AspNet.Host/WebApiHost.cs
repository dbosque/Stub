using dBosque.Stub.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace dBosque.Stub.AspNet.Host
{

    /// <summary>
    /// WebApiHost to host webapi's without IIS
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WebApiHost<T> : IDisposable, IStubService where T : class
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public WebApiHost(string url, string name, Microsoft.Extensions.Logging.ILogger logger)
        {
            _url = url;
            _name = name;
            _logger = logger;
        }

        private Microsoft.Extensions.Logging.ILogger _logger;

        private IDisposable _app;
        private readonly string _url;
        private readonly string _name;      

        /// <summary>
        /// Close the host
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        /// <summary>
        /// Start the service
        /// </summary>
        /// <returns></returns>
        public IStubService Start(Action<IServiceCollection> collectionAction, IConfiguration configuration)
        {

            var api = WebHost.CreateDefaultBuilder()            
             .UseKestrel()
             .UseStartup<T>()             
             .UseConfiguration(configuration)
             .ConfigureServices(collectionAction)
             .UseSerilog()
             .UseUrls(_url)                
             .CaptureStartupErrors(false)             
             .Build();
           
            _app = api;            
            api.Start();
            return this;
        }
        
        /// <summary>
        /// Stop the service
        /// </summary>
        /// <returns></returns>
        public IStubService Stop()
        {
            _app?.Dispose();
            _logger.LogInformation("Stopped webapi.");
            return this;
        }

        /// <summary>
        /// A description of the runtime environment
        /// </summary>
        public string Description
        {
            get
            {
                return $"Running webapi host {_name} on url : {_url}";                                 
            }
        }
    }
}
