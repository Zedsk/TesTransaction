using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TesTransaction.BL;
using TesTransaction.Models;

namespace TesTransaction.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index(int terminal)
        {
            TrIndexViewModel vm = new TrIndexViewModel();
            //terminal name or id
            vm.TerminalId = terminal;

            ////transaction num = id
            // To do -> vérification si transaction en cours
            vm.NumTransaction = TransactionBL.InitializeNewTransaction(terminal);

            // to do --> quid date et heure?
            vm.DateDay = DateTime.Now.Date.ToString("d");
            vm.HourDay = DateTime.Now.ToString("T");
            vm.Vendor = "Toto"; // --> id = 1
            vm.VatsList = TransactionBL.FindVatsList();
            
            return View(vm);
        }

        public ActionResult Test()
        {
            return View();
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
        public ActionResult Index(string subTotitem, string discountG, string subTot, TrIndexViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
            //Sum subTotItems 
            ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
            vmodel.DetailsListWithTot = detailsListTot;
            vmodel.VatsList = TransactionBL.FindVatsList();
            return View(vmodel);
        }
    }
}