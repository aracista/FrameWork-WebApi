using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace WebApi.Helper
{
    public static class DataTableHelper
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
                                Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], t), null);
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

        public static List<T> DataTableToListObjectNullable<T>(this DataTable table) where T : class, new()
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
                                Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name] != DBNull.Value ? row[prop.Name] : null, t), null);
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
                                Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], t), null);


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

        public static T DataTableToObjectNullable<T>(this DataTable table) where T : class, new()
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
                                Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name] != DBNull.Value ? row[prop.Name] : null, t), null);


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

        public static string GenerateHTMLTable<T>(this IEnumerable<T> list, params Expression<Func<T, object>>[] fxns)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<TABLE>\n");

            sb.Append("<TR>\n");
            foreach (var fxn in fxns)
            {
                sb.Append("<TD>");
                sb.Append(GetName(fxn));
                sb.Append("</TD>");
            }
            sb.Append("</TR> <!-- HEADER -->\n");


            foreach (var item in list)
            {
                sb.Append("<TR>\n");
                foreach (var fxn in fxns)
                {
                    sb.Append("<TD>");
                    sb.Append(fxn.Compile()(item));
                    sb.Append("</TD>");
                }
                sb.Append("</TR>\n");
            }
            sb.Append("</TABLE>");

            return sb.ToString();
        }

        static string GetName<T>(Expression<Func<T, object>> expr)
        {
            var member = expr.Body as MemberExpression;
            if (member != null)
                return GetName2(member);

            var unary = expr.Body as UnaryExpression;
            if (unary != null)
                return GetName2((MemberExpression)unary.Operand);

            return "?+?";
        }

        static string GetName2(MemberExpression member)
        {
            var fieldInfo = member.Member as FieldInfo;
            if (fieldInfo != null)
            {
                var d = fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (d != null) return d.Description;
                return fieldInfo.Name;
            }

            var propertInfo = member.Member as PropertyInfo;
            if (propertInfo != null)
            {
                var d = propertInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (d != null) return d.Description;
                return propertInfo.Name;
            }

            return "?-?";
        }
    }
}
