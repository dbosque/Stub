using System;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls.Behaviours
{
    /// <summary>
    /// DragDrop behaviour on a control
    /// </summary>
    public class DragDropPasteBehaviour 
    {
        /// <summary>
        /// The contol to act upon
        /// </summary>
        protected readonly Control _child;

        /// <summary>
        /// The event to invoke
        /// </summary>
        private readonly EventHandler<DragDropPasteContentEventArgs> _event;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="c">The control to act upon</param>
        /// <param name="e">The event to invoke</param>
        public DragDropPasteBehaviour(Control c , EventHandler<DragDropPasteContentEventArgs> e)
        {
            _child = c;
            _child.AllowDrop = true;
            _child.DragEnter += T_DragEnter;
            _child.DragDrop += T_DragDrop;
            _child.KeyUp += T_KeyUp;
            _event = e;
        }

        /// <summary>
        /// Key up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.Handled = e.SuppressKeyPress = true;
                string content = GetDataObjectData(Clipboard.GetDataObject());
                _event?.Invoke(_child, new DragDropPasteContentEventArgs(content));
            }
        }

        /// <summary>
        /// Retrieve the data from the clipboard or the drag/drop event
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private string GetDataObjectData(IDataObject e)
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

        /// <summary>
        /// DragDrop event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_DragDrop(object sender, DragEventArgs e)
        {
            string content = GetDataObjectData(e.Data);
            _event?.Invoke(_child, new DragDropPasteContentEventArgs(content));
        }

        /// <summary>
        /// Drag enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

     
    }
}
