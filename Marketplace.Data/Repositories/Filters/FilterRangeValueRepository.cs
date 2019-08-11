using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class FilterRangeValueRepository : Repository<FilterRangeValue>, IFilterRangeValueRepository
    {
        public FilterRangeValueRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


    }

    public interface IFilterRangeValueRepository : IRepository<FilterRangeValue>
    {

    }
}
