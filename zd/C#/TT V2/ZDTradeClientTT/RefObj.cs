using CommonClassLib;
using QuickFix.Fields;
using QuickFix.FIX42;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZDTradeClientTT
{
    public class RefObj
    {
        public const char ORD_INIT_STATUS = (char)0;
        public bool isPendingRequest = false;
        public volatile char orderStatus = ORD_INIT_STATUS;
        public string[] strArray;
        public QuickFix.FIX42.NewOrderSingle newOrderSingle;
        public List<QuickFix.Message> fromClient = new List<QuickFix.Message>();
        public List<QuickFix.FIX42.ExecutionReport> fromGlobex = new List<QuickFix.FIX42.ExecutionReport>();
        public string orderID = null;
        public string clOrderID = null;

        public decimal cumFilled = 0;

        public ManualResetEvent bizSynLock = new ManualResetEvent(false);
        public NetInfo NetInfo { get; set; }
        public char getOrderStatus()
        {
            return orderStatus;
        }

        /// <summary>
        /// 将（下改撤）fix message 加入内存
        /// </summary>
        /// <param name="msg"></param>
        public void addClientReq(QuickFix.FIX42.Message msg)
        {
            isPendingRequest = true;
            fromClient.Add(msg);
        }

        public void addGlobexRes(QuickFix.FIX42.ExecutionReport exeReport)
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

        public void addGlobexRes(QuickFix.FIX42.OrderCancelReject cancelRejMsg)
        {
            isPendingRequest = false;
            fromClient.RemoveAt(fromClient.Count - 1);
            bizSynLock.Set();
        }


        public (string MessageType, QuickFix.Message Message) GetOrder(string clOrdId)
        {
            string lastClOrdID = string.Empty;
            // var last = this.fromClient.Where(p=>p.)
            string messageType = string.Empty;
            foreach (var message in this.fromClient)
            {


                switch (message)
                {
                    case OrderCancelRequest cancelOrder:
                        lastClOrdID = cancelOrder.ClOrdID.getValue();
                        messageType = "D";
                        break;
                    case NewOrderSingle newOrder:
                        lastClOrdID = newOrder.ClOrdID.getValue();
                        messageType = "F";
                        break;
                    case OrderCancelReplaceRequest replaceOrder:
                        lastClOrdID = replaceOrder.ClOrdID.getValue();
                        messageType = "G";
                        break;

                }
                if(!string.IsNullOrEmpty(lastClOrdID))
                {
                    if(lastClOrdID==clOrdId)
                    {
                        return (messageType, message);
                    }
                }
            }
            return ("",null);

        }




    }

}
