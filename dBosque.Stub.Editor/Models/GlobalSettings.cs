using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Repository.Interfaces.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dBosque.Stub.Editor.Models
{
    /// <summary>
    /// Global configuration settings
    /// </summary>
    public class GlobalSettings
    {
        private static GlobalSettings _instance = null;
        private IConfigurationRepository _config;
        private IRepositoryFactory _factory;
        private string _defaultConfiguration;

        public static GlobalSettings Instance
        {
            get {
                if (_instance == null)
                    _instance = new GlobalSettings();
                return _instance;
            }
        }

        /// <summary>
        /// The datastore configuration
        /// </summary>
        public Datastore Store { get; set; }

        /// <summary>
        /// The version to use
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Configure the settings
        /// </summary>
        /// <param name="provider"></param>
        public void Configure(IServiceProvider provider)
        {
            Version = provider.GetService<IOptions<Version>>()?.Value;
            Store = provider.GetService<IOptions<Datastore>>()?.Value;
            Configuration = provider.GetService<IConfigurationRepository>();
            _factory = provider.GetService<IRepositoryFactory>();
        }

        public void Initialize()
        {
            var config = SelectedConnection ?? _config.GetAllConnectionStringKeys().First();
           
            SelectedConnection = config;
            //   Configuration = _factory.CreateConfigurationFromDb(config);
        }

        private void UpdateValues()
        {
            _defaultConfiguration = _config.GetAllConnectionStringKeys().FirstOrDefault();
            StripSoapEnvelope = (_config.Get($"Editor.{Environment.MachineName}.stripSoapEnvelope") ?? "True") == "True";
            string supportedContentTypes = _config.Get("Editor.SupportedContentTypes") ?? "";
            SupportedContentTypes = new List<string>() { string.Empty };
            SupportedContentTypes.AddRange(supportedContentTypes.Split(';').OrderBy(a => a).ToArray());
        }

        public IConfigurationRepository Configuration
        {
            set
            {
                _config = value;
                UpdateValues();
            }
            get
            {
                return _config;
            }
        }

        /// <summary>
        /// Which contenttypes are supported
        /// </summary>
        public List<string> SupportedContentTypes { get; private set; }
        
        /// <summary>
        /// Should the soapenvelope be stripped?
        /// </summary>
        public bool StripSoapEnvelope
        {
            get { return (_config.Get($"Editor.{Environment.MachineName}.stripSoapEnvelope") ?? "True") == "True"; }
            set { _config.Set($"Editor.{Environment.MachineName}.stripSoapEnvelope", value.ToString()); }
        }

        /// <summary>
        /// Get the currently selected configuration string
        /// </summary>
        public string SelectedConnection
        {
            get { return (_config.Get($"Editor.Database.{Environment.MachineName}.SelectedConnection") ?? _defaultConfiguration); }
            set
            {
                _config.Set($"Editor.Database.{Environment.MachineName}.SelectedConnection", value);
                // Make sure the current environment supports the newly selected connection
                //RepositoryFactory.SelectedConfiguration = value;
            }

        }

        /// <summary>
        /// Retrieve the autostart settings
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool PluginAutoStartup(string name)
        {
            // Default no autostart
            return ((_config.Get($"Editor.{Environment.MachineName}.PluginAutoStartup.{name}") ?? string.Empty) == "True");
        }

        /// <summary>
        /// Update the autostart settings.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void UpdatePluginAutoStartup(string name, bool value)
        {
            _config.Set($"Editor.{Environment.MachineName}.PluginAutoStartup.{name}", value.ToString());
        }
    }
}
