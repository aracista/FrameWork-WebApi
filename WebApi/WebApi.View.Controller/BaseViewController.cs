using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.DataAccess;
using WebApi.Core;
using WebApi.Core.Service;

namespace WebApi.View.Controller
{
    public class BaseViewController
    {
        protected IConfiguration ConfigurationItem;
        protected DataAccessServiceOracle OraDatabase;
        protected DataAccessServiceSqlServer SqlDatabase;

        public BaseViewController()
        {
            OraDatabase = new DataAccessServiceOracle("OracleDB");
            SqlDatabase = new DataAccessServiceSqlServer("MSSQLDB");
        }

        public IConfiguration ConfigurationSetting(string KeyName)
        {
            ConfigService config = new ConfigService();
            ConfigurationItem = config.GetAPIUrl(KeyName);
            return ConfigurationItem;
        }
    }
}
