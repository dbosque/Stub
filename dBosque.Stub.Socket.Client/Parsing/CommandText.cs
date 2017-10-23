using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace dBosque.Stub.Socket.Client.Parsing
{
    /// <summary>
    /// The commandtext from raw sql
    /// </summary>
    public class CommandText
    {
        /// <summary>
        /// The raw input
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        /// The statement type
        /// </summary>
        public string Statement { get; set; }

        /// <summary>
        /// The tables participating in the command
        /// </summary>
        public List<string> Tables { get; set; } = new List<string>();

        /// <summary>
        /// The variables and their corresponding column
        /// </summary>
        public List<Tuple<string, string, string>> Variables { get; set; } = new List<Tuple<string, string, string>>();

        /// <summary>
        /// Create a new CommandText from a sql string
        /// </summary>
        /// <param name="sql">The raw sql to parse.</param>
        /// <returns></returns>
        public static CommandText Create(string sql)
        {
            return DbCommandTreeExtractor.Extract(sql);
        }

        /// <summary>
        /// Update the variables with the values of the corresponding command parameters.
        /// </summary>
        /// <param name="command"></param>
        public void UpdateParameterValues(DbCommand command)
        {
            foreach (var p in command.Parameters.OfType<DbParameter>())
            {
                var v = Variables.FirstOrDefault(v1 => v1.Item1 == p.ParameterName);
                if (v != null)
                {
                    // If found, remove and replace the variable.
                    Variables.Remove(v);
                    Variables.Add(new Tuple<string, string, string>(v.Item1, v.Item2, p.Value.ToString()));
                }
            }

        }
    }
}
