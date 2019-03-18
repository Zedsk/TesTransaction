using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Dal;
using TesTransaction.Data.Entity;
using TesTransaction.Models.Transactions;

namespace TesTransaction.BL
{
    public class PaymentBL
    {
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

        internal static List<string> MakeAmountsList(string amount, List<string> amountsList)
        {
            if (amountsList == null)
            {
                List<string> newList = new List<string>
                {
                    amount
                };
                return newList;
            }
            amountsList.Add(amount);
            return amountsList;
        }

        internal static List<string> MakeAmountsList(string numTransaction)
        {
            IList<PAYMENT> listPayments = new List<PAYMENT>();
            List<string> amountsList = new List<string>();
            listPayments = FindPaymentsByTransacId(numTransaction);
            foreach (var payment in listPayments)
            {
                amountsList.Add((payment.amount).ToString());
            }
            return amountsList;
        }

        internal static decimal AdaptTotalWithPaid(string gTot, List<string> listAmounts)
        {
            decimal amounts = 0;
            for (int i = 0; i < listAmounts.Count; i++)
            {
                amounts += decimal.Parse(listAmounts[i]);
            }
            decimal tot = decimal.Parse(gTot) - amounts;
            return tot;
        }

        internal static IList<PAYMENT> FindPaymentsByTransacId(string numTransaction)
        {
            using (IDal dal = new TransactionDal())
            {

                int tr = int.Parse(numTransaction);
                return dal.GetAllPaymentsByTransacId(tr);
            }
        }
    }
}