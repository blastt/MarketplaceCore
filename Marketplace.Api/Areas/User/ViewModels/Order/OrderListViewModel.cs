using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Areas.User.ViewModels
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}
