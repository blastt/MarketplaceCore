using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class BillingRepository : Repository<Billing>, IBillingRepository
    {
        public BillingRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

    }

    public interface IBillingRepository : IRepository<Billing>
    {

    }
}
