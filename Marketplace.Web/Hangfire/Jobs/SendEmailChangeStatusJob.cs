using Marketplace.Service.Services;
using Marketplace.Web.HtmlHelpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Hangfire.Jobs
{
    public class SendEmailChangeStatusJob
    {
        private readonly IOrderService orderService;
        private readonly IEmailSender emailSender;
        private const string message = "Статус заказа изменился";

        public SendEmailChangeStatusJob(IOrderService orderService, IEmailSender emailSender)
        {
            this.orderService = orderService;
            this.emailSender = emailSender;
        }

        //[DisableConcurrentExecution(10 * 60)]
        public async Task Do(int orderId, string userEmail, string currentStatus, string callbackUrl)
        {
            //Url.Action("BuyDetails", "Order", new { id = offer.Order.Id }, protocol: Request.Url.Scheme)).ToString()
            string body = EmailHelper.ActivateForm(null, $"Статус вашего заказа (id:{orderId}) изменился на: {currentStatus}", "Посмотреть детали", callbackUrl).ToString();
            await emailSender.SendEmailAsync(userEmail, body, message);
        }
    }
}
