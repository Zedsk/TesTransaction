using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TesTransaction.BL;
using TesTransaction.Models.Transactions;

namespace TesTransaction.Controllers
{
    public class TransactionController : Controller
    {
        #region Index
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
                    // to do --> provisoire vendorId = 1, shopId = 1, customerId = 1
                    NumTransaction = TransactionBL.InitializeNewTransaction(terminal),

                    // to do --> quid date et heure?
                    DateDay = DateTime.Now.Date.ToString("d"),
                    HourDay = DateTime.Now.ToString("T"),

                    Vendor = "Toto", // --> id = 1
                };
                return View(vm);
            }
            catch (EntityException ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log
                TempData["Error"] = "L'initialisation de la transaction ne s'est pas déroulé correctement, veuillez contacter l'administrateur";
                return RedirectToAction("Transaction", "Home");
            }
            catch (InvalidOperationException ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log
                TempData["Error"] = "Il manque un fond de caisse pour cette date";
                return RedirectToAction("Transaction", "Home");
            }
            catch (Exception ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log
                TempData["Error"] = "Il y a un soucis avec l'action demandé, veuillez contacter l'administrateur";
                return RedirectToAction("Transaction", "Home");
            }


        }

        [HandleError]
        public ActionResult TransacReturn(TrPaymentMenuViewModel vmodel)
        {
            try
            {
                var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
                var transac = TransactionBL.FindTransactionById(vmodel.NumTransaction);
                TrIndexViewModel vm = new TrIndexViewModel
                {
                    //vm.TerminalId = terminal;
                    NumTransaction = vmodel.NumTransaction,
                    TerminalId = transac.terminalId,
                    GlobalTot = transac.total.ToString(),
                    GlobalDiscount = (transac.discountGlobal)*100,
                    // to do --> quid date et heure?
                    DateDay = DateTime.Now.Date.ToString("d"),
                    HourDay = DateTime.Now.ToString("T"),
                    //to do --> ameliorer   Vendor = string  et vendorId = int
                    Vendor = (transac.vendorId).ToString(),
                    //to do or not--> transac.discountGlobal à afficher
                    //to do or not--> with transac.vatId  return appliedVat
                    DetailsListWithTot = detailsListTot
                };
                //Sum subTotItems 
                ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
                return View("Index", vm);
            }
            catch (InvalidOperationException ex)
            {
                //to do insert to log file
                //NumTransaction not valid
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log
                return RedirectToAction("Index", "Pay", vmodel);
            }
            catch (Exception ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log
                return View("Error");
            }
        }

        //POST:
        [HandleError]
        [HttpPost]
        public ActionResult Index(string submitButton, string globalDiscount, TrIndexViewModel vmodel)
        {
            try
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
            catch (Exception ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                return View("Error");
            }
        }
        #endregion

        #region Detail
        [HandleError]
        [HttpPost]
        public ActionResult RefreshDetails(string numTransaction, string terminalId, TrDetailsViewModel vmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ////Add detail
                    TransactionBL.AddNewTransactionDetail(vmodel.AddProduct, terminalId, numTransaction, vmodel.Minus);
                }
                //Find details with id transaction  + Add itemSubTotal
                var detailsListTot = TransactionBL.ListDetailsWithTot(numTransaction);
                //Sum subTotItems 
                ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
                vmodel.DetailsListWithTot = detailsListTot;
                return PartialView("_PartialTransactionDetail", vmodel);
            }
            catch (InvalidOperationException ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                ViewBag.ErrorAdd = "N° de produit invalide";
                return PartialView("_PartialTransactionDetail", vmodel);
            }
            catch (Exception ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                return View("Error");
            }
        }
        #endregion

        #region Search
        //POST:
        [HttpPost]
        public ActionResult SearchProduct(string product)
        {
            TrSearchViewModel vm = new TrSearchViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    if (int.TryParse(product, out int codeP))
                    {
                        vm.Products = TransactionBL.FindAllProductByCode(product);
                    }
                    else
                    {
                        //to do 
                        //vm.Products = TransactionBL.FindAllProductByName(product);
                    }
                }
                return PartialView("_PartialTransactionSearch", vm);
            }
            catch (InvalidOperationException ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                ViewBag.ErrorSearch = "N° de produit invalide";
                return PartialView("_PartialTransactionSearch", vm);
            }
            catch (Exception ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                return View("Error");
            }
        }

        //POST:
        [HttpPost]
        public ActionResult SearchBy(string method)
        {
            try
            {
                TrSearchViewModel vm = new TrSearchViewModel();
                string meth = method.ToLower();
                switch (meth)
                {
                    case "brand":
                        return (SearchByBrand(vm));

                    case "hero":
                        return (SearchByHero(vm));

                    case "age":
                        return (SearchByAge(vm));

                    case "cat":
                        return (SearchByCat(vm));

                    default:
                        ViewBag.ticket = false;
                        return PartialView("_PartialTransactionSearch", vm);

                }
            }
            catch (Exception ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                return View("Error");
            }
        }

        private ActionResult SearchByCat(TrSearchViewModel vm)
        {
            vm.Cats = TransactionBL.FindCatsList();
            return PartialView("_PartialTransactionSearch", vm);
        }

        private ActionResult SearchByAge(TrSearchViewModel vm)
        {
            vm.Ages = TransactionBL.FindAgesList();
            return PartialView("_PartialTransactionSearch", vm);
        }

        private ActionResult SearchByHero(TrSearchViewModel vm)
        {
            vm.Heros = TransactionBL.FindHerosList();
            return PartialView("_PartialTransactionSearch", vm);
        }

        private ActionResult SearchByBrand(TrSearchViewModel vm)
        {
            vm.Brands = TransactionBL.FindBrandsList();
            return PartialView("_PartialTransactionSearch", vm);
        }

        //POST:
        [HttpPost]
        public ActionResult ProductBy(string method, string argument, TrSearchViewModel vmodel)
        {
            try
            {
                string meth = method.ToLower();
                switch (meth)
                {
                    case "brand":
                        return (ProductByBrand(argument, vmodel));

                    case "hero":
                        return (ProductByHero(argument, vmodel));

                    case "age":
                        return (ProductByAge(argument, vmodel));

                    case "cat":
                        return (ProductByCat(argument, vmodel));

                    default:
                        ViewBag.ticket = false;
                        return PartialView("_PartialTransactionSearch", vmodel);

                }
            }
            catch (Exception ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                return View("Error");
            }
        }

        private ActionResult ProductByBrand(string argument, TrSearchViewModel vmodel)
        {
            vmodel.Products = TransactionBL.FindProductListByIdBrand(argument);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        private ActionResult ProductByHero(string argument, TrSearchViewModel vmodel)
        {
            vmodel.Products = TransactionBL.FindProductListByIdHero(argument);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        private ActionResult ProductByAge(string argument, TrSearchViewModel vmodel)
        {
            vmodel.Products = TransactionBL.FindProductListByIdAge(argument);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        private ActionResult ProductByCat(string argument, TrSearchViewModel vmodel)
        {
            vmodel.Products = TransactionBL.FindProductListByIdCat(argument);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        #endregion

        #region Payment
        private ActionResult Payment(string globalDiscount, TrIndexViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                //save part of transaction
                TransactionBL.SaveTransactionBeforePayment(vmodel.NumTransaction, vmodel.GlobalTot, globalDiscount);
                var gTot = vmodel.GlobalTot;
                var nTransac = vmodel.NumTransaction;
                return RedirectToAction("Index", "Pay", new { gTot, nTransac });
            }
            var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
            //Sum subTotItems 
            ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
            vmodel.DetailsListWithTot = detailsListTot;
            //vmodel.VatsList = TransactionBL.FindVatsList();
            return View(vmodel);
        }
        #endregion

        #region Cancel
        private ActionResult CancelTransac(TrIndexViewModel vmodel)
        {
            if (string.IsNullOrEmpty(vmodel.NumTransaction))
            {
                var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
                //Sum subTotItems 
                ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
                vmodel.DetailsListWithTot = detailsListTot;
                return View("Index", vmodel);
            }
            TransactionBL.CancelTransac(vmodel.NumTransaction);
            return RedirectToAction("Transaction", "Home");

        }
        #endregion

    }
}