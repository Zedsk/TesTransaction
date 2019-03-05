using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TesTransaction.BL;
using TesTransaction.Models.Transactions;

namespace TesTransaction.Controllers
{
    public class TransactionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                int terminal = TransactionBL.FindTerminalIdByDate();
                TrIndexViewModel vm = new TrIndexViewModel
                {
                    ////terminal name or id
                    TerminalId = terminal,
                    ////transaction num = id
                    // To do -> vérification si transaction en cours
                    // to do --> provisoire vendorId = 1, shopId = 1, customerId = 1
                    NumTransaction = TransactionBL.InitializeNewTransaction(terminal),
                    // to do --> quid date et heure?
                    DateDay = DateTime.Now.Date.ToString("d"),
                    HourDay = DateTime.Now.ToString("T"),
                    Vendor = "Toto", // --> id = 1
                    VatsList = TransactionBL.FindVatsList()
                };
                return View(vm);
            }
            catch (InvalidOperationException)
            {
                //Viewbag not work with RedirectToAction --> use TempData
                //ViewBag.Error = "Il manque un fond de caisse pour cette date";
                TempData["Error"] = "Il manque un fond de caisse pour cette date";
                return RedirectToAction("Transaction", "Home");
            }
            catch (Exception)
            {
                TempData["Error"] = "Il y a un soucis avec l'action demandé, veuillez contacter l'administrateur";
                return RedirectToAction("Transaction", "Home");
            }
        }

        [HttpGet]
        public ActionResult TransacReturn(TrPaymentMenuViewModel vmodel)
        {
            var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
            var transac = TransactionBL.FindTransactionById(vmodel.NumTransaction);
            TrIndexViewModel vm = new TrIndexViewModel
            {
                //vm.TerminalId = terminal;
                NumTransaction = vmodel.NumTransaction,
                TerminalId = transac.terminalId,
                // to do --> quid date et heure?
                DateDay = DateTime.Now.Date.ToString("d"),
                HourDay = DateTime.Now.ToString("T"),
                //to do --> ameliorer   Vendor = string  et vendorId = int
                Vendor = (transac.vendorId).ToString(),
                //to do or not--> transac.discountGlobal à afficher
                //to do or not--> with transac.vatId  return appliedVat
                VatsList = TransactionBL.FindVatsList(),
                DetailsListWithTot = detailsListTot
            };
            //Sum subTotItems 
            ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
            return View("Index", vm);
        }

        //POST:
        [HttpPost]
        public ActionResult RefreshDetails(string numTransaction, string terminalId, TrDetailsViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                //Add detail
                TransactionBL.AddNewTransactionDetail(vmodel.AddProduct, terminalId, numTransaction, vmodel.Minus);
            }
            //Find details with id transaction  + Add itemSubTotal
            var detailsListTot = TransactionBL.ListDetailsWithTot(numTransaction);
            //Sum subTotItems 
            ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
            vmodel.DetailsListWithTot = detailsListTot;
            return PartialView("_PartialTransactionDetail", vmodel);
        }

        //POST:
        [HttpPost]
        public ActionResult Index(string submitButton, string globalDiscount, TrIndexViewModel vmodel)
        {
            switch (submitButton)
            {
                case "Payment":
                    return (Payment(globalDiscount, vmodel));

                case "Cancel":
                    return (CancelTransac(vmodel));

                default:
                    ViewBag.ticket = false;
                    return View(vmodel);
            }
        }

        private ActionResult Payment(string globalDiscount, TrIndexViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                //save part of transaction
                TransactionBL.SaveTransactionBeforePayment(vmodel.NumTransaction, vmodel.GlobalTotal, globalDiscount, vmodel.GlobalVAT);
                var gt = vmodel.GlobalTotal;
                var nt = vmodel.NumTransaction;
                return RedirectToAction("Index", "Pay", new { gt, nt });
            }
            var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
            //Sum subTotItems 
            ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
            vmodel.DetailsListWithTot = detailsListTot;
            vmodel.VatsList = TransactionBL.FindVatsList();
            return View(vmodel);
        }

        private ActionResult CancelTransac(TrIndexViewModel vmodel)
        {
            if (string.IsNullOrEmpty(vmodel.NumTransaction))
            {
                var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
                //Sum subTotItems 
                ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
                vmodel.DetailsListWithTot = detailsListTot;
                vmodel.VatsList = TransactionBL.FindVatsList();
                return View("Index", vmodel);
            }
            TransactionBL.CancelTransac(vmodel.NumTransaction);
            return RedirectToAction("Transaction", "Home");

        }
    }
}