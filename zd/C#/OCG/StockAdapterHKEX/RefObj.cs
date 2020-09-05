using CommonClassLib;
using QuickFix.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockAdapterHKEX
{
    public class RefObj
    {
        public const char ORD_INIT_STATUS = (char)0;
        public bool isPendingRequest = false;
        public volatile char orderStatus = ORD_INIT_STATUS;
        public string[] strArray_;
        public QuickFix.FIX50.NewOrderSingle newOrderSingle;
        public List<QuickFix.Message> fromClient = new List<QuickFix.Message>();
        public List<QuickFix.FIX50.ExecutionReport> fromGlobex = new List<QuickFix.FIX50.ExecutionReport>();
        /// <summary>
        /// tag37
        /// </summary>
        public string orderID = null;
        /// <summary>
        /// NewOrderSingle 的 tag 11
        /// </summary>
        public string clOrderID = null;

        public decimal cumFilled = 0;

        public ManualResetEvent bizSynLock = new ManualResetEvent(false);

        public NetInfo lastSendInfo;

        public string[] strArray
        {
            get { return strArray_; }
            set
            {
                try
                {

                    strArray_ = value;

                    string workOrderIDMapString = strArray_[7];
                    if (string.IsNullOrEmpty(workOrderIDMapString))
                    {
                    }
                    else if (workOrderIDMapString.IndexOf(':') > 0)
                    {
                        string[] orderID = workOrderIDMapString.Split(':');
                        HKEXCommunication.specialReference.TryAdd(orderID[0], long.Parse(orderID[1]));
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        public char getOrderStatus()
        {
            return orderStatus;
        }

        public void addClientReq(QuickFix.FIX50.Message msg)
        {
            isPendingRequest = true;
            lock (fromClient)
            {
                fromClient.Add(msg);
            }

        }

        /// <summary>
        /// 订单生成、改单成功时候加入
        /// </summary>
        /// <param name="exeReport"></param>
        public void addGlobexRes(QuickFix.FIX50.ExecutionReport exeReport)
        {
            /*
            orderStatus = exeReport.OrdStatus.getValue();
            fromGlobex.Add(exeReport);
            if (orderStatus == OrdStatus.NEW || orderStatus == OrdStatus.REPLACED)
            {
                isPendingRequest = false;
                bizSynLock.Set();
            }
            */
            orderStatus = exeReport.OrdStatus.getValue();

            char execType = exeReport.ExecType.getValue();// Fix4.4
            fromGlobex.Add(exeReport);
            if (execType == ExecType.NEW || execType == ExecType.REPLACED || execType == ExecType.CANCELED)
            {
                isPendingRequest = false;
                bizSynLock.Set();
            }
        }

        public void addGlobexRes(QuickFix.FIX50.OrderCancelReject cancelRejMsg)
        {

            if (orderStatus == OrdStatus.PENDING_CANCEL || orderStatus == OrdStatus.PENDING_REPLACE)
            {
                orderStatus = ORD_INIT_STATUS;
            }

            isPendingRequest = false;
            fromClient.RemoveAt(fromClient.Count - 1);
            bizSynLock.Set();
        }

    }
}
