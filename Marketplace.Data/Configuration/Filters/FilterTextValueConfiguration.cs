using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class FilterTextValueConfiguration
    {
        public FilterTextValueConfiguration(EntityTypeBuilder<FilterTextValue> builder)
        {
            builder.ToTable("FilterTextValues");
            builder.HasKey(f => f.Id);



            builder
                .HasMany(bc => bc.OfferFilterTextValues)
                .WithOne(b => b.FilterTextValue)
                .HasForeignKey(bc => bc.FilterTextValueId);
            
            builder.HasOne(b => b.SelectedFilterText).WithOne(u => u.FilterTextValue).HasForeignKey<FilterText>(f => f.FilterTextValueId);
        }
    }
}
