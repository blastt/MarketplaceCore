using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class FilterRangeValue : BaseEntity<int>
    {
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public double Value { get; set; }
        public FilterRange FilterRange { get; set; }
    }
}
