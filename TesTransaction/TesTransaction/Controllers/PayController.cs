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
            ////provisoire
            //vm.GlobalTotal = "123.99";
            //vm.NumTransaction = "999";

            return View(vm);
        }

        [HttpPost]
        public ActionResult MethodChoice(TrPaymentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                switch (vm.MethodP)
                {
                    case "Cash":
                        return PartialView("_PartialPayCash");
                    case "Card":
                        return PartialView("_PartialPayCard");
                    case "Voucher":
                        return PartialView("_PartialPayVoucher");
                    default:
                        return PartialView(vm);
                }
            }
            return PartialView(vm);
        }
    }
}