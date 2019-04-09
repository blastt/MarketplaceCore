using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class OrderStatusConfiguration
    {
        public OrderStatusConfiguration(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("OrderStatuses");
            builder.HasKey(a => a.Id);
        }
    }
}
