using Marketplace.Api.ViewModels.FilterBoolean;
using System.Collections.Generic;

namespace Marketplace.Api.ViewModels
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
