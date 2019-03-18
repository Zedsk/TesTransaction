using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesTransaction.Data.Entity;

namespace TesTransaction.Dal
{
    interface IDalTerminal : IDisposable
    {
        List<TERMINAL> GetAllTerminals();
        TERMINAL GetTerminalById(int id);
        int GetTerminalIdByDate();
    }
}
