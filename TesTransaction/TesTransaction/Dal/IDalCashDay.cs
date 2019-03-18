using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    interface IDalCashDay : IDisposable
    {
        void CreateCashDay(DateTime date, int terminalid, decimal beginCash);
        void UpdateCashDay();
        List<CASH_BOTTOM_DAY> GetAllCashDays();
        List<CASH_BOTTOM_DAY> GetAllCashDaysByDay(DateTime day);
    }
}
