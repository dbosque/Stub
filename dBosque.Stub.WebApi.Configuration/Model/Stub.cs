using Newtonsoft.Json;
using System.Collections.Generic;

namespace dBosque.Stub.WebApi.Configuration.Model
{
    /// <summary>
    /// A Stub
    /// </summary>
    public class Stub : Linkable
    {
        /// <summary>
        /// 
        /// </summary>
        public Stub(): base ("Stub")
        {
        }

        /// <summary>
        /// A nice description to humanly identify the stub
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The namespace on which this stub reacts
        /// </summary>
        [JsonProperty(Order = 2)]
        public string Namespace
        {
            get;
            set;
        }

        /// <summary>
        /// The regular expression on which this stub reacts
        /// The .NET regular expression parser is used (https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference)
        /// All named groups are extract and used as template restrictions (https://docs.microsoft.com/en-us/dotnet/standard/base-types/grouping-constructs-in-regular-expressions)
        /// </summary>
        [JsonProperty(Order = 3)]
        public string UriRegex
        {
            get;
            set;
        }

        /// <summary>
        /// The rootnode on which this stub reacts
        /// </summary>
        [JsonProperty(Order = 4)]
        public string RootNode
        {
            get;
            set;
        }

        /// <summary>
        /// Allow passthrough of the request when no match was found.
        /// </summary>
        [JsonProperty(Order = 5)]
        public bool ? AllowPassthrough
        {
            get;
            set;
        }

        /// <summary>
        /// The uri which to relay the request when no match was found.
        /// </summary>
        [JsonProperty(Order = 6)]
        public string PassthroughUri
        {
            get;
            set;
        }

        /// <summary>
        /// Sample message (base64encoded)
        /// </summary>
        [JsonProperty(Order = 7)]
        public string Sample
        {
            get;
            set;
        }

        /// <summary>
        /// The templates that are available for this stub
        /// </summary>
        [JsonProperty(Order = 8)]
        public IEnumerable<Linkable> Templates
        {
            get;
            set;
        }
    }
}