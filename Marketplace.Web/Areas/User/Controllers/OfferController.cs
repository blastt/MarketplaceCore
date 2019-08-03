using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Marketplace.Model;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.User.Models.Offer;
using Marketplace.Web.Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Web.Areas.User.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {

        private readonly IOfferService offerService;
        private readonly IGameService gameService;
        private readonly IUserService userService;
        private readonly IUserProfileService userProfileService;
        private readonly int offerDays = 30;
        // GET: Offer
        public OfferController(IOfferService offerService, IUserProfileService userProfileService, IGameService gameService, IUserService userService)
        {
            this.offerService = offerService;
            this.userProfileService = userProfileService;
            this.gameService = gameService;
            this.userService = userService;
        }



        public async Task<ActionResult> Active()
        {
            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            var model = new Models.Offer.OfferListViewModel();
            var offers = await offerService.GetOffersAsync(o => o.UserProfileId == currentUserId && o.State == OfferState.Active, include: source => source.Include(i => i.Game));
            model.CountOfActive = offers.Count;
            model.CountOfClosed = (await offerService.GetOffersAsync(o => o.UserProfileId == currentUserId && o.State == OfferState.Closed)).Count();
            model.CountOfInactive = (await offerService.GetOffersAsync(o => o.UserProfileId == currentUserId && o.State == OfferState.Inactive)).Count();
            model.Offers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);
            return View(model);
        }

        public async Task<ActionResult> Inactive()
        {
            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            var model = new Models.Offer.OfferListViewModel();
            var offers = await offerService.GetOffersAsync(o => o.UserProfileId == currentUserId && o.State == OfferState.Inactive, include: source => source.Include(i => i.Game));
            model.CountOfInactive = offers.Count;
            model.CountOfClosed = (await offerService.GetOffersAsync(o => o.UserProfileId == currentUserId && o.State == OfferState.Closed)).Count();
            model.CountOfActive = (await offerService.GetOffersAsync(o => o.UserProfileId == currentUserId && o.State == OfferState.Active)).Count();
            model.Offers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);
            return View(model);
        }

        public async Task<ActionResult> Closed()
        {
            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            var model = new Models.Offer.OfferListViewModel();
            var offers = await offerService.GetOffersAsync(o => o.UserProfileId == currentUserId && o.State == OfferState.Closed, include: source => source.Include(i => i.Game));
            model.CountOfClosed = offers.Count;
            model.CountOfInactive = (await offerService.GetOffersAsync(o => o.UserProfileId == currentUserId && o.State == OfferState.Inactive)).Count();
            model.CountOfActive = (await offerService.GetOffersAsync(o => o.UserProfileId == currentUserId && o.State == OfferState.Active)).Count();
            model.Offers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);
            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> Deactivate(int? id = 1)
        {
            if (id != null)
            {
                var currentUserId = await userService.GetCurrentUserId(HttpContext.User);
                var offer = await offerService.GetOfferAsync(id.Value, include: source => source.Include(i => i.UserProfile));
                if (offer != null && offer.UserProfileId == currentUserId && offer.State == OfferState.Active)
                {
                    if (offer.JobId != null)
                    {
                        BackgroundJob.Delete(offer.JobId);
                        offer.JobId = null;
                    }
                    offerService.DeactivateOffer(offer, currentUserId);
                    await offerService.SaveOfferAsync();
                    return View();
                }
            }
            return NotFound();
        }
        [Authorize]
        public async Task<ActionResult> Activate(int? id = 1)
        {
            if (id != null)
            {
                var currentUserId = await userService.GetCurrentUserId(HttpContext.User);
                var offer = await offerService.GetOfferAsync(id.Value, include: source => source.Include(i => i.UserProfile));
                if (offer != null && offer.UserProfileId == currentUserId && offer.State == OfferState.Inactive)
                {
                    offer.State = OfferState.Active;
                    offer.CreatedDate = DateTime.Now;
                    offer.DateDeleted = offer.CreatedDate.AddDays(offerDays);
                    await offerService.SaveOfferAsync();

                    if (Request.Method != null)
                        offer.JobId = MarketplaceHangfire.SetDeactivateOfferJob(offer.Id,
                            Url.Action("Activate", "Offer", new { id = offer.Id }, Request.Method),
                            TimeSpan.FromDays(30));
                    await offerService.SaveOfferAsync();
                    return View();
                }
            }
            return NotFound();
        }
        [Authorize]
        public async Task<ActionResult> Delete(int? id = 1)
        {
            if (id != null)
            {
                var currentUserId = await userService.GetCurrentUserId(HttpContext.User);
                var offer = await offerService.GetOfferAsync(id.Value, include: source => source.Include(i => i.UserProfile));
                if (offer != null && offer.UserProfileId == currentUserId && offer.State == OfferState.Inactive)
                {
                    offerService.Delete(offer);
                    await offerService.SaveOfferAsync();
                    return View();
                }
            }
            return NotFound();
        }

    }
}