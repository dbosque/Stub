using dBosque.Stub.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace dBosque.Stub.Editor
{
    public partial class ConnectionStringSelection : Form
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="config"></param>
        public ConnectionStringSelection(IConfigurationRepository config)
        {
            InitializeComponent();
            _config = config;
            Setup(null, _config);
        }

        private IConfigurationRepository _config;
        public ConnectionStringSelection Setup(string selected, IConfigurationRepository config)
        {
            _config = config;
            Connections = new List<ConnectionStringSetting>();
            Selected = selected;
            StartPosition = FormStartPosition.CenterParent;

            foreach (var a in config.GetAllConnectionStringKeys())
                Connections.Add(config.GetConnection(a));

            return this;
        }

        public string Selected { get; set; }
        public List<ConnectionStringSetting> Connections { get; set; }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            Selected = (string)connectionlistbx.SelectedValue;
            DialogResult = DialogResult.OK;


            Connections.ToList().ForEach(v => _config.SetConnection(v));

            this.Close();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {

            var f = new DatabaseConnectionAddForm
            {
                KnownModelNames = Connections.Select(a => a.Name).ToList()
            };
            if (f.ShowDialog() == DialogResult.OK)
            {
                Connections.Add(new ConnectionStringSetting(f.ConnectionName, f.ConnectionString, f.Provider));
                var items = Connections.Select(a => new ComboItem(a)).ToList();
                connectionlistbx.DataSource = items;                
                connectionlistbx.Refresh();
                connectionlistbx.SelectedValue = f.ConnectionName;
                deleteButton.Enabled = items.Any();
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            Connections.Remove(Connections.FirstOrDefault(a => a.Name == (string)connectionlistbx.SelectedValue));
            var items = Connections.Select(a => new ComboItem(a)).ToList();
            connectionlistbx.DataSource = items;
            connectionlistbx.Refresh();
            deleteButton.Enabled = items.Any();
        }

        private void ConnectionStringSelection_Load(object sender, EventArgs e)
        {
            connectionlistbx.DisplayMember = "Text";
            connectionlistbx.ValueMember = "Value";
            var items = Connections.Select(a => new ComboItem(a)).ToList();
            connectionlistbx.DataSource = items;
            if (!string.IsNullOrEmpty(Selected))
                connectionlistbx.SelectedValue = Selected;

            deleteButton.Enabled = items.Any();

        }

        public class ComboItem
        {
            public ComboItem(ConnectionStringSetting con)
            {
                Text = $"{con.Name} | {con.ConnectionString}";
                Value = con.Name;
            }
            public string Text { get; set; }
            public string Value { get; set; }
        }
    }
}
