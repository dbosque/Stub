using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class Response
    {
        public string ToHeaders(Dictionary<string, string[]> value) => JsonConvert.SerializeObject(value);

        public string ToHeaders(string[] value)
        {
            var pairs = value.Select((v) => {
                // When there is an ':', split the value
                if (v.IndexOf(':') > -1)
                {
                    return new KeyValuePair<string, string>(v.Substring(0, v.IndexOf(':')).Trim(), v.Substring(v.IndexOf(':')+1).Trim());
                }
                return new KeyValuePair<string, string>("", v);
            })
            .GroupBy(k => k.Key)
            .Select(g => new KeyValuePair<string, string[]>(g.Key, g.Select(val => val.Value).ToArray()));


            return JsonConvert.SerializeObject(pairs);
        }

        public Dictionary<string, string[]> FromHeaders()
        {
            if (Headers == null)
            {
                return new Dictionary<string, string[]>();
            }
           // return new Dictionary<string, string[]>()
            var d = JsonConvert.DeserializeObject<KeyValuePair<string, string[]>[]>(Headers);
            return d.ToDictionary( x => x.Key, x => x.Value );
        }
        public string[] ToHeadersStringArray()
        {
            if (Headers == null)
            {
                return new string[] { };
            }
            return JsonConvert.DeserializeObject<KeyValuePair<string, string[]>[]>(Headers)
                    .SelectMany(a => a.Value.Select(m => $"{a.Key} : {m}"))
                    .ToArray();
        }

    }
}
