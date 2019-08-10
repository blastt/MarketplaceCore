using AutoMapper;
using Marketplace.Api.Areas.User.Automapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Automapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {

                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();

                x.AddProfile<DomainToViewModelUserMappingProfile>();
                x.AddProfile<ViewModelToDomainUserMappingProfile>();

                //x.AddProfile<DomainToViewModelAdminMappingProfile>();
                //x.AddProfile<ViewModelToDomainAdminMappingProfile>();

                //x.AddProfile<DomainToViewModelMiddlemanMappingProfile>();

            });
        }
    }
}
