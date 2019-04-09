using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class DialogRepository : Repository<Dialog>, IDialogRepository
    {
        public DialogRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


    }

    public interface IDialogRepository : IRepository<Dialog>
    {

    }
}
