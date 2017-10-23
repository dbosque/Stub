using Newtonsoft.Json;
using System.Collections.Generic;

namespace dBosque.Stub.WebApi.Configuration.Model
{
    /// <summary>
    /// A abstract template (xpath collection)
    /// </summary>
    public class Template : Linkable
    {
        /// <summary>
        /// 
        /// </summary>
        public Template(): base ("Template")
        {
        }

        /// <summary>
        /// A human readable name for this template
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The concrete instances that are avalable for this template
        /// </summary>
        [JsonProperty(Order = 2)]
        public IEnumerable<Linkable> Instances
        {
            get;
            set;
        }

        /// <summary>
        /// The Matches that make up this template.
        /// </summary>
        [JsonProperty(Order = 3)]
        public IEnumerable<Linkable> Matches
        {
            get;
            set;
        }

        /// <summary>
        /// A link back to the stub
        /// </summary>
        [JsonProperty(Order = 4)]
        public Linkable Stub
        {
            get;
            set;
        }
    }

}