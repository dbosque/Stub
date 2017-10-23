using System;

namespace dBosque.Stub.Editor.Controls.Behaviours
{
    /// <summary>
    /// Event arguments for a drag/drop/paste event
    /// </summary>
    public class DragDropPasteContentEventArgs : EventArgs
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="data"></param>
        public DragDropPasteContentEventArgs(string data)
        {
            Data = data;
        }

        /// <summary>
        /// The data being posted.
        /// </summary>
        public string Data { get; set; }
    }

}
