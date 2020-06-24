using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;
using TT.Common;

namespace ZDTradeClientTT
{
    /// <summary>
    /// tag 11 的生成类
    /// </summary>
    public class ClOrderIDGen
    {
        private static object _synLock =null;
        private static long _lastOrderID;
        private static long _beginOrderId;
        private static long _endOrderId;

        static ClOrderIDGen()
        {
            _synLock = new object();
            _beginOrderId = ZDTradeClientTTConfiurations.MinClOrderID;
            _endOrderId = ZDTradeClientTTConfiurations.MaxClOrderID;
            _lastOrderID = _beginOrderId;

            var datas = TxtFile.ReadTxtFile(ZDTradeClientTTConfiurations.OrderID);
            if(datas.Count>0)
            {
                _lastOrderID = long.Parse(datas[0]);
            }
        }


        public static long GetNextClOrderID(ConcurrentDictionary<long, RefObj> xReference=null)
        {
            lock (_synLock)
            {
                _lastOrderID++;
                _lastOrderID = _lastOrderID <= _beginOrderId ? _beginOrderId + 1 : _lastOrderID;
                _lastOrderID = _lastOrderID >= _endOrderId ? _beginOrderId + 1 : _lastOrderID;
                if (xReference != null)
                {
                    if (xReference.ContainsKey(_lastOrderID))
                    {
                        GetNextClOrderID(xReference);
                    }
                }
                return _lastOrderID;
            }
        }

        public static void SaveOrderId()
        {
            TxtFile.SaveTxtFile(ZDTradeClientTTConfiurations.OrderID, new List<string> { _lastOrderID.ToString() });
        }
    }

}
