using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web
{
    public enum SecureTransactionPayerViewModel
    {
        [Display(Name = "Seller", ResourceType = typeof(SharedResource))]
        Seller = SecureTransactionPayer.Seller,

        [Display(Name = "Buyer", ResourceType = typeof(SharedResource))]
        Buyer = SecureTransactionPayer.Buyer,

        [Display(Name = "InTwain", ResourceType = typeof(SharedResource))]
        InTwain = SecureTransactionPayer.InTwain
    }
}
