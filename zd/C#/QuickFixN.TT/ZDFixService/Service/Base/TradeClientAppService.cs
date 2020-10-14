using CommonClassLib;
using QuickFix.Fields;
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
using ZDFixService.Utility.Queue;
using QuickFix.FIX42;

using Message = QuickFix.Message;

namespace ZDFixService.Service.Base
{
    abstract class TradeClientAppService : ITradeService
    {
        #region 私有成员
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        IMessageQueue<NetInfo> _orderQueue = null;
        IMessageQueue<Message> _fixMessageQueue = null;
        #endregion

        #region 公有成员
        public event Action<string> ExecutionReport;
        public event Action<string> Logon;
        public event Action<string> Logout;
        #endregion


        internal TradeClientAppService()
        {

            TradeClient.Instance.Logon += (msg =>
            {
                Logon?.Invoke(msg);
                Init();
            });

            TradeClient.Instance.LogOut += (msg =>
            {
                Logout?.Invoke(msg);
            });

            TradeClient.Instance.ReceiveMsgFromApp += (message, sessionID) =>
            {
                _fixMessageQueue.Enqueue(message);

            };

        }

        public void Start()
        {
            TradeClient.Instance.SocketInitiator.Start();
        }

        private async void Init()
        {

            var messageQeue = Configurations.Configuration["ZDFixService:MessageQeue:Type"];
            this._orderQueue = UnityRegister.Resolve<IMessageQueue<NetInfo>>(messageQeue);// RabbitMQQueue<NetInfo>.Instance;
            _fixMessageQueue = new MemoryQueue<Message>();
            _orderQueue.Dequeue += ConsumerOrder;
            _fixMessageQueue.Dequeue += ConsumerFromAppMsg;
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


            //CommunicationClient.Instance.ReceiveMsg += ExecutionReport;
            //CommunicationClient.Instance.Connect();


        }

        public void Stop()
        {
            MemoryData.AppStop = true;
            TradeClient.Instance.SocketInitiator.Stop();
            WaitForCompleting();
            MemoryData.IPersist?.Persist();
            //CommunicationClient.Instance.Close();
            ZDFixServiceServer.Instance.Close();
            ZDFixServiceWebSocketServer.Instance.Close();
            //var re = NLog.LogManager.AutoShutdown;//true
            //NLog.LogManager.Shutdown();
        }

        private void WaitForCompleting()
        {
            _orderQueue.WaitForCompleting();
            _fixMessageQueue.WaitForCompleting();
        }


        public void Order(NetInfo netInfo)
        {
            _nLog.Info($"ClientIn:{netInfo.MyToString()}");
            if (!PreOrder(netInfo))
            {
                _nLog.Info($"Server forbidden - {netInfo.MyToString()}");
                if (string.IsNullOrEmpty(netInfo.errorMsg))
                {
                    netInfo.errorMsg = "Server forbidden!";
                }
                netInfo.Exception();
                ResponseClient(netInfo);
                return;
            }
            _orderQueue.Enqueue(netInfo);
        }


        private void ConsumerOrder(NetInfo netInfo)
        {

            try
            {

                if (netInfo.code == CommandCode.ORDER || netInfo.code == CommandCode.OrderStockHK)
                {
                    Order order = new Order();
                    order.OrderNetInfo = netInfo;
                    order.TempCommandCode = netInfo.code;
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
                netInfo.errorMsg = ex.Message;
                netInfo.Exception();
                ResponseClient(netInfo);
            }

        }

        /// <summary>
        /// 子类可重写该方法，拦截是否允许下单。
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

        private void ResponseClient(NetInfo netInfo)
        {

            var str = netInfo.MyToString();
            ExecutionReport?.Invoke(str);
            _nLog.Info($"ToClient:{str}");
            ZDFixServiceServer.Instance.SendMsgAsync<SocketMessage<NetInfo>>(netInfo);
            ZDFixServiceWebSocketServer.Instance.SendMsgAsync(str);

        }

        #region FromAppMsg

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
                ResponseClient(netInfo);
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

        protected abstract NetInfo Replaced(ExecutionReport execReport);

        protected abstract NetInfo Cancelled(ExecutionReport execReport);

        protected abstract NetInfo PartiallyFilledOrFilled(ExecutionReport execReport);

        #region Rejected
        NetInfo ExecType_Rejected(Message message)
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
                var tempCommandCode = order.TempCommandCode;

                if (string.IsNullOrEmpty(tempCommandCode))
                {
                    tempCommandCode = order.ClientOrderIDCommandCode[currentCliOrderID];
                }
                if (tempCommandCode == CommandCode.ORDER || tempCommandCode == CommandCode.OrderStockHK)
                {
                    //netInfo = order.OrderNetInfo.Clone();
                    netInfo.NewOrderSingleException(errorMessage, tempCommandCode);
                    MemoryData.Orders.TryRemove(order.SystemCode, out _);
                }
                else if (tempCommandCode == CommandCode.MODIFY || tempCommandCode == CommandCode.ModifyStockHK)
                {
                    //netInfo = order.AmendNetInfo;
                    //netInfo = order.OrderNetInfo.Clone(); 
                    netInfo.OrderCancelReplaceRequestException(errorMessage, order.NewOrderSingleClientID, tempCommandCode);
                }
                else if (tempCommandCode == CommandCode.CANCEL || tempCommandCode == CommandCode.CancelStockHK)
                {
                    //netInfo = order.OrderNetInfo.Clone(); 
                    netInfo.OrderCancelRequestException(errorMessage, order.NewOrderSingleClientID, tempCommandCode);
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
                ResponseClient(netInfo);
            }
        }
    }
}
