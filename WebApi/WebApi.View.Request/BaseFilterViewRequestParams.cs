using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApi.View.Request
{
    public class BaseFilterViewRequestParams
    {
        public int page { get; set; }
        public int size { get; set; }
        [StringLength(50)]
        public string order_by { get; set; }
        [StringLength(50)]
        public string search_by_text { get; set; }
        [StringLength(50)]
        public string search_by_value { get; set; }
    }
}
