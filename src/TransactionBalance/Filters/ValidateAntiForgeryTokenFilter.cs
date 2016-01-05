using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TransactionBalance.Filters
{
    
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        //http://stackoverflow.com/questions/11476883/web-api-and-validateantiforgerytoken
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            try
            {
                //http://geekswithblogs.net/Frez/archive/2015/01/11/anti-forgery-tokens-with-angularjs-and-asp.net-web-api.aspx
                if (actionContext == null)
                {
                    throw new ArgumentNullException("actionContext");
                }
                var headers = actionContext.Request.Headers;
                var cookie = headers
                    .GetCookies()
                    .Select(c => c[AntiForgeryConfig.CookieName])
                    .FirstOrDefault();
                var tokenFromHeader = "";
                if (headers.Contains("X-XSRF-Token"))
                    tokenFromHeader = headers.GetValues("X-XSRF-Token").FirstOrDefault();
                AntiForgery.Validate(cookie != null ? cookie.Value : null, tokenFromHeader);
            }
            catch
            {
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    RequestMessage = actionContext.ControllerContext.Request
                };
                return FromResult(actionContext.Response);
            }
            return continuation();
        }

        private Task<HttpResponseMessage> FromResult(HttpResponseMessage result)
        {
            var source = new TaskCompletionSource<HttpResponseMessage>();
            source.SetResult(result);
            return source.Task;
        }
    }
}