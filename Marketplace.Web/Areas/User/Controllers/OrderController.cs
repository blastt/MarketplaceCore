using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Model.Enums;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.User.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var userId = await userService.GetCurrentUserId(HttpContext.User);
            var orders = await orderService.GetOrdersAsync(o => o.SellerId == userId, i => i.Buyer, i => i.CurrentStatus, i => i.Offer);
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
            var orders = await orderService.GetOrdersAsync(o => o.BuyerId == userId, i => i.Seller, i => i.CurrentStatus, i => i.Offer);
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

                var order = await orderService.GetOrderAsync(id.Value, i => i.Seller, i => i.Middleman, i => i.Buyer, i => i.CurrentStatus, i => i.StatusLogs, i => i.AccountInfos, i => i.Offer, i => i.StatusLogs.Select(m => m.OldStatus), i => i.StatusLogs.Select(m => m.NewStatus));
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

                        var currentStatus = order.CurrentStatus.Value;
                        if (currentStatus == OrderStatusValue.BuyerPaying ||
                            currentStatus == OrderStatusValue.OrderCreating ||
                            currentStatus == OrderStatusValue.MiddlemanFinding ||
                            currentStatus == OrderStatusValue.SellerProviding ||
                            currentStatus == OrderStatusValue.MiddlemanChecking)
                        {
                            model.ShowCloseButton = true;
                        }
                        if (currentStatus == OrderStatusValue.BuyerPaying)
                        {
                            model.ShowPayButton = true;
                        }
                        if ((currentStatus == OrderStatusValue.ClosedSuccessfully ||
                            currentStatus == OrderStatusValue.PayingToSeller) && !order.BuyerFeedbacked)
                        {
                            model.ShowFeedbackToSeller = true;
                        }
                        if ((currentStatus == OrderStatusValue.ClosedSuccessfully ||
                            currentStatus == OrderStatusValue.PayingToSeller) && !order.SellerFeedbacked)
                        {
                            model.ShowFeedbackToBuyer = true;
                        }
                        if (currentStatus == OrderStatusValue.BuyerConfirming ||
                            currentStatus == OrderStatusValue.ClosedSuccessfully ||
                            currentStatus == OrderStatusValue.PayingToSeller)
                        {
                            model.ShowAccountInfo = true;
                        }

                        if (currentStatus == OrderStatusValue.BuyerConfirming)
                        {
                            model.ShowConfirm = true;
                        }

                        if (currentStatus == OrderStatusValue.SellerProviding)
                        {
                            model.ShowProvideData = true;
                        }
                        if (order.Offer.SellerPaysMiddleman)
                        {
                            model.MiddlemanPrice = 0;
                        }
                        else
                        {
                            model.MiddlemanPrice = order.Offer.MiddlemanPrice.Value;
                        }
                        IList<StatusLog> orderLogs = new List<StatusLog>();

                        foreach (var log in order.StatusLogs)
                        {
                            orderLogs.Add(log);
                            if (log.NewStatus.Value == OrderStatusValue.AbortedByBuyer)
                            {
                                var test = order.StatusLogs.FirstOrDefault(s => s.NewStatus.Value == OrderStatusValue.BuyerConfirming);
                                orderLogs.Remove(test);
                            }
                        }
                        model.Logs = orderLogs;
                        model.CurrentStatusName = order.CurrentStatus.DuringName;
                        model.StatusLogs = order.StatusLogs;
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