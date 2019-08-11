using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class FilterBooleanConfiguration
    {
        public FilterBooleanConfiguration(EntityTypeBuilder<FilterBoolean> builder)
        {
            builder.ToTable("FiltersBoolean");
            builder.HasKey(a => a.Id);
            
            builder.HasOne(f => f.Game).WithMany(u => u.BooleanFilters).HasForeignKey(f => f.GameId);
        }
    }
}
