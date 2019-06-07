using Marketplace.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Hangfire.Jobs
{
    public interface IOrderCloseJob
    {
        void Do(int orderId);
    }
    public class OrderCloseJob : IOrderCloseJob
    {
        private readonly IOrderService orderService;

        public OrderCloseJob(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        //[DisableConcurrentExecution(10 * 60)]
        public void Do(int orderId)
        {
            orderService.CloseOrderAutomatically(orderId);
            orderService.SaveOrderAsync();

        }
    }
}
