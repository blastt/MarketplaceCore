using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public class Withdraw : BaseEntity<int>
    {
        public string PaywayName { get; set; }
        public string Details { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }

        public int UserId { get; set; }
        public UserProfile User { get; set; }
    }
}
