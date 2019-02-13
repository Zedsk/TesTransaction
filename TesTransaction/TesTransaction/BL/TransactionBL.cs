using System;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;
using TesTransaction.Dal;
using System.Web.Mvc;
using System.Collections.Generic;
using TesTransaction.Models;

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

        internal static IList<string> FindTerminals()
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

        #endregion

        #region Transaction
        internal static string InitializeNewTransaction(int terminal)
        {
            using (IDal dal = new TransactionDal())
            {
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

        internal static void AddNewTransactionDetail(string codeProduct, string terminal, string transaction)
        {
            using (IDal dal = new TransactionDal())
            {
                //find productId with codeProduct
                var prod = dal.GetProductByCode(codeProduct);
                //verify if product exist in detail
                IList<TRANSACTION_DETAILS> detailList = FindTransactionDetailsListById(transaction);
                var result = VerifyProductInDetail(prod.idProduct, detailList);
                if (result)
                {
                    foreach (var item in detailList)
                    {
                        if (item.productId == prod.idProduct)
                        {
                            //qty++
                            dal.EditQtyToDetailById(item.idTransactionDetails);
                            break;
                        }
                    }
                }
                else
                {
                    //find value vatItem
                    var vatItem = dal.GetAppliedVatById(prod.vatId).appliedVat;
                    //Add new detail --> param product, terminalId, transactionId, vatItem 
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
                    Total = st
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
                result += vm.Total;
            }
            return result;
        }

        #endregion

        #region Product
        //internal static PRODUCTs FindProductByCode(string codeProduct)
        //{
        //    using(IDal dal = new TransactionDal())
        //    {
        //        return dal.
        //    }
        //}
        #endregion

        #region VAT
        internal static Decimal VatIncl(int id, decimal price)
        {
            using (IDal dal = new TransactionDal())
            {
                decimal vat = dal.GetAppliedVatById(id).appliedVat;
                if (vat != 0)
                {
                    ++vat;
                    return (price * vat);
                }
                return price;
            }
        }
        #endregion
    }
}