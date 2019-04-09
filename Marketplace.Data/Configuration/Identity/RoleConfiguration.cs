using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class RoleConfiguration
    {
        public RoleConfiguration(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
