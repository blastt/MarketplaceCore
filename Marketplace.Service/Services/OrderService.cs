using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Services
{
    enum Closer
    {
        Buyer,
        Seller,
        Middleman,
        Automatically
    }

    public interface IOrderService
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrders(Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include);
        IEnumerable<Order> GetOrders(Expression<Func<Order, bool>> where, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include);
        Task<List<Order>> GetOrdersAsync(Expression<Func<Order, bool>> where, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include = null);

        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrders(Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetAllOrdersAsync(Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include);


        //IEnumerable<Offer> GetCategoryGadgets(string categoryName, string gadgetName = null);
        Order GetOrder(int id);
        Task<Order> GetOrderAsync(int id);
        void DeleteOrder(Order order);
        Order GetOrder(string accountLogin, int? moderatorId, int? sellerId, int? buyerId);
        Order GetOrder(string accountLogin, int? moderatorId, int? sellerId, int? buyerId, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include);
        Order GetOrder(int id, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include);
        Task<Order> GetOrderAsync(string accountLogin, int? moderatorId, int? sellerId, int? buyerId);
        Task<Order> GetOrderAsync(string accountLogin, int? moderatorId, int? sellerId, int? buyerId, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include);
        Task<Order> GetOrderAsync(int id, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include);
        void UpdateOrder(Order order);
        void CreateOrder(Order order);
        bool ConfirmAbortOrder(int orderId, int userId);
        bool AbortOrder(int orderId, int currentUserId);
        bool ConfirmOrderByMiddleman(int orderId, int currentUserId);
        bool CloseOrderByBuyer(int orderId);
        bool CloseOrderBySeller(int orderId);
        bool CloseOrderByMiddleman(int orderId);
        bool CloseOrderAutomatically(int orderId);
        bool ConfirmOrder(int orderId, int currentUserId);
        void SaveOrder();
        Task SaveOrderAsync();
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository ordersRepository;
        private readonly IFeedbackRepository feedbacksRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly ITransactionRepository transactionRepository;

        public OrderService(IOrderRepository ordersRepository, IFeedbackRepository feedbacksRepository, ITransactionRepository transactionRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
        {
            this.feedbacksRepository = feedbacksRepository;
            this.ordersRepository = ordersRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.transactionRepository = transactionRepository;
        }

        #region IOrderService Members

        public IEnumerable<Order> GetOrders()
        {
            var orders = ordersRepository.GetAll();
            return orders;
        }

        public IEnumerable<Order> GetOrders(Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include)
        {
            var orders = ordersRepository.GetAll(include);
            return orders;
        }

        public IEnumerable<Order> GetOrders(Expression<Func<Order, bool>> where, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include)
        {
            var query = ordersRepository.GetMany(where, include);
            return query;
        }

        public async Task<List<Order>> GetOrdersAsync(Expression<Func<Order, bool>> where, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include)
        {
            return await ordersRepository.GetManyAsync(where, include);

        }

        public void DeleteOrder(Order order)
        {
            ordersRepository.Remove(order);
        }

        public void UpdateOrder(Order order)
        {
            ordersRepository.Update(order);
        }
        public Order GetOrder(int id)
        {
            var order = ordersRepository.GetById(id);
            return order;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var order = await ordersRepository.GetByIdAsync(id);
            return order;
        }

        public Order GetOrder(int id, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include)
        {
            var order = ordersRepository.Get(o => o.Id == id, include);
            return order;
        }

        public async Task<Order> GetOrderAsync(int id, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include)
        {
            return await ordersRepository.GetByIdAsync(id, include);
        }

        public Order GetOrder(string accountLogin, int? middlemanId, int? sellerId, int? buyerId)
        {
            Order order = ordersRepository.GetMany(o => o.Offer.AccountLogin == accountLogin &&
            o.MiddlemanId == middlemanId && o.BuyerId == buyerId && o.SellerId == sellerId).FirstOrDefault();
            return order;

        }

        public Order GetOrder(string accountLogin, int? middlemanId, int? sellerId, int? buyerId, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include)
        {
            Order order = ordersRepository.GetMany(o => o.Offer.AccountLogin == accountLogin &&
            o.MiddlemanId == middlemanId && o.BuyerId == buyerId && o.SellerId == sellerId, include).FirstOrDefault();
            return order;

        }

        public async Task<Order> GetOrderAsync(string accountLogin, int? middlemanId, int? sellerId, int? buyerId)
        {
            return await ordersRepository.GetAsync(o => o.Offer.AccountLogin == accountLogin &&
            o.MiddlemanId == middlemanId && o.BuyerId == buyerId && o.SellerId == sellerId);

        }

        public async Task<Order> GetOrderAsync(string accountLogin, int? middlemanId, int? sellerId, int? buyerId, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include)
        {
            return await ordersRepository.GetAsync(o => o.Offer.AccountLogin == accountLogin &&
            o.MiddlemanId == middlemanId && o.BuyerId == buyerId && o.SellerId == sellerId, include);

        }

        public void CreateOrder(Order order)
        {
            ordersRepository.Add(order);
        }

        public void SaveOrder()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveOrderAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        public bool ConfirmAbortOrder(int orderId, int userId)
        {
            var order = GetOrder(orderId, include: source => source.Include(i => i.CurrentStatus).Include(i => i.StatusLogs).Include(i => i.Middleman).Include(i => i.Transactions).ThenInclude(t => t.Sender).Include(i => i.Transactions).ThenInclude(t => t.Receiver));
            if (order != null)
            {
                OrderStatus orderStatus;
                if (order.MiddlemanId == userId && order.CurrentStatus == OrderStatus.MiddlemanBackingAccount)
                {
                    orderStatus = OrderStatus.MiddlemanClosed;
                    var orderTransactions = order.Transactions.ToList();

                    foreach (var transaction in orderTransactions)
                    {
                        transaction.Sender.Balance += transaction.Amount;
                        transaction.Receiver.Balance -= transaction.Amount;

                        order.Transactions.Add(new Transaction
                        {
                            Receiver = transaction.Sender,
                            Sender = transaction.Receiver,
                            Amount = transaction.Amount,
                            CreatedDate = DateTime.Now
                        });
                    }

                    order.StatusLogs.AddLast(new StatusLog()
                    {
                        OrderStatus = order.CurrentStatus,
                        TimeStamp = DateTime.Now
                    });
                    order.CurrentStatus = orderStatus;
                    order.BuyerChecked = false;
                    order.SellerChecked = false;
                    return true;

                }
            }
            return false;

        }

        private bool CloseOrder(int orderId, Closer closer)
        {
            var order = GetOrder(orderId, include: source => source.Include(i => i.CurrentStatus).Include(i => i.StatusLogs).Include(i => i.Transactions).ThenInclude(t => t.Sender).Include(i => i.Transactions).ThenInclude(t => t.Receiver));
            if (order != null)
            {
                if (order.CurrentStatus == OrderStatus.BuyerPaying ||
                    order.CurrentStatus == OrderStatus.OrderCreating ||
                    order.CurrentStatus == OrderStatus.MiddlemanFinding ||
                    order.CurrentStatus == OrderStatus.SellerProviding ||
                    order.CurrentStatus == OrderStatus.MiddlemanChecking)
                {
                    OrderStatus orderStatus;
                    switch (closer)
                    {
                        case Closer.Buyer:
                            {
                                orderStatus = OrderStatus.BuyerClosed;
                            }
                            break;
                        case Closer.Seller:
                            {
                                orderStatus = OrderStatus.SellerClosed;
                            }
                            break;
                        case Closer.Middleman:
                            {
                                orderStatus = OrderStatus.MiddlemanClosed;
                            }
                            break;
                        case Closer.Automatically:
                            {
                                orderStatus = OrderStatus.ClosedAutomatically;
                            }
                            break;
                        default:
                            {
                                orderStatus = OrderStatus.ClosedAutomatically;
                            }
                            break;
                    }

                    var orderTransactions = order.Transactions.ToList();
                    foreach (var transaction in orderTransactions)
                    {
                        transaction.Sender.Balance += transaction.Amount;
                        transaction.Receiver.Balance -= transaction.Amount;

                        order.Transactions.Add(new Transaction
                        {
                            Receiver = transaction.Sender,
                            Sender = transaction.Receiver,
                            Amount = transaction.Amount,
                            CreatedDate = DateTime.Now
                        });
                    }

                    order.StatusLogs.AddLast(new StatusLog()
                    {
                        OrderStatus = order.CurrentStatus,
                        TimeStamp = DateTime.Now
                    });
                    order.CurrentStatus = orderStatus;
                    order.BuyerChecked = false;
                    order.SellerChecked = false;
                    return true;


                }

            }
            return false;
        }


        public bool CloseOrderByBuyer(int orderId)
        {
            return CloseOrder(orderId, Closer.Buyer);
        }

        public bool CloseOrderBySeller(int orderId)
        {
            return CloseOrder(orderId, Closer.Seller);
        }


        public bool CloseOrderByMiddleman(int orderId)
        {
            return CloseOrder(orderId, Closer.Middleman);
        }

        public bool CloseOrderAutomatically(int orderId)
        {
            return CloseOrder(orderId, Closer.Automatically);
        }


        public bool ConfirmOrder(int orderId, int currentUserId)
        {
            var order = GetOrder(orderId, include: source => source.Include(i => i.Buyer).Include(i => i.Seller).Include(i => i.CurrentStatus).Include(i => i.StatusLogs));
            if (order != null)
            {

                if (order.BuyerId == currentUserId && order.CurrentStatus == OrderStatus.BuyerConfirming)
                {
                    var mainCup = userProfileRepository.GetUserByName("palyerup");
                    if (mainCup != null)
                    {
                        decimal amount = order.AmmountSellerGet.Value;
                        transactionRepository.Add(new Transaction
                        {
                            Amount = amount,
                            Receiver = order.Seller,
                            Sender = mainCup,
                            Order = order,
                            CreatedDate = DateTime.Now
                        });

                        mainCup.Balance -= amount;
                        order.Seller.Balance += amount;
                        order.StatusLogs.AddLast(new StatusLog()
                        {
                            OrderStatus = order.CurrentStatus,
                            TimeStamp = DateTime.Now
                        });
                        order.CurrentStatus = OrderStatus.PayingToSeller;

                        order.StatusLogs.AddLast(new StatusLog()
                        {
                            OrderStatus = OrderStatus.ClosedSuccessfully,
                            TimeStamp = DateTime.Now
                        });
                        order.CurrentStatus = OrderStatus.ClosedSuccessfully;



                        order.BuyerChecked = false;
                        order.SellerChecked = false;



                        //TempData["orderBuyStatus"] = "Спасибо за подтверждение сделки! Сделка успешно закрыта.";
                        //return RedirectToAction("BuyDetails", "Order", new { id = order.Id });
                        return true;
                    }
                }

            }
            return false;
        }

        public bool ConfirmOrderByMiddleman(int orderId, int currentUserId)
        {
            var order = GetOrder(orderId, include: source => source.Include(i => i.Seller).Include(i => i.CurrentStatus).Include(i => i.StatusLogs));
            if (order != null)
            {

                if (order.MiddlemanId == currentUserId && order.CurrentStatus == OrderStatus.MiddlemanBackingAccount)
                {
                    var mainCup = userProfileRepository.GetUserByName("palyerup");
                    if (mainCup != null)
                    {
                        decimal amount = order.AmmountSellerGet.Value;
                        transactionRepository.Add(new Transaction
                        {
                            Amount = amount,
                            Receiver = order.Seller,
                            Sender = mainCup,
                            Order = order,
                            CreatedDate = DateTime.Now
                        });

                        mainCup.Balance -= amount;
                        order.Seller.Balance += amount;
                        order.StatusLogs.AddLast(new StatusLog()
                        {
                            OrderStatus = OrderStatus.PayingToSeller,
                            TimeStamp = DateTime.Now
                        });
                        order.CurrentStatus = OrderStatus.PayingToSeller;

                        order.StatusLogs.AddLast(new StatusLog()
                        {
                            OrderStatus = OrderStatus.ClosedSuccessfully,
                            TimeStamp = DateTime.Now
                        });
                        order.CurrentStatus = OrderStatus.ClosedSuccessfully;



                        order.BuyerChecked = false;
                        order.SellerChecked = false;



                        //TempData["orderBuyStatus"] = "Спасибо за подтверждение сделки! Сделка успешно закрыта.";
                        //return RedirectToAction("BuyDetails", "Order", new { id = order.Id });
                        return true;

                    }
                }
            }
            return false;
        }

        public bool AbortOrder(int orderId, int currentUserId)
        {
            var order = GetOrder(orderId, include: source => source.Include(i => i.Buyer).Include(i => i.CurrentStatus).Include(i => i.StatusLogs));
            if (order != null)
            {

                if (order.BuyerId == currentUserId && order.CurrentStatus == OrderStatus.BuyerConfirming)
                {


                    order.StatusLogs.AddLast(new StatusLog()
                    {
                        OrderStatus = OrderStatus.AbortedByBuyer,
                        TimeStamp = DateTime.Now
                    });
                    order.CurrentStatus = OrderStatus.AbortedByBuyer;

                    order.StatusLogs.AddLast(new StatusLog()
                    {
                        OrderStatus = OrderStatus.MiddlemanBackingAccount,
                        TimeStamp = DateTime.Now
                    });
                    order.CurrentStatus = OrderStatus.MiddlemanBackingAccount;



                    order.BuyerChecked = false;
                    order.SellerChecked = false;



                    //TempData["orderBuyStatus"] = "Спасибо за подтверждение сделки! Сделка успешно закрыта.";
                    //return RedirectToAction("BuyDetails", "Order", new { id = order.Id });
                    return true;

                }

            }
            return false;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orders = ordersRepository.GetAll();
            return orders;
        }

        public IEnumerable<Order> GetAllOrders(Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include)
        {
            var orders = ordersRepository.GetAll(include);
            return orders;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await ordersRepository.GetAllAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync(Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include)
        {
            return await ordersRepository.GetAllAsync(include);
        }

        #endregion

    }
}
