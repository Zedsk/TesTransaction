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
            ////terminal name or id
            //vm.TerminalsNames = TransactionBL.FindTerminalsNames(); // list names terminals
            //vm.TerminalsList = TransactionBL.FindTerminalsList();  // list terminals
            vm.TerminalId = terminal;

            ////transaction num = id
            //vm.NumTransaction = "9999";
            // To do -> vérification si transaction en cours
            vm.NumTransaction = TransactionBL.InitializeNewTransaction(terminal);

            // to do --> quid date et heure?
            vm.DateDay = DateTime.Now.Date.ToString("d");
            vm.HourDay = DateTime.Now.ToString("T");

            vm.Vendor = "Toto"; // --> id = 1

            //detail vide ->  provisoire
            //vm.TransactionDetailsListById = TransactionBL.InitializeTransactionDetails();
            vm.VatsList = TransactionBL.FindVatsList();
            //ViewBag.subTot1 = 0;
            return View(vm);
        }

        public ActionResult Test()
        {
            return View();
        }

        //POST:
        [HttpPost]
        //public ActionResult RefreshDetails(string addProduct, string numTransaction, string terminalId, bool minus, TrIndexViewModel vmodel)
        public ActionResult RefreshDetails(string numTransaction, string terminalId, TrDetailsViewModel vmodel)
        {
            //TrDetailsViewModel vm = new TrDetailsViewModel();
            ////Add detail
            //TransactionBL.AddNewTransactionDetail(addProduct, terminalId, numTransaction, minus);
            ////Find details with id transaction  + Add itemSubTotal
            //var detailsList = TransactionBL.FindTransactionDetailsListById(numTransaction);
            //var detailsListTot = TransactionBL.AddSubTotalPerDetailToList(detailsList);
            ////Sum subTotItems 
            //ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
            //vm.DetailsListWithTot = detailsListTot;
            ////vm.DetailsListById = detailsList;

            //return PartialView("_PartialTransactionDetail", vm);

            if (ModelState.IsValid)
            {
                //TrDetailsViewModel vm = new TrDetailsViewModel();
                ////Add detail
                TransactionBL.AddNewTransactionDetail(vmodel.AddProduct, terminalId, numTransaction, vmodel.Minus);
                ////Find details with id transaction  + Add itemSubTotal
                //var detailsListTot = TransactionBL.ListDetailsWithTot(numTransaction);
                ////Sum subTotItems 
                //ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
                //vm.DetailsListWithTot = detailsListTot;
                //vm.DetailsListById = detailsList;

                //return PartialView("_PartialTransactionDetail", vm);
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
        //public ActionResult PaymentTransaction(string numTransaction, string terminalId, string vendor, string subTotitem, string discountG, string subTot, string vat, string globalTotal)
        //public ActionResult PaymentTransaction(string subTotitem, string discountG, string subTot, string vat, TrIndexViewModel vmodel)
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