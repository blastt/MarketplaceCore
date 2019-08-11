using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public class Billing : BaseEntity<int>
    {
        public decimal Amount { get; set; }

        public int UserId { get; set; }
        public UserProfile User { get; set; }
    }
}
