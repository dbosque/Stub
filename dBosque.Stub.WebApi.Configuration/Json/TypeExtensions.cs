using System;
using System.Collections.Generic;

namespace dBosque.Stub.WebApi.Json.Configuration
{
    /// <summary>
    /// Helper extension
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Return all types in the hierachy
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> BaseTypesAndSelf(this Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}