using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{

    internal class FeedbackConfiguration
    {

        public FeedbackConfiguration(EntityTypeBuilder<Feedback> builder)
        {
            builder.ToTable("Feedbacks");
            builder.HasKey(a => a.Id);
            builder.Property(f => f.Comment).IsRequired().HasMaxLength(50);
            builder.HasOne(f => f.Order).WithMany(o => o.Feedbacks).OnDelete(DeleteBehavior.Cascade);
            builder.Property(f => f.Grade).IsRequired();
        }
    }
}
