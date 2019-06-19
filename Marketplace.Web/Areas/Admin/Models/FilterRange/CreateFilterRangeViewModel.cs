using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Areas.Admin.Models.FilterRange
{
    public class CreateFilterRangeViewModel
    {
        public IList<SelectListItem> Games { get; set; }
        public string Game { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public double From { get; set; }
        public double To { get; set; }
    }
}
