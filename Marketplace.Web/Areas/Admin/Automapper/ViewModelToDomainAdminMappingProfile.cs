using AutoMapper;
using Marketplace.Model.Models;
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
        }
    }
}
