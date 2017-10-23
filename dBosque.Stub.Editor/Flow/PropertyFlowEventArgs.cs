using dBosque.Stub.Editor.Controls.Interfaces;

namespace dBosque.Stub.Editor.Flow
{
    /// <summary>
    /// Eventargs to pass a propertyprovider
    /// </summary>
    public class PropertyFlowEventArgs : FlowEventArgs
    {
        private PropertyFlowEventArgs()
        { }

        public PropertyFlowEventArgs(IPropertyProvider provider)
        {
            Provider = provider;
        }
        /// <summary>
        /// The actual provider
        /// </summary>
        public IPropertyProvider Provider { get; set; }

        /// <summary>
        /// An empty flowevents
        /// </summary>
        public new static PropertyFlowEventArgs Empty => new PropertyFlowEventArgs();

        public bool IsEmpty() => Provider == null && Id == null;
    }
}
