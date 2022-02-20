using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Core.Entity
{
    public class BasePagingResponse<T>
    {
        public List<T> Data { get; set; }
        public int TotalRecord { get; set; }
    }
}
