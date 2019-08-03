using Marketplace.Data.Context;
using Marketplace.Model.Abstracts;
using Marketplace.Model.Interfaces;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(Expression<Func<T, bool>> where);

        T Get(Expression<Func<T, bool>> where);
        Task<T> GetAsync(Expression<Func<T, bool>> where);

        T Get(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        Task<T> GetAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where);

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);

        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();

        IEnumerable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        Task<List<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);

        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        T GetById(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);


    }

    public class Repository<T> : IRepository<T>
        where T : class, IEntity<int>
    {
        #region Properties
        protected ApplicationContext db;
        protected readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ApplicationContext DbContext
        {
            get { return db ?? (db = DbFactory.Init()); }
        }
        #endregion

        protected Repository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(Expression<Func<T, bool>> where)
        {
            dbSet.RemoveRange(dbSet.Where(where));
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.FirstOrDefault<T>(where);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return dbSet.FirstOrDefaultAsync<T>(where);
        }

        public T Get(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            var query = dbSet.Where(where);            
            query = include(query);
            return query.FirstOrDefault<T>();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            var query = dbSet.Where(where);
            query = include(query);
            return query.FirstOrDefaultAsync<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public Task<List<T>> GetAllAsync()
        {
            return dbSet.ToListAsync();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            IQueryable<T> set = dbSet;

            set = include(set);
            return set.ToList();
        }

        public Task<List<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            IQueryable<T> set = dbSet;
            
            set = include(set);
            
            return set.ToListAsync();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public Task<T> GetByIdAsync(int id)
        {
            return dbSet.FindAsync(id);
        }

        public T GetById(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            IQueryable<T> set = dbSet;

            set = include(set);
            return set.SingleOrDefault(i => i.Id == id);
        }

        public Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            IQueryable<T> set = dbSet;

            set = include(set);
            return set.SingleOrDefaultAsync(i => i.Id == id);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToListAsync();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            IQueryable<T> set = dbSet;

            set = include(set);
            return set.ToList();
        }

        public Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            IQueryable<T> set = dbSet;

            set = include(set);
            return set.ToListAsync();
        }

        #region Implementation




        #endregion

    }
}
