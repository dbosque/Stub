using System.Collections.Generic;
using System.ComponentModel;

namespace dBosque.Stub.Editor.Controls.Models.Converters
{
    public class HeaderConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(
               ITypeDescriptorContext context)
        {
            return false;
        }

        /// <summary>
        /// Translate an int to a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static KeyValuePair<string,string> Transpose(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<KeyValuePair<string,string>>(value);
        }

        /// <summary>
        /// Translate a string to an int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Transpose(KeyValuePair<string, string> value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
    }
}
