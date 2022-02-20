using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Util.Paging
{
    public class PagingOutput
    {
        public PagingHeader Paging { get; set; }
        public List<LinkInfo> Links { get; set; }
        public object Data { get; set; }
    }
}
