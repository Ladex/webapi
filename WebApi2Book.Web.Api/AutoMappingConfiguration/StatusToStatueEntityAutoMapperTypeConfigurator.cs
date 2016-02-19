using AutoMapper;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class StatusToStatueEntityAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfiguration mapperConfiguration)
        {
            mapperConfiguration.CreateMap<Status, Data.Entities.Status>()
                .ForMember(opt => opt.Version, x => x.Ignore());

        }
    }
}