using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class FilterTextValue : BaseEntity<int>
    {
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public FilterText FilterText { get; set; }
        public FilterText SelectedFilterText { get; set; }
    }
}
