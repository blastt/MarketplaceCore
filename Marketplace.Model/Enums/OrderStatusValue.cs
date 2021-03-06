﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Enums
{
    public enum OrderStatusValue
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
