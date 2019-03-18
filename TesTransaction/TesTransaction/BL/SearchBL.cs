using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Dal;
using TesTransaction.Data.Entity;

namespace TesTransaction.BL
{
    public class SearchBL
    {
        internal static IList<BRAND> FindBrandsList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllBrands();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdBrand(string argument)
        {
            using (IDal dal = new TransactionDal())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdBrand(id);
            }
        }

        internal static IList<HERO> FindHerosList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllHeros();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdHero(string argument)
        {
            using (IDal dal = new TransactionDal())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdHero(id);
            }
        }

        internal static IList<AGE> FindAgesList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllAges();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdAge(string argument)
        {
            using (IDal dal = new TransactionDal())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdAge(id);
            }
        }

        internal static IList<CATEGORY> FindCatsList()
        {
            using (IDal dal = new TransactionDal())
            {
                return dal.GetAllCats();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdCat(string argument)
        {
            using (IDal dal = new TransactionDal())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdCat(id);
            }
        }
    }
}