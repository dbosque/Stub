using System;
using System.IO;
using System.Reflection;
using dBosque.Stub.Editor.Interfaces;
using dBosque.Stub.Editor.Models;

namespace dBosque.Stub.Editor.Pluggable
{
    /// <summary>
    /// Plugin runner
    /// </summary>
    [Serializable]
    public class PluginRunner : MarshalByRefObject
    {

        public virtual bool RunPlugin(PluggedItem pluginItem, MarshalByRefObject owner)
        {
            if (!File.Exists(pluginItem.AssemblyFileName)) return false;

            Assembly ass = Assembly.LoadFile(pluginItem.AssemblyFileName);
            foreach (Type t in ass.GetTypes())
            {
                Type it = t.GetInterface(typeof(IStubEditorDockablePlugin).ToString());

                // Exists?
                if (it != null )
                {
                    // Default Constructor
                    ConstructorInfo constructorInfo = t.GetConstructor(new Type[] { });
                    var obj = ((IStubEditorDockablePlugin)constructorInfo.Invoke(null));

                    if (obj is IStubEditorConfigurablePlugin)
                    {
                        var x = GlobalSettings.Instance.Configuration.GetConnection(GlobalSettings.Instance.SelectedConnection);
                        (obj as IStubEditorConfigurablePlugin).Configure(new RuntimePluginConfiguration() { Connection = x.ConnectionString, Provider = x.ProviderName });
                    }
                    if (obj.Name == pluginItem.Name)
                        return obj.Start(owner, (int)WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide);
                }
            }
            return false;
        }
    }
}
