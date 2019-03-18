using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    public class DalProduct : IDalProduct
    {
        #region DB

        private TestTransactionEntities db;

        public DalProduct()
        {
            db = new TestTransactionEntities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public PRODUCT GetProductByCode(string codeProduct)
        {
            return db.PRODUCTs.Where(p => p.barcode == codeProduct).Single();
        }

        public PRODUCT GetProductByName(string product)
        {
            throw new NotImplementedException();
        }

        public List<PRODUCT> GetAllProductByCode(string codeProduct)
        {
            return db.PRODUCTs.Where(p => p.barcode.Contains(codeProduct)).ToList();
        }

        public List<PRODUCT> GetAllProductByName(string codeProduct)
        {
            throw new NotImplementedException();
        }
    }
}