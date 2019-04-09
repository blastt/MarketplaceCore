using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class DialogConfiguration
    {

        public DialogConfiguration(EntityTypeBuilder<Dialog> builder)
        {
            builder.ToTable("Dialogs");
            builder.HasKey(a => a.Id);
            builder.HasMany(b => b.Messages).WithOne(u => u.Dialog).HasForeignKey(b => b.DialogId);
        }
    }
}
