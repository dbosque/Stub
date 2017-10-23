
namespace dBosque.Stub.Editor.Interfaces
{
    /// <summary>
    /// Configuration object to be passed to a plugin
    /// </summary>
    public class RuntimePluginConfiguration
    {
        /// <summary>
        /// The connection we are running against
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// The provider we are running with.
        /// </summary>
        public string Provider { get; set; }
    }
}
