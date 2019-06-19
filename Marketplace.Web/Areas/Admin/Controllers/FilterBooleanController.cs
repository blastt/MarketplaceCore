using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.Admin.Models.FilterBoolean;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Areas.Admin.Controllers
{
    public class FilterBooleanController : Controller
    {
        readonly private IFilterBooleanService filterBooleanService;
        readonly private IGameService gameService;
        public FilterBooleanController(IFilterBooleanService filterBooleanService, IGameService gameService)
        {
            this.filterBooleanService = filterBooleanService;
            this.gameService = gameService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFilterBooleanViewModel model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Game> games = await gameService.GetAllGamesAsync();
                var game = games.FirstOrDefault(g => g.Value == model.Game);
                if (game != null)
                {
                    var filterBoolean = Mapper.Map<CreateFilterBooleanViewModel, FilterBoolean>(model);
                    filterBooleanService.CreateFilterBoolean(filterBoolean);
                    await filterBooleanService.SaveFilterBooleanAsync();
                    return View();
                }
            }
            return View();
        }
    }
}