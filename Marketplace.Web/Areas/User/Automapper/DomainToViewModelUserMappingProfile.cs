using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Web.Areas.User.Models.Dialog;
using Marketplace.Web.Areas.User.Models.Offer;
using Marketplace.Web.Areas.User.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Areas.User.Automapper
{
    public partial class DomainToViewModelUserMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelUserMappingProfile"; }
        }

        public DomainToViewModelUserMappingProfile()
        {
            CreateMap<Offer, OfferViewModel>()
                .ForPath(o => o.Game, map => map.MapFrom(vm => vm.Game.Name))
                .ForMember(o => o.Header, map => map.MapFrom(vm => vm.Header))
                .ForMember(o => o.SellerPaysMiddleman, map => map.MapFrom(vm => vm.SellerPaysMiddleman))
                .ForMember(o => o.IsBanned, map => map.MapFrom(vm => vm.IsBanned))
                .ForMember(o => o.PersonalAccount, map => map.MapFrom(vm => vm.PersonalAccount))
                .ForMember(o => o.Url, map => map.MapFrom(vm => vm.Url))
                .ForMember(o => o.Discription, map => map.MapFrom(vm => vm.Discription))
                .ForMember(o => o.CreatedAccountDate, map => map.MapFrom(vm => vm.CreatedAccountDate))
                .ForMember(o => o.Price, map => map.MapFrom(vm => vm.Price));

            CreateMap<Dialog, DialogViewModel>()
               .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id))

               .ForMember(o => o.Messages, map => map.MapFrom(vm => vm.Messages))
               .ForMember(o => o.Companion, map => map.MapFrom(vm => vm.Companion))
               .ForMember(o => o.Creator, map => map.MapFrom(vm => vm.Creator)); ;

            CreateMap<Dialog, DetailsDialogViewModel>()
               .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id))

               .ForMember(o => o.Messages, map => map.MapFrom(vm => vm.Messages))
               .ForMember(o => o.Companion, map => map.MapFrom(vm => vm.Companion))
               .ForMember(o => o.Creator, map => map.MapFrom(vm => vm.Creator));

            CreateMap<Order, OrderViewModel>()
                .ForPath(o => o.BuyerName, map => map.MapFrom(vm => vm.Buyer.UserName))
                .ForPath(o => o.CurrentStatus, map => map.MapFrom(vm => vm.CurrentStatus.DuringName))
                .ForPath(o => o.SellerName, map => map.MapFrom(vm => vm.Seller.UserName))
                .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(o => o.CreatedDate, map => map.MapFrom(vm => vm.CreatedDate));

            CreateMap<Order, DetailsOrderViewModel>()
                .ForMember(o => o.BuyerFeedbacked, map => map.MapFrom(vm => vm.BuyerFeedbacked))
                .ForMember(o => o.SellerFeedbacked, map => map.MapFrom(vm => vm.SellerFeedbacked))
                .ForPath(o => o.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(o => o.AccountInfos, map => map.MapFrom(vm => vm.AccountInfos))
                .ForMember(o => o.BuyerId, map => map.MapFrom(vm => vm.BuyerId))
                .ForMember(o => o.SellerId, map => map.MapFrom(vm => vm.SellerId))
                .ForPath(o => o.BuyerName, map => map.MapFrom(vm => vm.Buyer.UserName))
                .ForMember(o => o.DateCreated, map => map.MapFrom(vm => vm.CreatedDate))
                .ForPath(o => o.OfferHeader, map => map.MapFrom(vm => vm.Offer.Header))
                .ForPath(o => o.OfferId, map => map.MapFrom(vm => vm.Offer.Id))
                .ForPath(o => o.OfferPrice, map => map.MapFrom(vm => vm.Offer.Price))
                .ForPath(o => o.ModeratorName, map => map.MapFrom(vm => vm.Middleman.UserName))
                .ForPath(o => o.SellerName, map => map.MapFrom(vm => vm.Seller.UserName));
        }
    }
}
