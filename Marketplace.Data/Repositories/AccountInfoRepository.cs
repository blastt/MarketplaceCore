using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class AccountInfoRepository : Repository<AccountInfo>, IAccountInfoRepository
    {

        public AccountInfoRepository(IDbFactory dbFactory)
            : base(dbFactory) { }




    }

    public interface IAccountInfoRepository : IRepository<AccountInfo>
    {
    }
}
