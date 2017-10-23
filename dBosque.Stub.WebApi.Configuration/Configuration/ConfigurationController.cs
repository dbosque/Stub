using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.WebApi.Configuration.Extensions;
using dBosque.Stub.WebApi.Configuration.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace dBosque.Stub.WebApi.Configuration
{


    /// <summary>
    /// Base configurationcontroller
    /// </summary>
    public abstract class ConfigurationController : Controller
    {        
        /// <summary>
        /// Repository connection
        /// </summary>
        protected readonly IStubDataRepository _repository;

        /// <summary>
        /// Configuration repository connection
        /// </summary>
        protected readonly IConfigurationRepository _configRespository;
        /// <summary>
        /// Url the controller is hosted on.
        /// </summary>
        protected string BaseUri { get; private set; }


        private readonly ILogger _logger;

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected ConfigurationController(IRepositoryFactory factory, ILogger logger)
        {
            _repository = factory.CreateDataRepository();
            _configRespository = factory.CreateConfiguration();
            _logger = logger;
        }

        /// <summary>
        /// Override for Created result, extracting the url
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        protected internal CreatedResult Created<T>(T content)  where T: Linkable
        {
           return base.Created(content.Link, content);
        }

        /// <summary>
        /// Return a paged result, with links to the other pages
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">The content to return</param>
        /// <param name="action">The action this content was retrieved from</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecordCount"></param>
        /// <returns></returns>
        protected internal IActionResult PagedOk<T>(T content, string action, int pageNo, int pageSize, long totalRecordCount)
        {
            return new PagedActionResult(content).
                           AddLinkHeader(Url, action, null, pageNo, pageSize, totalRecordCount);

        }

        /// <summary>
        /// Return a paged result, with links to the other pages
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">The content to return</param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected internal IActionResult PagedOk<T>(T content, PagedResult result)
        {
            return new PagedActionResult(content).
                           AddLinkHeader(Url, result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected internal IActionResult NotFoundMessage()
        {
            return new NotFoundResult();
        }
        /// <summary>
        /// Catch all exceptions
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        protected IActionResult TryCatchResponse(Func<IActionResult> func)
        {
            try
            {
                if (Request != null && Request.Path != null)
                    BaseUri = Request.PathBase;
                return func();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Catch all exceptions
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        protected IActionResult TryCatch(Func<IActionResult> func)
        {
            try
            {
                if (Request != null && Request.Path != null)
                    BaseUri = Request.PathBase;
                return func();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}