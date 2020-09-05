using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using StockAdapterHKEX;



namespace StockAdapterHKEX
{
    public class ClOrderIDGen
    {
        private static LockObject synLock = new LockObject();
        private static long lastOrderID = 8000000;
        private static ConcurrentDictionary<long, RefObj> xReference = new ConcurrentDictionary<long, RefObj>();

        //private static long baseTimeTicks = new DateTime(2012, 4, 1).Ticks;
        //public static long getNextClOrderID()
        //{
        //    try
        //    {
        //        Monitor.Enter(synLock);
        //        long elapsed = DateTime.Now.Ticks - baseTimeTicks;
        //        if (elapsed <= lastOrderID)
        //            lastOrderID++;
        //        else
        //            lastOrderID = elapsed;

        //        return lastOrderID;
        //    }
        //    finally
        //    {
        //        Monitor.Exit(synLock);
        //    }
        //}

        static ClOrderIDGen()
        {
            //StockAdapterHKEX.CfgManager cfg = StockAdapterHKEX.CfgManager.getInstance("ZDTradeClientTT.exe");
            StockAdapterHKEX.CfgManager cfg = StockAdapterHKEX.CfgManager.getInstance(null);
            if (!string.IsNullOrEmpty(cfg.ClOrderID))
            {
                lastOrderID = long.Parse(cfg.ClOrderID);
                Console.WriteLine("Next order id: " + lastOrderID);
            }
        }

        private static DateTime baseDT = new DateTime(2012, 9, 1);
        //public static long getNextClOrderID()
        //{
        //    lock (synLock)
        //    {
        //        long elapsed = (long)(DateTime.Now.Subtract(baseDT).TotalSeconds);
        //        if (elapsed > lastOrderID)
        //            lastOrderID = elapsed;
        //        else
        //            lastOrderID++;

        //        return lastOrderID;
        //    }
        //}

        public static void setXRef(ConcurrentDictionary<long, RefObj> xReference)
        {
            ClOrderIDGen.xReference = xReference;
        }

        public static long getNextClOrderID()
        {
            lock (synLock)
            {
                if (ClOrderIDGen.xReference == null)
                {
                    return lastOrderID++;
                }
                else
                {
                    while (xReference.ContainsKey(lastOrderID))
                        lastOrderID++;

                    return lastOrderID++;
                }
            }
        }

    }

    class LockObject
    {
    }
}
