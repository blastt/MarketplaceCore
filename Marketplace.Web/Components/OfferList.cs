using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Models.Offer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Components
{
    public class OfferListViewComponent : ViewComponent
    {
        private readonly IOfferService offerService;
        public OfferListViewComponent(IOfferService offerService)
        {
            this.offerService = offerService;
        }
        public async Task<IViewComponentResult> InvokeAsync(SearchOfferViewModel searchInfo)
        {
            var model = new OfferListViewModel();
            var offers = await offerService.GetAllOffersAsync(i => i.UserProfile.User, i => i.Game);
            Sort sort;
            Enum.TryParse(searchInfo.SortBy, out sort);
            switch (sort)
            {
                case Sort.BestSeller:
                    offers = offers.OrderBy(o => o.UserProfile.Rating).ToList();
                    break;
                case Sort.Newest:
                    offers = offers.OrderBy(o => o.CreatedDate).ToList();
                    break;
                case Sort.PriceAsc:
                    offers = offers.OrderBy(o => o.Price).ToList();
                    break;
                case Sort.PriceDesc:
                    offers = offers.OrderByDescending(o => o.Price).ToList();
                    break;
                default:
                    offers = offers.OrderBy(o => o.UserProfile.Rating).ToList();
                    break;
            }
            foreach (var sortItem in model.SortBy)
            {
                if (sortItem.Value == searchInfo.SortBy)
                {
                    sortItem.Selected = true;
                }
                else
                {
                    sortItem.Selected = false;
                }
            }
            if (!string.IsNullOrWhiteSpace(searchInfo.Game))
                offers = offers.Where(o => o.Game.Value == searchInfo.Game).ToList();            
            var offersViewModel = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);
            model.Offers = offersViewModel;
            return View("_OfferList", model);
        }

    }
}
