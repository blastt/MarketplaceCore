using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Enums
{
    public enum SecureTransactionPayerViewModel
    {
        Seller = SecureTransactionPayer.Seller,

        Buyer = SecureTransactionPayer.Buyer,

        InTwain = SecureTransactionPayer.InTwain
    }
}
