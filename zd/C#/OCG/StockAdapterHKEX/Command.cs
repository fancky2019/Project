using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAdapterHKEX
{
    class Command
    {
        /// <summary>
        /// 港股下单及返回指令
        /// </summary>
        public static string OrderStockHK = "OrdeStHK";
        /// <summary>
        /// 港股改单及返回指令
        /// </summary>
        public static string ModifyStockHK = "ModiStHK";
        /// <summary>
        /// 港股撤单及返回指令
        /// </summary>
        public static string CancelStockHK = "CancStHK";
        /// <summary>
        /// 港股成交返回指令
        /// </summary>
        public static string FilledStockHK = "FillStHK";
        /// <summary>
        /// 港股最新资金返回指令
        /// </summary>
        public static string AccountStockHK = "AccoStHK";
        /// <summary>
        /// 港股最新订单状态返回指令
        /// </summary>
        public static string StatusStockHK = "StatStHK";
        /// <summary>
        /// 港股最新持仓返回指令
        /// </summary>
        public static string HoldStockHK = "HoldStHK";
    }
}
