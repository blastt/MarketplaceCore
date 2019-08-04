﻿using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class FilterBooleanValueRepository : Repository<FilterBooleanValue>, IFilterBooleanValueRepository
    {
        public FilterBooleanValueRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


    }

    public interface IFilterBooleanValueRepository : IRepository<FilterBooleanValue>
    {

    }
}