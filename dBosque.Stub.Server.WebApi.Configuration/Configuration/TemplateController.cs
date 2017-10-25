using dBosque.Stub.Services.Extensions;
using dBosque.Stub.Server.WebApi.Configuration.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using dBosque.Stub.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// Template controller
    /// </summary>
    [Route("Template")]
    public class TemplateController : ConfigurationController
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="logger"></param>
        public TemplateController(IRepositoryFactory factory, ILogger<TemplateController> logger)
            : base(factory, logger)
        {
        }

        /// <summary>
        /// Retrieve a specific template
        /// </summary>
        /// <param name="id">The id for the required template</param>
        /// <returns>The found template, or NotFound</returns>
        [Route("{id:int}")]
        //[ResponseType(typeof(Template))]
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
        [HttpGet]
        public IActionResult Get(int id)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetTemplate(id);
                if (obj == null)
                    return NotFound();
                return Ok(obj.AsModel(BaseUri));
            });
        }

        /// <summary>
        /// Delete a specific template
        /// </summary>
        /// <param name="id">The id for the required template to delete</param>
        /// <returns>Ok if deleted or NotFound</returns>
        [Route("{id:int}")]
        [HttpDelete]
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
        public IActionResult Delete(int id)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetTemplate(id);
                if (obj == null)
                    return NotFound();

                _repository.DeleteTemplateDown(id);
                return Ok();
            });
        }

        /// <summary>
        /// Create a new template for the given stub and xpaths
        /// </summary>
        /// <param name="template">The template to create</param>
        /// <returns></returns>
        [Route("")]
       // [ResponseType(typeof(Template))]
        [StatusCodeSwaggerResponse(HttpStatusCode.BadRequest)]
        [StatusCodeSwaggerResponse(HttpStatusCode.Created)]
        [HttpPost]
        public IActionResult CreateFor([FromBody]Template template)
        {
            return TryCatch(() =>
            {
                if (!template.Matches.Any())
                    return BadRequest("No matches defined.");
                if (string.IsNullOrEmpty(template.Stub?.Link))
                    return BadRequest("Template should always be linked to a Stub.");

                var s = _repository.GetMessageType(template.Stub.IdFromLink());
                if (s == null)
                    return BadRequest($"No stub found at {template.Stub.Link}");

                var newTemplate = _repository.UpdateTemplate(null, t => {
                    t.MessageTypeId = template.Stub.Id.Value;
                    t.Description = template.Name;
                    t.TemplateXpath.AddRange(template.Matches.Select(x => new Repository.StubDb.Entities.TemplateXpath() { XpathId = x.IdFromLink() }));

                });
                var combo = _repository.UpdateCombination(null, c => {
                    c.TemplateId = newTemplate.TemplateId;
                    c.MessageTypeId = template.Stub.Id.Value;
                    c.Response = new Repository.StubDb.Entities.Response() { Description = newTemplate.Description, ResponseText = "<EMPTY_RESPONSE/>" };
                    c.CombinationXpath.AddRange(template.Matches.Select(x => new Repository.StubDb.Entities.CombinationXpath() { XpathId = x.IdFromLink(), XpathValue = "<EMPTY>" }));

                });

                newTemplate = _repository.UpdateTemplate(newTemplate.TemplateId, t => t.Combination.Add(combo));
                _repository.Flush();
                newTemplate = _repository.GetTemplate(newTemplate.TemplateId);
                return Created(newTemplate.AsModel(BaseUri));
            });
        }

        /// <summary>
        /// Update the given template
        /// </summary>
        /// <param name="id">The template id to update</param>
        /// <param name="template">The new data</param>
        /// <returns>The updated template, or NotFound</returns>
        [Route("{id:int}")]
     //   [ResponseType(typeof(Template))]
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
        [HttpPatch]
        public IActionResult Update(int id, [FromBody]Template template)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetTemplate(id);
                if (obj == null)
                    return NotFound();

                _repository.UpdateTemplate(id, a => a.Description = template.Name);

                return Ok(obj.AsModel(BaseUri));
            });
        }


    }
}
