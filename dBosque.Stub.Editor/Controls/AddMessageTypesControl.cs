using dBosque.Stub.Editor.Controls.Models;
using dBosque.Stub.Editor.Flow;
using dBosque.Stub.Editor.UserInteraction;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Services.ExternalReferenceResolvers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls
{
    public partial class AddMessageTypesControl : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public AddMessageTypesControl(string content, IStubDataRepository repository, ExternalResolverBase resolver = null)
        {
            InitializeComponent();
            _uri = content;
            _repository = repository;
            _resolver = resolver??new WSDLResolver();
        }

        public event FlowHandler<FlowEventArgs> OnSave;

        private ExternalResolverBase _resolver { get; set; }
      
        private string _uri { get; set; }

        private IStubDataRepository _repository { get; set; }

        private void LoadData()
        {
            var info = _resolver.Execute(_uri, _repository);
            if (info == null)
                return;

            var data = info.Select(c => new WsdlMessageInfo()
            {
                Checked = c.Id != null,
                Enabled = c.PassthroughEnabled ?? false,
                Namespace = c.Namespace,
                Rootnode = c.RootNode,
                Sample = c.Request,
                Url = c.Uri,
                IsInDatabase = c.Id != null,
                Description = c.Description
            });

            wsdlMessageInfoBindingSource.DataSource = data.ToList();

            foreach (DataGridViewRow d in dataGridView1.Rows)
                d.ReadOnly = d.Frozen = (d.DataBoundItem as WsdlMessageInfo)?.IsInDatabase ?? false;
        }
        private void AddMessageTypesForm_Load(object sender, EventArgs e)
        {
            LoadData();   
        }

    

        private void selectallButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow d in dataGridView1.Rows)
                (d.DataBoundItem as WsdlMessageInfo).Checked = !d.ReadOnly;
        }

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow d in dataGridView1.Rows)
                (d.DataBoundItem as WsdlMessageInfo).Checked = d.ReadOnly;
        }

        private void createAllButton_Click(object sender, EventArgs e)
        {
            var data = (wsdlMessageInfoBindingSource.DataSource as List<WsdlMessageInfo>)
                            .Where(a => a.Checked && !a.IsInDatabase);
            if (!data.Any())
            {
                UserInteractor.NoDataChanged.Warning();
                return;
            }
            foreach (var d in data)
            {
                _repository.UpdateMessageType(null, a => {
                    a.Description           = d.Description;
                    a.Namespace             = d.Namespace;
                    a.Rootnode              = d.Rootnode;
                    a.PassthroughUrl        = d.Url;
                    a.PassthroughEnabled    = d.Enabled;    
                });

            }
            _repository.Flush();
            OnSave.Invoke(this, new FlowEventArgs());

            dataGridView1.Enabled = createAllButton.Enabled = selectallButton.Enabled = deselectAllButton.Enabled = false;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].ReadOnly)
                e.Cancel = true;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadData();
            dataGridView1.Enabled = createAllButton.Enabled = selectallButton.Enabled = deselectAllButton.Enabled = true;
        }
    }
}
