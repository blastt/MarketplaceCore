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
    public interface IFilterBooleanService
    {
        void Delete(FilterBoolean filterBoolean);

        IEnumerable<FilterBoolean> GetAllFiltersBoolean();
        IEnumerable<FilterBoolean> GetAllFiltersBoolean(Func<IQueryable<FilterBoolean>, IIncludableQueryable<FilterBoolean, object>> include);
        Task<IList<FilterBoolean>> GetAllFiltersBooleanAsync();
        Task<IList<FilterBoolean>> GetAllFiltersBooleanAsync(Func<IQueryable<FilterBoolean>, IIncludableQueryable<FilterBoolean, object>> include);
        IEnumerable<FilterBoolean> GetFiltersBoolean(Expression<Func<FilterBoolean, bool>> where, Func<IQueryable<FilterBoolean>, IIncludableQueryable<FilterBoolean, object>> include);
        Task<IList<FilterBoolean>> GetFiltersBooleanAsync(Expression<Func<FilterBoolean, bool>> where, Func<IQueryable<FilterBoolean>, IIncludableQueryable<FilterBoolean, object>> include);

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

        public IEnumerable<FilterBoolean> GetAllFiltersBoolean(Func<IQueryable<FilterBoolean>, IIncludableQueryable<FilterBoolean, object>> include)
        {
            var filterBoolean = filterBooleanRepository.GetAll(include);
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
        public async Task<IList<FilterBoolean>> GetAllFiltersBooleanAsync(Func<IQueryable<FilterBoolean>, IIncludableQueryable<FilterBoolean, object>> include)
        {
            return await filterBooleanRepository.GetAllAsync(include);
        }


        public IEnumerable<FilterBoolean> GetFiltersBoolean(Expression<Func<FilterBoolean, bool>> where, Func<IQueryable<FilterBoolean>, IIncludableQueryable<FilterBoolean, object>> include)
        {
            var query = filterBooleanRepository.GetMany(where, include);
            return query;
        }

        public async Task<IList<FilterBoolean>> GetFiltersBooleanAsync(Expression<Func<FilterBoolean, bool>> where, Func<IQueryable<FilterBoolean>, IIncludableQueryable<FilterBoolean, object>> include)
        {
            return await filterBooleanRepository.GetManyAsync(where, include);
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
