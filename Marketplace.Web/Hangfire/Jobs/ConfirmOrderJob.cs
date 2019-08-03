using Marketplace.Service.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Hangfire.Jobs
{
    public interface IConfirmOrderJob
    {
        Task Do(int orderId);
    }
    public class ConfirmOrderJob : IConfirmOrderJob
    {
        private readonly IOrderService orderService;

        public ConfirmOrderJob(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        //[DisableConcurrentExecution(10 * 60)]
        public async Task Do(int orderId)
        {

            var order = orderService.GetOrder(orderId, source => source.Include(i => i.Buyer).Include(i => i.Seller));

            if (order != null)
            {
                var result = orderService.ConfirmOrder(orderId, order.BuyerId.Value);
                if (result)
                {
                    order.JobId = MarketplaceHangfire.SetLeaveFeedbackJob(order.SellerId, order.BuyerId.Value, order.Id, TimeSpan.FromDays(15));
                }
                await orderService.SaveOrderAsync();
            }
        }
    }
}
