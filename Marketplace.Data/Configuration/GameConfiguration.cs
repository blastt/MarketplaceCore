using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class GameConfiguration
    {

        public GameConfiguration(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");
            builder.HasKey(a => a.Id);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Value).IsRequired();
            builder.HasMany(g => g.Offers).WithOne(o => o.Game).HasForeignKey(o => o.GameId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
