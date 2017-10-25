using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dBosque.Stub.Server.WebApi.Configuration.Extensions
{
    /// <summary>
    /// A paged result
    /// </summary>
    public class PagedActionResult : OkObjectResult
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name = "value"></param>
        public PagedActionResult(object value): base (value)
        {
        }

        /// <summary>
        /// A text representation from the page links
        /// </summary>
        public string Link
        {
            get;
            set;
        }

        ///<summary>
        ///Executes the result operation of the action method synchronously. This method is called by MVC to process
        ///the result of an action method.
        ///</summary>
        ///<param name = "context">The context in which the result is executed. The context information includes
        ///information about the action that was executed and request information.</param>
        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add("Link", Link);
            base.ExecuteResult(context);
        }

        ///<summary>
        ///Executes the result operation of the action method asynchronously. This method is called by MVC to process
        ///the result of an action method.
        ///</summary>
        ///<param name = "context">The context in which the result is executed. The context information includes
        ///information about the action that was executed and request information.</param>
        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add("Link", Link);
            return base.ExecuteResultAsync(context);
        }
    }
}