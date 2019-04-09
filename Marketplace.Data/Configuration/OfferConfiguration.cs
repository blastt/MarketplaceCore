using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class OfferConfiguration
    {
        public OfferConfiguration(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offers");
            builder.HasKey(a => a.Id);
            builder.HasOne(o => o.Order).WithOne(o => o.Offer).HasForeignKey<Order>(o => o.OfferId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(o => o.Header).IsRequired().HasMaxLength(100);
            builder.Property(o => o.MiddlemanPrice).ValueGeneratedOnAddOrUpdate();
            builder.Property(o => o.Discription).IsRequired().HasMaxLength(1000);
            builder.Property(o => o.AccountLogin).IsRequired().HasMaxLength(50);
        }
    }
}
