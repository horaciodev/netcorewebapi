using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SampleAPI.DataUtils.Sql
{
    public class DataGrunt<T> where T: class, new()
    {
        private readonly string _connStr;

        public DataGrunt(string connStr)
        {
            _connStr = connStr;
        }

        public List<T> GetObjectListFromProc(string procName, 
                                            List<SqlParameter> paramList,
                                            Func<IDataReader, T> MappingFunction)
        {
            var objList = new List<T>();

            using(var cn = new SqlConnection(_connStr))
            {
                using(var cmd = new SqlCommand(procName, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(paramList!=null)
                    {
                        foreach(var p in paramList)
                            cmd.Parameters.Add(p);
                    }
                    cn.Open();
                    using(var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var obj = MappingFunction.Invoke(reader);
                            objList.Add(obj);
                        }                        
                    }
                }
            }
            return objList;
        }
    }
}