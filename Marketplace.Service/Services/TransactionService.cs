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
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAllTransactions();
        IEnumerable<Transaction> GetAllTransactions(Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include);
        IEnumerable<Transaction> GetTransactions(Expression<Func<Transaction, bool>> where, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include);
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<List<Transaction>> GetAllTransactionsAsync(Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include);
        Task<List<Transaction>> GetTransactionsAsync(Expression<Func<Transaction, bool>> where, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include);
        Transaction GetTransaction(int id);
        Task<Transaction> GetTransactionAsync(int id);
        Transaction GetTransaction(int id, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include);
        Task<Transaction> GetTransactionAsync(int id, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include);
        //Task<Transaction> GetTransactionAsync(int id);
        bool TransferMoney(Order order, UserProfile buyer, UserProfile admin);
        Task<IEnumerable<Transaction>> GetTransactionsAsync(Expression<Func<Transaction, bool>> where);
        void CreateTransaction(Transaction transaction);
        void SaveTransaction();
        Task SaveTransactionAsync();
    }

    public class TransactionService : ITransactionService
    {
        private readonly IAccountInfoRepository accountInfosRepository;
        private readonly ITransactionRepository transactionsRepository;
        private readonly IFeedbackRepository feedbacksRepository;
        private readonly IStatusLogRepository statusLogsRepository;
        private readonly IOrderRepository ordersRepository;
        private readonly IUnitOfWork unitOfWork;

        public TransactionService(IOrderRepository ordersRepository, IFeedbackRepository feedbacksRepository, IStatusLogRepository statusLogsRepository, IUnitOfWork unitOfWork, IAccountInfoRepository accountInfosRepository, ITransactionRepository transactionsRepository)
        {
            this.ordersRepository = ordersRepository;
            this.feedbacksRepository = feedbacksRepository;
            this.statusLogsRepository = statusLogsRepository;
            this.transactionsRepository = transactionsRepository;
            this.accountInfosRepository = accountInfosRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ITransactionService Members

        public IEnumerable<Transaction> GetAllTransactions()
        {
            var transactions = transactionsRepository.GetAll();
            return transactions;
        }

        public bool TransferMoney(Order order, UserProfile buyer, UserProfile admin)
        {
            if (buyer.Balance >= order.Sum)
            {
                CreateTransaction(new Transaction
                {
                    Amount = order.Sum,
                    Order = order,
                    Receiver = admin,
                    SenderId = buyer.Id
                });
                buyer.Balance -= order.Sum;
                admin.Balance += order.Sum;
                return true;
            }            
            return false;            
        }

        public IEnumerable<Transaction> GetAllTransactions(Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include)
        {
            var transactions = transactionsRepository.GetAll(include);
            return transactions;
        }

        public IEnumerable<Transaction> GetTransactions(Expression<Func<Transaction, bool>> where, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include)
        {
            var query = transactionsRepository.GetMany(where, include);
            return query;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await transactionsRepository.GetAllAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync(Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include)
        {
            return await transactionsRepository.GetAllAsync(include);
        }

        public async Task<List<Transaction>> GetTransactionsAsync(Expression<Func<Transaction, bool>> where, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include)
        {
            return await transactionsRepository.GetManyAsync(where, include);
        }

        public Transaction GetTransaction(int id)
        {
            var transaction = transactionsRepository.GetById(id);
            return transaction;
        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            return await transactionsRepository.GetByIdAsync(id);
        }

        public Transaction GetTransaction(int id, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include)
        {
            var transaction = transactionsRepository.GetById(id, include);
            return transaction;
        }

        public async Task<Transaction> GetTransactionAsync(int id, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>> include)
        {
            return await transactionsRepository.GetByIdAsync(id, include);
        }

        //public async Task<Transaction> GetTransactionAsync(int id)
        //{
        //    var transaction = await transactionsRepository.GetAsync(o => o.Id == id);
        //    return transaction;
        //}

        public void Delete(Transaction transaction)
        {
            transactionsRepository.Remove(transaction);
        }

        public void CreateTransaction(Transaction transaction)
        {
            transactionsRepository.Add(transaction);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            transactionsRepository.Update(transaction);
        }             

        public void SaveTransaction()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveTransactionAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(Expression<Func<Transaction, bool>> where)
        {
            return await transactionsRepository.GetManyAsync(where);
        }



        #endregion

    }
}
