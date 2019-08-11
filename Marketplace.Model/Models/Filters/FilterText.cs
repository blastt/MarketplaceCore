using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public class FilterText : BaseFilter
    {
        public IList<FilterTextValue> PredefinedValues { get; set; }

        
        public int? FilterTextValueId { get; set; }
        public FilterTextValue FilterTextValue { get; set; }

        public FilterText()
        {
            PredefinedValues = new List<FilterTextValue>();
        }
    }
}
