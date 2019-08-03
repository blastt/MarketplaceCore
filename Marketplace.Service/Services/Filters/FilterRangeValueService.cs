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
    public interface IFilterRangeValueService
    {
        void Delete(FilterRangeValue filterRangeValue);

        IEnumerable<FilterRangeValue> GetAllFilterRangeValues();
        IEnumerable<FilterRangeValue> GetAllFilterRangeValues(Func<IQueryable<FilterRangeValue>, IIncludableQueryable<FilterRangeValue, object>> include);
        Task<IList<FilterRangeValue>> GetAllFilterRangeValuesAsync();
        Task<IList<FilterRangeValue>> GetAllFilterRangeValuesAsync(Func<IQueryable<FilterRangeValue>, IIncludableQueryable<FilterRangeValue, object>> include);
        IEnumerable<FilterRangeValue> GetFilterRangeValues(Expression<Func<FilterRangeValue, bool>> where, Func<IQueryable<FilterRangeValue>, IIncludableQueryable<FilterRangeValue, object>> include);
        Task<IList<FilterRangeValue>> GetFilterRangeValuesAsync(Expression<Func<FilterRangeValue, bool>> where, Func<IQueryable<FilterRangeValue>, IIncludableQueryable<FilterRangeValue, object>> include);

        void CreateFilterRangeValue(FilterRangeValue filterRangeValue);
        void SaveFilterRangeValue();
        Task SaveFilterRangeValueAsync();
    }
    public class FilterRangeValueService : IFilterRangeValueService
    {
        private readonly IFilterRangeValueRepository filterRangeValueRepository;
        private readonly IUnitOfWork unitOfWork;

        public FilterRangeValueService(IFilterRangeValueRepository filterRangeValueRepository, IUnitOfWork unitOfWork)
        {
            this.filterRangeValueRepository = filterRangeValueRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateFilterRangeValue(FilterRangeValue filterRangeValue)
        {
            filterRangeValueRepository.Add(filterRangeValue);
        }

        public void Delete(FilterRangeValue filterRangeValue)
        {
            filterRangeValueRepository.Remove(filterRangeValue);
        }

        public IEnumerable<FilterRangeValue> GetAllFilterRangeValues(Func<IQueryable<FilterRangeValue>, IIncludableQueryable<FilterRangeValue, object>> include)
        {
            var filterRangeValue = filterRangeValueRepository.GetAll(include);
            return filterRangeValue;
        }

        public IEnumerable<FilterRangeValue> GetAllFilterRangeValues()
        {
            var filterRangeValues = filterRangeValueRepository.GetAll();
            return filterRangeValues;
        }

        public async Task<IList<FilterRangeValue>> GetAllFilterRangeValuesAsync()
        {
            return await filterRangeValueRepository.GetAllAsync();
        }
        public async Task<IList<FilterRangeValue>> GetAllFilterRangeValuesAsync(Func<IQueryable<FilterRangeValue>, IIncludableQueryable<FilterRangeValue, object>> include)
        {
            return await filterRangeValueRepository.GetAllAsync(include);
        }


        public IEnumerable<FilterRangeValue> GetFilterRangeValues(Expression<Func<FilterRangeValue, bool>> where, Func<IQueryable<FilterRangeValue>, IIncludableQueryable<FilterRangeValue, object>> include)
        {
            var query = filterRangeValueRepository.GetMany(where, include);
            return query;
        }

        public async Task<IList<FilterRangeValue>> GetFilterRangeValuesAsync(Expression<Func<FilterRangeValue, bool>> where, Func<IQueryable<FilterRangeValue>, IIncludableQueryable<FilterRangeValue, object>> include)
        {
            return await filterRangeValueRepository.GetManyAsync(where, include);
        }

        public void SaveFilterRangeValue()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveFilterRangeValueAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }
    }
}
