using Marketplace.Data.Infrastructure;
using Marketplace.Model.Enums;
using Marketplace.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Data.Repositories
{
    public class OrderStatusRepository : Repository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public OrderStatus GetOrderStatusByValue(OrderStatusValue value)
        {
            return DbContext.OrderStatuses.FirstOrDefault(g => g.Value == value);
        }
    }

    public interface IOrderStatusRepository : IRepository<OrderStatus>
    {
        OrderStatus GetOrderStatusByValue(OrderStatusValue value);
    }
}
