using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    

    public class OrderStatus : BaseEntity<int>
    {

        public OrderStatus()
        {
            Orders = new List<Order>();
            NewStatusLogs = new List<StatusLog>();
            OldStatusLogs = new List<StatusLog>();
        }

        public string DuringName { get; set; }
        public string FinishedName { get; set; }
        public OrderStatusValue Value { get; set; }


        public IList<Order> Orders { get; set; }

        public IList<StatusLog> NewStatusLogs { get; set; }
        public IList<StatusLog> OldStatusLogs { get; set; }
    }
}
