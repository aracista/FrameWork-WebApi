using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace WebApi.Util.ExternalFileVariable
{
    public class XmlVariable
    {
        public static string ReadExternalConString(string fullpathFile, string elementName, string componentName)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNodeList xmlnode;
            string str = "";
            FileStream fs = new FileStream(fullpathFile, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName(elementName);
            for (int i = 0; i < xmlnode.Count; i++)
            {
                if (xmlnode[i].Attributes["name"].Value == componentName)
                {
                    str = xmlnode[i].Attributes["value"].Value;
                }
            }
            xmldoc = null;
            fs.Close();

            return str;
        }
    }
}
