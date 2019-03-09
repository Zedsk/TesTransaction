using System;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;
using TesTransaction.Dal;
using System.Web.Mvc;
using System.Collections.Generic;
using TesTransaction.Models;
using TesTransaction.Models.Transactions;

namespace TesTransaction.BL
{
    public class TransactionBL
    {
        #region Terminal
        internal static TERMINAL FindTerminalById(int id)
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetTerminalById(id);
            }
        }

        internal static IList<string> FindTerminalsNames()
        {
            using (IDal dal = new TransactionDal())
            {
                List<TERMINAL> terminals = dal.GetAllTerminals();
                List<string> terminalsNames = new List<string>();
                foreach (var t in terminals)
                {
                    terminalsNames.Add(t.nameTerminal);
                }
                return terminalsNames;
            }
        }

        internal static IList<TERMINAL> FindTerminalsList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllTerminals();
            }
        }

        internal static int FindTerminalIdByDate()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetTerminalIdByDate();
            }
        }

        #endregion

        #region Transaction
        internal static string InitializeNewTransaction(int terminal)
        {
            using (IDal dal = new TransactionDal())
            {
                // to do --> provisoire vendorId = 1, shopId = 1, customerId = 1
                int t = dal.CreateTransaction(terminal);
                return t.ToString();
            }
        }

        internal static IList<TRANSACTION_DETAILS> InitializeTransactionDetails()
        {
            IList<TRANSACTION_DETAILS> result = new List<TRANSACTION_DETAILS>();
            TRANSACTION_DETAILS detail = new TRANSACTION_DETAILS();
            result.Add(detail);
            return result;

        }

        internal static void AddNewTransactionDetail(string codeProduct, string terminal, string transaction, bool minus)
        {
            using (IDal dal = new TransactionDal())
            {
                //find productId with codeProduct
                var prod = dal.GetProductByCode(codeProduct);
                //verify if product exist in detail and Add or Remove itemDetail
                IList<TRANSACTION_DETAILS> detailList = FindTransactionDetailsListById(transaction);
                var result = VerifyProductInDetail(prod.idProduct, detailList);
                if (result)
                {
                    //Product exist --> Modify qty
                    foreach (var item in detailList)
                    {
                        if (item.productId == prod.idProduct)
                        {
                            var newqty = 0;
                            if (minus)
                            {
                                //qty--
                                newqty = item.quantity - 1;
                                if (newqty == 0)
                                {
                                    dal.DeleteDetail(item.idTransactionDetails);
                                    break;
                                }
                                dal.EditQtyToDetailById(item.idTransactionDetails, newqty);
                                break;
                            }
                            else
                            {
                                //qty++
                                newqty = item.quantity + 1;
                                dal.EditQtyToDetailById(item.idTransactionDetails, newqty);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    //Add new detail --> param product, terminalId, transactionId, vatItem 
                    var vatItem = dal.GetVatValById(prod.vatId);
                    int terminalId = int.Parse(terminal);
                    int transactionId = int.Parse(transaction);
                    dal.CreateDetail(prod, terminalId, transactionId, vatItem);
                }
            }
        }

        private static bool VerifyProductInDetail(int idProd, IList<TRANSACTION_DETAILS> detailList)
        {
            var result = false;
            if (detailList.Count != 0)
            {
                foreach (var item in detailList)
                {
                    if (item.productId == idProd)
                    {
                        result = true;
                    }
                }
                return result;
            }
            else
            {
                return result;
            }
        }

        internal static IList<TRANSACTION_DETAILS> FindTransactionDetailsListById(string transaction)
        {
            using (IDal dal = new TransactionDal())
            {
                int transactionId = int.Parse(transaction);
                return dal.GetAllDetailsByTransactionId(transactionId);
            }

        }

        internal static TRANSACTIONS FindTransactionById(string numTransaction)
        {
            using (IDal dal = new TransactionDal())
            {
                int transactionId = int.Parse(numTransaction);
                return dal.GetTransactionById(transactionId);
            }
        }

        internal static IList<TrDetailsViewModel> AddSubTotalPerDetailToList(IList<TRANSACTION_DETAILS> detailsList)
        {
            IList<TrDetailsViewModel> vmList = new List<TrDetailsViewModel>();
            foreach (var item in detailsList)
            {
                var p = item.price;
                var q = item.quantity;
                var d = item.Discount;
                decimal? st;
                if (p == 0 || q == 0)
                {
                    st = 0;
                }
                else if (d == 0 || d == null)
                {
                    st = (p * q);
                }
                else
                {
                    //ex: (100*2)*0.05 = 10
                    var temp = (p * q) * d;
                    st = (p * q) - temp;
                }

                TrDetailsViewModel vm = new TrDetailsViewModel
                {
                    ProductName = item.nameItem,
                    Price = p,
                    Quantity = q,
                    ProductVat = item.vatItem,
                    Discount = item.Discount,
                    TotalItem = st
                };
                vmList.Add(vm);
            }

            return vmList;
        }

        internal static decimal? SumItemsSubTot(IList<TrDetailsViewModel> detailsListTot)
        {
            decimal? result = 0;
            foreach (var vm in detailsListTot)
            {
                result += vm.TotalItem;
            }
            return result;
        }

        internal static IList<TrDetailsViewModel> ListDetailsWithTot(string numTransaction)
        {
            var detailsList = FindTransactionDetailsListById(numTransaction);
            var detailsListTot = TransactionBL.AddSubTotalPerDetailToList(detailsList);
            return detailsListTot;

        }

        internal static void SaveTransactionBeforePayment(string numTransaction, string globalTotal, string discountG, decimal globalVAT)
        {
            using (IDal dal = new TransactionDal())
            {
                var idTr = int.Parse(numTransaction);
                //var gTot = decimal.Parse(globalTotal);
                //probleme le string "157.98" devient 15798
                var temp = globalTotal.Replace(".", ",");
                var gTot = decimal.Parse(temp);
                //find idVAT by name
                var gVat = FindVatIdByVal(globalVAT);

                decimal? gDisc = null;
                //rem si premiere condition true --> ne regarde pas la seconde??? non
                if (discountG != "")
                {
                    gDisc = decimal.Parse(discountG);
                    gDisc /= 100;
                }
                dal.UpdateTransaction(idTr, gTot, gDisc, gVat);
            }
        }

        internal static void CancelTransac(string numTransaction)
        {
            using (IDal dal = new TransactionDal())
            {
                var transac = int.Parse(numTransaction);
                dal.CancelTransactionById(transac);
            }
        }

        internal static void AddTicketAndCloseTransac(string numTransaction, string numTicket)
        {
            using (IDal dal = new TransactionDal())
            {
                var transac = int.Parse(numTransaction);
                var ticket = int.Parse(numTicket);
                dal.CloseTransaction(transac, ticket);
            }
        }

        #endregion

        #region Product
        internal static PRODUCT FindProductByCode(string codeProduct)
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetProductByCode(codeProduct);
            }
        }
        #endregion

        #region VAT
        internal static Decimal VatIncl(int id, decimal price)
        {
            using (IDal dal = new TransactionDal())
            {
                decimal vat = dal.GetVatValById(id);
                if (vat != 0)
                {
                    ++vat;
                    return (price * vat);
                }
                return price;
            }
        }

        internal static IList<VAT> FindVatsList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllVats();
            }
        }

        internal static int FindVatIdByVal(decimal globalVAT)
        {
            using (IDal dal = new TransactionDal())
            {

                return dal.GetVatIdByVal(globalVAT);
            }
        }

        private static string FindVatValById(int? vatId)
        {
            if (vatId != null)
            {
                using (IDal dal = new TransactionDal())
                {

                    return (dal.GetVatValById(vatId)).ToString();
                }
            }
            return "no VAT";
        }
        #endregion

        #region Payment
        internal static TrPaymentMenuViewModel CalculCash(TrPaymentMenuViewModel vmodel)
        {
            var temp1 = vmodel.GlobalTotal.Replace(".", ",");
            var temp2 = vmodel.Amount.Replace(".", ",");
            decimal cash = decimal.Parse(temp2);
            decimal tot = decimal.Parse(temp1);
            int meth = int.Parse(vmodel.MethodP);
            int transac = int.Parse(vmodel.NumTransaction);

            using (IDal dal = new TransactionDal())
            {
                if (cash > tot)
                {
                    dal.CreatePayment(tot, meth, transac);
                    vmodel.CashReturn = (cash - tot).ToString();
                    vmodel.Amount = "0";
                    vmodel.GlobalTotal = "0";
                }
                else if (cash == tot)
                {
                    dal.CreatePayment(tot, meth, transac);
                    vmodel.CashReturn = "0";
                    vmodel.Amount = "0";
                    vmodel.GlobalTotal = "0";
                }
                else if (cash < tot)
                {
                    dal.CreatePayment(cash, meth, transac);
                    vmodel.CashReturn = "0";
                    vmodel.Amount = "0";
                    vmodel.GlobalTotal = (tot - cash).ToString();
                }
                return vmodel;
            }
        }

        internal static int AskValidationCard(string amount)
        {
            Random random = new Random();
            int val = random.Next(2);
            return val;
        }

        internal static IList<PAYMENT_METHOD> FindMethodsList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllMethods();
            }
        }
        #endregion

        #region Ticket
        internal static TrTicketViewModel FillTicket(string numTransaction)
        {
            using (IDal dal = new TransactionDal())
            {
                //find transac
                var transac = FindTransactionById(numTransaction);
                //create ticket
                TrTicketViewModel vm = new TrTicketViewModel();
                vm.Ticket = (dal.CreateTicket()).ToString();

                vm.DateTicket = (DateTime.Now).ToString();
                //n° transac
                vm.Transaction = numTransaction;
                //to do --> magasin

                //detail
                vm.DetailsListWithTot = ListDetailsWithTot(numTransaction);

                //discount

                if (transac.discountGlobal == null)
                {
                    vm.DiscountG = " - ";
                }
                else
                {
                    vm.DiscountG = (transac.discountGlobal).ToString();
                }

                //VAT
                vm.VatG = (FindVatValById(transac.vatId)).ToString();
                //vm.VatG = dal.GetAppliedVatById(transac.vatId).appliedVat;

                //Total
                vm.TotalG = (transac.total).ToString();

                //payment method & amount
                vm.Payments = FindPaymentsByTransacId(numTransaction);

                return vm;
            }
        }

        private static IList<PAYMENT> FindPaymentsByTransacId(string numTransaction)
        {
            using (IDal dal = new TransactionDal())
            {
                int tr = int.Parse(numTransaction);
                return dal.GetAllPaymentsByTransacId(tr);
            }
        }
        #endregion

        #region Search
        internal static IList<BRAND> FindBrandsList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllBrands();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdBrand(string argument)
        {
            using (IDal dal = new TransactionDal())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdBrand(id);
            }
        }

        internal static IList<HERO> FindHerosList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllHeros();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdHero(string argument)
        {
            using (IDal dal = new TransactionDal())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdHero(id);
            }
        }

        internal static IList<AGE> FindAgesList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllAges();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdAge(string argument)
        {
            using (IDal dal = new TransactionDal())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdAge(id);
            }
        }

        internal static IList<CATEGORY> FindCatsList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllCats();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdCat(string argument)
        {
            using (IDal dal = new TransactionDal())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdCat(id);
            }
        }

        #endregion
    }
}