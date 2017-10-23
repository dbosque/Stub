using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace dBosque.Stub.WebApi.Configuration.Model
{
    /// <summary>
    /// A specific instance with value for a xpath/groupname
    /// </summary>
    public class MatchInstance : Match
    {
        /// <summary>
        /// 
        /// </summary>
        public MatchInstance(): base ("Match/Instance")
        {
        }

        /// <summary>
        /// The value on which to react
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Value
        {
            get;
            set;
        }
    }
}