using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Models
{
    public class FilterRangeViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public double From { get; set; }
        public double To { get; set; }
    }
}
