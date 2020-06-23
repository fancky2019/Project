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
        public static ConcurrentDictionary<long, RefObj> XReference { get; set; }
        public static long beginOrderId;
        public static long endOrderId;

        public const string ORDER_ID_FILE = "config/OrderID.data";

        static ClOrderIDGen()
        {
            XReference = new ConcurrentDictionary<long, RefObj>();

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


        //public static void setXRef(ConcurrentDictionary<long, RefObj> reference)
        //{
        //    XReference = reference;
        //}

        public static long GetNextClOrderID()
        {
            lock (synLock)
            {
                lastOrderID++;
                lastOrderID = lastOrderID <= beginOrderId ? beginOrderId + 1 : lastOrderID;
                lastOrderID = lastOrderID >= endOrderId ? beginOrderId + 1 : lastOrderID;
                if (ClOrderIDGen.XReference != null)
                {
                    if (XReference.ContainsKey(lastOrderID))
                    {
                        GetNextClOrderID();
                    }
                }
                return lastOrderID;
            }
        }

        public static void SaveOrderId()
        {
            using (StreamWriter sWriter = new StreamWriter(File.Open(ORDER_ID_FILE, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
            {
                sWriter.WriteLine(lastOrderID.ToString());
            }
        }
    }

}
