using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Models;
using Marketplace.Web.Models.Offer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            Game game = gameService.GetGameByValue(searchInfo.Game,include: source => source.Include(i => i.RangeFilters).Include(i => i.BooleanFilters).Include(i => i.TextFilters).Include(i => i.TextFilters).ThenInclude(tf => tf.PredefinedValues));
            if (game != null)
            {
                searchInfo.FilterList.RangeFilters = Mapper.Map<IList<FilterRange>, IList<FilterRangeViewModel>>(game.RangeFilters);
                searchInfo.FilterList.BooleanFilters = Mapper.Map<IList<FilterBoolean>, IList<FilterBooleanViewModel>>(game.BooleanFilters);
                searchInfo.FilterList.TextFilters = Mapper.Map<IList<FilterText>, IList<FilterTextViewModel>>(game.TextFilters);
            }

            return View("_SearchPanelPartial", searchInfo);
        }

    }
}
