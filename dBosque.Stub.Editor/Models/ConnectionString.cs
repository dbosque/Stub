using System;
using System.Collections.Generic;
using System.Data;

namespace dBosque.Stub.Editor.Models
{
    /// <summary>
    /// A destructed version of a connectionstring
    /// </summary>
    public class ConnectionString
    {
        /// <summary>
        /// The database
        /// </summary>
        public string Database
        {
            set;
            private get;
        }

        /// <summary>
        /// The given datasource
        /// </summary>
        public string Datasource
        {
            set;
            private get;
        }

        /// <summary>
        /// The username to use to connect to the database
        /// </summary>
        public string Username
        {
            set;
            private get;
        }

        /// <summary>
        /// The password to use to connect to the database
        /// </summary>
        public string Password
        {
            set;
            private get;
        }

        /// <summary>
        /// Create a connectionstring from the separate properties
        /// </summary>
        /// <returns></returns>
        private string Construct()
        {
            string connection = $"Data Source={Datasource};";
            if (!string.IsNullOrEmpty(Database))
                connection += $"Initial Catalog={Database};";

            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password))
                connection += "Integrated Security=True";
            else
                connection += $"uid={Username};pwd={Password}";

            return connection;
        }

        ///<summary>Returns a string that represents the current object.</summary>
        ///<returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Construct();
        }


        /// <summary>
        /// Get a list of databases from the connectionstring
        /// </summary>
        /// <returns></returns>
        public List<string> GetDatabases()
        {
            List<string> databases = new List<string>();
            System.Data.SqlClient.SqlConnection SqlCon = null;
            try
            {
                SqlCon = new System.Data.SqlClient.SqlConnection(ToString());
                SqlCon.Open();
                System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand()
                { Connection = SqlCon, CommandType = CommandType.StoredProcedure, CommandText = "sp_databases" };
                var sqlDr = SqlCom.ExecuteReader();
                while (sqlDr.Read())
                {
                    databases.Add(sqlDr.GetString(0));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                SqlCon?.Close();
            }

            return databases;
        }
    }
}