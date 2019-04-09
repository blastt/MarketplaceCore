using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{

    internal class AccountInfoConfiguration
    {

        public AccountInfoConfiguration(EntityTypeBuilder<AccountInfo> builder)
        {
            builder.ToTable("AccountInfos");
            builder.HasKey(a => a.Id);
            builder.HasOne(o => o.Order).WithMany(a => a.AccountInfos).HasForeignKey(a => a.OrderId);
        }
    }
}
