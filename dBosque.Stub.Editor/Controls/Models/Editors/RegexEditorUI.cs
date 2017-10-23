using dBosque.Stub.Editor.Controls.Extensions;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace dBosque.Stub.Editor.Controls.Models.Editors
{
    public partial class RegexEditorUI : Control
    {
        public RegexEditorUI()
        {
            InitializeComponent();
        }

        private string value;
        private string regEx;
        private IWindowsFormsEditorService edSvc;

        public object Value
        {
            get
            {
                return value;
            }
        }

        public void Start(IWindowsFormsEditorService edSvc, object value, string regEx)
        {
            UpdateControls(false, string.Empty);
            this.edSvc = edSvc;
            this.regEx = regEx;
            this.value = (string)value;
            if (value != null)
                Convert();
        }

        public void End()
        {
            edSvc = null;
            value = null;
        }

        public void UpdateControls(bool isvalid, string message)
        {
            label1.Text = message;
            label1.Visible = !isvalid;
            listView1.Visible = isvalid;
        }

        private void Convert()
        {
            try
            {
                listView1.Items.Clear();
                var regex = new Regex($"^{regEx}$", RegexOptions.IgnoreCase);
                var isValid = regex.Match(value);
                if (regex.GetNamedGroups().Length == 0)
                    UpdateControls(false, "No named groups where found in the regular expression." + Environment.NewLine + "E.g. : abc(?< name >.*)");
                else if (isValid.Success && isValid.Groups.Count > 1)
                {
                    StringBuilder builder = new StringBuilder();
                    for (int i = 1; i < isValid.Groups.Count; i++)
                    {
                        var it = new ListViewItem(regex.GroupNameFromNumber(i));
                        it.SubItems.AddRange(new string[] { isValid.Groups[i].Value });
                        listView1.Items.Add(it);

                        builder.AppendFormat("{0}:{1}{2}", regex.GroupNameFromNumber(i), isValid.Groups[i].Value, Environment.NewLine);
                    }
                    UpdateControls(true, string.Empty);
                }
                else
                {
                    UpdateControls(false, "No matches where found.");
                }
            }
            catch (Exception ex)
            {
                UpdateControls(false, $"Invalid regex : {ex.Message}");
            }
        }
    }
}