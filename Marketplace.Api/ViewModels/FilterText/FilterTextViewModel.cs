using System.Collections.Generic;

namespace Marketplace.Api.ViewModels
{
	public class FilterTextViewModel
	{
		public string Name { get; set; }
		public string Value { get; set; }

		public IList<FilterTextValueViewModel> PredefinedValues { get; set; }
	}
}
