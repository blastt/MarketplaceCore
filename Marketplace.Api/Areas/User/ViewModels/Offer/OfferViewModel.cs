using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Areas.User.ViewModels
{
    public class OfferViewModel
    {
        public int? Id { get; set; }

        public string Game { get; set; }

        public string Header { get; set; }

        public string Discription { get; set; }

        public bool PersonalAccount { get; set; }

        public int? CountOfGames { get; set; }

        public DateTime? CreatedAccountDate { get; set; }

        public bool IsBanned { get; set; }

        public string Url { get; set; }

        public string ShortUrl { get; set; }

        public decimal Price { get; set; }

        public bool SellerPaysMiddleman { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DeletedDate { get; set; }
    }
}
