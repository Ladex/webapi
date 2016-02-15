using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using WebApi2Book.Common.Logging;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            
            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("apiVersionConstraint", typeof(ApiVersionConstraint));
            config.MapHttpAttributeRoutes(constraintsResolver);

            config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));
            config.EnableSystemDiagnosticsTracing();

            config.Services.Replace(typeof(ITraceWriter), new SimpleTraceWriter(WebContainerManager.Get<ILogManager>()));
        }
    }
}
