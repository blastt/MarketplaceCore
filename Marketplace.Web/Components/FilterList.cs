using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Components
{
    public class FilterListViewComponent : ViewComponent
    {
        private readonly IGameService gameService;
        public FilterListViewComponent(IGameService gameService)
        {
            this.gameService = gameService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string game)
        {
            Game gameEntity = gameService.GetGameByValue(game, include: source => source.Include(i => i.RangeFilters).Include(i => i.BooleanFilters).Include(i => i.TextFilters).Include(i => i.TextFilters).ThenInclude(tf => tf.PredefinedValues));

            if (game != null)
            {
                FilterListViewModel model = new FilterListViewModel();
                model.RangeFilters = Mapper.Map<IList<FilterRange>, IList<FilterRangeViewModel>>(gameEntity.RangeFilters);
                model.BooleanFilters = Mapper.Map<IList<FilterBoolean>, IList<FilterBooleanViewModel>>(gameEntity.BooleanFilters);
                model.TextFilters = Mapper.Map<IList<FilterText>, IList<FilterTextViewModel>>(gameEntity.TextFilters);
                return View("_FilterList", model);
            }

            return View("_FilterList");
        }

    }
}
