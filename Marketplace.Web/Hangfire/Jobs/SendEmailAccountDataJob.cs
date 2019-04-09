using Marketplace.Service.Services;
using Marketplace.Web.HtmlHelpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Hangfire.Jobs
{
    public class SendEmailAccountDataJob
    {
        private readonly IOrderService orderService;
        private readonly IEmailSender emailSender;
        private const string message = "Данные от аккаунта";

        public SendEmailAccountDataJob(IOrderService orderService, IEmailSender emailSender)
        {
            this.orderService = orderService;
            this.emailSender = emailSender;
        }

        //[DisableConcurrentExecution(10 * 60)]
        public async Task Do(string login, string password, string email, string emailPassword, string additionalInfo, string userEmail)
        {
            //Url.Action("BuyDetails", "Order", new { id = offer.Order.Id }, protocol: Request.Url.Scheme)).ToString()
            string body = EmailHelper.AccountData(null, login, password, email, emailPassword, additionalInfo).ToString();

            await emailSender.SendEmailAsync(userEmail, body, message);
        }
    }
}
