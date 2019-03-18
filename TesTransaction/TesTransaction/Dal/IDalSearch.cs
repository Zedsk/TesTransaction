using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    interface IDalSearch : IDisposable
    {
        IList<BRAND> GetAllBrands();
        IList<PRODUCT> GetAllProductByIdBrand(int id);
        IList<HERO> GetAllHeros();
        IList<PRODUCT> GetAllProductByIdHero(int id);
        IList<AGE> GetAllAges();
        IList<PRODUCT> GetAllProductByIdAge(int id);
        IList<CATEGORY> GetAllCats();
        IList<PRODUCT> GetAllProductByIdCat(int id);
    }
}
