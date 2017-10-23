using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace dBosque.Stub.Editor.Controls.Models.Converters
{
    /// <summary>
    /// Convert statuscodes to a supported dropdown list
    /// </summary>
    public class StatusCodeConverter : StringConverter
    {

        /// <summary>
        /// Keyvalue pair to hold all statuscodes with their corresponding text
        /// </summary>
        private static List<KeyValuePair<string, long?>> _data = new List<KeyValuePair<string, long?>>();

        /// <summary>
        /// Init only once
        /// </summary>
        static StatusCodeConverter()
        {
            _data.Add(new KeyValuePair<string, long?>("", null));
            foreach (var s in Enum.GetValues(typeof(System.Net.HttpStatusCode)).Cast<object>().Distinct())
                _data.Add(new KeyValuePair<string, long?>(string.Format("{0} ({1})", s.ToString(), (int)s), (int)s));
            // Passthrough is a special case
            _data.Add(new KeyValuePair<string, long?>("Passthrough", -1));
        }

        public override bool GetStandardValuesSupported(
                       ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection
                 GetStandardValues(ITypeDescriptorContext context)
        {

            return new StandardValuesCollection(_data.Select(a => a.Key).ToArray());
        }

        /// <summary>
        /// Translate an int to a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Transpose(long? value)
        {
            return _data.FirstOrDefault(a => a.Value == (value??200)).Key;
        }

        /// <summary>
        /// Translate a string to an int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long? Transpose(string value)
        {
            return _data.FirstOrDefault(a => a.Key == value).Value;
        }
    }
}
