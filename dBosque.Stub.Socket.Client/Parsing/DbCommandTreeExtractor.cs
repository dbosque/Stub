using dBosque.Stub.Socket.Client.Exception;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;


namespace dBosque.Stub.Socket.Client.Parsing
{

    /// <summary>
    /// The SQL command Extractor
    /// </summary>
    internal static class DbCommandTreeExtractor
    {
        static DbCommandTreeExtractor()
        {
            try
            {
                parserMethodInfo = Type.GetType(PARSERTYPE).GetMethod(PARSERMETHOD, new Type[] { typeof(string) });
            }
            catch 
            {
                // Parsing not available.
            }
        }

        /// <summary>
        /// Parser information
        /// </summary>
        private static readonly string PARSERTYPE = "Microsoft.SqlServer.Management.SqlParser.Parser.Parser, Microsoft.SqlServer.Management.SqlParser, Version=13.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
        private static readonly string PARSERMETHOD = "Parse";
        private static readonly MethodInfo parserMethodInfo;


        /// <summary>
        /// Table element names
        /// </summary>
        private static readonly string[] TABLE =  { "SqlTableRefExpression" };
        
        /// <summary>
        /// Object element name
        /// </summary>
        private static readonly string[] OBJECT ={ "SqlObjectIdentifier" };
        
        /// <summary>
        /// Column attribute names
        /// </summary>
        private static readonly string[] COLUMNNAMES = { "ColumnOrPropertyName", "ColumnName" };

        /// <summary>
        /// Variable element names
        /// </summary>
        private static readonly string[] VARIABLES =  { "SqlGlobalScalarVariableRefExpression", "SqlScalarVariableRefExpression" };

        /// <summary>
        /// Column element names
        /// </summary>
        private static readonly string[] COLUMNS = { "SqlColumnRefExpression", "SqlScalarRefExpression" };

        /// <summary>
        /// The Table schemaname
        /// </summary>
        private static readonly string SCHEMANAME = "SchemaName";

        /// <summary>
        /// Object attribute name
        /// </summary>
        private static readonly string OBJECTNAME = "ObjectName";

        /// <summary>
        /// Variable attribute name
        /// </summary>
        private static readonly string VARIABLENAME = "VariableName";

        /// <summary>
        /// Supported statements
        /// </summary>
        private static readonly Dictionary<string, string> STATEMENTS = new Dictionary<string, string>()
        {
            { "SqlSelectStatement","select"},
            { "SqlDeleteStatement","delete"},
            { "SqlInsertStatement","insert"},
            { "SqlUpdateStatement","update"}
        };

        /// <summary>
        /// Extract the commandText from the given sql
        /// </summary>
        /// <param name="sql">The raw sql statement</param>
        /// <returns></returns>
        internal static CommandText Extract(string sql)
        {
            // If no sql given, return the command
            var command = new CommandText() { Raw = sql };
            if (!string.IsNullOrEmpty(sql) && parserMethodInfo != null)
            {
                // Extract the tree
                var doc = ExtractSyntaxTree(sql);                
                ExtractTables(command, doc);
                ExtractStatement(command, doc);
                ExtractVariables(command, doc);
            }
            return command;
        }

        /// <summary>
        /// Extract the statement from the commandtree
        /// </summary>
        /// <param name="command">The command to update</param>
        /// <param name="doc">The commandtree</param>
        private static void ExtractStatement(CommandText command, XContainer doc)
        {
            var items = new List<string>();
            foreach (var s in STATEMENTS)
                items.AddRange(doc.Descendants(s.Key).Select(a => s.Value));
            command.Statement = items.First();
        }

        /// <summary>
        /// Extract the tables from the commandtree
        /// </summary>
        /// <param name="command">The command to update</param>
        /// <param name="doc">The commandtree</param>
        private static void ExtractTables(CommandText command, XContainer doc)
        {
            foreach (var t in TABLE)
                foreach (var o in OBJECT)
                    command.Tables.AddRange(doc.Descendants(t).Descendants(o).Select(x => $"{x.Attribute(SCHEMANAME)?.Value??""}.{x.Attribute(OBJECTNAME)?.Value??""}"));
        }

        /// <summary>
        /// Extract the variables from the commandtree
        /// </summary>
        /// <param name="command">The command to update</param>
        /// <param name="doc">The commandtree</param>
        private static void ExtractVariables(CommandText command, XContainer doc)
        {
           
            foreach (var v in VARIABLES)
            {
                foreach (var x in doc.Descendants(v))
                {
                    var key = x.Attribute(VARIABLENAME)?.Value;
                    var refExpr = x.ElementsBeforeSelf()
                                        .Where(a => COLUMNS.ToList().Contains(a.Name.LocalName))
                                        .Attributes()
                                        .FirstOrDefault(a => COLUMNNAMES.ToList().Contains(a.Name.LocalName))
                                        ?.Value;

                    if (!string.IsNullOrEmpty(refExpr))
                        command.Variables.Add( new Tuple<string, string, string>( key?.Replace("@",""), refExpr, null));
                }
            }
        }



        /// <summary>
        /// Extract the syntax tree from the raw sql
        /// </summary>
        /// <param name="raw">The raw sql statement</param>
        /// <returns></returns>
        private static XDocument ExtractSyntaxTree(string raw)
        {            
            var rst = parserMethodInfo.Invoke(null, new object[] { raw }); //Parser.Parse(raw);
            var errors = rst.GetType().GetProperty("Errors").GetValue(rst) as IEnumerable<object>;
            var nonWarnings = errors.Where(a => !((bool)a.GetType().GetProperty("IsWarning").GetValue(a)));
            if (nonWarnings.Any())
            {
                var first = nonWarnings.First();                
                throw new CommandTextException(first.GetType().GetProperty("Message").GetValue(first) as string);
            }
            var script = rst.GetType().GetProperty("Script", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(rst);
            var xml = script.GetType().GetProperty("Xml").GetValue(script) as string;
            return XDocument.Parse(xml);
        }
    }
}
