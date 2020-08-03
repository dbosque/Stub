using dBosque.Stub.Services.ExternalReferenceResolvers;
using NSwag;
using System.Collections.Generic;
using System.Linq;

namespace dBosque.Stub.Services.Extensions
{
    public static class SwaggerDocumentExtensions
    {    
        public static IEnumerable<ExternalMessageType> ExtractInfo(this OpenApiDocument document)
        {
            if (document != null)
            {                
                foreach (var o in document.Operations)
                {
                    string request = string.Empty;
                    var path = o.Path;
                    var bodyparam = o.Operation.Parameters.FirstOrDefault(p => p.Kind == OpenApiParameterKind.Body);
                    if (bodyparam != null)
                        request = bodyparam.Schema.Parse().ToString();
                    var queryparameters = o.Operation.Parameters.Where(p => p.Kind == OpenApiParameterKind.Query);
                    if (queryparameters.Any())
                        path += "?"+ string.Join("&", queryparameters.Select(p => $"(<{p.Name}>{p.Type.AsNonGreedyRegex()})"));
                    var pathparameters = o.Operation.Parameters.Where(p => p.Kind == OpenApiParameterKind.Path);
                    if (pathparameters.Any())
                    {
                        // Remove the parameters in the path
                        foreach (var p in pathparameters)
                        { 
                            path = path.Replace("/{" + p.Name + "}", "");
                            path +=  $"/(?<{p.Name}>{p.Type.AsNonGreedyRegex()})";
                        }                       
                    }
                    
                    yield return new ExternalMessageType
                    {
                        // The path to the passthrough uri is added by the relay handler
                        Uri = document.BaseUrl,
                        Request = request,
                        // Remove the first character of the path
                        RegEx = path.Substring(1),
                        Description = o.Operation.Summary ?? o.Operation.OperationId
                    };
                }
                   
            }
        }
    }
}
