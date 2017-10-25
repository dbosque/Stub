using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dBosque.Stub.Server.WebApi.Json.Configuration
{

    /// <summary>
    /// Resolve the basetype properties first in the json
    /// </summary>
    public class BaseTypeFirstContractResolver : DefaultContractResolver
    {
        static BaseTypeFirstContractResolver instance;
        static BaseTypeFirstContractResolver()
        {
            instance = new BaseTypeFirstContractResolver();
        }

        /// <summary>
        /// Singleton implementation
        /// </summary>
        public static BaseTypeFirstContractResolver Instance => instance;
        ///<summary>
        ///Creates properties for the given <see cref = "T:Newtonsoft.Json.Serialization.JsonContract"/>.
        ///</summary>
        ///<param name = "type">The type to create properties for.</param>
        ///<param name = "memberSerialization">The member serialization mode for the type.</param>
        ///<returns>Properties for the given <see cref = "T:Newtonsoft.Json.Serialization.JsonContract"/>.</returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            if (properties != null)
                return properties.OrderBy(p => p.DeclaringType.BaseTypesAndSelf().Count()).ThenBy(p => p.Order).ToList();
            return properties;
        }
    }
}