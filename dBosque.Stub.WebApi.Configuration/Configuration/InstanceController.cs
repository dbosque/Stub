using dBosque.Stub.Services.Extensions;
using dBosque.Stub.WebApi.Configuration.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using dBosque.Stub.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace dBosque.Stub.WebApi.Configuration
{
    /// <summary>
    /// The Instance controller
    /// </summary>
    [Route("Instance")]
    [ApiExplorerSettings]
    public class InstanceController : ConfigurationController
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="logger"></param>
        public InstanceController(IRepositoryFactory factory, ILogger<InstanceController> logger)
            : base(factory, logger)
        {
        }

        /// <summary>
        /// Retreive a specific instance.
        /// </summary>
        /// <param name="id">The Id to retrieve</param>
        /// <returns>The found instance, or NotFound</returns>
        [Route("{id:int}")]
        [HttpGet]
    //    [ResponseType(typeof(Instance))]
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
        public IActionResult Get(int id)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetCombination(id);
                if (obj == null)
                    return NotFound();
                return Ok(obj.AsModel(BaseUri));
            });
        }

        /// <summary>
        /// Delete a specific instance.
        /// </summary>
        /// <param name="id">The Id to delete</param>
        /// <returns>Ok if deleted or NotFound</returns>
        [Route("{id:int}")]
        [HttpDelete]
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
        public IActionResult Delete(int id)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetCombination(id);
                if (obj == null)
                    return NotFound();
                _repository.DeleteCombinationDown(id);
                return Ok();
            });
        }


        /// <summary>
        /// Clone a specific template instance.
        /// </summary>
        /// <param name="id">The instance to clone</param>
        /// <param name="instance">The new data </param>
        /// <returns>the newly created item, or NotFound</returns>
        [Route("{id:int}/clone")]
        //      [ResponseType(typeof(Instance))]
        [StatusCodeSwaggerResponse(HttpStatusCode.Created)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
        [HttpPost]
        public IActionResult Clone(int id, [FromBody]Instance instance)
        {
            return TryCatch(() =>
            {
                var clone = _repository.GetCombination(id);
                if (clone == null)
                    return NotFound();

                var newid = _repository.CloneResponseTree(clone.ResponseId);


                var obj = _repository.GetCombination(newid);
                if (obj == null)
                    return NotFound();

                _repository.UpdateResponse(obj.ResponseId, a =>
                {
                    if (!string.IsNullOrEmpty(instance?.Name))
                        a.Combination.FirstOrDefault().Description = instance.Name;
                    if (!string.IsNullOrEmpty(instance?.Response))
                        a.ResponseText = instance.Response.Base64Decode();
                    if (!string.IsNullOrEmpty(instance?.ContentType))
                        a.ContentType = instance.ContentType;
                    if ((instance?.HttpStatusCode.HasValue).HasValue)
                        a.StatusCode = instance.HttpStatusCode;
                });


                return Created(obj.AsModel(BaseUri));
            });
        }

        /// <summary>
        /// Update a specific instance
        /// </summary>
        /// <param name="id">The instance to update</param>
        /// <param name="instance">The new data </param>
        /// <returns>The updated intance or NotFound</returns>
        [Route("{id:int}")]
       // [ResponseType(typeof(Instance))]
        [HttpPatch]
        [StatusCodeSwaggerResponse(HttpStatusCode.OK)]
        [StatusCodeSwaggerResponse(HttpStatusCode.NotFound)]
        public IActionResult Patch(int id, [FromBody]Instance instance)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetCombination(id);
                if (obj == null)
                    return NotFound();
                _repository.UpdateResponse(obj.ResponseId, a =>
                {
                    a.Combination.FirstOrDefault().Description = instance.Name;
                    a.ResponseText = instance.Response;
                    a.ContentType = instance.ContentType;
                    a.StatusCode = instance.HttpStatusCode;
                });


                return Ok(obj.AsModel(BaseUri));
            });
        }
    }
}
