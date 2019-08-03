using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class OfferFilterTextValue : BaseEntity<int>
    {
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public int FilterTextValueId { get; set; }
        public FilterTextValue FilterTextValue { get; set; }
    }
}
