using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Util.GeneralFunction
{
    public static class SecurityCryptography
    {
        public static List<T> DataTableToListObject<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable ConvertIenumerableObjectToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }


        /// <summary>
        /// Validate the minimum require parameter
        /// </summary>
        /// <param name="obj">Parameters that must be validated</param>
        /// <param name="obj2">Parameters to be excluded</param>
        /// <returns></returns>
        public static (bool status, string message) ValidateMinRequired(object obj, object obj2 = null)
        {
            var allParam = obj.GetType().GetProperties();
            if (obj2 == null)
            {

                var status = allParam.Any(a => a.GetValue(obj) != null);
                var message = String.Join(",", allParam.Select(x => x.Name));
                return (status, message);
            }
            else
            {

                var baseFilter = obj2.GetType().GetProperties();

                var request = allParam.Where(i => !baseFilter.Any(e => i.Name.Contains(e.Name)));

                var status = request.Any(a => a.GetValue(obj) != null);
                var message = String.Join(",", request.Select(x => x.Name));
                return (status, message);
            }
        }
    }
}
