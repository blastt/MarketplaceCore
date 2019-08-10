using Marketplace.Api.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Marketplace.Api.ViewModels.Offer
{
    public class CreateOfferViewModel
    {

        public string Game { get; set; }

        public IList<SelectListItem> Games { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        public string AccountLogin { get; set; }

        public bool PersonalAccount { get; set; }

        public DateTime? CreatedAccountDate { get; set; }

        public bool IsBanned { get; set; }

        public string Url { get; set; }

        public decimal Price { get; set; }

        public SecureTransactionPayerViewModel SecureTransactionPayer { get; set; }

        public IList<SelectListItem> SecureTransactionPayers { get; set; }


        public CreateOfferViewModel()
        {
            Games = new List<SelectListItem>();
            SecureTransactionPayers = new List<SelectListItem>();
        }
    }
}
