using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesTransaction.BL;
using TesTransaction.Models.Transactions;

namespace TesTransaction.Controllers
{
    public class PayController : Controller
    {
        // GET: Pay
        [HttpGet]
        public ActionResult Index(string gt, string nt)
        {
            try
            {
                TrPaymentMenuViewModel vm = new TrPaymentMenuViewModel();
                if (string.IsNullOrEmpty(gt) || string.IsNullOrEmpty(nt))
                {
                    throw new NullReferenceException();
                }
                else
                {
                    vm.GlobalTotal = gt;
                    vm.NumTransaction = nt;
                    ViewBag.tot = gt;
                    ViewBag.transac = nt;
                }

                vm.MethodsP = TransactionBL.FindMethodsList();
                ViewBag.messageCard = "";
                ViewBag.ticket = false;
                return View(vm);
            }
            catch (NullReferenceException)
            {
                //to do --> add ex to log file
                ViewBag.Error = "Il n'y a pas de transaction en cours !";
                return View("Error");
            }
            catch (Exception ex)
            {
                var temp = ex.GetType();
                //to do --> add ex to log file
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string submitButton, TrPaymentMenuViewModel vmodel)
        {
            switch (submitButton)
            {
                case "Payment":
                    return (Payment(vmodel));

                case "End":
                    return (EndTransac(vmodel));

                case "Cancel":
                    return (CancelTransac(vmodel));

                case "Back":
                    return (BackTransac(vmodel));

                default:
                    ViewBag.ticket = false;
                    return View(vmodel);

            }
        }

        private ActionResult Payment(TrPaymentMenuViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                if (vmodel.GlobalTotal == "0")
                {
                    ViewBag.nopay = "La transaction est payée!";
                    ViewBag.tot = vmodel.GlobalTotal;
                    ViewBag.amount = vmodel.Amount;
                    ViewBag.cashBack = vmodel.CashReturn;
                    vmodel.MethodsP = TransactionBL.FindMethodsList();
                    ViewBag.ticket = true;
                    return View(vmodel);
                }
                TrPaymentMenuViewModel vm = new TrPaymentMenuViewModel();
                switch (vmodel.MethodP)
                {
                    //method cash
                    case "1":
                        return (PayCash(vmodel));

                    //method debit card
                    case "2":
                        //simulation
                        return (PayCardDebit(vmodel));

                    //method credit card
                    case "3":
                        ////simulation
                        //return (PayCardCredit(vmodel));
                        return (PayCardDebit(vmodel));

                    //method voucher
                    case "4":
                        //same process PayCash
                        return (PayCash(vmodel));

                    default:
                        ViewBag.tot = vmodel.GlobalTotal;
                        ViewBag.amount = vmodel.Amount;
                        ViewBag.cashBack = vmodel.CashReturn;
                        vmodel.MethodsP = TransactionBL.FindMethodsList();
                        ViewBag.messageCard = "";
                        ViewBag.ticket = false;
                        return View(vmodel);
                }
            }
            vmodel.MethodsP = TransactionBL.FindMethodsList();
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.cashBack = vmodel.CashReturn;
            vmodel.MethodsP = TransactionBL.FindMethodsList();
            ViewBag.messageCard = "";
            ViewBag.ticket = false;
            return View(vmodel);
        }


        private ActionResult EndTransac(TrPaymentMenuViewModel vmodel)
        {
            if (vmodel.GlobalTotal == "0")
            {
                //to do --> print ticket  ???

                //to do --> add n° ticket & close transaction
                TransactionBL.AddTicketAndCloseTransac(vmodel.NumTransaction, vmodel.NumTicket);
                return RedirectToAction("Transaction", "Home");
            }
            vmodel.MethodsP = TransactionBL.FindMethodsList();
            ViewBag.nopay = "La transaction n'est pas payée!";
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.cashBack = vmodel.CashReturn;
            ViewBag.ticket = false;
            return View(vmodel);
        }

