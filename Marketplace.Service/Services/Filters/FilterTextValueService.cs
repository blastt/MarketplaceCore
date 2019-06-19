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
    public interface IFilterTextValueService
    {
        void Delete(FilterTextValue filterTextValue);

        IEnumerable<FilterTextValue> GetAllFilterTextValues();
        IEnumerable<FilterTextValue> GetAllFilterTextValues(params Expression<Func<FilterTextValue, object>>[] includes);
        Task<IList<FilterTextValue>> GetAllFilterTextValuesAsync();
        Task<IList<FilterTextValue>> GetAllFilterTextValuesAsync(params Expression<Func<FilterTextValue, object>>[] includes);
        IEnumerable<FilterTextValue> GetFilterTextValues(Expression<Func<FilterTextValue, bool>> where, params Expression<Func<FilterTextValue, object>>[] includes);
        Task<IList<FilterTextValue>> GetFilterTextValuesAsync(Expression<Func<FilterTextValue, bool>> where, params Expression<Func<FilterTextValue, object>>[] includes);

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

        public IEnumerable<FilterTextValue> GetAllFilterTextValues(params Expression<Func<FilterTextValue, object>>[] includes)
        {
            var filterTextValue = filterTextValueRepository.GetAll(includes);
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
        public async Task<IList<FilterTextValue>> GetAllFilterTextValuesAsync(params Expression<Func<FilterTextValue, object>>[] includes)
        {
            return await filterTextValueRepository.GetAllAsync(includes);
        }


        public IEnumerable<FilterTextValue> GetFilterTextValues(Expression<Func<FilterTextValue, bool>> where, params Expression<Func<FilterTextValue, object>>[] includes)
        {
            var query = filterTextValueRepository.GetMany(where, includes);
            return query;
        }

        public async Task<IList<FilterTextValue>> GetFilterTextValuesAsync(Expression<Func<FilterTextValue, bool>> where, params Expression<Func<FilterTextValue, object>>[] includes)
        {
            return await filterTextValueRepository.GetManyAsync(where, includes);
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
