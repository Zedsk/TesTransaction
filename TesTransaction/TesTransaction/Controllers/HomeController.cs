using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesTransaction.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Transaction()
        {
            if (TempData["Error"] == null)
            {
                return View("Transaction");
            }
            else
            {
                ViewBag.Error = TempData["Error"].ToString();
                return View("Transaction");
            }
        }
    }
}