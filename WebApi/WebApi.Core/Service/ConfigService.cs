using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebApi.Core.Service
{
    public class ConfigService
    {
        public string GetConnection(string key)
        {
            var basePath = Directory.GetCurrentDirectory();
            var AppSetting = new ConfigurationBuilder()
               .SetBasePath(basePath)
               .AddJsonFile("appsettings.json")
               .Build();

            return AppSetting.GetConnectionString(key);
        }

        public IConfiguration GetAPIUrl(string key)
        {
            var basePath = Directory.GetCurrentDirectory();
            var AppSetting = new ConfigurationBuilder()
               .SetBasePath(basePath)
               .AddJsonFile("appsettings.json")
               .Build();
            return AppSetting.GetSection(key);
        }

        public IConfiguration GetConfiguration()
        {
            var basePath = Directory.GetCurrentDirectory();
            var AppSetting = new ConfigurationBuilder()
               .SetBasePath(basePath)
               .AddJsonFile("appsettings.json")
               .Build();
            return AppSetting;
        }
    }
}
