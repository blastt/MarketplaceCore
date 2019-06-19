using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class FilterRangeConfiguration
    {
        public FilterRangeConfiguration(EntityTypeBuilder<FilterRange> builder)
        {
            builder.ToTable("FiltersRange");
            builder.HasKey(a => a.Id);
            builder.HasOne(f => f.Game).WithMany(u => u.RangeFilters).HasForeignKey(f => f.GameId);
        }
    }
}
