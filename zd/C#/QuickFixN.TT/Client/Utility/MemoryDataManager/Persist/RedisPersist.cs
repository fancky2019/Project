using Client.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utility.MemoryDataManager.Persist
{
    class RedisPersist : IPersist
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();

        private const string LAST_CLIENT_ORDER_ID_KEY = "LastClientOrderID";
        private const string ORDER_KEY = "Orders";

        private object _lockObj = new object();
        public void Load()
        {
            try
            {

                var bytes = RedisHelper.GetData(LAST_CLIENT_ORDER_ID_KEY);
                if (bytes != null)
                {
                    MemoryData.LastClientOrderID = MessagePackUtility.Deserialize<long>(bytes);
                }
            }
            catch (Exception ex)
            {
                _nLog.Error("Load LastClientOrderID Failed");
                _nLog.Error(ex.ToString());
            }

            try
            {
                var bytes = RedisHelper.GetData(ORDER_KEY);
                if (bytes != null)
                {
                    MemoryData.Orders = MessagePackUtility.Deserialize<ConcurrentDictionary<string, Order>>(bytes);
                }
            }
            catch (Exception ex)
            {
                _nLog.Error("Load Orders Failed");
                _nLog.Error(ex.ToString());
            }

            MemoryData.InitUsingCliOrderIDSystemCode();

        }


        public void Persist()
        {
            lock (_lockObj)
            {
                try
                {

                    var lastClientOrderIDBytes = MessagePackUtility.Serialize<long>(MemoryData.LastClientOrderID);
                    RedisHelper.SaveData(LAST_CLIENT_ORDER_ID_KEY, lastClientOrderIDBytes);
                }
                catch (Exception ex)
                {
                    _nLog.Error("Save LastClientOrderID Failed");
                    _nLog.Error(ex.ToString());
                }



                try
                {
                    var ordersBytes = MessagePackUtility.Serialize<ConcurrentDictionary<string, Order>>(MemoryData.Orders);
                    RedisHelper.SaveData(ORDER_KEY, ordersBytes);

                }
                catch (Exception ex)
                {
                    _nLog.Error("Save Orders Failed");
                    _nLog.Error(ex.ToString());
                }

            }
        }
    }
}
