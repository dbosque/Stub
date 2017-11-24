namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// Configuration
    /// </summary>
    public class Hosting
    {
        /// <summary>
        /// Default Uri where the configuration api is hosted
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Hosting"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; }
    }
}
