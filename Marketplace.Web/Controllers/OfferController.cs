using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Models;
using Marketplace.Web.Models.Offer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Marketplace.Web.Hangfire;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.Web.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOfferService offerService;
        private readonly IGameService gameService;
        private readonly IUserProfileService userProfileService;
        private readonly IUserService userService;
        private readonly int offerDays = 30;
        private const int pageSize = 4;

        public OfferController(IOfferService offerService, IUserProfileService userProfileService, IGameService gameService, IUserService userService)
        {
            this.offerService = offerService;
            this.userProfileService = userProfileService;
            this.gameService = gameService;
            this.userService = userService;
        }


        public async Task<ActionResult> List(string game = "csgo")
        {
            var offers = await offerService.GetOffersAsync(o => o.Game.Value == game && o.State == OfferState.active, i => i.Game, i => i.UserProfile);
            var model = new OfferListViewModel();
            var gameObj = gameService.GetGameByValue(game);
            model.SearchInfo = new SearchOfferViewModel
            {
                Game = game
            };
            model.GameName = gameObj == null ? "" : gameObj.Name;
            model.Offers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers).Take(pageSize);
            model.PageInfo = new PageInfoViewModel
            {
                PageNumber = 1,
                PageSize = pageSize,
                TotalItems = offers.Count()
            };
            return View(model);
        }

        public async Task<ActionResult> OfferSearch(SearchOfferViewModel search)
        {
            Sort sort = (Sort)Enum.Parse(typeof(Sort), search.SortBy, true);


            List<Offer> offers = await offerService.GetOffersAsync(o => o.Game.Value == search.Game && o.State == OfferState.active, i => i.Game, i => i.UserProfile);

            if (search.PersonalAccount)
            {
                offers = offers.Where(o => o.PersonalAccount).ToList();
            }

            if (search.IsBanned)
            {
                offers = offers.Where(o => o.IsBanned).ToList();
            }

            //offers = offers.Where(o => search.IsBanned && o.IsBanned).ToList();
            //offers = offers.Where(o => search.Game == o.Game.Value).ToList();

            if (offers.Any() && search.PriceFrom == 0)
            {
                search.PriceFrom = offers.Min(o => o.Price);
            }
            if (offers.Any() && search.PriceTo == 0)
            {
                search.PriceTo = offers.Max(o => o.Price);
            }
            offers = offers.Where(o => search.PriceFrom <= o.Price && search.PriceTo >= o.Price).ToList();
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
            var modelOffers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);

            var model = new OfferListViewModel()
            {
                Offers = modelOffers.Skip((search.Page - 1) * pageSize).Take(pageSize).ToList(),
                PageInfo = new PageInfoViewModel
                {

                    PageNumber = search.Page,
                    PageSize = pageSize,
                    TotalItems = modelOffers.Count()
                }
            };



            foreach (var item in model.SortBy)
            {
                if (item.Value == search.SortBy.ToString())
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
            return PartialView("_OfferTable", model);
        }

        //public JsonResult GetFilters(string game)
        //{
        //    Game gameObj = gameService.GetGameByValue(game);
        //    var filters = gameObj.FilterItems;
        //    return Json("test");
        //}

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var model = new CreateOfferViewModel();
            foreach (var game in (await gameService.GetAllGamesAsync()).OrderBy(g => g.Name))
            {
                model.Games.Add(
                    new SelectListItem
                    {
                        Value = game.Value,
                        Text = game.Name
                    }
                );
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateOfferViewModel model)
        {
            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            Game game = gameService.GetGameByValue(model.Game);
            UserProfile user = await userProfileService.GetUserProfileByIdAsync(currentUserId);
            if (ModelState.IsValid && game != null && user != null)
            {
                var offer = Mapper.Map<CreateOfferViewModel, Offer>(model);
                offer.CreatedDate = offer.CreatedDate.AddDays(offerDays);
                offer.UserProfile = user;
                offer.Game = game;
                offerService.CreateOffer(offer);
                offerService.SaveOffer();
                
                if (Request.Path != null)
                    offer.JobId = Hangfire.Hangfire.SetDeactivateOfferJob(offer.Id,
                        Url.Action("Activate", "Offer", new { id = offer.Id }, Request.Scheme), TimeSpan.FromDays(30));
                offerService.SaveOffer();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return View();
            if (id != null)
            {
                Offer offer = await offerService.GetOfferAsync(id.Value, i => i.UserProfile, i => i.Game);
                if (offer != null)
                {
                    var model = Mapper.Map<Offer, DetailsOfferViewModel>(offer);

                    return View(model);
                }
            }
            return View();
            return NotFound();
        }
    }
}