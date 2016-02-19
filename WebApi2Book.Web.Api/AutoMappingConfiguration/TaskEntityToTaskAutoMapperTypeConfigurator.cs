    using AutoMapper;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class TaskEntityToTaskAutoMapperTypeConfigurator:IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfiguration mapperConfiguration)
        {
            mapperConfiguration.CreateMap<Task, Models.Task>()
                .ForMember(opt => opt.Links, x => x.Ignore())
                .ForMember(opt => opt.Assingnee, x => x.ResolveUsing<TaskAssigneeResolver>());
        }
    }
}