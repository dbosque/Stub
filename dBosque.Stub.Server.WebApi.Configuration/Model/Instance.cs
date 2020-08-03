using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
namespace dBosque.Stub.Server.WebApi.Configuration.Model
{
    /// <summary>
    /// A specific instance from a template.
    /// </summary>
    public class Instance : Linkable
    {
        /// <summary>
        /// 
        /// </summary>
        public Instance() : base("Instance")
        {
        }

        /// <summary>
        /// A human readably name to identify this instance.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The response to return (base64encoded)
        /// </summary>
        [JsonProperty(Order = 5)]
        public string Response
        {
            get;
            set;
        }

        /// <summary>
        /// The HttpStatusCode to return
        /// </summary>
        [JsonProperty(Order = 2)]
        public long? HttpStatusCode
        {
            get;
            set;
        }

        /// <summary>
        /// The ContentType to return
        /// </summary>
        [JsonProperty(Order = 3)]
        public string ContentType
        {
            get;
            set;
        }

        /// <summary>
        /// The Matches and their corresponding values on which this instance reacts
        /// </summary>
        [JsonProperty(Order = 4)]
        public IEnumerable<MatchInstance> Matches
        {
            get;
            set;
        }

        /// <summary>
        /// The headers to be returned.
        /// </summary>
        [JsonProperty(Order = 5)]
        public Dictionary<string, string[]> Headers
        {
            get;
            set;
        }

        /// <summary>
        /// A link back to the template
        /// </summary>
        [JsonProperty(Order = 6)]
        public Linkable Template
        {
            get;
            set;
        }
    }
}