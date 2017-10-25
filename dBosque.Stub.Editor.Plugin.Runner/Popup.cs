using dBosque.Stub.Services.Extensions;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Plugin.Runner
{
    public partial class Popup : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public Popup(string info)
        {
            InitializeComponent();
            textBox1.Text = info;
            textBox1.Select(0, 0);
        }

        public Popup Update(string info)
        {
            textBox1.Text = info;
            return this;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox1.SelectAll();
            }
        }

        private void prettyPrintButton_Click(object sender, System.EventArgs e)
        {
            textBox1.Text = textBox1.Text.Pretty();
        }
    }
}
