using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model;
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
    public interface IOfferService
    {
        IEnumerable<Offer> GetAllOffers();
        IEnumerable<Offer> GetAllOffers(Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include);
        IEnumerable<Offer> GetOffers(Expression<Func<Offer, bool>> where, Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include);
        Task<List<Offer>> GetAllOffersAsync();
        Task<List<Offer>> GetAllOffersAsync(Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include);
        Task<List<Offer>> GetOffersAsync(Expression<Func<Offer, bool>> where, Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include);

        //IEnumerable<Offer> GetCategoryGadgets(string categoryName, string gadgetName = null);
        Offer GetOffer(int id);
        Task<Offer> GetOfferAsync(int id);
        void Delete(Offer offer);
        Offer GetOffer(int id, Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include);
        Task<Offer> GetOfferAsync(int id, Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include);
        //Task<Offer> GetOfferAsync(int id);
        Task<IEnumerable<Offer>> GetOffersAsync(Expression<Func<Offer, bool>> where);
        decimal CalculateMiddlemanPrice(decimal offerPrice);
        IEnumerable<Offer> SearchOffers(string game, string sort, ref bool isOnline, ref bool searchInDiscription,
            string searchString, ref int page, int pageSize, ref int totalItems, ref decimal minGamePrice, ref decimal maxGamePrice, ref decimal priceFrom, ref decimal priceTo, string[] filters);
        void CreateOffer(Offer offer);
        void UpdateOffer(Offer offer);
        bool DeactivateOffer(Offer offer, int currentUserId);
        void SaveOffer();
        Task SaveOfferAsync();
    }

    public class OfferService : IOfferService
    {
        private readonly IAccountInfoRepository accountInfosRepository;
        private readonly ITransactionRepository transactionsRepository;
        private readonly IFeedbackRepository feedbacksRepository;
        private readonly IStatusLogRepository statusLogsRepository;
        private readonly IOfferRepository offersRepository;
        private readonly IOrderRepository ordersRepository;
        private readonly IUnitOfWork unitOfWork;

        public OfferService(IOfferRepository offersRepository, IOrderRepository ordersRepository, IFeedbackRepository feedbacksRepository, IStatusLogRepository statusLogsRepository, IUnitOfWork unitOfWork, IAccountInfoRepository accountInfosRepository, ITransactionRepository transactionsRepository)
        {
            this.ordersRepository = ordersRepository;
            this.feedbacksRepository = feedbacksRepository;
            this.statusLogsRepository = statusLogsRepository;
            this.transactionsRepository = transactionsRepository;
            this.accountInfosRepository = accountInfosRepository;
            this.offersRepository = offersRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IOfferService Members

        public IEnumerable<Offer> GetAllOffers()
        {
            var offers = offersRepository.GetAll();
            return offers;
        }

        public IEnumerable<Offer> GetAllOffers(Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include)
        {
            var offers = offersRepository.GetAll(include);
            return offers;
        }

        public IEnumerable<Offer> GetOffers(Expression<Func<Offer, bool>> where, Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include)
        {
            var query = offersRepository.GetMany(where, include);
            return query;
        }

        public async Task<List<Offer>> GetAllOffersAsync()
        {
            return await offersRepository.GetAllAsync();
        }

        public async Task<List<Offer>> GetAllOffersAsync(Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include)
        {
            return await offersRepository.GetAllAsync(include);
        }

        public async Task<List<Offer>> GetOffersAsync(Expression<Func<Offer, bool>> where, Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include)
        {
            return await offersRepository.GetManyAsync(where, include);
        }


        public bool DeactivateOffer(Offer offer, int currentUserId)
        {
            if (offer != null && offer.UserProfileId == currentUserId && offer.State == OfferState.Active)
            {
                offer.State = OfferState.Inactive;
                offer.DateDeleted = DateTime.Now;
                return true;
            }
            return false;
        }

        public Offer GetOffer(int id)
        {
            var offer = offersRepository.GetById(id);
            return offer;
        }

        public async Task<Offer> GetOfferAsync(int id)
        {
            return await offersRepository.GetByIdAsync(id);
        }

        public Offer GetOffer(int id, Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include)
        {
            var offer = offersRepository.GetById(id, include);
            return offer;
        }

        public async Task<Offer> GetOfferAsync(int id, Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>> include)
        {
            return await offersRepository.GetByIdAsync(id, include);
        }

        //public async Task<Offer> GetOfferAsync(int id)
        //{
        //    var offer = await offersRepository.GetAsync(o => o.Id == id);
        //    return offer;
        //}

        public void Delete(Offer offer)
        {
            offersRepository.Remove(offer);
        }

        public void CreateOffer(Offer offer)
        {
            offersRepository.Add(offer);
        }

        public void UpdateOffer(Offer offer)
        {
            offersRepository.Update(offer);
        }

        private IEnumerable<Offer> SearchOffersByGame(string game)
        {
            return offersRepository.GetMany(m => m.Game.Value == game, include: source => source.Include(i => i.Game).Include(i => i.UserProfile));

        }



        private IEnumerable<Offer> SearchOffersByPrice(IEnumerable<Offer> offers, ref decimal priceFrom, ref decimal priceTo, ref decimal minGamePrice, ref decimal maxGamePrice)
        {
            var offersList = offers;
            if (offers.Any())
            {
                minGamePrice = offersList.Min(m => m.Price);

                maxGamePrice = offersList.Max(m => m.Price);


                if (priceFrom == 0)
                {
                    priceFrom = minGamePrice;


                }
                if (priceTo == 0)
                {
                    priceTo = maxGamePrice;

                }

                decimal priceF = priceFrom;
                decimal priceT = priceTo;

                offersList = offersList.Where(offer => offer.Price >= priceF && offer.Price <= priceT);
            }

            return offersList;
        }

        private IEnumerable<Offer> SearchOffersBySearchString(IEnumerable<Offer> offers, string searchString, ref bool searchInDiscription)
        {
            if (searchInDiscription)
            {
                offers = offers.Where(o => o.Header.Replace(" ", "").ToLower().Contains(searchString.Replace(" ", "").ToLower()) || o.Discription.Replace(" ", "").ToLower().Contains(searchString.Replace(" ", "").ToLower()));
            }
            else
            {
                offers = offers.Where(o => o.Header.Replace(" ", "").ToLower().Contains(searchString.Replace(" ", "").ToLower()));
            }
            return offers;
        }

        private IEnumerable<Offer> SearchOffersByPage(IEnumerable<Offer> offers, ref int page, int pageSize, ref int totalItems)

        {
            offers = offers.Skip((page - 1) * pageSize).Take(pageSize);
            return offers;
        }

        private IEnumerable<Offer> SortOffers(IEnumerable<Offer> offers, string sort)
        {
            switch (sort)
            {
                case "bestSeller":
                    {
                        offers = from offer in offers
                                 orderby (offer.UserProfile.PositiveFeedbackCount - offer.UserProfile.NegativeFeedbackCount) descending
                                 select offer;
                        break;
                    }
                case "priceDesc":
                    {
                        offers = from offer in offers
                                 orderby offer.Price descending
                                 select offer;
                        break;
                    }
                case "priceAsc":
                    {
                        offers = from offer in offers
                                 orderby offer.Price ascending
                                 select offer;
                        break;
                    }
                case "newestOffer":
                    {
                        offers = from offer in offers
                                 orderby offer.CreatedDate descending
                                 select offer;
                        break;
                    }
                default:
                    break;
            }
            return offers;
        }

        public IEnumerable<Offer> SearchOffers(string game, string sort, ref bool isOnline, ref bool searchInDiscription,
            string searchString, ref int page, int pageSize, ref int totalItems, ref decimal minGamePrice, ref decimal maxGamePrice, ref decimal priceFrom, ref decimal priceTo, string[] filters)
        {
            IEnumerable<Offer> offers;
            offers = SearchOffersByGame(game);
            offers = offers.Where(o => o.State == OfferState.Active);
            offers = SearchOffersBySearchString(offers, searchString, ref searchInDiscription);
            offers = SearchOffersByPrice(offers, ref priceFrom, ref priceTo, ref minGamePrice, ref maxGamePrice);
            offers = SortOffers(offers, sort);
            return offers;
        }

        public decimal CalculateMiddlemanPrice(decimal offerPrice)
        {
            decimal middlemanPrice = 0;

            if (offerPrice < 3000)
            {
                middlemanPrice = 300;

            }
            else if (offerPrice < 15000)
            {
                middlemanPrice = offerPrice * Convert.ToDecimal(0.1);
            }
            else
            {
                middlemanPrice = 1500;
            }

            return middlemanPrice;
        }

        public void SaveOffer()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveOfferAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Offer>> GetOffersAsync(Expression<Func<Offer, bool>> where)
        {
            return await offersRepository.GetManyAsync(where);
        }



        #endregion

    }
}
