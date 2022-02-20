using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Util.Paging
{
    public class LinkInfo
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
    }
}
