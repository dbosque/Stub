using dBosque.Stub.Editor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace dBosque.Stub.Editor
{

    public partial class DatabaseConnectionAddForm : Form
    {
        public DatabaseConnectionAddForm(int mode = 0)
        {
            _mode = mode;
            InitializeComponent();
            EnableDisable();
            if (_illegalChars == null)
            {
                var illegalchars = System.IO.Path.GetInvalidFileNameChars().ToList();
                illegalchars.AddRange(System.IO.Path.GetInvalidPathChars().ToList());
                illegalchars.Add(' ');
                _illegalChars = illegalchars.Select(c => c.ToString()).ToArray();
            }
           
            // Get all factories
            customProviderName.Items.AddRange(GlobalSettings.Instance.Store.Providers.Select(p => p.Name).ToArray());
            //customProviderName.Items.AddRange()
            modelnameLabel.Text = _mode == 0 ? "Model name :" : "Connection name :";
        }

        private readonly int _mode = 0;
        private static string[] _illegalChars = null;
        private void connectBt_Click(object sender, EventArgs e)
        {
            LoadDatabases();
        }

        private ConnectionString ConstructConnectionString()
        {
            ConnectionString s = new ConnectionString { Datasource = connectionStringTb.Text, Username = usernameTb.Text, Password = passwordTb.Text, };
            if (databaseCombo.Enabled)
                s.Database = databaseCombo.Text;
            return s;
        }

        private void EnableDisable()
        {
            databaseCombo.Enabled = databaseCombo.Items.Count > 0;
            modelNameTb.Enabled = modelNameTb2.Enabled = (databaseCombo.Enabled = databaseCombo.Items.Count > 0) || !string.IsNullOrEmpty(customTb.Text);
            if (tabControl1.SelectedTab.Name == "customForm")
            {
                okBt.Enabled = ((!string.IsNullOrEmpty(modelNameTb.Text)) && !KnownModelNames.Contains(modelNameTb.Text) && !HasInvalidChars(modelNameTb.Text));
            }
            else
            {
                okBt.Enabled = !string.IsNullOrEmpty(modelNameTb.Text) && !string.IsNullOrEmpty(customTb.Text) && !string.IsNullOrEmpty(customProviderName.Text);
            }
            updateBtn.Enabled = ((!string.IsNullOrEmpty(modelNameTb.Text)) && KnownModelNames.Contains(modelNameTb.Text) && !HasInvalidChars(modelNameTb.Text));
        }

        public string Provider
        {
            get;
            set;
        }

        public string ConnectionString
        {
            get;
            set;
        }

        public string ConnectionName
        {
            get;
            set;
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "customForm")
            {
                ConnectionString = customTb.Text;
                ConnectionName = modelNameTb.Text;
                Provider = customProviderName.Text;
            }
            else
            {
                ConnectionString = ConstructConnectionString().ToString();
                ConnectionName = modelNameTb2.Text;
                Provider = "SQLServer";
            }
        }

        private void LoadDatabases()
        {
            databaseCombo.Items.Clear();
            EnableDisable();
            try
            {
                databaseCombo.Items.AddRange(ConstructConnectionString().GetDatabases().ToArray());
                if (databaseCombo.Items.Count == 0)
                {
                    // Add a dummy to make sure it;s enabled
                    databaseCombo.Items.Add(string.Empty);
                    MessageBox.Show(@"No databases found, or no rights to access them.
Enter the databasename if known and press enter to continue.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            EnableDisable();
        }

        public List<string> KnownModelNames
        {
            private get;
            set;
        }

        private bool HasInvalidChars(string value)
        {
            return _illegalChars.Any(value.Contains);
        }

        private void modelNameTb_TextChanged(object sender, EventArgs e)
        {
            EnableDisable();
            if (HasInvalidChars(modelNameTb.Text))
            {
                modelnameLabel.ForeColor = Color.Red;
                messageLabel.Text = @"Invalid characters : " + string.Join(",", _illegalChars.Select(a => "' " + a + " '").ToArray());
            }
            else if (KnownModelNames.Contains(modelNameTb.Text))
            {
                modelnameLabel.ForeColor = Color.Red;
                messageLabel.Text = @"Model name already in use.";
            }
            else
            {
                modelnameLabel.ForeColor = SystemColors.ControlText;
                messageLabel.Text = string.Empty;
            }

            if (HasInvalidChars(modelNameTb2.Text))
            {
                label7.ForeColor = Color.Red;
                messageLabel2.Text = @"Invalid characters : " + string.Join(",", _illegalChars.Select(a => "' " + a + " '").ToArray());
            }
            else if (KnownModelNames.Contains(modelNameTb2.Text))
            {
                label7.ForeColor = Color.Red;
                messageLabel2.Text = @"Model name already in use.";
            }
            else
            {
                label7.ForeColor = SystemColors.ControlText;
                messageLabel2.Text = string.Empty;
            }
        }

        private void customTb_TextChanged(object sender, EventArgs e)
        {
            EnableDisable();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisable();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.EndsWith("db"))
                {
                    customTb.Text = $"Data Source='{openFileDialog1.FileName}'";
                    if (customProviderName.Items.Contains("SQLite"))
                        customProviderName.SelectedText = "SQLite";
                }
                else
                    customTb.Text = openFileDialog1.FileName;
            }
        }
    }
    
}