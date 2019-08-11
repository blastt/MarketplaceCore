using Marketplace.Model.Abstracts;
using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public class Game : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public string ImagePath { get; set; }

        public int Rank { get; set; } // порядковый номер среди рангов

        public IList<FilterText> TextFilters { get; set; }
        public IList<FilterRange> RangeFilters { get; set; }
        public IList<FilterBoolean> BooleanFilters { get; set; }
        public ICollection<Offer> Offers { get; set; }

        public Game()
        {
            TextFilters = new List<FilterText>();
            RangeFilters = new List<FilterRange>();
            BooleanFilters = new List<FilterBoolean>();
        }
    }
}
