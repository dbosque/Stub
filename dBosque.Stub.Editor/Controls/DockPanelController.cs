using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using dBosque.Stub.Editor.Controls.Extensions;
using dBosque.Stub.Editor.Controls.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace dBosque.Stub.Editor.Controls
{
    public class DockPanelController
    {
        private DockPanel _panel;
        private ToolStripMenuItem _menu;
        private IControlFactory _factory;

        private LoggingControl log;
        private MessageTypeViewControl msg;
        private PropertyEditor prop;
        private MessageEditor msg_edt;

        public DockPanelController(IEventBroker broker)
        {
            broker.Register(this);
        }

        [EventPublication("topic://Documents.CloseAll")]
        public event EventHandler CloseAllDocuments;

        [EventSubscription("topic://ApplicationClosed", typeof(OnPublisher))]
        public void OnApplicationClosed(object sender, EventArgs e)
        {
            CloseAll();
        }


        /// <summary>
        /// Register the control factory
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public DockPanelController RegisterFactory(IControlFactory factory)
        {
            // Handle separatly, otherwise we get a circular reference
            _factory = factory;
            return this;
        }

        /// <summary>
        /// Register the dockpanel
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public DockPanelController RegisterPanel(DockPanel panel)
        {
            _panel = panel;
            return this;
        }

        /// <summary>
        /// Register the menu shortcut
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public DockPanelController RegisterMenu(ToolStripMenuItem menu)
        {
            _menu = menu;
            return this;
        }

        /// <summary>
        /// Reset the windows layout
        /// </summary>
        public void Reset()
        {
            CloseAll();
            Create(true);
        }

        /// <summary>
        /// Save the current layout
        /// </summary>
        public void Save()
        {
            string configFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "dBosque.Stub.Editor.layout");

            _panel.SaveAsXml(configFile);
        }
        
        /// <summary>
        /// Close all documents
        /// </summary>
        public void CloseAll()
        {
            _panel.SuspendLayout(true);
            log.DockPanel = null;
            msg.DockPanel = null;
            prop.DockPanel = null;
            msg_edt.DockPanel = null;

            CloseAllDocuments.Invoke();
            foreach (DockContent window in _panel.Contents.ToList())
            {
                window.Close();
                window.DockPanel = null;
            }
            foreach (FloatWindow window in _panel.FloatWindows.ToList())
            {
                window.Close();
                window.Dispose();
            }
            _menu.DropDownItems.RemoveByKey("&Stubs");
            _menu.DropDownItems.RemoveByKey("&Message Editor");
            _menu.DropDownItems.RemoveByKey("&Properties");
            _menu.DropDownItems.RemoveByKey("&Logging"); 

            _panel.ResumeLayout(true, true);
        }

        /// <summary>
        /// Create all controls
        /// </summary>
        /// <param name="reset"></param>
        public void Create(bool reset = false)
        {
            _panel.SuspendLayout(true);
            _panel.ShowDocumentIcon = false;
            _panel.Theme = new VS2015BlueTheme();

            log = _factory.Create<LoggingControl>();
            msg = _factory.Create<MessageTypeViewControl>();
            prop = _factory.Create<PropertyEditor>();
            msg_edt = _factory.Create<MessageEditor>();

            // Try to load the settings from the configuration
            string configFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "dBosque.Stub.Editor.layout");

            if (System.IO.File.Exists(configFile) && !reset)
                _panel.LoadFromXml(configFile, new DeserializeDockContent(_factory.CreateFrom));
            else
                SetDefaultLayout();
            
            _menu.DropDownItems.Insert(0, new ToolStripMenuItem("&Stubs", null, (a, b) => msg.Show(_panel, DockState.Float)));
            _menu.DropDownItems.Insert(0, new ToolStripMenuItem("&Message Editor", null, (a, b) => msg_edt.Show(_panel, DockState.Float)));
            _menu.DropDownItems.Insert(0, new ToolStripMenuItem("&Properties", null, (a, b) => prop.Show(_panel, DockState.Float)));
            _menu.DropDownItems.Insert(0, new ToolStripMenuItem("&Logging", null, (a, b) => log.Show(_panel, DockState.Float)));

            _panel.ResumeLayout(true, true);

        }

        private void SetDefaultLayout()
        {
            log.Show(_panel, DockState.DockBottomAutoHide);
            msg_edt.Show(_panel, DockState.DockRight);
            msg.Show(_panel, DockState.DockTop);
            prop.Show(_panel, DockState.Float);

            prop.DockHandler.FloatPane.DockTo(_panel.DockWindows[DockState.DockRight]);

            _panel.UpdateDockWindowZOrder(DockStyle.Right, true);
        }
    }
}
