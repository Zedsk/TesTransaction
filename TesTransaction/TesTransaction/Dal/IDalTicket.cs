using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesTransaction.Dal
{
    interface IDalTicket : IDisposable
    {
        string GetTicketMessageByIdAndLanguage(int messageId, int languageMessage);
    }
}
