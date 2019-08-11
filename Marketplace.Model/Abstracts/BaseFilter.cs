using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Abstracts
{
    public class BaseFilter : BaseEntity<int>
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
