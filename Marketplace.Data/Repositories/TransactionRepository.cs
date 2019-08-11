using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace Marketplace.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


    }

    public interface ITransactionRepository : IRepository<Transaction>
    {

    }
}
