using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class WithdrawConfiguration
    {
        public WithdrawConfiguration(EntityTypeBuilder<Withdraw> builder)
        {
            builder.ToTable("Withdraws");
            builder.HasKey(a => a.Id);
            builder.HasOne(b => b.User).WithMany(u => u.Withdraws).HasForeignKey(b => b.UserId);
        }
    }
}