        private ActionResult CancelTransac(TrPaymentMenuViewModel vmodel)
        {
            if (string.IsNullOrEmpty(vmodel.NumTransaction))
            {
                //to do --> ???
                return RedirectToAction("Transaction", "Home");
            }
            else
            {
                TransactionBL.CancelTransac(vmodel.NumTransaction);
                return RedirectToAction("Transaction", "Home");
            }
        }

        private ActionResult BackTransac(TrPaymentMenuViewModel vmodel)
        {

            return RedirectToAction("TransacReturn", "Transaction", vmodel);
        }

        private ActionResult PayCash(TrPaymentMenuViewModel vmodel)
        {
            TransactionBL.CalculCash(vmodel);
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.cashBack = vmodel.CashReturn;
            vmodel.MethodsP = TransactionBL.FindMethodsList();
            ViewBag.messageCard = "";
            if (ViewBag.tot == "0")
            {
                vmodel.Ticket = TransactionBL.FillTicket(vmodel.NumTransaction);
                ViewBag.NumT = vmodel.Ticket.Ticket;
                vmodel.NumTicket = vmodel.Ticket.Ticket;
                ViewBag.ticket = true;
            }
            else
            {
                ViewBag.ticket = false;
            }
            return View(vmodel);
        }

        private ActionResult PayCardDebit(TrPaymentMenuViewModel vmodel)
        {
            vmodel.Resp = TransactionBL.AskValidationCard(vmodel.Amount);
            if (vmodel.Resp == 1)
            {
                TransactionBL.CalculCash(vmodel);
                ViewBag.messageCard = "Demande acceptée !";
                ViewBag.tot = vmodel.GlobalTotal;
                ViewBag.amount = vmodel.Amount;
                ViewBag.cashBack = vmodel.CashReturn;
                if (ViewBag.tot == "0")
                {
                    vmodel.Ticket = TransactionBL.FillTicket(vmodel.NumTransaction);
                    ViewBag.NumT = vmodel.Ticket.Ticket;
                    vmodel.NumTicket = vmodel.Ticket.Ticket;
                    ViewBag.ticket = true;
                }
                else
                {
                    ViewBag.ticket = false;
                }
            }
            else
            {
                ViewBag.messageCard = "Demande refusée !";
                ViewBag.tot = vmodel.GlobalTotal;
                ViewBag.amount = "";
                ViewBag.cashBack = "0";
                ViewBag.ticket = false;
            }
            vmodel.MethodsP = TransactionBL.FindMethodsList();
            return View(vmodel);
        }

        ////// same simulation process PayCardDebit
        //private ActionResult PayCardCredit(TrPaymentMenuViewModel vmodel)
        //{
        //    vmodel.Resp = TransactionBL.AskValidationCard(vmodel.Amount);
        //    if (vmodel.Resp == 1)
        //    {
        //        //to do --> create payment
        //        ViewBag.messageCard = "Demande acceptée !";
        //        ViewBag.tot = vmodel.GlobalTotal;
        //        ViewBag.amount = vmodel.Amount;
        //        ViewBag.cashBack = vmodel.CashReturn;
        //        if (ViewBag.tot == "0")
        //        {
        //            vmodel.Ticket = TransactionBL.FillTicket(vmodel.NumTransaction);
        //            //vmodel.NumTicket = vmodel.Ticket.Ticket;
        //            ViewBag.NumT = vmodel.Ticket.Ticket;
        //            vmodel.NumTicket = vmodel.Ticket.Ticket;
        //            ViewBag.ticket = true;
        //        }
        //        else
        //        {

        //            ViewBag.ticket = false;
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.messageCard = "Demande refusée !";
        //        ViewBag.tot = vmodel.GlobalTotal;
        //        ViewBag.amount = "";
        //        ViewBag.cashBack = "0";
        //        ViewBag.ticket = false;
        //    }
        //    vmodel.MethodsP = TransactionBL.FindMethodsList();
        //    return View(vmodel);
        //}
    }
}