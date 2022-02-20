using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Reflection;
using WebApi.DataAccess;
using WebApi.Util.Database;

namespace WebApi.Repository.Query
{
    public abstract class BaseRepositoryQuery
    {
        protected DataAccessServiceOracle OraDatabase;
        protected DataAccessServiceSqlServer SqlDatabase;
        protected Type sQueries;
        Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains("LOS.Database.SQLQuery"));

        #region Oracle
        public DataSet OraExecDataSet(string queryName, object param)
        {
            return OraDatabase.ExecDataSetOra(DatabaseUtil.ReadSQLQueriesFromResourceFile(queryName, sQueries), DatabaseUtil.ObjectToParamSP(param));
        }

        protected DataSet OraExecDataSet(string queryName)
        {
            return OraDatabase.ExecDataSetOra(DatabaseUtil.ReadSQLQueriesFromResourceFile(queryName, sQueries));
        }

        public DataSet OraGetQueryDynamicQuery(object param, string key, string resourceName)
        {
            var typeResource = assembly.DefinedTypes.Where(x => x.Name.Equals(resourceName)).Single();
            string query = DatabaseUtil.ReadSQLQueriesFromResourceFile(key, typeResource);

            return param == null ? OraDatabase.ExecDataSetOra(query) : OraDatabase.ExecDataSetOra(query, DatabaseUtil.ObjectToParamSP(param));
        }

        public string OraReplaceQueryWithParam(string query, object param)
        {
            PropertyInfo[] properties = param.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                query = query.Replace(string.Format(@"[[{0}]]", pi.Name), Convert.ToString(pi.GetValue(param, null)));
            }

            return query;
        }
        #endregion

        #region Sql Server
        public DataSet SqlExecDataSet(string queryName, object param)
        {
            return SqlDatabase.ExecDataSet(DatabaseUtil.ReadSQLQueriesFromResourceFile(queryName, sQueries), DatabaseUtil.ObjectToParamSP(param));
        }

        protected DataSet SqlExecDataSet(string queryName)
        {
            return SqlDatabase.ExecDataSet(DatabaseUtil.ReadSQLQueriesFromResourceFile(queryName, sQueries));
        }

        public DataSet SqlGetQueryDynamicQuery(object param, string key, string resourceName)
        {
            var typeResource = assembly.DefinedTypes.Where(x => x.Name.Equals(resourceName)).Single();
            string query = DatabaseUtil.ReadSQLQueriesFromResourceFile(key, typeResource);

            return param == null ? SqlDatabase.ExecDataSet(query) : SqlDatabase.ExecDataSet(query, DatabaseUtil.ObjectToParamSP(param));
        }

        public string ReplaceQueryWithParam(string query, object param)
        {
            PropertyInfo[] properties = param.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                query = query.Replace(string.Format(@"[[{0}]]", pi.Name), Convert.ToString(pi.GetValue(param, null)));
            }

            return query;
        }
        #endregion
    }
}
