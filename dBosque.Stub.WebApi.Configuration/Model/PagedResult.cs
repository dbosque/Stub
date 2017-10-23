using System.Collections.Generic;
using System.Linq;

namespace dBosque.Stub.WebApi.Configuration.Model
{
    /// <summary>
    /// Paged result set
    /// </summary>
    public class PagedResult
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name = "pageNo"></param>
        /// <param name = "pageSize"></param>
        /// <param name = "total"></param>
        /// <param name = "routeName"></param>
        /// <param name = "additional"></param>
        public PagedResult(int pageNo, int pageSize, int total, string routeName, params KeyValuePair<string, object>[] additional)
        {
            this.routeName = routeName;
            this.pageNo = pageNo;
            this.pageSize = pageSize;
            this.total = total;
            Additional = new Dictionary<string, object>(additional.ToDictionary(x => x.Key, x => x.Value));
        }

        /// <summary>
        /// The route name
        /// </summary>
        public string routeName
        {
            get;
            set;
        }

        /// <summary>
        /// The current pageno
        /// </summary>
        public int pageNo
        {
            get;
            set;
        }

        /// <summary>
        /// The current pagesize
        /// </summary>
        public int pageSize
        {
            get;
            set;
        }

        /// <summary>
        /// The total number of possible elements
        /// </summary>
        public int total
        {
            get;
            set;
        }

        /// <summary>
        /// Additional parameters
        /// </summary>
        public IDictionary<string, object> Additional
        {
            get;
            set;
        }
    }
}