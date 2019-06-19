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
    public interface IFilterRangeValueService
    {
        void Delete(FilterRangeValue filterRangeValue);

        IEnumerable<FilterRangeValue> GetAllFilterRangeValues();
        IEnumerable<FilterRangeValue> GetAllFilterRangeValues(params Expression<Func<FilterRangeValue, object>>[] includes);
        Task<IList<FilterRangeValue>> GetAllFilterRangeValuesAsync();
        Task<IList<FilterRangeValue>> GetAllFilterRangeValuesAsync(params Expression<Func<FilterRangeValue, object>>[] includes);
        IEnumerable<FilterRangeValue> GetFilterRangeValues(Expression<Func<FilterRangeValue, bool>> where, params Expression<Func<FilterRangeValue, object>>[] includes);
        Task<IList<FilterRangeValue>> GetFilterRangeValuesAsync(Expression<Func<FilterRangeValue, bool>> where, params Expression<Func<FilterRangeValue, object>>[] includes);

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

        public IEnumerable<FilterRangeValue> GetAllFilterRangeValues(params Expression<Func<FilterRangeValue, object>>[] includes)
        {
            var filterRangeValue = filterRangeValueRepository.GetAll(includes);
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
        public async Task<IList<FilterRangeValue>> GetAllFilterRangeValuesAsync(params Expression<Func<FilterRangeValue, object>>[] includes)
        {
            return await filterRangeValueRepository.GetAllAsync(includes);
        }


        public IEnumerable<FilterRangeValue> GetFilterRangeValues(Expression<Func<FilterRangeValue, bool>> where, params Expression<Func<FilterRangeValue, object>>[] includes)
        {
            var query = filterRangeValueRepository.GetMany(where, includes);
            return query;
        }

        public async Task<IList<FilterRangeValue>> GetFilterRangeValuesAsync(Expression<Func<FilterRangeValue, bool>> where, params Expression<Func<FilterRangeValue, object>>[] includes)
        {
            return await filterRangeValueRepository.GetManyAsync(where, includes);
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
