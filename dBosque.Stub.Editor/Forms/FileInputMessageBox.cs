using System;
using System.Windows.Forms;

namespace dBosque.Stub.Editor
{
    public partial class FileInputMessageBox : Form
    {
        public FileInputMessageBox()
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

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "xml files (*.xml)|*.xml|json files (*.json)|*.json|wsdl files (*.wsdl)|*.wsdl";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inputTextBox.Text = openFileDialog1.FileName;
            }
            
        }
    }
}