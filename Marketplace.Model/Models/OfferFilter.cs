using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class OfferFilter
    {
        public Filter Filter { get; set; }
        public FilterItem FilterItem { get; set; }
        public Offer Offer { get; set; }
    }
}
