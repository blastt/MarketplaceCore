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
    public interface IFilterTextService
    {
        void Delete(FilterText filterText);

        IEnumerable<FilterText> GetAllFiltersText();
        IEnumerable<FilterText> GetAllFiltersText(params Expression<Func<FilterText, object>>[] includes);
        Task<IList<FilterText>> GetAllFiltersTextAsync();
        Task<IList<FilterText>> GetAllFiltersTextAsync(params Expression<Func<FilterText, object>>[] includes);
        IEnumerable<FilterText> GetFiltersText(Expression<Func<FilterText, bool>> where, params Expression<Func<FilterText, object>>[] includes);
        Task<IList<FilterText>> GetFiltersTextAsync(Expression<Func<FilterText, bool>> where, params Expression<Func<FilterText, object>>[] includes);

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

        public IEnumerable<FilterText> GetAllFiltersText(params Expression<Func<FilterText, object>>[] includes)
        {
            var filterText = filterTextRepository.GetAll(includes);
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
        public async Task<IList<FilterText>> GetAllFiltersTextAsync(params Expression<Func<FilterText, object>>[] includes)
        {
            return await filterTextRepository.GetAllAsync(includes);
        }


        public IEnumerable<FilterText> GetFiltersText(Expression<Func<FilterText, bool>> where, params Expression<Func<FilterText, object>>[] includes)
        {
            var query = filterTextRepository.GetMany(where, includes);
            return query;
        }

        public async Task<IList<FilterText>> GetFiltersTextAsync(Expression<Func<FilterText, bool>> where, params Expression<Func<FilterText, object>>[] includes)
        {
            return await filterTextRepository.GetManyAsync(where, includes);
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
