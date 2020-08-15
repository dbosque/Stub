using Appccelerate.EventBroker;
using dBosque.Stub.Editor.Controls.Behaviours;
using dBosque.Stub.Editor.Controls.Extensions;
using dBosque.Stub.Editor.Flow;
using dBosque.Stub.Editor.Models;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Repository.StubDb.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls
{
    public partial class LoggingControl : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private enum logVisibility
        {
            All,
            Matches,
            NoMatches

        }
        private logVisibility _logvisibility = logVisibility.All;
        private long last_id = 0;
        private PeriodicEventFocusBehaviour _tick;
        private IRepositoryFactory _factory;   

        [EventPublication("topic://CreateMessageType")]
        public event EventHandler<CreateMessageTypeFlowEventArgs> OnCreateMessageType;

        [DesignOnly(true)]
        public LoggingControl() 
            : this(null, null)
        { }

        public LoggingControl(IRepositoryFactory factory, IEventBroker broker)
        {
            broker.Register(this);
            InitializeComponent();
            _factory = factory;
            _tick = new PeriodicEventFocusBehaviour(this, () => UpdateLogging(false), updatePlayStopButton);
            updatePlayStopButton();            
        }   

        private void UpdateLogging(bool force = false)
        {
            tenantDataGridViewTextBoxColumn.Visible = GlobalSettings.Instance.Version?.IsFull ?? false;
            try
            {
                // Get the query to use
                var repos = _factory.CreateConfiguration();
                var data = _factory.CreateDataRepository();//GlobalSettings.Instance.Configuration);
                var logQuery = repos.Get("Log." + data.Provider);

                var list = data.GetLogs(100, 0, false, logQuery).ToList();

                if (list.Any() && last_id != list.Max(a => a.Id) || force)
                {
                    last_id = list.Max(a => a.Id);
                    switch (_logvisibility)
                    {
                        case logVisibility.All:
                            dataGridView1.DataSource = list;
                            break;
                        case logVisibility.Matches:
                            dataGridView1.DataSource = list.Where(a => !string.IsNullOrEmpty(a.Combination)).ToList();
                            break;
                        default:
                            dataGridView1.DataSource = list.Where(a => string.IsNullOrEmpty(a.Combination)).ToList();
                            break;
                    }
                }
            }
            catch // Catch all, exception of no connection is triggerd somewhere else
            { 
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var log = (dataGridView1.DataSource as List<stp_selectLog_Result>)?[e.RowIndex];
            if (log != null)
                OnCreateMessageType.Invoke(new CreateMessageTypeFlowEventArgs() { Content = string.IsNullOrEmpty(log.Request) ? log.Uri: log.Request, Namespace = log.Namespace, Rootnode = log.Rootnode }); 
        }

  
        private void allToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            matchesToolStripMenuItem.Checked = noMatchesToolStripMenuItem.Checked = false;
            _logvisibility = logVisibility.All;
            allToolStripMenuItem.Checked = true;
            UpdateLogging(true);
        }

        private void matchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allToolStripMenuItem.Checked = noMatchesToolStripMenuItem.Checked = false;
            _logvisibility = logVisibility.Matches;
            matchesToolStripMenuItem.Checked = true;
            UpdateLogging(true);
        }

        private void noMatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allToolStripMenuItem.Checked = matchesToolStripMenuItem.Checked = false;
            _logvisibility = logVisibility.NoMatches;
            noMatchesToolStripMenuItem.Checked = true;
            UpdateLogging(true);
        }
        private void updatePlayStopButton()
        {
            playstopButton.Image = _tick.Enabled ? Properties.Resources.ic_pause_black_24dp : Properties.Resources.ic_play_arrow_black_24dp;
        }
        private void playstopButton_Click(object sender, EventArgs e)
        {
            _tick.Enabled = !_tick.Enabled;
            updatePlayStopButton();
        }
    }
}
