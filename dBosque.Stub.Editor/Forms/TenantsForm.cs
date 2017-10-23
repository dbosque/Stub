using dBosque.Stub.Repository.ConfigurationDb.Entities;
using dBosque.Stub.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace dBosque.Stub.Editor
{
    public partial class TenantsForm : Form
    {
        private Timer _timer = new Timer() { Interval = 3000 };
        public TenantsForm()
        {
            InitializeComponent();
        }

        public IStubConfigurationRepository Repository { get; set; }

        private IConfigurationRepository Config => Repository as IConfigurationRepository;

        private void tenantCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tenant = (tenantCombo.SelectedItem as Tenant);
            tenantNameTb.Text = tenant.Name;
            tenantActivechbx.Checked = tenant.Active;
            connectionTb.Text = tenant.Connectionstring;
            
            securityCodeCombo.DataSource = Repository.GetAllSecurityCodes(tenant.TenantId).OrderByDescending( t=> t.Active).ToList();

            addBtn.Enabled = tenantNameTb.Text != tenant.Name;

            datagridSettings.DataSource = Repository.GetAllSettings(tenant.TenantId).OrderBy(a => a.Name).ToList();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Repository.UpdateTenant(null, t => { t.Active = tenantActivechbx.Checked; t.Name = tenantNameTb.Text; t.Connectionstring = connectionTb.Text; });
            tenantCombo.DataSource = Repository.GetAllTenants().ToList();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            var tenant = (tenantCombo.SelectedItem as Tenant);
            Repository.DeleteTenant(tenant.TenantId);
            tenantCombo.DataSource = Repository.GetAllTenants().ToList();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void newCodeBtn_Click(object sender, EventArgs e)
        {
            var tenant = (tenantCombo.SelectedItem as Tenant);
            Repository.AddNewSecurityCode(tenant.TenantId);
            securityCodeCombo.DataSource = Repository.GetAllSecurityCodes(tenant.TenantId).OrderByDescending(t => t.Active).ToList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, securityCodeCombo.SelectedText);
            toolTip1.SetToolTip(pictureBox1, "Copied to clipboard.");
            _timer.Tick += _timer_Tick;
            _timer.Enabled = true;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "");
            _timer.Enabled = false;
            _timer.Tick -= _timer_Tick;
        }

        private void TenantsForm_Load(object sender, EventArgs e)
        {
            tenantCombo.DisplayMember = "Name";
            tenantCombo.ValueMember = "TenantId";
            securityCodeCombo.DisplayMember = "SecurityCode";
            securityCodeCombo.ValueMember = "TenantSecurityId";
            tenantCombo.DataSource = Repository.GetAllTenants().ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Repository.ActivateSecurtyCode((int?)securityCodeCombo.SelectedValue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Save
            var tenant = (tenantCombo.SelectedItem as Tenant);
            Repository.UpdateTenant(tenant.TenantId, x => { x.Name = tenantNameTb.Text; x.Connectionstring = connectionTb.Text; x.Active = tenantActivechbx.Checked; });
        }

        private void tenantNameTb_TextChanged(object sender, EventArgs e)
        {
            var tenant = (tenantCombo.SelectedItem as Tenant);
            addBtn.Enabled = tenantNameTb.Text != tenant.Name;
        }

        private void applySettingsBtn_Click(object sender, EventArgs e)
        {
            foreach (var i in datagridSettings.DataSource as List<Stub.Repository.ConfigurationDb.Entities.Settings>)
            {
                Repository.UpdateSetting(i.Id, x => x.Value = i.Value);
            }
        }
    }
}
