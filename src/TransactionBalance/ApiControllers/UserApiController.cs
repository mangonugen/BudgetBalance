using System.Web;

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using TransactionBalance.Filters;
using TransactionBalance.Models;
using TransactionDataModels;
using TransactionService.Interfaces;

namespace TransactionBalance.ApiControllers
{
    [RoutePrefix("api/user")]
    public class UserApiController : ApiController
    {
        private readonly IUsersBusinessService _userBusinessService;

        public UserApiController(IUsersBusinessService userBusinessService)
        {
            this._userBusinessService = userBusinessService;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="registerUserDTO"></param>
        /// <returns></returns>
        [Route("RegisterUser")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterUser([FromBody] RegisterViewModel registerUserDTO)
        {
            var user = Mapper.Map<User>(registerUserDTO);
            var transaction = new TransactionInformationDTO();
            await this._userBusinessService.RegisterUser(user, registerUserDTO.PasswordConfirmation, transaction);

            if (transaction.ReturnStatus == false)
            {
                return Content(HttpStatusCode.BadRequest, transaction);
            }

            FormsAuthentication.SetAuthCookie(user.UserId.ToString(), false);
            //SetCookie(user);
            transaction.IsAuthenticated = true;
            return Ok(transaction);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginUserDto"></param>
        /// <returns></returns>
        [Route("Login")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginViewModel loginUserDto)
        {
            var user = Mapper.Map<User>(loginUserDto);
            var transaction = new TransactionInformationDTO();
            await this._userBusinessService.Login(user, transaction);

            if (transaction.ReturnStatus == false)
            {
                return Content(HttpStatusCode.BadRequest, transaction);
            }

            FormsAuthentication.SetAuthCookie(user.UserId.ToString(), false);
            //SetCookie(user);
            transaction.IsAuthenticated = true;
            return Ok(transaction);
        }

        [Route("Logout")]
        [HttpGet]
        public HttpResponseMessage Logout()
        {
            var transaction = new TransactionInformationDTO();
            //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, " ") { Expires = DateTime.Now.AddDays(-1) };
            //HttpContext.Current.Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            transaction.IsAuthenticated = false;
            var response = Request.CreateResponse(HttpStatusCode.OK, transaction);
            return response;
        }

        /// <summary>
        /// Is User Authenicated
        /// </summary>
        /// <returns>The <see cref="TransactionInformationDTO"/></returns>
        [Route("IsUserAuthenicated")]
        [HttpGet]
        public IHttpActionResult IsUserAuthenicated()
        {
            var transaction = new TransactionInformationDTO();
            if (User.Identity.IsAuthenticated)
            {
                transaction.IsAuthenticated = true;
            }

            return Ok(transaction);
        }

        private void SetCookie(User user)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                user.UserId.ToString(),
                DateTime.Now,
                DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                false,
                string.Empty, //Roles.GetRolesForUser(user.UserName).ToString(), //set user role in cookies
                FormsAuthentication.FormsCookiePath);
            HttpContext.Current.Response.Cookies.Add(
                new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(ticket)));
        }
    }
}
