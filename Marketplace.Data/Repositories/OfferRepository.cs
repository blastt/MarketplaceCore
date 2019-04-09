using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


    }

    public interface IOfferRepository : IRepository<Offer>
    {

    }
}
