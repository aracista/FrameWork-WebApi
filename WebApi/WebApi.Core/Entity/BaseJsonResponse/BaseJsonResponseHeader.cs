using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Core.Entity.BaseJsonResponse
{
    public class BaseJsonResponseHeader
    {
        public IList<BaseJsonResponseError> Errors = new List<BaseJsonResponseError>();
    }
}
