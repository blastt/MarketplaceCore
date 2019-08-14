using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.ViewModels
{
    public struct GameGroupedViewModel
    {
        private const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Dictionary<string, Dictionary<string, string>> GroupedGames { get; set; }
    }
}
