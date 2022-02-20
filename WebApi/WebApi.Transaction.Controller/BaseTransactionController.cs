using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Core.Service;
using WebApi.DataAccess;

namespace WebApi.Transaction.Controller
{
    public class BaseTransactionController
    {
        protected DataAccessServiceOracle OraDatabase;
        protected DataAccessServiceSqlServer SqlDatabase;
        protected ConfigService _configService;

        public BaseTransactionController()
        {
            OraDatabase = new DataAccessServiceOracle("OracleDB");
            SqlDatabase = new DataAccessServiceSqlServer("MSSQLDB");
            _configService = new ConfigService();

        }
    }
}
