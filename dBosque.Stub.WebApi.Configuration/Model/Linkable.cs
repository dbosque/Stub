using Newtonsoft.Json;
using System.Linq;

namespace dBosque.Stub.WebApi.Configuration.Model
{
    /// <summary>
    /// A generic link class
    /// </summary>
    [JsonObject(Title = "Link")]
    public class Linkable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "url"></param>
        public Linkable(string url)
        {
            this.url = url;
        }

        /// <summary>
        /// Get the Id from the supplied link
        /// </summary>
        /// <returns></returns>
        public long IdFromLink() => (Id = long.Parse(Link.Split('/').Last())) ?? -1;
        [JsonIgnore]
        private string url
        {
            get;
            set;
        }

        /// <summary>
        /// The Id for this item
        /// </summary>
        [JsonIgnore]
        public long ? Id
        {
            get;
            set;
        }

        /// <summary>
        /// The fully qualified id for this item
        /// </summary>
        [JsonProperty(PropertyName = "Id", Order = 1)]
        public string Link
        {
            get;
            set;
        }

        /// <summary>
        /// Create the link property
        /// </summary>
        /// <param name = "baseUri"></param>
        /// <returns></returns>
        public Linkable CreateLink(string baseUri)
        {
            if (Id.HasValue)
                Link = $"{baseUri}/{url}/{Id}";
            return this;
        }

        /// <summary>
        /// Create the link property
        /// </summary>
        /// <returns></returns>
        public Linkable CreateLink()
        {
            return CreateLink("");
        }
    }

}