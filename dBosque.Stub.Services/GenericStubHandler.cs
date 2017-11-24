using dBosque.Stub.Interfaces;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Services.Logging;
using dBosque.Stub.Services.XSLT;
using Microsoft.Extensions.Logging;
using System;

namespace dBosque.Stub.Services
{

    /// <summary>
    /// Generic handler to process a message
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class GenericStubHandler<TResult> : IStubHandler<TResult> 
    {
        private IRepositoryFactory _factory;
        private ILogger _logger;
        private XSLTPostProcessor _xsltProcessor = new XSLTPostProcessor();

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="logger"></param>
        public GenericStubHandler(IRepositoryFactory factory, ILogger<GenericStubHandler<TResult>> logger)
        {
            _factory = factory;
            _logger = logger;         
        }

        /// <summary>
        /// Get the datarepository for a specific message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private IStubDataRepository GetConfigurationFor(IStubMessage<TResult> message)
        {
            IStubDataRepository data = null;
            try
            {
                IConfigurationRepository config = null;
                if (Guid.TryParse(message.Tenant, out Guid tenantSecuritycode))
                    config = _factory.CreateConfigurationRepositoryFor(tenantSecuritycode);
                else if (message.Tenant == Constants.DefaultTenant)
                {
                    config = _factory.CreateConfiguration();
                    _logger.LogInformation($"Configuration created for default.");
                }
                message.TenantId = config?.Id;              
                // Create data repository
                data = _factory.CreateDataRepository(config);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return data;
        }

        /// <summary>
        /// Handle the message in case there was a failure
        /// </summary>
        /// <param name="message"></param>
        /// <param name="watch"></param>
        private void HandleFailure(IStubMessage<TResult> message, Timer watch)
        {
            message.HttpStatusCode = 404;
            if (message.HasMultipleMatches)
                message.HttpStatusCode = 409;

            watch.Stop();
            _logger.LogWarning("{@Id}", message.Failure(watch.ElapsedMilliseconds));
        }

        /// <summary>
        /// Handle the message in case it was successfull
        /// </summary>
        /// <param name="message"></param>
        /// <param name="watch"></param>
        private void HandleSuccess(IStubMessage<TResult> message, Timer watch)
        {
            if (message.IsPassTrough)
                message.Relay(message.PassthroughUri);

            watch.Stop();
            _logger.LogInformation("{@Id}", message.Success(watch.ElapsedMilliseconds));
            _logger.LogTrace(message.Response);
        }        

        ///<summary>
        ///Handle a message that has been marked as passthrough
        ///</summary>
        ///<param name="message">The message to handle</param>
        ///<param name="passthroughUrl">The passthroughUri</param>
        ///<returns>The found result</returns>
        public TResult HandlePassthrough(IStubMessage<TResult> message,  string passthroughUrl)
        {
            using (var watch = new Timer())
            {
                message.IsPassTrough = true;
                message.PassthroughUri = passthroughUrl;

                var data = GetConfigurationFor(message);
                if (data == null)
                    return message.AsUnauthorized();

                _logger.LogInformation("[{@Id}]", message.Info());
                _logger.LogTrace(message.RawRequest);

                HandleSuccess(message, watch);

                data.Log(null, message.Request, message.TenantId, message.Uri, null);
                return message.AsResult();
            }
        }

        ///<summary>
        ///Handle a message
        ///</summary>
        ///<param name="message">The message to nahdle </param>
        ///<returns>The mocked result</returns>      
        public TResult HandleMessage(IStubMessage<TResult> message)
        {
            using (var watch = new Timer())
            {
                var data = GetConfigurationFor(message);
                if (data == null)
                    return message.AsUnauthorized();

                _logger.LogInformation("[{@Id}]", message.Info());
                _logger.LogTrace(message.RawRequest);

                // Find a stub
                data.GetStubForMessage(message);

                // Perform any postprocessing
                if (message.HasMatch && message.hasPostProcessing)
                    _xsltProcessor.Execute(message);

                // Handle the result
                if (!message.HasMatch && !message.IsPassTrough)
                    HandleFailure(message, watch);
                else
                    HandleSuccess(message, watch);

                return message.AsResult();
            }
        }
    }
}
