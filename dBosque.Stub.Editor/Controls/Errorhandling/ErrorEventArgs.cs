using System;
namespace dBosque.Stub.Editor.Controls.Errorhandling
{
    /// <summary>
    /// Default errorhandling eventargs
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ErrorEventArgs()
        { }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ex"></param>
        public ErrorEventArgs(Exception ex)
            :this(ex.ToString(), "Error", true)
        { }

        /// <summary>
        /// Default constructor with params
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <param name="fatal"></param>
        public ErrorEventArgs(string message, string caption, bool fatal = false)
        {
            Message = message;
            Caption = caption;
            Fatal = fatal;
        }
        public string Message { get; set; }
        public string Caption { get; set; }

        public bool Fatal { get; set; }
    }
    
}
