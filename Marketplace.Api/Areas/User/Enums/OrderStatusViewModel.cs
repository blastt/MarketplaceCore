using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Areas.User.Enums
{
    public enum OrderStatusViewModel
    {
        OrderCreating,
        BuyerPaying,
        MiddlemanFinding,
        SellerProviding,
        MiddlemanChecking,
        BuyerConfirming,
        PayingToSeller,
        ClosedSuccessfully,
        BuyerClosed,
        SellerClosed,
        MiddlemanClosed,
        ClosedAutomatically,
        AbortedByBuyer,
        MiddlemanBackingAccount
    }
}
