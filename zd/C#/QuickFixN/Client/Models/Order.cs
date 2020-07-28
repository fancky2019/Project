using CommonClassLib;
using QuickFix.FIX42;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
   public class Order
    {
        /// <summary>
        /// 客户号  --redis hash key
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 最新单的TAG 11--- redis key
        /// </summary>
        public string ClientID { get; set; }

        public string NewOrderSingleClientID { get; set; }
        /// <summary>
        /// Total number of contracts that have filled over the life of this order
        /// </summary>
        public decimal CumQty { get; set; }
        
        /// <summary>
        /// 下单
        /// </summary>
        public NetInfo OrderNetInfo { get; set; }
        /// <summary>
        /// 改单
        /// </summary>
        public NetInfo AmendNetInfo { get; set; }

        /// <summary>
        /// 撤单
        /// </summary>
        public NetInfo CancelNetInfo { get; set; }
        /// <summary>
        /// 下单FIX
        /// </summary>
        public string NewOrderSingle { get; set; }
        /// <summary>
        /// 改单FIX
        /// </summary>
        public string OrderCancelReplaceRequest { get; set; }
        
    }
}
