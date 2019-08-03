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
    public interface IFilterTextValueService
    {
        void Delete(FilterTextValue filterTextValue);

        IEnumerable<FilterTextValue> GetAllFilterTextValues();
        IEnumerable<FilterTextValue> GetAllFilterTextValues(Func<IQueryable<FilterTextValue>, IIncludableQueryable<FilterTextValue, object>> include);
        Task<IList<FilterTextValue>> GetAllFilterTextValuesAsync();
        Task<IList<FilterTextValue>> GetAllFilterTextValuesAsync(Func<IQueryable<FilterTextValue>, IIncludableQueryable<FilterTextValue, object>> include);
        IEnumerable<FilterTextValue> GetFilterTextValues(Expression<Func<FilterTextValue, bool>> where, Func<IQueryable<FilterTextValue>, IIncludableQueryable<FilterTextValue, object>> include);
        Task<IList<FilterTextValue>> GetFilterTextValuesAsync(Expression<Func<FilterTextValue, bool>> where, Func<IQueryable<FilterTextValue>, IIncludableQueryable<FilterTextValue, object>> include);

        void CreateFilterTextValue(FilterTextValue filterTextValue);
        void SaveFilterTextValue();
        Task SaveFilterTextValueAsync();
    }
    public class FilterTextValueService : IFilterTextValueService
    {
        private readonly IFilterTextValueRepository filterTextValueRepository;
        private readonly IUnitOfWork unitOfWork;

        public FilterTextValueService(IFilterTextValueRepository filterTextValueRepository, IUnitOfWork unitOfWork)
        {
            this.filterTextValueRepository = filterTextValueRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateFilterTextValue(FilterTextValue filterTextValue)
        {
            filterTextValueRepository.Add(filterTextValue);
        }

        public void Delete(FilterTextValue filterTextValue)
        {
            filterTextValueRepository.Remove(filterTextValue);
        }

        public IEnumerable<FilterTextValue> GetAllFilterTextValues(Func<IQueryable<FilterTextValue>, IIncludableQueryable<FilterTextValue, object>> include)
        {
            var filterTextValue = filterTextValueRepository.GetAll(include);
            return filterTextValue;
        }

        public IEnumerable<FilterTextValue> GetAllFilterTextValues()
        {
            var filterTextValues = filterTextValueRepository.GetAll();
            return filterTextValues;
        }

        public async Task<IList<FilterTextValue>> GetAllFilterTextValuesAsync()
        {
            return await filterTextValueRepository.GetAllAsync();
        }
        public async Task<IList<FilterTextValue>> GetAllFilterTextValuesAsync(Func<IQueryable<FilterTextValue>, IIncludableQueryable<FilterTextValue, object>> include)
        {
            return await filterTextValueRepository.GetAllAsync(include);
        }


        public IEnumerable<FilterTextValue> GetFilterTextValues(Expression<Func<FilterTextValue, bool>> where, Func<IQueryable<FilterTextValue>, IIncludableQueryable<FilterTextValue, object>> include)
        {
            var query = filterTextValueRepository.GetMany(where, include);
            return query;
        }

        public async Task<IList<FilterTextValue>> GetFilterTextValuesAsync(Expression<Func<FilterTextValue, bool>> where, Func<IQueryable<FilterTextValue>, IIncludableQueryable<FilterTextValue, object>> include)
        {
            return await filterTextValueRepository.GetManyAsync(where, include);
        }

        public void SaveFilterTextValue()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveFilterTextValueAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }
    }
}
