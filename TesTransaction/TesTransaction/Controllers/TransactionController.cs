using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Test()
        {
            return View();
        }

        //POST: 
        public ActionResult RefreshDetails(string codeProduct, string numTransaction, string terminal)
        {
            TransactionBL.AddNewTransactionDetail(codeProduct, terminal, numTransaction);
            TrDetailsViewModel vm = new TrDetailsViewModel();
            vm.DetailsListById = TransactionBL.FindTransactionDetailsListById(numTransaction);

            return PartialView("_PartialTransactionDetail", vm);
        }
    }
}