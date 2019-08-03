using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class FilterRange : BaseFilter
    {
        public double From { get; set; }
        public double To { get; set; }
        public int? FilterRangeValueId { get; set; }
        public FilterRangeValue FilterRangeValue { get; set; }
    }
}
