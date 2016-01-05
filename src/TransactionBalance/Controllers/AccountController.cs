using System.Threading.Tasks;
using System.Web.Mvc;
using TransactionBalance.Models;
using TransactionService.Interfaces;
using AutoMapper;
using TransactionDataModels;
using System.Web.Security;

namespace TransactionBalance.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountsBusinessService _accountBusinessService;

        public AccountController(IAccountsBusinessService accountBusinessService)
        {
            _accountBusinessService = accountBusinessService;
        }

        [AllowAnonymous]
        public ActionResult Login1()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginUserDto)
        {
            var user = Mapper.Map<User>(loginUserDto);
            var transaction = new TransactionInformationDTO();
            await _accountBusinessService.Login(user, transaction);

            if (transaction.ReturnStatus == false)
            {                
                return Content("Naughty");
            }

            FormsAuthentication.SetAuthCookie(user.UserId.ToString(), false);
            return View(loginUserDto);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {            
            return PartialView();
        }

        //
        // POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // This doesn't count login failures towards account lockout
        //    // To enable password failures to trigger account lockout, change to shouldLockout: true
        //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //            return View(model);
        //    }
        //}   

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return PartialView();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var user = Mapper.Map<User>(model);
            var transaction = new TransactionInformationDTO();
            await _accountBusinessService.RegisterUser(user, model.PasswordConfirmation, transaction);
            
            return View(transaction);
        }        
    }
}