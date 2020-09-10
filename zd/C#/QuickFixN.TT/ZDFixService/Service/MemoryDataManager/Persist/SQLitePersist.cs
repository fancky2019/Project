using ZDFixService.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDFixService.Utility;

namespace ZDFixService.Service.MemoryDataManager.Persist
{
    class SQLitePersist : IPersist
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        Log _logger = LogManager.GetLogger("SerializeTime");

        private object _lockObj = new object();

        public SQLitePersist()
        {


        }
        public void Load()
        {
            try
            {

                var clientOrderID = SQLiteHelper.SelectClientOrderID();
                if (string.IsNullOrEmpty(clientOrderID))
                {
                    MemoryData.LastClientOrderID = long.Parse(clientOrderID);
                }
            }
            catch (Exception ex)
            {
                _nLog.Error("Load LastClientOrderID Failed");
                _nLog.Error(ex.ToString());
            }

            try
            {
                var bytes = SQLiteHelper.SelectOrder();
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

                    SQLiteHelper.UpdateClientOrderID(MemoryData.LastClientOrderID.ToString());
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
                    //var ordersBytes = MessagePackUtility.Serialize<ConcurrentDictionary<string, Order>>(MemoryData.Orders);
                    SQLiteHelper.UpdateOrder(ordersBytes);
                    _logger.WriteLog($"{startSerializeTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{endSerializeTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}");


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
