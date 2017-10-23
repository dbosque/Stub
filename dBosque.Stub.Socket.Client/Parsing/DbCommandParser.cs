using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace dBosque.Stub.Socket.Client.Parsing
{
    /// <summary>
    /// Parse the DbCommand
    /// </summary>
    internal static class DbCommandParser
    {
        private const string _namespace = "stub.sql";
        /// <summary>
        /// Parse the command
        /// </summary>
        /// <typeparam name="T">The expectd type of the response</typeparam>
        /// <param name="command">The command that will be executed</param>
        /// <returns></returns>
        internal static string Parse<T>(DbCommand command)
        {
            return ParseCommand(command, typeof(T));
        }

        /// <summary>
        /// Parse the command and create its corresponding XML fragment
        /// </summary>
        /// <param name="w">the XML writer</param>
        /// <param name="command">The command send</param>
        /// <param name="ns">The namespace to use for the fragment</param>
        /// <param name="text">The SQL parsed commandtext</param>
        private static void ParseCommand(XmlWriter w, DbCommand command, string ns, CommandText text)
        {
            w.WriteStartElement("command", ns);
            w.WriteAttributeString("type", command.CommandType.ToString() == "Text"?text.Statement:command.CommandText.ToString());
            if (text.Tables.Any())
            {
                w.WriteStartElement("tables");
                foreach (var t in text.Tables)
                    w.WriteElementString("table", t);
                w.WriteEndElement();
            }
            foreach (var v in text.Variables)
            {
                w.WriteStartElement(v.Item2);
                    w.WriteAttributeString("parameter", v.Item1);
                    w.WriteAttributeString("value", v.Item3);
                w.WriteEndElement();
            }
           
            w.WriteElementString("database", ns, command.Connection.Database);
            w.WriteElementString("datasource", ns, command.Connection.DataSource);
            w.WriteEndElement();
        }

        /// <summary>
        /// Parse the parameter and create its corresponding XML fragment
        /// </summary>
        /// <param name="w">the XML writer</param>
        /// <param name="parameter">The parameter to proces</param>
        /// <param name="ns">The namespace to use for the fragment</param>
        private static void parseParameter(XmlWriter w, IDbDataParameter parameter, string ns)
        {
            w.WriteStartElement("parameter", ns);
                w.WriteAttributeString("name", ns, parameter.ParameterName);           
                w.WriteAttributeString("direction", ns, parameter.Direction.ToString());
                w.WriteAttributeString("nullable", ns, parameter.IsNullable.ToString());
                w.WriteAttributeString("size", ns, parameter.Size.ToString());
                w.WriteAttributeString("precision", ns, parameter.Precision.ToString());
                w.WriteAttributeString("scale", ns, parameter.Scale.ToString());
                w.WriteElementString("value", ns, (parameter.Value == null || parameter.Value == DBNull.Value) ? "null" : parameter.Value.ToString());
                w.WriteEndElement();
        }

        /// <summary>
        /// Parse the command and create its corresponding XML
        /// </summary>
        /// <param name="command">The command to be executed</param>
        /// <param name="resulttype">The expected type of the response</param>
        private static string ParseCommand(DbCommand command, Type resulttype)
        {
            // Parse the SQL
            var text = CommandText.Create(command.CommandText);
            // Update the parameter values
            text.UpdateParameterValues(command);

            // Create the XML
            var builder = new StringBuilder();
            using (TextWriter w = new StringWriter(builder))
            {
                // Force UTF8
                var settings = new XmlWriterSettings() { Encoding = Encoding.UTF8, Indent = true, OmitXmlDeclaration = true };

                using (var writer = XmlWriter.Create(builder, settings))
                {
                    // Main header
                    writer.WriteStartDocument(true);
                    writer.WriteStartElement("sql", _namespace);
                        // The command
                        ParseCommand(writer, command, _namespace, text);
                        // The expected response type
                        writer.WriteElementString("resulttype", _namespace, resulttype.ToString());
                        // Parameters if any
                        if (command.Parameters?.Count > 0)
                        {
                            writer.WriteStartElement("parameters", _namespace);
                            foreach (var parameter in command.Parameters.OfType<DbParameter>())
                                parseParameter(writer, parameter, _namespace);

                            writer.WriteEndElement();
                        }
                    writer.WriteElementString("sql", _namespace, command.CommandText ?? "<null>");
                    // Close the main SQL element
                    writer.WriteEndElement();
                    // close the document
                    writer.WriteEndDocument();
                }
            }

            return builder.ToString();
        }
    }
}
