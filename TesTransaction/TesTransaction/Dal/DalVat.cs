using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    public class DalVat : IDalVat
    {
        #region DB

        private TestTransactionEntities db;

        public DalVat()
        {
            db = new TestTransactionEntities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

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
    }
}