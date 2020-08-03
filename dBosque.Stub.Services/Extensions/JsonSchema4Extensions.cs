using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using System.Linq;

namespace dBosque.Stub.Services.Extensions
{
    public static class JsonSchema4Extensions
    {

        /// <summary>
        /// Create a default object value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        private static object CreateDefault(JsonObjectType type = JsonObjectType.None)
        {
            switch (type)
            {
                case JsonObjectType.Boolean:
                    return true;
                case JsonObjectType.Integer:
                    return 0;
                case JsonObjectType.Null:
                    return null;
                case JsonObjectType.Number:
                    return 0;
                case JsonObjectType.String:
                    return "string";
            }
            return null;
        }


        /// <summary>
        /// Generate a nonGreedy regex
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string AsNonGreedyRegex(this JsonObjectType type)
        {
            switch (type)
            {
                case JsonObjectType.Boolean:
                    return "[true|false|0|1]";
                case JsonObjectType.Integer:
                    return @"\d+?";
                case JsonObjectType.Number:
                    return @"\d+?";
                case JsonObjectType.String:
                    return ".*?";
            }
            return ".*?";
        }

        /// <summary>
        /// Parse a schema to jtoken
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static JToken Parse(this JsonSchema schema)
        {
            // Override schema with reference is needed
            if (schema.HasReference)
                schema = schema.Reference;
            else
                schema = schema.ActualSchema;

            if (schema.Type == JsonObjectType.Array)               
                return JToken.FromObject(new[] { schema.Item.Parse() });

            // Simple type, just return
            if (schema.ActualProperties.Count == 0 && 
                schema.Type != JsonObjectType.None && 
                schema.Type != JsonObjectType.Array)
                return JToken.FromObject(CreateDefault(schema.Type));

            // Parse the properties
            dynamic obj = new JObject();
            foreach (var p in schema.ActualProperties)
                obj[p.Key] = p.Value.Parse();                     
      
            return obj;

        }
    }
}
