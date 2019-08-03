using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Game GetGameByValue(string name)
        {
            return DbContext.Games.FirstOrDefault(g => g.Value == name);
        }

        public Game GetGameByValue(string name, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include)
        {

            IQueryable<Game> set = DbContext.Games;
            set = include(set);
            return set.SingleOrDefault(i => i.Value == name);
        }

        public async Task<Game> GetGameByValueAsync(string name, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include)
        {
            IQueryable<Game> set = DbContext.Games;

            set = include(set);
            return await set.SingleOrDefaultAsync(i => i.Value == name);
        }

        //public Game GetGameByValueAsNoTracking(string name, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include)
        //{
        //    var query = DbContext.Games.AsNoTracking().Where(g => g.Value == name);
        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }
        //    return query.FirstOrDefault();
        //}
    }

    public interface IGameRepository : IRepository<Game>
    {
        Game GetGameByValue(string userName);
        Game GetGameByValue(string name, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include);
        Task<Game> GetGameByValueAsync(string name, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include);
        //Game GetGameByValueAsNoTracking(string name, params Expression<Func<Game, object>>[] includes);
    }
}
