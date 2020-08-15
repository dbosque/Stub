using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Server.WebApi.Configuration.Model;
using dBosque.Stub.Services.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// The Match controller
    /// </summary>
    [Route("Match")]
    public class MatchController : ConfigurationController
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="logger"></param>
        public MatchController(IRepositoryFactory factory, ILogger<MatchController> logger) 
            : base(factory, logger)
        {
        }

        /// <summary>
        /// Return all matches for a specific request
        /// </summary>
        /// <param name="data">The request to check</param>
        /// <returns>All Matches that can react on this request, or NotFound</returns>
        [Route("for")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetXpathsFor([FromBody] string data)
        {
            return TryCatch(() =>
            {
                var obj = data.CreateDocument().GetAllValidFor(_repository.GetXpaths().Where( x => x.Type == 0), (x) => x.Expression).ToList();
                if (obj == null || obj.Count == 0)
                    return NotFound();
                return Ok(obj.Select(a => a.AsModel(BaseUri)));
            });
        }

        /// <summary>
        /// Create a new Match record
        /// </summary>
        /// <param name="match">The match record to create</param>
        /// <returns>The created match or Conflict in case it already exists</returns>
        [Route("")]
        [HttpPost]
        //[ResponseType(typeof(Match))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create([FromBody]Match match)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetXpaths().FirstOrDefault(a => a.Expression == match.Expression && a.Type == (int)match.Type);
                if (obj != null)
                    return new ConflictResult();

                obj = _repository.UpdateXpath(null, a => {
                    a.Type = (int)match.Type;
                    a.Expression = match.Expression;

                });
                return Created(obj.AsModel(BaseUri));
            });
        }

        /// <summary>
        /// Retrieve all availabe matches
        /// </summary>
        /// <returns>All available matches, or NotFound</returns>
        [Route("")]
        [HttpGet]
        //[ResponseType(typeof(IEnumerable<Match>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetXpaths();
                if (obj == null)
                    return NotFound();
                return Ok(obj.Select(a => a.AsModel(BaseUri)));
            });
        }


        /// <summary>
        /// Delete a specific math
        /// </summary>
        /// <param name="id">The match id to delete</param>
        /// <returns>Ok if deleted or NotFound or conflict in case the match is still being used</returns>
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Delete(int id)
        {
            return TryCatch(() => 
            {
                var obj = _repository.GetXpath(id);
                if (obj == null)
                    return NotFound();
                if (_repository.IsInUse(obj))
                    return new ConflictResult();
                _repository.DeleteXpath(obj);
                return Ok();
            });
        }

        /// <summary>
        /// Get a specific match
        /// </summary>
        /// <param name="id">The match id to find</param>
        /// <returns>The found match or NotFound</returns>
        [Route("{id:int}")]
        [HttpGet]
        //  [ResponseType(typeof(Match))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetXpath(id);
                if (obj == null)
                    return NotFound();
                return Ok(obj.AsModel(BaseUri, true));
            });
        }

        /// <summary>
        /// Get a specific match and it's corresponding value
        /// </summary>
        /// <param name="id">The match to find</param>
        /// <returns>The found match or NotFound</returns>
        [Route("Instance/{id:int}")]
        [HttpGet]
        // [ResponseType(typeof(MatchInstance))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMatchInstance(int id)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetCombinationXpath(id);
                if (obj == null)
                    return NotFound();
                return Ok(obj.AsModel(BaseUri));
            });
        }

        /// <summary>
        /// Update a value for a specific match
        /// </summary>
        /// <param name="id">The match to update</param>
        /// <param name="path">The updated match data</param>
        /// <returns>The updated match or NotFound</returns>
        [Route("Instance/{id:int}")]
        //  [ResponseType(typeof(MatchInstance))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public IActionResult PostMatchInstance(int id, [FromBody]MatchInstance path)
        {
            return TryCatch(() =>
            {
                var obj = _repository.GetCombinationXpath(id);
                if (obj == null)
                    return NotFound();
                _repository.UpdateCombinationXpath(id, a => a.XpathValue = path.Value);

                return Ok(obj.AsModel(BaseUri));
            });
        }

    }
}
