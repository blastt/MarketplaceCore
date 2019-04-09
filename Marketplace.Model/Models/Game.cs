using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class Game : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public string ImagePath { get; set; }

        public int Rank { get; set; } // порядковый номер среди рангов

        public ICollection<Offer> Offers { get; set; }
    }
}
