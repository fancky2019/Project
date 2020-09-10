using ZDFixService.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDFixService.Utility;

namespace ZDFixService.Service.MemoryDataManager.Persist
{
    class RedisPersist : IPersist
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        Log _logger = LogManager.GetLogger("SerializeTime");
        //private const string LAST_CLIENT_ORDER_ID_KEY = "LastClientOrderID";
        //private const string ORDER_KEY = "Orders";

        private readonly string _lastCliientOrderIDKey = "LastClientOrderID";
        private readonly string _orderKey = "Orders";

        private object _lockObj = new object();


        internal RedisPersist()
        {
            _lastCliientOrderIDKey =$"{Configurations.Configuration["ZDFixService:ITradeService"]}_{_lastCliientOrderIDKey}";
            _orderKey = $"{Configurations.Configuration["ZDFixService:ITradeService"]}_{_orderKey}";
        }

        public void Load()
        {
            try
            {

                var bytes = RedisHelper.GetData(_lastCliientOrderIDKey);
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
                var bytes = RedisHelper.GetData(_orderKey);
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
                    RedisHelper.SaveData(_lastCliientOrderIDKey, lastClientOrderIDBytes);
                }
                catch (Exception ex)
                {
                    _nLog.Error("Save LastClientOrderID Failed");
                    _nLog.Error(ex.ToString());
                }



                try
                {
                    var startSerializeTime = DateTime.Now;
                    var ordersBytes = MessagePackUtility.Serialize<ConcurrentDictionary<string, Order>>(MemoryData.Orders);
                    var endSerializeTime = DateTime.Now;
                    RedisHelper.SaveData(_orderKey, ordersBytes);
                    _logger.WriteLog($"{startSerializeTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{endSerializeTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}");

                    //var ordersBytes = MessagePackUtility.Serialize<ConcurrentDictionary<string, Order>>(MemoryData.Orders);
                    //RedisHelper.SaveData(_orderKey, ordersBytes);

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
