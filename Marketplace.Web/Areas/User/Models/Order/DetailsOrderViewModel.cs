using Marketplace.Model;
using Marketplace.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Web;

namespace Marketplace.Web.Areas.User.Models.Order
{
    public class DetailsOrderViewModel
    {
        public int CountOfBuy { get; set; }
        public int CountOfSell { get; set; }

        public int Id { get; set; }
        //public LinkedList<StatusLog> StatusLogs { get; set; }


        public OrderStatusViewModel CurrentStatus { get; set; }
        public string OfferHeader { get; set; }

        [DataType(DataType.Currency)]
        public decimal OfferPrice { get; set; }
        public string OfferId { get; set; }

        [DataType(DataType.Currency)]
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

        public LinkedList<StatusLog> Logs { get; set; }

        public virtual IList<AccountInfo> AccountInfos { get; set; }

        public DateTime DateCreated { get; set; }

        public DetailsOrderViewModel()
        {
            Logs = new LinkedList<StatusLog>();
            AccountInfos = new List<AccountInfo>();
        }
    }
}
