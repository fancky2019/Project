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
        public bool Pending { get; set; }
        /// <summary>
        /// 客户号  --redis hash key
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 最新单的TAG 11--- redis key
        /// </summary>
        public string CurrentCliOrderID { get; set; }

        /// <summary>
        /// 暂存客户端生产的单号,订单请求可能不成功，请求成功将此值赋值给CurrentCliOrderID，
        /// 同时清空此值
        /// </summary>
        public string TempCliOrderID { get; set; }

        /// <summary>
        /// orderNo
        /// </summary>
        public string NewOrderSingleClientID { get; set; }

        /// <summary>
        /// 上手的ID tag 37 (TT平台类似我们的sysCode不会变的）
        /// </summary>
        public string OrderID { get; set; }
        
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
        /// 下改撤
        /// </summary>
        public string CommandCode { get; set; }
        
        /// <summary>
        /// 订单生成时间，订单持久化用，不保存过期的日单，只保存GTC单
        /// </summary>
        public DateTime CreateNewOrderSingleTime { get; set; }
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
