using dBosque.Stub.Services.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dBosque.Stub.Server.WebApi.Configuration.Configuration
{
    /// <summary>
    /// Helper methods to populate different lists
    /// </summary>
    [Route("List")]
    [ApiExplorerSettings]
    public class ListController : Controller
    {

        /// <summary>
        /// Return a list of allowed contenttypes
        /// </summary>
        /// <returns></returns>
        [Route("contenttype")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContentType()
        {
            return Ok(ContentTypes.AsArray());
        }

        /// <summary>
        /// Return a list of possible statuscodes
        /// </summary>
        /// <returns></returns>
        [Route("statuscode")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetStatusCode()
        {
            return Ok(Enum.GetValues(typeof(System.Net.HttpStatusCode)).Cast<object>().Distinct().Select(
                s => new KeyValuePair<string, long?>(string.Format("{0} ({1})", s.ToString(), (int)s), (int)s)));
        }
    }
 }
