using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Marketplace.Model;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Models.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUserService userService;
        private readonly IOfferService offerService;
        private readonly IUserProfileService userProfileService;
        private readonly ITransactionService transactionService;
        public OrderController(IOfferService offerService, IUserService userService, IUserProfileService userProfileService, ITransactionService transactionService)
        {
            this.userService = userService;
            this.offerService = offerService;
            this.userProfileService = userProfileService;
            this.transactionService = transactionService;
        }

        public async Task<IActionResult> Create(int? offerId)
        {
            if (offerId != null)
            {
                Offer offer = await offerService.GetOfferAsync(offerId.Value, source => source.Include(i => i.Game).Include(i => i.UserProfile));
                int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
                if (offer != null && offer.Order == null && offer.State == OfferState.Active && offer.UserProfileId != currentUserId)
                {
                    UserProfile userProfile = await userProfileService.GetUserProfileByIdAsync(currentUserId);
                    var model = new CreateOrderViewModel()
                    {
                        OfferHeader = offer.Header,
                        OfferId = offer.Id,
                        Game = offer.Game.Name,
                        SecureTransactionPayer = offer.SecureTransactionPayer,
                        SecureTransactionPrice = offer.MiddlemanPrice.Value,
                        OrderSum = offer.Price,
                        Quantity = 1,
                        SellerId = offer.UserProfileId,
                        BuyerId = currentUserId
                    };
                    if (offer.SecureTransactionPayer == SecureTransactionPayer.Seller)
                    {
                        model.OrderSum = offer.Price;
                    }
                    else if (offer.SecureTransactionPayer == SecureTransactionPayer.Buyer)
                    {
                        model.OrderSum = offer.Price + offer.MiddlemanPrice.Value;
                    }
                    else
                    {
                        model.OrderSum = offer.Price + offer.MiddlemanPrice.Value / 2;
                    }
                    model.UserCanPayWithBalance = userProfile.Balance >= model.OrderSum;
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            Offer offer = await offerService.GetOfferAsync(model.OfferId, source => source.Include(i => i.UserProfile).ThenInclude(u => u.User).Include(i => i.Order).ThenInclude(o => o.StatusLogs));
            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            if (offer != null && offer.Order == null && offer.State == OfferState.Active && offer.UserProfileId != currentUserId)
            {
                var userProfile = await userProfileService.GetUserProfileByIdAsync(currentUserId);
                var mainCup = userProfileService.GetUserProfileByName("palyerup");
                if (userProfile != null && mainCup != null && userProfile.Balance >= model.OrderSum)
                {
                    Order order = new Order
                    {
                        BuyerId = currentUserId,
                        SellerId = offer.UserProfileId,
                        CreatedDate = DateTime.Now
                    };
                    offer.Order = order;
                    offer.State = OfferState.Closed;
                    if (mainCup != null)
                    {
                        var seller = offer.UserProfile;
                        var buyer = userProfile;
                        switch (offer.SecureTransactionPayer)
                        {
                            case SecureTransactionPayer.Seller:
                                order.Sum = offer.Price;
                                order.AmmountSellerGet = offer.Price - offer.MiddlemanPrice.Value;
                                break;
                            case SecureTransactionPayer.Buyer:
                                order.Sum = offer.Price + offer.MiddlemanPrice.Value;
                                order.AmmountSellerGet = offer.Price;
                                break;
                            case SecureTransactionPayer.InTwain:
                                //TODO
                                order.Sum = offer.Price + offer.MiddlemanPrice.Value;
                                order.AmmountSellerGet = offer.Price;
                                break;
                            default:
                                throw new Exception(); //TODO
                                
                        }
                        if (!transactionService.TransferMoney(order, buyer, mainCup))
                        {
                            return View("NotEnoughMoney");
                        }
                        order.StatusLogs.AddLast(new StatusLog()
                        {
                            OrderStatus = OrderStatus.BuyerPaying
                        });

                        order.StatusLogs.AddLast(new StatusLog()
                        {
                            OrderStatus = OrderStatus.MiddlemanFinding
                        });
                        order.CurrentStatus = OrderStatus.MiddlemanFinding;

                        order.BuyerChecked = false;
                        order.SellerChecked = false;

                        if (order.JobId != null)
                        {
                            BackgroundJob.Delete(order.JobId);
                            order.JobId = null;
                        }

                        if (offer.JobId != null)
                        {
                            BackgroundJob.Delete(offer.JobId);
                            offer.JobId = null;
                        }

                        offerService.SaveOffer();

                        //MarketHangfire.SetSendEmailChangeStatus(offer.Order.Id, seller.ApplicationUser.Email, order.CurrentStatus.DuringName, Url.Action("SellDetails", "Order", new { id = offer.Order.Id }, protocol: Request.Url.Scheme));

                        //MarketHangfire.SetSendEmailChangeStatus(offer.Order.Id, buyer.ApplicationUser.Email, order.CurrentStatus.DuringName, Url.Action("BuyDetails", "Order", new { id = offer.Order.Id }, protocol: Request.Url.Scheme));
                        //offer.Order.JobId = MarketHangfire.SetOrderCloseJob(offer.Order.Id, TimeSpan.FromDays(1));
                        ////_orderService.UpdateOrder(order);
                        //_orderService.SaveOrder();
                        TempData["orderBuyStatus"] = "Оплата прошла успешно";

                        return RedirectToAction("BuyDetails", "Order", new { id = offer.Order.Id });
                    }

                }
            }
            return NotFound();
        }
    }
}