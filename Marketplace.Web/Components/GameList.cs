using Marketplace.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Views.GameList.Components
{
    public class GameListViewComponent : ViewComponent
    {
        private readonly IGameService gameService;
        public GameListViewComponent(IGameService gameService)
        {
            this.gameService = gameService;
        }
        public IViewComponentResult Invoke()
        {
            Dictionary<char, List<KeyValuePair<string, string>>> gameNames = new Dictionary<char, List<KeyValuePair<string, string>>>();
            IList<Char> letters = new List<Char>()
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
                'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
                'Y', 'Z'
            };

            var games = gameService.GetAllGames();
            var sortedGames = games.OrderBy(g => g.Name);

            
            foreach (var letter in letters)
            {
                var gamesInLetter = new List<KeyValuePair<string, string>>();
                foreach (var game in sortedGames.Where(g => char.ToLowerInvariant(g.Name.FirstOrDefault()) == char.ToLowerInvariant(letter)))
                {
                    gamesInLetter.Add(new KeyValuePair<string, string>(game.Value, game.Name));
                }
                gameNames.Add(letter, gamesInLetter);
            }
            return View("_GameList", gameNames);
        }

    }
}
