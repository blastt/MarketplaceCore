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
    public interface IFilterBooleanValueService
    {
        void Delete(FilterBooleanValue filterBooleanValue);

        IEnumerable<FilterBooleanValue> GetAllFilterBooleanValues();
        IEnumerable<FilterBooleanValue> GetAllFilterBooleanValues(Func<IQueryable<FilterBooleanValue>, IIncludableQueryable<FilterBooleanValue, object>> include);
        Task<IList<FilterBooleanValue>> GetAllFilterBooleanValuesAsync();
        Task<IList<FilterBooleanValue>> GetAllFilterBooleanValuesAsync(Func<IQueryable<FilterBooleanValue>, IIncludableQueryable<FilterBooleanValue, object>> include);
        IEnumerable<FilterBooleanValue> GetFilterBooleanValues(Expression<Func<FilterBooleanValue, bool>> where, Func<IQueryable<FilterBooleanValue>, IIncludableQueryable<FilterBooleanValue, object>> include);
        Task<IList<FilterBooleanValue>> GetFilterBooleanValuesAsync(Expression<Func<FilterBooleanValue, bool>> where, Func<IQueryable<FilterBooleanValue>, IIncludableQueryable<FilterBooleanValue, object>> include);

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

        public IEnumerable<FilterBooleanValue> GetAllFilterBooleanValues(Func<IQueryable<FilterBooleanValue>, IIncludableQueryable<FilterBooleanValue, object>> include)
        {
            var filterBooleanValue = filterBooleanValueRepository.GetAll(include);
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
        public async Task<IList<FilterBooleanValue>> GetAllFilterBooleanValuesAsync(Func<IQueryable<FilterBooleanValue>, IIncludableQueryable<FilterBooleanValue, object>> include)
        {
            return await filterBooleanValueRepository.GetAllAsync(include);
        }


        public IEnumerable<FilterBooleanValue> GetFilterBooleanValues(Expression<Func<FilterBooleanValue, bool>> where, Func<IQueryable<FilterBooleanValue>, IIncludableQueryable<FilterBooleanValue, object>> include)
        {
            var query = filterBooleanValueRepository.GetMany(where, include);
            return query;
        }

        public async Task<IList<FilterBooleanValue>> GetFilterBooleanValuesAsync(Expression<Func<FilterBooleanValue, bool>> where, Func<IQueryable<FilterBooleanValue>, IIncludableQueryable<FilterBooleanValue, object>> include)
        {
            return await filterBooleanValueRepository.GetManyAsync(where, include);
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
