using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class StubDbEntities
    {
        public StubDbEntities()
        { }
        public StubDbEntities(DbContextOptions options) :
            base(options)
        { }

        private List<T> ExecSQL<T>(string query)
        {
            using (this)
            {
                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        List<T> list = new List<T>();
                        T obj = default(T);
                        while (result.Read())
                        {
                            obj = Activator.CreateInstance<T>();
                            foreach (PropertyInfo prop in obj.GetType().GetProperties())
                            {
                                if (!object.Equals(result[prop.Name], DBNull.Value))
                                {
                                    prop.SetValue(obj, result[prop.Name], null);
                                }
                            }
                            list.Add(obj);
                        }
                        return list;

                    }
                }
            }
        }

        public virtual List<stp_selectLog_Result> stp_log(string query)
        {
            return ExecSQL<stp_selectLog_Result>(query);
        }           
    }
}
