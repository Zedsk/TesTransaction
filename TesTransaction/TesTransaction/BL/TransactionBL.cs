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

        internal static void SaveTransactionBeforePayment(string numTransaction, string globalTotal, string discountG)
        {
            using (IDal dal = new TransactionDal())
            {
                var idTr = int.Parse(numTransaction);
                //var gTot = decimal.Parse(globalTotal);
                //probleme le string "157.98" devient 15798
                var temp = globalTotal.Replace(".", ",");
                var gTot = decimal.Parse(temp);
                decimal? gDisc = null;
                if (discountG != "")
                {
                    gDisc = decimal.Parse(discountG);
                    gDisc /= 100;
                }
                dal.UpdateTransaction(idTr, gTot, gDisc);
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

        internal static void AddTicketAndCloseTransac(string numTransaction)
        {
            using (IDal dal = new TransactionDal())
            {
                var transac = int.Parse(numTransaction);
                dal.CloseTransaction(transac);
            }
        }

        internal static string FindTotalByTransacId(string nTransac)
        {
            TRANSACTIONS transac = FindTransactionById(nTransac);
            return transac.total.ToString();
        }
        #endregion
    }
}