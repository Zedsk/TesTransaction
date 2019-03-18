using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Dal;
using TesTransaction.Models.Transactions;

namespace TesTransaction.BL
{
    public class TicketBL
    {
        internal static TrTicketViewModel FillTicket(string numTransaction)
        {
            using (IDal dal = new TransactionDal())
            {
                //find transac
                var transac = TransactionBL.FindTransactionById(numTransaction);
                //create ticket
                TrTicketViewModel vm = new TrTicketViewModel();
                //vm.Ticket = (dal.CreateTicket()).ToString();

                vm.DateTicket = (DateTime.Now).ToString();
                //n° transac
                vm.Transaction = numTransaction;
                //to do --> magasin

                //detail
                vm.DetailsListWithTot = TransactionBL.ListDetailsWithTot(numTransaction);

                //discount

                if (transac.discountGlobal == null)
                {
                    vm.DiscountG = " - ";
                }
                else
                {
                    vm.DiscountG = (transac.discountGlobal).ToString();
                }

                ////VAT
                //vm.VatG = (FindVatValById(transac.vatId)).ToString();
                //vm.VatG = dal.GetAppliedVatById(transac.vatId).appliedVat;

                //Total
                vm.TotalG = (transac.total).ToString();

                //payment method & amount
                vm.Payments = PaymentBL.FindPaymentsByTransacId(numTransaction);

                //message
                var message = FindTicketMessageById(transac.messageId, transac.languageMessage);
                vm.Message = message;

                return vm;
            }
        }

        private static string FindTicketMessageById(int messageId, int languageMessage)
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetTicketMessageByIdAndLanguage(messageId, languageMessage);
            }
        }
    }
}