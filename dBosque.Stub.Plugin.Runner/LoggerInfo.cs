using dBosque.Stub.Editor.Interfaces;
using dBosque.Stub.Interfaces;
using dBosque.Stub.Services.LoggerService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace dBosque.Stub.Plugin.Runner
{
    public partial class LoggerInfo : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private bool isrunning = false;
        private bool _autostart = false;
        private MarshalByRefObject _parentwindow; 
        private Popup _popup = new Popup(string.Empty);

        private Microsoft.Extensions.Logging.ILogger _logger;

        private List<IStubService> _services = new List<IStubService>();
  
        public LoggerInfo(bool autostart = false)
        {
            _autostart = autostart;         
            InitializeComponent();
            logMessageBindingSource.DataSource = typeof(LogMessage);
            dataGridView1.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            LogWatcher.OnEvent += (y) => AppendText(y.Severity, y.Message);
            stopButton.Enabled = false;
            FormClosed += LoggerInfo_FormClosed;
        }

        private void LoggerInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopButton.PerformClick();
        }

        private IConfiguration _configuration;

        public MarshalByRefObject Parentwindow { get => _parentwindow; set => _parentwindow = value; }

        public RuntimePluginConfiguration RuntimeConfiguration { get; set; }

        private ServiceProvider SetupServices()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();


            var serviceProvider = new ServiceCollection();

            _configuration.SetupSeriLog();

            ConfigureGeneric(serviceProvider);
            serviceProvider.AddLogging()
                           .AddSocketModule()
                           .AddWCFModule()
                           .AddWebApiConfigurationModule()
                           .AddWebApiModule();
       
            var provider = serviceProvider.BuildServiceProvider();

            var fact = provider.GetService<ILoggerFactory>()
                            .AddSerilog();

            _logger = fact.CreateLogger<LoggerInfo>();
            return provider;
           
        }

        private void ConfigureGeneric(IServiceCollection collection)
        {
            collection
                .AddScoped(s => _configuration)             
                .AddRepositoryModule(_configuration)
                .AddConfigurationOverride(RuntimeConfiguration);
        }

        public void Setup()
        {
            var provider = SetupServices();
            _services.AddRange(provider.GetServices<IServiceRegister>().Select(s => s.Service).ToList());
        }

        public bool Start()
        {
            if (isrunning)
                return true;

           
            // Start
            try
            {
                _services.ForEach(a => a?.Start(ConfigureGeneric, _configuration));
                _services.ForEach(a => _logger.LogInformation(a.Description));
                isrunning = true;
                startButton.Enabled = false;
                stopButton.Enabled = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                // Shutdown
                _services.ForEach(a => a?.Stop());
                return false;
            }
            return true;
        }
        private void start_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void AppendText(LogLevel severity, string text)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate { AppendText(severity, text); };
                this.Invoke(del);
                return;
            }
            Trace.TraceInformation(string.Format("{0} : {1}", severity.ToString(), text));
            logMessageBindingSource.Insert(0,new LogMessage() { Severity = severity, Text = text });
            try
            {
                if (dataGridView1.Visible)
                    dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            }
            catch
            {
                // nothing
            }
            
        }

        private void pause_Click(object sender, EventArgs e)
        {
            if (!isrunning)
                return;
            // Stop
            _services.ForEach(a => a?.Stop());
            _services.Clear();

            stopButton.Enabled = false;
            startButton.Enabled = true;
            isrunning = false;
        }

        private void clear_Click(object sender, EventArgs e)
        {
            logMessageBindingSource.Clear();
        }

        private void close_Click(object sender, EventArgs e)
        {
            stopButton.PerformClick();
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < logMessageBindingSource.Count)
            {
                if (logMessageBindingSource[e.RowIndex] is LogMessage message)
                {
                    if (Parentwindow == null)
                        new Popup(message.Text).ShowDialog();
                    else
                        _popup.Update(message.Text).Show(Parentwindow as WeifenLuo.WinFormsUI.Docking.DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Float);
                }
            }
        }

        private void LoggerInfo_Load(object sender, EventArgs e)
        {
            if (_autostart)
                Start();
        }

        private void LoggerInfo_VisibleChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Visible && IsActivated)
                dataGridView1.FirstDisplayedScrollingRowIndex = 0;
        }
    }
}
