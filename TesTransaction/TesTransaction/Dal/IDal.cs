﻿using System;
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

        #endregion

        #region Transaction
        int CreateTransaction(int terminal);
        List<TRANSACTION_DETAILS> GetAllDetailsByTransactionId(int id);
        void CreateDetail(PRODUCT prod, int transactionId, int terminalId, decimal vat);
        void EditQtyToDetailById(int id, int qty);
        void DeleteDetail(int id);
        void UpdateTransaction(int idTransaction, decimal globalTotal, decimal? discountG, int globalVAT);
        #endregion

        #region Product
        PRODUCT GetProductByCode(string codeProduct);

        #endregion

        #region VAT
        VAT GetAppliedVatById(int id);
        List<VAT> GetAllVats();
        int GetVatIdByVal(decimal globalVAT);
        #endregion
    }
}
