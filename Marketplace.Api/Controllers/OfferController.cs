using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Api.ViewModels.Offer;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers
{
    [Route("api/[controller]")]
    public class OfferController : Controller
    {
        private readonly IOfferService offerService;
        private readonly IFilterTextService filterTextService;
        private readonly IFilterRangeService filterRangeService;
        private readonly IGameService gameService;
        private readonly IUserProfileService userProfileService;
        private readonly IUserService userService;
        private readonly int offerDays = 30;
        private const int pageSize = 4;

        public OfferController(IOfferService offerService, IUserProfileService userProfileService, IGameService gameService, IUserService userService, IFilterTextService filterTextService, IFilterRangeService filterRangeService)
        {
            this.offerService = offerService;
            this.userProfileService = userProfileService;
            this.gameService = gameService;
            this.userService = userService;
            this.filterTextService = filterTextService;
            this.filterRangeService = filterRangeService;
        }

        [HttpGet("[action]")]
        public IEnumerable<Offer> Get()
        {
            var offers = offerService.GetAllOffers();
            IList<string> s = new List<string>() { "l", "d,", "ds" };

            return offers;
            
        }

        [HttpGet("[action]")]
        public CreateOfferViewModel Create()
        {
            var model = new CreateOfferViewModel();
            return model;
        }
        [HttpPost]
        public IActionResult Create([FromBody]CreateOfferViewModel model)
        {  
            return Ok(model);
        }
    }
}