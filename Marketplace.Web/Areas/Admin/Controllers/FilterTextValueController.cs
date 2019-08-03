using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var game = gameService.GetGameByValue(model.Game, include: source => source.Include(i => i.TextFilters).ThenInclude(tf => tf.FilterTextValue));
                if (game != null)
                {
                    var filterText = game.TextFilters.FirstOrDefault(f => f.Value == model.FilterText);
                    if (filterText != null)
                    {
                        var filterTextValue = Mapper.Map<CreateFilterTextValueViewModel, FilterTextValue>(model); //TODO
                        filterText.PredefinedValues.Add(filterTextValue);
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