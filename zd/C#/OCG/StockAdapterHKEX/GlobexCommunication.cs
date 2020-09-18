using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonClassLib;
using CommonClassLib.ContractTransfer;
using QuickFix.Fields;
using System.Collections.Concurrent;
using System.Threading;
using AuthCommon;
using QuickFix.FIX50;
using System.IO;
using StockAdapterHKEX;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using StockAdapterHKEX.Common;

namespace StockAdapterHKEX
{
    public class HKEXCommunication
    {

        public TradeApp tradeApp = null;
        private HandlInst handInst = new HandlInst('1');
        private SecurityIDSource securityIDSource = new SecurityIDSource("8");
        private SecurityExchange securityExchange = new SecurityExchange("XHKG");
        private OrdType orderType = new OrdType(OrdType.LIMIT);

        private CustomerOrFirm customerOrFirm = new CustomerOrFirm(0);
        private ManualOrderIndicator moi = new ManualOrderIndicator(true);

        public bool isTestMode = false;
        public ICustomFixStrategy strategy = null;
        private Log globexIFLogger = null;
        private static Log _receiveMsgsLog = null;

        private string sessionID = null;

        public string ConfigFile { get; set; }
        private CfgManager cfgInstance;

        /// <summary>
        /// 往交易所发下单message时候加入
        /// key:ClOrdID(tag11),value:RefObj
        /// Mapping between ClOrdID and ZD.accountNo + ZD.systemNo
        /// </summary>
        public ConcurrentDictionary<long, RefObj> xReference = new ConcurrentDictionary<long, RefObj>();

        /// <summary>
        /// key:ClOrdId,value:IsPending
        /// </summary>
        public ConcurrentDictionary<long, bool> ClOrdIdIsPending = new ConcurrentDictionary<long, bool>();
        // Key: upper orderID
        /// <summary>
        /// 交易所返回订单确认的时候初次加入
        /// key:OrderID(tag37),value:RefObj
        /// </summary>
        public ConcurrentDictionary<string, RefObj> downReference = new ConcurrentDictionary<string, RefObj>();

        public static ConcurrentDictionary<string, long> specialReference = new ConcurrentDictionary<string, long>();

        public Dictionary<string, uint> cmeProductCdToSecuIdDict = new Dictionary<string, uint>();

        /// <summary>
        /// 港股交易时段及状态 v2.xlsx中的分订单类型：竞价限价单（5-->0）、增强限价单（7-->1）
        /// 的二位数组索引X轴
        /// </summary>
        private Dictionary<string, int> OrderTypeIdx = null;
        /// <summary>
        /// 港股交易时段及状态 v2.xlsx中的下单时间段Y轴
        /// </summary>
        private List<TimeSpan> timeList = null;
        /*
         * 根据X、Y轴获取表格的值
         * P:待送出
         * Y:可以下单
         * N:不可以下单
         * -:当拒绝处理
         */
        //private char[][] newOrderMatrix = null;
        /// <summary>
        /// 港股交易时段及状态 v2.xlsx - 下单表
        /// </summary>
        private char[,] newOrderMatrix = new char[11, 2] { { 'P', 'N' }, { 'Y', 'N' }, { 'N', 'P' }, { 'N', 'P' }, { 'N', 'Y' }, { 'N', 'P' }, { 'N', 'P' }, { 'N', 'Y' }, { 'P', 'N' }, { 'Y', 'N' }, { 'Y', 'N' } };
        //UAT配置
        //private char[,] newOrderMatrix = new char[11, 2] { { 'P', 'N' }, { 'Y', 'N' }, { 'N', 'P' }, { 'N', 'P' }, { 'N', 'Y' }, { 'N', 'P' }, { 'N', 'P' }, { 'Y', 'Y' }, { 'P', 'N' }, { 'Y', 'N' }, { 'Y', 'N' } };


        ///// <summary>
        /// 港股交易时段及状态 v2.xlsx - 改单表
        /// </summary>
        private char[,] modifyOrderMatrix = new char[11, 2] { { '-', '-' }, { 'Y', '-' }, { 'N', '-' }, { 'P', '-' }, { 'Y', 'Y' }, { 'P', 'P' }, { 'P', 'P' }, { 'Y', 'Y' }, { 'P', 'P' }, { 'Y', 'Y' }, { 'N', 'N' } };
        /// <summary>
        /// 港股交易时段及状态 v2.xlsx - 撤单表
        /// </summary>
        private char[,] cancelOrderMatrix = new char[11, 2] { { '-', '-' }, { 'Y', '-' }, { 'N', '-' }, { 'P', '-' }, { 'Y', 'Y' }, { 'P', 'P' }, { 'Y', 'Y' }, { 'Y', 'Y' }, { 'P', 'P' }, { 'Y', 'Y' }, { 'N', 'N' } };


        private TimeSpan offsetTS = new TimeSpan(0, 0, 0);

        /// <summary>
        /// TPS ：每秒下单数量
        /// </summary>
        public int messageThrottle = 4;
        //  private BlockingCollection<NetInfo> _sendQueue;

        private TPSQueue<NetInfo> _sendQueue;
        // private BlockingCollection<NetInfo> failureQueue;

        public void init(ICustomFixStrategy strategy, bool isTestMode)
        {
            //   isTestMode = true;
            //globexIFLogger = ZDLoggerFactory.getSynWriteLogger("StockAdapterHKEX.log");

            globexIFLogger = LogManager.GetLogger("StockAdapterHKEX");
            //globexIFLogger.setLogLevel(ZDLogger.LVL_TRACE);
            //CodeTransfer_TT.errLogger = globexIFLogger;
            //_receiveMsgsLog = ZDLoggerFactory.getSynWriteLogger("ReceiveMsgsLog.log");

            _receiveMsgsLog = LogManager.GetLogger("ReceiveMsgsLog");
            this.strategy = strategy;
            this.isTestMode = isTestMode;
            cfgInstance = CfgManager.getInstance("StockAdapterHKEX.exe");



            sessionID = cfgInstance.SessionAndPsw.Split(',')[0];

            //  sendQueue = new BlockingCollection<NetInfo>();
            //_sendQueue = new TPSQueue<NetInfo>(3);
            //_sendQueue.Cunsumer(p =>
            //{
            //    sendMsg(p);
            //});
            //   failureQueue = new BlockingCollection<NetInfo>();


            //CodeTransfer_HKEX.init(cfgInstance.SecurityDefFile);

            if (strategy == null)
                strategy = new HKEXFixStrategy();


            if (!isTestMode)
            {
                // FIX app settings and related
                QuickFix.SessionSettings settings = new QuickFix.SessionSettings(this.ConfigFile);
                strategy.SessionSettings = settings;

                // FIX application setup
                QuickFix.IMessageStoreFactory storeFactory = new QuickFix.FileStoreFactory(settings);
                QuickFix.ILogFactory logFactory = new QuickFix.FileLogFactory(settings);
                tradeApp = new TradeApp(settings, strategy);

                QuickFix.IInitiator initiator = new QuickFix.Transport.SocketInitiator(tradeApp, storeFactory, settings, logFactory);
                tradeApp.Initiator = initiator;


                EventHandler<ExectutionEventArgs> execReportHandler = new EventHandler<ExectutionEventArgs>(onExecReportEvent);
                tradeApp.ExecutionEvent += execReportHandler;
                tradeApp.OrderCacnelRejectEvent += OrderCacnelRejectHandler;
                tradeApp.BusinessMessageRejectEvent += businessMessageRejectHandler;
                tradeApp.UserResponseEvent += UserResponseHandler;

                EventHandler<EventArgs> logonEventHandler = new EventHandler<EventArgs>(onLogonEvent);
                tradeApp.LogonEvent += logonEventHandler;
                globexIFLogger.WriteLog("logonEventHandler");
                //try
                //{
                //     GTCOrderMgr.loadGTCOrder(cfgInstance.GTCOrderFile, xReference, downReference, globexIFLogger);
                //}
                //catch (Exception ex)
                //{
                //    globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                //}

                //NonGTCOrderMgr
                try
                {
                    NonGTCOrderMgr.loadNonGTCOrder(cfgInstance.DayOrderFile, xReference, downReference, globexIFLogger);
                }
                catch (Exception ex)
                {
                    globexIFLogger.WriteLog(ex.ToString());
                }
            }
            else
            {
                if (_sendQueue == null)
                {
                    _sendQueue = new TPSQueue<NetInfo>(messageThrottle);
                    _sendQueue.Cunsumer(p =>
                    {
                        //  globexIFLogger.log(ZDLogger.LVL_ERROR, "== dequeue, systemCode=" + p.systemCode);
                        globexIFLogger.WriteLog($"Dequeue:{p.MyToString()}");
                        SendMsg(p);
                    });
                }
            }

            ClOrderIDGen.setXRef(xReference);
            UnexpectedExceptionHandler.globexCommunication = this;


            //Common_comboBox_OrderType_StockHK_1=限价盘[PL：Price Limit Order]
            //Common_comboBox_OrderType_StockHK_2=市价盘
            //Common_comboBox_OrderType_StockHK_5=竞价限价盘[AL: Auction limit order]
            //Common_comboBox_OrderType_StockHK_6=竞价盘[AO: Auction Order(Sell only)]
            //Common_comboBox_OrderType_StockHK_7=增强限价盘[EL: Enhace limit order]
            //Common_comboBox_OrderType_StockHK_8=特别限价盘[SL: Special limit ordre]
            OrderTypeIdx = new Dictionary<string, int>();
            OrderTypeIdx.Add("5", 0);
            OrderTypeIdx.Add("6", 0);//市价竞价盘
            OrderTypeIdx.Add("7", 1);


            //*
            timeList = new List<TimeSpan>();
            timeList.Add(new TimeSpan(9, 0, 1).Add(offsetTS));
            timeList.Add(new TimeSpan(9, 15, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(9, 20, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(9, 30, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(12, 0, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(12, 30, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(13, 0, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(16, 0, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(16, 1, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(16, 6, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(16, 16, 0).Add(offsetTS));


            try
            {
                EventTriggerV2 eventTrigger = new EventTriggerV2();

                AuthCommon.EventDetail ed = new AuthCommon.EventDetail();
                ed.eventID = 50;
                ed.timeSpan = new TimeSpan(9, 0, 1).Add(offsetTS);
                ed.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed);

                AuthCommon.EventDetail ed2 = new AuthCommon.EventDetail();
                ed2.eventID = 51;
                ed2.timeSpan = new TimeSpan(9, 30, 0).Add(offsetTS);
                ed2.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed2);

                AuthCommon.EventDetail ed3 = new AuthCommon.EventDetail();
                ed3.eventID = 52;
                ed3.timeSpan = new TimeSpan(13, 0, 0).Add(offsetTS);
                ed3.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed3);

                AuthCommon.EventDetail ed4 = new AuthCommon.EventDetail();
                ed4.eventID = 53;
                ed4.timeSpan = new TimeSpan(16, 1, 0).Add(offsetTS);
                ed4.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed4);

                //modify
                AuthCommon.EventDetail ed5 = new AuthCommon.EventDetail();
                ed5.eventID = 54;
                ed5.timeSpan = new TimeSpan(9, 0, 1).Add(offsetTS);
                ed5.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed5);

                AuthCommon.EventDetail ed6 = new AuthCommon.EventDetail();
                ed6.eventID = 55;
                ed6.timeSpan = new TimeSpan(9, 30, 1).Add(offsetTS);
                ed6.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed6);

                AuthCommon.EventDetail ed7 = new AuthCommon.EventDetail();
                ed7.eventID = 56;
                ed7.timeSpan = new TimeSpan(13, 0, 1).Add(offsetTS);
                ed7.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed7);

                AuthCommon.EventDetail ed8 = new AuthCommon.EventDetail();
                ed8.eventID = 57;
                ed8.timeSpan = new TimeSpan(16, 1, 1).Add(offsetTS);
                ed8.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed8);

                //cancel
                AuthCommon.EventDetail ed9 = new AuthCommon.EventDetail();
                ed9.eventID = 58;
                ed9.timeSpan = new TimeSpan(9, 30, 2).Add(offsetTS);
                ed9.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed9);

                AuthCommon.EventDetail ed10 = new AuthCommon.EventDetail();
                ed10.eventID = 59;
                ed10.timeSpan = new TimeSpan(12, 30, 0).Add(offsetTS);
                ed10.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed10);

                AuthCommon.EventDetail ed11 = new AuthCommon.EventDetail();
                ed11.eventID = 60;
                ed11.timeSpan = new TimeSpan(16, 1, 2).Add(offsetTS);
                ed11.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed11);


                //定时清理xReference和downReference
                AuthCommon.EventDetail ed20 = new AuthCommon.EventDetail();
                ed20.eventID = 100;
                ed20.timeSpan = new TimeSpan(17, 00, 0).Add(offsetTS);
                ed20.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed20);

