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
            if (!string.IsNullOrWhiteSpace(searchInfo.Game))
                offers = offers.Where(o => o.Game.Value == searchInfo.Game).ToList();            
            var offersViewModel = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);
            model.Offers = offersViewModel;
            return View("_OfferList", model);
        }

    }
}
