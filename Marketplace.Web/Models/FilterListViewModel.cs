using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Models
{
    public class FilterListViewModel
    {
        public IList<FilterTextViewModel> TextFilters { get; set; }
        public IList<FilterRangeViewModel> RangeFilters { get; set; }
        public IList<FilterBooleanViewModel> BooleanFilters { get; set; }

        public FilterListViewModel()
        {
            TextFilters = new List<FilterTextViewModel>();
            RangeFilters = new List<FilterRangeViewModel>();
            BooleanFilters = new List<FilterBooleanViewModel>();
        }
    }
}
