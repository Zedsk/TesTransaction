using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Dal;
using TesTransaction.Data.Entity;

namespace TesTransaction.BL
{
    public class ProductBL
    {
        internal static PRODUCT FindProductByCode(string codeProduct)
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetProductByCode(codeProduct);
            }
        }

        internal static List<PRODUCT> FindAllProductByCode(string codeProduct)
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllProductByCode(codeProduct);
            }
        }

        internal static object FindProductByName(string product)
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetProductByName(product);
            }
        }

        internal static List<PRODUCT> FindAllProductByName(string product)
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllProductByName(product);
            }
        }
    }
}