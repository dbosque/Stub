using dBosque.Stub.Repository.Interfaces;

namespace dBosque.Stub.Editor.Flow
{
    /// <summary>
    /// Eventargs to pass a Repository
    /// </summary>
    public class RepositoryFlowEventArgs : FlowEventArgs
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="item"></param>
        public RepositoryFlowEventArgs(IStubDataRepository item)
        {
            Item = item;
        }
        public IStubDataRepository Item { get; set; }
        
    }
}
