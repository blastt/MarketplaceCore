using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class FilterTextConfiguration
    {
        public FilterTextConfiguration(EntityTypeBuilder<FilterText> builder)
        {
            builder.ToTable("FiltersText");
            builder.HasKey(a => a.Id);
            builder.HasMany(b => b.PredefinedValues).WithOne(u => u.FilterText);
            builder.HasOne(f => f.Game).WithMany(u => u.TextFilters).HasForeignKey(f => f.GameId);
        }
    }
}
