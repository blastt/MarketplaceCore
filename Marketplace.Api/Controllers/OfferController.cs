using System.Collections.Generic;
using Marketplace.Model;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Api.ViewModels;
using Marketplace.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        private readonly IMapper mapper;

        private readonly int offerDays = 30;
		private const int pageSize = 4;

		public OfferController(IMapper mapper, IOfferService offerService, IUserProfileService userProfileService, IGameService gameService, IUserService userService, IFilterTextService filterTextService, IFilterRangeService filterRangeService)
		{
            this.mapper = mapper;
            this.offerService = offerService;
			this.userProfileService = userProfileService;
			this.gameService = gameService;
			this.userService = userService;
			this.filterTextService = filterTextService;
			this.filterRangeService = filterRangeService;
		}

		[HttpGet("[action]")]
		public async Task<OfferListViewModel> Get(string game = "csgo")
		{
			var offers = await offerService.GetOffersAsync(o => o.Game.Value == game && o.State == OfferState.Active, source => source.Include(i => i.Game).Include(i => i.UserProfile));
			var model = new OfferListViewModel();
			var gameObj = gameService.GetGameByValue(game);

			model.SearchInfo = new SearchOfferViewModel
			{
				Game = game
			};
			model.GameName = gameObj == null ? "" : gameObj.Name;
			model.Offers = mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers).Take(pageSize);
			model.PageInfo = new PageInfoViewModel
			{
				PageNumber = 1,
				PageSize = pageSize,
				TotalItems = offers.Count()
			};
			return model;
		}

        [HttpGet("[action]")]
        public async Task<CreateOfferViewModel> Create()
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
			return model;
		}

        [HttpPost]
        public IActionResult Create([FromBody]CreateOfferViewModel model)
        {  
            return Ok(model);
        }
    }
}
