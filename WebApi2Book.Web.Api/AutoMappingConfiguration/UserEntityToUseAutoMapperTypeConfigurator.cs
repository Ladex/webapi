using AutoMapper;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class UserEntityToUseAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfiguration mapperConfiguration)
        {
            mapperConfiguration.CreateMap<User, Models.User>()
                .ForMember(opt => opt.Links, x => x.Ignore());
        }
    }
}