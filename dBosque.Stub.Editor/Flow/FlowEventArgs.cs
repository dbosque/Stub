using System;

namespace dBosque.Stub.Editor.Flow
{
    public delegate void FlowHandler<T>(object sender, T e) where T : FlowEventArgs;

    /// <summary>
    /// Default flowevent args
    /// </summary>
    public class FlowEventArgs : EventArgs
    {
        public long? Id { get; set; }

        /// <summary>
        /// An empty flowevents
        /// </summary>
        public new static FlowEventArgs Empty => new FlowEventArgs();
    }

  

}
