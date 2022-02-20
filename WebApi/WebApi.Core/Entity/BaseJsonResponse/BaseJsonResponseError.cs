using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Core.Entity.BaseJsonResponse
{
    public class BaseJsonResponseError
    {
        public string Message { get; set; }
        public string Cause { get; set; }
        public string Code { get; set; }
        public BaseJsonResponseError(string message, string cause, string code)
        {
            this.Message = message;
            this.Cause = cause;
            this.Code = Code;
        }
    }
}
