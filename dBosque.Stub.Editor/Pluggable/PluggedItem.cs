using System;
using dBosque.Stub.Editor.Pluggable;
using System.Windows.Forms;
using System.Threading;

namespace dBosque.Stub.Editor
{
    /// <summary>
    /// Container class for a plugin
    /// </summary>
    [Serializable]
    public class PluggedItem
    {
        public string Name              { get; set; }
        public string TypeName          { get; set; }
        public string AssemblyFileName  { get; set; }
        public bool DockPossible        { get; set; }
        public bool AutoStart           { get; set; }

        /// <summary>
        /// ThreadPool callback for plugin
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void RunPlugin(Object stateInfo)
        {
            try
            {
              new AppDomainPluginRunner().RunPlugin(stateInfo as PluggedItem, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        /// <summary>
        /// Start a specific plugin
        /// </summary>
        /// <param name="owner"></param>
        public bool Start(MarshalByRefObject owner = null)
        {
            if (owner != null)
            {
                return new PluginRunner().RunPlugin(this, owner);                
            }
            // Queue the running in the threadpool so we can keep on going
            ThreadPool.QueueUserWorkItem(new WaitCallback(RunPlugin), this);
            return true;
        }
    }
}
