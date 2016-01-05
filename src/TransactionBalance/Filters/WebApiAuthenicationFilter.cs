using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using TransactionDataModels;

namespace TransactionBalance.Filters
{
    /// <summary>
    /// Authenicate User
    /// </summary>
    public class WebApiAuthenicationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var headers = request.Headers;
            if (!headers.Contains("X-Requested-With") || headers.GetValues("X-Requested-With").FirstOrDefault() != "XMLHttpRequest")
            {
                TransactionInformation transactionInformation = new TransactionInformation();
                transactionInformation.ReturnMessage.Add("Access has been denied.");
                transactionInformation.ReturnStatus = false;
                actionContext.Response = request.CreateResponse(HttpStatusCode.BadRequest, transactionInformation);
            }
            else
            {
                HttpContext ctx = default(HttpContext);
                ctx = HttpContext.Current;
                if (ctx.User.Identity.IsAuthenticated == false)
                {
                    TransactionInformation transactionInformation = new TransactionInformation();
                    transactionInformation.ReturnMessage.Add("Your session has expired.");
                    transactionInformation.ReturnStatus = false;
                    actionContext.Response = request.CreateResponse(HttpStatusCode.BadRequest, transactionInformation);
                }

            }
        }
    }
}