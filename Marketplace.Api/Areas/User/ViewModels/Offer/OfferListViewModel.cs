﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Areas.User.ViewModels.Offer
{
    public class OfferListViewModel
    {
        public IEnumerable<OfferViewModel> Offers { get; set; } = new List<OfferViewModel>();
        public int CountOfActive { get; set; }
        public int CountOfInactive { get; set; }
        public int CountOfClosed { get; set; }
    }
}
