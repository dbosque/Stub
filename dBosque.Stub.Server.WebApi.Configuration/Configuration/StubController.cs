using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Services.Extensions;
using dBosque.Stub.Services.ExternalReferenceResolvers;
using dBosque.Stub.Server.WebApi.Configuration.Extensions;
using dBosque.Stub.Server.WebApi.Configuration.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// Controller for Stubs
    /// </summary>
    [Route("Stub")]
    
    public class StubController : ConfigurationController
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="logger"></param>
        public StubController(IRepositoryFactory factory, ILogger<StubController> logger) 
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
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.Created)]
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
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
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
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
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
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.Created)]
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
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
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
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
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
        [Route("{id:int}")]
        [HttpGet]
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
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
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
        [StatusCodeSwaggerResponse(HttpStatusCode.Conflict)]
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
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
        [HttpPatch]
        public IActionResult Patch(int id, [FromBody]Model.Stub stub)
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
