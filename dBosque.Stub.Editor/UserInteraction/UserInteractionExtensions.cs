using System.Windows.Forms;

namespace dBosque.Stub.Editor.UserInteraction
{
    internal static class UserInteractionExtensions
    {
        public static void Warning(this UserInteractor message, params object[] items)
        {
            MessageBox.Show(string.Format(message.Description, items), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(this UserInteractor message, params object[] items)
        {
            MessageBox.Show(string.Format(message.Description, items), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void Info(this UserInteractor message, params object[] items)
        {
            MessageBox.Show(string.Format(message.Description, items), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool Allowed(this UserInteractor message, params object[] items)
        {
            return MessageBox.Show(string.Format(message.Description, items), "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK;
        }

        public static bool Ask(this UserInteractor message, params object[] items)
        {
            return MessageBox.Show(string.Format(message.Description, items), "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK;
        }
        public static bool AskInput(this InputMessageBox box, out string message, string def = "")
        {
            message = string.Empty;
            box.Inputstring = def;
            if (box.ShowDialog() == DialogResult.OK)
            {
                message = box.Inputstring;
                return true;
            }
            return false;
        }
        public static bool AskInput(this FileInputMessageBox box, out string message, string def = "")
        {
            message = string.Empty;
            box.Inputstring = def;
            if (box.ShowDialog() == DialogResult.OK)
            {
                message = box.Inputstring;
                return true;
            }
            return false;
        }
    }
}
