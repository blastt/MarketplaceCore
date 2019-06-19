using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Services
{
    public interface IFilterRangeService
    {
        void Delete(FilterRange filterRange);

        IEnumerable<FilterRange> GetAllFiltersRange();
        IEnumerable<FilterRange> GetAllFiltersRange(params Expression<Func<FilterRange, object>>[] includes);
        Task<IList<FilterRange>> GetAllFiltersRangeAsync();
        Task<IList<FilterRange>> GetAllFiltersRangeAsync(params Expression<Func<FilterRange, object>>[] includes);
        IEnumerable<FilterRange> GetFiltersRange(Expression<Func<FilterRange, bool>> where, params Expression<Func<FilterRange, object>>[] includes);
        Task<IList<FilterRange>> GetFiltersRangeAsync(Expression<Func<FilterRange, bool>> where, params Expression<Func<FilterRange, object>>[] includes);

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

        public IEnumerable<FilterRange> GetAllFiltersRange(params Expression<Func<FilterRange, object>>[] includes)
        {
            var filterRange = filterRangeRepository.GetAll(includes);
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
        public async Task<IList<FilterRange>> GetAllFiltersRangeAsync(params Expression<Func<FilterRange, object>>[] includes)
        {
            return await filterRangeRepository.GetAllAsync(includes);
        }


        public IEnumerable<FilterRange> GetFiltersRange(Expression<Func<FilterRange, bool>> where, params Expression<Func<FilterRange, object>>[] includes)
        {
            var query = filterRangeRepository.GetMany(where, includes);
            return query;
        }

        public async Task<IList<FilterRange>> GetFiltersRangeAsync(Expression<Func<FilterRange, bool>> where, params Expression<Func<FilterRange, object>>[] includes)
        {
            return await filterRangeRepository.GetManyAsync(where, includes);
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
