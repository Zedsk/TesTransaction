using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    interface IDalPayment : IDisposable
    {
        void CreatePayment(decimal tot, int methodP, int numTransaction);
        IList<PAYMENT_METHOD> GetAllMethods();
        IList<PAYMENT> GetAllPaymentsByTransacId(int numTransaction);
    }
}
