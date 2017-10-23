
namespace dBosque.Stub.Editor.Flow
{
    /// <summary>
    /// Eventargs to create a messagetype of template
    /// </summary>
    public class CreateMessageTypeFlowEventArgs : FlowEventArgs
    {
        /// <summary>
        /// The XML/JSON content
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// The namespace of the content
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// The rootnode of the content
        /// </summary>
        public string Rootnode { get; set; }
    }
}
