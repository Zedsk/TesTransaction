using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    public class DalPayment : IDalPayment
    {
        #region DB

        private TestTransactionEntities db;

        public DalPayment()
        {
            db = new TestTransactionEntities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

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
    }
}