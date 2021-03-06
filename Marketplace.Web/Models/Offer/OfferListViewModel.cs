﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Models.Offer
{   
    public enum Sort
    {
        BestSeller,
        PriceAsc,
        PriceDesc,
        Newest
    }
    public class OfferListViewModel
    {
        public string GameName { get; set; }

        public SearchOfferViewModel SearchInfo { get; set; }
        public PageInfoViewModel PageInfo { get; set; }

        public IEnumerable<OfferViewModel> Offers { get; set; } = new List<OfferViewModel>();
        public IList<SelectListItem> Games { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> SortBy { get; set; }

        public OfferListViewModel()
        {
            SortBy = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "Лучший продавец",
                    Value = Sort.BestSeller.ToString()
                },
                new SelectListItem
                {
                    Text = "От дешевых к дорогим",
                    Value = Sort.PriceAsc.ToString()
                },
                new SelectListItem
                {
                    Text = "От дорогих к дешевым",
                    Value = Sort.PriceDesc.ToString()
                },
                new SelectListItem
                {
                    Text = "Самые новые",
                    Value = Sort.Newest.ToString()
                }
            };
        }
    }
    
}
