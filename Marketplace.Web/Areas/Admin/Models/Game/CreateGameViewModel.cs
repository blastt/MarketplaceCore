using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Areas.Admin.Models.Game
{
    public class CreateGameViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public int Rank { get; set; } // порядковый номер среди рангов

    }
}
