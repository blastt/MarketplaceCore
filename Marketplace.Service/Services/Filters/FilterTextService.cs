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
    public interface IFilterTextService
    {
        void Delete(FilterText filterText);

        IEnumerable<FilterText> GetAllFiltersText();
        IEnumerable<FilterText> GetAllFiltersText(Func<IQueryable<FilterText>, IIncludableQueryable<FilterText, object>> include);
        Task<IList<FilterText>> GetAllFiltersTextAsync();
        Task<IList<FilterText>> GetAllFiltersTextAsync(Func<IQueryable<FilterText>, IIncludableQueryable<FilterText, object>> include);
        IEnumerable<FilterText> GetFiltersText(Expression<Func<FilterText, bool>> where, Func<IQueryable<FilterText>, IIncludableQueryable<FilterText, object>> include);
        Task<IList<FilterText>> GetFiltersTextAsync(Expression<Func<FilterText, bool>> where, Func<IQueryable<FilterText>, IIncludableQueryable<FilterText, object>> include);

        void CreateFilterText(FilterText filterText);
        void SaveFilterText();
        Task SaveFilterTextAsync();
    }
    public class FilterTextService : IFilterTextService
    {
        private readonly IFilterTextRepository filterTextRepository;
        private readonly IUnitOfWork unitOfWork;

        public FilterTextService(IFilterTextRepository filterTextRepository, IUnitOfWork unitOfWork)
        {
            this.filterTextRepository = filterTextRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateFilterText(FilterText filterText)
        {
            filterTextRepository.Add(filterText);
        }

        public void Delete(FilterText filterText)
        {
            filterTextRepository.Remove(filterText);
        }

        public IEnumerable<FilterText> GetAllFiltersText(Func<IQueryable<FilterText>, IIncludableQueryable<FilterText, object>> include)
        {
            var filterText = filterTextRepository.GetAll(include);
            return filterText;
        }

        public IEnumerable<FilterText> GetAllFiltersText()
        {
            var filtersText = filterTextRepository.GetAll();
            return filtersText;
        }

        public async Task<IList<FilterText>> GetAllFiltersTextAsync()
        {
            return await filterTextRepository.GetAllAsync();
        }
        public async Task<IList<FilterText>> GetAllFiltersTextAsync(Func<IQueryable<FilterText>, IIncludableQueryable<FilterText, object>> include)
        {
            return await filterTextRepository.GetAllAsync(include);
        }


        public IEnumerable<FilterText> GetFiltersText(Expression<Func<FilterText, bool>> where, Func<IQueryable<FilterText>, IIncludableQueryable<FilterText, object>> include)
        {
            var query = filterTextRepository.GetMany(where, include);
            return query;
        }

        public async Task<IList<FilterText>> GetFiltersTextAsync(Expression<Func<FilterText, bool>> where, Func<IQueryable<FilterText>, IIncludableQueryable<FilterText, object>> include)
        {
            return await filterTextRepository.GetManyAsync(where, include);
        }

        public void SaveFilterText()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveFilterTextAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }
    }
}
