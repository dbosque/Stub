using System;
using System.IO;
using System.Reflection;
using dBosque.Stub.Editor.Interfaces;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Pluggable
{

    /// <summary>
    /// Plugin runner for seperate Appdomain
    /// </summary>
    [Serializable]
    public class AppDomainPluginRunner : PluginRunner
    {
        #region RunInDomain
        /// <summary>
        /// Runs the specified type name in a new domain.
        /// </summary>
        /// <param name="pluginItem">The plugin</param>
        /// <param name="owner">The owner object</param>
        public override bool RunPlugin(PluggedItem pluginItem, MarshalByRefObject owner)
        {
            if (!File.Exists(pluginItem.AssemblyFileName))
                throw new FileNotFoundException("File not found", pluginItem.AssemblyFileName);

            //create a new domain with a config
            AppDomainSetup setup = new AppDomainSetup()
            {
                //directory where the dll and dependencies are
                ApplicationBase = Path.GetDirectoryName(pluginItem.AssemblyFileName),
                ApplicationName = "PluginRunner"
            };

            //try to load a configuration file
            if (File.Exists(pluginItem.AssemblyFileName + ".config"))
                setup.ConfigurationFile = pluginItem.AssemblyFileName + ".config";

            //Create the new domain
            AppDomain domain = AppDomain.CreateDomain("PluginRunner", null, setup);
            try
            {
                //load this assembly/ type into the new domain
                var runner =
                    (AppDomainPluginRunner)domain.CreateInstanceFromAndUnwrap(
                    Assembly.GetExecutingAssembly().Location,
                    typeof(AppDomainPluginRunner).FullName);

                //other instance of this class in new domain loads dll
                return runner.RunPluginEx(pluginItem);

            }
            finally
            {
                //unload domain
                AppDomain.Unload(domain);
            }
        }
        #endregion

        #region private properties
        /// <summary>
        /// Is the current plugin running?
        /// </summary>
        private bool _pluginRunning;

        #endregion

        /// <summary>
        /// Loads the DLL (this should be run in a different domain)
        /// </summary>
        /// <param name="pluginItem">Name of the type.</param>
        protected bool RunPluginEx(PluggedItem pluginItem)
        {
            if (!File.Exists(pluginItem.AssemblyFileName)) return false;

            string location = Path.GetDirectoryName(pluginItem.AssemblyFileName);
            //resolve any dependencies
            AppDomain.CurrentDomain.AssemblyResolve +=
                delegate (object sender, ResolveEventArgs args)
                {
                    string findName = args.Name;
                    string simpleName = new AssemblyName(findName).Name;
                    string assemblyPath = Path.Combine(location, simpleName) + ".dll";
                    if (File.Exists(assemblyPath))
                        return Assembly.LoadFrom(assemblyPath);
                    //can't find it
                    return null;
                };

            //load the assembly into bytes and load it
            byte[] assemblyBytes = File.ReadAllBytes(pluginItem.AssemblyFileName);
            Assembly a = Assembly.Load(assemblyBytes);

            //find the type in the assembly
            Type t = a.GetType(pluginItem.TypeName, true);
            ConstructorInfo constructorInfo = t.GetConstructor(new Type[] { });

            // Create the plugin
            var plugin = (IStubEditorlugin)constructorInfo.Invoke(null);

            // Add eventhandler for exit.
            plugin.OnExit += plugin_OnExit;

            // Start the plugin           
            _pluginRunning = plugin.Start();

            // Wait until the plugin exited.
#pragma warning disable S2589 // Boolean expressions should not be gratuitous
            while (_pluginRunning)
#pragma warning restore S2589 // Boolean expressions should not be gratuitous
                Application.DoEvents();
            return true;
        }

        #region Plugin Events

        void plugin_OnExit(object sender, EventArgs e)
        {
            // Signal that we can stop waiting
            _pluginRunning = false;
        }
        #endregion
    }
}
