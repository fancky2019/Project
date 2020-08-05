using Client.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utility.MemoryDataManager.Persist
{
    class FilePersist : IPersist
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        private object _lockObj = new object();
        public void Persist()
        {
            lock (_lockObj)
            {


                try
                {
                    TxtFile.SaveString(ConfigurationManager.AppSettings["OrderIDFilePath"].ToString(), MemoryData.LastClientOrderID.ToString());
                }
                catch (Exception ex)
                {
                    _nLog.Error("Save LastOrderID Failed");
                    _nLog.Error(ex.ToString());
                }



                try
                {

                    //var jsonStr = NewtonsoftHelper.SerializeObject(MemoryData.Orders);
                    var jsonStr = NewtonsoftHelper.JsonSerializeObjectFormat(MemoryData.Orders);
                    //var jsonStr = MessagePackUtility.SerializeToJson<ConcurrentDictionary<string, Order>>(Orders);
                    TxtFile.SaveString(ConfigurationManager.AppSettings["PersistOrdersPath"].ToString(), jsonStr);

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
                var clientOrderID = TxtFile.ReadString(ConfigurationManager.AppSettings["OrderIDFilePath"].ToString()).Trim();
                if (!string.IsNullOrEmpty(clientOrderID))
                {
                    MemoryData.LastClientOrderID = long.Parse(clientOrderID);
                }
            }
            catch (Exception ex)
            {
                _nLog.Error("Load Orders Failed");
                _nLog.Error(ex.ToString());
            }

            try
            {
                var ordersStr = TxtFile.ReadString(ConfigurationManager.AppSettings["PersistOrdersPath"].ToString());
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

          

            try
            {
                foreach (var item in MemoryData.Orders.Values)
                {
                    var cliOrderID = long.Parse(item.CurrentCliOrderID);
                    MemoryData.UsingCliOrderIDSystemCode.TryAdd(cliOrderID, item.SystemCode);
                }
            }
            catch (Exception ex)
            {
                _nLog.Error("Load UsingCliOrderID Failed");
                _nLog.Error(ex.ToString());
            }



        }
    }
}
