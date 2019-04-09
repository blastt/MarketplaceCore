using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class StatusLogConfiguration
    {

        public StatusLogConfiguration(EntityTypeBuilder<StatusLog> builder)
        {
            builder.ToTable("StatusLogs");
            builder.HasKey(a => a.Id);
            builder.HasOne(m => m.Order).WithMany(m => m.StatusLogs).HasForeignKey(m => m.OrderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.NewStatus).WithMany(m => m.NewStatusLogs).HasForeignKey(m => m.NewStatusId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.OldStatus).WithMany(m => m.OldStatusLogs).HasForeignKey(m => m.OldStatusId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
