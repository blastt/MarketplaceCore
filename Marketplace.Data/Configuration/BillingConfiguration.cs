using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class BillingConfiguration
    {

        public BillingConfiguration(EntityTypeBuilder<Billing> builder)
        {
            builder.ToTable("Billings");
            builder.HasKey(a => a.Id);
            builder.HasOne(b => b.User).WithMany(u => u.Billings).HasForeignKey(b => b.UserId);
        }
    }
}
