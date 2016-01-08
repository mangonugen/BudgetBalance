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
    public class UserController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {            
            return PartialView();
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return PartialView();
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Logout()
        {
            return PartialView();
        }   
    }
}