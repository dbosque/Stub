using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using dBosque.Stub.Editor.Models;
using dBosque.Stub.Editor.UserInteraction;
using dBosque.Stub.Editor.Controls.Extensions;

namespace dBosque.Stub.Editor
{
    public partial class AddEditMessageTypeForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public AddEditMessageTypeForm()
        {
            InitializeComponent();

        }

        private MessageTypeItem _message;
        public MessageTypeItem Message
        {
            get {
                _message.AllowPassthrough = passthroughEnabled.Checked;
                _message.Description = descriptionTb.Text;
                _message.NameSpace = namespaceTb.Text;
                _message.PassthroughUri = tbPassthrougUri.Text;
                _message.Sample = sampleTb.Text;
                _message.RootNode = rootnodeTb.Text;
                return _message;
            }
            set {
                _message = value;
                namespaceTb.Text = value.NameSpace;
                rootnodeTb.Text = value.RootNode;
                descriptionTb.Text = value.Description;
                passthroughEnabled.Checked = value.AllowPassthrough;
                tbPassthrougUri.Text = value.PassthroughUri;
                sampleTb.Text = value.Sample;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {

                if (passthroughEnabled.Checked)
                {
                  tbPassthrougUri.Text =  new UriBuilder(tbPassthrougUri.Text).Uri.ToString();
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception)
            {
                UserInteractor.InvalidUri.Error(tbPassthrougUri.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var regex = new Regex($"^{namespaceTb.Text}$");
                var isValid = regex.Match(regexTb.Text);
                if (regex.GetNamedGroups().Length == 0)
                {
                    UserInteractor.RegexNoNamedGroups.Error();
                }
                else if (isValid.Success && isValid.Groups.Count > 1)
                {
                    StringBuilder builder = new StringBuilder();
                    for (int i = 1; i < isValid.Groups.Count; i++)
                        builder.AppendFormat("{0}:{1}{2}", regex.GroupNameFromNumber(i), isValid.Groups[i].Value, Environment.NewLine);

                    UserInteractor.RegexMatch.Info(builder.ToString());

                }
                else
                {
                    UserInteractor.RegexNoMatch.Error();
                }
            }
            catch (Exception ex)
            {
                UserInteractor.RegexError.Error(ex.Message);
            }
        }
    }
}