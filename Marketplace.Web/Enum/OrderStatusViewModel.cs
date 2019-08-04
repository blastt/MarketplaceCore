using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web
{
    public enum OrderStatusViewModel
    {
        [Display(Name = "OrderCreating", ResourceType = typeof(SharedResource))]
        OrderCreating,
        [Display(Name = "BuyerPaying", ResourceType = typeof(SharedResource))]
        BuyerPaying,
        [Display(Name = "OrderCreating", ResourceType = typeof(SharedResource))]
        MiddlemanFinding,
        [Display(Name = "MiddlemanFinding", ResourceType = typeof(SharedResource))]
        SellerProviding,
        [Display(Name = "SellerProviding", ResourceType = typeof(SharedResource))]
        MiddlemanChecking,
        [Display(Name = "MiddlemanChecking", ResourceType = typeof(SharedResource))]
        BuyerConfirming,
        [Display(Name = "BuyerConfirming", ResourceType = typeof(SharedResource))]
        PayingToSeller,
        [Display(Name = "PayingToSeller", ResourceType = typeof(SharedResource))]
        ClosedSuccessfully,
        [Display(Name = "ClosedSuccessfully", ResourceType = typeof(SharedResource))]
        BuyerClosed,
        [Display(Name = "BuyerClosed", ResourceType = typeof(SharedResource))]
        SellerClosed,
        [Display(Name = "SellerClosed", ResourceType = typeof(SharedResource))]
        MiddlemanClosed,
        [Display(Name = "MiddlemanClosed", ResourceType = typeof(SharedResource))]
        ClosedAutomatically,
        [Display(Name = "ClosedAutomatically", ResourceType = typeof(SharedResource))]
        AbortedByBuyer,
        [Display(Name = "AbortedByBuyer", ResourceType = typeof(SharedResource))]
        MiddlemanBackingAccount
    }
}
