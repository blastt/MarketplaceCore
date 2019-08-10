using Marketplace.Api.Areas.User.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Areas.User.ViewModels
{
    public class DetailsOrderViewModel
    {
        public int CountOfBuy { get; set; }
        public int CountOfSell { get; set; }

        public int Id { get; set; }

        public OrderStatusViewModel CurrentStatus { get; set; }
        public string OfferHeader { get; set; }

        public decimal OfferPrice { get; set; }
        public string OfferId { get; set; }
        public decimal MiddlemanPrice { get; set; }
        public bool ShowPayButton { get; set; }
        public bool ShowCloseButton { get; set; }
        public bool ShowFeedbackToBuyer { get; set; }
        public bool ShowFeedbackToSeller { get; set; }
        public bool ShowAccountInfo { get; set; }
        public bool ShowConfirm { get; set; }
        public bool ShowProvideData { get; set; }

        public int? ModeratorId { get; set; }
        public string ModeratorName { get; set; }

        public bool SellerFeedbacked { get; set; }
        public bool BuyerFeedbacked { get; set; }

        public int BuyerId { get; set; }
        public string BuyerName { get; set; }

        public int SellerId { get; set; }
        public string SellerName { get; set; }

        public LinkedList<StatusLogViewModel> Logs { get; set; }

        public IList<AccountInfoViewModel> AccountInfos { get; set; }

        public DateTime DateCreated { get; set; }

        public DetailsOrderViewModel()
        {
            Logs = new LinkedList<StatusLogViewModel>();
            AccountInfos = new List<AccountInfoViewModel>();
        }
    }
}
