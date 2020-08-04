using Client.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utility
{
    class MemoryDataManager
    {


        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Key:SystemCode , Value:Order
        /// </summary>
        internal static ConcurrentDictionary<string, Order> Orders { get; set; }

        /// <summary>
        /// 根据ExecutionReport里CliOrderID映射到SystemCode
        /// </summary>
        internal static ConcurrentDictionary<string, string> TempCliOrderIDSystemCode { get; set; }

        // SortedDictionary O(logn)、ConcurrentDictionary O(1)
        /// <summary>
        /// 当前使用的CliOrderID,一个交易日内CliOrderID不能重复。
        /// </summary>
        internal static ConcurrentDictionary<long, long> UsingCliOrderID { get; set; }




        internal static long LastClientOrderID { get; private set; }
        static long _beginOrderId = 0;
        static long _endOrderId = 0;


        static MemoryDataManager()
        {
            Orders = new ConcurrentDictionary<string, Order>();
            TempCliOrderIDSystemCode = new ConcurrentDictionary<string, string>();
            UsingCliOrderID = new ConcurrentDictionary<long, long>();

            var cliOrderIDScope = ConfigurationManager.AppSettings["CliOrderIDScope"].ToString();
            if (string.IsNullOrEmpty(cliOrderIDScope))
            {
                _nLog.Error("Order_ID_Scope is null!");
            }
            _beginOrderId = long.Parse(cliOrderIDScope.Split(',')[0]);
            _endOrderId = long.Parse(cliOrderIDScope.Split(',')[1]);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static long GetNextClOrderID()
        {

            LastClientOrderID++;
            LastClientOrderID = LastClientOrderID <= _beginOrderId ? _beginOrderId + 1 : LastClientOrderID;
            LastClientOrderID = LastClientOrderID >= _endOrderId ? _beginOrderId + 1 : LastClientOrderID;
            //UsingCliOrderID 待优化
            if (Orders.ContainsKey(LastClientOrderID.ToString()))
            {
                GetNextClOrderID();
            }


            if (UsingCliOrderID.ContainsKey(LastClientOrderID))
            {
                GetNextClOrderID();
            }

            return LastClientOrderID;
        }




        public static void Persist()
        {
            try
            {
                TxtFile.SaveString(ConfigurationManager.AppSettings["OrderIDFilePath"].ToString(), LastClientOrderID.ToString());
            }
            catch (Exception ex)
            {
                _nLog.Error("Save LastOrderID Failed");
                _nLog.Error(ex.ToString());
            }

            try
            {
                var jsonStr = NewtonsoftHelper.SerializeObject(Orders);
                //var jsonStr = MessagePackUtility.SerializeToJson<ConcurrentDictionary<string, Order>>(Orders);
                TxtFile.SaveString(ConfigurationManager.AppSettings["PersistOrdersPath"].ToString(), jsonStr);

            }
            catch (Exception ex)
            {
                _nLog.Error("Save Orders Failed");
                _nLog.Error(ex.ToString());
            }

            try
            {
                var jsonStr = NewtonsoftHelper.SerializeObject(TempCliOrderIDSystemCode);
                //var jsonStr = MessagePackUtility.SerializeToJson<ConcurrentDictionary<string, Order>>(Orders);
                TxtFile.SaveString(ConfigurationManager.AppSettings["TempCliOrderIDSystemCode"].ToString(), jsonStr);

            }
            catch (Exception ex)
            {
                _nLog.Error("Save TempCliOrderIDSystemCode Failed");
                _nLog.Error(ex.ToString());
            }



        }

        public static void Load()
        {
            try
            {
                var clientOrderID = TxtFile.ReadString(ConfigurationManager.AppSettings["OrderIDFilePath"].ToString()).Trim();
                if (!string.IsNullOrEmpty(clientOrderID))
                {
                    LastClientOrderID = long.Parse(clientOrderID);
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
                    Orders = NewtonsoftHelper.DeserializeObject<ConcurrentDictionary<string, Order>>(ordersStr);
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
                var ordersStr = TxtFile.ReadString(ConfigurationManager.AppSettings["TempCliOrderIDSystemCode"].ToString());
                if (!string.IsNullOrEmpty(ordersStr))
                {
                    TempCliOrderIDSystemCode = NewtonsoftHelper.DeserializeObject<ConcurrentDictionary<string, string>>(ordersStr);
                    //Orders =  MessagePackUtility.DeserializeFromJson<ConcurrentDictionary<string, Order>>(ordersStr);
                }
            }
            catch (Exception ex)
            {
                _nLog.Error("Load TempCliOrderIDSystemCode Failed");
                _nLog.Error(ex.ToString());
            }


            try
            {
                foreach (var item in Orders.Values)
                {
                    var cliOrderID = long.Parse(item.CurrentCliOrderID);
                    UsingCliOrderID.TryAdd(cliOrderID, cliOrderID);
                }
            }
            catch (Exception ex)
            {
                _nLog.Error("Load UsingCliOrderID Failed");
                _nLog.Error(ex.ToString());
            }



        }

        /// <summary>
        /// 公司遗留的数据库设计不合理问题。设计成int存储CliOrderID。
        /// CliOrderID fix 是string 类型;利用snowflake 算法来生成此CliOrderID
        /// </summary>
        /// <param name="cliOrderID"></param>
        /// <returns></returns>

        internal static Order GetOrderByCliOrderID(string cliOrderID)
        {
            string systemCode;
            TempCliOrderIDSystemCode.TryGetValue(cliOrderID, out systemCode);
            if (string.IsNullOrEmpty(systemCode))
            {
                throw new Exception($"TempCliOrderIDSystemCode can not find CliOrderID - {cliOrderID}");
            }
            Order order;
            Orders.TryGetValue(systemCode, out order);
            if (order == null)
            {
                throw new Exception($"Orders  can not find systemCode - {systemCode}! ");
            }

            return order;
        }



    }
}
