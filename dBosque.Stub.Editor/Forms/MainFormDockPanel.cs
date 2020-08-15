using Appccelerate.EventBroker;
using dBosque.Stub.Editor.Controls;
using dBosque.Stub.Editor.Controls.Errorhandling;
using dBosque.Stub.Editor.Controls.Extensions;
using dBosque.Stub.Editor.Controls.Interfaces;
using dBosque.Stub.Editor.Flow;
using dBosque.Stub.Editor.Models;
using dBosque.Stub.Editor.Pluggable;
using dBosque.Stub.Repository.Interfaces;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace dBosque.Stub.Editor.Forms
{
    public partial class MainFormDockPanel : Form, IMainFormEvents
    {
        public event EventHandler CloseAllDocuments;
        public event EventHandler ApplicationClosed;
        public event EventHandler ApplicationLoaded;
        public event EventHandler<RepositoryFlowEventArgs> RepositoryChanged;

        #region DLLImport
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, IntPtr lParam);

        private const uint WM_SETICON = 0x80u;
        private const int ICON_SMALL = 0;
        private const int ICON_BIG = 1;
        #endregion

        private string _header;
        private IRepositoryFactory _factory;
        private IEventBroker _broker;
        private IStubDataRepository _repository = null;
        private IConfigurationRepository _config;
        private IControlFactory _controlFactory;
        private TemplateViewController _controller;
        private DockPanelController _dockController;
        private Plugins _plugins;

        private readonly ToolStripRenderer _toolStripProfessionalRenderer = new ToolStripProfessionalRenderer();


        [DesignOnly(true)]
        public MainFormDockPanel()            
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            _header = Text;
            Load += (a, b) => ApplicationLoaded.Invoke();
            FormClosed += (a, b) => ApplicationClosed.Invoke();

            // Set the correct icons, one for the taskbar, one for the form
            SendMessage(Handle, WM_SETICON, ICON_BIG, Properties.Resources.ic_cloud_off_white_48dp.GetHicon());
            SendMessage(Handle, WM_SETICON, ICON_SMALL, Properties.Resources.ic_cloud_off_black_48dp.GetHicon());

            visualStudioToolStripExtender1.SetStyle(toolStrip1, VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015BlueTheme1);
            visualStudioToolStripExtender1.SetStyle(menuStrip1, VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015BlueTheme1);

            toolStrip1.Enabled = windowsToolStripMenuItem.Enabled = settingsToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="config"></param>
        /// <param name="broker"></param>
        /// <param name="controlFactory"></param>
        /// <param name="controller"></param>
        public MainFormDockPanel(IRepositoryFactory factory, 
            IConfigurationRepository config, 
            IEventBroker broker,
            IControlFactory controlFactory, 
            TemplateViewController controller,
            DockPanelController dockController)
            :this()
        {
            _factory = factory;
            
            _controlFactory = controlFactory;
            _config = config;
            _broker = broker;
            _dockController = dockController.RegisterPanel(dockPanel1)
                                            .RegisterMenu(windowsToolStripMenuItem)
                                            .RegisterFactory(controlFactory);

            _controller = controller.RegisterPanel(dockPanel1)
                                    .RegisterMenu(windowsToolStripMenuItem)
                                    .RegisterFactory(controlFactory);

            _broker.Register(this);
            _plugins = new Plugins(dockPanel1, _broker).LoadAll();

             if (_config.HasConnectionStrings)
                CreateControlsForActiveConnection();
          
            visualStudioToolStripExtender1.DefaultRenderer = _toolStripProfessionalRenderer;

        }

        /// <summary>
        /// Catch event when the repository changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnRepositoryChanged(RepositoryFlowEventArgs e)
        {
            Text = $"{_header} - Using {e.Item.Connection}.";
            UpdateAvailabilityPlugins();
            UpdateStripSoapEnvironment();
            UpdateAvailabilityTenantConfiguration();
        }

        ///<summary>
        ///Handle the event when the application is shown for the first time
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        public void OnShow(EventArgs e)
        {
            if (!_config.HasConnectionStrings)
            {
                var form = _controlFactory.Create<ConnectionStringSelection>();
                form.ShowDialog();
                if (!string.IsNullOrEmpty(form.Selected))
                {
                    GlobalSettings.Instance.SelectedConnection = form.Selected;
                    CreateControlsForActiveConnection();
                }
            }
        }

        private void CreateControlsForActiveConnection()
        {
            GlobalSettings.Instance.Initialize();
            _factory.SelectedConfiguration = _config.GetConnection(GlobalSettings.Instance.SelectedConnection);
            _dockController.Create();
            toolStrip1.Enabled = windowsToolStripMenuItem.Enabled = settingsToolStripMenuItem.Enabled = true;
            ForceRepositoryReload();
        }

        private void UpdateAvailabilityPlugins()
        {
            _plugins.SetupControls(pluginButton, pluginsToolStripMenuItem);
        }

        private bool ForceRepositoryReload()
        {
            try
            {
                _repository = null;
                Repository.Flush();
                return true;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error connecting to the database.");
            }
            return false;
        }

        private IStubDataRepository Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = _factory.CreateDataRepository();
                    RepositoryChanged.Invoke(new RepositoryFlowEventArgs(Repository));
                }
                return _repository;
            }
        }
        ///<summary>
        ///Handle the Error event
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>   
        public void OnError( ErrorEventArgs e)
        {
            MessageBox.Show(e.Message, e.Caption);
        }

    
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _dockController.Save();
            Close();
            Application.Exit();
        }

        private void UpdateStripSoapEnvironment()
        {
            stripSoapEnvelopeButton.Image = GlobalSettings.Instance.StripSoapEnvelope ? Properties.Resources.ic_drafts_black_24dp : Properties.Resources.ic_mail_black_24dp;
            stripSoapEnvelopeButton.Text = GlobalSettings.Instance.StripSoapEnvelope ? "Strip SoapEnvelope" : "Keep SoapEnvelope";
        }

        private void stripSoapEnvelopeButton_Click(object sender, EventArgs e)
        {
            GlobalSettings.Instance.StripSoapEnvelope = !GlobalSettings.Instance.StripSoapEnvelope;
            UpdateStripSoapEnvironment();
        }

        private void connectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var selected = GlobalSettings.Instance.SelectedConnection;
            var form = _controlFactory.Create<ConnectionStringSelection>().Setup(selected, _config);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Selected != selected)
                {
                    GlobalSettings.Instance.SelectedConnection = form.Selected;
                    _factory.SelectedConfiguration = _config.GetConnection(form.Selected);

                    // Make sure the repository is loaded from scratch
                    if (ForceRepositoryReload())
                    {
                        _dockController.CloseAll();
                        _dockController.Create();
                    } else
                    {
                        GlobalSettings.Instance.SelectedConnection = selected;
                        _factory.SelectedConfiguration = _config.GetConnection(selected);
                        ForceRepositoryReload();
                    }
                }
            }
        }
        private void UpdateAvailabilityTenantConfiguration()
        {
            tenantsButton.Visible = tenantsToolStripMenuItem1.Visible = false;
            if (Repository is IStubConfigurationRepository && (GlobalSettings.Instance.Version?.IsFull??false) )
            {
                bool enabled = (Repository as IStubConfigurationRepository).GetAllTenants().Any();
                tenantsButton.Visible = tenantsToolStripMenuItem1.Visible = enabled;
            }
        }

        private void tenantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TenantsForm() { Repository = _factory.CreateConfiguration() as IStubConfigurationRepository }.ShowDialog();
        }

        private void resetWindowLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _dockController.Reset();
        }

        private void closeLlDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllDocuments.Invoke();            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
    }

  
}
