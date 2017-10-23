
using dBosque.Stub.Editor.Controls.Models.Converters;
using System.ComponentModel;

namespace dBosque.Stub.Editor.Controls.Models
{

    /// <summary>
    /// A container object to hold a match value and key
    /// </summary>
    [TypeConverter(typeof(MatchPropertyConverter))]
    public class MatchProperty
    {
        /// <summary>
        /// The typename
        /// </summary>
        private readonly string _typename;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="typename"></param>
        public MatchProperty(string key, string value, string typename)
        {
            Match = key;
            Value = value;
            _typename = typename;
        }

        /// <summary>
        /// The match name (xpath, regexgroupname), can not be edited
        /// </summary>
        [Description("The match key")]
        public string Match { get; private set; }

        /// <summary>
        /// The value to trigger upon
        /// </summary>
        [Description("The value to trigger upon.")]
        public string Value { get; set; }

        /// <summary>
        /// A nice displayname
        /// </summary>
        /// <returns></returns>
        public string DisplayName() =>$"{ _typename} : {Match}";
    }
}
