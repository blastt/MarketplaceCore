using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class FilterItem : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
        public Filter Filter { get; set; }
    }
}
