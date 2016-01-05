using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using TransactionBalance.Models;
using TransactionRepository;
using TransactionRepository.Interfaces;

namespace TransactionBalance.ApiControllers
{
    [RoutePrefix("api/main")]
    public class MainApiController : ApiController
    {
        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public MainApiController()
        {
        }


        /// <summary>
        /// Initialize Application
        /// </summary>
        /// <returns></returns>
        [Route("AuthenicateUser")]
        [HttpGet]
        public HttpResponseMessage AuthenicateUser([FromUri] string route)
        {
            TransactionInformationDTO transaction = new TransactionInformationDTO();
            transaction.IsAuthenicated = User.Identity.IsAuthenticated;
            var response = Request.CreateResponse(HttpStatusCode.OK, transaction);
            return response;

        }

        [Route("Logout")]
        [HttpGet]
        public HttpResponseMessage Logout()
        {
            TransactionInformationDTO transaction = new TransactionInformationDTO();
            FormsAuthentication.SignOut();
            transaction.IsAuthenicated = false;
            var response = Request.CreateResponse(HttpStatusCode.OK, transaction);
            return response;

        }

        /// <summary>
        /// Initialize Application
        /// </summary>
        /// <returns></returns>
        [Route("InitializeApplication")]
        [HttpGet]
        public async Task<IHttpActionResult> InitializeApplication()
        {
            //ApplicationApiModel applicationWebApiModel = new ApplicationApiModel();
            //TransactionInformationDTO transaction = new TransactionInformationDTO();
            //ApplicationInitializationBusinessService initializationBusinessService;

            //initializationBusinessService = new ApplicationInitializationBusinessService(applicationDataService);
            //initializationBusinessService.InitializeApplication(out transaction);

            //if (transaction.ReturnStatus == false)
            //{
            //    applicationWebApiModel.ReturnMessage = transaction.ReturnMessage;
            //    applicationWebApiModel.ReturnStatus = transaction.ReturnStatus;
            //    var badResponse = Request.CreateResponse<ApplicationApiModel>(HttpStatusCode.BadRequest, applicationWebApiModel);
            //    return badResponse;
            //}

            //initializationBusinessService = new ApplicationInitializationBusinessService(applicationDataService);
            //List<ApplicationMenu> menuItems = initializationBusinessService.GetMenuItems(User.Identity.IsAuthenticated, out transaction);

            //if (transaction.ReturnStatus == false)
            //{
            //    applicationWebApiModel.ReturnMessage = transaction.ReturnMessage;
            //    applicationWebApiModel.ReturnStatus = transaction.ReturnStatus;
            //    var badResponse = Request.CreateResponse<ApplicationApiModel>(HttpStatusCode.BadRequest, applicationWebApiModel);
            //    return badResponse;
            //}

            //applicationWebApiModel.ReturnMessage.Add("Application has been initialized.");
            //applicationWebApiModel.ReturnStatus = transaction.ReturnStatus;
            //applicationWebApiModel.MenuItems = menuItems;
            //applicationWebApiModel.IsAuthenicated = User.Identity.IsAuthenticated;

            //var response = Request.CreateResponse<ApplicationApiModel>(HttpStatusCode.OK, applicationWebApiModel);
            //return response;
            var transaction = new TransactionInformationDTO();
            if(User.Identity.IsAuthenticated)
            {
                transaction.IsAuthenicated = true;
            }

            return Ok(transaction);
        }

    }
}
