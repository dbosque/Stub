using Newtonsoft.Json;
using System.Collections.Generic;

namespace dBosque.Stub.Server.WebApi.Configuration.Model
{
    /// <summary>
    /// The different matchtypes supported
    /// </summary>
    public enum MatchType
    {
        /// <summary>
        /// Xpatch to an object in the request
        /// </summary>
        XPath = 0,
        /// <summary>
        /// Group name of an uri regex
        /// </summary>
        UriGroupName = 1,
       
        /// <summary>
        /// Regex to match on content
        /// </summary>
        RegEx = 2
    }
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
        /// The value of the match
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Expression
        {
            get;
            set;
        }

        /// <summary>
        /// The type of match
        /// </summary>
        [JsonProperty(Order = 2)]
        public MatchType Type 
        {
            get;
            set;
        }
    }
}