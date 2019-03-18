using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    interface IDalVat : IDisposable
    {
        decimal GetVatValById(int? vatId);
        List<VAT> GetAllVats();
        int GetVatIdByVal(decimal globalVAT);
    }
}
