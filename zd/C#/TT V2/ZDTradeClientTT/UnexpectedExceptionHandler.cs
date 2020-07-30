using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZDTradeClientTT
{
    /// <summary>
    /// rainer  壳调用
    /// </summary>
    public class UnexpectedExceptionHandler
    {
        public static TTCommunication globexCommunication = null;

        public static void doOnExcetion()
        {
            if (globexCommunication != null)
            {
                globexCommunication.persistDayRefObj();
                globexCommunication.shutdown();
            }
        }
    }
}
