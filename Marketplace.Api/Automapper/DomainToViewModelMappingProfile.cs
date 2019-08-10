using AutoMapper;
using Marketplace.Api.Areas.User.ViewModels;
using Marketplace.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Automapper
{
    public partial class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        private string GetSplitedUrl(string str, char ch, int number)
        {
            try
            {
                return str.Split(ch)[number].ToString();
            }
            catch (Exception)
            {

                return "Url скрыт";
            }

        }

        public DomainToViewModelMappingProfile()
        {
            //CreateMap<Offer, OfferViewModel>()
            //    .ForMember(o => o.Header, map => map.MapFrom(vm => vm.Header))
            //   //.ForMember(o => o.SellerPaysMiddleman, map => map.MapFrom(vm => vm.SellerPaysMiddleman))
            //   .ForMember(o => o.IsBanned, map => map.MapFrom(vm => vm.IsBanned))
            //   .ForMember(o => o.PersonalAccount, map => map.MapFrom(vm => vm.PersonalAccount))
            //   .ForMember(o => o.Url, map => map.MapFrom(vm => vm.Url))
            //   .ForMember(o => o.Discription, map => map.MapFrom(vm => vm.Discription))
            //   .ForMember(o => o.CreatedAccountDate, map => map.MapFrom(vm => vm.CreatedAccountDate))
            //   .ForMember(o => o.Price, map => map.MapFrom(vm => vm.Price))
            //   .ForMember(o => o.UserName, map => map.MapFrom(vm => vm.UserProfile.User.UserName))
            //   .ForMember(o => o.UserAvatar32, map => map.MapFrom(vm => vm.UserProfile.Avatar32))
            //   .ForMember(o => o.UserAvatar64, map => map.MapFrom(vm => vm.UserProfile.Avatar64))
            //   .ForMember(o => o.UserAvatar96, map => map.MapFrom(vm => vm.UserProfile.Avatar96))
            //   .ForMember(o => o.RefreshDate, map => map.MapFrom(vm => vm.CreatedDate))
            //    .ForMember(o => o.ShortUrl, map => map.MapFrom((vm) => GetSplitedUrl(vm.Url, '/', 4)))
            //    .ForPath(vm => vm.Game, map => map.MapFrom(o => o.Game.Name));

            //CreateMap<Offer, DetailsOfferViewModel>()
            //    .ForMember(o => o.ShortUrl, map => map.MapFrom(vm => GetSplitedUrl(vm.Url, '/', 4)))
            //    .ForPath(o => o.Game, map => map.MapFrom(vm => vm.Game.Name))
            //    .ForPath(o => o.User.Id, map => map.MapFrom(vm => vm.UserProfile.Id))
            //    .ForPath(o => o.User.Avatar32, map => map.MapFrom(vm => vm.UserProfile.Avatar32))
            //    .ForPath(o => o.User.Avatar64, map => map.MapFrom(vm => vm.UserProfile.Avatar64))
            //    .ForPath(o => o.User.Avatar96, map => map.MapFrom(vm => vm.UserProfile.Avatar96))
            //    .ForPath(o => o.User.Name, map => map.MapFrom(vm => vm.UserProfile.User.UserName));



            //CreateMap<UserProfile, UserProfileViewModel>()
            //   .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id));

            //CreateMap<UserProfile, DetailsUserProfileViewModel>()
            //    .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id))
            //    .ForMember(o => o.Name, map => map.MapFrom(vm => vm.User.UserName))
            //    .ForMember(o => o.Avatar32, map => map.MapFrom(vm => vm.Avatar32))
            //.ForMember(o => o.Avatar64, map => map.MapFrom(vm => vm.Avatar64))
            //   .ForMember(o => o.Avatar96, map => map.MapFrom(vm => vm.Avatar96));

            //CreateMap<FilterRange, FilterRangeViewModel>()
            //   .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
            //   .ForMember(o => o.Value, map => map.MapFrom(vm => vm.Value))
            //   .ForMember(o => o.From, map => map.MapFrom(vm => vm.From))
            //   .ForMember(o => o.To, map => map.MapFrom(vm => vm.To));

            //CreateMap<FilterBoolean, FilterBooleanViewModel>()
            //   .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
            //   .ForMember(o => o.Value, map => map.MapFrom(vm => vm.Value));

            //CreateMap<FilterText, FilterTextViewModel>()
            //    .ForMember(o => o.PredefinedValues, map => map.MapFrom(vm => vm.PredefinedValues))
            //  .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
            //   .ForMember(o => o.Value, map => map.MapFrom(vm => vm.Value));

            //CreateMap<FilterTextValue, FilterTextValueViewModel>()
            //  .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
            //   .ForMember(o => o.Value, map => map.MapFrom(vm => vm.Value));
        }
    }
}
