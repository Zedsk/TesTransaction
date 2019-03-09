using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    interface IDal : IDisposable
    {
        #region CashDay
        void CreateCashDay(DateTime date, int terminalid, decimal beginCash);
        void UpdateCashDay();
        List<CASH_BOTTOM_DAY> GetAllCashDays();
        List<CASH_BOTTOM_DAY> GetAllCashDaysByDay(DateTime day);
        #endregion

        #region Terminal
        List<TERMINAL> GetAllTerminals();
        TERMINAL GetTerminalById(int id);
        int GetTerminalIdByDate();
        #endregion

        #region Transaction
        int CreateTransaction(int terminal);
        List<TRANSACTION_DETAILS> GetAllDetailsByTransactionId(int id);
        TRANSACTIONS GetTransactionById(int transactionId);
        void CreateDetail(PRODUCT prod, int transactionId, int terminalId, decimal vat);
        void EditQtyToDetailById(int id, int qty);
        void DeleteDetail(int id);
        void UpdateTransaction(int idTransaction, decimal globalTotal, decimal? discountG, int globalVAT);
        void CancelTransactionById(int transactionId);
        void UpdateTransactionTicketId(int transactionId, int idTicket);
        void CloseTransaction(int transac, int ticket);
        #endregion

        #region Product
        PRODUCT GetProductByCode(string codeProduct);

        #endregion

        #region VAT
        decimal GetVatValById(int? vatId);
        List<VAT> GetAllVats();
        int GetVatIdByVal(decimal globalVAT);
        #endregion

        #region Payment
        void CreatePayment(decimal tot, int methodP, int numTransaction);
        IList<PAYMENT_METHOD> GetAllMethods();
        IList<PAYMENT> GetAllPaymentsByTransacId(int numTransaction);

        #endregion

        #region Ticket
        int CreateTicket();

        #endregion

        #region Search
        IList<BRAND> GetAllBrands();
        IList<PRODUCT> GetAllProductByIdBrand(int id);
        IList<HERO> GetAllHeros();
        IList<PRODUCT> GetAllProductByIdHero(int id);
        IList<AGE> GetAllAges();
        IList<PRODUCT> GetAllProductByIdAge(int id);
        IList<CATEGORY> GetAllCats();
        IList<PRODUCT> GetAllProductByIdCat(int id);
        #endregion
    }
}
