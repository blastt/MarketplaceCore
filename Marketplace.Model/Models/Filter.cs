using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public enum FilterType
    {
        Text,
        Number,
        Boolean
    }
    public class Filter : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public FilterType Types { get; set; }

        public IList<FilterItem> FilterItems { get; set; }
    }
}
