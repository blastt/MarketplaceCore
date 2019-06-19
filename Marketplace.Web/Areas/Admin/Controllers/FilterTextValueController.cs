using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.Admin.Models.FilterTextValue;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Areas.Admin.Controllers
{
    public class FilterTextValueController : Controller
    {
        readonly private IFilterTextService filterTextService;
        readonly private IFilterTextValueService filterTextValueService;
        readonly private IGameService gameService;
        public FilterTextValueController(IFilterTextService filterTextService, IFilterTextValueService filterTextValueService, IGameService gameService)
        {
            this.filterTextService = filterTextService;
            this.filterTextValueService = filterTextValueService;
            this.gameService = gameService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFilterTextValueViewModel model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Game> games = await gameService.GetAllGamesAsync();
                var game = games.FirstOrDefault(g => g.Value == model.Game);
                if (game != null)
                {
                    var filterText = game.TextFilters.FirstOrDefault(f => f.FilterTextValue.Value == model.Value);
                    if (filterText != null)
                    {
                        var filterTextValue = Mapper.Map<CreateFilterTextValueViewModel, FilterTextValue>(model); //TODO
                        filterTextValueService.CreateFilterTextValue(filterTextValue);
                        await filterTextValueService.SaveFilterTextValueAsync();
                        return View();
                    }
                }
            }
            return View();
        }
    }
}