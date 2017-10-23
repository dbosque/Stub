using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace dBosque.Stub.WebApi.Configuration.Extensions
{
    /// <summary>
    /// A conflict result
    /// </summary>
    public class ConflictResult : NoContentResult
    {
        ///<inheritdoc/>
        public override void ExecuteResult(ActionContext context)
        {
            base.ExecuteResult(context);
            context.HttpContext.Response.StatusCode = 409;
        }
    }
}