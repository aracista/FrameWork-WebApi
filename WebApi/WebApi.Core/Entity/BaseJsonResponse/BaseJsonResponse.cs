using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Core.Entity.BaseJsonResponse
{
    public class BaseJsonResponse
    {
        public BaseJsonResponseHeader Header { get; set; }
        public object Data { get; set; }

        public BaseJsonResponse()
        {
            this.Header = new BaseJsonResponseHeader();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
