using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class UserConfiguration
    {

        public UserConfiguration(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.Email)
                .HasMaxLength(256);
            builder.Property(u => u.UserName)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
