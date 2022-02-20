using System;
using System.Data;
using System.Linq;
using System.Reflection;
using WebApi.DataAccess;
using WebApi.Util.Database;

namespace WebApi.Repository.Command
{
    public abstract class BaseRepositoryCommand
    {
        protected DataAccessServiceOracle OraDatabase;
        protected DataAccessServiceSqlServer SqlDatabase;
        protected Type sQueries;
        Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains("WebApi.Database.SqlQuery"));
        #region Oracle
        protected void OraExecNonQuery(string queryName, object param)
        {
            OraDatabase.ExecNonQueryOra(DatabaseUtil.ReadSQLQueriesFromResourceFile(queryName, sQueries), DatabaseUtil.ObjectToParamSP(param));
        }

        protected void OraExecNonQuery(string queryName, Type type, object param)
        {
            OraDatabase.ExecNonQueryOra(DatabaseUtil.ReadSQLQueriesFromResourceFile(queryName, type), DatabaseUtil.ObjectToParamSP(param));
        }

        protected void OraExecNonQuery(string queryName)
        {
            OraDatabase.ExecNonQueryOra(DatabaseUtil.ReadSQLQueriesFromResourceFile(queryName, sQueries));
        }

        public DataSet OraGetQueryDynamicCommand(object param, string key, string resourceName)
        {
            var typeResource = assembly.DefinedTypes.Where(x => x.Name.Equals(resourceName)).Single();
            string query = DatabaseUtil.ReadSQLQueriesFromResourceFile(key, typeResource);

            return param == null ? OraDatabase.ExecDataSetOra(query) : OraDatabase.ExecDataSetOra(query, DatabaseUtil.ObjectToParamSP(param));
        }
        #endregion
        #region SQLServer
        protected void SqlExecNonQuery(string queryName, object param)
        {
            SqlDatabase.ExecNonQuery(DatabaseUtil.ReadSQLQueriesFromResourceFile(queryName, sQueries), DatabaseUtil.ObjectToParamSP(param));
        }

        protected void SqlExecNonQuery(string queryName, Type type, object param)
        {
            SqlDatabase.ExecNonQuery(DatabaseUtil.ReadSQLQueriesFromResourceFile(queryName, type), DatabaseUtil.ObjectToParamSP(param));
        }

        protected void SqlExecNonQuery(string queryName)
        {
            SqlDatabase.ExecNonQuery(DatabaseUtil.ReadSQLQueriesFromResourceFile(queryName, sQueries));
        }

        public DataSet SqlGetQueryDynamicCommand(object param, string key, string resourceName)
        {
            var typeResource = assembly.DefinedTypes.Where(x => x.Name.Equals(resourceName)).Single();
            string query = DatabaseUtil.ReadSQLQueriesFromResourceFile(key, typeResource);

            return param == null ? SqlDatabase.ExecDataSet(query) : SqlDatabase.ExecDataSet(query, DatabaseUtil.ObjectToParamSP(param));
        }
        #endregion
    }
}
