using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Models
{
    public class FilterTextViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public IList<FilterTextValueViewModel> PredefinedValues { get; set; }
    }
}
