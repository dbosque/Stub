using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Net;

namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// HttpStatusCode to int converting SwaggerResponse Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class StatusCodeSwaggerResponseAttribute : SwaggerResponseAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="code"></param>
        public StatusCodeSwaggerResponseAttribute(HttpStatusCode code) 
            : base((int)code)
        { }
    }
}
