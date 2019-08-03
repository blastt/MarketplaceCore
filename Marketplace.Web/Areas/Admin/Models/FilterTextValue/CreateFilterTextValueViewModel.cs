using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Areas.Admin.Models
{
    public class CreateFilterTextValueViewModel
    {
        public IList<string> Games { get; set; }
        public string Game { get; set; }
        public string FilterText { get; set; }
        public string FilterTextValue { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
