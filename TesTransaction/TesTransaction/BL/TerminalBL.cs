using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Dal;
using TesTransaction.Data.Entity;

namespace TesTransaction.BL
{
    public class TerminalBL
    {
        internal static TERMINAL FindTerminalById(int id)
        {
            using (IDalTerminal dal = new DalTerminal())
            {
                return dal.GetTerminalById(id);
            }
        }

        internal static IList<string> FindTerminalsNames()
        {
            using (IDalTerminal dal = new DalTerminal())
            {
                List<TERMINAL> terminals = dal.GetAllTerminals();
                List<string> terminalsNames = new List<string>();
                foreach (var t in terminals)
                {
                    terminalsNames.Add(t.nameTerminal);
                }
                return terminalsNames;
            }
        }

        internal static IList<TERMINAL> FindTerminalsList()
        {
            using (IDalTerminal dal = new DalTerminal())
            {
                return dal.GetAllTerminals();
            }
        }

        internal static int FindTerminalIdByDate()
        {
            using (IDalTerminal dal = new DalTerminal())
            {
                return dal.GetTerminalIdByDate();
            }
        }
    }
}