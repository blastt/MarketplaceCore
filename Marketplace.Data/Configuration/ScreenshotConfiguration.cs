using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Marketplace.Data.Configuration
{
    internal class ScreenshotConfiguration
    {

        public ScreenshotConfiguration(EntityTypeBuilder<Screenshot> builder)
        {
            builder.ToTable("Screenshots");
            builder.HasKey(a => a.Id);
            builder.HasOne(o => o.Offer).WithMany(a => a.Screenshots).HasForeignKey(a => a.OfferId);
        }
    }
}
