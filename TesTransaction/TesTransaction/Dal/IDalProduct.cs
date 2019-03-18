using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    interface IDalProduct : IDisposable
    {
        PRODUCT GetProductByCode(string codeProduct);
        List<PRODUCT> GetAllProductByCode(string codeProduct);
        PRODUCT GetProductByName(string product);
        List<PRODUCT> GetAllProductByName(string codeProduct);
    }
}
