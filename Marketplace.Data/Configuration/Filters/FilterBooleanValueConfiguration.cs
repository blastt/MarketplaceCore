using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class FilterBooleanValueConfiguration
    {
        public FilterBooleanValueConfiguration(EntityTypeBuilder<FilterBooleanValue> builder)
        {
            builder.ToTable("FilterBooleanValues");
            builder.HasKey(f => f.Id);
            builder.HasOne(f => f.Offer).WithMany(o => o.FilterBooleanValues).HasForeignKey(o => o.OfferId);
            builder.HasOne(b => b.FilterBoolean).WithOne(u => u.FilterBooleanValue).HasForeignKey<FilterBoolean>(f => f.FilterBooleanValueId);
        }
    }
}
