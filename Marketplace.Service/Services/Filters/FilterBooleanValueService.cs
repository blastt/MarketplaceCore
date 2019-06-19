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
    public interface IFilterBooleanValueService
    {
        void Delete(FilterBooleanValue filterBooleanValue);

        IEnumerable<FilterBooleanValue> GetAllFilterBooleanValues();
        IEnumerable<FilterBooleanValue> GetAllFilterBooleanValues(params Expression<Func<FilterBooleanValue, object>>[] includes);
        Task<IList<FilterBooleanValue>> GetAllFilterBooleanValuesAsync();
        Task<IList<FilterBooleanValue>> GetAllFilterBooleanValuesAsync(params Expression<Func<FilterBooleanValue, object>>[] includes);
        IEnumerable<FilterBooleanValue> GetFilterBooleanValues(Expression<Func<FilterBooleanValue, bool>> where, params Expression<Func<FilterBooleanValue, object>>[] includes);
        Task<IList<FilterBooleanValue>> GetFilterBooleanValuesAsync(Expression<Func<FilterBooleanValue, bool>> where, params Expression<Func<FilterBooleanValue, object>>[] includes);

        void CreateFilterBooleanValue(FilterBooleanValue filterBooleanValue);
        void SaveFilterBooleanValue();
        Task SaveFilterBooleanValueAsync();
    }
    public class FilterBooleanValueService : IFilterBooleanValueService
    {
        private readonly IFilterBooleanValueRepository filterBooleanValueRepository;
        private readonly IUnitOfWork unitOfWork;

        public FilterBooleanValueService(IFilterBooleanValueRepository filterBooleanValueRepository, IUnitOfWork unitOfWork)
        {
            this.filterBooleanValueRepository = filterBooleanValueRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateFilterBooleanValue(FilterBooleanValue filterBooleanValue)
        {
            filterBooleanValueRepository.Add(filterBooleanValue);
        }

        public void Delete(FilterBooleanValue filterBooleanValue)
        {
            filterBooleanValueRepository.Remove(filterBooleanValue);
        }

        public IEnumerable<FilterBooleanValue> GetAllFilterBooleanValues(params Expression<Func<FilterBooleanValue, object>>[] includes)
        {
            var filterBooleanValue = filterBooleanValueRepository.GetAll(includes);
            return filterBooleanValue;
        }

        public IEnumerable<FilterBooleanValue> GetAllFilterBooleanValues()
        {
            var filterBooleanValues = filterBooleanValueRepository.GetAll();
            return filterBooleanValues;
        }

        public async Task<IList<FilterBooleanValue>> GetAllFilterBooleanValuesAsync()
        {
            return await filterBooleanValueRepository.GetAllAsync();
        }
        public async Task<IList<FilterBooleanValue>> GetAllFilterBooleanValuesAsync(params Expression<Func<FilterBooleanValue, object>>[] includes)
        {
            return await filterBooleanValueRepository.GetAllAsync(includes);
        }


        public IEnumerable<FilterBooleanValue> GetFilterBooleanValues(Expression<Func<FilterBooleanValue, bool>> where, params Expression<Func<FilterBooleanValue, object>>[] includes)
        {
            var query = filterBooleanValueRepository.GetMany(where, includes);
            return query;
        }

        public async Task<IList<FilterBooleanValue>> GetFilterBooleanValuesAsync(Expression<Func<FilterBooleanValue, bool>> where, params Expression<Func<FilterBooleanValue, object>>[] includes)
        {
            return await filterBooleanValueRepository.GetManyAsync(where, includes);
        }

        public void SaveFilterBooleanValue()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveFilterBooleanValueAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }
    }
}
