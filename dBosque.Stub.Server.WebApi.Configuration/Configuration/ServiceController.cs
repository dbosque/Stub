using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Server.WebApi.Configuration.Model;
using dBosque.Stub.Services.Extensions;
using dBosque.Stub.Services.ExternalReferenceResolvers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// Controller for Stubs
    /// </summary>
    [Route("Service")]
    
    public class ServiceController : ConfigurationController
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="logger"></param>
        public ServiceController(IRepositoryFactory factory, ILogger<ServiceController> logger) 
            : base(factory, logger)
        {
        }

        /// <summary>
        /// Create a new Stub based on the specifiec Regular Expression
        /// The .NET regular expression parser is used (https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference)
        /// All named groups are extract and used as template restrictions (https://docs.microsoft.com/en-us/dotnet/standard/base-types/grouping-constructs-in-regular-expressions)
        /// </summary>
        /// <param name="regex">The regular expression.</param>
        /// <returns>The newly created stub.</returns>
        [Route("regex", Order = 10)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateForRegEx([FromBody] RegexStub regex)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetMessageTypes().FirstOrDefault(m => m.Namespace == regex.Pattern);
                if (obj == null)
                {
                    obj = _repository.UpdateMessageType(null, a =>
                    {
                        a.Description = regex.Description;
                        a.Rootnode = string.Empty;
                        a.Namespace = regex.Pattern;
                        a.PassthroughEnabled = false;
                        a.PassthroughUrl = string.Empty;
                    });

                    // Get all named groups and create rexpressions for them
                    var groups = new Regex(regex.Pattern).GetGroupNames().Where(a => !int.TryParse(a, out int dummy)).ToArray();
                    foreach (var g in groups)
                    {
                        var xpath = _repository.GetXpaths().FirstOrDefault(a => a.Expression == g && a.Type == 1);
                        if (xpath == null)
                        {
                            _repository.UpdateXpath(null, a => {
                                a.Type = 1;
                                a.Expression = g;

                            });
                        }
                    }
                    return Created(obj.AsModel(BaseUri));
                }

                return Ok(obj.AsModel(BaseUri));
            });
        }

        /// <summary>
        /// Retrieve possible stubs from the given wsdl
        /// </summary>
        /// <param name="data">The wsdl to parse from</param>
        /// <returns>Possible stubs.</returns>
        [Route("wsdl")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Wsdl([FromBody] string data)
        {
            var wsdl = new WSDLResolver().Execute(data, _repository);
            return Ok(wsdl.Select(i => i.AsModel()));
        }

        /// <summary>
        /// Retrieve possible stubs from the given Swagger doc
        /// </summary>
        /// <param name="data">The Swagger doc to parse from</param>
        /// <returns>Possible stubs.</returns>
        [Route("swagger")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Swagger([FromBody] string data)
        {
            var wsdl = new SwaggerResolver().Execute(data, _repository);
            return Ok(wsdl.Select(i => i.AsModel()));

        }

        /// <summary>
        /// Create a new Stub based on a specifc namespace/rootnode taken from the posted data.
        /// </summary>
        /// <param name="data">The data from which to extract the namespace/rootnode.</param>
        /// <returns>The newly created stub.</returns>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateFor([FromBody] string data)
        {
            return TryCatch(() =>
            {

                var dom = data.GetDocumentInfo();
                var obj = _repository.GetMessageTypes().FirstOrDefault(m => m.Namespace == dom.Namespace && m.Rootnode == dom.RootNode);
                if (obj == null)
                {
                    obj = _repository.UpdateMessageType(null, a =>
                    {
                        a.Description = DateTime.Now.ToLongTimeString();
                        a.Rootnode = dom.RootNode;
                        a.Namespace = dom.Namespace;
                        a.PassthroughEnabled = false;
                        a.PassthroughUrl = string.Empty;
                        a.Sample = data;
                    });
                    return Created(obj.AsModel(BaseUri));
                }
                return Ok(obj.AsModel(BaseUri));
            });
        }


        /// <summary>
        /// Check to see if for the given posted data a stub already exists.
        /// </summary>
        /// <param name="data">The data from which to extract the namespace/rootnode.</param>
        /// <param name="url">The url from which to extract the info</param>
        /// <returns>The found stub, or NotFound</returns>
        [Route("has", Order = -1)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult HasStubFor([FromBody] string data, string url = null)
        {
            return TryCatch(() => {

                var dom = data.CreateDocument();
                string newNamespace = dom.DocumentElement.NamespaceURI.IfEmpty(url);
                string newRootnode = dom.DocumentElement.LocalName;
                var obj = _repository.GetMessageTypes().Where(m => m.Namespace == newNamespace && m.Rootnode == newRootnode);
                if (!obj.Any())
                    return NotFound();
                return Ok(obj.Select(a => a.AsModel(BaseUri)).FirstOrDefault());
            });
        }

        /// <summary>
        /// Retrieve all stubs
        /// </summary>
        /// <returns>All available stubs</returns>
        [Route("", Name = "GetAllStubs")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPaged(int pageNo = 1, int pageSize = 50)
        {
            return TryCatchResponse(() =>
            {
                int skip = (pageNo - 1) * pageSize;
               
                var items = _repository.GetMessageTypes();
                if (!items.Any())
                    return NotFoundMessage();

                return PagedOk(items.Skip(skip).Take(pageSize).Select(a => a.AsModel(BaseUri)),
                                "GetAllStubs", pageNo, pageSize, items.Count());               
            });
        }

        /// <summary>
        /// Retrieve a specific stub
        /// </summary>
        /// <param name="id">The id from the required stub</param>
        /// <returns>The stub matching the given id, or NotFound</returns>
        [Route("{id:int}/instance")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetInstances(int id)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetMessageType(id);
                if (obj == null)
                    return NotFound();

                var combo = obj.Template.SelectMany(t => t.Combination);


                return Ok(combo.Select(t => t.AsModel(BaseUri)));
            });
        }

        /// <summary>
        /// Retrieve a specific stub
        /// </summary>
        /// <param name="id">The id from the required stub</param>
        /// <returns>The stub matching the given id, or NotFound</returns>
        [Route("{id:int}/template")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTemplates(int id)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetMessageType(id);
                if (obj == null)
                    return NotFound();

              

                return Ok(obj.Template.Select(t => t.AsModel(BaseUri)));
            });
        }

        /// <summary>
        /// Retrieve a specific stub
        /// </summary>
        /// <param name="id">The id from the required stub</param>
        /// <returns>The stub matching the given id, or NotFound</returns>
        [Route("{id:int}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetMessageType(id);
                if (obj == null)
                    return NotFound();
                return Ok(obj.AsModel(BaseUri));
            });
        }

        /// <summary>
        /// Delete a specific stub
        /// </summary>
        /// <param name="id">The id from the required stub</param>
        /// <returns>Ok if deleted, Conflict if there are still connected templates, otherwise NotFound</returns>
        [Route("{id:int}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Delete(int id)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetMessageType(id);
                if (obj == null)
                    return NotFound();
                if (_repository.GetTemplates(id).Any())
                    return new ConflictResult();
                _repository.DeleteMessageType(id);
                return Ok();
            });
        }

        /// <summary>
        /// Update the given stub
        /// </summary>
        /// <param name="id">>The id from the required stub</param>
        /// <param name="stub">The new data</param>
        /// <returns>The updated stub, or NotFound</returns>
        [Route("{id:int}", Order = -1)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch]
        public IActionResult Patch(int id, [FromBody]Model.Service stub)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetMessageType(id);
                if (obj == null)
                    return NotFound();
                _repository.UpdateMessageType(id, a =>
                {
                    a.Description = stub.Description;
                    a.Rootnode = stub.RootNode;
                    a.Namespace = stub.UriRegex ?? stub.Namespace;
                    a.PassthroughEnabled = stub.AllowPassthrough??false;
                    a.PassthroughUrl = stub.PassthroughUri;
                    a.Sample = stub.Sample.Base64Decode();
                });

                return Ok(obj.AsModel(BaseUri));
            });
        }
    }
}
