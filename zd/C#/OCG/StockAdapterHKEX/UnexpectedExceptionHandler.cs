using StockAdapterHKEX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAdapterHKEX
{
    public class UnexpectedExceptionHandler
    {
        public static HKEXCommunication globexCommunication = null;

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
