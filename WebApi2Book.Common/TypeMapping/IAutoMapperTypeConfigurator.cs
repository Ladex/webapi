using AutoMapper;

namespace WebApi2Book.Common.TypeMapping
{
    public interface IAutoMapperTypeConfigurator
    {
        void Configure(IMapperConfiguration mapperConfiguration);
    }
}