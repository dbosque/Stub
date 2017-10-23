using dBosque.Stub.Editor.Models;

namespace dBosque.Stub.Editor.Flow
{
    /// <summary>
    /// Eventargs to pass a messagetypeitem
    /// </summary>
    public class MessageTypeFlowEventArgs : FlowEventArgs
    {
        public MessageTypeItem Item { get; set; }        
    }
}
