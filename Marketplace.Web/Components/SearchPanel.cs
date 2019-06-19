using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Models;
using Marketplace.Web.Models.Offer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Components
{
    public class SearchPanelViewComponent : ViewComponent
    {
        private readonly IGameService gameService;
        public SearchPanelViewComponent(IGameService gameService)
        {
            this.gameService = gameService;
        }
        public async Task<IViewComponentResult> InvokeAsync(SearchOfferViewModel searchInfo)
        {
            Game game = gameService.GetGameByValue(searchInfo.Game, i => i.RangeFilters, i => i.BooleanFilters, i => i.TextFilters, i => i.TextFilters.Select(tf => tf.PredefinedValues), includes: s => s.Include(i => i.TextFilters));
            if (game != null)
            {
                searchInfo.RangeFilters = Mapper.Map<IList<FilterRange>, IList<FilterRangeViewModel>>(game.RangeFilters);
                searchInfo.BooleanFilters = Mapper.Map<IList<FilterBoolean>, IList<FilterBooleanViewModel>>(game.BooleanFilters);
                searchInfo.TextFilters = Mapper.Map<IList<FilterText>, IList<FilterTextViewModel>>(game.TextFilters);
            }

            return View("_SearchPanelPartial", searchInfo);
        }

    }
}
