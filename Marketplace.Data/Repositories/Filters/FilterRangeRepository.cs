using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class FilterRangeRepository : Repository<FilterRange>, IFilterRangeRepository
    {
        public FilterRangeRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


    }

    public interface IFilterRangeRepository : IRepository<FilterRange>
    {

    }
}
