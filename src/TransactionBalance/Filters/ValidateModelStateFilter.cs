using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TransactionBalance.Infrastructure;
using TransactionBalance.Models;

namespace TransactionBalance.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpContext ctx = default(HttpContext);
            ctx = HttpContext.Current;

            var request = actionContext.Request;
            if (!actionContext.ModelState.IsValid)
            {
                var transactionInformation = new TransactionInformationDTO();

                transactionInformation.ReturnMessage = ModelStateHelper.ReturnErrorMessages(actionContext.ModelState.Values);
                transactionInformation.ValidationErrors = ModelStateHelper.ReturnValidationErrors(actionContext.ModelState);
                transactionInformation.ReturnStatus = false;
                transactionInformation.IsAuthenicated = ctx.User.Identity.IsAuthenticated;
                actionContext.Response = request.CreateResponse(HttpStatusCode.BadRequest, transactionInformation);
            }
        }
    }
}