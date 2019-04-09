﻿using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class StatusLog : BaseEntity<int>
    {
        public DateTime TimeStamp { get; set; } // Время изменения статуса
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int OldStatusId { get; set; }
        public OrderStatus OldStatus { get; set; } // с какого

        public int NewStatusId { get; set; }
        public OrderStatus NewStatus { get; set; } // на какой

    }
}
