using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class FilterRangeValueConfiguration
    {
        public FilterRangeValueConfiguration(EntityTypeBuilder<FilterRangeValue> builder)
        {
            builder.ToTable("FilterRangeValues");
            builder.HasKey(f => f.Id);
            builder.HasOne(f => f.Offer).WithMany(o => o.FilterRangeValues).HasForeignKey(o => o.OfferId);
            builder.HasOne(b => b.FilterRange).WithOne(u => u.FilterRangeValue).HasForeignKey<FilterRange>(f => f.FilterRangeValueId);
        }
    }
}
