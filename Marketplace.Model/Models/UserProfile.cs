using Marketplace.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class UserProfile : BaseEntity<int>
    {
        public string Avatar32 { get; set; }
        public string Avatar64 { get; set; }
        public string Avatar96 { get; set; }

        public string UserName { get; set; }
        public int PositiveFeedbackCount
        {
            set; get;
        }

        public int NegativeFeedbackCount
        {
            set; get;
        }

        public int? Rating
        {
            get
            {
                return PositiveFeedbackCount - NegativeFeedbackCount;
            }
            private set
            {

            }
        }

        public int? AllFeedbackCount
        {
            get
            {
                return PositiveFeedbackCount + NegativeFeedbackCount;
            }
            private set
            {

            }
        }

        public double? PositiveFeedbackProcent
        {
            get
            {
                if (AllFeedbackCount != 0)
                {
                    double pos = Math.Round((double)(100 * PositiveFeedbackCount) / (PositiveFeedbackCount + NegativeFeedbackCount), 2);
                    return pos;
                }
                return 0;
            }
            private set
            {

            }
        }

        public double? NegativeFeedbackProcent
        {
            get
            {
                if (AllFeedbackCount != 0)
                {
                    double neg = Math.Round((double)(100 * NegativeFeedbackCount) / (PositiveFeedbackCount + NegativeFeedbackCount), 2);
                    return neg;
                }
                return 0;
            }
            private set
            {

            }
        }

        public int? SuccessOrderRate
        {

            get
            {


                return 0;
            }
            private set
            {

            }
        }


        public string Discription { get; set; }
        public bool IsOnline { get; set; }


        public IList<Billing> Billings { get; set; }
        public IList<Transaction> TransactionsAsReceiver { get; set; }
        public IList<Transaction> TransactionsAsSender { get; set; }

        public decimal Balance { get; set; }
        public User User { get; set; }
        public ICollection<Message> MessagesAsSender { get; set; }
        public ICollection<Message> MessagesAsReceiver { get; set; }
        public ICollection<Offer> Offers { get; set; }

        public ICollection<Order> OrdersAsSeller { get; set; }
        public ICollection<Order> OrdersAsBuyer { get; set; }
        public ICollection<Order> OrdersAsMiddleman { get; set; }

        public ICollection<Feedback> FeedbacksMy { get; set; }
        public ICollection<Feedback> FeedbacksToOthers { get; set; }

        public ICollection<Dialog> DialogsAsCreator { get; set; }
        public ICollection<Dialog> DialogsAsСompanion { get; set; }

        public ICollection<Withdraw> Withdraws { get; set; }

        public UserProfile()
        {
            Billings = new List<Billing>();
            TransactionsAsReceiver = new List<Transaction>();
            TransactionsAsSender = new List<Transaction>();
            MessagesAsSender = new List<Message>();
            MessagesAsReceiver = new List<Message>();
            Offers = new List<Offer>();
            OrdersAsSeller = new List<Order>();
            OrdersAsBuyer = new List<Order>();
            OrdersAsMiddleman = new List<Order>();
            FeedbacksMy = new List<Feedback>();
            FeedbacksToOthers = new List<Feedback>();
            DialogsAsCreator = new List<Dialog>();
            DialogsAsСompanion = new List<Dialog>();
            Withdraws = new List<Withdraw>();
        }
    }
}
