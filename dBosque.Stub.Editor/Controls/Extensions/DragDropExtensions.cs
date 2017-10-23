using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls.Extensions
{
    /// <summary>
    /// Drag/Drop extensions
    /// </summary>
    public static class DragDropExtensions
    {

        /// <summary>
        /// Get the dropped data 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetFileDropData(this IDataObject e)
        {
            // Only allow files
            if (e.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                    return new System.IO.StreamReader(files[0]).ReadToEnd();
            }

            if (e.GetDataPresent(DataFormats.Text))
                return (string)e.GetData(DataFormats.Text);
            return string.Empty;
        }
    }
}