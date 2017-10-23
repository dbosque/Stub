
namespace dBosque.Stub.Editor.Controls.Models
{
    /// <summary>
    /// Model to visualize an action taken from a wsdl.
    /// </summary>
    public class WsdlMessageInfo
    {
        /// <summary>
        /// Did the user check this item
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// The namespace of the message
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// The rootnode of the message
        /// </summary>
        public string Rootnode { get; set; }

        /// <summary>
        /// The base url of the service
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Is the action enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// A sample request
        /// </summary>
        public string Sample { get; set; }

        /// <summary>
        /// Is the action already in the database?
        /// </summary>
        public bool IsInDatabase { get; set; }

        /// <summary>
        /// A humanreadable description to be filled in by the user.
        /// </summary>
        public string Description { get; set; }
    }
}
