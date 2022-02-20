using System;
using System.Reflection;
using System.Resources;

namespace WebApi.Util.Database
{
    public class DatabaseUtil
    {
        public static object[] ObjectToParamSP(object param)
        {
            PropertyInfo[] properties = param.GetType().GetProperties();
            object[] result = new object[properties.Length * 2];
            int index = -1;
            foreach (PropertyInfo pi in properties)
            {
                index += 1;

                result[index] = "@" + pi.Name;

                index += 1;

                result[index] = pi.GetValue(param, null);
            }

            return result;
        }

        public static string ReadSQLQueriesFromResourceFile(string keyName, Type sQueries)
        {
            ResourceManager myManager = new ResourceManager(sQueries);
            string sqlQueries = myManager.GetString(keyName);

            return sqlQueries;
        }
    }
}

