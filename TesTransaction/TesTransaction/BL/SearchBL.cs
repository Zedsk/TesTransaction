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
            using (IDalSearch dal = new DalSearch())
            {
                return dal.GetAllBrands();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdBrand(string argument)
        {
            using (IDalSearch dal = new DalSearch())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdBrand(id);
            }
        }

        internal static IList<HERO> FindHerosList()
        {
            using (IDalSearch dal = new DalSearch())
            {
                return dal.GetAllHeros();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdHero(string argument)
        {
            using (IDalSearch dal = new DalSearch())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdHero(id);
            }
        }

        internal static IList<AGE> FindAgesList()
        {
            using (IDalSearch dal = new DalSearch())
            {
                return dal.GetAllAges();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdAge(string argument)
        {
            using (IDalSearch dal = new DalSearch())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdAge(id);
            }
        }

        internal static IList<CATEGORY> FindCatsList()
        {
            using (IDalSearch dal = new DalSearch())
            {
                return dal.GetAllCats();
            }
        }

        internal static IList<PRODUCT> FindProductListByIdCat(string argument)
        {
            using (IDalSearch dal = new DalSearch())
            {
                int id = int.Parse(argument);
                return dal.GetAllProductByIdCat(id);
            }
        }
    }
}