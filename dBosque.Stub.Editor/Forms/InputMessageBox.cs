using System;
using System.Windows.Forms;

namespace dBosque.Stub.Editor
{
    public partial class InputMessageBox : Form
    {
        public InputMessageBox()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            inputTextBox.Focus();
            inputTextBox.SelectAll();
        }

        public string Label
        {
            get
            {
                return InputLabel.Text;
            }

            set
            {
                InputLabel.Text = value;
            }
        }

        public string Inputstring
        {
            get
            {
                return inputTextBox.Text;
            }

            set
            {
                inputTextBox.Text = value;
            }
        }

        public string Caption
        {
            get
            {
                return this.Text;
            }

            set
            {
                this.Text = value;
            }
        }

        private void Okbutton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InputMessageBox_Load(object sender, EventArgs e)
        {
            splitContainer1.Focus();
            splitContainer1.Panel1.Focus();
            inputTextBox.Focus();
            inputTextBox.SelectAll();
        }
    }
}