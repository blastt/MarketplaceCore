using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class FilterBooleanRepository : Repository<FilterBoolean>, IFilterBooleanRepository
    {
        public FilterBooleanRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


    }

    public interface IFilterBooleanRepository : IRepository<FilterBoolean>
    {

    }
}
