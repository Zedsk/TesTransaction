using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    public class DalCashDay : IDalCashDay
    {
        #region DB

        private TestTransactionEntities db;

        public DalCashDay()
        {
            db = new TestTransactionEntities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public void CreateCashDay(DateTime date, int terminalid, decimal beginCash)
        {
            // at the beginning endCash = 0
            db.CASH_BOTTOM_DAYs.Add(new CASH_BOTTOM_DAY { dateDay = date, terminalId = terminalid, beginningCash = beginCash, endCash = 0 });
            db.SaveChanges();
        }

        public List<CASH_BOTTOM_DAY> GetAllCashDays()
        {
            return db.CASH_BOTTOM_DAYs.ToList();
        }

        public List<CASH_BOTTOM_DAY> GetAllCashDaysByDay(DateTime day)
        {
            List<CASH_BOTTOM_DAY> cashDaysList = new List<CASH_BOTTOM_DAY>();
            cashDaysList = db.CASH_BOTTOM_DAYs.Where(d => d.dateDay.Date == day.Date).ToList();
            return cashDaysList;
        }

        public void UpdateCashDay()
        {
            throw new NotImplementedException();
        }
    }
}