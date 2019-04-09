using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class Screenshot : BaseEntity<int>
    {
        public string Value { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
