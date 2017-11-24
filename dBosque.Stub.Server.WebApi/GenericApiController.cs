using dBosque.Stub.Interfaces;
using dBosque.Stub.Server.WebApi.Types;
using Microsoft.AspNetCore.Mvc;

namespace dBosque.Stub.Server.WebApi
{
    /// <summary>
    /// Generic input API controller
    /// </summary>
    public class GenericApiController : Controller
    {
        private IStubHandler<IActionResult> _handler;
        public GenericApiController(IStubHandler<IActionResult> handler)
        {
            _handler = handler;
        }
        /// <summary>
        /// Execute the request for a specific tenant and uri
        /// </summary>
        /// <param name="tenant">The passed tenant</param>
        /// <param name="uri">The passed uri</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [HttpPatch]
        [HttpPut]
        [HttpHead]
        [HttpOptions]       
       // [Route("Execute")]
        public IActionResult Execute(string tenant, string uri)
        {
            return _handler.HandleMessage(new ApiStubMessage(Request, uri, this, tenant));             
        }

        /// <summary>
        /// Execute the request for a specific uri
        /// </summary>
        /// <param name="uri">The passed uri</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [HttpPatch]
        [HttpPut]
        [HttpHead]
        [HttpOptions]
        //[Route("ExecuteDefault")]
        public IActionResult ExecuteDefault(string uri)
        {
            return Execute(Constants.DefaultTenant, uri);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="protocol"></param>
        /// <param name="pass"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [HttpPatch]
        [HttpPut]
        [HttpHead]
        [HttpOptions]
       // [Route("Passthrough")]
        public IActionResult Passthrough(string tenant, string protocol, string pass, string uri)
        {
            if (!string.IsNullOrEmpty(uri))
                uri = "/" + uri;
            return _handler.HandlePassthrough(new ApiStubMessage(Request, uri, this, tenant), $"{protocol}://{pass.Replace(":","/")}{uri}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="protocol"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [HttpPatch]
        [HttpPut]
        [HttpHead]
        [HttpOptions]
     //   [Route("PassthroughDefault")]
        public IActionResult PassthroughDefault( string pass, string protocol, string uri)
        {
            return Passthrough(Constants.DefaultTenant, protocol, pass, uri);            
        }
    }   
}