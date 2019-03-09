using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    public class TransactionDal : IDal
    {
        private TestTransactionEntities db;

        public TransactionDal()
        {
            db = new TestTransactionEntities();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        #region CashDay

        public void CreateCashDay(DateTime date, int terminalid, decimal beginCash)
        {
            // at the beginning endCash = 0
            db.CASH_BOTTOM_DAYs.Add(new CASH_BOTTOM_DAY { dateDay = date, terminalId = terminalid, beginningCash = beginCash, endCash = 0 });
            db.SaveChanges();
        }

        public List<CASH_BOTTOM_DAY> GetAllCashDays()
        {
            return db.CASH_BOTTOM_DAYs.ToList();
        }

        public List<CASH_BOTTOM_DAY> GetAllCashDaysByDay(DateTime day)
        {
            List<CASH_BOTTOM_DAY> cashDaysList = new List<CASH_BOTTOM_DAY>();
            cashDaysList = db.CASH_BOTTOM_DAYs.Where(d => d.dateDay.Date == day.Date).ToList();
            return cashDaysList;
        }

        public void UpdateCashDay()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Terminal
        public List<TERMINAL> GetAllTerminals()
        {
            return db.TERMINALs.ToList();
        }

        public TERMINAL GetTerminalById(int id)
        {
            return db.TERMINALs.Where(t => t.idTerminal == id).Single();
        }

        public int GetTerminalIdByDate()
        {
            CASH_BOTTOM_DAY cashDay = db.CASH_BOTTOM_DAYs.Where(c => c.dateDay == DateTime.Today).Single();
            return cashDay.terminalId;
        }

        #endregion

        #region Transaction
        public int CreateTransaction(int terminal)
        {
            // to do --> provisoire vendorId = 1, shopId = 1, customerId = 1
            TRANSACTIONS tr = new TRANSACTIONS { transactionDateBegin = DateTime.Now, transactionDateEnd = DateTime.Parse("2000-01-01 00:00:00"), terminalId = terminal, vendorId = 1, shopId = 1, customerId = 1 };
            db.TRANSACTIONSs.Add(tr);
            db.SaveChanges();
            return tr.idTransaction;
        }

        public void CreateDetail(PRODUCT prod, int termId, int trId, decimal vat)
        {
            // name item -> barcode  à changer!
            db.TRANSACTION_DETAILSs.Add(new TRANSACTION_DETAILS { transactionId = trId, productId = prod.idProduct, nameItem = prod.barcode, price = prod.salesPrice, quantity = 1, Discount = prod.discountRate, vatItem = vat });
            db.SaveChanges();
        }

        public List<TRANSACTION_DETAILS> GetAllDetailsByTransactionId(int id)
        {
            return db.TRANSACTION_DETAILSs.Where(t => t.transactionId == id).ToList();
        }

        public TRANSACTIONS GetTransactionById(int transactionId)
        {
            return db.TRANSACTIONSs.Where(t => t.idTransaction == transactionId).Single();
        }

        public void EditQtyToDetailById(int id, int qty)
        {
            var detail = db.TRANSACTION_DETAILSs.First(d => d.idTransactionDetails == id);
            if (detail != null)
            {
                detail.quantity = qty;
                db.SaveChanges();
            }
        }

        public void DeleteDetail(int id)
        {
            TRANSACTION_DETAILS detail = db.TRANSACTION_DETAILSs.Find(id);
            db.TRANSACTION_DETAILSs.Remove(detail);
            db.SaveChanges();
        }

        public void UpdateTransaction(int transactionId, decimal globalTotal, decimal? discountG, int globalVAT)
        {
            var transac = db.TRANSACTIONSs.First(d => d.idTransaction == transactionId);
            if (transac != null)
            {
                transac.total = globalTotal;
                transac.discountGlobal = discountG;
                transac.vatId = globalVAT;

                db.SaveChanges();
            }
        }

        public void UpdateTransactionTicketId(int transactionId, int idTicket)
        {

            var transac = db.TRANSACTIONSs.First(t => t.idTransaction == transactionId);
            if (transac != null)
            {
                transac.ticketId = idTicket;
                db.SaveChanges();
            }
        }

        public void CancelTransactionById(int transactionId)
        {
            var transac = db.TRANSACTIONSs.First(d => d.idTransaction == transactionId);
            if (transac != null)
            {
                transac.isCanceled = true;
                transac.isClose = true;
                transac.transactionDateEnd = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void CloseTransaction(int transacId, int ticket)
        {
            var transac = db.TRANSACTIONSs.First(d => d.idTransaction == transacId);
            if (transac != null)
            {
                transac.ticketId = ticket;
                transac.isClose = true;
                transac.transactionDateEnd = DateTime.Now;
                db.SaveChanges();
            }
        }

        #endregion

        #region Product
        public PRODUCT GetProductByCode(string codeProduct)
        {
            return db.PRODUCTs.Where(p => p.barcode == codeProduct).Single();
        }

        #endregion

        #region VAT
        public List<VAT> GetAllVats()
        {
            return db.VATs.ToList();
        }

        public int GetVatIdByVal(decimal globalVAT)
        {
            VAT vat = db.VATs.Where(v => v.appliedVat == globalVAT).Single();
            return vat.idVat;
        }

        public decimal GetVatValById(int? vatId)
        {
            VAT vat = db.VATs.Where(v => v.idVat == vatId).Single();
            return vat.appliedVat;
        }

        #endregion

        #region Payment
        public void CreatePayment(decimal tot, int methodP, int numTransaction)
        {
            PAYMENT p = new PAYMENT { amount = tot, momentPay = DateTime.Now, paymentMethodId = methodP, transactionId = numTransaction };
            db.PAYMENTs.Add(p);
            db.SaveChanges();
        }

        public IList<PAYMENT_METHOD> GetAllMethods()
        {
            return db.PAYMENT_METHODs.ToList();
        }

        public IList<PAYMENT> GetAllPaymentsByTransacId(int numTransaction)
        {
            return db.PAYMENTs.Where(p => p.transactionId == numTransaction).ToList();
        }

        #endregion

        #region Ticket
        public int CreateTicket()
        {
            // to do --> provisoire messageId = 1 , languageId = 1
            TICKET t = new TICKET { messageId = 1, languageId = 1 };
            db.TICKETs.Add(t);
            db.SaveChanges();
            return t.idTicket;
        }


        #endregion
    }
}