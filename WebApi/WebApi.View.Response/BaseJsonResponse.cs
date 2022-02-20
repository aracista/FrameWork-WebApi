using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Core.Entity.BaseJsonResponse;

namespace WebApi.View.Response
{
    public class BaseJsonResponse
    {
        public object Data { set; get; }
        public BaseJsonResponseHeader Header { set; get; }
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
