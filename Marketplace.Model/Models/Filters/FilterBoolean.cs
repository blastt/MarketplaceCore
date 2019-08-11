using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public class FilterBoolean : BaseFilter
    {
        public int? FilterBooleanValueId { get; set; }
        public FilterBooleanValue FilterBooleanValue { get; set; }
    }
}
