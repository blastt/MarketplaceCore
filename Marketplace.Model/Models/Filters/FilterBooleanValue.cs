using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public class FilterBooleanValue : BaseEntity<int>
    {
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public bool Value { get; set; }

        public FilterBoolean FilterBoolean { get; set; }
    }
}
