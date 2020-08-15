
namespace dBosque.Stub.Server.WebApi.Configuration.Model
{
    /// <summary>
    /// Trace info
    /// </summary>
    public class Trace : Linkable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "url"></param>
        public Trace(string url): base (url)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public Trace(): base ("Trace")
        {
        }

        /// <summary>
        /// The template instance that triggerd this trace
        /// </summary>
        public Linkable Instance
        {
            get;
            set;
        }

        /// <summary>
        /// The stub that triggerd this trace
        /// </summary>
        public Linkable Stub
        {
            get;
            set;
        }

        /// <summary>
        /// The time the trace originated
        /// </summary>
        public System.DateTime Time
        {
            get;
            set;
        }

        /// <summary>
        /// The data that was recieved (base64encoded)
        /// </summary>
        public string Data
        {
            get;
            set;
        }

        /// <summary>
        /// The endpoint uri
        /// </summary>
        public string Uri
        {
            get;
            set;
        }
    }

}