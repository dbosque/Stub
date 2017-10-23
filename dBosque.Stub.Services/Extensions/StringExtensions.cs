using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace dBosque.Stub.Services.Extensions
{
    public static class StringExtensions
    {

        /// <summary>
        /// Return the specified string, or the other in case the first is null or empty
        /// </summary>
        /// <param name="val"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static string IfEmpty(this string val, string other = null)
        {
            return string.IsNullOrWhiteSpace(val) ? other : val;
        }
        /// <summary>
        /// Pretty print a json syntax
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string PrettyJson(this string data)
        {
            string result = data;
            try
            {
                dynamic parsedJson = JsonConvert.DeserializeObject(data);
                if (parsedJson != null)
                    result = JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception)
            {
                // nothing
            }
            return result;
        }

        /// <summary>
        /// Pretty print json/xml
        /// </summary>
        /// <param name="data">The  json/xml to pretty print.</param>
        /// <returns></returns>
        public static string Pretty(this string data)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;

            string result = data;

            // First try xml
            using (MemoryStream mStream = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode))
                {
                    try
                    {
                        XmlDocument document = new XmlDocument();
                        // Load the XmlDocument with the XML.
                        document.LoadXml(data);

                        writer.Formatting = System.Xml.Formatting.Indented;
                        // Write the XML into a formatting XmlTextWriter
                        document.WriteContentTo(writer);
                        writer.Flush();
                        mStream.Flush();

                        // Have to rewind the MemoryStream in order to read
                        // its contents.
                        mStream.Position = 0;

                        // Read MemoryStream contents into a StreamReader.
                        using (StreamReader sReader = new StreamReader(mStream))
                        {
                            // Extract the text from the StreamReader.
                            result = sReader.ReadToEnd();
                        }
                    }
                    catch (XmlException)
                    {
                        // Fallback to json
                        result = PrettyJson(data);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Remove the localnamespace from a str
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveLocalNamespace(this string str)
        {
            return Regex.Replace(str, @"\/\*\[local-name\(\)='(.*?)'\]", "/$1");
        }

        /// <summary>
        /// Strip the soapenvelope from a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StripSoapEnvelope(this string str)
        {
            if (str.StartsWith("/*[local-name()='Envelope']/*[local-name()='Body']", StringComparison.Ordinal))
                str = str.Replace("/*[local-name()='Envelope']/*[local-name()='Body']", "");
            return str;
        }

        /// <summary>
        /// Prepend a soapenvelope to a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AppendSoapEnvelope(this string str)
        {
            if (!str.StartsWith("/*[local-name()='Envelope']/*[local-name()='Body']", StringComparison.Ordinal))
                str = @"/*[local-name()='Envelope']/*[local-name()='Body']" + str;
            return str;
        }

        /// <summary>
        /// Transform to a base64Encoded string
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decode from a base64Encoded string
        /// </summary>
        /// <param name="codedText"></param>
        /// <returns></returns>
        public static string Base64Decode(this string codedText)
        {
            if (string.IsNullOrEmpty(codedText))
                return codedText;
            var plainTextBytes = Convert.FromBase64String(codedText);
            return Encoding.UTF8.GetString(plainTextBytes);
        }
    }
}
