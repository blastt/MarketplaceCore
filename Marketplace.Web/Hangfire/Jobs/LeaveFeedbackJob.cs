using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Service.Services;

namespace Marketplace.Web.Hangfire.Jobs
{
    public interface ILeaveFeedbackJob
    {
        void Do(int sellerId, int buyerId, int orderId);
    }
    public class LeaveFeedbackJob : ILeaveFeedbackJob
    {
        private readonly IFeedbackService feedbackService;

        public LeaveFeedbackJob(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        //[DisableConcurrentExecution(10 * 60)]
        public void Do(int sellerId, int buyerId, int orderId)
        {
            feedbackService.LeaveAutomaticFeedback(sellerId, buyerId, orderId);
            feedbackService.SaveFeedback();
        }
    }
}
