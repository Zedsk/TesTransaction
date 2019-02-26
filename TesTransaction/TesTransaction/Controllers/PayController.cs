using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesTransaction.Models;

namespace TesTransaction.Controllers
{
    public class PayController : Controller
    {
        // GET: Pay
        public ActionResult Index(TrPaymentViewModel vm)
        {
            return View(vm);
        }
    }
}