                eventTrigger.ready();
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
            //*/
        }

        private int getCurrentTimeDurIdx()
        {
            TimeSpan dtNow = DateTime.Now.TimeOfDay;

            for (int i = 0; i < timeList.Count; i++)
            {
                if (dtNow.Ticks <= timeList[i].Ticks)
                    return i;
            }

            return -1;
        }

        public void timerEventHandler(int eventID)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                if (dtNow.DayOfWeek == DayOfWeek.Saturday || dtNow.DayOfWeek == DayOfWeek.Sunday)
                {
                    return;
                }

                globexIFLogger.WriteLog("Triggered event: " + eventID);

                if (eventID >= 50 && eventID < 54)
                {
                    lock (pendingNewOrdersDict)
                    {
                        foreach (string key in pendingNewOrdersDict.Keys)
                        {
                            NetInfo netInfo;
                            if (pendingNewOrdersDict.TryRemove(key, out netInfo))
                            {
                                //OrderInfo orderinfo = new OrderInfo();
                                //orderinfo.MyReadString(netInfo.infoT);

                                //PlaceOrder(netInfo, orderinfo);

                                //add by xiang at 20170911 -begin
                                PreparingEnqueue(netInfo);
                                globexIFLogger.WriteLog("== add send queue timer new order, systemCode=" + netInfo.systemCode);
                                //add by xiang at 20170911 -end
                            }
                        }
                    }
                }
                else if (eventID >= 54 && eventID < 58)
                {
                    lock (pendingModifyOrdersDict)
                    {
                        foreach (string key in pendingModifyOrdersDict.Keys)
                        {
                            NetInfo netInfo;


                            if (pendingModifyOrdersDict.TryRemove(key, out netInfo))
                            {
                                ModifyInfo modifyInfo = new ModifyInfo();
                                modifyInfo.MyReadString(netInfo.infoT);

                                long clOrdID = Convert.ToInt64(modifyInfo.orderNo);
                                RefObj refObj;
                                bool ret = xReference.TryGetValue(clOrdID, out refObj);
                                if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_CANCELREPLACE)
                                {
                                    //cancelReplaceOrderWork(netInfo);

                                    //add by xiang at 20170911 -begin
                                    PreparingEnqueue(netInfo);
                                    globexIFLogger.WriteLog("== add send queue timer replace order, systemCode=" + netInfo.systemCode);
                                    //add by xiang at 20170911 -end
                                }
                            }
                        }
                    }
                }
                else if (eventID >= 58 && eventID < 61)
                {
                    lock (pendingCancelOrdersDict)
                    {
                        foreach (string key in pendingCancelOrdersDict.Keys)
                        {
                            NetInfo netInfo;

                            if (pendingCancelOrdersDict.TryRemove(key, out netInfo))
                            {
                                CancelInfo cancelInfo = new CancelInfo();
                                cancelInfo.MyReadString(netInfo.infoT);

                                long clOrdID = Convert.ToInt64(cancelInfo.orderNo);
                                RefObj refObj;
                                bool ret = xReference.TryGetValue(clOrdID, out refObj);
                                if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_REPLACE)
                                {
                                    //cancelOrderWork(netInfo);
                                    //add by xiang at 20170911 -begin
                                    PreparingEnqueue(netInfo);
                                    globexIFLogger.WriteLog("== add send queue timer cancel order, systemCode=" + netInfo.systemCode);
                                    //add by xiang at 20170911 -end
                                }
                            }
                        }
                    }
                }
                else if (eventID == 100)
                {
                    //定时清理xReference和downReference
                    lock (xReference)
                    {
                        xReference.Clear();
                    }
                    lock (downReference)
                    {
                        downReference.Clear();
                    }
                    lock (specialReference)
                    {
                        specialReference.Clear();
                    }
                    lock (_clientNoDic)
                    {
                        _clientNoDic.Clear();
                    }

                }
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
        }

        private void PreparingEnqueue(NetInfo netInfo)
        {
            long clOrdID = 0;
            if (netInfo.code == Command.OrderStockHK) //== CommandCode.ORDER
            {
                globexIFLogger.WriteLog($"Enqueue:{netInfo.MyToString()}");
                _sendQueue.Producer(netInfo);
                return;
            }
            else if (netInfo.code == Command.ModifyStockHK) //CommandCode.MODIFY
            {
                ModifyInfo info = new ModifyInfo();
                info.MyReadString(netInfo.infoT);
                clOrdID = Convert.ToInt64(info.orderNo);
            }
            else if (netInfo.code == Command.CancelStockHK) //CommandCode.CANCEL
            {
                CancelInfo info = new CancelInfo();
                info.MyReadString(netInfo.infoT);
                clOrdID = Convert.ToInt64(info.orderNo);
            }

            if (IsNotPending(clOrdID))
            {
                SetPending(clOrdID, netInfo);
                globexIFLogger.WriteLog($"Enqueue:{netInfo.MyToString()}");
                _sendQueue.Producer(netInfo);
            }
            else
            {
                string errorMessage = "The  order is  pending already.";
                if (netInfo.code == Command.ModifyStockHK) //CommandCode.MODIFY)
                {
                    OrderCancelReplaceException(netInfo, errorMessage);
                }
                else if (netInfo.code == Command.CancelStockHK) //CommandCode.CANCEL)
                {
                    CancelOrderException(netInfo, errorMessage);
                }

            }
        }

        /// <summary>
        /// 同一个订单必须等待交易所返回才能进行后续操作
        /// </summary>
        /// <param name="clOrdID">tag11</param>
        /// <returns></returns>
        private bool IsNotPending(long clOrdID)
        {
            return !ClOrdIdIsPending.TryGetValue(clOrdID, out _);
        }

        /// <summary>
        /// 订单进入TPS队列设置订单状态成pending
        /// </summary>
        /// <param name="clOrdID"></param>
        /// <param name="netInfo"></param>
        private void SetPending(long clOrdID, NetInfo netInfo)
        {
            ClOrdIdIsPending.TryAdd(clOrdID, true);
        }

        /// <summary>
        /// 同一个订单必须等待交易所返回才能进行后续操作
        /// </summary>
        /// <param name="clOrdID">tag11</param>
        /// <returns></returns>
        private void UnPending(long clOrdID)
        {
            ClOrdIdIsPending.TryRemove(clOrdID, out _);
        }



        private void OrderException(NetInfo obj, string errorMsg)
        {
            OrderInfo info = new OrderInfo();
            info.MyReadString(obj.infoT);
            obj.errorMsg = errorMsg;
            obj.exchangeCode = info.exchangeCode;
            obj.accountNo = info.accountNo;
            obj.errorCode = ErrorCode.ERR_ORDER_0000;
            obj.code = Command.OrderStockHK;
            TradeServerFacade.SendString(obj);
            globexIFLogger.WriteLog(errorMsg);
        }

        private void CancelOrderException(NetInfo obj, string errorMsg)
        {
            CancelInfo info = new CancelInfo();
            info.MyReadString(obj.infoT);
            CancelResponseInfo cinfo = new CancelResponseInfo();
            cinfo.orderNo = info.orderNo;
            obj.errorMsg = errorMsg;
            obj.infoT = cinfo.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            obj.errorCode = ErrorCode.ERR_ORDER_0014;
            obj.code = Command.CancelStockHK;

            TradeServerFacade.SendString(obj);
            globexIFLogger.WriteLog(errorMsg);
        }

        private void OrderCancelReplaceException(NetInfo obj, string errorMsg)
        {
            ModifyInfo info = new ModifyInfo();
            info.MyReadString(obj.infoT);
            OrderResponseInfo minfo = new OrderResponseInfo();
            obj.errorMsg = errorMsg;
            minfo.orderNo = info.orderNo;
            obj.infoT = minfo.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.accountNo = info.accountNo;
            obj.errorCode = ErrorCode.ERR_ORDER_0016;
            obj.code = Command.ModifyStockHK;

            TradeServerFacade.SendString(obj);
            globexIFLogger.WriteLog(errorMsg);
        }

        public void connectGlobex()
        {
            //try
            //{
            if (!isTestMode)
                tradeApp.Start();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
        }

        public void disconnectGlobex()
        {
            //无法100%做到内容不丢失，还是看log
            _sendQueue?.WaitForConsumerCompleted();
            //try
            //{
            if (!isTestMode)
                tradeApp.Stop();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

            //程序退出,给queue加标记
            //如果在交易时间关闭通讯，此时正好有客户单子到通讯 但是还没发出去，可能会在客户端看到已请求的状态
            //  _sendQueue.CompleteAdding();

            //failureQueue.CompleteAdding();
        }

        public void shutdown()
        {
            StockAdapterHKEX.CfgManager cfgInstance = StockAdapterHKEX.CfgManager.getInstance("StockAdapterHKEX.exe");

            try
            {
                NonGTCOrderMgr.persistNonGTCOrder();
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }

            try
            {
                cfgInstance.ClOrderID = ClOrderIDGen.getNextClOrderID().ToString();
                cfgInstance.save();
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
        }


        public void onLogonEvent(object sender, EventArgs e)
        {
            //登陆成功后请求throttle
            throttleEntitlement();

            ////启动控制一秒时间下单量的线程，防止超出throttle.
            //  Thread throttleThread = new Thread(new ThreadStart(throttleSendMsg));
            //throttleThread.IsBackground = true;
            //throttleThread.Start();


        }

        public void persistDayRefObj()
        {
            try
            {
                NonGTCOrderMgr.persistNonGTCOrder();
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
        }

        public void OrderCacnelRejectHandler(object sender, OrderCancelRejectEventArgs e)
        {
            try
            {
                QuickFix.FIX50.OrderCancelReject cancelRejMsg = e.CancelRejMsg;
                NetInfo netInfo = replyCancelReject(cancelRejMsg);
                //if(netInfo != null)
                //    AuditTrailMgr.addMsg(cancelRejMsg, netInfo);
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
        }

        public void businessMessageRejectHandler(object sender, BusinessMessageRejectEventArgs e)
        {
            try
            {
                QuickFix.FIX50.BusinessMessageReject businessRejMsg = e.BusinessRejMsg;
                NetInfo netInfo = replyBusinessMessageReject(businessRejMsg);
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
        }

        public void UserResponseHandler(object sender, UserResponseEventArgs e)
        {
            QuickFix.FIX50.UserResponse userResp = e.UserResponseMsg;

            QuickFix.FIX50.UserResponse.NoThrottlesGroup noThrottlesGroup = new QuickFix.FIX50.UserResponse.NoThrottlesGroup();
            int noThrottles = userResp.NoThrottles.getValue();

            userResp.GetGroup(noThrottles, noThrottlesGroup);

            messageThrottle = noThrottlesGroup.ThrottleNoMsgs.getValue();
            globexIFLogger.WriteLog("-------messageThrottle------- is " + messageThrottle);

            if (_sendQueue == null)
            {
                _sendQueue = new TPSQueue<NetInfo>(messageThrottle);
                _sendQueue.Cunsumer(p =>
                {
                    //  globexIFLogger.log(ZDLogger.LVL_ERROR, "== dequeue, systemCode=" + p.systemCode);
                    globexIFLogger.WriteLog($"Dequeue:{p.MyToString()}");
                    SendMsg(p);
                });
            }

        }

        public void onExecReportEvent(object sender, ExectutionEventArgs e)
        {
            try
            {
                QuickFix.FIX50.ExecutionReport execReport = e.ExecReportObject;

                //char ordStatus = execReport.OrdStatus.getValue();
                char execType = execReport.ExecType.getValue();

                NetInfo netInfo = null;
                switch (execType)
                {
                    case ExecType.NEW:
                        netInfo = replyOrderCreation(execReport);
                        break;

                    case ExecType.TRADE:
                        netInfo = replyFill(execReport);
                        break;

                    case ExecType.CANCELED:
                        netInfo = replyCancelled(execReport);
                        break;

                    case ExecType.REJECTED:
                        netInfo = replyRejected(execReport);
                        break;

                    case ExecType.PENDING_CANCEL:
                    case ExecType.PENDING_REPLACE:
                        doPendingCancel(execReport);
                        break;

                    case ExecType.REPLACED:
                        netInfo = replyReplaced(execReport);
                        break;

                    case ExecType.EXPIRED:
                        //netInfo = doExpired(execReport);
                        netInfo = replyCancelled(execReport);
                        break;

                        //case GlobexExt.ORD_STATUS_TRADE_CANCELLATION:
                        //    break;
                }

                //if (netInfo != null)
                //AuditTrailMgr.addMsg(execReport, netInfo);
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
        }

        public NetInfo doPendingCancel(QuickFix.FIX50.ExecutionReport execReport)
        {
            RefObj refObj;
            bool ret = downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            if (ret)
            {
                refObj.orderStatus = execReport.OrdStatus.getValue();
            }

            return null;
        }

        private string removeIlleagalChar(string msg)
        {
            string result = msg;
            if (msg.IndexOf('@') > -1)
                result = result.Replace('@', ' ');

            if (result.IndexOf('&') > -1)
                result = result.Replace('&', ' ');

            return result;
        }

        public NetInfo replyCancelReject(QuickFix.FIX50.OrderCancelReject cancelRejMsg)
        {
            //globexIFLogger.log(ZDLogger.LVL_CRITCAL, "now at replyCancelReject()");

            NetInfo netInfo = new NetInfo();

            if (cancelRejMsg.IsSetText())
                //netInfo.errorMsg = "CxlRejectReason(Tag58):" + removeIlleagalChar(cancelRejMsg.GetField(Tags.Text));
                netInfo.errorMsg = "CxlRejectReason(Tag1328):" + removeIlleagalChar(cancelRejMsg.GetField(Tags.RejectText));
            else
                netInfo.errorMsg = "";

            long temp = 0;
            specialReference.TryRemove(cancelRejMsg.OrigClOrdID.getValue(), out temp);

            //系统号
            RefObj refObj;
            //bool ret = downReference.TryGetValue(cancelRejMsg.OrderID.getValue(), out refObj);
            bool ret = xReference.TryGetValue(temp, out refObj);

            if (!ret)
            {
                globexIFLogger.WriteLog("replyCancelReject() -> not find refOBJ for orderID:" + cancelRejMsg.OrderID.getValue());
                globexIFLogger.WriteLog(cancelRejMsg.ToString());
                return null;
            }
            else
            {
                //在改单时会将downReference中的此OrderID remove掉。如果失败了就在加回去
                string orderID = cancelRejMsg.OrderID.getValue();
                if (!downReference.ContainsKey(orderID))
                {
                    downReference.TryAdd(orderID, refObj);
                    globexIFLogger.WriteLog("readd order id replyCancelReject " + orderID);
                }

                UnPending(long.Parse(refObj.clOrderID));

            }

            refObj.addGlobexRes(cancelRejMsg);

            char cancelRejResponseTo = cancelRejMsg.GetChar(Tags.CxlRejResponseTo);

            //globexIFLogger.log(ZDLogger.LVL_CRITCAL, "cancelRejResponseTo:" + cancelRejResponseTo);

            // 2 = Order Cancel/Replace Request
            if (cancelRejResponseTo == '2')
            {
                //globexIFLogger.log(ZDLogger.LVL_CRITCAL, "cancelRejResponseTo @ condiont 2");
                OrderResponseInfo minfo = new OrderResponseInfo();

                minfo.orderNo = refObj.clOrderID;
                minfo.accountNo = refObj.strArray[0];
                minfo.systemNo = refObj.strArray[1];

                netInfo.infoT = minfo.MyToString();
                netInfo.accountNo = refObj.strArray[0];
                netInfo.systemCode = refObj.strArray[1];
                netInfo.errorCode = ErrorCode.ERR_ORDER_0016;
                netInfo.code = Command.ModifyStockHK; //CommandCode.MODIFY;
                netInfo.clientNo = refObj.strArray[2];
                netInfo.exchangeCode = refObj.strArray[5];// OrderRouterID
            }
            // 1 = Order Cancel Request
            else if (cancelRejResponseTo == '1')
            {
                //globexIFLogger.log(ZDLogger.LVL_CRITCAL, "cancelRejResponseTo @ condiont 1");

                CancelResponseInfo cinfo = new CancelResponseInfo();
                cinfo.orderNo = refObj.clOrderID;

                netInfo.infoT = cinfo.MyToString();
                netInfo.accountNo = refObj.strArray[0];
                netInfo.systemCode = refObj.strArray[1];
                netInfo.errorCode = ErrorCode.ERR_ORDER_0014;
                netInfo.code = Command.CancelStockHK; //CommandCode.CANCELCAST;
                netInfo.clientNo = refObj.strArray[2];
                netInfo.exchangeCode = refObj.strArray[5];// OrderRouterID
            }

            //fancky add 20190508
            if (cancelRejMsg.IsSetRejectText())
                //netInfo.errorMsg = "CxlRejectReason(Tag58):" + removeIlleagalChar(cancelRejMsg.GetField(Tags.Text));
                netInfo.errorMsg = "CxlRejectReason(Tag1328):" + cancelRejMsg.GetField(Tags.RejectText);
            else
                netInfo.errorMsg = "";


            TradeServerFacade.SendString(netInfo);
            //globexIFLogger.log(ZDLogger.LVL_CRITCAL, "data sent");

            return netInfo;
        }

        public NetInfo replyBusinessMessageReject(QuickFix.FIX50.BusinessMessageReject execReport)
        {
            NetInfo obj = null;

            //if (execReport.RefMsgType.getValue() == "D")
            //{
            RefObj refObj;
            long clOrdID = Convert.ToInt64(execReport.BusinessRejectRefID.getValue());
            bool ret = xReference.TryGetValue(clOrdID, out refObj);

            if (ret)
            {
                //obj = new NetInfo();

                /*In case the BusinessRejectReason (380) = 8
                (Throttle Limit Exceeded), the Text (58) field will
                indicate the remaining throttle interval time in
                milliseconds.*/

                obj = refObj.lastSendInfo;

                if (execReport.BusinessRejectReason.getValue() == BusinessRejectReason.THROTTLE_LIMIT_EXCEEDED)
                {
                    //如果是发的太快被拒绝要remove保存的最后一条的fromClient，但是如果是下单,速度快被拒绝就不移除
                    //if (refObj.fromClient.Count > 0)
                    //{
                    //    QuickFix.Message msg = refObj.fromClient[refObj.fromClient.Count - 1];
                    //    if (msg.GetType() != typeof(QuickFix.FIX50.NewOrderSingle))
                    //    {
                    //        //refObj.fromClient.RemoveAt(refObj.fromClient.Count - 1);
                    //        refObj.addGlobexRes(new QuickFix.FIX50.OrderCancelReject());
                    //    }
                    //}


                    //obj.errorMsg = "BusinessRejectReason(Tag380):" + "Throttle limit exceeded";
                    //如果发送超过限制就加入失败的队列。到下一个时间窗口重新发送
                    //failureQueue.TryAdd(refObj.lastSendInfo);

                    if (execReport.RefMsgType.getValue() != "D")
                    {
                        globexIFLogger.WriteLog("replyBusinessMessageReject " + clOrdID + ",fromClient= " + refObj.fromClient.Count);
                        refObj.addGlobexRes(new QuickFix.FIX50.OrderCancelReject());
                        globexIFLogger.WriteLog("replyBusinessMessageReject " + clOrdID + ",fromClient= " + refObj.fromClient.Count);
                    }

                    //fancky  注释
                    //如果发送超过限制就加入失败的队列。到下一个时间窗口重新发送
                    // failureQueue.TryAdd(refObj.lastSendInfo);

                    //xReference.TryRemove(clOrdID, out refObj);
                    //fancky  注释
                    //重新发送的单子不返回状态给交易
                    // return null;
                    obj.errorMsg = "BusinessRejectReason:Exceeding max MPS";


                }
                else
                    obj.errorMsg = "BusinessRejectReason(Tag380):" + removeIlleagalChar(execReport.Text.getValue());

                string responseInfo = string.Empty;
                switch (obj.code)
                {
                    case "OrdeStHK":
                        OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
                        orderResponseInfo.orderNo = refObj.orderID;
                        responseInfo = orderResponseInfo.MyToString();

                        obj.errorCode = ErrorCode.ERR_ORDER_0004;
                        xReference.TryRemove(long.Parse(refObj.clOrderID), out refObj);
                        break;
                    case "CancStHK":
                        CancelResponseInfo cinfo = new CancelResponseInfo();
                        cinfo.orderNo = refObj.orderID;
                        responseInfo = cinfo.MyToString();

                        obj.errorCode = ErrorCode.ERR_ORDER_0014;
                        break;
                    case "ModiStHK":
                        ModifyResponseInfo modifyResponseInfo = new ModifyResponseInfo();
                        modifyResponseInfo.orderNo = refObj.orderID;
                        responseInfo = modifyResponseInfo.MyToString();

                        obj.errorCode = ErrorCode.ERR_ORDER_0016;
                        break;

                }


                UnPending(long.Parse(refObj.clOrderID));


                obj.infoT = responseInfo;
                //obj.exchangeCode = refObj.strArray[5];// OrderRouterID
                //obj.errorCode = ErrorCode.ERR_ORDER_0004;
                // obj.code = Command.OrderStockHK; // CommandCode.ORDER;
                //obj.accountNo = refObj.strArray[0];
                //obj.systemCode = refObj.strArray[1];
                //obj.todayCanUse = refObj.strArray[2]; //execReport.Header.GetField(Tags.SenderSubID);
                //obj.clientNo = refObj.strArray[2];




                string reqNo;
                if (requestNoDict.TryRemove(refObj.clOrderID, out reqNo))
                    obj.RequestNo = reqNo;

                TradeServerFacade.SendString(obj);

                //xReference.TryRemove(long.Parse(refObj.clOrderID), out refObj);
            }
            else
            {
                globexIFLogger.WriteLog("replyBusinessMessageReject(): can not find RefObj, clOrdID= " + clOrdID);
            }
            //}

            return obj;
        }

        public NetInfo replyOrderCreation(QuickFix.FIX50.ExecutionReport execReport)
        {
            OrderResponseInfo info = new OrderResponseInfo();

            info.orderNo = execReport.ClOrdID.getValue();

            string OrderID = execReport.OrderID.getValue();
            //info.origOrderNo = info.orderNo;
            info.origOrderNo = OrderID;

            info.orderMethod = "1";
            info.htsType = "";

            string strSecurityID = execReport.SecurityID.getValue();
            HKCodeBean codeBean = CodeTransfer_HKEX.getZDCodeInfoByUpperCode(strSecurityID, "");

            if (codeBean == null)
                info.exchangeCode = "HKEX";
            else
                info.exchangeCode = codeBean.zdExchg;

            if (execReport.Side.getValue() == Side.BUY)
                info.buySale = "1";
            else
                info.buySale = "2";

            info.tradeType = "1";

            char ordType = execReport.OrdType.getValue();


            if (ordType == OrdType.LIMIT)
            {
                info.orderPrice = execReport.Price.getValue().ToString(); // CodeTransfer_TT.toClientPrx(execReport.Price.getValue(), strSymbol);
            }


            char tif = execReport.TimeInForce.getValue();

            info.orderNumber = getShareVol(execReport.OrderQty.getValue());
            info.filledNumber = "0";
            info.acceptType = "1";
            DateTime transTime = Utils.toChinaLocalTime(execReport.TransactTime.getValue());
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");

            info.orderState = "2";

            //系统号
            RefObj refObj;
            long clOrdID = Convert.ToInt64(execReport.ClOrdID.getValue());
            bool ret = xReference.TryGetValue(clOrdID, out refObj);

            refObj.orderID = OrderID;
            refObj.addGlobexRes(execReport);

            downReference.TryAdd(OrderID, refObj);
            globexIFLogger.WriteLog("readd order id replyOrderCreation " + OrderID);

            info.priceType = refObj.strArray[6];
            info.validDate = refObj.strArray[7];

            info.code = refObj.strArray[4];
            info.accountNo = refObj.strArray[0];
            info.systemNo = refObj.strArray[1];
            NetInfo obj = new NetInfo();

            obj.infoT = info.MyToString();
            obj.exchangeCode = refObj.strArray[5];// OrderRouterID
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = Command.OrderStockHK; // CommandCode.ORDER;

            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            obj.todayCanUse = refObj.strArray[2]; // .Header.GetField(Tags.TargetSubID);
            obj.clientNo = refObj.strArray[2];
            obj.localSystemCode = refObj.strArray[3];

            string reqNo;
            if (requestNoDict.TryRemove(execReport.ClOrdID.getValue(), out reqNo))
                obj.RequestNo = reqNo;

            TradeServerFacade.SendString(obj);

            return obj;
        }

        public NetInfo replyFill(QuickFix.FIX50.ExecutionReport execReport)
        {
            try
            {

                FilledResponseInfo info = new FilledResponseInfo();

                if (execReport.IsSetExecType())
                {
                    char execTransType = execReport.ExecType.getValue();
                    if (execTransType == ExecTransType.CORRECT)
                    {
                        globexIFLogger.WriteLog("Discard fill modification(tag20=2):");
                        globexIFLogger.WriteLog(execReport.ToString());
                        return null;
                    }
                }


                string strSymbol = execReport.SecurityID.getValue();

                HKCodeBean codeBean = CodeTransfer_HKEX.getZDCodeInfoByUpperCode(strSymbol, "");
                if (codeBean == null)
                    info.exchangeCode = "HKEX";
                else
                    info.exchangeCode = codeBean.zdExchg;

                if (execReport.Side.getValue() == Side.BUY)
                    info.buySale = "1";
                else
                    info.buySale = "2";

                info.filledNo = execReport.ExecID.getValue();
                info.filledNumber = getShareVol(execReport.LastQty.getValue());
                info.filledPrice = execReport.LastPx.getValue().ToString(); // CodeTransfer_TT.toClientPrx(execReport.LastPx.getValue(), strSymbol);

                //获取成交时间。TransactTime没有精确到ms,先从SendingTime拿
                //暂时先不上
                /*DateTime transTime = DateTime.Now;
                if(execReport.Header.IsSetField(Tags.SendingTime))
                {
                   transTime = QuickFix.Fields.Converters.DateTimeConverter.ConvertToDateTime(execReport.Header.GetField(Tags.SendingTime));
                }
                else
                {
                    transTime = execReport.TransactTime.getValue();
                }

                transTime = transTime.ToLocalTime();

                info.filledTime = transTime.ToString("HH:mm:ss.fff");
                info.filledDate = transTime.ToString("yyyy-MM-dd");*/


                DateTime transTime = Utils.toChinaLocalTime(execReport.TransactTime.getValue());
                info.filledTime = transTime.ToString("HH:mm:ss");
                info.filledDate = transTime.ToString("yyyy-MM-dd");

                //fancky add 20190828
                //var group = execReport.GetGroup(1, Tags.NoPartyIDs);
                //info.FillBrokerList = group.GetString(Tags.PartyID);
                List<string> brokers = new List<string>();
                int repeatCnt = execReport.NoPartyIDs.getValue();
                for (int i = 1; i <= repeatCnt; i++)
                {
                    var g = execReport.GetGroup(i, Tags.NoPartyIDs);
                    if (g.GetString(Tags.PartyRole) == "17")
                    {
                        brokers.Add(g.GetString(Tags.PartyID));
                    }

                }
                info.FillBrokerList = string.Join(",", brokers);

                //系统号
                RefObj refObj;
                bool ret = downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
                if (!ret)
                {
                    globexIFLogger.WriteLog("replyFill downReference no order id " + execReport.OrderID.getValue());
                    return null;
                }

                refObj.cumFilled = execReport.CumQty.getValue();

                info.orderNo = refObj.clOrderID;
                info.accountNo = refObj.strArray[0];
                info.systemNo = refObj.strArray[1];
                info.code = refObj.strArray[4];

                NetInfo obj = new NetInfo();
                obj.infoT = info.MyToString();
                obj.exchangeCode = info.exchangeCode;
                obj.errorCode = ErrorCode.SUCCESS;
                obj.code = Command.FilledStockHK; //CommandCode.FILLEDCAST;
                obj.accountNo = info.accountNo;
                obj.todayCanUse = refObj.strArray[2];  //execReport.Header.GetField(Tags.SenderSubID);

                TradeServerFacade.SendString(obj);
                //globexIFLogger.log(ZDLogger.LVL_CRITCAL, obj.MyToString());
                //完全成交了，移除内存数据
                if (execReport.LeavesQty.getValue() == 0)
                {
                    xReference.TryRemove(long.Parse(info.orderNo), out refObj);
                    downReference.TryRemove(refObj.orderID, out refObj);

                    RemoveXRefrenceDownReferenceDada(refObj);
                    globexIFLogger.WriteLog("downReference tryRemove in replyFill " + refObj.orderID);
                }

                return obj;
            }
            catch (Exception e)
            {
                globexIFLogger.WriteLog(e.ToString());
                return null;
            }
        }

        public NetInfo replyCancelled(QuickFix.FIX50.ExecutionReport execReport)
        {

            CancelResponseInfo info = new CancelResponseInfo();
            string strSymbol = execReport.SecurityID.getValue();

            HKCodeBean codeBean = CodeTransfer_HKEX.getZDCodeInfoByUpperCode(strSymbol, "");
            if (codeBean == null)
                info.exchangeCode = "HKEX";
            else
                info.exchangeCode = codeBean.zdExchg;

            if (execReport.Side.getValue() == Side.BUY)
                info.buySale = "1";
            else
                info.buySale = "2";



            //add by xiang at 2017/06/10 start 
            //node: 被港交所拒绝的单子没有OrigClOrdID,只有ClOrdID
            //long temp = 0;
            /*if (execReport.IsSetOrigClOrdID())
            {
                specialReference.TryRemove(execReport.OrigClOrdID.getValue(), out temp);
            }
            else if (execReport.IsSetClOrdID())
            {
                specialReference.TryRemove(execReport.ClOrdID.getValue(), out temp);
            }
            //add by xiang at 2017/06/10 end

            RefObj refObj;
            //bool ret = downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            bool ret = xReference.TryGetValue(temp, out refObj);


            if (!ret && execReport.IsSetOrderID())
            {
                downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            }*/
            RefObj refObj;
            bool ret = downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            if (!ret)
            {
                globexIFLogger.WriteLog($"OrderID:{execReport.OrderID.getValue()},内存信息丢失无法返回给客户端。");
                return null;
            }
            info.orderNo = refObj.clOrderID;
            //系统号
            info.accountNo = refObj.strArray[0];
            info.systemNo = refObj.strArray[1];
            info.code = refObj.strArray[4];

            //info.priceType = QueryPriceType(ordType);
            info.priceType = refObj.strArray[6];

            if (execReport.IsSetPrice())
            {
                info.orderPrice = execReport.Price.getValue().ToString(); // CodeTransfer_TT.toClientPrx(execReport.Price.getValue(), strSymbol);
            }


            info.cancelNumber = getShareVol(execReport.LeavesQty.getValue());
            info.orderNumber = getShareVol(execReport.OrderQty.getValue());
            info.filledNumber = getShareVol(refObj.cumFilled);
            info.cancelNo = info.orderNo;

            DateTime transTime = Utils.toChinaLocalTime(execReport.TransactTime.getValue());
            info.cancelTime = transTime.ToString("HH:mm:ss");
            info.cancelDate = transTime.ToString("yyyy-MM-dd");

            info.errorCode = "6";

            NetInfo obj = new NetInfo();
            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = Command.CancelStockHK; // CommandCode.CANCELCAST;
            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            obj.todayCanUse = refObj.strArray[2]; //execReport.Header.GetField(Tags.TargetSubID);
            obj.clientNo = refObj.strArray[2];

            string reqNo;
            if (requestNoDict.TryRemove(execReport.ClOrdID.getValue(), out reqNo))
                obj.RequestNo = reqNo;

            TradeServerFacade.SendString(obj);

            //删除最新的订单号、客户端单号的单据
            xReference.TryRemove(long.Parse(info.orderNo), out refObj);
            downReference.TryRemove(refObj.orderID, out refObj);

            RemoveXRefrenceDownReferenceDada(refObj);

            globexIFLogger.WriteLog("downReference tryRemove in replyCancelled " + refObj.orderID);
            if (!string.IsNullOrEmpty(refObj.clOrderID))
            {
                UnPending(long.Parse(refObj.clOrderID));
            }
            return obj;
        }

        /// <summary>
        /// 删除因为改单可能造成缓存数据有多个:
        /// xReference有多个ClientID(tag11),
        /// downReference有多个OrderID(tag37)
        /// </summary>
        /// <param name="refObj"></param>
        private void RemoveXRefrenceDownReferenceDada(RefObj refObj)
        {
            try
            {
                //删除因为改单可能造成缓存数据有多个xReference、downReference有ClientID的
                //此操作应包含上面两句的移除操作。
                refObj.fromClient.ForEach(message =>
                {
                    xReference.TryRemove(long.Parse(message.GetField(11)), out _);
                });

                refObj.fromGlobex.ForEach(message =>
                {
                    downReference.TryRemove(message.GetField(37), out _);
                });
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
        }

        public NetInfo replyRejected(QuickFix.FIX50.ExecutionReport execReport)
        {
            //被拒绝了
            OrderResponseInfo info = new OrderResponseInfo();

            info.orderMethod = "1";
            info.htsType = "";
            string strSymbol = execReport.SecurityID.getValue();

            HKCodeBean codeBean = CodeTransfer_HKEX.getZDCodeInfoByUpperCode(strSymbol, "");
            if (codeBean == null)
                info.exchangeCode = "HKEX";
            else
                info.exchangeCode = codeBean.zdExchg;

            if (execReport.Side.getValue() == Side.BUY)
                info.buySale = "1";
            else
                info.buySale = "2";

            info.tradeType = "1";


            info.orderNumber = getShareVol(execReport.OrderQty.getValue());
            info.filledNumber = "0";
            info.acceptType = "1";
            DateTime transTime = Utils.toChinaLocalTime(DateTime.Now); // execReport.TransactTime.getValue();
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");

            //系统号
            RefObj refObj;
            bool ret = downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            // Case of order creation is not ACKed, e.g out of trading time
            if (!ret)
            {
                long clOrdID = Convert.ToInt64(execReport.ClOrdID.getValue());
                ret = xReference.TryGetValue(clOrdID, out refObj);
            }


            info.orderNo = refObj.clOrderID;
            info.accountNo = refObj.strArray[0];
            info.systemNo = refObj.strArray[1];
            info.code = refObj.strArray[4];

            NetInfo obj = new NetInfo();
            obj.errorMsg = "OrdRejectReason(Tag1328):" + removeIlleagalChar(execReport.GetField(Tags.RejectText));

            obj.infoT = info.MyToString();
            obj.exchangeCode = refObj.strArray[5];// OrderRouterID
            obj.errorCode = ErrorCode.ERR_ORDER_0004;
            obj.code = Command.OrderStockHK; // CommandCode.ORDER;
            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            obj.todayCanUse = refObj.strArray[2]; //execReport.Header.GetField(Tags.SenderSubID);
            obj.clientNo = refObj.strArray[2];


            string reqNo;
            if (execReport.IsSetClOrdID() && requestNoDict.TryRemove(execReport.ClOrdID.getValue(), out reqNo))
                obj.RequestNo = reqNo;

            TradeServerFacade.SendString(obj);

            xReference.TryRemove(long.Parse(info.orderNo), out refObj);
            if (!string.IsNullOrEmpty(refObj.clOrderID))
            {
                UnPending(long.Parse(refObj.clOrderID));
            }

            return obj;
        }

        /// <summary>
        /// 获最近一次下单、或者改单成功的ExecutionReport
        /// </summary>
        /// <param name="execReports"></param>
        /// <returns></returns>
        private ExecutionReport GetLastOrderAmendExecutionReport(List<QuickFix.FIX50.ExecutionReport> execReports)
        {
            ExecutionReport execReport = null;
            execReports.ForEach(p =>
            {
                var execType = p.GetField(Tags.ExecType);
                if (execType == "0" || execType == "5")
                {
                    execReport = p;
                }
            });
            return execReport;
        }

        public NetInfo replyReplaced(QuickFix.FIX50.ExecutionReport execReport)
        {
            OrderResponseInfo info = new OrderResponseInfo();
            string strSecurityID = execReport.SecurityID.getValue();
            HKCodeBean codeBean = CodeTransfer_HKEX.getZDCodeInfoByUpperCode(strSecurityID, "");
            if (codeBean == null)
                info.exchangeCode = "HKEX";
            else
                info.exchangeCode = codeBean.zdExchg;

            if (execReport.Side.getValue() == Side.BUY)
                info.buySale = "1";
            else
                info.buySale = "2";

            info.tradeType = "1";

            char ordType = execReport.OrdType.getValue();
            //info.priceType = QueryPriceType(ordType);

            if (ordType == OrdType.LIMIT)
            {
                info.orderPrice = execReport.Price.getValue().ToString(); // CodeTransfer_TT.toClientPrx(execReport.Price.getValue(), strSymbol);
            }

            char tif = execReport.TimeInForce.getValue();

            //info.modifyNumber = execReport.OrderQty.getValue().ToString();
            info.orderNumber = getShareVol(execReport.OrderQty.getValue());
            info.filledNumber = "0";

            DateTime transTime = Utils.toChinaLocalTime(execReport.TransactTime.getValue());
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");

            info.orderState = "2";

            long zdOrdID;
            specialReference.TryRemove(execReport.OrigClOrdID.getValue(), out zdOrdID);

            //系统号
            RefObj refObj;
            //bool ret = downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            bool ret = xReference.TryGetValue(zdOrdID, out refObj);

            //fancky add 20200902
            if (ret)
            {
                var executionReport = GetLastOrderAmendExecutionReport(refObj.fromGlobex);
                var lastOrderID = executionReport.GetField(Tags.OrderID);
                if (!string.IsNullOrEmpty(lastOrderID))
                {
                    downReference.TryRemove(lastOrderID, out _);
                    globexIFLogger.WriteLog("remove order id replyReplaced " + lastOrderID);
                }
            }


            //add by xiang at 20171011 -begin
            downReference.TryAdd(execReport.OrderID.getValue(), refObj);
            globexIFLogger.WriteLog("add new order id in replyReplaced " + execReport.OrderID.getValue());
            refObj.orderID = execReport.OrderID.getValue();
            //add by xiang at 20171011 -end

            //info.orderNo = refObj.clOrderID;
            //info.origOrderNo = info.orderNo;

            info.orderNo = execReport.ClOrdID.getValue();
            info.origOrderNo = refObj.clOrderID;

            refObj.addGlobexRes(execReport);

            info.priceType = refObj.strArray[6];
            info.validDate = refObj.strArray[7];

            info.accountNo = refObj.strArray[0];
            info.systemNo = refObj.strArray[1];
            info.code = refObj.strArray[4];

            NetInfo obj = new NetInfo();

            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = Command.ModifyStockHK; // CommandCode.MODIFY;
            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            obj.todayCanUse = refObj.strArray[2]; //execReport.Header.GetField(Tags.TargetSubID);
            obj.clientNo = refObj.strArray[2];

            string reqNo;
            if (requestNoDict.TryRemove(execReport.ClOrdID.getValue(), out reqNo))
                obj.RequestNo = reqNo;

            TradeServerFacade.SendString(obj);
            if (!string.IsNullOrEmpty(refObj.clOrderID))
            {
                UnPending(long.Parse(refObj.clOrderID));
            }
            return obj;
        }

        public NetInfo doExpired(QuickFix.FIX50.ExecutionReport execReport)
        {
            //系统号
            RefObj refObj;
            bool ret = downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            if (ret)
            {
                string orderNo = refObj.clOrderID;
                xReference.TryRemove(long.Parse(orderNo), out refObj);
                downReference.TryRemove(refObj.orderID, out refObj);
                globexIFLogger.WriteLog("downReference tryRemove in doExpired " + refObj.orderID);
            }

            return null;
        }

        private ConcurrentDictionary<string, string> requestNoDict = new ConcurrentDictionary<string, string>();

        private ConcurrentDictionary<string, NetInfo> pendingNewOrdersDict = new ConcurrentDictionary<string, NetInfo>();
        private void addToNewOrderPendingQueue(NetInfo obj)
        {
            lock (pendingNewOrdersDict)
            {
                pendingNewOrdersDict.TryAdd(obj.systemCode, obj);
                globexIFLogger.WriteLog("Queue new order, systemCode=" + obj.systemCode);
            }
        }


        //订单状态（1：已请求；2：已排队；3：部分成交；4：完全成交；5：已撤余单；6：已撤单；7：指令失败；8：待送出；9：待更改；10：待撤单）

        private const string ZD_PENDING_SEND_OUT = "8";
        private const string ZD_PENDING_MODIFY = "9";
        private const string ZD_PENDING_CANCEL = "A";
        private const string ZD_CANCELED = "6";

        /// <summary>
        /// 壳调用，之后订单入队（PreparingEnqueue(obj)），出队之后调用PlaceOrder(NetInfo obj, OrderInfo info)
        /// </summary>
        /// <param name="obj"></param>
        public void PlaceOrder(NetInfo obj)
        {
            OrderInfo info = new OrderInfo();
            info.MyReadString(obj.infoT);



            //Common_comboBox_OrderType_StockHK_1=限价盘[PL：Price Limit Order]
            //Common_comboBox_OrderType_StockHK_2=市价盘
            //Common_comboBox_OrderType_StockHK_5=竞价限价盘[AL: Auction limit order]
            //Common_comboBox_OrderType_StockHK_6=竞价盘[AO: Auction Order(Sell only)]
            //Common_comboBox_OrderType_StockHK_7=增强限价盘[EL: Enhace limit order]
            //Common_comboBox_OrderType_StockHK_8=特别限价盘[SL: Special limit ordre]

            //（16:01-16:06）9,竞价限价盘,竞价盘|下单,改单,撤单
            //（16:06-16:10）10,竞价限价盘,竞价盘|下单

            //8:30-9:15支持下竞价限价盘
            //9:15-16:00支持下增强限价盘

            //int orderTypeIdx = -1;
            if (OrderTypeIdx.TryGetValue(info.priceType, out int orderTypeIdx))
            {
                int timeDurIdx = getCurrentTimeDurIdx();
                if (timeDurIdx == -1) return;

                char flag = newOrderMatrix[timeDurIdx, orderTypeIdx];
                if (flag == 'P')
                {
                    addToNewOrderPendingQueue(obj);

                    string orderId = ClOrderIDGen.getNextClOrderID().ToString();
                    // Todo...
                    sendOrderResonseInfo_ForNewOrder(obj, orderId, ZD_PENDING_SEND_OUT);
                    obj.errorMsg = orderId;

                    requestNoDict.TryAdd(orderId, obj.RequestNo);
                }
                else if (flag == 'Y')
                {
                    //PlaceOrder(obj, info);
                    //add by xiang at 20170911 -begin
                    //_sendQueue.Producer(obj);
                    PreparingEnqueue(obj);
                    globexIFLogger.WriteLog("== add send queue new order, systemCode=" + obj.systemCode);
                    //add by xiang at 20170911 -end
                }
                else
                {
                    OrderException(obj, "Place order is not alowed at current time ,Please follow the order time table!");
                }
            }
            else
            {
                OrderException(obj, "Unsupported order type!");
            }



            // <summary>
            // 下单失败：不支持的下单类型
            // </summary>
            //public static string ERR_ORDER_0035 = "20035";

            //if (caseNum == 0)
            //{
            //    TradeServerFacade.writeLog(LogLevel.IMPORTANT, "Place order is not alowed at current time ,Please follow the order time table!");
            //    obj.errorCode = ErrorCode.ERR_ORDER_0035;
            //    obj.code = Command.OrderStockHK;
            //    obj.infoT = "";

            //    TradeServerFacade.SendString(obj);
            //}
        }

        /// <summary>
        /// 出队之后调用此真正下单到港交所
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="info"></param>
        public void PlaceOrder(NetInfo obj, OrderInfo info)
        {
            try
            {
                HKCodeBean codeBean = CodeTransfer_HKEX.getUpperCodeInfoByZDCode(info.code, info.exchangeCode);

                QuickFix.FIX50.NewOrderSingle newOrderSingle = new QuickFix.FIX50.NewOrderSingle();

                //obj.errorMsg is set to one new OrderId on PlaceOrder(NetInfo obj)
                long clOrdID = 0;
                string strClOrdId = null;
                if (string.IsNullOrEmpty(obj.errorMsg))
                {
                    clOrdID = ClOrderIDGen.getNextClOrderID();
                    strClOrdId = clOrdID.ToString();
                    newOrderSingle.ClOrdID = new ClOrdID(strClOrdId);
                }
                else
                {
                    clOrdID = long.Parse(obj.errorMsg);
                    strClOrdId = obj.errorMsg;
                    newOrderSingle.ClOrdID = new ClOrdID(obj.errorMsg);
                }

                requestNoDict.TryAdd(strClOrdId, obj.RequestNo);

                // ******* Header fields ***************
                // SenderSubID(Tag50 ID)
                //newOrderSingle.SetField(new SenderSubID(obj.todayCanUse));


                // Tag109 ClientID
                //...
                //newOrderSingle.ClientID = new ClientID(obj.todayCanUse);

                // Tag1
                //newOrderSingle.Account = new Account(obj.accountNo);


                // Tag21 HandlInst
                //newOrderSingle.HandlInst = handInst;

                // Tag48
                newOrderSingle.SecurityID = new SecurityID(codeBean.upperProduct);
                //Tag 22
                newOrderSingle.SecurityIDSource = securityIDSource;
                //Tag 207
                newOrderSingle.SecurityExchange = securityExchange;

                // Tag54
                newOrderSingle.Side = QuerySide(info.buySale);

                // Tag60
                newOrderSingle.TransactTime = new TransactTime(DateTime.UtcNow);

                // Tag38
                newOrderSingle.OrderQty = new OrderQty(decimal.Parse(info.orderNumber));

                // Tag40
                newOrderSingle.OrdType = QueryOrdType(info.priceType);



                char charOrdType = newOrderSingle.OrdType.getValue();

                if (newOrderSingle.IsSetOrdType() && newOrderSingle.OrdType.getValue() == OrdType.LIMIT)
                {
                    // Tag44
                    if (info.priceType == "6") //Auction order
                    {
                        //newOrderSingle.Price = new Price(-1);
                    }
                    else
                    {
                        decimal prx = decimal.Parse(info.orderPrice);
                        newOrderSingle.Price = new Price(prx);

                    }
                }

                var execInst = "c";
                //add by xiang at 20181102 start
                //tag:1093:碎股标识，区分碎股,包含此tag1093是碎股
                if (info.htsType == "1")
                {
                    newOrderSingle.LotType = new LotType('1');
                    execInst = "x";
                }

                //Tag18 忽略价格校验:普通的c,碎股:x
                newOrderSingle.ExecInst = new ExecInst(execInst);


                //add by xiang at 20181102 end

                //int mod = Convert.ToInt32(info.orderNumber) % codeBean.stockSize;
                //if (newOrderSingle.IsSetOrdType() && newOrderSingle.OrdType.getValue() == OrdType.LIMIT)
                //{
                //    newOrderSingle.MaxPriceLevels = new MaxPriceLevels(1);
                //}

                // Tag18: ExecInst
                //newOrderSingle.ExecInst = new ExecInst("c x");


                //Code for OCG simulator start

                //string[] p = info.userType.Split('|');
                //if (p[0] == "1")
                //    newOrderSingle.MaxPriceLevels = new MaxPriceLevels(1);

                //if (p[1] != "")
                //    newOrderSingle.ExecInst = new ExecInst(p[1]);

                //sessionID = p[2];

                //if (p[3] == "1")
                //    newOrderSingle.LotType = new LotType('1');

                //Code for OCG simulator end


                // Tag59
                //string timeInForce = info.validDate;
                newOrderSingle.TimeInForce = QueryTimeInForce(info.validDate);
                //9:00-9:15;竞价时间段内设置 9；
                if (cfgInstance.IsPreOpenTime)
                {
                    newOrderSingle.TimeInForce = new TimeInForce('9');
                }
                //newOrderSingle.TimeInForce = QueryTimeInForce(info.validDate);

                newOrderSingle.NoPartyIDs = new NoPartyIDs(1);

                QuickFix.FIX50.NewOrderSingle.NoPartyIDsGroup noPartyIDsGroup = new QuickFix.FIX50.NewOrderSingle.NoPartyIDsGroup();
                noPartyIDsGroup.PartyID = new PartyID(sessionID);
                noPartyIDsGroup.PartyIDSource = new PartyIDSource('D');
                noPartyIDsGroup.PartyRole = new PartyRole(1);
                newOrderSingle.AddGroup(noPartyIDsGroup);

                QuickFix.FIX50.NewOrderSingle.NoDisclosureInstructionsGroup noDisclosureInstGroup = new QuickFix.FIX50.NewOrderSingle.NoDisclosureInstructionsGroup();
                noDisclosureInstGroup.DisclosureType = new DisclosureType(100);
                noDisclosureInstGroup.DisclosureInstruction = new DisclosureInstruction(1);
                newOrderSingle.AddGroup(noDisclosureInstGroup);

                //newOrderSingle.Currency = new Currency("HKD");

                // Maintain XReference
                RefObj refObj = new RefObj();
                refObj.clOrderID = clOrdID.ToString();

                // obj.exchangeCode is used to put "OrderRouterID"
                string[] temp = { obj.accountNo, obj.systemCode, obj.clientNo, obj.localSystemCode, info.code, obj.exchangeCode, info.priceType, info.validDate, "specialReferenceReserve" };

                refObj.newOrderSingle = newOrderSingle;
                refObj.strArray = temp;
                //add by xiang at 20170911 -begin
                //保存最后一次发送的netinfo,如果超过throttle限制就重发一遍
                refObj.lastSendInfo = obj;
                //add by xiang at 20170911 -end
                xReference.TryAdd(clOrdID, refObj);
                refObj.addClientReq(newOrderSingle);

                bool ret;
                if (isTestMode)
                {
                    newOrderSingle.Header.SetField(new SenderCompID("79G100N"));
                    strategy.ProcessToApp(newOrderSingle, null);
                    //newOrderSingle.SetField(new ClOrdID("10001"));
                    //xReference.Clear();
                    //xReference.TryAdd(10001, refObj);

                    ret = true;
                }
                else
                    ret = tradeApp.SendMessage(newOrderSingle);

                if (!ret)
                {
                    //TradeServerFacade.writeLog(LogLevel.IMPORTANT, "PlaceOrder() fail!");
                    //obj.infoT = "";  // ErrorCode.ERR_ORDER_0000_MSG;  //can not include Chinese character
                    //obj.errorCode = ErrorCode.ERR_ORDER_0000;
                    //obj.code = Command.OrderStockHK; //CommandCode.ORDER;
                    //obj.errorMsg = "Place order fail";

                    //TradeServerFacade.SendString(obj);

                    OrderException(obj, "socket exception,order fail.");
                }

            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
                OrderException(obj, msg);
            }
        }

        private ConcurrentDictionary<string, NetInfo> pendingCancelOrdersDict = new ConcurrentDictionary<string, NetInfo>();
        private void addToCacnelOrderPendingQueue(NetInfo obj)
        {
            lock (pendingCancelOrdersDict)
            {
                pendingCancelOrdersDict.TryAdd(obj.systemCode, obj);
                globexIFLogger.WriteLog("Queue cancel order, systemCode=" + obj.systemCode);
            }
        }

        public void CancelOrder(NetInfo obj)
        {
            #region old style
            /*
            try
            {
                CancelInfo info = new CancelInfo();
                info.MyReadString(obj.infoT);

                long clOrdID = Convert.ToInt64(info.orderNo);
                RefObj refObj;
                bool ret = xReference.TryGetValue(clOrdID, out refObj);

                if (isTestMode)
                {
                    cancelOrderWork(obj);
                }
                else
                {
                    // If not find from XReference, that means this order was cleared because filled or canceled
                    if (ret && refObj.getOrderStatus() != OrdStatus.CANCELED)
                    {
                        // In case of no Tag37-OrderID because of now no New Order Ack, or now waiting for Cancel Ack, Modify Ack
                        if (refObj.isPendingRequest)
                            ThreadPool.QueueUserWorkItem(new WaitCallback(doCancelTask), obj);
                        else
                            cancelOrderWork(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                TradeServerFacade.writeLog(LogLevel.SYSTEMERROR, ex.ToString());
            }
            */
            #endregion

            try
            {
                CancelInfo info = new CancelInfo();
                info.MyReadString(obj.infoT);

                if (pendingModifyOrdersDict.ContainsKey(obj.systemCode))
                {
                    NetInfo oldModifyObj;
                    pendingModifyOrdersDict.TryRemove(obj.systemCode, out oldModifyObj);
                }
                else if (pendingNewOrdersDict.ContainsKey(obj.systemCode))
                {
                    NetInfo oldNewOrderObj;
                    pendingNewOrdersDict.TryRemove(obj.systemCode, out oldNewOrderObj);

                    // Todo...
                    sendCancelReponseInfo_ForCancel(obj, ZD_CANCELED);
                    return;
                }




                //int caseNum = -1;
                char flag = '-';
                //int orderTypeIdx = -1;
                if (OrderTypeIdx.TryGetValue(info.priceType, out int orderTypeIdx))
                {
                    int timeDurIdx = getCurrentTimeDurIdx();
                    if (timeDurIdx == -1) return;


                    flag = cancelOrderMatrix[timeDurIdx, orderTypeIdx];
                    if (flag == 'P')
                    {
                        addToCacnelOrderPendingQueue(obj);

                        //Todo...
                        string replyState = ZD_PENDING_CANCEL; //waiting for canceling
                        sendCancelReponseInfo_ForCancel(obj, replyState);
                    }
                    //else if (flag == 'Y')
                    //    caseNum = 0;
                    //else if (flag == 'N')
                    //    caseNum = 1;

                    else if (flag == 'Y')
                    {
                        long clOrdID = Convert.ToInt64(info.orderNo);
                        RefObj refObj;
                        bool ret = xReference.TryGetValue(clOrdID, out refObj);

                        if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_REPLACE)
                        {
                            //cancelOrderWork(obj);
                            //add by xiang at 20170911 -begin
                            //_sendQueue.Producer(obj);
                            PreparingEnqueue(obj);
                            //globexIFLogger.log(ZDLogger.LVL_ERROR, "== add send queue cancel order, systemCode=" + obj.systemCode);
                            //add by xiang at 20170911 -end
                            globexIFLogger.WriteLog($"CancelOrder Stack4");
                        }
                        else
                        {
                            CancelOrderException(obj, $"ClOrdID {info.orderNo} does not exist!");
                        }
                    }
                    else
                    {
                        CancelOrderException(obj, "Cancel order is not alowed at current time.Please follow the order time table!");

                    }
                }
                else
                {

                    CancelOrderException(obj, "Unsupported order type!");
                }


                //if (flag == 'Y')
                //{
                //    long clOrdID = Convert.ToInt64(info.orderNo);
                //    RefObj refObj;
                //    bool ret = xReference.TryGetValue(clOrdID, out refObj);

                //    if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_REPLACE)
                //    {
                //        //cancelOrderWork(obj);
                //        //add by xiang at 20170911 -begin
                //        //_sendQueue.Producer(obj);
                //        PreparingEnqueue(obj);
                //        //globexIFLogger.log(ZDLogger.LVL_ERROR, "== add send queue cancel order, systemCode=" + obj.systemCode);
                //        //add by xiang at 20170911 -end
                //        globexIFLogger.log(ZDLogger.LVL_ERROR, $"CancelOrder Stack4");
                //    }
                //    else
                //    {
                //        CancelOrderException(obj, $"ClOrdID {info.orderNo} does not exist!");
                //    }
                //}
                //else if (flag == 'N')
                //{
                //    sendCancelReponseInfo_ForCancel_Fail(obj, null);
                //}
                //else
                //{
                //    CancelOrderException(obj,"Cancel order is not alowed at current time.Please follow the order time table!");

                //}


                //TODO 临时放开注释
                /*lock (pendingNewOrdersDict)
                 {
                     NetInfo objX;
                     if (pendingNewOrdersDict.TryRemove(obj.systemCode, out objX))
                     {
                         cancelOrderInPendingDict(objX, obj);
                         return;
                     }
                     else
                     {
                         if (string.IsNullOrEmpty(info.orderNo))
                         {
                             cancelOrderInPendingDictFail(objX, obj);
                         }
                     }
                 }

                 long mclOrdID = Convert.ToInt64(info.orderNo);
                 RefObj mrefObj;
                 bool mret = xReference.TryGetValue(mclOrdID, out mrefObj);
                 if (mret && mrefObj.orderStatus != OrdStatus.PENDING_CANCEL && mrefObj.orderStatus != OrdStatus.PENDING_REPLACE)
                 {
                     cancelOrderWork(obj);
                 }
                 */

            }
            catch (Exception ex)
            {
                //CancelInfo info = new CancelInfo();
                //info.MyReadString(obj.infoT);
                //TradeServerFacade.writeLog(LogLevel.SYSTEMERROR, info.MyToString());
                globexIFLogger.WriteLog(ex.ToString());
                CancelOrderException(obj, ex.Message);

            }

        }

        public void cancelOrderWork(NetInfo obj)
        {
            long clOrdID;
            try
            {
                CancelInfo info = new CancelInfo();
                info.MyReadString(obj.infoT);
                long newOrderClOrdID = Convert.ToInt64(info.orderNo);
                RefObj refObj;
                bool ret = xReference.TryGetValue(newOrderClOrdID, out refObj);

                if (ret)
                {
                    QuickFix.FIX50.OrderCancelRequest orderCancelRequest = new QuickFix.FIX50.OrderCancelRequest();
                    HKCodeBean codeBean = CodeTransfer_HKEX.getUpperCodeInfoByZDCode(info.code, info.exchangeCode);

                    // ******* Order Message fields ********
                    //orderCancelRequest.OrderID = new OrderID(refObj.orderID);

                    clOrdID = ClOrderIDGen.getNextClOrderID();
                    orderCancelRequest.ClOrdID = new ClOrdID(clOrdID.ToString());

                    requestNoDict.TryAdd(clOrdID.ToString(), obj.RequestNo);

                    QuickFix.Message lastMsg = null;
                    if (refObj.fromClient.Count == 0)
                        lastMsg = refObj.newOrderSingle;
                    else
                        lastMsg = refObj.fromClient[refObj.fromClient.Count - 1];
                    orderCancelRequest.OrigClOrdID = new OrigClOrdID(lastMsg.GetField(Tags.ClOrdID));

                    specialReference.TryAdd(orderCancelRequest.OrigClOrdID.getValue(), long.Parse(refObj.clOrderID));
                    //refObj.strArray[7] = orderCancelRequest.OrigClOrdID.getValue() + ":" + refObj.clOrderID;
                    ////add by xiang at 20170911 -begin
                    ////保存最后一次发送的netinfo,如果超过throttle限制就重发一遍
                    //refObj.lastSendInfo = obj;
                    //add by xiang at 20170911 -end
                    //*/

                    // Tag1
                    //orderCancelRequest.Account = new Account(obj.accountNo);

                    // Tag55
                    orderCancelRequest.SecurityID = new SecurityID(codeBean.upperProduct);
                    orderCancelRequest.SecurityIDSource = securityIDSource;
                    orderCancelRequest.SecurityExchange = securityExchange;


                    // Tag54
                    orderCancelRequest.Side = QuerySide(info.buySale);

                    // Tag38
                    orderCancelRequest.OrderQty = new OrderQty(decimal.Parse(info.orderNumber));

                    // Tag60
                    orderCancelRequest.TransactTime = new TransactTime(DateTime.UtcNow);

                    //Code for OCG simulator start

                    //string[] p = info.userType.Split('|');

                    //sessionID = p[2];

                    //Code for OCG simulator end


                    QuickFix.FIX50.OrderCancelRequest.NoPartyIDsGroup noPartyIDsGroup = new QuickFix.FIX50.OrderCancelRequest.NoPartyIDsGroup();
                    noPartyIDsGroup.PartyID = new PartyID(sessionID);
                    noPartyIDsGroup.PartyIDSource = new PartyIDSource('D');
                    noPartyIDsGroup.PartyRole = new PartyRole(1);
                    orderCancelRequest.AddGroup(noPartyIDsGroup);

                    if (isTestMode)
                    {
                        orderCancelRequest.Header.SetField(new SenderCompID("79G100N"));
                        strategy.ProcessToApp(orderCancelRequest, null);
                        ret = true;
                    }
                    else
                    {
                        ret = tradeApp.SendMessage(orderCancelRequest);
                    }
                    if (!ret)
                    {
                        //CancelResponseInfo cinfo = new CancelResponseInfo();
                        //cinfo.orderNo = info.orderNo;

                        //obj.infoT = cinfo.MyToString();
                        //obj.exchangeCode = info.exchangeCode;
                        //obj.accountNo = info.accountNo;
                        //obj.systemCode = info.systemNo;
                        //obj.errorCode = ErrorCode.ERR_ORDER_0014;
                        //obj.code = Command.CancelStockHK;  // CommandCode.CANCELCAST;
                        //obj.errorMsg = "Cancel order fail";

                        //TradeServerFacade.SendString(obj);

                        CancelOrderException(obj, $"socket exception,clOderNo:{info.orderNo} cancel order fail!");
                    }
                    else
                    {
                        refObj.strArray[7] = orderCancelRequest.OrigClOrdID.getValue() + ":" + refObj.clOrderID;
                        //add by xiang at 20170911 -begin
                        //保存最后一次发送的netinfo,如果超过throttle限制就重发一遍
                        refObj.lastSendInfo = obj;
                        //AuditTrailMgr.addMsg(cancelMsg, obj);
                        refObj.addClientReq(orderCancelRequest);
                        xReference.TryAdd(clOrdID, refObj);

                    }
                }
                else
                {
                    var errorMsg = @"cancelOrderWork: Cannot find XReference. clOrderID:{newOrderClOrdID}";
                    CancelOrderException(obj, errorMsg);
                }

            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
                CancelOrderException(obj, msg);
            }
        }

        /// <summary>
        /// 内存数据丢失调用此方法
        /// </summary>
        /// <param name="obj"></param>
        public void CancelOrderByResponse(NetInfo obj)
        {
            long clOrdID;
            try
            {
                CancelInfo info = new CancelInfo();
                info.MyReadString(obj.infoT);
                long newOrderClOrdID = Convert.ToInt64(info.orderNo);


                QuickFix.FIX50.OrderCancelRequest orderCancelRequest = new QuickFix.FIX50.OrderCancelRequest();
                HKCodeBean codeBean = CodeTransfer_HKEX.getUpperCodeInfoByZDCode(info.code, info.exchangeCode);

                // ******* Order Message fields ********
                //orderCancelRequest.OrderID = new OrderID(refObj.orderID);

                clOrdID = ClOrderIDGen.getNextClOrderID();
                orderCancelRequest.ClOrdID = new ClOrdID(clOrdID.ToString());

                requestNoDict.TryAdd(clOrdID.ToString(), obj.RequestNo);


                orderCancelRequest.OrigClOrdID = new OrigClOrdID(info.orderNo);

                // specialReference.TryAdd(orderCancelRequest.OrigClOrdID.getValue(), long.Parse(refObj.clOrderID));
                //refObj.strArray[7] = orderCancelRequest.OrigClOrdID.getValue() + ":" + refObj.clOrderID;
                ////add by xiang at 20170911 -begin
                ////保存最后一次发送的netinfo,如果超过throttle限制就重发一遍
                //refObj.lastSendInfo = obj;
                //add by xiang at 20170911 -end
                //*/

                // Tag1
                //orderCancelRequest.Account = new Account(obj.accountNo);

                // Tag55
                orderCancelRequest.SecurityID = new SecurityID(codeBean.upperProduct);
                orderCancelRequest.SecurityIDSource = securityIDSource;
                orderCancelRequest.SecurityExchange = securityExchange;


                // Tag54
                orderCancelRequest.Side = QuerySide(info.buySale);

                // Tag38
                orderCancelRequest.OrderQty = new OrderQty(decimal.Parse(info.orderNumber));

                // Tag60
                orderCancelRequest.TransactTime = new TransactTime(DateTime.UtcNow);

                //Code for OCG simulator start

                //string[] p = info.userType.Split('|');

                //sessionID = p[2];

                //Code for OCG simulator end


                QuickFix.FIX50.OrderCancelRequest.NoPartyIDsGroup noPartyIDsGroup = new QuickFix.FIX50.OrderCancelRequest.NoPartyIDsGroup();
                noPartyIDsGroup.PartyID = new PartyID(sessionID);
                noPartyIDsGroup.PartyIDSource = new PartyIDSource('D');
                noPartyIDsGroup.PartyRole = new PartyRole(1);
                orderCancelRequest.AddGroup(noPartyIDsGroup);



                tradeApp.SendMessage(orderCancelRequest);





            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
                CancelOrderException(obj, msg);
            }
        }

        public void doCancelTask(object stateObj)
        {
            try
            {
                NetInfo obj = (NetInfo)stateObj;
                CancelInfo info = new CancelInfo();
                info.MyReadString(obj.infoT);

                long clOrdID = Convert.ToInt64(info.orderNo);
                RefObj refObj;
                bool ret = xReference.TryGetValue(clOrdID, out refObj);
                if (ret)
                {
                    refObj.bizSynLock.Reset();
                    refObj.bizSynLock.WaitOne();
                    //cancelOrderWork(obj);

                    //add by xiang at 20170911 -begin
                    //_sendQueue.Producer(obj);
                    PreparingEnqueue(obj);
                    globexIFLogger.WriteLog("== add send queue doCancelTask, systemCode=" + obj.systemCode);
                    //add by xiang at 20170911 -end
                }
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
        }

        private ConcurrentDictionary<string, NetInfo> pendingModifyOrdersDict = new ConcurrentDictionary<string, NetInfo>();
        private void addToModifyOrderPendingQueue(NetInfo obj)
        {
            lock (pendingModifyOrdersDict)
            {
                pendingModifyOrdersDict.TryAdd(obj.systemCode, obj);
                globexIFLogger.WriteLog("Queue modify order, systemCode=" + obj.systemCode);
            }
        }

        public void CancelReplaceOrder(NetInfo obj)
        {
            #region old style
            /*
            try
            {
                ModifyInfo info = new ModifyInfo();
                info.MyReadString(obj.infoT);

                long clOrdID = Convert.ToInt64(info.orderNo);
                RefObj refObj;
                bool ret = xReference.TryGetValue(clOrdID, out refObj);
                // If not find from XReference, that means this order was cleared because filled or canceled
                if (ret && refObj.getOrderStatus() != OrdStatus.CANCELED)
                {
                    // In case of no Tag37-OrderID because of now no New Order Ack, or now waiting for Cancel Ack, Modify Ack
                    if (refObj.isPendingRequest)
                        ThreadPool.QueueUserWorkItem(new WaitCallback(doCancelRepalceTask), obj);
                    else
                    {
                        cancelReplaceOrderWork(obj);
                    }
                }

            }
            catch (Exception e)
            {
                TradeServerFacade.writeLog(LogLevel.SYSTEMERROR, e.ToString());
            }
            */
            #endregion

            try
            {
                ModifyInfo info = new ModifyInfo();
                info.MyReadString(obj.infoT);


                if (pendingModifyOrdersDict.ContainsKey(obj.systemCode))
                {
                    lock (pendingModifyOrdersDict)
                    {
                        NetInfo oldModifyObj;
                        pendingModifyOrdersDict.TryRemove(obj.systemCode, out oldModifyObj);
                        pendingModifyOrdersDict.TryAdd(obj.systemCode, obj);

                        string replyState = null;
                        long clOrdID = Convert.ToInt64(info.orderNo);
                        RefObj refObj;
                        bool ret = xReference.TryGetValue(clOrdID, out refObj);
                        if (ret)
                        {
                            // Todo...
                            replyState = ZD_PENDING_MODIFY;    //waiting for modifying
                        }


                        sendOrderResonseInfo_ForModify_onModifyOrder(obj, info.orderNo, replyState);
                    }
                    return;
                }
                else if (pendingNewOrdersDict.ContainsKey(obj.systemCode))
                {

                    lock (pendingNewOrdersDict)
                    {
                        NetInfo oldNewOrderObj;
                        if (pendingNewOrdersDict.TryGetValue(obj.systemCode, out oldNewOrderObj))
                        {

                            //pendingNewOrdersDict.TryAdd(obj.systemCode, obj);
                            OrderInfo orderInfo = new OrderInfo();
                            orderInfo.MyReadString(oldNewOrderObj.infoT);
                            orderInfo.orderPrice = info.modifyPrice;
                            orderInfo.orderNumber = info.modifyNumber;
                            orderInfo.triggerPrice = info.modifyTriggerPrice;

                            oldNewOrderObj.RequestNo = obj.RequestNo;

                            oldNewOrderObj.infoT = orderInfo.MyToString();
                        }

                        // Todo...
                        sendOrderResonseInfo_ForModify_onNewOrder(oldNewOrderObj, info.orderNo, ZD_PENDING_SEND_OUT, obj.RequestNo);
                    }

                    return;
                }


                //int caseNum = -1;

                //int orderTypeIdx = -1;
                char flag = '-';
                if (OrderTypeIdx.TryGetValue(info.priceType, out int orderTypeIdx))
                {
                    int timeDurIdx = getCurrentTimeDurIdx();
                    if (timeDurIdx == -1) return;


                    flag = modifyOrderMatrix[timeDurIdx, orderTypeIdx];
                    if (flag == 'P')
                    {
                        addToModifyOrderPendingQueue(obj);

                        //Todo...
                        string replyState = ZD_PENDING_MODIFY; //waiting for modifying
                        sendOrderResonseInfo_ForModify_onModifyOrder(obj, info.orderNo, replyState);
                    }
                    //else if (flag == 'Y')
                    //    caseNum = 0;
                    //else if (flag == 'N')
                    //    caseNum = 1;
                    else if (flag == 'Y')
                    {
                        long clOrdID = Convert.ToInt64(info.orderNo);
                        RefObj refObj;
                        bool ret = xReference.TryGetValue(clOrdID, out refObj);
                        if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_CANCELREPLACE)
                        {
                            //cancelReplaceOrderWork(obj);

                            //add by xiang at 20170911 -begin
                            //_sendQueue.Producer(obj);
                            PreparingEnqueue(obj);
                            globexIFLogger.WriteLog("== add send queue replace order, systemCode=" + obj.systemCode);
                            //add by xiang at 20170911 -end
                        }
                        else
                        {
                            OrderCancelReplaceException(obj, $"ClOrdID {info.orderNo} does not exist!");
                        }
                    }
                    else
                    {
                        //OrderResponseInfo minfo = new OrderResponseInfo();
                        //minfo.orderNo = info.orderNo;

                        //obj.infoT = minfo.MyToString();
                        //obj.exchangeCode = info.exchangeCode;
                        //obj.accountNo = info.accountNo;
                        //obj.errorCode = ErrorCode.ERR_ORDER_0016;
                        //obj.code = Command.ModifyStockHK; //CommandCode.MODIFY;

                        //TradeServerFacade.SendString(obj);
                        OrderCancelReplaceException(obj, "Cancel replace order is not alowed at current time.Please follow the order time table!");

                    }
                }
                else
                {
                    OrderCancelReplaceException(obj, "Unsupported order type!");
                }



            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
                OrderCancelReplaceException(obj, ex.Message);
            }
        }

        public void cancelReplaceOrderWork(NetInfo obj)
        {
            long clOrdID;
            try
            {
                ModifyInfo info = new ModifyInfo();
                info.MyReadString(obj.infoT);

                long newOrderClOrdID = Convert.ToInt64(info.orderNo);
                RefObj refObj;
                bool ret = xReference.TryGetValue(newOrderClOrdID, out refObj);
                if (ret)
                {
                    QuickFix.FIX50.NewOrderSingle newOrderSingle = refObj.newOrderSingle;
                    QuickFix.FIX50.OrderCancelReplaceRequest ocrr = new QuickFix.FIX50.OrderCancelReplaceRequest();
                    HKCodeBean codeBean = CodeTransfer_HKEX.getUpperCodeInfoByZDCode(info.code, info.exchangeCode);

                    // ******* Order Message fields ********


                    clOrdID = ClOrderIDGen.getNextClOrderID();
                    ocrr.ClOrdID = new ClOrdID(clOrdID.ToString());

                    requestNoDict.TryAdd(clOrdID.ToString(), obj.RequestNo);


                    QuickFix.Message lastMsg = null;
                    QuickFix.Message fromGlobex = null;
                    if (refObj.fromClient.Count == 0)
                        lastMsg = refObj.newOrderSingle;
                    else
                    {
                        lastMsg = refObj.fromClient[refObj.fromClient.Count - 1];
                        fromGlobex = refObj.fromGlobex[refObj.fromGlobex.Count - 1];
                    }

                    string lastClOrderId = lastMsg.GetField(Tags.ClOrdID);
                    //获取最后一条从fix server 过来的消息的OrderID(改单需要指定,其他毫无卵用)
                    ocrr.OrderID = new OrderID(fromGlobex.GetField(Tags.OrderID));
                    ocrr.OrigClOrdID = new OrigClOrdID(lastClOrderId);

                    //关联 最后一条消息的ClOrdID <--> 下单时的ClOrdID
                    //上手的OrderID在改单时会变,无法使用,建立ClOrdID的变化关系
                    specialReference.TryAdd(lastClOrderId, long.Parse(refObj.clOrderID));
                    //refObj.strArray[7] = lastClOrderId + ":" + refObj.clOrderID;
                    ////add by xiang at 20170911 -begin
                    ////保存最后一次发送的netinfo,如果超过throttle限制就重发一遍
                    //refObj.lastSendInfo = obj;
                    //add by xiang at 20170911 -end

                    //add by xiang at 20171011 -begin
                    RefObj temp;
                    downReference.TryRemove(ocrr.OrderID.getValue(), out temp);
                    globexIFLogger.WriteLog("remove order id cancelReplaceOrderWork " + ocrr.OrderID.getValue());
                    //add by xiang at 20171011 -end

                    ocrr.SecurityID = new SecurityID(codeBean.upperProduct);
                    ocrr.SecurityIDSource = securityIDSource;
                    ocrr.SecurityExchange = securityExchange;

                    //Added by Rainer on 20150609 -begin
                    //ocrr.TimeInForce = newOrderSingle.TimeInForce;
                    //Added by Rainer on 20150609 -end

                    ocrr.Side = newOrderSingle.Side;

                    // Tag60
                    ocrr.TransactTime = new TransactTime(DateTime.UtcNow);

                    OrdType ordType = newOrderSingle.OrdType;
                    ocrr.OrdType = ordType;
                    ocrr.OrderQty = new OrderQty(Convert.ToDecimal(info.modifyNumber));
                    char charOrderTpe = ordType.getValue();
                    if (charOrderTpe == OrdType.LIMIT)
                    {
                        decimal prx = decimal.Parse(info.modifyPrice); // CodeTransfer_TT.toGlobexPrx(info.modifyPrice, strSymbol);
                        ocrr.Price = new Price(prx);
                    }

                    //设置成原单的TimeInForce值
                    ocrr.TimeInForce = newOrderSingle.TimeInForce;

                    //Code for OCG simulator start

                    //string[] p = info.userType.Split('|');
                    //if (p[0] == "1")
                    //    ocrr.MaxPriceLevels = new MaxPriceLevels(1);

                    //if (p[1] != "")
                    //    ocrr.ExecInst = new ExecInst(p[1]);

                    //sessionID = p[2];

                    //Code for OCG simulator end

                    // Tag18: ExecInst
                    //ocrr.ExecInst = new ExecInst("c x");

                    ocrr.NoPartyIDs = new NoPartyIDs(1);

                    QuickFix.FIX50.OrderCancelReplaceRequest.NoPartyIDsGroup noPartyIDsGroup = new QuickFix.FIX50.OrderCancelReplaceRequest.NoPartyIDsGroup();
                    noPartyIDsGroup.PartyID = new PartyID(sessionID);
                    noPartyIDsGroup.PartyIDSource = new PartyIDSource('D');
                    noPartyIDsGroup.PartyRole = new PartyRole(1);
                    ocrr.AddGroup(noPartyIDsGroup);

                    QuickFix.FIX50.OrderCancelReplaceRequest.NoDisclosureInstructionsGroup noDisclosureInstGroup = new QuickFix.FIX50.OrderCancelReplaceRequest.NoDisclosureInstructionsGroup();
                    noDisclosureInstGroup.DisclosureType = new DisclosureType(100);
                    noDisclosureInstGroup.DisclosureInstruction = new DisclosureInstruction(1);
                    ocrr.AddGroup(noDisclosureInstGroup);

                    if (isTestMode)
                    {
                        ocrr.Header.SetField(new SenderCompID("79G100N"));
                        strategy.ProcessToApp(ocrr, null);
                        ret = true;
                    }
                    else
                        ret = tradeApp.SendMessage(ocrr);

                    if (!ret)
                    {
                        //OrderResponseInfo minfo = new OrderResponseInfo();
                        //minfo.orderNo = info.orderNo;

                        //obj.infoT = minfo.MyToString();
                        //obj.exchangeCode = info.exchangeCode;
                        //obj.accountNo = info.accountNo;
                        //obj.errorCode = ErrorCode.ERR_ORDER_0016;
                        //obj.code = Command.ModifyStockHK; //CommandCode.MODIFY;
                        //obj.errorMsg = "Modify order fail";

                        //TradeServerFacade.SendString(obj);

                        //globexIFLogger.log(ZDLogger.LVL_ERROR, $"{obj.systemCode}:Modify order fail");

                        OrderCancelReplaceException(obj, $"socket exception.{obj.systemCode}:Modify order fail");
                    }
                    else
                    {
                        refObj.strArray[7] = lastClOrderId + ":" + refObj.clOrderID;
                        //add by xiang at 20170911 -begin
                        //保存最后一次发送的netinfo,如果超过throttle限制就重发一遍
                        refObj.lastSendInfo = obj;
                        //AuditTrailMgr.addMsg(ocrr, obj);
                        refObj.addClientReq(ocrr);
                        xReference.TryAdd(clOrdID, refObj);
                    }
                }
                else
                {
                    string errormsg = $"cancelReplaceOrderWork: Cannot find XReference.ClOrderID:{ newOrderClOrdID} ";

                    OrderCancelReplaceException(obj, errormsg);
                }

            }
            catch (Exception ex)
            {
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
                OrderCancelReplaceException(obj, msg);
            }
        }

        public void doCancelRepalceTask(object stateObj)
        {
            try
            {
                NetInfo obj = (NetInfo)stateObj;
                ModifyInfo info = new ModifyInfo();
                info.MyReadString(obj.infoT);

                long clOrdID = Convert.ToInt64(info.orderNo);
                RefObj refObj;
                bool ret = xReference.TryGetValue(clOrdID, out refObj);
                if (ret)
                {
                    refObj.bizSynLock.Reset();
                    refObj.bizSynLock.WaitOne();
                    //cancelReplaceOrderWork(obj);

                    //add by xiang at 20170911 -begin
                    //_sendQueue.Producer(obj);
                    PreparingEnqueue(obj);
                    globexIFLogger.WriteLog("== add send queue doCancelRepalceTask, systemCode=" + obj.systemCode);
                    //add by xiang at 20170911 -end
                }
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
        }


        private string getShareVol(decimal valDec)
        {

            if (valDec == 0)
                return "0";

            string temp = valDec.ToString();
            int idx = -1;
            idx = temp.IndexOf('.');

            if (idx == -1)
                return temp;
            else if (idx > 1)
                return temp.Substring(0, idx);
            else if (idx == 0)
                return "0";

            return "";
        }

        private Side QuerySide(string zdSide)
        {
            Side side = null;
            if (zdSide == "1")
                side = new Side(Side.BUY);
            else if (zdSide == "2")
                side = new Side(Side.SELL);
            else if (zdSide == "5")
                side = new Side(Side.SELL_SHORT);
            return side;
        }


        //Common_comboBox_OrderType_StockHK_1=限价盘[PL：Price Limit Order]
        //Common_comboBox_OrderType_StockHK_2=市价盘
        //Common_comboBox_OrderType_StockHK_5=竞价限价盘[AL: Auction limit order]
        //Common_comboBox_OrderType_StockHK_6=竞价盘[AO: Auction Order(Sell only)]
        //Common_comboBox_OrderType_StockHK_7=增强限价盘[EL: Enhace limit order]
        //Common_comboBox_OrderType_StockHK_8=特别限价盘[SL: Special limit ordre]



        public OrdType QueryOrdType(string s)
        {
            char c = ' ';
            switch (s)
            {
                case "6":
                case "2":
                    c = OrdType.MARKET;
                    break;

                case "1":
                case "5":
                case "7":
                case "8":
                    c = OrdType.LIMIT;
                    break;

                //case "1":
                //    c = OrdType.MARKET;
                //    break;
                //case "2":
                //    c = OrdType.LIMIT;
                //    break;

                default: throw new Exception("Unsupported Order Type!");
            }
            return new OrdType(c);
        }

        //public const char FILL_AND_KILL = '3';
        private TimeInForce QueryTimeInForce(string s)
        {
            char c = ' ';
            switch (s)
            {
                //Code for OCG simulator start
                //case "3":
                //    c = TimeInForce.IMMEDIATE_OR_CANCEL;
                //    break;
                //case "4":
                //    c = TimeInForce.FILL_OR_KILL;
                //    break;
                //Code for OCG simulator end

                //case "5": //竞价限价盘[AL: Auction limit order]
                //    c = TimeInForce.AT_CROSSING;
                //    break;
                //case "7": //增强限价盘[EL: Enhace limit order]
                //    c = TimeInForce.DAY;
                //    break;

                case "1":
                case "7":
                    c = TimeInForce.DAY;
                    break;

                case "5":
                case "6":
                    c = TimeInForce.AT_CROSSING;
                    break;

                case "8":
                    c = TimeInForce.IMMEDIATE_OR_CANCEL;
                    break;

                default: throw new Exception("Unsupported Time In Force!");
            }

            return new TimeInForce(c);
        }

        //private string QueryValidDate(char ordType)
        //{
        //    string ret = "";
        //    switch (ordType)
        //    {
        //        case TimeInForce.DAY: ret = "1"; break;
        //        case TimeInForce.GOOD_TILL_CANCEL: ret = "2"; break;
        //        case TimeInForce.AT_THE_OPENING: ret = "3"; break;
        //        case TimeInForce.IMMEDIATE_OR_CANCEL: ret = "4"; break;
        //        case TimeInForce.FILL_OR_KILL: ret = "5"; break;
        //        case TimeInForce.GOOD_TILL_DATE: ret = "6"; break;
        //        case TimeInForce.AT_THE_CLOSE: ret = "7"; break;

        //        default: throw new Exception("unsupported input");
        //    }

        //    return ret;
        //}


        // production --begin
        //*

        //*/
        // production --end

        // test --begin
        //private TimeSpan ts1 = new TimeSpan(9, 59, 59);
        //private TimeSpan ts2 = new TimeSpan(16, 10, 0);
        // test --end


        /*
         * 我们系统盘前只支持限价单。
         * 
         * 市价单待确认是否交易所能下通。
         */
        /// <summary>
        /// 8:30
        /// </summary>
        private TimeSpan ts1 = new TimeSpan(8, 29, 59); //.Add(offsetTS);
        /// <summary>
        /// 16:10
        /// </summary>
        private TimeSpan ts2 = new TimeSpan(16, 10, 0); //.Add(offsetTS);
        /// <summary>
        /// 改单、撤单不传客户端，把下单中的客户号缓存，留改、撤单用
        /// key:systemCode.val:clientNo
        /// </summary>
        /// 
        private ConcurrentDictionary<string, string> _clientNoDic = new ConcurrentDictionary<string, string>();



        /// <summary>
        /// 壳调用下单入口
        /// </summary>
        /// <param name="netInfo"></param>
        public void doOrder(NetInfo netInfo)
        {
            try
            {
                _receiveMsgsLog.WriteLog(netInfo.MyToString());
                // Not login into upper
                /*
                if (!isUnitTestEnabled && SessionStateData.sessionID == null)
                {
                    netInfo.errorCode = ErrorCode.ERR_SERVER_0003;
                    netInfo.errorMsg = "Order gateway is not connected.";
                    communicationServer.SendString(netInfo);
                    return;
                }
                */

                //TimeSpan ts1 = new TimeSpan(8, 29, 59).Add(offsetTS);
                //TimeSpan ts2 = new TimeSpan(16, 10, 0).Add(offsetTS);

                # region 8:30之前，16:10不给下单
                TimeSpan dtNow = DateTime.Now.TimeOfDay;
                if (dtNow.Ticks < ts1.Ticks || dtNow.Ticks > ts2.Ticks)
                {
                    var errorMsg = "before 8:30:00 and 16:10:00 - order is not allowed!";
                    globexIFLogger.WriteLog(errorMsg);
                    netInfo.errorCode = ErrorCode.ERR_ORDER_0035;
                    //netInfo.code = Command.OrderStockHK;
                    if (netInfo.code == Command.OrderStockHK) //== CommandCode.ORDER)
                    {
                        netInfo.infoT = "";
                    }
                    else if (netInfo.code == Command.ModifyStockHK) //CommandCode.MODIFY)
                    {
                        ModifyInfo info = new ModifyInfo();
                        info.MyReadString(netInfo.infoT);

                        OrderResponseInfo retInfo = new OrderResponseInfo();
                        retInfo.orderNo = info.orderNo;
                        retInfo.systemNo = netInfo.systemCode;

                        netInfo.infoT = retInfo.MyToString();

                    }
                    else if (netInfo.code == Command.CancelStockHK) //CommandCode.CANCEL)
                    {
                        CancelInfo info = new CancelInfo();
                        info.MyReadString(netInfo.infoT);

                        CancelResponseInfo cancelResponseInfo = new CancelResponseInfo();
                        cancelResponseInfo.orderNo = info.orderNo;
                        cancelResponseInfo.systemNo = info.systemNo;

                        netInfo.infoT = cancelResponseInfo.MyToString();
                    }

                    TradeServerFacade.SendString(netInfo);
                    return;
                }
                #endregion

                // For development test -begin
                //netInfo.accountNo = "000040";
                // For development test -end
                if (netInfo.code == Command.OrderStockHK) //== CommandCode.ORDER)
                {
                    //待优化
                    if (_clientNoDic.ContainsKey(netInfo.systemCode))
                    {
                        globexIFLogger.WriteLog("systemCode exists _clientNoDic");
                    }
                    else
                    {
                        _clientNoDic.TryAdd(netInfo.systemCode, netInfo.clientNo);
                    }
                    PlaceOrder(netInfo);
                }
                else if (netInfo.code == Command.ModifyStockHK) //CommandCode.MODIFY)
                {
                    if (_clientNoDic.ContainsKey(netInfo.systemCode))
                    {
                        netInfo.clientNo = _clientNoDic[netInfo.systemCode];
                    }
                    CancelReplaceOrder(netInfo);
                }
                else if (netInfo.code == Command.CancelStockHK) //CommandCode.CANCEL)
                {
                    if (_clientNoDic.ContainsKey(netInfo.systemCode))
                    {
                        netInfo.clientNo = _clientNoDic[netInfo.systemCode];
                    }
                    CancelOrder(netInfo);
                }
                else
                {
                    globexIFLogger.WriteLog($"{netInfo.systemCode} - Unsupported  order command");
                }
            }
            catch (Exception ex)
            {
                globexIFLogger.WriteLog(ex.ToString());
            }
        }

        private void cancelOrderInPendingDict(NetInfo netInfoOfOrder, NetInfo netInfoOfCancel)
        {

            OrderResponseInfo info = new OrderResponseInfo();
            OrderInfo orderInfo = new OrderInfo();

            orderInfo.MyReadString(netInfoOfOrder.infoT);
            info.orderNo = ClOrderIDGen.getNextClOrderID().ToString();
            info.origOrderNo = "";
            info.orderMethod = "1";
            info.htsType = "";
            info.exchangeCode = orderInfo.exchangeCode;
            info.buySale = orderInfo.buySale;
            info.tradeType = orderInfo.tradeType;
            info.orderPrice = orderInfo.orderPrice;
            info.triggerPrice = orderInfo.triggerPrice;
            info.validDate = orderInfo.validDate;
            info.orderNumber = orderInfo.orderNumber;
            info.filledNumber = "0";
            info.acceptType = "1";

            DateTime transTime = Utils.toChinaLocalTime(DateTime.Now);
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");
            info.priceType = orderInfo.priceType;
            info.code = orderInfo.code;
            info.accountNo = orderInfo.accountNo;
            info.systemNo = netInfoOfOrder.systemCode;

            NetInfo obj = new NetInfo();

            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = Command.OrderStockHK; // CommandCode.ORDER;

            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            obj.todayCanUse = netInfoOfOrder.todayCanUse;
            obj.clientNo = netInfoOfOrder.clientNo;
            obj.localSystemCode = netInfoOfOrder.localSystemCode;

            TradeServerFacade.SendString(obj);


            CancelResponseInfo cancelResponseInfo = new CancelResponseInfo();
            CancelInfo cancelInfo = new CancelInfo();
            cancelInfo.MyReadString(netInfoOfCancel.infoT);
            cancelResponseInfo.exchangeCode = orderInfo.exchangeCode;
            cancelResponseInfo.buySale = orderInfo.buySale;
            cancelResponseInfo.orderNo = info.orderNo;
            //系统号
            cancelResponseInfo.accountNo = cancelInfo.accountNo;
            cancelResponseInfo.systemNo = cancelInfo.systemNo;
            cancelResponseInfo.code = orderInfo.code;
            cancelResponseInfo.priceType = orderInfo.priceType;
            cancelResponseInfo.orderPrice = orderInfo.orderPrice;
            cancelResponseInfo.cancelNumber = orderInfo.orderNumber;
            cancelResponseInfo.orderNumber = orderInfo.orderNumber;
            cancelResponseInfo.filledNumber = "0";
            cancelResponseInfo.cancelNo = cancelResponseInfo.orderNo;

            transTime = Utils.toChinaLocalTime(DateTime.Now);
            cancelResponseInfo.cancelTime = transTime.ToString("HH:mm:ss");
            cancelResponseInfo.cancelDate = transTime.ToString("yyyy-MM-dd");

            NetInfo cancelObj = new NetInfo();
            cancelObj.infoT = cancelResponseInfo.MyToString();
            cancelObj.exchangeCode = cancelResponseInfo.exchangeCode;
            cancelObj.errorCode = ErrorCode.SUCCESS;
            cancelObj.code = Command.CancelStockHK; // CommandCode.CANCELCAST;
            cancelObj.accountNo = cancelResponseInfo.accountNo;
            cancelObj.systemCode = cancelResponseInfo.systemNo;
            cancelObj.todayCanUse = netInfoOfOrder.todayCanUse;
            cancelObj.clientNo = netInfoOfOrder.clientNo;

            TradeServerFacade.SendString(cancelObj);
        }

        private void cancelOrderInPendingDictFail(NetInfo netInfoOfOrder, NetInfo netInfoOfCancel)
        {

            OrderResponseInfo info = new OrderResponseInfo();
            OrderInfo orderInfo = new OrderInfo();

            orderInfo.MyReadString(netInfoOfOrder.infoT);
            info.orderNo = ClOrderIDGen.getNextClOrderID().ToString();
            info.origOrderNo = "";
            info.orderMethod = "1";
            info.htsType = "";
            info.exchangeCode = orderInfo.exchangeCode;
            info.buySale = orderInfo.buySale;
            info.tradeType = orderInfo.tradeType;
            info.orderPrice = orderInfo.orderPrice;
            info.triggerPrice = orderInfo.triggerPrice;
            info.validDate = orderInfo.validDate;
            info.orderNumber = orderInfo.orderNumber;
            info.filledNumber = "0";
            info.acceptType = "1";

            DateTime transTime = Utils.toChinaLocalTime(DateTime.Now);
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");
            info.priceType = orderInfo.priceType;
            info.code = orderInfo.code;
            info.accountNo = orderInfo.accountNo;
            info.systemNo = netInfoOfOrder.systemCode;

            NetInfo obj = new NetInfo();

            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = Command.OrderStockHK; // CommandCode.ORDER;

            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            obj.todayCanUse = netInfoOfOrder.todayCanUse;
            obj.clientNo = netInfoOfOrder.clientNo;
            obj.localSystemCode = netInfoOfOrder.localSystemCode;

            TradeServerFacade.SendString(obj);


            CancelResponseInfo cinfo = new CancelResponseInfo();
            cinfo.orderNo = info.orderNo;

            netInfoOfCancel.infoT = cinfo.MyToString();
            netInfoOfCancel.exchangeCode = info.exchangeCode;
            netInfoOfCancel.accountNo = info.accountNo;
            netInfoOfCancel.systemCode = info.systemNo;
            netInfoOfCancel.errorCode = ErrorCode.ERR_ORDER_0014;
            netInfoOfCancel.code = CommandCode.CANCELCAST;

            TradeServerFacade.SendString(netInfoOfCancel);
        }


        private void sendOrderResonseInfo_ForNewOrder(NetInfo netInfoOfOrder, string orderId, string replyState)
        {
            OrderResponseInfo info = new OrderResponseInfo();

            OrderInfo orderInfo = new OrderInfo();
            orderInfo.MyReadString(netInfoOfOrder.infoT);

            info.orderNo = orderId;
            info.origOrderNo = "";
            info.orderMethod = "1";
            info.htsType = "";
            info.exchangeCode = orderInfo.exchangeCode;
            info.buySale = orderInfo.buySale;
            info.tradeType = orderInfo.tradeType;
            info.orderPrice = orderInfo.orderPrice;
            info.triggerPrice = orderInfo.triggerPrice;
            info.validDate = orderInfo.validDate;
            info.orderNumber = orderInfo.orderNumber;
            info.filledNumber = "0";
            info.acceptType = "1";

            DateTime transTime = Utils.toChinaLocalTime(DateTime.Now);
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");
            info.priceType = orderInfo.priceType;
            info.code = orderInfo.code;
            info.accountNo = orderInfo.accountNo;
            info.systemNo = netInfoOfOrder.systemCode;

            // important value
            if (replyState != null)
                info.orderState = replyState;

            NetInfo obj = new NetInfo();

            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = Command.OrderStockHK; // CommandCode.ORDER;

            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            obj.todayCanUse = netInfoOfOrder.todayCanUse;
            obj.clientNo = netInfoOfOrder.clientNo;
            obj.localSystemCode = netInfoOfOrder.localSystemCode;
            obj.RequestNo = netInfoOfOrder.RequestNo;

            TradeServerFacade.SendString(obj);
        }


        private void sendOrderResonseInfo_ForModify_onNewOrder(NetInfo netInfoOfOrder, string orderId, string replyState, string requestNo)
        {
            OrderResponseInfo info = new OrderResponseInfo();

            OrderInfo orderInfo = new OrderInfo();
            orderInfo.MyReadString(netInfoOfOrder.infoT);

            info.orderNo = orderId;
            info.origOrderNo = "";
            info.orderMethod = "1";
            info.htsType = "";
            info.exchangeCode = orderInfo.exchangeCode;
            info.buySale = orderInfo.buySale;
            info.tradeType = orderInfo.tradeType;
            info.orderPrice = orderInfo.orderPrice;
            info.triggerPrice = orderInfo.triggerPrice;
            info.validDate = orderInfo.validDate;
            info.orderNumber = orderInfo.orderNumber;
            info.filledNumber = "0";
            info.acceptType = "1";

            DateTime transTime = Utils.toChinaLocalTime(DateTime.Now);
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");
            info.priceType = orderInfo.priceType;
            info.code = orderInfo.code;
            info.accountNo = orderInfo.accountNo;
            info.systemNo = netInfoOfOrder.systemCode;

            // important value
            if (replyState != null)
                info.orderState = replyState;

            NetInfo obj = new NetInfo();

            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = Command.ModifyStockHK; // CommandCode.ORDER;

            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            obj.todayCanUse = netInfoOfOrder.todayCanUse;
            obj.clientNo = netInfoOfOrder.clientNo;
            obj.localSystemCode = netInfoOfOrder.localSystemCode;

            obj.RequestNo = requestNo;

            TradeServerFacade.SendString(obj);
        }

        private void sendOrderResonseInfo_ForModify_onModifyOrder(NetInfo netInfoOfModify, string orderId, string replyState)
        {
            OrderResponseInfo info = new OrderResponseInfo();

            ModifyInfo modifyInfo = new ModifyInfo();
            modifyInfo.MyReadString(netInfoOfModify.infoT);

            info.orderNo = orderId;
            info.origOrderNo = "";
            info.orderMethod = "1";
            info.htsType = "";
            info.exchangeCode = modifyInfo.exchangeCode;
            info.buySale = modifyInfo.buySale;
            info.tradeType = modifyInfo.tradeType;
            info.orderPrice = modifyInfo.modifyPrice;
            info.triggerPrice = modifyInfo.modifyTriggerPrice;
            info.validDate = modifyInfo.validDate;
            info.orderNumber = modifyInfo.modifyNumber;
            info.filledNumber = "0";
            info.acceptType = "1";

            DateTime transTime = Utils.toChinaLocalTime(DateTime.Now);
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");
            info.priceType = modifyInfo.priceType;
            info.code = modifyInfo.code;
            info.accountNo = modifyInfo.accountNo;
            info.systemNo = netInfoOfModify.systemCode;

            // important value
            if (replyState != null)
                info.orderState = replyState;

            NetInfo obj = new NetInfo();

            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = Command.ModifyStockHK; // CommandCode.ORDER;

            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            obj.todayCanUse = netInfoOfModify.todayCanUse;
            obj.clientNo = netInfoOfModify.clientNo;
            obj.localSystemCode = netInfoOfModify.localSystemCode;

            obj.RequestNo = netInfoOfModify.RequestNo;

            TradeServerFacade.SendString(obj);
        }

        private void sendCancelReponseInfo_ForCancel(NetInfo netInfoOfCancel, string replyState)
        {
            CancelResponseInfo cancelResponseInfo = new CancelResponseInfo();
            CancelInfo cancelInfo = new CancelInfo();
            cancelInfo.MyReadString(netInfoOfCancel.infoT);
            cancelResponseInfo.exchangeCode = cancelInfo.exchangeCode;
            cancelResponseInfo.buySale = cancelInfo.buySale;
            cancelResponseInfo.orderNo = cancelInfo.orderNo;
            //系统号
            cancelResponseInfo.accountNo = cancelInfo.accountNo;
            cancelResponseInfo.systemNo = cancelInfo.systemNo;
            cancelResponseInfo.code = cancelInfo.code;
            cancelResponseInfo.priceType = cancelInfo.priceType;
            cancelResponseInfo.orderPrice = cancelInfo.orderPrice;
            cancelResponseInfo.cancelNumber = cancelInfo.orderNumber;
            cancelResponseInfo.orderNumber = cancelInfo.orderNumber;
            cancelResponseInfo.filledNumber = "0";
            cancelResponseInfo.cancelNo = cancelResponseInfo.orderNo;

            if (replyState != null)
                cancelResponseInfo.errorCode = replyState;

            DateTime transTime = Utils.toChinaLocalTime(DateTime.Now);
            cancelResponseInfo.cancelTime = transTime.ToString("HH:mm:ss");
            cancelResponseInfo.cancelDate = transTime.ToString("yyyy-MM-dd");

            NetInfo cancelObj = new NetInfo();
            cancelObj.infoT = cancelResponseInfo.MyToString();
            cancelObj.exchangeCode = cancelResponseInfo.exchangeCode;
            cancelObj.errorCode = ErrorCode.SUCCESS;
            cancelObj.code = Command.CancelStockHK; // CommandCode.CANCELCAST;
            cancelObj.accountNo = cancelResponseInfo.accountNo;
            cancelObj.systemCode = cancelResponseInfo.systemNo;
            cancelObj.todayCanUse = netInfoOfCancel.todayCanUse;
            cancelObj.clientNo = netInfoOfCancel.clientNo;

            cancelObj.RequestNo = netInfoOfCancel.RequestNo;

            TradeServerFacade.SendString(cancelObj);
        }

        private void sendCancelReponseInfo_ForCancel_Fail(NetInfo netInfoOfCancel, string replyState)
        {
            CancelResponseInfo cinfo = new CancelResponseInfo();

            CancelInfo cancelInfo = new CancelInfo();
            cancelInfo.MyReadString(netInfoOfCancel.infoT);

            cinfo.orderNo = cancelInfo.orderNo;

            netInfoOfCancel.infoT = cinfo.MyToString();
            netInfoOfCancel.exchangeCode = cancelInfo.exchangeCode;
            netInfoOfCancel.accountNo = cancelInfo.accountNo;
            netInfoOfCancel.systemCode = cancelInfo.systemNo;
            netInfoOfCancel.errorCode = ErrorCode.ERR_ORDER_0014;
            netInfoOfCancel.code = CommandCode.CANCELCAST;

            cinfo.errorCode = netInfoOfCancel.RequestNo;

            TradeServerFacade.SendString(netInfoOfCancel);
        }


        public void partyEntitlement()
        {
            //PartyEntitlementsRequest per = new PartyEntitlementsRequest();
            ////Client assigned identifier for this request
            //per.EntitlementsRequestID = new EntitlementsRequestID("800101");

            //tradeApp.SendMessage(per);
        }

        public void throttleEntitlement()
        {
            UserRequest ur = new UserRequest();
            //Client assigned identifier for this request
            ur.UserRequestID = new UserRequestID(cfgInstance.BERequestID);
            //5 = Request Throttle Limit
            ur.UserRequestType = new UserRequestType(5);
            //Same as SenderCompID
            ur.Username = new Username(cfgInstance.SenderCompID);

            tradeApp.SendMessage(ur);
        }

        public void resendRequest(int begin, int end)
        {
            //ResendRequest rReq = new ResendRequest();
            //rReq.BeginSeqNo = new BeginSeqNo(begin);
            //rReq.EndSeqNo = new EndSeqNo(end);

            //tradeApp.SendMessage(rReq);
        }

        /// <summary>
        /// 根据throttle来控制一秒的发单量，如果因为时间精度问题导致单子被打回将会优先重新发送
        /// </summary>
        /*private void throttleSendMsg()
        {
            try
            {
                bool result;
                NetInfo netInfo;
                short mCount = 0;
                TimeSpan dtNow = DateTime.Now.TimeOfDay;

                while (true)
                {

                    double differ = DateTime.Now.TimeOfDay.TotalMilliseconds - dtNow.TotalMilliseconds;

                    //如果在一秒钟内下单超过messageThrottle(默认4个)  就睡一会。等下一秒的时间窗口
                    if (mCount >= messageThrottle && differ < 1000)
                    {
                        Thread.Sleep(1000 - (int)differ);

                        dtNow = DateTime.Now.TimeOfDay;
                        mCount = 0;
                    }
                    //如果下单数量超过4个，就清空。
                    if (mCount >= messageThrottle)
                    {
                        mCount = 0;
                    }
                    //如果到了下一个时间窗口，就重置一下时间
                    if (differ >= 1000)
                    {
                        dtNow = DateTime.Now.TimeOfDay;
                    }


                    if (failureQueue.Count > 0)
                    {
                        result = failureQueue.TryTake(out netInfo, 100);
                        if (result)
                        {
                            if (netInfo != null)
                            {
                                sendMsg(netInfo);
                                mCount++;
                            }
                        }
                        continue;
                    }

                    result = sendQueue.TryTake(out netInfo, 100);
                    if (result)
                    {
                        if (netInfo != null)
                        {
                            sendMsg(netInfo);
                            mCount++;
                        }
                    }

                    //判断退出标记
                    if (sendQueue.IsCompleted || failureQueue.IsCompleted)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                globexIFLogger.log(ZDLogger.LVL_CRITCAL, e.Message + e.Source + e.StackTrace);
            }
        }
        */

        //private void throttleSendMsg()
        //{
        //    try
        //    {
        //        bool result;
        //        NetInfo netInfo;

        //        while (true)
        //        {
        //            try
        //            {
        //                for (int i = 0; i < messageThrottle; i++)
        //                {
        //                    //if (failureQueue.Count > 0)
        //                    //{
        //                    //    result = failureQueue.TryTake(out netInfo, 100);
        //                    //    if (result)
        //                    //    {
        //                    //        if (netInfo != null)
        //                    //        {
        //                    //            sendMsg(netInfo);
        //                    //        }
        //                    //    }
        //                    //    continue;
        //                    //}

        //                    result = _sendQueue.co(out netInfo, 100);
        //                    if (result)
        //                    {
        //                        if (netInfo != null)
        //                        {
        //                            sendMsg(netInfo);
        //                        }
        //                    }
        //                }

        //                Thread.Sleep(1000);

        //                ////判断退出标记
        //                //if (sendQueue.IsCompleted || failureQueue.IsCompleted)
        //                //{
        //                //    break;
        //                //}

        //                //判断退出标记
        //                if (_sendQueue.IsCompleted)
        //                {
        //                    break;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                globexIFLogger.log(ZDLogger.LVL_CRITCAL, ex.ToString());
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        globexIFLogger.log(ZDLogger.LVL_CRITCAL, e.ToString());
        //    }
        //}
        private void SendMsg(NetInfo netInfo)
        {
            try
            {
                if (netInfo.code == Command.OrderStockHK) //== CommandCode.ORDER)
                {
                    globexIFLogger.WriteLog("== Send queue new order, systemCode=" + netInfo.systemCode);
                    OrderInfo info = new OrderInfo();
                    info.MyReadString(netInfo.infoT);
                    PlaceOrder(netInfo, info);

                }
                else if (netInfo.code == Command.ModifyStockHK) //CommandCode.MODIFY)
                {
                    globexIFLogger.WriteLog("== Send queue replace order, systemCode=" + netInfo.systemCode);
                    cancelReplaceOrderWork(netInfo);
                }
                else if (netInfo.code == Command.CancelStockHK) //CommandCode.CANCEL)
                {
                    globexIFLogger.WriteLog("== Send queue cancel order, systemCode=" + netInfo.systemCode);
                    cancelOrderWork(netInfo);

                }
            }
            catch (Exception e)
            {
                globexIFLogger.WriteLog(e.Message + e.Source + e.StackTrace);
            }
        }

        private OrderForm orderForm = null;
        public void ShowOrderForm()
        {
            if (orderForm == null || orderForm.IsDisposed)
            {
                orderForm = new OrderForm(this);
                //壳会设置
                //TradeServerFacade.setCommuServer(this);
            }
            orderForm.Show();
        }

        #region E2E
        public void TCR(string tradeReportID, string tradeID, string lastPx, string lastQty, string securityID)
        {

            TradeCaptureReport tradeCaptureReport = new TradeCaptureReport();
            //tag571
            tradeCaptureReport.TradeReportID = new TradeReportID(tradeReportID);
            //tag1003
            tradeCaptureReport.TradeID = new TradeID(tradeID);
            //tag856
            tradeCaptureReport.TradeReportType = new TradeReportType(6);
            //tag31
            tradeCaptureReport.LastPx = new LastPx(decimal.Parse(lastPx));
            //tag32
            tradeCaptureReport.LastQty = new LastQty(decimal.Parse(lastQty));
            // Tag60
            tradeCaptureReport.TransactTime = new TransactTime(DateTime.UtcNow);


            TradeCaptureReport.NoSidesGroup noSidesGroup = new TradeCaptureReport.NoSidesGroup();
            //tag54
            noSidesGroup.Side = new Side('1');
            tradeCaptureReport.AddGroup(noSidesGroup);

            OrderMassCancelRequest.NoPartyIDsGroup noPartyIDsGroup = new OrderMassCancelRequest.NoPartyIDsGroup();
            //tag448
            noPartyIDsGroup.PartyID = new PartyID(sessionID);
            //tag447
            noPartyIDsGroup.PartyIDSource = new PartyIDSource('D');
            //tag452
            noPartyIDsGroup.PartyRole = new PartyRole(1);
            tradeCaptureReport.AddGroup(noPartyIDsGroup);

            // Tag48
            tradeCaptureReport.SecurityID = new SecurityID(securityID);
            //Tag 22
            tradeCaptureReport.SecurityIDSource = securityIDSource;
            tradeCaptureReport.SecurityExchange = new SecurityExchange("XHKG");
            var result = tradeApp.SendMessage(tradeCaptureReport);
        }

        public void MassCancelAll()
        {
            OrderMassCancelRequest orderMassCancelRequest = new OrderMassCancelRequest();
            var clOrdID = ClOrderIDGen.getNextClOrderID();
            //tag11
            orderMassCancelRequest.ClOrdID = new ClOrdID(clOrdID.ToString());
            //tag530
            orderMassCancelRequest.MassCancelRequestType = new MassCancelRequestType('7');

            OrderMassCancelRequest.NoPartyIDsGroup noPartyIDsGroup = new OrderMassCancelRequest.NoPartyIDsGroup();
            //tag448
            noPartyIDsGroup.PartyID = new PartyID(sessionID);
            //tag447
            noPartyIDsGroup.PartyIDSource = new PartyIDSource('D');
            //tag452
            noPartyIDsGroup.PartyRole = new PartyRole(1);
            orderMassCancelRequest.AddGroup(noPartyIDsGroup);

            // Tag60
            orderMassCancelRequest.TransactTime = new TransactTime(DateTime.UtcNow);
            var result = tradeApp.SendMessage(orderMassCancelRequest);
        }

        public void MassCancelSecurityID(NetInfo netInfo)
        {
            OrderMassCancelRequest orderMassCancelRequest = new OrderMassCancelRequest();
            var clOrdID = ClOrderIDGen.getNextClOrderID();
            //tag11
            orderMassCancelRequest.ClOrdID = new ClOrdID(clOrdID.ToString());
            //tag530
            orderMassCancelRequest.MassCancelRequestType = new MassCancelRequestType('1');

            OrderMassCancelRequest.NoPartyIDsGroup noPartyIDsGroup = new OrderMassCancelRequest.NoPartyIDsGroup();
            //tag448
            noPartyIDsGroup.PartyID = new PartyID(sessionID);
            //tag447
            noPartyIDsGroup.PartyIDSource = new PartyIDSource('D');
            //tag452
            noPartyIDsGroup.PartyRole = new PartyRole(1);
            orderMassCancelRequest.AddGroup(noPartyIDsGroup);

            CancelInfo cancelInfo = new CancelInfo();
            cancelInfo.MyReadString(netInfo.infoT);
            HKCodeBean codeBean = CodeTransfer_HKEX.getUpperCodeInfoByZDCode(cancelInfo.code, cancelInfo.exchangeCode);
            // Tag48
            orderMassCancelRequest.SecurityID = new SecurityID(codeBean.upperProduct);
            //Tag 22
            orderMassCancelRequest.SecurityIDSource = securityIDSource;
            //Tag 207
            orderMassCancelRequest.SecurityExchange = securityExchange;
            // Tag60
            orderMassCancelRequest.TransactTime = new TransactTime(DateTime.UtcNow);
            var result = tradeApp.SendMessage(orderMassCancelRequest);
        }

        public void MassCancelSegment()
        {
            OrderMassCancelRequest orderMassCancelRequest = new OrderMassCancelRequest();
            var clOrdID = ClOrderIDGen.getNextClOrderID();
            //tag11
            orderMassCancelRequest.ClOrdID = new ClOrdID(clOrdID.ToString());
            //tag530
            orderMassCancelRequest.MassCancelRequestType = new MassCancelRequestType('9');

            OrderMassCancelRequest.NoPartyIDsGroup noPartyIDsGroup = new OrderMassCancelRequest.NoPartyIDsGroup();
            //tag448
            noPartyIDsGroup.PartyID = new PartyID(sessionID);
            //tag447
            noPartyIDsGroup.PartyIDSource = new PartyIDSource('D');
            //tag452
            noPartyIDsGroup.PartyRole = new PartyRole(1);
            orderMassCancelRequest.AddGroup(noPartyIDsGroup);
            //tag1300
            orderMassCancelRequest.MarketSegmentID = new MarketSegmentID("MAIN");
            // Tag60
            orderMassCancelRequest.TransactTime = new TransactTime(DateTime.UtcNow);
            var result = tradeApp.SendMessage(orderMassCancelRequest);
        }
        #endregion
    }


}