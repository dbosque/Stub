namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// Hosting configuration
    /// </summary>
    public class Hosting
    {
        /// <summary>
        /// The uri where to host
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
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
