namespace Marketplace.Api.ViewModels
{
	public class SearchOfferViewModel
	{
		public string Game { get; set; }
		public string SearchString { get; set; }
		public bool SerchInDescription { get; set; }
		public bool OnlineOnly { get; set; }
		public decimal PriceFrom { get; set; }
		public string SortBy { get; set; }
		public decimal PriceTo { get; set; }
		public bool PersonalAccount { get; set; }
		public bool IsBanned { get; set; }

		public FilterListViewModel FilterList { get; set; }


		//public IList<FilterTextViewModel> TextFilters { get; set; }
		//public IList<FilterRangeViewModel> RangeFilters { get; set; }
		//public IList<FilterBooleanViewModel> BooleanFilters { get; set; }

		public int Page { get; set; }

		public SearchOfferViewModel()
		{
			Page = 1;
			FilterList = new FilterListViewModel();
			//TextFilters = new List<FilterTextViewModel>();
			//RangeFilters = new List<FilterRangeViewModel>();
			//BooleanFilters = new List<FilterBooleanViewModel>();
		}
		public SearchOfferViewModel(int page, string game)
		{
			Page = page;
			Game = game;
		}
	}
}
