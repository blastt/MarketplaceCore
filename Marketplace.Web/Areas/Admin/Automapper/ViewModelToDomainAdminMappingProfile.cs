using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Web.Areas.Admin.Models;
using Marketplace.Web.Areas.Admin.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Areas.Admin.Automapper
{
    public class ViewModelToDomainAdminMappingProfile : Profile
    {
        public ViewModelToDomainAdminMappingProfile()
        {
            CreateMap<CreateGameViewModel, Game>()
                .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(o => o.Value, map => map.MapFrom(vm => vm.Value))
                .ForMember(o => o.Rank, map => map.MapFrom(vm => vm.Rank));

            CreateMap<CreateFilterRangeViewModel, FilterRange>()
                .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(o => o.Value, map => map.MapFrom(vm => vm.Value))
                .ForMember(o => o.From, map => map.MapFrom(vm => vm.From))
                .ForMember(o => o.To, map => map.MapFrom(vm => vm.To))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CreateFilterTextViewModel, FilterText>()
                .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(o => o.Value, map => map.MapFrom(vm => vm.Value))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<CreateFilterTextValueViewModel, FilterTextValue>()
                .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(o => o.Value, map => map.MapFrom(vm => vm.Value))
                .ForAllOtherMembers(opt => opt.Ignore());
            //CreateMap<CreateFilterViewModel, Filter>()
            //    .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
            //    .ForMember(o => o.Value, map => map.MapFrom(vm => vm.Value))
            //    .ForMember(o => o.Rank, map => map.MapFrom(vm => vm.Rank))
            //    .ForMember(o => o.Type, map => map.MapFrom(vm => vm.Type))
            //    .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
