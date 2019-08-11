using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class MessageConfiguration
    {
        public MessageConfiguration(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(a => a.Id);
            builder.Property(m => m.MessageBody).IsRequired().HasMaxLength(200);
        }
    }
}
