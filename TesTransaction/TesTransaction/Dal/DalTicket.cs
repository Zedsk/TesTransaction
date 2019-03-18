using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    public class DalTicket : IDalTicket
    {
        #region DB

        private TestTransactionEntities db;

        public DalTicket()
        {
            db = new TestTransactionEntities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public string GetTicketMessageByIdAndLanguage(int messageId, int languageMessage)
        {
            var ticket = db.TICKET_MESSAGEs.Where(t => t.idMessage == messageId && t.languageId == languageMessage).Single();
            return ticket.message;
        }
    }
}