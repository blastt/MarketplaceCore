using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Services
{
    public interface IGameService
    {
        void Delete(Game game);

        IEnumerable<Game> GetAllGames();
        Task<List<Game>> GetAllGamesAsync();

        IEnumerable<Game> GetGames(Expression<Func<Game, bool>> where, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include);
        Task<List<Game>> GetGamesAsync(Expression<Func<Game, bool>> where, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include);

        Game GetGame(int id);
        Game GetGameByValue(string name);

        Game GetGameByValue(string name, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include);
        void CreateGame(Game message);
        void SaveGame();
        Task SaveGameAsync();
    }

    public class GameService : IGameService
    {
        private readonly IGameRepository gamesRepository;
        private readonly IUnitOfWork unitOfWork;

        public GameService(IGameRepository gamesRepository, IUnitOfWork unitOfWork)
        {
            this.gamesRepository = gamesRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IGameService Members

        public IEnumerable<Game> GetAllGames()
        {
            var games = gamesRepository.GetAll();
            return games;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await gamesRepository.GetAllAsync();
        }


        public IEnumerable<Game> GetGames(Expression<Func<Game, bool>> where, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include)
        {
            var query = gamesRepository.GetMany(where, include);
            return query;
        }

        public async Task<List<Game>> GetGamesAsync(Expression<Func<Game, bool>> where, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include)
        {
            return await gamesRepository.GetManyAsync(where, include);
        }

        public Game GetGame(int id)
        {
            var game = gamesRepository.GetById(id);
            return game;
        }


        public void CreateGame(Game game)
        {
            gamesRepository.Add(game);
        }

        public void Delete(Game game)
        {
            gamesRepository.Remove(game);
        }

        public void SaveGame()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveGameAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        public Game GetGameByValue(string name)
        {
            return gamesRepository.GetGameByValue(name);
        }

        public Game GetGameByValue(string name, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include)
        {
            var query = gamesRepository.GetGameByValue(name, include);
            return query;
        }


        #endregion

    }
}
