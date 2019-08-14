using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Model;
using Marketplace.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {

        private readonly IGameService gameService;
        private const string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet("[action]")]
        public Dictionary<char, Dictionary<string, string>> GetGropedGames()
        {
            char[] letters = alpha.ToCharArray();

            var games = gameService.GetAllGames();
            var gropedGames = games.GroupBy(g => g.Name[0]);
            var gamesGrouped = new Dictionary<char, Dictionary<string, string>>();
            foreach (char letter in letters)
            {
                gamesGrouped.Add(letter, games.Where(m => m.Name[0] == letter).ToDictionary(x => x.Value, x => x.Name));
            }
            return gamesGrouped;
        }
    }
}