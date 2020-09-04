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

namespace ZDFixService.Service.MemoryDataManager
{
    partial class MemoryData
    {


        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Key:SystemCode , Value:Order
        /// </summary>
        public static ConcurrentDictionary<string, Order> Orders { get; set; }

        // SortedDictionary O(logn)、ConcurrentDictionary O(1)
        /// <summary>
        /// 当前使用的CliOrderID,一个交易日内CliOrderID不能重复。
        /// 根据ExecutionReport里CliOrderID映射到SystemCode
        /// Key:CliOrderID, Value:SystemCode
        /// </summary>
        internal static ConcurrentDictionary<long, string> UsingCliOrderIDSystemCode { get; set; }

        /// <summary>
        /// 最新使用的ClientOrderID
        /// </summary>
        internal static long LastClientOrderID { get; set; }

        static ClOrderIDGenerator _clOrderIDGenerator;

        internal static void Init()
        {
            Orders = new ConcurrentDictionary<string, Order>();
            UsingCliOrderIDSystemCode = new ConcurrentDictionary<long, string>();

            InitPersist();

            if (_clOrderIDGenerator == null)
            {
                _clOrderIDGenerator = new ClOrderIDGenerator(LastClientOrderID);
            }
        }



        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //static long GetNextClOrderID()
        //{
        //    if (_snowFlake == null)
        //    {
        //        LastClientOrderID++;
        //        LastClientOrderID = LastClientOrderID <= _beginOrderId ? _beginOrderId + 1 : LastClientOrderID;
        //        LastClientOrderID = LastClientOrderID >= _endOrderId ? _beginOrderId + 1 : LastClientOrderID;
        //    }
        //    else
        //    {
        //        LastClientOrderID = _snowFlake.NextId();
        //    }

        //    if (UsingCliOrderIDSystemCode.ContainsKey(LastClientOrderID))
        //    {
        //        GetNextClOrderID();
        //    }
        //    return LastClientOrderID;
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static long GetNextClOrderID()
        {
            LastClientOrderID = _clOrderIDGenerator.GetNextClOrderID();
            if (UsingCliOrderIDSystemCode.ContainsKey(LastClientOrderID))
            {
                GetNextClOrderID();
            }
            return LastClientOrderID;
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
            UsingCliOrderIDSystemCode.TryGetValue(long.Parse(cliOrderID), out systemCode);
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

        private static void RemoveExpireDayOrder()
        {
            //删除过期日单
            foreach (var key in Orders.Keys)
            {
                //var key = Orders.Keys.ElementAt<string>(i);
                if (Orders.TryGetValue(key, out Order order))
                {
                    if (!order.IsGTCOrder && order.CreateNewOrderSingleTime.Date != DateTime.Now.Date)
                    {
                        if (!string.IsNullOrEmpty(order.CurrentCliOrderID))
                        {
                            UsingCliOrderIDSystemCode.TryRemove(long.Parse(order.CurrentCliOrderID), out _);
                        }
                        else
                        {
                            _nLog.Info($"SysCode - {order.SystemCode},CurrentCliOrderID is null.");
                        }
                        Orders.TryRemove(key, out _);
                    }
                }
            }


        }

        internal static void InitUsingCliOrderIDSystemCode()
        {

            foreach (var item in Orders.Values)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.CurrentCliOrderID))
                    {
                        var cliOrderID = long.Parse(item.CurrentCliOrderID);
                        UsingCliOrderIDSystemCode.TryAdd(cliOrderID, item.SystemCode);
                    }
                    else
                    {
                        _nLog.Info($"SysCode - {item.SystemCode},CurrentCliOrderID is null.");
                    }
                }
                catch (Exception ex)
                {
                    _nLog.Error("InitUsingCliOrderIDSystemCode Failed");
                    _nLog.Error(ex.ToString());
                }
            }

        }
    }
}
