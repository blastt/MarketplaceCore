using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Services
{
    public interface IFeedbackService
    {
        IEnumerable<Feedback> GetAllFeedbacks();
        Task<List<Feedback>> GetAllFeedbacksAsync();
        IEnumerable<Feedback> GetFeedbacks(Expression<Func<Feedback, bool>> where, Func<IQueryable<Feedback>, IIncludableQueryable<Feedback, object>> include);
        Task<List<Feedback>> GetFeedbacksAsync(Expression<Func<Feedback, bool>> where, Func<IQueryable<Feedback>, IIncludableQueryable<Feedback, object>> include);

        int PositiveFeedbackCount(UserProfile user);
        int NegativeFeedbackCount(UserProfile user);
        void LeaveAutomaticFeedback(int sellerId, int buyerId, int orderId);
        double PositiveFeedbackProcent(int positiveFeedbacks, int negativeFeedbacks);
        double NegativeFeedbackProcent(int positiveFeedbacks, int negativeFeedbacks);
        Feedback GetFeedback(int id);
        Task<Feedback> GetFeedbackAsync(int id);

        void CreateFeedback(Feedback feedback);
        void SaveFeedback();
        Task SaveFeedbackAsync();
    }

    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository feedbacksRepository;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUnitOfWork unitOfWork;

        public FeedbackService(IFeedbackRepository feedbacksRepository, IUserProfileRepository userProfileRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            this.userProfileRepository = userProfileRepository;
            this.orderRepository = orderRepository;
            this.feedbacksRepository = feedbacksRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IMessageService Members

        public IEnumerable<Feedback> GetAllFeedbacks()
        {
            var feedbacks = feedbacksRepository.GetAll();
            return feedbacks;
        }

        public async Task<List<Feedback>> GetAllFeedbacksAsync()
        {
            return await feedbacksRepository.GetAllAsync();
        }

        public IEnumerable<Feedback> GetFeedbacks(Expression<Func<Feedback, bool>> where, Func<IQueryable<Feedback>, IIncludableQueryable<Feedback, object>> include)
        {
            var feedbacks = feedbacksRepository.GetMany(where, include);
            return feedbacks;
        }

        public async Task<List<Feedback>> GetFeedbacksAsync(Expression<Func<Feedback, bool>> where, Func<IQueryable<Feedback>, IIncludableQueryable<Feedback, object>> include)
        {
            return await feedbacksRepository.GetManyAsync(where, include);
        }

        public void LeaveAutomaticFeedback(int sellerId, int buyerId, int orderId)
        {
            var seller = userProfileRepository.Get(u => u.Id == sellerId, include: source => source.Include(i => i.FeedbacksToOthers));
            var buyer = userProfileRepository.Get(u => u.Id == buyerId, include: source => source.Include(i => i.FeedbacksToOthers));
            var order = orderRepository.GetById(orderId, include: source => source.Include(i => i.Buyer).Include(i => i.Seller));
            if (seller != null && buyer != null && order != null)
            {
                if (order.Buyer.Id == buyerId && order.Seller.Id == sellerId) // добавить условие на статус заказа
                {
                    var feedbackToSeller = new Feedback()
                    {
                        Grade = FeedbackGrade.Good,
                        Comment = "Автоматический отзыв",
                        CreatedDate = DateTime.Now,
                        Order = order,
                        UserFrom = buyer,
                        UserTo = seller
                    };
                    var feedbackToBuyer = new Feedback()
                    {
                        Grade = FeedbackGrade.Good,
                        Comment = "Автоматический отзыв",
                        CreatedDate = DateTime.Now,
                        Order = order,
                        UserFrom = seller,
                        UserTo = buyer
                    };
                    if (!order.SellerFeedbacked)
                    {
                        seller.FeedbacksToOthers.Add(feedbackToBuyer);
                        order.SellerFeedbacked = true;

                    }
                    if (!order.BuyerFeedbacked)
                    {
                        buyer.FeedbacksToOthers.Add(feedbackToSeller);
                        order.BuyerFeedbacked = true;
                    }
                }
            }
        }

        public int PositiveFeedbackCount(UserProfile user)
        {
            int positiveCount = user.FeedbacksMy.Where(f => f.Grade == FeedbackGrade.Good).Count();
            return positiveCount;
        }

        public int NegativeFeedbackCount(UserProfile user)
        {
            int negativeCount = user.FeedbacksMy.Where(f => f.Grade == FeedbackGrade.Bad).Count();
            return negativeCount;
        }

        public double PositiveFeedbackProcent(int positiveFeedbacks, int negativeFeedbacks)
        {
            int allFeedbackCount = positiveFeedbacks + negativeFeedbacks;

            double pos = Math.Round((double)(100 * positiveFeedbacks) / (allFeedbackCount), 2);
            return pos;
        }

        public double NegativeFeedbackProcent(int positiveFeedbacks, int negativeFeedbacks)
        {
            int allFeedbackCount = positiveFeedbacks + negativeFeedbacks;

            double neg = Math.Round((double)(100 * negativeFeedbacks) / (allFeedbackCount), 2);
            return neg;
        }

        public Feedback GetFeedback(int id)
        {
            var feedback = feedbacksRepository.GetById(id);
            return feedback;
        }

        public Task<Feedback> GetFeedbackAsync(int id)
        {
            var feedback = feedbacksRepository.GetByIdAsync(id);
            return feedback;
        }


        public void CreateFeedback(Feedback feedback)
        {
            feedbacksRepository.Add(feedback);
        }

        public void SaveFeedback()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveFeedbackAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        #endregion

    }
}
