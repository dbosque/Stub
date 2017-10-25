using Newtonsoft.Json;
using System.Collections.Generic;

namespace dBosque.Stub.Server.WebApi.Configuration.Model
{
    /// <summary>
    /// A specifc Match (xpath of groupname)
    /// </summary>
    [JsonObject(Title = "Match")]
    public class Match : Linkable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "url"></param>
        public Match(string url): base (url)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public Match(): base ("Match")
        {
        }

        /// <summary>
        /// The Path in the XML/Json document to match
        /// </summary>
        [JsonProperty(Order = 1)]
        public string XPath
        {
            get;
            set;
        }

        /// <summary>
        ///  The groupname from a regular expression
        /// </summary>
        [JsonProperty(Order = 2)]
        public string GroupName
        {
            get;
            set;
        }

        /// <summary>
        /// The type of this match (groupname of xpath)
        /// </summary>
        [JsonProperty(Order = 3)]
        [JsonIgnore]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// The templates that use this match
        /// </summary>
        [JsonProperty(Order = 4)]
        public IEnumerable<Linkable> Templates
        {
            get;
            set;
        }
    }
}