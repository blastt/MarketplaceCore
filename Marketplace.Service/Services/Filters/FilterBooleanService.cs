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
    public interface IFilterBooleanService
    {
        void Delete(FilterBoolean filterBoolean);

        IEnumerable<FilterBoolean> GetAllFiltersBoolean();
        IEnumerable<FilterBoolean> GetAllFiltersBoolean(params Expression<Func<FilterBoolean, object>>[] includes);
        Task<IList<FilterBoolean>> GetAllFiltersBooleanAsync();
        Task<IList<FilterBoolean>> GetAllFiltersBooleanAsync(params Expression<Func<FilterBoolean, object>>[] includes);
        IEnumerable<FilterBoolean> GetFiltersBoolean(Expression<Func<FilterBoolean, bool>> where, params Expression<Func<FilterBoolean, object>>[] includes);
        Task<IList<FilterBoolean>> GetFiltersBooleanAsync(Expression<Func<FilterBoolean, bool>> where, params Expression<Func<FilterBoolean, object>>[] includes);

        void CreateFilterBoolean(FilterBoolean filterBoolean);
        void SaveFilterBoolean();
        Task SaveFilterBooleanAsync();
    }
    public class FilterBooleanService : IFilterBooleanService
    {
        private readonly IFilterBooleanRepository filterBooleanRepository;
        private readonly IUnitOfWork unitOfWork;

        public FilterBooleanService(IFilterBooleanRepository filterBooleanRepository, IUnitOfWork unitOfWork)
        {
            this.filterBooleanRepository = filterBooleanRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateFilterBoolean(FilterBoolean filterBoolean)
        {
            filterBooleanRepository.Add(filterBoolean);
        }

        public void Delete(FilterBoolean filterBoolean)
        {
            filterBooleanRepository.Remove(filterBoolean);
        }

        public IEnumerable<FilterBoolean> GetAllFiltersBoolean(params Expression<Func<FilterBoolean, object>>[] includes)
        {
            var filterBoolean = filterBooleanRepository.GetAll(includes);
            return filterBoolean;
        }

        public IEnumerable<FilterBoolean> GetAllFiltersBoolean()
        {
            var filtersBoolean = filterBooleanRepository.GetAll();
            return filtersBoolean;
        }

        public async Task<IList<FilterBoolean>> GetAllFiltersBooleanAsync()
        {
            return await filterBooleanRepository.GetAllAsync();
        }
        public async Task<IList<FilterBoolean>> GetAllFiltersBooleanAsync(params Expression<Func<FilterBoolean, object>>[] includes)
        {
            return await filterBooleanRepository.GetAllAsync(includes);
        }


        public IEnumerable<FilterBoolean> GetFiltersBoolean(Expression<Func<FilterBoolean, bool>> where, params Expression<Func<FilterBoolean, object>>[] includes)
        {
            var query = filterBooleanRepository.GetMany(where, includes);
            return query;
        }

        public async Task<IList<FilterBoolean>> GetFiltersBooleanAsync(Expression<Func<FilterBoolean, bool>> where, params Expression<Func<FilterBoolean, object>>[] includes)
        {
            return await filterBooleanRepository.GetManyAsync(where, includes);
        }

        public void SaveFilterBoolean()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveFilterBooleanAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }
    }
}
