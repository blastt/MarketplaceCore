using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public enum OrderStatus
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
