using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class StatusLogRepository : Repository<StatusLog>, IStatusLogRepository
    {
        public StatusLogRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


    }

    public interface IStatusLogRepository : IRepository<StatusLog>
    {

    }
}
