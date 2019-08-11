using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class FilterTextRepository : Repository<FilterText>, IFilterTextRepository
    {
        public FilterTextRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


    }

    public interface IFilterTextRepository : IRepository<FilterText>
    {

    }
}
