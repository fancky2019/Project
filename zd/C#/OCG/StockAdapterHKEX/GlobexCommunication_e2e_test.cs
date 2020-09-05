﻿using System;
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
        private ZDLogger globexIFLogger = null;

        private string sessionID = null;

        public string ConfigFile { get; set; }
        private CfgManager cfgInstance;

        /// <summary>
        /// Mapping between ClOrdID and ZD.accountNo + ZD.systemNo
        /// </summary>
        private ConcurrentDictionary<long, RefObj> xReference = new ConcurrentDictionary<long, RefObj>();
        // Key: upper orderID
        private ConcurrentDictionary<string, RefObj> downReference = new ConcurrentDictionary<string, RefObj>();

        public static ConcurrentDictionary<string, long> specialReference = new ConcurrentDictionary<string, long>();

        public Dictionary<string, uint> cmeProductCdToSecuIdDict = new Dictionary<string, uint>();


        private Dictionary<string, int> OrderTypeIdx = null;
        private List<TimeSpan> timeList = null;
        //private char[][] newOrderMatrix = null;
        private char[,] newOrderMatrix = new char[12, 2] { { 'P', 'N' }, { 'Y', 'N' }, { 'N', 'P' }, { 'N', 'P' }, { 'N', 'Y' }, { 'N', 'P' }, { 'N', 'P' }, { 'N', 'Y' }, { 'P', 'N' }, { 'Y', 'N' }, { 'Y', 'N' }, { 'Y', 'N' } };
        private char[,] modifyOrderMatrix = new char[12, 2] { { '-', '-' }, { 'Y', '-' }, { 'N', '-' }, { 'P', '-' }, { 'Y', 'Y' }, { 'P', 'P' }, { 'P', 'P' }, { 'Y', 'Y' }, { 'P', 'P' }, { 'Y', 'Y' }, { 'N', 'N' }, { 'N', 'N' } };
        private char[,] cancelOrderMatrix = new char[12, 2] { { '-', '-' }, { 'Y', '-' }, { 'N', '-' }, { 'P', '-' }, { 'Y', 'Y' }, { 'P', 'P' }, { 'Y', 'Y' }, { 'Y', 'Y' }, { 'P', 'P' }, { 'Y', 'Y' }, { 'N', 'N' }, { 'N', 'N' } };


        private TimeSpan offsetTS = new TimeSpan(0, 0, 0);

        private int messageThrottle = 2;
        private BlockingCollection<NetInfo> sendQueue;
        private BlockingCollection<NetInfo> failureQueue;

        public void init(ICustomFixStrategy strategy, bool isTestMode)
        {
            globexIFLogger = ZDLoggerFactory.getSynWriteLogger("GlobexIF.log");
            globexIFLogger.setLogLevel(ZDLogger.LVL_TRACE);
            //CodeTransfer_TT.errLogger = globexIFLogger;

            this.strategy = strategy;
            this.isTestMode = isTestMode;
            cfgInstance = CfgManager.getInstance("StockAdapterHKEX.exe");
            sessionID = cfgInstance.SessionAndPsw.Split(',')[0];

            sendQueue = new BlockingCollection<NetInfo>();
            failureQueue = new BlockingCollection<NetInfo>();

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
                    globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
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
            timeList.Add(new TimeSpan(16, 30, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(16, 31, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(16, 45, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(16, 55, 0).Add(offsetTS));
            timeList.Add(new TimeSpan(17, 00, 0).Add(offsetTS));


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
                ed4.timeSpan = new TimeSpan(16, 31, 0).Add(offsetTS);
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
                ed8.timeSpan = new TimeSpan(16, 31, 1).Add(offsetTS);
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
                ed11.timeSpan = new TimeSpan(16, 31, 2).Add(offsetTS);
                ed11.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed11);


                //定时清理xReference和downReference
                AuthCommon.EventDetail ed20 = new AuthCommon.EventDetail();
                ed20.eventID = 100;
                ed20.timeSpan = new TimeSpan(17, 10, 0).Add(offsetTS);
                ed20.eventHandler = timerEventHandler;
                eventTrigger.registerEvent(ed20);

                eventTrigger.ready();
            }
            catch (Exception ex)
            {
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
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

                globexIFLogger.log(ZDLogger.LVL_CRITCAL, "Triggered event: " + eventID);

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
                                sendQueue.TryAdd(netInfo);
                                globexIFLogger.log(ZDLogger.LVL_ERROR, "== add send queue timer new order, systemCode=" + netInfo.systemCode);
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
                                    sendQueue.TryAdd(netInfo);
                                    globexIFLogger.log(ZDLogger.LVL_ERROR, "== add send queue timer replace order, systemCode=" + netInfo.systemCode);
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
                                    sendQueue.TryAdd(netInfo);
                                    globexIFLogger.log(ZDLogger.LVL_ERROR, "== add send queue timer cancel order, systemCode=" + netInfo.systemCode);
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
                }
            }
            catch (Exception ex)
            {
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }
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
            sendQueue.CompleteAdding();
            failureQueue.CompleteAdding();
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
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }

            try
            {
                cfgInstance.ClOrderID = ClOrderIDGen.getNextClOrderID().ToString();
                cfgInstance.save();
            }
            catch (Exception ex)
            {
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }
        }


        public void onLogonEvent(object sender, EventArgs e)
        {
            //登陆成功后请求throttle
            throttleEntitlement();

            //启动控制一秒时间下单量的线程，防止超出throttle.
            Thread throttleThread = new Thread(new ThreadStart(throttleSendMsg));
            throttleThread.IsBackground = true;
            throttleThread.Start();
        }

        public void persistDayRefObj()
        {
            try
            {
                NonGTCOrderMgr.persistNonGTCOrder();
            }
            catch (Exception ex)
            {
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
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
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
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
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }
        }

        public void UserResponseHandler(object sender, UserResponseEventArgs e)
        {
            QuickFix.FIX50.UserResponse userResp = e.UserResponseMsg;

            QuickFix.FIX50.UserResponse.NoThrottlesGroup noThrottlesGroup = new QuickFix.FIX50.UserResponse.NoThrottlesGroup();
            int noThrottles = userResp.NoThrottles.getValue();

            userResp.GetGroup(noThrottles, noThrottlesGroup);
            
            messageThrottle = noThrottlesGroup.ThrottleNoMsgs.getValue();

            globexIFLogger.log(ZDLogger.LVL_ERROR, "-------messageThrottle------- is " + messageThrottle);
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
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
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
                globexIFLogger.log(ZDLogger.LVL_CRITCAL, "replyCancelReject() -> not find refOBJ for orderID:" + cancelRejMsg.OrderID.getValue());
                globexIFLogger.log(ZDLogger.LVL_CRITCAL, cancelRejMsg.ToString());
                return null;
            }
            else
            {
                //在改单时会将downReference中的此OrderID remove掉。如果失败了就在加回去
                string orderID = cancelRejMsg.OrderID.getValue();
                if (!downReference.ContainsKey(orderID))
                {
                    downReference.TryAdd(orderID, refObj);
                    globexIFLogger.log(ZDLogger.LVL_CRITCAL, "readd order id replyCancelReject " + orderID);
                }

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
                obj = new NetInfo();

                /*In case the BusinessRejectReason (380) = 8
                (Throttle Limit Exceeded), the Text (58) field will
                indicate the remaining throttle interval time in
                milliseconds.*/
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
                        globexIFLogger.log(ZDLogger.LVL_CRITCAL, "replyBusinessMessageReject " + clOrdID + ",fromClient= " + refObj.fromClient.Count);
                        refObj.addGlobexRes(new QuickFix.FIX50.OrderCancelReject());
                        globexIFLogger.log(ZDLogger.LVL_CRITCAL, "replyBusinessMessageReject " + clOrdID + ",fromClient= " + refObj.fromClient.Count);
                    }

                    //如果发送超过限制就加入失败的队列。到下一个时间窗口重新发送
                    failureQueue.TryAdd(refObj.lastSendInfo);

                    xReference.TryRemove(clOrdID, out refObj);
                    //重新发送的单子不返回状态给交易
                    return null;
                }
                else
                    obj.errorMsg = "BusinessRejectReason(Tag380):" + removeIlleagalChar(execReport.Text.getValue());

                obj.infoT = "";
                obj.exchangeCode = refObj.strArray[5];// OrderRouterID
                obj.errorCode = ErrorCode.ERR_ORDER_0004;
                obj.code = Command.OrderStockHK; // CommandCode.ORDER;
                obj.accountNo = refObj.strArray[0];
                obj.systemCode = refObj.strArray[1];
                obj.todayCanUse = refObj.strArray[2]; //execReport.Header.GetField(Tags.SenderSubID);
                obj.clientNo = refObj.strArray[2];

                string reqNo;
                if (requestNoDict.TryRemove(refObj.clOrderID, out reqNo))
                    obj.RequestNo = reqNo;

                TradeServerFacade.SendString(obj);

                xReference.TryRemove(long.Parse(refObj.clOrderID), out refObj);
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
            globexIFLogger.log(ZDLogger.LVL_CRITCAL, "readd order id replyOrderCreation " + OrderID);

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
                        globexIFLogger.log(ZDLogger.LVL_CRITCAL, "Discard fill modification(tag20=2):");
                        globexIFLogger.log(ZDLogger.LVL_CRITCAL, execReport.ToString());
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

                //系统号
                RefObj refObj;
                bool ret = downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
                if (!ret)
                {
                    globexIFLogger.log(ZDLogger.LVL_CRITCAL, "replyFill downReference no order id " + execReport.OrderID.getValue());
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
                if (execReport.LeavesQty.getValue() == 0)
                {
                    xReference.TryRemove(long.Parse(info.orderNo), out refObj);
                    downReference.TryRemove(refObj.orderID, out refObj);
                    globexIFLogger.log(ZDLogger.LVL_CRITCAL, "downReference tryRemove in replyFill " + refObj.orderID);
                }

                return obj;
            }
            catch (Exception e)
            {
                globexIFLogger.log(ZDLogger.LVL_CRITCAL, e.ToString());
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

            xReference.TryRemove(long.Parse(info.orderNo), out refObj);
            downReference.TryRemove(refObj.orderID, out refObj);
            globexIFLogger.log(ZDLogger.LVL_CRITCAL, "downReference tryRemove in replyCancelled " + refObj.orderID);

            return obj;
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

            return obj;
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

            //add by xiang at 20171011 -begin
            downReference.TryAdd(execReport.OrderID.getValue(), refObj);
            globexIFLogger.log(ZDLogger.LVL_TRACE, "add new order id in replyReplaced " + execReport.OrderID.getValue());
            refObj.orderID = execReport.OrderID.getValue();
            //add by xiang at 20171011 -end

            info.orderNo = refObj.clOrderID;
            info.origOrderNo = info.orderNo;

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
                globexIFLogger.log(ZDLogger.LVL_CRITCAL, "downReference tryRemove in doExpired " + refObj.orderID);
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
                globexIFLogger.log(ZDLogger.LVL_ERROR, "Queue new order, systemCode=" + obj.systemCode);
            }
        }


        //订单状态（1：已请求；2：已排队；3：部分成交；4：完全成交；5：已撤余单；6：已撤单；7：指令失败；8：待送出；9：待更改；10：待撤单）

        private const string ZD_PENDING_SEND_OUT = "8";
        private const string ZD_PENDING_MODIFY = "9";
        private const string ZD_PENDING_CANCEL = "A";
        private const string ZD_CANCELED = "6";


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
            int caseNum = -1;

            int orderTypeIdx = -1;
            if (OrderTypeIdx.TryGetValue(info.priceType, out orderTypeIdx))
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
                    sendQueue.TryAdd(obj);
                    globexIFLogger.log(ZDLogger.LVL_ERROR, "== add send queue new order, systemCode=" + obj.systemCode);
                    //add by xiang at 20170911 -end
                }
                else
                    caseNum = 0;
            }
            else
            {
                caseNum = 0;
            }


            // <summary>
            // 下单失败：不支持的下单类型
            // </summary>
            //public static string ERR_ORDER_0035 = "20035";

            if (caseNum == 0)
            {
                TradeServerFacade.writeLog(LogLevel.IMPORTANT, "PlaceOrder() fail!");
                obj.errorCode = ErrorCode.ERR_ORDER_0035;
                obj.code = Command.OrderStockHK;
                obj.infoT = "";

                TradeServerFacade.SendString(obj);
            }
        }


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

                // Tag55
                newOrderSingle.SecurityID = new SecurityID(codeBean.upperProduct);
                newOrderSingle.SecurityIDSource = securityIDSource;
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
                newOrderSingle.TimeInForce = QueryTimeInForce(info.priceType);
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
                    TradeServerFacade.writeLog(LogLevel.IMPORTANT, "PlaceOrder() fail!");
                    obj.infoT = "";  // ErrorCode.ERR_ORDER_0000_MSG;  //can not include Chinese character
                    obj.errorCode = ErrorCode.ERR_ORDER_0000;
                    obj.code = Command.OrderStockHK; //CommandCode.ORDER;
                    obj.errorMsg = "Place order fail";

                    TradeServerFacade.SendString(obj);
                }
                else
                {
                    //AuditTrailMgr.addMsg(newOrderSingle, obj);
                }
            }
            catch (Exception ex)
            {
                TradeServerFacade.writeLog(LogLevel.SYSTEMERROR, ex.ToString());
            }
        }

        private ConcurrentDictionary<string, NetInfo> pendingCancelOrdersDict = new ConcurrentDictionary<string, NetInfo>();
        private void addToCacnelOrderPendingQueue(NetInfo obj)
        {
            lock (pendingCancelOrdersDict)
            {
                pendingCancelOrdersDict.TryAdd(obj.systemCode, obj);
                globexIFLogger.log(ZDLogger.LVL_ERROR, "Queue cancel order, systemCode=" + obj.systemCode);
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

                int caseNum = -1;

                int orderTypeIdx = -1;
                if (OrderTypeIdx.TryGetValue(info.priceType, out orderTypeIdx))
                {
                    int timeDurIdx = getCurrentTimeDurIdx();
                    if (timeDurIdx == -1) return;


                    char flag = cancelOrderMatrix[timeDurIdx, orderTypeIdx];
                    if (flag == 'P')
                    {
                        addToCacnelOrderPendingQueue(obj);

                        //Todo...
                        string replyState = ZD_PENDING_CANCEL; //waiting for canceling
                        sendCancelReponseInfo_ForCancel(obj, replyState);
                    }
                    else if (flag == 'Y')
                        caseNum = 0;
                    else if (flag == 'N')
                        caseNum = 1;
                }


                if (caseNum == 0)
                {
                    long clOrdID = Convert.ToInt64(info.orderNo);
                    RefObj refObj;
                    bool ret = xReference.TryGetValue(clOrdID, out refObj);
                    if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_REPLACE)
                    {
                        //cancelOrderWork(obj);
                        //add by xiang at 20170911 -begin
                        sendQueue.TryAdd(obj);
                        globexIFLogger.log(ZDLogger.LVL_ERROR, "== add send queue cancel order, systemCode=" + obj.systemCode);
                        //add by xiang at 20170911 -end
                    }
                }
                else if (caseNum == 1)
                {
                    sendCancelReponseInfo_ForCancel_Fail(obj, null);
                }


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
                TradeServerFacade.writeLog(LogLevel.SYSTEMERROR, ex.ToString());
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
                    refObj.strArray[7] = orderCancelRequest.OrigClOrdID.getValue() + ":" + refObj.clOrderID;
                    //add by xiang at 20170911 -begin
                    //保存最后一次发送的netinfo,如果超过throttle限制就重发一遍
                    refObj.lastSendInfo = obj;
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
                        CancelResponseInfo cinfo = new CancelResponseInfo();
                        cinfo.orderNo = info.orderNo;

                        obj.infoT = cinfo.MyToString();
                        obj.exchangeCode = info.exchangeCode;
                        obj.accountNo = info.accountNo;
                        obj.systemCode = info.systemNo;
                        obj.errorCode = ErrorCode.ERR_ORDER_0014;
                        obj.code = Command.CancelStockHK;  // CommandCode.CANCELCAST;
                        obj.errorMsg = "Cancel order fail";

                        TradeServerFacade.SendString(obj);
                    }
                    else
                    {
                        //AuditTrailMgr.addMsg(cancelMsg, obj);
                        refObj.addClientReq(orderCancelRequest);
                        xReference.TryAdd(clOrdID, refObj);
                    }
                }
                else
                {
                    TradeServerFacade.writeLog(LogLevel.IMPORTANT, "cancelOrderWork: Cannot find XReference. clOrderID: " + newOrderClOrdID);
                }

            }
            catch (Exception ex)
            {
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
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
                    sendQueue.TryAdd(obj);
                    globexIFLogger.log(ZDLogger.LVL_ERROR, "== add send queue doCancelTask, systemCode=" + obj.systemCode);
                    //add by xiang at 20170911 -end
                }
            }
            catch (Exception ex)
            {
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }
        }

        private ConcurrentDictionary<string, NetInfo> pendingModifyOrdersDict = new ConcurrentDictionary<string, NetInfo>();
        private void addToModifyOrderPendingQueue(NetInfo obj)
        {
            lock (pendingModifyOrdersDict)
            {
                pendingModifyOrdersDict.TryAdd(obj.systemCode, obj);
                globexIFLogger.log(ZDLogger.LVL_ERROR, "Queue modify order, systemCode=" + obj.systemCode);
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


                int caseNum = -1;

                int orderTypeIdx = -1;
                if (OrderTypeIdx.TryGetValue(info.priceType, out orderTypeIdx))
                {
                    int timeDurIdx = getCurrentTimeDurIdx();
                    if (timeDurIdx == -1) return;


                    char flag = modifyOrderMatrix[timeDurIdx, orderTypeIdx];
                    if (flag == 'P')
                    {
                        addToModifyOrderPendingQueue(obj);

                        //Todo...
                        string replyState = ZD_PENDING_MODIFY; //waiting for modifying
                        sendOrderResonseInfo_ForModify_onModifyOrder(obj, info.orderNo, replyState);
                    }
                    else if (flag == 'Y')
                        caseNum = 0;
                    else if (flag == 'N')
                        caseNum = 1;
                }


                if (caseNum == 0)
                {
                    long clOrdID = Convert.ToInt64(info.orderNo);
                    RefObj refObj;
                    bool ret = xReference.TryGetValue(clOrdID, out refObj);
                    if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_CANCELREPLACE)
                    {
                        //cancelReplaceOrderWork(obj);

                        //add by xiang at 20170911 -begin
                        sendQueue.TryAdd(obj);
                        globexIFLogger.log(ZDLogger.LVL_ERROR, "== add send queue replace order, systemCode=" + obj.systemCode);
                        //add by xiang at 20170911 -end
                    }
                }
                else if (caseNum == 1)
                {
                    OrderResponseInfo minfo = new OrderResponseInfo();
                    minfo.orderNo = info.orderNo;

                    obj.infoT = minfo.MyToString();
                    obj.exchangeCode = info.exchangeCode;
                    obj.accountNo = info.accountNo;
                    obj.errorCode = ErrorCode.ERR_ORDER_0016;
                    obj.code = Command.ModifyStockHK; //CommandCode.MODIFY;

                    TradeServerFacade.SendString(obj);
                }

            }
            catch (Exception ex)
            {
                TradeServerFacade.writeLog(LogLevel.SYSTEMERROR, ex.ToString());
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
                    refObj.strArray[7] = lastClOrderId + ":" + refObj.clOrderID;
                    //add by xiang at 20170911 -begin
                    //保存最后一次发送的netinfo,如果超过throttle限制就重发一遍
                    refObj.lastSendInfo = obj;
                    //add by xiang at 20170911 -end

                    //add by xiang at 20171011 -begin
                    RefObj temp;
                    downReference.TryRemove(ocrr.OrderID.getValue(), out temp);
                    globexIFLogger.log(ZDLogger.LVL_CRITCAL, "remove order id cancelReplaceOrderWork " + ocrr.OrderID.getValue());
                    //add by xiang at 20171011 -end

                    ocrr.SecurityID = new SecurityID(codeBean.upperProduct);
                    ocrr.SecurityIDSource = securityIDSource;
                    ocrr.SecurityExchange = securityExchange;

                    //Added by Rainer on 20150609 -begin
                    ocrr.TimeInForce = newOrderSingle.TimeInForce;
                    //Added by Rainer on 20150609 -end

                    ocrr.Side = newOrderSingle.Side;

                    // Tag60
                    ocrr.TransactTime = new TransactTime(DateTime.UtcNow);

                    ocrr.OrderQty = new OrderQty(Convert.ToDecimal(info.modifyNumber));

                    OrdType ordType = newOrderSingle.OrdType;
                    ocrr.OrdType = ordType;

                    char charOrderTpe = ordType.getValue();

                    if (charOrderTpe == OrdType.LIMIT)
                    {
                        decimal prx = decimal.Parse(info.modifyPrice); // CodeTransfer_TT.toGlobexPrx(info.modifyPrice, strSymbol);
                        ocrr.Price = new Price(prx);
                    }

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
                        OrderResponseInfo minfo = new OrderResponseInfo();
                        minfo.orderNo = info.orderNo;

                        obj.infoT = minfo.MyToString();
                        obj.exchangeCode = info.exchangeCode;
                        obj.accountNo = info.accountNo;
                        obj.errorCode = ErrorCode.ERR_ORDER_0016;
                        obj.code = Command.ModifyStockHK; //CommandCode.MODIFY;
                        obj.errorMsg = "Modify order fail";

                        TradeServerFacade.SendString(obj);
                    }
                    else
                    {
                        //AuditTrailMgr.addMsg(ocrr, obj);
                        refObj.addClientReq(ocrr);
                        xReference.TryAdd(clOrdID, refObj);
                    }
                }
                else
                {
                    TradeServerFacade.writeLog(LogLevel.IMPORTANT, "cancelReplaceOrderWork: Cannot find XReference. ClOrderID: " + newOrderClOrdID);
                }

            }
            catch (Exception ex)
            {
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
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
                    sendQueue.TryAdd(obj);
                    globexIFLogger.log(ZDLogger.LVL_ERROR, "== add send queue doCancelRepalceTask, systemCode=" + obj.systemCode);
                    //add by xiang at 20170911 -end
                }
            }
            catch (Exception ex)
            {
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
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

                default: throw new Exception("unsupported input");
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

                case "8": c = TimeInForce.IMMEDIATE_OR_CANCEL;
                    break;

                default: throw new Exception("unsupported input");
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


        private TimeSpan ts1 = new TimeSpan(8, 29, 59); //.Add(offsetTS);
        private TimeSpan ts2 = new TimeSpan(17, 00, 0); //.Add(offsetTS);

        public void doOrder(NetInfo netInfo)
        {
            try
            {
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

                TimeSpan dtNow = DateTime.Now.TimeOfDay;
                if (dtNow.Ticks < ts1.Ticks || dtNow.Ticks > ts2.Ticks)
                {
                    TradeServerFacade.writeLog(LogLevel.IMPORTANT, "do order fail!");
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


                // For development test -begin
                //netInfo.accountNo = "000040";
                // For development test -end

                if (netInfo.code == Command.OrderStockHK) //== CommandCode.ORDER)
                {
                    PlaceOrder(netInfo);
                }
                else if (netInfo.code == Command.ModifyStockHK) //CommandCode.MODIFY)
                {
                    CancelReplaceOrder(netInfo);
                }
                else if (netInfo.code == Command.CancelStockHK) //CommandCode.CANCEL)
                {
                    CancelOrder(netInfo);
                }
            }
            catch (Exception ex)
            {
                globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
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

        private void throttleSendMsg()
        {
            try
            {
                bool result;
                NetInfo netInfo;

                while (true)
                {
                    try
                    {
                        for (int i = 0; i < messageThrottle; i++)
                        {
                            if (failureQueue.Count > 0)
                            {
                                result = failureQueue.TryTake(out netInfo, 100);
                                if (result)
                                {
                                    if (netInfo != null)
                                    {
                                        sendMsg(netInfo);
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
                                }
                            }
                        }

                        Thread.Sleep(1000);

                        //判断退出标记
                        if (sendQueue.IsCompleted || failureQueue.IsCompleted)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        globexIFLogger.log(ZDLogger.LVL_CRITCAL, ex.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                globexIFLogger.log(ZDLogger.LVL_CRITCAL, e.ToString());
            }
        }
        private void sendMsg(NetInfo netInfo)
        {
            try
            {
                if (netInfo.code == Command.OrderStockHK) //== CommandCode.ORDER)
                {
                    OrderInfo info = new OrderInfo();
                    info.MyReadString(netInfo.infoT);

                    PlaceOrder(netInfo, info);
                    globexIFLogger.log(ZDLogger.LVL_ERROR, "== Send queue new order, systemCode=" + netInfo.systemCode);
                }
                else if (netInfo.code == Command.ModifyStockHK) //CommandCode.MODIFY)
                {
                    cancelReplaceOrderWork(netInfo);
                    globexIFLogger.log(ZDLogger.LVL_ERROR, "== Send queue replace order, systemCode=" + netInfo.systemCode);
                }
                else if (netInfo.code == Command.CancelStockHK) //CommandCode.CANCEL)
                {
                    cancelOrderWork(netInfo);
                    globexIFLogger.log(ZDLogger.LVL_ERROR, "== Send queue cancel order, systemCode=" + netInfo.systemCode);
                }
            }
            catch (Exception e)
            {
                globexIFLogger.log(ZDLogger.LVL_CRITCAL, e.Message + e.Source + e.StackTrace);
            }
        }

    }

    public class RefObj
    {
        public const char ORD_INIT_STATUS = (char)0;
        public bool isPendingRequest = false;
        public volatile char orderStatus = ORD_INIT_STATUS;
        public string[] strArray_;
        public QuickFix.FIX50.NewOrderSingle newOrderSingle;
        public List<QuickFix.Message> fromClient = new List<QuickFix.Message>();
        public List<QuickFix.FIX50.ExecutionReport> fromGlobex = new List<QuickFix.FIX50.ExecutionReport>();
        public string orderID = null;
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