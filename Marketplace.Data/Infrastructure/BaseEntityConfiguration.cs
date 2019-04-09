using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Infrastructure
{
    public abstract class BaseEntityConfiguration<TEntityType> : IEntityTypeConfiguration
        where TEntityType : class
    {
        public void Map(ModelBuilder builder)
        {
            InternalConfiguration(builder.Entity<TEntityType>());
        }

        protected abstract void InternalConfiguration(EntityTypeBuilder<TEntityType> builder);
    }
}
