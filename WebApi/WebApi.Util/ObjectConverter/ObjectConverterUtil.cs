using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApi.Util.ObjectConverter
{
    public static class ObjectConverterUtil
    {
        public static string ObjctToCommaSeparatedString<T, TU>(this IEnumerable<T> source, Func<T, TU> func)
        {
            return string.Join(",",
            source.Select(s => func(s).ToString()).ToArray());
        }

        public static T GetParamQueryStringToObject<T>(HttpRequest request)
        {
            string responseString = request.QueryString.ToString().Replace("?", "");
            var dict = HttpUtility.ParseQueryString(responseString);
            string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k.Replace("_", ""), v => dict[v]));
            T respObj = JsonConvert.DeserializeObject<T>(json);

            return respObj;
        }
    }
}
