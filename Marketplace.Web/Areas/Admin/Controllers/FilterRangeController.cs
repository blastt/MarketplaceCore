using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Areas.Admin.Controllers
{
    public class FilterRangeController : Controller
    {

        readonly private IFilterRangeService filterRangeService;
        readonly private IGameService gameService;
        public FilterRangeController(IFilterRangeService filterRangeService, IGameService gameService)
        {
            this.filterRangeService = filterRangeService;
            this.gameService = gameService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFilterRangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var game = gameService.GetGameByValue(model.Game);
                if (game != null)
                {
                    var filterRange = Mapper.Map<CreateFilterRangeViewModel, FilterRange>(model);
                    filterRange.Game = game;
                    filterRangeService.CreateFilterRange(filterRange);
                    await filterRangeService.SaveFilterRangeAsync();
                    return View();
                }
            }
            return View();
        }
    }
}