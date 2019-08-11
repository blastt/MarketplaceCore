using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public class Transaction : BaseEntity<int>
    {
        public decimal Amount { get; set; }

        public int SenderId { get; set; }
        public UserProfile Sender { get; set; }

        public int ReceiverId { get; set; }
        public UserProfile Receiver { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
