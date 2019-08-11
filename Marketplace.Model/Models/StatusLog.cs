using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model
{
    public class StatusLog : BaseEntity<int>
    {
        public DateTime TimeStamp { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public StatusLog()
        {
            TimeStamp = DateTime.Now;
        }

    }
}
