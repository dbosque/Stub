using dBosque.Stub.Services.Extensions;
using NSwag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBosque.Stub.Services.ExternalReferenceResolvers
{
    /// <summary>
    /// Resolve a Swagger documention object to potential messagetypes
    /// </summary>
    public class SwaggerResolver : ExternalResolverBase
    {
        ///<summary>
        ///Extract the messageTypes from the given content
        ///</summary>
        ///<param name="content">The content to parse</param>
        ///<returns></returns>
        protected override IEnumerable<ExternalMessageType> FromContent(string content)
        {
            var doc = Task.Run(async () => { return await OpenApiDocument.FromJsonAsync(content); }).Result;
            return doc.ExtractInfo().Distinct();
        }

        ///<summary>
        ///Extract the messageTypes from the given uri
        ///</summary>
        ///<param name="uri">The url to retrieve</param>
        ///<returns></returns>
        protected override IEnumerable<ExternalMessageType> FromUri(string uri)
        {
            OpenApiDocument doc = null;
            try
            {
                doc = Task.Run(async () => { return await OpenApiDocument.FromUrlAsync(uri); }).Result; 

            }
            catch (Exception)
            {
                //
            }
            return doc.ExtractInfo();
        }
    }
}
