using System;
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
            //vm.TerminalsNames = TransactionBL.FindTerminals(); // list names terminals
            //vm.TerminalsList = TransactionBL.FindTerminalsList();  // list terminals
            vm.TerminalId = terminal;
            ////transaction num = id
            //vm.NumTransaction = "9999";
            // To do -> vérification si transaction en cours
            vm.NumTransaction = TransactionBL.InitializeNewTransaction(terminal);
            // to do --> quid date et heure?
            vm.DateDay = (DateTime.Now.Date).ToString();
            vm.HourDay = (DateTime.Now.TimeOfDay).ToString("t");

            vm.Vendor = "Toto"; // --> id = 1

            //detail vide ->  provisoire
            //vm.TransactionDetailsListById = TransactionBL.InitializeTransactionDetails();

            return View(vm);
        }

        //POST: 
        public ActionResult RefreshDetails(string codeProduct, string numTransaction, string terminal)
        {
            TrDetailsViewModel vm = new TrDetailsViewModel();

            //Add detail
            TransactionBL.AddNewTransactionDetail(codeProduct, terminal, numTransaction);
            //Find details with id transaction  + Add itemSubTotal
            var detailsList = TransactionBL.FindTransactionDetailsListById(numTransaction);
            vm.DetailsListWithTot = TransactionBL.AddSubTotalPerDetailToList(detailsList);

            return PartialView("_PartialTransactionDetail", vm);
        }
    }
}