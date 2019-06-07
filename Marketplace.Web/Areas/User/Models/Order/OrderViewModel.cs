using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Areas.User.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string OfferHeader { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CurrentStatus { get; set; }
    }
}
