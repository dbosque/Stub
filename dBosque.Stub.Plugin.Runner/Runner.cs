using dBosque.Stub.Editor.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace dBosque.Stub.Plugin.Runner
{
    public class Runner : IStubEditorDockablePlugin, IStubEditorConfigurablePlugin
    {
        public event EventHandler OnExit;

        private LoggerInfo _form;

        public Runner()
        {
            _form = new LoggerInfo(true);
            _form.FormClosing += Form_FormClosing;
        }

        ///<summary>
        ///The display name of the plugin
        ///</summary>
        public string Name => "Runner";

        ///<summary>
        ///Is it possible to dock the plugin
        ///</summary>
        public bool Dockable
        {
            get
            {
                return true;
            }
        }

        ///<summary>
        ///Start the plugin
        ///</summary>
        public bool Start()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);
            Application.Run(_form);
            return true;
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
           OnExit?.Invoke(this, EventArgs.Empty);
        }

        ///<summary>
        ///Start the plugin
        ///</summary>
        public bool Start(MarshalByRefObject window, int dockstate)
        {
            _form.Show(window as WeifenLuo.WinFormsUI.Docking.DockPanel, (WeifenLuo.WinFormsUI.Docking.DockState)dockstate);
            return _form.Start();
        }

        public void Configure(RuntimePluginConfiguration configuration)
        {
            _form.RuntimeConfiguration = configuration; 
            _form.Setup();
        }
    }
}
