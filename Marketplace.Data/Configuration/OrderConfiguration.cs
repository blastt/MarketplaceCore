using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class OrderConfiguration
    {
        public OrderConfiguration(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(a => a.Id);
            builder.Property(o => o.WithmiddlemanSum).ValueGeneratedOnAddOrUpdate();            
        }
    }
}
