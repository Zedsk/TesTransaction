using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    interface IDalTransaction : IDisposable
    {
        #region Transaction
        int CreateTransaction(int terminal);
        List<TRANSACTION_DETAILS> GetAllDetailsByTransactionId(int id);
        TRANSACTIONS GetTransactionById(int transactionId);
        void CreateDetail(PRODUCT prod, int transactionId, int terminalId, decimal vat);
        void EditQtyToDetailById(int id, int qty);
        void DeleteDetail(int id);
        void UpdateTransaction(int transactionId, decimal globalTotal, decimal? discountG);
        void CancelTransactionById(int transactionId);
        void UpdateTransactionMessageId(int transactionId, int idTicket);
        void CloseTransaction(int transac);
        #endregion
    }
}
