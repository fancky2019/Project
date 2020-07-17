using Client.FixUtility;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Service
{
    public class TradeClientAppService
    {
        #region 私有成员
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        private BlockingCollection<QuickFix.Message> _receiveFromAppMsgs = new BlockingCollection<QuickFix.Message>();
        private BlockingCollection<object> _orders = new BlockingCollection<object>();
        #endregion

        #region 公有成员
        public static TradeClientAppService Instance { get; private set; }
        public event Action<string> ExecutionReport;
        public event Action<string> Logon;
        public event Action<string> LogonOut;
        #endregion

        static TradeClientAppService()
        {
            Instance = new TradeClientAppService();
        }

        private TradeClientAppService()
        {
            TradeClient.Instance.Logon += (msg =>
            {
                Logon?.Invoke(msg);

            });

            TradeClient.Instance.LogOut += (msg =>
            {
                LogonOut?.Invoke(msg);
            });

            TradeClient.Instance.ReceiveMsgFromApp += (message, sessionID) =>
            {
                if (!_receiveFromAppMsgs.TryAdd(message, 1000))
                {
                    //异常
                }

            };


            Task.Run(() =>
            {
                ConsumerOrders();
            });
            Task.Run(() =>
            {
                ConsumerFromAppMsgs();
            });
        }



        public void Connect()
        {
            TradeClient.Instance.SocketInitiator.Start();
        }

        public void Disnnect()
        {
            TradeClient.Instance.SocketInitiator.Stop();
            WaitForAdding();
        }

        private void WaitForAdding()
        {
            //为了避免异常--未扔到交易所的单在内存就丢失
            while (!_orders.IsAddingCompleted && !_receiveFromAppMsgs.IsAddingCompleted)
            {
                //所有的单据不送交易所处理。
            }
        }

        public void Order(Object obj)
        {
            //if (!_orders.IsAddingCompleted)
            //{

            //    _orders.Add(obj);
            //}
            if (!_orders.TryAdd(obj, 1000))
            {
                //异常
            }
        }

        private void ConsumerOrders()
        {
            foreach (var item in _orders.GetConsumingEnumerable())
            {
                ConsumerOrder(item);
            }
        }


        private void ConsumerOrder(Object order)
        {

        }

        private void ConsumerFromAppMsgs()
        {
            try
            {
                foreach (var message in _receiveFromAppMsgs.GetConsumingEnumerable())
                {

                    ConsumerFromAppMsg(message);
                }
            }
            catch (Exception ex)
            {
                _nLog.Info(ex.ToString());
            }
        }

        private void ConsumerFromAppMsg(QuickFix.Message message)
        {
            switch (message)
            {
                case QuickFix.FIX44.ExecutionReport executionReport:
                    //ExecutionReport?.Invoke(executionReport.ToString());
                    char execType = executionReport.ExecType.getValue();

                    string netInfo = null;
                    switch (execType)
                    {
                        case ExecType.NEW:
                            //netInfo = replyOrderCreation(execReport);
                            break;

                        case ExecType.FILL:
                        case ExecType.PARTIAL_FILL:
                            //netInfo = replyFill(execReport);
                            break;

                        case ExecType.CANCELED:
                            //netInfo = replyCancelled(execReport);
                            break;

                        case ExecType.REJECTED:
                            //netInfo = replyRejected(execReport);
                            break;

                        case ExecType.PENDING_CANCEL:
                            //doPendingCancel(execReport);
                            break;

                        case ExecType.REPLACED:
                            //netInfo = replyReplaced(execReport);
                            break;

                        case ExecType.EXPIRED:
                            //netInfo = doExpired(execReport);
                            //netInfo = replyCancelled(execReport);
                            break;
                        default:
                            break;
                            //case GlobexExt.ORD_STATUS_TRADE_CANCELLATION:
                            //    break;
                    }
                    break;
                case QuickFix.FIX44.OrderCancelReject orderCancelReject:
                    break;
                case QuickFix.FIX44.News news:
                    break;
                case QuickFix.FIX44.BusinessMessageReject businessMessageReject:
                    break;
                default:
                    break;
            }
        }

    }
}
