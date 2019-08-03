using Marketplace.Model.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Marketplace.Model
{
    public enum SecureTransactionPayer
    {
        Seller,
        Buyer,
        InTwain
    }
}
