using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi2Book.Common.TypeMapping;

namespace WebApi2Book.Web.Api
{
    public class AutoMapperConfigurator
    {
        public void Configure(IEnumerable<IAutoMapperTypeConfigurator> autoMapperTypeConfigurators)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                autoMapperTypeConfigurators.ToList().ForEach(x => x.Configure(cfg));
            });
            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}