using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Server.WebApi.Configuration.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// The Trace controller
    /// </summary>
    [Route("Trace")]
    public class TraceController : ConfigurationController
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="logger"></param>
        public TraceController(IRepositoryFactory factory, ILogger<TraceController> logger)
            : base(factory, logger)
        {
        }

        ///// <summary>
        ///// Retrieve all stubs
        ///// </summary>
        ///// <returns>All available stubs</returns>
        //[Route("", Name = "GetAllTraces")]
        //[HttpGet]
        //[StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        //[ResponseType(typeof(IEnumerable<Trace>))]
        //public HttpResponseMessage Get(int pageNo = 1, int pageSize = 50)
        //{
        //    return TryCatchResponse(() =>
        //    {
        //        int skip = (pageNo - 1) * pageSize;

        //        var items = _repository.GetLogs();
        //        if (!items.Any())
        //            return NotFoundMessage();

        //        return PagedOk(items.Skip(skip).Take(pageSize).Select(a => a.AsModel(BaseUri)), 
        //            new PagedResult(pageNo, pageSize, items.Count(), "GetAllTraces"));
        //    });
        //}

        /// <summary>
        /// Retrieve trace information
        /// </summary>
        /// <returns>All available stubs</returns>
        [Route("", Name = "GetAllTraces")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [ResponseType(typeof(IEnumerable<Trace>))]
        public IActionResult Get(int pageNo = 1, int pageSize = 50, DateTime? from = null, DateTime? to = null, string filter = "", int? id = null )
        {
            return TryCatchResponse(() =>
            {
                int skip = (pageNo - 1) * pageSize;

                from = from ?? DateTime.MinValue;
                to = to ?? DateTime.MaxValue;
                var items = _repository.GetLogs(l => l.ResponseDatumTijd >= from  &&
                                        l.ResponseDatumTijd <= to &&
                                        (string.IsNullOrEmpty(filter)?true:l.Request.Contains(filter)) &&
                                        (id.HasValue ? (l.StubLogId == id) : true));
                if (!items.Any())
                    return NotFoundMessage();

                return PagedOk(items.Skip(skip).Take(pageSize).Select(a => a.AsModel(BaseUri)),
                                new PagedResult(pageNo, pageSize, items.Count(), "GetAllTraces", 
                                new KeyValuePair<string, object>("from", from ),
                                new KeyValuePair<string, object>("to", to),
                                new KeyValuePair<string, object>("filter", filter),
                                new KeyValuePair<string, object>("id", id)));
            });
        }
    }
}
