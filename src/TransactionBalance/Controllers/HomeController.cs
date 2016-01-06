using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransactionBalance.Controllers
{
    public class HomeController : Controller //AsyncController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Default()
        {
            return PartialView();
        }

        public ActionResult MenuItems()
        {
            return PartialView("_MenuPartial");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}