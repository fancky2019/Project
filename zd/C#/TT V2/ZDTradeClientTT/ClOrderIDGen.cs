using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;

namespace ZDTradeClientTT
{
    public class ClOrderIDGen
    {
        private static object synLock = new object();
        public static long lastOrderID;
        private static ConcurrentDictionary<long, RefObj> xReference = new ConcurrentDictionary<long, RefObj>();

        public static long beginOrderId;
        public static long endOrderId;

        public const string ORDER_ID_FILE = "config/OrderID.txt";

        static ClOrderIDGen()
        {
            beginOrderId = ZDTradeClientTTConfiurations.MinClOrderID;
            endOrderId = ZDTradeClientTTConfiurations.MaxClOrderID;
            lastOrderID = beginOrderId;

            if (File.Exists(ORDER_ID_FILE))
            {
                using (StreamReader sReader = new StreamReader(File.Open(ORDER_ID_FILE, FileMode.Open), System.Text.Encoding.ASCII))
                {
                    string oneLine = sReader.ReadLine().Trim();
                    lastOrderID = long.Parse(oneLine);
                }
            }
        }


        public static void setXRef(ConcurrentDictionary<long, RefObj> reference)
        {
            xReference = reference;
        }

        public static long getNextClOrderID()
        {
            lock (synLock)
            {
                lastOrderID++;
                lastOrderID = lastOrderID <= beginOrderId ? beginOrderId + 1 : lastOrderID;
                lastOrderID = lastOrderID >= endOrderId ? beginOrderId + 1 : lastOrderID;
                if (ClOrderIDGen.xReference != null)
                {
                    if (xReference.ContainsKey(lastOrderID))
                    {
                        getNextClOrderID();
                    }
                }
                return lastOrderID;
            }
        }

        public static void saveOrderId()
        {
            using (StreamWriter sWriter = new StreamWriter(File.Open(ORDER_ID_FILE, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
            {
                sWriter.WriteLine(lastOrderID.ToString());
            }
        }
    }

}
