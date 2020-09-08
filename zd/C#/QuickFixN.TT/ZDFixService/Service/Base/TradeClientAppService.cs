using CommonClassLib;
using QuickFix.Fields;
using QuickFix.FIX42;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ZDFixService.FixUtility;
using ZDFixService.Models;
using ZDFixService.Service.MemoryDataManager;
using ZDFixService.SocketNetty;
using ZDFixService.Service.ZDCommon;
using ZDFixService.Utility;

namespace ZDFixService.Service.Base
{
    abstract class TradeClientAppService : ITradeService
    {
        #region 私有成员
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        private BlockingCollection<QuickFix.Message> _receiveFromAppMsgs = new BlockingCollection<QuickFix.Message>();
        private BlockingCollection<NetInfo> _orderQueue = new BlockingCollection<NetInfo>();
        #endregion

        #region 公有成员
        public event Action<string> ExecutionReport;
        public event Action<string> Logon;
        public event Action<string> Logout;
        #endregion


        internal TradeClientAppService()
        {
            Init();
            TradeClient.Instance.Logon += (msg =>
            {
                Logon?.Invoke(msg);
            });

            TradeClient.Instance.LogOut += (msg =>
            {
                Logout?.Invoke(msg);
            });

            TradeClient.Instance.ReceiveMsgFromApp += (message, sessionID) =>
            {
                if (!_receiveFromAppMsgs.IsAddingCompleted)
                {
                    if (!_receiveFromAppMsgs.TryAdd(message, 1000))
                    {
                        //异常

                    }
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

        public void Start()
        {
            TradeClient.Instance.SocketInitiator.Start();
        }

        private async void Init()
        {
            MemoryData.Init();
            //Task.Run(() =>
            //{
            //    ZDFixServiceServer.Instance.RunServerAsync().Wait();
            //});
            //Task.Run(() =>
            //{
            //    ZDFixServiceWebSocketServer.Instance.RunServerAsync().Wait();
            //});

            Task.Run(() =>
            {
                Scheduler.Init();
            });

            await ZDFixServiceServer.Instance.RunServerAsync();
            await ZDFixServiceWebSocketServer.Instance.RunServerAsync();
        }

        public void Stop()
        {
            MemoryData.AppStop = true;
            TradeClient.Instance.SocketInitiator.Stop();
            WaitForAdding();
            MemoryData.IPersist?.Persist();
            ZDFixServiceServer.Instance.Close();
            ZDFixServiceWebSocketServer.Instance.Close();
            //var re = NLog.LogManager.AutoShutdown;//true
            //NLog.LogManager.Shutdown();
        }

        private void WaitForAdding()
        {
            _orderQueue.CompleteAdding();
            _receiveFromAppMsgs.CompleteAdding();
            //为了避免异常--未扔到交易所的单在内存就丢失
            while (_orderQueue.Count != 0 || _receiveFromAppMsgs.Count != 0)
            {
                //直到所有的单据处理完成。
                Thread.Sleep(1);

            }


        }


        public void Order(NetInfo netInfo)
        {
            _nLog.Info($"ClientIn:{netInfo.MyToString()}");
            if (!PreOrder(netInfo))
            {
                _nLog.Info($"PreOrder return - {netInfo.MyToString()}");
                return;
            }
            if (!_orderQueue.IsAddingCompleted)
            {
                if (!_orderQueue.TryAdd(netInfo, 1000))
                {
                    //异常
                }
            }

        }

        private void ConsumerOrders()
        {
            foreach (NetInfo netInfo in _orderQueue.GetConsumingEnumerable())
            {
                try
                {

                    if (netInfo.code == CommandCode.ORDER || netInfo.code == CommandCode.OrderStockHK)
                    {
                        Order order = new Order();
                        order.OrderNetInfo = netInfo;
                        order.CommandCode = netInfo.code;
                        NewOrderSingle(order);
                    }
                    else if (netInfo.code == CommandCode.MODIFY || netInfo.code == CommandCode.ModifyStockHK)
                    {
                        OrderCancelReplaceRequest(netInfo);
                    }
                    else if (netInfo.code == CommandCode.CANCEL || netInfo.code == CommandCode.CancelStockHK)
                    {

                        OrderCancelRequest(netInfo);
                    }
                    else
                    {
                        throw new Exception("Can not find appropriate CommandCode");
                    }
                }
                catch (Exception ex)
                {
                    _nLog.Info(ex.ToString());
                    var str = netInfo?.MyToString();
                    ExecutionReport?.Invoke(str);
                    ZDFixServiceServer.Instance.SendMsgAsync<SocketMessage<NetInfo>>(netInfo);
                    ZDFixServiceWebSocketServer.Instance.SendMsgAsync(str);
                }
            }
        }

        /// <summary>
        /// 子类可实现该方法，拦截是否允许下单。
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        protected virtual bool PreOrder(NetInfo netInfo)
        {
            return true;
        }

        protected abstract void NewOrderSingle(Order order);

        protected abstract void OrderCancelReplaceRequest(NetInfo netInfo);

        protected abstract void OrderCancelRequest(NetInfo netInfo);

        #region FromAppMsg

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
            NetInfo netInfo = null;
            bool unHandleMessage = false;
            try
            {
                switch (message)
                {
                    case ExecutionReport executionReport:
                        //ExecutionReport?.Invoke(executionReport.ToString());
                        char execType = executionReport.ExecType.getValue();


                        switch (execType)
                        {
                            case ExecType.NEW:
                                netInfo = ExecType_New(executionReport);
                                break;

                            case ExecType.FILL:
                            case ExecType.PARTIAL_FILL:
                                netInfo = PartiallyFilledOrFilled(executionReport);
                                break;

                            case ExecType.CANCELED:
                                netInfo = Cancelled(executionReport);
                                break;

                            case ExecType.REJECTED:
                                netInfo = ExecType_Rejected(message);
                                break;
                            case ExecType.REPLACED:
                                netInfo = Replaced(executionReport);
                                break;

                            case ExecType.EXPIRED:
                                unHandleMessage = true;
                                //netInfo = doExpired(execReport);
                                //netInfo = replyCancelled(execReport);
                                break;
                            case ExecType.PENDING_NEW:
                                unHandleMessage = true;
                                break;
                            case ExecType.PENDING_CANCELREPLACE:
                                unHandleMessage = true;
                                break;
                            //
                            //case ExecType.PENDING_CANCEL:
                            //    break;
                            default:
                                unHandleMessage = true;
                                break;
                                //case GlobexExt.ORD_STATUS_TRADE_CANCELLATION:
                                //    break;
                        }
                        break;
                    case OrderCancelReject orderCancelReject:
                        netInfo = ExecType_Rejected(message);
                        break;
                    case News news:
                        break;
                    case BusinessMessageReject businessMessageReject:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }

            if (netInfo != null)
            {
                var str = netInfo.MyToString();
                ExecutionReport?.Invoke(str);
                _nLog.Info($"ToClient:{str}");
                ZDFixServiceServer.Instance.SendMsgAsync<SocketMessage<NetInfo>>(netInfo);
                ZDFixServiceWebSocketServer.Instance.SendMsgAsync(str);

            }
            else
            {
                var tip = "Deal Failed";
                if (unHandleMessage)
                {
                    tip = "UnHandle Message";
                }

                _nLog.Error($"{tip}:{message.ToString()}");
            }

        }


        #region ExecutionReport

        protected abstract NetInfo ExecType_New(ExecutionReport execReport);

        protected abstract NetInfo Replaced(QuickFix.FIX42.ExecutionReport execReport);

        protected abstract NetInfo Cancelled(QuickFix.FIX42.ExecutionReport execReport);

        protected abstract NetInfo PartiallyFilledOrFilled(QuickFix.FIX42.ExecutionReport execReport);

        #region Rejected
        NetInfo ExecType_Rejected(QuickFix.Message message)
        {
            NetInfo netInfo = null;
            try
            {
                if (!message.IsSetField(Tags.ClOrdID))
                {
                    _nLog.Error("Message is not set tag 11 ");
                    return netInfo;
                }
                var currentCliOrderID = message.GetString(Tags.ClOrdID).Replace("DA", ""); ;
                //var order = MemoryDataManager.Orders.Values.Where(p => p.TempCliOrderID == currentCliOrderID).FirstOrDefault();
                var order = MemoryData.GetOrderByCliOrderID(currentCliOrderID);
                order.Pending = false;

                MemoryData.UsingCliOrderIDSystemCode.TryRemove(long.Parse(currentCliOrderID), out _);


                var errorMessage = "";

                if (message.IsSetField(Tags.Text))
                {
                    errorMessage = message.GetString(Tags.Text);

                }
                netInfo = order.OrderNetInfo.Clone();
                if (order.CommandCode == CommandCode.ORDER || order.CommandCode == CommandCode.OrderStockHK)
                {
                    //netInfo = order.OrderNetInfo.Clone();
                    netInfo.NewOrderSingleException(errorMessage, order.CommandCode);
                    MemoryData.Orders.TryRemove(order.SystemCode, out _);
                }
                else if (order.CommandCode == CommandCode.MODIFY || order.CommandCode == CommandCode.ModifyStockHK)
                {
                    //netInfo = order.AmendNetInfo;
                    //netInfo = order.OrderNetInfo.Clone(); 
                    netInfo.OrderCancelReplaceRequestException(errorMessage, order.NewOrderSingleClientID, order.CommandCode);
                }
                else if (order.CommandCode == CommandCode.CANCEL || order.CommandCode == CommandCode.CancelStockHK)
                {
                    //netInfo = order.OrderNetInfo.Clone(); 
                    netInfo.OrderCancelRequestException(errorMessage, order.NewOrderSingleClientID, order.CommandCode);
                }
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
            return netInfo;
        }
        #endregion

        #endregion

        #endregion

        internal virtual void ServerAutoCancelOrder(NetInfo netInfo)
        {
            if (netInfo != null)
            {
                var str = netInfo.MyToString();
                ExecutionReport?.Invoke(str);
                _nLog.Info($"ToClient:{str}");
                ZDFixServiceServer.Instance.SendMsgAsync<SocketMessage<NetInfo>>(netInfo);
                ZDFixServiceWebSocketServer.Instance.SendMsgAsync(str);

            }
        }
    }
}
