using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Model;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.User.Models.Order;
using Marketplace.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Web.Areas.User.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        public OrderController(IOrderService orderService, IUserService userService)
        {
            this.orderService = orderService;
            this.userService = userService;
        }


        public async Task<ActionResult> SellList()
        {
            // include: source => source.Include(i => i.Game)
            var userId = await userService.GetCurrentUserId(HttpContext.User);
            var orders = await orderService.GetOrdersAsync(o => o.SellerId == userId, include: source => source.Include(i => i.Buyer).Include(i => i.CurrentStatus).Include(i => i.Offer));
            var modelOrders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);
            var model = new OrderListViewModel()
            {
                Orders = modelOrders
            };
            return View(model);
        }

        public async Task<ActionResult> BuyList()
        {
            var userId = await userService.GetCurrentUserId(HttpContext.User);
            var orders = await orderService.GetOrdersAsync(o => o.BuyerId == userId, include: source => source.Include(i => i.Seller).Include(i => i.CurrentStatus).Include(i => i.Offer));
            var modelOrders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);
            var model = new OrderListViewModel()
            {
                Orders = modelOrders
            };
            return View(model);
        }

        public async Task<ActionResult> BuyDetails(int? id)
        {
            if (id != null)
            {
                var order = await orderService.GetOrderAsync(id.Value, include: source => source.Include(i => i.Seller).Include(i => i.Middleman).Include(i => i.Buyer).Include(i => i.CurrentStatus).Include(i => i.StatusLogs).Include(i => i.StatusLogs).Include(i => i.AccountInfos).Include(i => i.Offer));
                if (order != null)
                {
                    var currenUserId = await userService.GetCurrentUserId(HttpContext.User);
                    if (order.BuyerId == currenUserId)
                    {
                        order.BuyerChecked = true;
                        await orderService.SaveOrderAsync();
                        var ordersBuy = await orderService.GetOrdersAsync(m => m.BuyerId == currenUserId);
                        var ordersSell = await orderService.GetOrdersAsync(m => m.SellerId == currenUserId);

                        DetailsOrderViewModel model = Mapper.Map<Order, DetailsOrderViewModel>(order);

                        model.CountOfBuy = ordersBuy.Count;
                        model.CountOfSell = ordersSell.Count;

                        var currentStatus = order.CurrentStatus;
                        if (currentStatus == OrderStatus.BuyerPaying ||
                            currentStatus == OrderStatus.OrderCreating ||
                            currentStatus == OrderStatus.MiddlemanFinding ||
                            currentStatus == OrderStatus.SellerProviding ||
                            currentStatus == OrderStatus.MiddlemanChecking)
                        {
                            model.ShowCloseButton = true;
                        }
                        if (currentStatus == OrderStatus.BuyerPaying)
                        {
                            model.ShowPayButton = true;
                        }
                        if ((currentStatus == OrderStatus.ClosedSuccessfully ||
                            currentStatus == OrderStatus.PayingToSeller) && !order.BuyerFeedbacked)
                        {
                            model.ShowFeedbackToSeller = true;
                        }
                        if ((currentStatus == OrderStatus.ClosedSuccessfully ||
                            currentStatus == OrderStatus.PayingToSeller) && !order.SellerFeedbacked)
                        {
                            model.ShowFeedbackToBuyer = true;
                        }
                        if (currentStatus == OrderStatus.BuyerConfirming ||
                            currentStatus == OrderStatus.ClosedSuccessfully ||
                            currentStatus == OrderStatus.PayingToSeller)
                        {
                            model.ShowAccountInfo = true;
                        }

                        if (currentStatus == OrderStatus.BuyerConfirming)
                        {
                            model.ShowConfirm = true;
                        }

                        if (currentStatus == OrderStatus.SellerProviding)
                        {
                            model.ShowProvideData = true;
                        }
                        //TODO
                        //if (order.Offer.SellerPaysMiddleman)
                        //{
                        //    model.MiddlemanPrice = 0;
                        //}
                        else
                        {
                            model.MiddlemanPrice = order.Offer.MiddlemanPrice.Value;
                        }
                        LinkedList<StatusLog> orderLogs = new LinkedList<StatusLog>();

                        foreach (var log in order.StatusLogs)
                        {
                            orderLogs.AddLast(log);
                            if (log.OrderStatus == OrderStatus.AbortedByBuyer)
                            {
                                var test = order.StatusLogs.FirstOrDefault(s => s.OrderStatus == OrderStatus.BuyerConfirming);
                                orderLogs.Remove(test);
                            }
                        }
                        model.Logs = orderLogs;
                        model.Logs = order.StatusLogs;
                        model.ModeratorId = order.MiddlemanId;
                        return View(model);
                    }
                    return RedirectToAction("OrderBuy");
                }
            }
            return NotFound();
        }
    }
}