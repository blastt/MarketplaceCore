using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Models.Order
{
    public class CreateOrderViewModel
    {
        public int OfferId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string Game { get; set; }
        public string OfferHeader { get; set; }
        public bool SellerPaysMiddleman { get; set; }
        public bool UserCanPayWithBalance { get; set; }
        public SecureTransactionPayer SecureTransactionPayer { get; set; }

        [DataType(DataType.Currency)]
        public decimal SecureTransactionPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal OrderSum { get; set; }
        public int SellerId { get; set; }
        public int? BuyerId { get; set; }
    }
}
