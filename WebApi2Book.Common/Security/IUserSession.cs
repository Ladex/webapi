using System;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Linq;

namespace WebApi2Book.Common.Security
{
    public interface IUserSession
    {
        string Firstname { get; }

        string Lastname { get; }

        string Username { get; }

        bool IsInRole(string rolename);
    }


    public interface IWebUserSession : IUserSession
    {
        string ApiVersionInUse { get; }

        Uri RequestUri { get; }

        string HttpRequestMethod { get; }
    }


    public class UserSession : IWebUserSession
    {
        public string Firstname
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst(ClaimTypes.GivenName).Value; }
        }

        public string Lastname
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst(ClaimTypes.Surname).Value; }

        }

        public string Username
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst(ClaimTypes.Name).Value; }
        }

        public bool IsInRole(string rolename)
        {
            return HttpContext.Current.User.IsInRole(rolename);
        }

        public string ApiVersionInUse
        {
            get
            {
                const int versionIndex = 2;
                if (HttpContext.Current.Request.Url.Segments.Count() < versionIndex + 1)
                {
                    return string.Empty;
                }

                var apiVersionInUse = HttpContext.Current.Request.Url.Segments[versionIndex].Replace(
                    "/", string.Empty);
                return apiVersionInUse;
            }
        }

        public Uri RequestUri
        {
            get
            {
                return HttpContext.Current.Request.Url;
            }
        }

        public string HttpRequestMethod
        {
            get
            {
                return HttpContext.Current.Request.HttpMethod;
            }
        }
    }
}