using AutoMapper;
using Marketplace.Api.Areas.User.Automapper;

namespace Marketplace.Api.Automapper
{
    public class AutoMapperConfiguration
    {
        public static IMapper Configure()
        {
            var mapperConfiguration = new MapperConfiguration(x =>
            {

                x.AddProfile(new DomainToViewModelMappingProfile());
                x.AddProfile(new ViewModelToDomainMappingProfile());

                x.AddProfile(new DomainToViewModelUserMappingProfile());
                x.AddProfile(new ViewModelToDomainUserMappingProfile());

                //x.AddProfile<DomainToViewModelAdminMappingProfile>();
                //x.AddProfile<ViewModelToDomainAdminMappingProfile>();

                //x.AddProfile<DomainToViewModelMiddlemanMappingProfile>();

            });

            var mapper = mapperConfiguration.CreateMapper();

            return mapper;
        }
    }
}
