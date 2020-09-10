using ZDFixService.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDFixService;
using ZDFixService.Utility;

namespace ZDFixService.Service.MemoryDataManager.Persist
{
    class FilePersist : IPersist
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        Log _logger = LogManager.GetLogger("SerializeTime");
        private object _lockObj = new object();
        public void Persist()
        {
            lock (_lockObj)
            {
                try
                {

                    TxtFile.SaveString(Configurations.Configuration["ZDFixService:Persist:File:OrderIDFilePath"], MemoryData.LastClientOrderID.ToString());
                }
                catch (Exception ex)
                {
                    _nLog.Error("Save LastClientOrderID Failed");
                    _nLog.Error(ex.ToString());
                }



                try
                {
                    var startSerializeTime = DateTime.Now;
                    //var jsonStr = NewtonsoftHelper.SerializeObject(MemoryData.Orders);
                    var jsonStr = NewtonsoftHelper.JsonSerializeObjectFormat(MemoryData.Orders);
                    var endSerializeTime = DateTime.Now;
                    //var jsonStr = MessagePackUtility.SerializeToJson<ConcurrentDictionary<string, Order>>(Orders);
                    TxtFile.SaveString(Configurations.Configuration["ZDFixService:Persist:File:PersistOrdersPath"].ToString(), jsonStr);
                    _logger.WriteLog($"{startSerializeTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{endSerializeTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}");

                }
                catch (Exception ex)
                {
                    _nLog.Error("Save Orders Failed");
                    _nLog.Error(ex.ToString());
                }



            }

        }

        public void Load()
        {
            try
            {
                var clientOrderID = TxtFile.ReadString(Configurations.Configuration["ZDFixService:Persist:File:OrderIDFilePath"].ToString()).Trim();
                if (!string.IsNullOrEmpty(clientOrderID))
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
                var ordersStr = TxtFile.ReadString(Configurations.Configuration["ZDFixService:Persist:File:PersistOrdersPath"].ToString());
                if (!string.IsNullOrEmpty(ordersStr))
                {
                    MemoryData.Orders = NewtonsoftHelper.DeserializeObject<ConcurrentDictionary<string, Order>>(ordersStr);
                    //Orders =  MessagePackUtility.DeserializeFromJson<ConcurrentDictionary<string, Order>>(ordersStr);
                }
            }
            catch (Exception ex)
            {
                _nLog.Error("Load Orders Failed");
                _nLog.Error(ex.ToString());
            }



            MemoryData.InitUsingCliOrderIDSystemCode();



        }
    }
}
