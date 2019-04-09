using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Infrastructure
{
    public interface IEntityTypeConfiguration
    {
        void Map(ModelBuilder builder);
    }
}
