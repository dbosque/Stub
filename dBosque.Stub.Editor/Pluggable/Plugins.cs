using dBosque.Stub.Editor.Interfaces;
using dBosque.Stub.Editor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Pluggable
{
    /// <summary>
    /// This class interacts with the different plugins found.
    /// </summary>
    internal class Plugins
    {
        /// <summary>
        /// Create a plugin handler
        /// </summary>
        /// <param name="owner"></param>
        public Plugins(MarshalByRefObject owner)
        {
            _owner = owner;
        }

        private MarshalByRefObject _owner;
        /// <summary>
        /// A list of loaded plugins.
        /// </summary>
        private IList<PluggedItem> _loadedPlugins = new List<PluggedItem>();

        #region LoadAll plugins

        /// <summary>
        /// Load all Plugins
        /// </summary>
        public Plugins LoadAll()
        {
            // Get all dll's in the current directory.
            FileInfo[] files = new DirectoryInfo(".").GetFiles("*.dll", SearchOption.AllDirectories);
            foreach (FileInfo file in files)
            {
                // Add to list
                foreach (PluggedItem p in GetPlugins(file.FullName))
                    _loadedPlugins.Add(p);

            }
            // Sort by name
            ((List<PluggedItem>)_loadedPlugins).Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.CurrentCulture));
            return this;
        }

        public Plugins SetupControls(ToolStripDropDownButton pluginButton, ToolStripMenuItem pluginsToolStripMenuItem)
        {            

            var menuitems = _loadedPlugins.Select(p => new ToolStripMenuItem() { Name = p.Name, Text = p.Name, Tag = p }).ToList();
            // Hookup click event
            menuitems.ForEach(i => i.Click += dummyToolStripMenuItem_Click);
            menuitems.ForEach(i => i.DropDownItems.Add(new ToolStripMenuItem("Autostart", null, autoStartToolStripMenuItem_Click, i.Name)));

            menuitems.Where(a => ((PluggedItem)a.Tag).AutoStart).ToList().ForEach(a => { a.PerformClick(); ((ToolStripMenuItem)a.DropDownItems[0]).Checked = true; });
            // Add all plugins to the menubar.
            pluginButton.DropDownItems.Clear();
            pluginButton.DropDownItems.AddRange(menuitems.ToArray());

            pluginsToolStripMenuItem.DropDownItems.Clear();
            pluginsToolStripMenuItem.DropDownItems.AddRange(menuitems.ToArray());

            // If there are plugins, show the menubar   
            pluginButton.Visible = _loadedPlugins.Count > 0;
            pluginsToolStripMenuItem.Visible = _loadedPlugins.Count > 0;

            return this;
        }

        private void autoStartToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Is it a ToolStripItem
            ToolStripMenuItem currentItem = sender as ToolStripMenuItem;
            if (currentItem != null)
            {
                currentItem.Checked = !currentItem.Checked;
                GlobalSettings.Instance.UpdatePluginAutoStartup(currentItem.Name, currentItem.Checked);
            }
        }

        private void dummyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Is it a ToolStripItem
            ToolStripItem currentItem = sender as ToolStripItem;
            if (currentItem != null)
            {
                // Does it contain a plugin?
                PluggedItem plugin = currentItem.Tag as PluggedItem;
                // Start it.
                if (plugin != null && !plugin.Start(_owner))
                    MessageBox.Show($"Error running plugin. {plugin?.Name}");
            }
        }

        /// <summary>
        /// Get a list of plugins for the given file
        /// </summary>  
        /// <param name="filename"></param>
        /// <returns></returns>
        private IList<PluggedItem> GetPlugins(string filename)
        {
            IList<PluggedItem> plugins = new List<PluggedItem>();
            try
            {
                // Load assembly
                Assembly ass = Assembly.LoadFile(filename);
                // Get All Types
                foreach (Type t in ass.GetTypes())
                {
                    try
                    {
                        // Try to get the interface 'IdBosque.Stub.EditorPlugin'
                        Type it = t.GetInterface(typeof(IStubEditorlugin).ToString());

                        // Exists?
                        if (it != null)
                        {
                            // Default Constructor
                            ConstructorInfo constructorInfo = t.GetConstructor(new Type[] { });
                            var obj = ((IStubEditorlugin)constructorInfo.Invoke(null));
                            plugins.Add(
                                new PluggedItem()
                                {
                                    Name = obj.Name,
                                    TypeName = t.FullName,
                                    AssemblyFileName = filename,
                                    DockPossible = (obj as IStubEditorDockablePlugin)?.Dockable ?? false,
                                    AutoStart = GlobalSettings.Instance.PluginAutoStartup(obj.Name)
                                });
                        }
                    }
                    catch
                    { 
                         // Just Continue
                    }
                }
            }
            catch
            {
                // Just continue
            }
            
            // Return the found plugins
            return plugins;
        }

        #endregion
    }
}
