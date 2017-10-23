using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure.DependencyResolution;

namespace dBosque.Stub.EF.Interceptor
{
    public class StubConfigurationInterceptor : IDisposable, IDbConfigurationInterceptor
    {
        /// <summary>
        /// Defaults
        /// </summary>
        private readonly string _server = "127.0.0.1";
        private readonly int _port = 2201;
        private readonly bool _passthroughOnNotFound = true;
        private StubInterceptor _interceptor;

        /// <summary>
        /// Default constructor
        /// </summary>
        public StubConfigurationInterceptor()
        { }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="server">The stub server </param>
        /// <param name="port">The stub server port</param>
        public StubConfigurationInterceptor(string server, int port)
        {
            _server = server;
            _port = port;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="server">The stub server </param>
        /// <param name="port">The stub server port</param>
        /// <param name="passthroughOnNotFound">Passthrough on notfound</param>
        public StubConfigurationInterceptor(string server, int port, bool passthroughOnNotFound)
        {
            _server = server;
            _port = port;
            _passthroughOnNotFound = passthroughOnNotFound;
        }

        /// <summary>
        /// Stops logging and closes the underlying file if output is being written to a file.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Stops logging and closes the underlying file if output is being written to a file.
        /// </summary>
        /// <param name="disposing">
        /// True to release both managed and unmanaged resources; False to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            DbInterception.Remove(_interceptor);
            _interceptor = null;           
        }

        ///<summary>
        ///Occurs during EF initialization after the <see cref="T:System.Data.Entity.DbConfiguration" /> has been constructed but just before
        ///it is locked ready for use. Use this event to inspect and/or override services that have been
        ///registered before the configuration is locked. Note that an interceptor of this type should be used carefully
        ///since it may prevent tooling from discovering the same configuration that is used at runtime.
        ///</summary>
        ///<remarks>
        ///Handlers can only be added before EF starts to use the configuration and so handlers should
        ///generally be added as part of application initialization. Do not access the DbConfiguration
        ///static methods inside the handler; instead use the the members of <see cref="T:System.Data.Entity.Infrastructure.DependencyResolution.DbConfigurationLoadedEventArgs" />
        ///to get current services and/or add overrides.
        ///</remarks>
        ///<param name="loadedEventArgs">Arguments to the event that this interceptor mirrors.</param>
        ///<param name="interceptionContext">Contextual information about the event.</param>
        public void Loaded(DbConfigurationLoadedEventArgs loadedEventArgs, DbConfigurationInterceptionContext interceptionContext)
        {
            if (_interceptor == null)
            {
                _interceptor = new StubInterceptor(_server, _port, _passthroughOnNotFound);
                DbInterception.Add(_interceptor);
            }
        }
    }
}
