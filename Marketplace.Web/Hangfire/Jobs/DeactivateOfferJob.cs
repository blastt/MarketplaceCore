using Marketplace.Service.Services;
using Marketplace.Web.HtmlHelpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Hangfire.Jobs
{
    public interface IDeactivateOfferJob
    {
        Task Do(int itemId, string callbackUrl);
    }
    public class DeactivateOfferJob : IDeactivateOfferJob
    {
        private readonly IOfferService offerService;
        private readonly IEmailSender emailSender;
        private const string message = "Ваше объявление деактивировано";
        public DeactivateOfferJob(IOfferService offerService, IEmailSender emailSender)
        {
            this.offerService = offerService;
            this.emailSender = emailSender;
        }

        //[DisableConcurrentExecution(10 * 60)]
        public async Task Do(int itemId, string callbackUrl)
        {

            var offer = offerService.GetOffer(itemId, i => i.UserProfile, i => i.UserProfile.User);
            if (offer != null)
            {                
                offerService.DeactivateOffer(offer, offer.UserProfileId);
                offerService.SaveOffer();
                string body = EmailHelper.ActivateForm(null, $"Здравствуйте {offer.UserProfile.User.UserName}, ваше объявление {offer.Header} деактивировано.", "Активировать", callbackUrl).ToString();
                await emailSender.SendEmailAsync(offer.UserProfile.User.Email, body, message);
            }

        }
    }
}
