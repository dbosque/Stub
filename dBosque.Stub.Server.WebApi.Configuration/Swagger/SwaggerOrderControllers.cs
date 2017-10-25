using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;


namespace dBosque.Stub.Server.WebApi.Configuration.Swagger
{
    /// <summary>
    /// Define specific documentorder
    /// </summary>
    internal class SwaggerOrderControllers : IDocumentFilter
    {

        readonly string[] _order = new[] { "/Stub", "/Template", "/Instance", "/Match" , "/Trace"};
        private int Order(string name, string[] keys)
        {
            // find order in list
            // Name starts with a name in the order list
            var key = Array.IndexOf(_order, _order.FirstOrDefault(name.StartsWith)) * 1000;
            return key + Array.IndexOf(keys, name);
        }
        void IDocumentFilter.Apply(Swashbuckle.AspNetCore.Swagger.SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {

            var items = swaggerDoc.Paths.OrderBy(a => a.Key).ToList();
            var keys = items.Select(a => a.Key).ToArray();
            swaggerDoc.Paths.Clear();
            items.Sort((a, b) => Order(a.Key, keys).CompareTo(Order(b.Key, keys)));
            items.ForEach(swaggerDoc.Paths.Add);
        }

       
    }
}
