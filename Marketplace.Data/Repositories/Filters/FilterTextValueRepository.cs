using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class FilterTextValueRepository : Repository<FilterTextValue>, IFilterTextValueRepository
    {
        public FilterTextValueRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


    }

    public interface IFilterTextValueRepository : IRepository<FilterTextValue>
    {

    }
}
