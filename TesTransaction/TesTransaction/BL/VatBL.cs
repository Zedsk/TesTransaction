using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Dal;
using TesTransaction.Data.Entity;

namespace TesTransaction.BL
{
    public class VatBL
    {
        internal static Decimal VatIncl(int id, decimal price)
        {
            using (IDalVat dal = new DalVat())
            {
                //decimal vat = dal.GetAppliedVatById(id).appliedVat;
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
            using (IDalVat dal = new DalVat())
            {
                return dal.GetAllVats();
            }
        }

        internal static int FindVatIdByVal(decimal globalVAT)
        {
            using (IDalVat dal = new DalVat())
            {

                return dal.GetVatIdByVal(globalVAT);
            }
        }

        private static string FindVatValById(int? vatId)
        {
            if (vatId != null)
            {
                using (IDalVat dal = new DalVat())
                {

                    return (dal.GetVatValById(vatId)).ToString();
                }
            }
            return "no VAT";
        }
    }
}