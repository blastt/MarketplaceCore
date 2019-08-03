using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Services
{
    public interface IFilterRangeService
    {
        void Delete(FilterRange filterRange);

        IEnumerable<FilterRange> GetAllFiltersRange();
        IEnumerable<FilterRange> GetAllFiltersRange(Func<IQueryable<FilterRange>, IIncludableQueryable<FilterRange, object>> include);
        Task<IList<FilterRange>> GetAllFiltersRangeAsync();
        Task<IList<FilterRange>> GetAllFiltersRangeAsync(Func<IQueryable<FilterRange>, IIncludableQueryable<FilterRange, object>> include);
        IEnumerable<FilterRange> GetFiltersRange(Expression<Func<FilterRange, bool>> where, Func<IQueryable<FilterRange>, IIncludableQueryable<FilterRange, object>> include);
        Task<IList<FilterRange>> GetFiltersRangeAsync(Expression<Func<FilterRange, bool>> where, Func<IQueryable<FilterRange>, IIncludableQueryable<FilterRange, object>> include);

        void CreateFilterRange(FilterRange filterRange);
        void SaveFilterRange();
        Task SaveFilterRangeAsync();
    }
    public class FilterRangeService : IFilterRangeService
    {
        private readonly IFilterRangeRepository filterRangeRepository;
        private readonly IUnitOfWork unitOfWork;

        public FilterRangeService(IFilterRangeRepository filterRangeRepository, IUnitOfWork unitOfWork)
        {
            this.filterRangeRepository = filterRangeRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateFilterRange(FilterRange filterRange)
        {
            filterRangeRepository.Add(filterRange);
        }

        public void Delete(FilterRange filterRange)
        {
            filterRangeRepository.Remove(filterRange);
        }

        public IEnumerable<FilterRange> GetAllFiltersRange(Func<IQueryable<FilterRange>, IIncludableQueryable<FilterRange, object>> include)
        {
            var filterRange = filterRangeRepository.GetAll(include);
            return filterRange;
        }

        public IEnumerable<FilterRange> GetAllFiltersRange()
        {
            var filtersRange = filterRangeRepository.GetAll();
            return filtersRange;
        }

        public async Task<IList<FilterRange>> GetAllFiltersRangeAsync()
        {
            return await filterRangeRepository.GetAllAsync();
        }
        public async Task<IList<FilterRange>> GetAllFiltersRangeAsync(Func<IQueryable<FilterRange>, IIncludableQueryable<FilterRange, object>> include)
        {
            return await filterRangeRepository.GetAllAsync(include);
        }


        public IEnumerable<FilterRange> GetFiltersRange(Expression<Func<FilterRange, bool>> where, Func<IQueryable<FilterRange>, IIncludableQueryable<FilterRange, object>> include)
        {
            var query = filterRangeRepository.GetMany(where, include);
            return query;
        }

        public async Task<IList<FilterRange>> GetFiltersRangeAsync(Expression<Func<FilterRange, bool>> where, Func<IQueryable<FilterRange>, IIncludableQueryable<FilterRange, object>> include)
        {
            return await filterRangeRepository.GetManyAsync(where, include);
        }

        public void SaveFilterRange()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveFilterRangeAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }
    }
}
