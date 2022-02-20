using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace WebApi.Util.ExternalFileVariable
{
    public class JsonVariable<T>
    {
        public static T ReadExternalConString(string fullpathFile)
        {
            using (StreamReader r = new StreamReader(fullpathFile))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);

            }
        }
    }
}

