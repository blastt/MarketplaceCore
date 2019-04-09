using Hangfire;
using Marketplace.Web.Hangfire.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Hangfire
{
    public static class Hangfire
    {
        public static string SetOrderCloseJob(int orderId, TimeSpan timeSpan)
        {
            return BackgroundJob.Schedule<OrderCloseJob>(
                j => j.Do(orderId),
                timeSpan);
        }
        public static string SetConfirmOrderJob(int orderId, TimeSpan timeSpan)
        {
            return BackgroundJob.Schedule<ConfirmOrderJob>(
                j => j.Do(orderId),
                timeSpan);
        }
        public static string SetLeaveFeedbackJob(int sellerId, int buyerId, int orderId, TimeSpan timeSpan)
        {
            return BackgroundJob.Schedule<LeaveFeedbackJob>(
                j => j.Do(sellerId, buyerId, orderId),
                timeSpan);
        }
        public static string SetDeactivateOfferJob(int offerId, string callbackUrl, TimeSpan timeSpan)
        {
            return BackgroundJob.Schedule<DeactivateOfferJob>(
                j => j.Do(offerId, callbackUrl),
                timeSpan);
        }

        public static string SetSendEmailChangeStatus(int orderId, string userEmail, string currentStatus, string callbackUrl)
        {
            return BackgroundJob.Enqueue<SendEmailChangeStatusJob>(
                j => j.Do(orderId, userEmail, currentStatus, callbackUrl));
        }

        public static string SetSendEmailAccountData(string login, string password, string email, string emailPassword, string additionalInfo, string userEmail)
        {
            return BackgroundJob.Enqueue<SendEmailAccountDataJob>(
                j => j.Do(login, password, email, emailPassword, additionalInfo, userEmail));
        }
    }
}
