using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.Admin.Models.FilterText;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Areas.Admin.Controllers
{
    public class FilterTextController : Controller
    {
        readonly private IFilterTextService filterTextService;
        readonly private IGameService gameService;
        public FilterTextController(IFilterTextService filterTextService, IGameService gameService)
        {
            this.filterTextService = filterTextService;
            this.gameService = gameService;
        }
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFilterTextViewModel model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Game> games = await gameService.GetAllGamesAsync();
                var game = games.FirstOrDefault(g => g.Value == model.Game);
                if (game != null)
                {
                    var filterText = Mapper.Map<CreateFilterTextViewModel, FilterText>(model);
                    filterTextService.CreateFilterText(filterText);
                    await filterTextService.SaveFilterTextAsync();
                    return View();
                }
            }
            return View();
        }
    }
}