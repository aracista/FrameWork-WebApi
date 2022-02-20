using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Core.Entity.BaseJsonResponse
{
    class BaseJsonSwaggerResponse<T>
    {
        public BaseJsonResponseHeader Header { get; set; }
        public T Data { get; set; }

        public BaseJsonSwaggerResponse()
        {
            this.Header = new BaseJsonResponseHeader();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
