using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace WebApi.Util.Datatable
{
    public static class DataTableUtil
    {
        public static List<T> DataTableToListObject<T>(this DataTable table) where T : class, new()
        {
            try
            {
                if (table.Rows.Count > 0)
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
                else
                {
                    return null;
                }
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

        public static T DataTableToObject<T>(this DataTable table) where T : class, new()
        {
            try
            {
                if (table.Rows.Count > 0)
                {
                    T obj = new T();
                    foreach (var row in table.AsEnumerable())
                    {
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
                    }
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
