using CommonClassLib;
using QuickFix.Fields;
using QuickFix.FIX42;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using TTMarketAdapter;
using TTMarketAdapter.Model;
using TTMarketAdapter.Utilities;
using static QuickFix.FIX42.NewOrderSingle;

namespace ZDTradeClientTT
{
    public class TTCommunication
    {

        public TradeApp tradeApp = null;
        private ManualOrderIndicator moi = new ManualOrderIndicator(true);

        private bool IsTestMode;
        //public ICustomFixStrategy strategy = null;
        // private ZDLogger globexIFLogger = null;
        private Action _logon = null;
        private Action _logonOut = null;
        private bool _securityDefinitionFileExist = false;
        //private string _secu_File = @"config\TT_Secu.dat";
        //private const string _secu_OPT_FILE = @"config\TT_Secu_Opt.dat";
        //public const string ConfigFile= @"config\Quickfix_ZDTradeClient.cfg";

        //兼容壳不报错
        public string ConfigFile { get; set; }

        //public const string TTConfig = @"config\Quickfix_ZDTradeClientTT.cfg";
        /// <summary>
        /// Mapping between ClOrdID and ZD.accountNo + ZD.systemNo  客户端下单的时候添加，成交、撤单、拒绝的时候会移除
        /// </summary>
        private ConcurrentDictionary<long, RefObj> _xReference = new ConcurrentDictionary<long, RefObj>();
        /// <summary>
        /// pending 的时候添加，撤单、成交时候移除
        /// </summary>
        // Key: upper orderID
        private ConcurrentDictionary<string, RefObj> _downReference = new ConcurrentDictionary<string, RefObj>();

        //public Dictionary<string, string> fftDic = new Dictionary<string, string>();

        private BlockingCollection<QuickFix.FIX42.ExecutionReport> _execReportBC = null;

        /// <summary>
        /// 兼容rainer的壳
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="isTestMode"></param>

        public void init(ICustomFixStrategy strategy, bool isTestMode)
        {
            init(isTestMode);
        }
        /// <summary>
        /// rainer 壳调用
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="isTestMode"></param>
        public void init(bool isTestMode)
        {
            try
            {
                if (!string.IsNullOrEmpty(ZDTradeClientTTConfiurations.Gate_FUT_IP) &&!string.IsNullOrEmpty(ZDTradeClientTTConfiurations.Gate_FUT_Port))
                {
                    //获取T+1时间
                    TPlusOneHelper.GetTPlusOneData();
                }
          



                //CodeTransfer_TT.initPrxFactor(false);

                //globexIFLogger = ZDLoggerFactory.getSynWriteLogger("GlobexIF.log");
                //globexIFLogger.setLogLevel(ZDLogger.LVL_TRACE);
                //  CodeTransfer_TT.errLogger = Logger.ErrorLogger;

                //this.strategy = strategy;
                this.IsTestMode = isTestMode;
                //ZDTradeClientTT.CfgManager cfgInstance = ZDTradeClientTT.CfgManager.getInstance("ZDTradeClientTT.exe");



                //string setString = cfgInstance.OtherSettings;
                //if (!string.IsNullOrEmpty(setString))
                //{
                //    string[] itmesArr = setString.Split(';');
                //    foreach (string oneItem in itmesArr)
                //    {
                //        string[] itemArr = oneItem.Split(':');
                //        fftDic.Add(itemArr[0], itemArr[1]);
                //    }
                //}
                //

                //if (isTestMode)
                //    CodeTransfer_CME.init4Test();
                //else
                //{
                //    CodeTransfer_CME.initCommuSvrSecurity(@"config\ZD_secdef.dat");
                //}


                // FIX app settings and related
                QuickFix.SessionSettings settings = new QuickFix.SessionSettings(ZDTradeClientTTConfiurations.QuickFixConfig);
                //strategy.SessionSettings = settings;

                _execReportBC = new BlockingCollection<QuickFix.FIX42.ExecutionReport>();

                // FIX application setup
                /* old FIX version
                QuickFix.MessageStoreFactory storeFactory = new QuickFix.FileStoreFactory(settings);
                QuickFix.LogFactory logFactory = new QuickFix.FileLogFactory(settings);
                tradeApp = new TradeApp(settings, strategy, execReportBC);

                QuickFix.IInitiator initiator = new QuickFix.Transport.SocketInitiator(tradeApp, storeFactory, settings, logFactory);
                tradeApp.Initiator = initiator;
                */

                // FIX application setup
                QuickFix.IMessageStoreFactory storeFactory = new QuickFix.FileStoreFactory(settings);
                QuickFix.ILogFactory logFactory = new QuickFix.FileLogFactory(settings);
                tradeApp = new TradeApp(settings, _execReportBC);


                QuickFix.IInitiator initiator = new QuickFix.Transport.SocketInitiator(tradeApp, storeFactory, settings, logFactory);
                tradeApp.Initiator = initiator;

                /*
                EventHandler<ExectutionEventArgs> execReportHandler = new EventHandler<ExectutionEventArgs>(onExecReportEvent);
                tradeApp.ExecutionEvent += execReportHandler;
                */

                tradeApp.OrderCacnelRejectEvent += OrderCacnelRejectHandler;

                EventHandler<EventArgs> logonEventHandler = new EventHandler<EventArgs>(onLogonEvent);
                tradeApp.LogonEvent += logonEventHandler;
                tradeApp.LogoutEvent += TradeApp_LogoutEvent;


                GTCOrderMgr.loadGTCOrder(ZDTradeClientTTConfiurations.GTCOrderFile, _xReference, _downReference);
                NonGTCOrderMgr.loadNonGTCOrder(ZDTradeClientTTConfiurations.DayOrderFile, _xReference, _downReference);

                ClOrderIDGen.setXRef(_xReference);
                //UnexpectedExceptionHandler.globexCommunication = this;


                //load期货数据
                if (File.Exists(ZDTradeClientTTConfiurations.SecurityDefinitionFuture))
                {
                    _securityDefinitionFileExist = true;
                    initInstrumentFromFile();

                    ////方法内要用到初始化合约数据，放在初始化合约之后执行
                    //CodeTransfer_TT.initPrxFactor();
                }
                else
                {
                    CodeTransfer_TT.initPrxFactor();
                    //  Logger.Info("Can not find securitydefinition contract file！");
                }
                ////load期权数据
                //if (File.Exists(SECU_OPT_FILE))
                //{
                //    initInstrumentFromFile_opt();
                //}

                ////方法内要用到初始化合约数据，放在初始化合约之后执行
                //CodeTransfer_TT.initPrxFactor();
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        private void TradeApp_LogoutEvent(object sender, EventArgs e)
        {
            _logonOut?.Invoke();
        }



        private void onLogonEvent(object sender, EventArgs e)
        {
            this._logon?.Invoke();

            //fancky  注释
            //if (!isInsturmentDownload)
            //{
            //    globexIFLogger.log(ZDLogger.LVL_CRITCAL, "init instrument from download data.");
            //    //downloadInstrument();
            //    ThreadPool.QueueUserWorkItem(new WaitCallback(downloadInstrument));
            //    isInsturmentDownload = true;
            //}

        }


        public void initOneInstrument()
        {

            string instrumentStr = "8=FIX.4.2|9=00318|35=d|49=TTDS68O|56=DAIFLDTS|34=77|52=20140820-06:03:28.594|55=AH|48=AHFCEPSR3M|10455=AH 3M|167=FUT|207=LME|15=USD|320=2|322=2:0|107=Primary high grade aluminium|18207=LME Pri Alum.|200=201411|205=20|16451=0|393=211|323=4|16452=0.25|16454=25|16552=0.25|16554=25|16456=0|146=0|18206=1|18203=LME|864=1|865=5|866=20141120|10=158|";
            string realIntruementStr = instrumentStr.Replace('|', (char)1);

            QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            dd.Load(ZDTradeClientTTConfiurations.FIX42);

            /*
            string exeReportStr = "8=FIX.4.2|9=00352|35=8|49=TTDS68O|56=DAIFLDTS|50=G|57=001016|34=6664|52=20140827-06:41:04.444|55=PB|48=PBFCEPSR3M|207=LME|1=00740|16102=00740|16103=00740|11=8000061|18203=LME|37=NONE|17=0G4TB2012|58=Insufficient permission to route order on this product.|32=0|103=99|151=0|14=0|54=1|40=2|59=0|11028=Y|150=8|20=0|39=8|44=2247|38=1|31=0|6=0|60=20140827-06:41:04.444|146=0|10=012|";
            string realExeRportStr = exeReportStr.Replace('|', (char)1);
            QuickFix.FIX42.ExecutionReport er = new ExecutionReport();
            er.FromString(realIntruementStr, false, null, dd);
            dd.Validate(er, "FIX.4.2", "8");
             */

            QuickFix.FIX42.SecurityDefinition secDef = new QuickFix.FIX42.SecurityDefinition();
            secDef.FromString(realIntruementStr, false, null, dd); //null); //dd);

            List<QuickFix.FIX42.SecurityDefinition> secuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            secuDefList.Add(secDef);

            //secDef.SetField(new DisplayFactor(100));
            //decimal ddata = secDef.GetDecimal(MyExtTags.DisplayFactor);

            TTMarketAdapter.CodeTransfer_TT.addSecurity("FUT", secuDefList);


            // Tag18203
            QuickFix.Group gr = secDef.GetGroup(1, Tags.NoGateways);
            //QuickFix.FIX42.SecurityDefinition.NoGatewaysGroup sGroup = null; // (QuickFix.FIX42.SecurityDefinition.NoGatewaysGroup)sd.GetGroup(1, Tags.NoGateways); //NoGateways);
            ExchangeGateway eg = new ExchangeGateway(gr.GetField(Tags.ExchangeGateway));
        }

        public void initInstrumentFromFile()
        {
            List<QuickFix.FIX42.SecurityDefinition> futSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            List<QuickFix.FIX42.SecurityDefinition> spreadSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            List<QuickFix.FIX42.SecurityDefinition> optionSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();

            QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            dd.Load(ZDTradeClientTTConfiurations.FIX42);

            //if (File.Exists(_secu_File))
            //{
            ReadFile(ZDTradeClientTTConfiurations.SecurityDefinitionFuture);
            TT.Common.NLogUtility.Info("init instrument from SECU_FILE file data completed.");
            //}

            if (File.Exists(ZDTradeClientTTConfiurations.SecurityDefinitionOption))
            {
                ReadFile(ZDTradeClientTTConfiurations.SecurityDefinitionOption);
                TT.Common.NLogUtility.Info("init instrument from SECU_OPT_FILE file data completed.");
            }

            void ReadFile(string fileName)
            {
                using (StreamReader sReader = new StreamReader(File.Open(fileName, FileMode.Open), System.Text.Encoding.ASCII))
                {
                    while (!sReader.EndOfStream)
                    {
                        string oneLine = sReader.ReadLine().Trim();
                        if (string.IsNullOrEmpty(oneLine))
                            continue;

                        QuickFix.FIX42.SecurityDefinition secDef = new QuickFix.FIX42.SecurityDefinition();
                        secDef.FromString(oneLine, false, null, dd);

                        if (secDef.SecurityType.getValue() == "FUT")
                        {
                            futSecuDefList.Add(secDef);
                        }
                        else if (secDef.SecurityType.getValue() == "MLEG")
                        {
                            spreadSecuDefList.Add(secDef);
                        }
                        else if (secDef.SecurityType.getValue() == "OPT")
                        {
                            optionSecuDefList.Add(secDef);
                        }
                    }
                }
            }

            TTMarketAdapter.CodeTransfer_TT.addSecurity("FUT", futSecuDefList);
            TTMarketAdapter.CodeTransfer_TT.addSecurity("MLEG", spreadSecuDefList);
            TTMarketAdapter.CodeTransfer_TT.addSecurity("OPT", optionSecuDefList);

            #region   fancky  注释 old
            //globexIFLogger.log(ZDLogger.LVL_CRITCAL, "init instrument from file data.");

            //List<QuickFix.FIX42.SecurityDefinition> futSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            //List<QuickFix.FIX42.SecurityDefinition> spreadSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();

            //QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            //dd.Load(@"config\FIX42.xml");

            //using (StreamReader sReader = new StreamReader(File.Open(SECU_FILE, FileMode.Open), System.Text.Encoding.ASCII))
            //{
            //    while (!sReader.EndOfStream)
            //    {
            //        string oneLine = sReader.ReadLine().Trim();
            //        if (string.IsNullOrEmpty(oneLine))
            //            continue;

            //        QuickFix.FIX42.SecurityDefinition secDef = new QuickFix.FIX42.SecurityDefinition();
            //        secDef.FromString(oneLine, false, null, dd);

            //        if (secDef.SecurityType.getValue() == "FUT")
            //            futSecuDefList.Add(secDef);
            //        else if (secDef.SecurityType.getValue() == "MLEG")
            //            spreadSecuDefList.Add(secDef);
            //    }
            //}

            //TTMarketAdapter.CodeTransfer_TT.addSecurity(FUTURES_REQ_BEGIN.ToString(), futSecuDefList);
            //TTMarketAdapter.CodeTransfer_TT.addSecurity(SPREADS_REQ_BEGIN.ToString(), spreadSecuDefList);


            ////load期权数据
            //if (File.Exists(SECU_OPT_FILE))
            //{
            //    //initInstrumentFromFile_opt();
            //    globexIFLogger.log(ZDLogger.LVL_CRITCAL, "init option instrument from file data.");

            //    List<QuickFix.FIX42.SecurityDefinition> optSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();

            //    using (StreamReader sReader = new StreamReader(File.Open(SECU_OPT_FILE, FileMode.Open), System.Text.Encoding.ASCII))
            //    {
            //        while (!sReader.EndOfStream)
            //        {
            //            string oneLine = sReader.ReadLine().Trim();
            //            if (string.IsNullOrEmpty(oneLine))
            //                continue;

            //            QuickFix.FIX42.SecurityDefinition secDef = new QuickFix.FIX42.SecurityDefinition();
            //            secDef.FromString(oneLine, false, null, dd);

            //            if (secDef.SecurityType.getValue() == "OPT")
            //            {
            //                optSecuDefList.Add(secDef);
            //            }
            //        }
            //    }
            //    TTMarketAdapter.CodeTransfer_TT.addSecurity(OPTIONS_REQ_BEGIN.ToString(), optSecuDefList);
            //}
            #endregion

            //方法内要用到初始化合约数据，放在初始化合约之后执行
            CodeTransfer_TT.initPrxFactor();
        }

        //Added on 20180416 -begin
        public void initInstrumentFromFile_opt()
        {

            TT.Common.NLogUtility.Info("init option instrument from file data.");

            List<QuickFix.FIX42.SecurityDefinition> optSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();

            QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            dd.Load(ZDTradeClientTTConfiurations.FIX42);

            using (StreamReader sReader = new StreamReader(File.Open(ZDTradeClientTTConfiurations.SecurityDefinitionOption, FileMode.Open), System.Text.Encoding.ASCII))
            {
                while (!sReader.EndOfStream)
                {
                    string oneLine = sReader.ReadLine().Trim();
                    if (string.IsNullOrEmpty(oneLine))
                        continue;

                    QuickFix.FIX42.SecurityDefinition secDef = new QuickFix.FIX42.SecurityDefinition();
                    secDef.FromString(oneLine, false, null, dd);

                    if (secDef.SecurityType.getValue() == "OPT")
                    {
                        optSecuDefList.Add(secDef);
                    }
                }
            }
            TTMarketAdapter.CodeTransfer_TT.addSecurity("OPT", optSecuDefList);
        }
        //Added on 20180416 -end


        public void connectGlobex()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(execReportHandler));


            if (!IsTestMode)
                tradeApp.Start();

        }
        public void connectGlobex(Action logon)
        {
            this._logon = logon;
            ThreadPool.QueueUserWorkItem(new WaitCallback(execReportHandler));

            //try
            //{
            if (!IsTestMode)
                tradeApp.Start();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
        }
        public void disconnectGlobex()
        {
            disconnectGlobex(null);
        }

        public void disconnectGlobex(Action logonOut)
        {
            try
            {
                this._logonOut = logonOut;
                //保存内存中的ClOrdID
                //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ZDTradeClientTT.exe.config");
                //ZDTradeClientTTConfiurations.UpdateConfig(path, "CL_ORDER_ID", ZDTradeClientTTConfiurations.ClOrderID);
                if (!IsTestMode)
                    tradeApp.Stop();
                if (!_execReportBC.IsCompleted)
                {
                    _execReportBC.CompleteAdding();
                }

            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        public void shutdown()
        {
            //ZDTradeClientTT.CfgManager cfgInstance = ZDTradeClientTT.CfgManager.getInstance("ZDTradeClientTT.exe");

            //分开try 避免一个异常而影响其他
            try
            {
                GTCOrderMgr.persistGTCOrder();
                //OrderModel.SaveToFile(GlobalData.OrderModelList);
                //ClOrderIDGen.saveOrderId();
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error("persistGTCOrder() 异常。");
                TT.Common.NLogUtility.Error(ex.ToString());
            }

            try
            {
      
                OrderModel.SaveToFile(GlobalData.OrderModelList);

            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error("SaveToFile() 异常。");
                TT.Common.NLogUtility.Error(ex.ToString());
            }


            try
            {

                ClOrderIDGen.saveOrderId();
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error("saveOrderId() 异常。");
                TT.Common.NLogUtility.Error(ex.ToString());
            }

           
        }

        /// <summary>
        /// rainer 壳调用
        /// </summary>
        public void persistDayRefObj()
        {
            try
            {
                NonGTCOrderMgr.persistNonGTCOrder();
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 改单（先撤单、然后下单）失败执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OrderCacnelRejectHandler(object sender, OrderCancelRejectEventArgs e)
        {
            try
            {
                QuickFix.FIX42.OrderCancelReject cancelRejMsg = e.CancelRejMsg;
                NetInfo netInfo = replyCancelReject(cancelRejMsg);
                //if(netInfo != null)
                //    AuditTrailMgr.addMsg(cancelRejMsg, netInfo);
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }

        }

        public void execReportHandler(object stateInfo)
        {
            try
            {
                while (!_execReportBC.IsCompleted)
                {
                    QuickFix.FIX42.ExecutionReport execReport;
                    if (_execReportBC.TryTake(out execReport, 500))
                    {
                        ExecReport(execReport);
                    }

                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        /// <summary>
        /// TT执行结果返回
        /// </summary>
        /// <param name="execReport"></param>
        public void ExecReport(QuickFix.FIX42.ExecutionReport execReport)
        {
            try
            {

                char execType = execReport.ExecType.getValue();

                NetInfo netInfo = null;
                switch (execType)
                {
                    case ExecType.NEW://下单
                        netInfo = replyOrderCreation(execReport);
                        break;

                    case ExecType.FILL:
                    case ExecType.PARTIAL_FILL:
                        netInfo = replyFill(execReport);
                        break;

                    case ExecType.CANCELED://撤销
                        netInfo = replyCancelled(execReport);
                        break;

                    case ExecType.REJECTED://150=8
                        netInfo = replyRejected(execReport);
                        break;

                    case ExecType.PENDING_CANCEL:
                        doPendingCancel(execReport);
                        break;

                    case ExecType.REPLACED://改单
                        netInfo = replyReplaced(execReport);
                        break;

                    case ExecType.EXPIRED:
                        //netInfo = doExpired(execReport);
                        netInfo = replyCancelled(execReport);
                        break;
                    default:
                        DefaultExecType(execReport);
                        break;

                        //case GlobexExt.ORD_STATUS_TRADE_CANCELLATION:
                        //    break;
                }

                //if (netInfo != null)
                //AuditTrailMgr.addMsg(execReport, netInfo);
            }
            catch (Exception ex)
            {

                TT.Common.NLogUtility.Error(ex.ToString());

            }
        }

        /// <summary>
        /// TT路没有用到此处代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onExecReportEvent(object sender, ExectutionEventArgs e)
        {
            try
            {
                QuickFix.FIX42.ExecutionReport execReport = e.ExecReportObject;

                //char ordStatus = execReport.OrdStatus.getValue();
                char execType = execReport.ExecType.getValue();

                NetInfo netInfo = null;
                switch (execType)
                {
                    case ExecType.NEW:
                        netInfo = replyOrderCreation(execReport);
                        break;

                    case ExecType.FILL:
                    case ExecType.PARTIAL_FILL:
                        netInfo = replyFill(execReport);
                        break;

                    case ExecType.CANCELED:
                        netInfo = replyCancelled(execReport);
                        break;

                    case ExecType.REJECTED:
                        netInfo = replyRejected(execReport);
                        break;

                    case ExecType.PENDING_CANCEL:
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
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        public NetInfo doPendingCancel(QuickFix.FIX42.ExecutionReport execReport)
        {
            RefObj refObj;
            bool ret = _downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
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

        public NetInfo replyCancelReject(QuickFix.FIX42.OrderCancelReject cancelRejMsg)
        {
            //globexIFLogger.log(ZDLogger.LVL_CRITCAL, "now at replyCancelReject()");
            NetInfo netInfo = new NetInfo();
            //  CodeBean codeBean = CodeTransfer_TT.getZDCodeInfoByUpperCode(cancelRejMsg.SecurityID.getValue());
            //     netInfo.exchangeCode = codeBean.zdExchg;
            netInfo.errorMsg = "CxlRejectReason(Tag102):" + cancelRejMsg.GetField(Tags.CxlRejReason) + "-" + removeIlleagalChar(cancelRejMsg.GetField(Tags.Text));

            //系统号
            RefObj refObj;
            bool ret = _downReference.TryGetValue(cancelRejMsg.OrderID.getValue(), out refObj);

            if (!ret)
            {
                TT.Common.NLogUtility.Info("replyCancelReject() -> not find refOBJ for orderID:" + cancelRejMsg.OrderID.getValue());
                TT.Common.NLogUtility.Info(cancelRejMsg.ToString());
                return null;
            }

            refObj.addGlobexRes(cancelRejMsg);

            char cancelRejResponseTo = cancelRejMsg.GetChar(Tags.CxlRejResponseTo);

            //globexIFLogger.log(ZDLogger.LVL_CRITCAL, "cancelRejResponseTo:" + cancelRejResponseTo);

            // 2 = Order Cancel/Replace Request
            if (cancelRejResponseTo == '2')
            {
                //globexIFLogger.log(ZDLogger.LVL_CRITCAL, "cancelRejResponseTo @ condiont 2");
                OrderResponseInfo minfo = new OrderResponseInfo();
                //CodeBean codeBean = CodeTransfer_TT.getZDCodeInfoByUpperCode(refObj.newOrderSingle.SecurityID.getValue());
                //minfo.exchangeCode = codeBean.zdExchg;
                var symbol = refObj.newOrderSingle.Symbol.getValue();
                var zd = Configurations.GetZDExchangeProduct(symbol);
                minfo.exchangeCode = zd.ZDExchange;
                minfo.orderNo = refObj.clOrderID;
                minfo.accountNo = refObj.strArray[0];
                minfo.systemNo = refObj.strArray[1];


                netInfo.infoT = minfo.MyToString();
                netInfo.exchangeCode = minfo.exchangeCode;
                netInfo.accountNo = refObj.strArray[0];
                netInfo.systemCode = refObj.strArray[1];
                netInfo.errorCode = ErrorCode.ERR_ORDER_0016;
                netInfo.code = CommandCode.MODIFY;
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
                netInfo.code = CommandCode.CANCELCAST;
            }

            TradeServerFacade.SendString(netInfo);
            //globexIFLogger.log(ZDLogger.LVL_CRITCAL, "data sent");

            return netInfo;
        }

        public NetInfo replyOrderCreation(QuickFix.FIX42.ExecutionReport execReport)
        {
            //系统号
            RefObj refObj;
            long clOrdID = Convert.ToInt64(execReport.ClOrdID.getValue());
            bool ret = _xReference.TryGetValue(clOrdID, out refObj);
            if (!ret)
            {
                TT.Common.NLogUtility.Error($"下单成功返回，ClOrdID:{execReport.ClOrdID.getValue()}内存数据丢失");
                return null;
            }
            OrderResponseInfo info = new OrderResponseInfo();

            info.orderNo = execReport.ClOrdID.getValue();

            string OrderID = execReport.OrderID.getValue();
            //info.origOrderNo = info.orderNo;
            //OrderID 是GUID,长度过长，改赋值ClOrdID
            info.origOrderNo = info.orderNo;

            info.orderMethod = "1";
            info.htsType = "";

            string strSymbol = execReport.Symbol.getValue();
            //CodeBean codeBean = CodeTransfer_TT.getZDCodeInfoByUpperCode(execReport.SecurityID.getValue());

            //info.exchangeCode = codeBean.zdExchg;
            var zd = Configurations.GetZDExchangeProduct(strSymbol);
            info.exchangeCode = zd.ZDExchange;

            //if (execReport.Side.getValue() == Side.BUY)
            //    info.buySale = "1";
            //else
            //    info.buySale = "2";
            info.buySale = QuerySide(execReport.Side);
            info.tradeType = "1";

            char ordType = execReport.OrdType.getValue();
            info.priceType = ConvertToZDOrdType(ordType.ToString());


            if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
            {
                info.orderPrice = CodeTransfer_TT.toClientPrx(execReport.Price.getValue(), strSymbol);
            }

            if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
            {
                info.triggerPrice = CodeTransfer_TT.toClientPrx(execReport.StopPx.getValue(), strSymbol);
            }


            info.orderNumber = execReport.OrderQty.getValue().ToString();
            info.filledNumber = "0";

            DateTime transTime = execReport.TransactTime.getValue();
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");



            //info.validDate = ConvertToZDTimeInForce(execReport.TimeInForce.ToString());

            info.validDate = ConvertToZDTimeInForce(refObj, execReport.TimeInForce.ToString());

            refObj.orderID = OrderID;
            refObj.addGlobexRes(execReport);

            _downReference.TryAdd(OrderID, refObj);

            info.code = refObj.strArray[4];
            info.accountNo = refObj.strArray[0];
            info.systemNo = refObj.strArray[1];


            info.acceptType = refObj.strArray[5];

            NetInfo obj = new NetInfo();

            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = CommandCode.ORDER;

            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            //obj.todayCanUse = execReport.Header.GetField(Tags.TargetSubID);
            obj.todayCanUse = refObj.strArray[6];
            obj.clientNo = refObj.strArray[2];
            obj.localSystemCode = refObj.strArray[3];

            TradeServerFacade.SendString(obj);

            return obj;
        }

        public NetInfo replyFill(QuickFix.FIX42.ExecutionReport execReport)
        {
            FilledResponseInfo info = new FilledResponseInfo();

            if (execReport.IsSetExecTransType())
            {
                char execTransType = execReport.ExecTransType.getValue();
                if (execTransType == ExecTransType.CORRECT)
                {
                    TT.Common.NLogUtility.Info("Discard fill modification(tag20=2):");
                    TT.Common.NLogUtility.Info(execReport.ToString());
                    return null;
                }
            }


            string strSymbol = execReport.Symbol.getValue();

            //CodeBean codeBean = null;

            //    codeBean = CodeTransfer_TT.getZDCodeInfoByUpperCode(execReport.SecurityID.getValue());
            //    info.exchangeCode = codeBean.zdExchg;
            var zd = Configurations.GetZDExchangeProduct(strSymbol);
            info.exchangeCode = zd.ZDExchange;

            //if (execReport.Side.getValue() == Side.BUY)
            //    info.buySale = "1";
            //else
            //    info.buySale = "2";
            info.buySale = QuerySide(execReport.Side);
            info.filledNo = execReport.ExecID.getValue();
            info.filledNumber = execReport.LastShares.ToString();
            info.filledPrice = CodeTransfer_TT.toClientPrx(execReport.LastPx.getValue(), strSymbol);

            //获取成交时间。TransactTime如果没有精确到ms,就从SendingTime拿
            //暂时先不上
            /*DateTime transTime = execReport.TransactTime.getValue();
            if (transTime.Millisecond == 0)
            {
                if (execReport.Header.IsSetField(Tags.SendingTime))
                {
                    transTime = QuickFix.Fields.Converters.DateTimeConverter.ConvertToDateTime(execReport.Header.GetField(Tags.SendingTime));
                }
            }

            transTime = transTime.ToLocalTime();

            info.filledTime = transTime.ToString("HH:mm:ss.fff");
            info.filledDate = transTime.ToString("yyyy-MM-dd");*/

            DateTime transTime = execReport.TransactTime.getValue();
            info.filledTime = transTime.ToString("HH:mm:ss");
            info.filledDate = transTime.ToString("yyyy-MM-dd");

            int mReportType = 1;

            // 1 = Outright;2 = Leg of spread;3 = Spread
            if (execReport.IsSetMultiLegReportingType())
            {
                mReportType = execReport.GetInt(Tags.MultiLegReportingType);
            }

            //系统号
            RefObj refObj;
            bool ret = _downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            if (!ret)
            {
                TT.Common.NLogUtility.Error($"订单成交返回，ClOrdID:{execReport.ClOrdID.getValue()}内存数据丢失");
                return null;
            }
            if (mReportType == 1 || mReportType == 3)
            {
                decimal cumQty = execReport.CumQty.getValue();
                if (refObj.cumFilled + execReport.LastShares.getValue() != cumQty)
                {
                    TT.Common.NLogUtility.Info("Discard repeated fill message:");
                    TT.Common.NLogUtility.Info(execReport.ToString());
                    return null;
                }

                refObj.cumFilled = cumQty;
            }

            info.orderNo = refObj.clOrderID;
            info.accountNo = refObj.strArray[0];
            info.systemNo = refObj.strArray[1];
            info.code = refObj.strArray[4]; // codeBean.zdCode; //refObj.strArray[4];

            NetInfo obj = new NetInfo();
            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = CommandCode.FILLEDCAST;
            obj.accountNo = info.accountNo;
            //obj.todayCanUse = execReport.Header.GetField(Tags.SenderSubID);
            obj.todayCanUse = refObj.strArray[6];


            if (mReportType == 1)//FUT
            {
                TradeServerFacade.SendString(obj);
                if (execReport.LeavesQty.getValue() == 0)
                {
                    _xReference.TryRemove(long.Parse(info.orderNo), out refObj);
                    _downReference.TryRemove(refObj.orderID, out refObj);
                }
            }
            else if (mReportType == 2)// multi-leg 
            {
                QuickFix.Group g2 = execReport.GetGroup(2, Tags.NoSecurityAltID);
                //BRN Jul19
                var securityAltID = g2.GetString(Tags.SecurityAltID);
                var securityExchange = execReport.SecurityExchange.getValue();
                info.code = TTMarketAdapterCommon.GetContract(securityAltID, securityExchange, SecurityTypeEnum.MLEG);
                obj.infoT = info.MyToString();
                TradeServerFacade.SendString(obj);
            }
            else if (mReportType == 3)
            {
                // Can not clear because each leg execution will follow
                //if (execReport.LeavesQty.getValue() == 0)
                //    xReference.TryRemove(clOrdID, out refObj);
            }

            return obj;
        }

        public NetInfo replyCancelled(QuickFix.FIX42.ExecutionReport execReport)
        {
            CancelResponseInfo info = new CancelResponseInfo();
            string strSymbol = execReport.Symbol.getValue();
            //CodeBean codeBean = CodeTransfer_TT.getZDCodeInfoByUpperCode(execReport.SecurityID.getValue());
            //info.exchangeCode = codeBean.zdExchg;
            var zd = Configurations.GetZDExchangeProduct(strSymbol);
            info.exchangeCode = zd.ZDExchange;
            //if (execReport.Side.getValue() == Side.BUY)
            //    info.buySale = "1";
            //else
            //    info.buySale = "2";

            info.buySale = QuerySide(execReport.Side);

            RefObj refObj;
            bool ret = _downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            if (!ret)
            {
                TT.Common.NLogUtility.Error($"撤单成功返回，ClOrdID:{execReport.ClOrdID.getValue()}内存数据丢失");
                return null;
            }
            info.orderNo = refObj.clOrderID;
            //系统号
            info.accountNo = refObj.strArray[0];
            info.systemNo = refObj.strArray[1];
            info.code = refObj.strArray[4];

            char ordType = execReport.OrdType.getValue();
            info.priceType = ConvertToZDOrdType(ordType.ToString());// QueryPriceType(ordType);

            if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
            {
                info.orderPrice = CodeTransfer_TT.toClientPrx(execReport.Price.getValue(), strSymbol);
            }
            else if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
            {
                //info.triggerPrice = execReport.StopPx.ToString();
            }

            info.cancelNumber = execReport.LeavesQty.ToString();
            info.orderNumber = execReport.OrderQty.ToString();
            info.filledNumber = refObj.cumFilled.ToString();
            info.cancelNo = info.orderNo;

            DateTime transTime = execReport.TransactTime.getValue();
            info.cancelTime = transTime.ToString("HH:mm:ss");
            info.cancelDate = transTime.ToString("yyyy-MM-dd");

            NetInfo obj = new NetInfo();
            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = CommandCode.CANCELCAST;
            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            //obj.todayCanUse = execReport.Header.GetField(Tags.TargetSubID);
            obj.todayCanUse = refObj.strArray[6];
            obj.clientNo = refObj.strArray[2];

            TradeServerFacade.SendString(obj);

            _xReference.TryRemove(long.Parse(info.orderNo), out refObj);
            _downReference.TryRemove(refObj.orderID, out refObj);

            return obj;
        }

        public NetInfo replyRejected(QuickFix.FIX42.ExecutionReport execReport)
        {
            //判断58=Timed out超时，直接返回
            if (execReport.IsSetText())
            {
                var text = execReport.Text.getValue();
                if (text.Contains("Timed out"))
                {
                    TT.Common.NLogUtility.Info(execReport.ToString());
                    TT.Common.NLogUtility.Info("TT处理超时，不返回信息给客户端。");
                    return null;
                }
            }
            //被拒绝了
            OrderResponseInfo info = new OrderResponseInfo();

            info.orderMethod = "1";
            info.htsType = "";
            string strSymbol = execReport.Symbol.getValue();
            //CodeBean codeBean = null;
            //if (execReport.IsSetSecurityID())
            //{
            //    codeBean = CodeTransfer_TT.getZDCodeInfoByUpperCode(execReport.SecurityID.getValue());
            //    info.exchangeCode = codeBean.zdExchg;
            //}
            //else
            //{
            var zd = Configurations.GetZDExchangeProduct(strSymbol);
            info.exchangeCode = zd.ZDExchange;
            //}


            //if (execReport.Side.getValue() == Side.BUY)
            //    info.buySale = "1";
            //else
            //    info.buySale = "2";
            info.buySale = QuerySide(execReport.Side);
            info.tradeType = "1";
            char ordType = execReport.OrdType.getValue();
            info.priceType = ConvertToZDOrdType(ordType.ToString());



            if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
            {
                if (execReport.IsSetField(Tags.Price))
                {
                    info.orderPrice = CodeTransfer_TT.toClientPrx(execReport.Price.getValue(), strSymbol);
                }
            }
            else if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
            {
                info.triggerPrice = CodeTransfer_TT.toClientPrx(execReport.StopPx.getValue(), strSymbol);
            }


            info.orderNumber = execReport.OrderQty.getValue().ToString();
            info.filledNumber = "0";
            info.acceptType = "1";
            DateTime transTime = execReport.TransactTime.getValue();
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");

            //系统号
            RefObj refObj;
            bool ret = _downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            // Case of order creation is not ACKed, e.g out of trading time
            if (!ret)
            {
                long clOrdID = Convert.ToInt64(execReport.ClOrdID.getValue());
                ret = _xReference.TryGetValue(clOrdID, out refObj);
                if (!ret)
                {
                    TT.Common.NLogUtility.Error($"下单拒绝返回，ClOrdID:{clOrdID}内存数据丢失");
                    return null;
                }

            }


            info.orderNo = refObj.clOrderID;
            info.accountNo = refObj.strArray[0];
            info.systemNo = refObj.strArray[1];
            info.code = refObj.strArray[4];

            NetInfo obj = new NetInfo();
            obj.errorMsg = "OrdRejectReason(Tag103):" + execReport.GetField(Tags.OrdRejReason) + "-" + removeIlleagalChar(execReport.GetField(Tags.Text));

            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.ERR_ORDER_0004;
            obj.code = CommandCode.ORDER;
            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            //obj.todayCanUse = execReport.Header.GetField(Tags.SenderSubID);
            obj.todayCanUse = refObj.strArray[6];
            obj.clientNo = refObj.strArray[2];

            TradeServerFacade.SendString(obj);

            _xReference.TryRemove(long.Parse(info.orderNo), out refObj);

            return obj;
        }

        public NetInfo replyReplaced(QuickFix.FIX42.ExecutionReport execReport)
        {
            OrderResponseInfo info = new OrderResponseInfo();
            string strSymbol = execReport.Symbol.getValue();
            //CodeBean codeBean = CodeTransfer_TT.getZDCodeInfoByUpperCode(execReport.SecurityID.getValue());
            //info.exchangeCode = codeBean.zdExchg;
            var zd = Configurations.GetZDExchangeProduct(strSymbol);
            info.exchangeCode = zd.ZDExchange;
            //if (execReport.Side.getValue() == Side.BUY)
            //    info.buySale = "1";
            //else
            //    info.buySale = "2";
            info.buySale = QuerySide(execReport.Side);
            info.tradeType = "1";

            char ordType = execReport.OrdType.getValue();


            info.priceType = ConvertToZDOrdType(ordType.ToString());

            if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
            {
                info.orderPrice = CodeTransfer_TT.toClientPrx(execReport.Price.getValue(), strSymbol);
            }
            // changed by Rainer on 20150304 -begin
            //else if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
            if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
            {
                info.triggerPrice = CodeTransfer_TT.toClientPrx(execReport.StopPx.getValue(), strSymbol);
            }
            // changed by Rainer on 20150304 -end

            //  char tif = execReport.TimeInForce.getValue();
            //info.validDate = QueryValidDate(tif);

            //info.modifyNumber = execReport.OrderQty.getValue().ToString();
            info.orderNumber = execReport.OrderQty.getValue().ToString();
            info.filledNumber = "0";

            DateTime transTime = execReport.TransactTime.getValue();
            info.orderTime = transTime.ToString("HH:mm:ss");
            info.orderDate = transTime.ToString("yyyy-MM-dd");

            //系统号
            RefObj refObj;
            bool ret = _downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            if (!ret)
            {
                TT.Common.NLogUtility.Error($"改单成功返回，ClOrdID:{execReport.ClOrdID.getValue()}内存数据丢失");
                return null;
            }
            //info.validDate = ConvertToZDTimeInForce(execReport.TimeInForce.ToString());
            info.validDate = ConvertToZDTimeInForce(refObj, execReport.TimeInForce.ToString());
            info.orderNo = refObj.clOrderID;
            info.origOrderNo = info.orderNo;

            refObj.addGlobexRes(execReport);

            info.accountNo = refObj.strArray[0];
            info.systemNo = refObj.strArray[1];
            info.code = refObj.strArray[4];

            info.acceptType = refObj.strArray[5];


            NetInfo obj = new NetInfo();

            obj.infoT = info.MyToString();
            obj.exchangeCode = info.exchangeCode;
            obj.errorCode = ErrorCode.SUCCESS;
            obj.code = CommandCode.MODIFY;
            obj.accountNo = info.accountNo;
            obj.systemCode = info.systemNo;
            //obj.todayCanUse = execReport.Header.GetField(Tags.TargetSubID);
            obj.todayCanUse = refObj.strArray[6];
            obj.clientNo = refObj.strArray[2];

            TradeServerFacade.SendString(obj);

            return obj;
        }

        public NetInfo doExpired(QuickFix.FIX42.ExecutionReport execReport)
        {
            //系统号
            RefObj refObj;
            bool ret = _downReference.TryGetValue(execReport.OrderID.getValue(), out refObj);
            if (ret)
            {
                string orderNo = refObj.clOrderID;
                _xReference.TryRemove(long.Parse(orderNo), out refObj);
                _downReference.TryRemove(refObj.orderID, out refObj);
            }

            return null;
        }


        private void DefaultExecType(ExecutionReport execReport)
        {
            var text = string.Empty;
            if (!execReport.IsSetText())
            {
                return;
            }
            text = execReport.Text.getValue();
            string clOrdId = execReport.OrderID.getValue();
            //系统号
            RefObj refObj = null;
            bool ret = _downReference.TryGetValue(clOrdId, out refObj);
            // Case of order creation is not ACKed, e.g out of trading time
            if (!ret)
            {
                long clOrdID = Convert.ToInt64(execReport.ClOrdID.getValue());
                //ret = _xReference.TryGetValue(clOrdID, out refObj);
                //_downReference 没有添加，如果_xReference添加说明是下单失败。
                if (_xReference.TryGetValue(clOrdID, out refObj))
                {
                    OrderException(refObj.NetInfo, text);
                }
            }
            else
            {

                var lastOrder = refObj.GetOrder(clOrdId);
                if (!string.IsNullOrEmpty(lastOrder.MessageType))
                {
                    switch (lastOrder.MessageType)
                    {
                        case "D":
                            OrderException(refObj.NetInfo, text);
                            break;
                        case "F":
                            CancelOrderException(refObj.NetInfo, text);
                            break;
                        case "G":
                            OrderCancelReplaceException(refObj.NetInfo, text);
                            break;
                    }
                }
            }
        }


        //不用tag48
        //https://library.tradingtechnologies.com/tt-fix/InstrumentBlock_Request.html
        //Section：When specifying by alternate security ID  

        public void PlaceOrder(NetInfo obj)
        {
            try
            {
                OrderInfo info = new OrderInfo();
                info.MyReadString(obj.infoT);

                long clOrdID = ClOrderIDGen.getNextClOrderID();
                QuickFix.FIX42.NewOrderSingle newOrderSingle = new QuickFix.FIX42.NewOrderSingle();
                // Tag11
                newOrderSingle.ClOrdID = new ClOrdID(clOrdID.ToString());
                //// Tag60
                //newOrderSingle.TransactTime = new TransactTime(DateTime.UtcNow);
                var securityExchange = info.exchangeCode;
                if (_securityDefinitionFileExist)
                {
                    #region When specifying by security ID
                    SecurityDefinition sd = CodeTransfer_TT.getUpperCodeInfoByZDCode(info.code, info.exchangeCode);
                    if (sd == null)
                    {
                        string msg = $"contract({ info.exchangeCode},{info.code}) not found!";
                        TT.Common.NLogUtility.Info(msg);
                        throw new Exception(msg);
                    }
                    // Tag48
                    newOrderSingle.SecurityID = sd.SecurityID;
                    //Tag22
                    newOrderSingle.IDSource = new IDSource("96");
                    //Tag 55
                    newOrderSingle.Symbol = sd.Symbol;

                    securityExchange = sd.SecurityExchange.getValue();
                    #endregion
                    //var symbol = sd.Symbol;
                    //var securityID = sd.SecurityID;
                }
                else
                {
                    //或者

                    #region When specifying by alternate security ID
                    //
                    var newCode = info.code;
                    var securityType = TTMarketAdapterCommon.GetSecurityType(info.code);
                    if(securityType==SecurityTypeEnum.OPT)
                    {
                        CompatibleOptionCodeConverter.IsCompatibleOption(info.code, ref newCode);
                    }










                    TTMarketAdapter.Model.OrderModel orderModel = TTMarketAdapterCommon.GetOrderModel(newCode);
                    var validate = orderModel.Validate();
                    if (!validate.Success)
                    {
                        OrderException(obj, validate.ErrorMessage);
                        return;
                    }
                    // Tag55
                    //newOrderSingle.Symbol = sd.Symbol;
                    newOrderSingle.Symbol = new Symbol(orderModel.Symbol);
                    // Tag207
                    newOrderSingle.SecurityExchange = new SecurityExchange(orderModel.SecurityExchange);
                    securityExchange = orderModel.SecurityExchange;
                    //167
                    newOrderSingle.SecurityType = new SecurityType(orderModel.SecurityType);
                    //454
                    NoSecurityAltIDGroup noSecurityAltIDGroup = new NoSecurityAltIDGroup();
                    //455
                    noSecurityAltIDGroup.SecurityAltID = new SecurityAltID(orderModel.SecurityAltID);
                    //456
                    noSecurityAltIDGroup.SecurityAltIDSource = new SecurityAltIDSource(orderModel.SecurityAltIDSource);
                    newOrderSingle.AddGroup(noSecurityAltIDGroup);
                    #endregion
                }

                //委托量
                var orderQty = decimal.Parse(info.orderNumber);
                // Tag38
                newOrderSingle.OrderQty = new OrderQty(orderQty);
                // Tag54
                newOrderSingle.Side = QuerySide(info.buySale);
                ////Tag167
                //newOrderSingle.SecurityType = sd.SecurityType;
                ////Tag207
                //newOrderSingle.SecurityExchange = sd.SecurityExchange;


                //客户端用的是FIX 7X和 新TT的FIX版本 不一样
                //两个版本的OrderType值1和2反了
                string orderType = ConvertToTTOrdType(info.priceType);

                char charOrdType = Char.Parse(orderType);
                // Tag40
                newOrderSingle.OrdType = new OrdType(charOrdType);// QueryOrdType(info.priceType);

                // Tag44
                if (charOrdType == OrdType.LIMIT || charOrdType == OrdType.STOP_LIMIT)
                {
                    decimal prx = CodeTransfer_TT.toGlobexPrx(info.orderPrice, newOrderSingle.Symbol.getValue());
                    newOrderSingle.Price = new Price(prx);
                }

                // Tag99
                if (charOrdType == OrdType.STOP || charOrdType == OrdType.STOP_LIMIT)
                {
                    decimal prx = CodeTransfer_TT.toGlobexPrx(info.triggerPrice, newOrderSingle.Symbol.getValue());
                    newOrderSingle.StopPx = new StopPx(prx);
                }
                //tag 77
                newOrderSingle.OpenClose = new OpenClose('O');
                // Tag11028
                newOrderSingle.ManualOrderIndicator = moi;

                ////T+1处理 17:15---1:00
                //if (ttPrdCode == "HSI")
                //{
                //    TimeSpan tsStart = new TimeSpan(17, 15, 0);
                //    TimeSpan tsEnd = new TimeSpan(1, 0, 0);
                //    TimeSpan tsCurrent = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                //    if (tsCurrent > tsStart || tsCurrent < tsEnd)
                //    {
                //        info.validDate = "W";
                //    }
                //}
                string timeInForce = info.validDate;
                //港交所T+1时间段内下单59=W
                if (info.exchangeCode == "HKEX")
                {
                    if (TPlusOneHelper.IsTPlusOne(newOrderSingle.Symbol.getValue()))
                    {
                        timeInForce = "W";
                    }
                }

                timeInForce = ConvertToTTTimeInForce(timeInForce);

                //FAK =IOC，此处待修改
                if (timeInForce == "3")
                {
                    //我们系统要把一下注释放开
                    if (info.MinQty == info.orderNumber)
                    {
                        timeInForce = "4";
                    }
                    else
                    {
                        string msg = $"not support FAK order!";
                        TT.Common.NLogUtility.Info(msg);
                        throw new Exception(msg);
                    }
                    string minQty = info.MinQty;
                    if (minQty != "0")
                        newOrderSingle.SetField(new MinQty(decimal.Parse(minQty)));
                }

                //////tag 59
                //newOrderSingle.TimeInForce = QueryTimeInForce(info.validDate);
                //tag 59
                newOrderSingle.TimeInForce = new TimeInForce(char.Parse(timeInForce));


                // Tag1  上手号
                newOrderSingle.Account = new Account(obj.accountNo);
                // SenderSubID(Tag50 ID)
                //newOrderSingle.SetField(new SenderSubID(obj.todayCanUse));

                ////Tag582
                //newOrderSingle.CustOrderCapacity = new CustOrderCapacity(4);


                //GHF 要求使用116，Phillip除了ICE之外不用
                if (!string.IsNullOrEmpty(ZDTradeClientTTConfiurations.OnBehalfOfSubID))
                {
                    if (ZDTradeClientTTConfiurations.Company.ToUpper() == "GHF")
                    {
                        //Tag 116  
                        newOrderSingle.OnBehalfOfSubID = new OnBehalfOfSubID(ZDTradeClientTTConfiurations.OnBehalfOfSubID);
                    }
                    else if (ZDTradeClientTTConfiurations.Company.ToUpper() == "PHILLIP")
                    {
                        //phillip ice 加tag116
                        if (securityExchange == "ICE")
                        {
                            //Tag 116  
                            newOrderSingle.OnBehalfOfSubID = new OnBehalfOfSubID(ZDTradeClientTTConfiurations.OnBehalfOfSubID);
                        }
                    }
                }

                //iceBerg单 显示数量<实际数量
                if (!string.IsNullOrEmpty(info.MaxShow) && info.MaxShow != "0")
                {
                    var displayQty = decimal.Parse(info.MaxShow);
                    if (orderQty > displayQty)
                    {
                        newOrderSingle.DisplayQty = new DisplayQty(displayQty);
                    }
                }



                #region GHF Tag
                if (ZDTradeClientTTConfiurations.Company == "GHF")
                {
                    string p_Fxd_Clis_Ac_Ref = ZDTradeClientTTConfiurations.Prefix + obj.todayCanUse;
                    //string p_Fxd_Clis_Ac_Ref = "ZD123456";
                    switch (securityExchange)
                    {
                        case "Euronext":
                            //Tag 50
                            newOrderSingle.SenderSubID = new SenderSubID(p_Fxd_Clis_Ac_Ref);
                            //Tag 16558
                            newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
                            break;
                        case "LME":
                        case "Eurex":
                            ////Tag 50
                            //newOrderSingle.SenderSubID = new SenderSubID("ZDTEST1234");
                            //Tag 16999
                            newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride(p_Fxd_Clis_Ac_Ref);
                            //Tag 16558
                            newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
                            
                            break;
                        case "ICE":
                        case "ICE_L":
                            NoPartyIDsGroup noPartyIDsGroup = new NoPartyIDsGroup();
                            //Tag 452=211
                            noPartyIDsGroup.PartyRole = new PartyRole(211);
                            //Tag 448
                            noPartyIDsGroup.PartyID = new PartyID(p_Fxd_Clis_Ac_Ref);
                            newOrderSingle.AddGroup(noPartyIDsGroup);
                            ////Tag 422
                            newOrderSingle.TotNoStrikes = new TotNoStrikes(211);
                            //Tag 16558
                            newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);

                            break;

                        case "CFE":
                            //Tag 16999
                            newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride("DX0" + ZDTradeClientTTConfiurations.Pre_Agreed_Ref);
                            //Tag 50
                            newOrderSingle.SenderSubID = new SenderSubID(p_Fxd_Clis_Ac_Ref);
                            //Tag 16558
                            newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
                            break;
                        case "CME":
                            //"CME Globex"
                            //Tag 50
                            newOrderSingle.SenderSubID = new SenderSubID(p_Fxd_Clis_Ac_Ref);
                            //Tag 16558
                            newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
                            //Tag 16999 注：CME Globex没有要求填写，BMD要填写，到时候根据具体品种反推
                            //是CME Globex 还是BMD来设置此Tag值。
                            newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride(ZDTradeClientTTConfiurations.KenangaRef);
                            ////Tag 7928
                            //newOrderSingle.SelfMatchPreventionID = new SelfMatchPreventionID(CfgManager.getInstance().CME_SMPID);
                            ////Tag 8000
                            //newOrderSingle.SMPInstruction = new SMPInstruction(CfgManager.getInstance().CME_Instruction);
                            break;
                        //case "MGEX":
                        //    break;
                        //case "MX":
                        //    break;
                        //case "CME":
                        //    //"BMD":
                        //    break;
                        case "HKEX":
                            //Tag 16557
                            newOrderSingle.TextB = new TextB(p_Fxd_Clis_Ac_Ref);
                            //Tag 16558
                            newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
                            break;
                        case "ASX":
                            ////Tag 16556
                            //newOrderSingle.TextA = new TextA(p_Fxd_Clis_Ac_Ref);
                            ////Tag 16558
                            //newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);

                            //ASX由于长度限制，不加前缀
                            //Tag 16556
                            newOrderSingle.TextA = new TextA(obj.todayCanUse);
                            //Tag 16558
                            newOrderSingle.TextTT = new TextTT(obj.todayCanUse);
                        
                            break;
                        case "SGX":
                            //Tag 16999
                            newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride(ZDTradeClientTTConfiurations.SGX_ClearingAccountOverride);
                            //Tag 16557
                            newOrderSingle.TextB = new TextB(p_Fxd_Clis_Ac_Ref);
                            //Tag 16558
                            newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
                            break;
                        case "OSE":
                            //Tag 16999
                            newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride(p_Fxd_Clis_Ac_Ref);
                            //Tag 16557
                            newOrderSingle.TextB = new TextB(p_Fxd_Clis_Ac_Ref);
                            //Tag 16558
                            newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
                            break;
                    }
                }

                ////16999
                //newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride("ZD123456");
                ////Tag 16558
                //newOrderSingle.TextTT = new TextTT("ZD123456");

                #endregion

                // Maintain XReference
                RefObj refObj = new RefObj();
                refObj.NetInfo = obj;
                refObj.clOrderID = clOrdID.ToString();
                string[] temp = { obj.accountNo, obj.systemCode, obj.clientNo, obj.localSystemCode, info.code, info.userType, obj.todayCanUse };
                refObj.newOrderSingle = newOrderSingle;
                refObj.strArray = temp;
                _xReference.TryAdd(clOrdID, refObj);
                refObj.addClientReq(newOrderSingle);


                bool ret = tradeApp.Send(newOrderSingle);

                if (!ret)
                {

                    OrderException(obj, "can not connect to TT server!");
                }

            }
            catch (Exception ex)
            {
                //去掉汉字
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
                OrderException(obj, msg);
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        /// <summary>  
        /// old 版本  通过合约文件取合约信息（合约的ID）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="info"></param>
        /// <param name="old"></param>
        //public void PlaceOrder(NetInfo obj,bool  old)
        //{
        //    try
        //    {
        //        if (!File.Exists(_secu_File))
        //        {
        //            PlaceOrder(obj);
        //            return;
        //        }
        //        OrderInfo info = new OrderInfo();
        //        info.MyReadString(obj.infoT);




        //        SecurityDefinition sd = CodeTransfer_TT.getUpperCodeInfoByZDCode(info.code, info.exchangeCode);
        //        if (sd == null)
        //        {
        //            string msg = $"contract({ info.exchangeCode},{info.code}) not found!";
        //            Logger.Info(msg);
        //            throw new Exception(msg);
        //        }

        //        var securityExchange = sd.SecurityExchange.getValue();
        //        var symbol = sd.Symbol;
        //        var securityID = sd.SecurityID;







        //        long clOrdID = ClOrderIDGen.getNextClOrderID();
        //        QuickFix.FIX42.NewOrderSingle newOrderSingle = new QuickFix.FIX42.NewOrderSingle();
        //        // Tag11
        //        newOrderSingle.ClOrdID = new ClOrdID(clOrdID.ToString());
        //        //// Tag60
        //        //newOrderSingle.TransactTime = new TransactTime(DateTime.UtcNow);
        //        // Tag48
        //        newOrderSingle.SecurityID = securityID;
        //        // Tag22
        //        newOrderSingle.IDSource = new IDSource("96");
        //        // Tag55
        //        newOrderSingle.Symbol = symbol;
        //        //委托量
        //        var orderQty = decimal.Parse(info.orderNumber);
        //        // Tag38
        //        newOrderSingle.OrderQty = new OrderQty(orderQty);
        //        // Tag54
        //        newOrderSingle.Side = QuerySide(info.buySale);
        //        ////Tag167
        //        //newOrderSingle.SecurityType = sd.SecurityType;
        //        ////Tag207
        //        //newOrderSingle.SecurityExchange = sd.SecurityExchange;


        //        //客户端用的是FIX 7X和 新TT的FIX版本 不一样
        //        //两个版本的OrderType值1和2反了，所以
        //        // 用客户端下单测试要把该注释放开
        //        switch (info.priceType)
        //        {
        //            case "1":
        //                info.priceType = "2";
        //                break;
        //            case "2":
        //                info.priceType = "1";
        //                break;
        //            case "3":
        //                info.priceType = "4";
        //                break;
        //            case "4":
        //                break;
        //            case "":
        //                break;
        //        }






        //        char charOrdType = Char.Parse(info.priceType);
        //        // Tag40
        //        newOrderSingle.OrdType = new OrdType(charOrdType);// QueryOrdType(info.priceType);
        //        string ttPrdCode = symbol.getValue();
        //        // Tag44
        //        if (charOrdType == OrdType.LIMIT || charOrdType == OrdType.STOP_LIMIT)
        //        {
        //            decimal prx = CodeTransfer_TT.toGlobexPrx(info.orderPrice, ttPrdCode);
        //            newOrderSingle.Price = new Price(prx);
        //        }






        //        // Tag99
        //        if (charOrdType == OrdType.STOP || charOrdType == OrdType.STOP_LIMIT)
        //        {
        //            decimal prx = CodeTransfer_TT.toGlobexPrx(info.triggerPrice, ttPrdCode);
        //            newOrderSingle.StopPx = new StopPx(prx);
        //        }
        //        //tag 77
        //        newOrderSingle.OpenClose = new OpenClose('O');
        //        // Tag11028
        //        newOrderSingle.ManualOrderIndicator = moi;

        //        ////T+1处理 17:15---1:00
        //        //if (ttPrdCode == "HSI")
        //        //{
        //        //    TimeSpan tsStart = new TimeSpan(17, 15, 0);
        //        //    TimeSpan tsEnd = new TimeSpan(1, 0, 0);
        //        //    TimeSpan tsCurrent = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        //        //    if (tsCurrent > tsStart || tsCurrent < tsEnd)
        //        //    {
        //        //        info.validDate = "W";
        //        //    }
        //        //}

        //        //港交所T+1时间段内下单59=W
        //        if (securityExchange == "HKEX")
        //        {
        //            if (TPlusOneHelper.IsTPlusOne(ttPrdCode))
        //            {
        //                info.validDate = "W";
        //            }
        //        }


        //        //  validDate==TimeInForce
        //        //新TT和老TT不一样
        //        switch (info.validDate)
        //        {
        //            case "1"://当日有效
        //                info.validDate = "0";
        //                break;
        //            case "2"://永久有效
        //                info.validDate = "1";
        //                break;
        //            case "4"://IOC
        //                info.validDate = "3";
        //                break;
        //            case "5":// Fill Or Kill (FOK)
        //                info.validDate = "4";
        //                break;
        //        }




        //        if (info.validDate == "3")
        //        {
        //            //我们系统要把一下注释放开
        //            if (info.MinQty == info.orderNumber)
        //            {
        //                info.validDate = "4";
        //            }
        //            else
        //            {
        //                string msg = $"not support FAK order!";
        //                Logger.Info(msg);
        //                throw new Exception(msg);
        //            }
        //            string minQty = info.MinQty;
        //            if (minQty != "0")
        //                newOrderSingle.SetField(new MinQty(decimal.Parse(minQty)));
        //        }

        //        //////tag 59
        //        //newOrderSingle.TimeInForce = QueryTimeInForce(info.validDate);
        //        //tag 59
        //        newOrderSingle.TimeInForce = new TimeInForce(char.Parse(info.validDate));


        //        // Tag1  上手号
        //        newOrderSingle.Account = new Account(obj.accountNo);


        //        ////Tag582
        //        //newOrderSingle.CustOrderCapacity = new CustOrderCapacity(4);






        //        //string exchange = sd.SecurityExchange.getValue();
        //        //if (exchange == "LME")
        //        //{
        //        //    newOrderSingle.OnBehalfOfSubID = new OnBehalfOfSubID("XJing");
        //        //}

        //        ////Tag 116  --测试用（账号在多个地方使用要加此tag），部署前注释掉
        //        //newOrderSingle.OnBehalfOfSubID = new OnBehalfOfSubID("LRui");



        //        //Tag 116  --测试用（账号在多个地方使用要加此tag），部署前注释掉
        //        newOrderSingle.OnBehalfOfSubID = new OnBehalfOfSubID(CfgManager.getInstance().OnBehalfOfSubID);

        //        //iceBerg单 显示数量<实际数量
        //        if (!string.IsNullOrEmpty(info.MaxShow) && info.MaxShow != "0")
        //        {
        //            var displayQty = decimal.Parse(info.MaxShow);
        //            if (orderQty > displayQty)
        //            {
        //                newOrderSingle.DisplayQty = new DisplayQty(displayQty);
        //            }
        //        }

        //        //var securityExchange = sd.SecurityExchange.getValue();

        //        if (CfgManager.getInstance().Company == "GHF")
        //        {
        //            #region GHF Tag
        //            string p_Fxd_Clis_Ac_Ref = CfgManager.getInstance().Prefix + obj.todayCanUse;
        //            switch (securityExchange)
        //            {
        //                case "Euronext":
        //                    //Tag 50
        //                    newOrderSingle.SenderSubID = new SenderSubID(p_Fxd_Clis_Ac_Ref);
        //                    //Tag 16558
        //                    newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
        //                    break;
        //                case "LME":
        //                case "Eurex":
        //                    //Tag 16999
        //                    newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride(p_Fxd_Clis_Ac_Ref);
        //                    //Tag 16558
        //                    newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);

        //                    break;
        //                case "ICE":
        //                case "ICE_L":
        //                    NoPartyIDsGroup noPartyIDsGroup = new NoPartyIDsGroup();
        //                    //Tag 452=211
        //                    noPartyIDsGroup.PartyRole = new PartyRole(211);
        //                    //Tag 448
        //                    noPartyIDsGroup.PartyID = new PartyID(p_Fxd_Clis_Ac_Ref);
        //                    newOrderSingle.AddGroup(noPartyIDsGroup);
        //                    ////Tag 422
        //                    newOrderSingle.TotNoStrikes = new TotNoStrikes(211);
        //                    //Tag 16558
        //                    newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);

        //                    break;

        //                case "CFE":
        //                    //Tag 16999
        //                    newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride("DX0" + CfgManager.getInstance().Pre_Agreed_Ref);
        //                    //Tag 50
        //                    newOrderSingle.SenderSubID = new SenderSubID(p_Fxd_Clis_Ac_Ref);
        //                    //Tag 16558
        //                    newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
        //                    break;
        //                case "CME":
        //                    //"CME Globex"
        //                    //Tag 50
        //                    newOrderSingle.SenderSubID = new SenderSubID(p_Fxd_Clis_Ac_Ref);
        //                    //Tag 16558
        //                    newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
        //                    //Tag 16999 注：CME Globex没有要求填写，BMD要填写，到时候根据具体品种反推
        //                    //是CME Globex 还是BMD来设置此Tag值。
        //                    newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride(CfgManager.getInstance().KenangaRef);
        //                    ////Tag 7928
        //                    //newOrderSingle.SelfMatchPreventionID = new SelfMatchPreventionID(CfgManager.getInstance().CME_SMPID);
        //                    ////Tag 8000
        //                    //newOrderSingle.SMPInstruction = new SMPInstruction(CfgManager.getInstance().CME_Instruction);
        //                    break;
        //                //case "MGEX":
        //                //    break;
        //                //case "MX":
        //                //    break;
        //                //case "CME":
        //                //    //"BMD":
        //                //    break;
        //                case "HKFE":
        //                    //Tag 16557
        //                    newOrderSingle.TextB = new TextB(p_Fxd_Clis_Ac_Ref);
        //                    //Tag 16558
        //                    newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
        //                    break;
        //                case "ASX":
        //                    //Tag 16556
        //                    newOrderSingle.TextA = new TextA(p_Fxd_Clis_Ac_Ref);
        //                    //Tag 16558
        //                    newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
        //                    break;
        //                case "SGX":
        //                    //Tag 16999
        //                    newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride(CfgManager.getInstance().SGX_ClearingAccountOverride);
        //                    //Tag 16557
        //                    newOrderSingle.TextB = new TextB(p_Fxd_Clis_Ac_Ref);
        //                    //Tag 16558
        //                    newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
        //                    break;
        //                case "OSE":
        //                    //Tag 16999
        //                    newOrderSingle.ClearingAccountOverride = new ClearingAccountOverride(p_Fxd_Clis_Ac_Ref);
        //                    //Tag 16557
        //                    newOrderSingle.TextB = new TextB(p_Fxd_Clis_Ac_Ref);
        //                    //Tag 16558
        //                    newOrderSingle.TextTT = new TextTT(p_Fxd_Clis_Ac_Ref);
        //                    break;
        //            }
        //            #endregion
        //        }
        //        // Maintain XReference
        //        RefObj refObj = new RefObj();
        //        refObj.clOrderID = clOrdID.ToString();
        //        string[] temp = { obj.accountNo, obj.systemCode, obj.clientNo, obj.localSystemCode, info.code, info.userType, obj.todayCanUse };
        //        refObj.newOrderSingle = newOrderSingle;
        //        refObj.strArray = temp;
        //        xReference.TryAdd(clOrdID, refObj);
        //        refObj.addClientReq(newOrderSingle);

        //        //bool ret;
        //        //if (isTestMode)
        //        //{
        //        //    newOrderSingle.Header.SetField(new SenderCompID("79G100N"));
        //        //    strategy.ProcessToApp(newOrderSingle, null);
        //        //    //newOrderSingle.SetField(new ClOrdID("10001"));
        //        //    //xReference.Clear();
        //        //    //xReference.TryAdd(10001, refObj);

        //        //    ret = true;
        //        //}
        //        //else
        //        bool ret = tradeApp.Send(newOrderSingle);

        //        if (!ret)
        //        {
        //            //Logger.Info($"PlaceOrder() fail:{info.MyToString()}");
        //            //obj.infoT = ErrorCode.ERR_ORDER_0000_MSG;
        //            //obj.errorCode = ErrorCode.ERR_ORDER_0000;
        //            //obj.code = CommandCode.ORDER;
        //            //TradeServerFacade.SendString(obj);
        //            OrderException(obj, "can not connect to TT server!");

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        //去掉汉字
        //        string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
        //        //obj.errorMsg = msg;
        //        //obj.errorCode = ErrorCode.ERR_ORDER_0004;
        //        //obj.code = CommandCode.ORDER;
        //        //TradeServerFacade.SendString(obj);
        //        //  globexIFLogger.log(ZDLogger.LVL_ERROR, ex.ToString());

        //        OrderException(obj, msg);

        //        Logger.Error(ex.ToString());
        //    }
        //}

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="obj"></param>
        public void CancelOrder(NetInfo obj)
        {

            try
            {
                CancelInfo info = new CancelInfo();
                info.MyReadString(obj.infoT);

                long clOrdID = Convert.ToInt64(info.orderNo);
                RefObj refObj;
                bool ret = _xReference.TryGetValue(clOrdID, out refObj);
                if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_CANCELREPLACE)
                {
                    QuickFix.FIX42.OrderCancelRequest orderCancelRequest = new QuickFix.FIX42.OrderCancelRequest();

                    //Tag 37
                    orderCancelRequest.OrderID = new OrderID(refObj.orderID);

                    //Tag 11
                    //long clOrdID = ClOrderIDGen.getNextClOrderID();
                    orderCancelRequest.ClOrdID = new ClOrdID(ClOrderIDGen.getNextClOrderID().ToString());

                    string lastClOrdID = string.Empty;
                    var last = refObj.fromClient.Last();

                    switch (last)
                    {
                        case OrderCancelRequest cancelOrder:
                            lastClOrdID = cancelOrder.ClOrdID.getValue();
                            break;
                        case NewOrderSingle newOrder:
                            lastClOrdID = newOrder.ClOrdID.getValue();
                            break;
                        case OrderCancelReplaceRequest replaceOrder:
                            lastClOrdID = replaceOrder.ClOrdID.getValue();
                            break;

                    }

                    //Tag 41
                    orderCancelRequest.OrigClOrdID = new OrigClOrdID(lastClOrdID);



                    refObj.addClientReq(orderCancelRequest);


                    ret = tradeApp.Send(orderCancelRequest);

                    if (!ret)
                    {


                        CancelOrderException(obj, "can not connect to TT server!");
                    }
                }
                else
                {
                    CancelOrderException(obj, $@"Order:{info.orderNo}, Not Found!");
                    TT.Common.NLogUtility.Info($"CancelOrder 订单没找到：{info.MyToString()}");
                }
            }
            catch (Exception ex)
            {
                //去掉汉字
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
                CancelOrderException(obj, msg);
            }

        }


        //public void CancelOrder(NetInfo obj, CancelInfo info, string timeInForce)
        //public void CancelOrder(NetInfo obj, CancelInfo info)
        //{

        //    try
        //    {
        //        //   CancelInfo info = new CancelInfo();
        //        //info.MyReadString(obj.infoT);

        //        long clOrdID = Convert.ToInt64(info.orderNo);
        //        RefObj refObj;
        //        bool ret = xReference.TryGetValue(clOrdID, out refObj);
        //        if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_CANCELREPLACE)
        //        {
        //            //cancelOrderWork(obj, info, timeInForce);
        //            cancelOrderWork(obj, info);
        //        }
        //        else
        //        {
        //            CancelOrderException(obj, $@"Order:{info.orderNo}, Not Found!");
        //            Logger.Info($"CancelOrder 订单没找到：{info.MyToString()}");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        CancelOrderException(obj, $@"Order:{info.orderNo}, Not Found!");
        //    }

        //}

        //public void cancelOrderWork(NetInfo obj, CancelInfo info, string timeInForce)
        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="info"></param>
        //public void cancelOrderWork(NetInfo obj, CancelInfo info)
        //{
        //    try
        //    {
        //        //CancelInfo info = new CancelInfo();
        //        //info.MyReadString(obj.infoT);

        //        long newOrderClOrdID = Convert.ToInt64(info.orderNo);
        //        RefObj refObj;
        //        bool ret = xReference.TryGetValue(newOrderClOrdID, out refObj);

        //        if (ret)
        //        {
        //            QuickFix.FIX42.OrderCancelRequest orderCancelRequest = new QuickFix.FIX42.OrderCancelRequest();

        //            //Tag 37
        //            orderCancelRequest.OrderID = new OrderID(refObj.orderID);

        //            //Tag 11
        //            long clOrdID = ClOrderIDGen.getNextClOrderID();
        //            orderCancelRequest.ClOrdID = new ClOrdID(clOrdID.ToString());

        //            string lastClOrdID = string.Empty;
        //            var last = refObj.fromClient.Last();

        //            switch (last)
        //            {
        //                case OrderCancelRequest cancelOrder:
        //                    lastClOrdID = cancelOrder.ClOrdID.getValue();
        //                    break;
        //                case NewOrderSingle newOrder:
        //                    lastClOrdID = newOrder.ClOrdID.getValue();
        //                    break;
        //                case OrderCancelReplaceRequest replaceOrder:
        //                    lastClOrdID = replaceOrder.ClOrdID.getValue();
        //                    break;

        //            }

        //            //Tag 41
        //            orderCancelRequest.OrigClOrdID = new OrigClOrdID(lastClOrdID);



        //            refObj.addClientReq(orderCancelRequest);


        //            ret = tradeApp.Send(orderCancelRequest);

        //            if (!ret)
        //            {


        //                CancelOrderException(obj);
        //            }

        //        }
        //        else
        //        {
        //            Logger.Info($"CancelOrder 订单没找到：{info.MyToString()}");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //去掉汉字
        //        string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
        //        obj.errorMsg = msg;
        //        obj.errorCode = ErrorCode.ERR_ORDER_0004;
        //        obj.code = CommandCode.ORDER;
        //        TradeServerFacade.SendString(obj);
        //        Logger.Info(ex.ToString());
        //    }
        //}






        /// <summary>
        /// 改单
        /// </summary>
        /// <param name="obj"></param>
        public void CancelReplaceOrder(NetInfo obj)
        {

            try
            {
                ModifyInfo info = new ModifyInfo();
                info.MyReadString(obj.infoT);

                long clOrdID = Convert.ToInt64(info.orderNo);
                RefObj refObj;
                bool ret = _xReference.TryGetValue(clOrdID, out refObj);
                if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_CANCELREPLACE)
                {
                    QuickFix.FIX42.NewOrderSingle newOrderSingle = refObj.newOrderSingle;
                    QuickFix.FIX42.OrderCancelReplaceRequest ocrr = new QuickFix.FIX42.OrderCancelReplaceRequest();

                    //Tag 37
                    ocrr.OrderID = new OrderID(refObj.orderID);
                    string lastClOrdID = string.Empty;

                    var last = refObj.fromClient.Last();
                    switch (last)
                    {
                        case OrderCancelRequest cancelOrder:
                            lastClOrdID = cancelOrder.ClOrdID.getValue();
                            break;
                        case NewOrderSingle newOrder:
                            lastClOrdID = newOrder.ClOrdID.getValue();
                            break;
                        case OrderCancelReplaceRequest replaceOrder:
                            lastClOrdID = replaceOrder.ClOrdID.getValue();
                            break;

                    }
                    //Tag 41
                    ocrr.OrigClOrdID = new OrigClOrdID(lastClOrdID);
                    //Tag 11
                    long newClOrdID = ClOrderIDGen.getNextClOrderID();
                    ocrr.ClOrdID = new ClOrdID(newClOrdID.ToString());
                    // Tag1
                    ocrr.Account = new Account(obj.accountNo);
                    // Tag38
                    ocrr.OrderQty = new OrderQty(decimal.Parse(info.modifyNumber));
                    // Tag54
                    ocrr.Side = QuerySide(info.buySale);




                    // Tag40 ,不能用QueryOrdType(info.priceType);方法，有的客户端LME交易所不传值
                    ocrr.OrdType = newOrderSingle.OrdType;
                    info.priceType = newOrderSingle.OrdType.ToString();

                    char ordType = Char.Parse(info.priceType);
                    string symbol = newOrderSingle.Symbol.getValue();
                    // Tag44
                    if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
                    {
                        decimal prx = CodeTransfer_TT.toGlobexPrx(info.modifyPrice, symbol);
                        ocrr.Price = new Price(prx);
                    }


                    // StopPx
                    if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
                    {
                        decimal prx = CodeTransfer_TT.toGlobexPrx(info.modifyTriggerPrice, symbol);
                        ocrr.StopPx = new StopPx(prx);
                    }
                    //tag 77
                    ocrr.OpenClose = new OpenClose('O');
                    //tag 59
                    ocrr.TimeInForce = newOrderSingle.TimeInForce;

                    // Tag11028
                    ocrr.ManualOrderIndicator = moi;

                    refObj.addClientReq(ocrr);


                    ret = tradeApp.Send(ocrr);

                    if (!ret)
                    {
                        //ModifyResponseInfo minfo = new ModifyResponseInfo();
                        //minfo.orderNo = info.orderNo;
                        //obj.errorMsg = "can not connect to TT server!";
                        //obj.infoT = minfo.MyToString();
                        //obj.exchangeCode = info.exchangeCode;
                        //obj.accountNo = info.accountNo;
                        //obj.errorCode = ErrorCode.ERR_ORDER_0016;
                        //obj.code = CommandCode.MODIFY;

                        //TradeServerFacade.SendString(obj);


                        OrderCancelReplaceException(obj, "can not connect to TT server!");
                    }
                    //else
                    //{
                    //    //AuditTrailMgr.addMsg(ocrr, obj);
                    //}
                }
                else
                {
                    OrderCancelReplaceException(obj, $@"Order:{info.orderNo}, Not Found!");
                }

            }
            catch (Exception ex)
            {
                //去掉汉字
                string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
                //obj.errorMsg = msg;
                //obj.errorCode = ErrorCode.ERR_ORDER_0004;
                //obj.code = CommandCode.ORDER;
                //TradeServerFacade.SendString(obj);
                OrderCancelReplaceException(obj, msg);
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        private void OrderException(NetInfo obj, string errorMsg)
        {
            OrderInfo info = new OrderInfo();
            info.MyReadString(obj.infoT);
            obj.errorMsg = errorMsg;
            obj.exchangeCode = info.exchangeCode;
            obj.accountNo = info.accountNo;
            obj.errorCode = ErrorCode.ERR_ORDER_0000;
            obj.code = CommandCode.ORDER;
            TradeServerFacade.SendString(obj);
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
            obj.code = CommandCode.CANCELCAST;

            TradeServerFacade.SendString(obj);
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
            obj.code = CommandCode.MODIFY;

            TradeServerFacade.SendString(obj);
        }

        //public void CancelReplaceOrder(NetInfo obj, ModifyInfo info, string timeInForce)
        //public void CancelReplaceOrder(NetInfo obj, ModifyInfo info)
        //{

        //    try
        //    {
        //        //ModifyInfo info = new ModifyInfo();
        //        //info.MyReadString(obj.infoT);

        //        long clOrdID = Convert.ToInt64(info.orderNo);
        //        RefObj refObj;
        //        bool ret = xReference.TryGetValue(clOrdID, out refObj);
        //        if (ret && refObj.orderStatus != OrdStatus.PENDING_CANCEL && refObj.orderStatus != OrdStatus.PENDING_CANCELREPLACE)
        //        {
        //            QuickFix.FIX42.NewOrderSingle newOrderSingle = refObj.newOrderSingle;
        //            QuickFix.FIX42.OrderCancelReplaceRequest ocrr = new QuickFix.FIX42.OrderCancelReplaceRequest();

        //            //Tag 37
        //            ocrr.OrderID = new OrderID(refObj.orderID);
        //            string lastClOrdID = string.Empty;

        //            var last = refObj.fromClient.Last();
        //            switch (last)
        //            {
        //                case OrderCancelRequest cancelOrder:
        //                    lastClOrdID = cancelOrder.ClOrdID.getValue();
        //                    break;
        //                case NewOrderSingle newOrder:
        //                    lastClOrdID = newOrder.ClOrdID.getValue();
        //                    break;
        //                case OrderCancelReplaceRequest replaceOrder:
        //                    lastClOrdID = replaceOrder.ClOrdID.getValue();
        //                    break;

        //            }
        //            //Tag 41
        //            ocrr.OrigClOrdID = new OrigClOrdID(lastClOrdID);
        //            //Tag 11
        //            long clOrdID = ClOrderIDGen.getNextClOrderID();
        //            ocrr.ClOrdID = new ClOrdID(clOrdID.ToString());
        //            // Tag1
        //            ocrr.Account = new Account(obj.accountNo);
        //            // Tag38
        //            ocrr.OrderQty = new OrderQty(decimal.Parse(info.modifyNumber));
        //            // Tag54
        //            ocrr.Side = QuerySide(info.buySale);




        //            // Tag40 ,不能用QueryOrdType(info.priceType);方法，有的客户端LME交易所不传值
        //            ocrr.OrdType = newOrderSingle.OrdType;
        //            info.priceType = newOrderSingle.OrdType.ToString();

        //            char ordType = Char.Parse(info.priceType);
        //            string symbol = newOrderSingle.Symbol.getValue();
        //            // Tag44
        //            if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
        //            {
        //                decimal prx = CodeTransfer_TT.toGlobexPrx(info.modifyPrice, symbol);
        //                ocrr.Price = new Price(prx);
        //            }


        //            // StopPx
        //            if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
        //            {
        //                decimal prx = CodeTransfer_TT.toGlobexPrx(info.modifyTriggerPrice, symbol);
        //                ocrr.StopPx = new StopPx(prx);
        //            }
        //            //tag 77
        //            ocrr.OpenClose = new OpenClose('O');
        //            //tag 59
        //            ocrr.TimeInForce = newOrderSingle.TimeInForce;

        //            // Tag11028
        //            ocrr.ManualOrderIndicator = moi;





        //            refObj.addClientReq(ocrr);


        //            ret = tradeApp.Send(ocrr);

        //            if (!ret)
        //            {
        //                ModifyResponseInfo minfo = new ModifyResponseInfo();
        //                minfo.orderNo = info.orderNo;
        //                obj.errorMsg = "can not connect to TT server!";
        //                obj.infoT = minfo.MyToString();
        //                obj.exchangeCode = info.exchangeCode;
        //                obj.accountNo = info.accountNo;
        //                obj.errorCode = ErrorCode.ERR_ORDER_0016;
        //                obj.code = CommandCode.MODIFY;

        //                TradeServerFacade.SendString(obj);
        //            }
        //            //else
        //            //{
        //            //    //AuditTrailMgr.addMsg(ocrr, obj);
        //            //}
        //        }
        //        else
        //        {
        //            NotFoundOrderCancelReplaceOperation(obj, info);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.ToString());
        //    }
        //}

        /// <summary>
        /// 改单
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="info"></param>
        //public void cancelReplaceOrderWork(NetInfo obj, ModifyInfo info, string timeInForce)
        //public void cancelReplaceOrderWork(NetInfo obj, ModifyInfo info)
        //{
        //    try
        //    {
        //        //ModifyInfo info = new ModifyInfo();
        //        //info.MyReadString(obj.infoT);

        //        long newOrderClOrdID = Convert.ToInt64(info.orderNo);
        //        RefObj refObj;
        //        bool ret = xReference.TryGetValue(newOrderClOrdID, out refObj);
        //        if (ret)
        //        {
        //            QuickFix.FIX42.NewOrderSingle newOrderSingle = refObj.newOrderSingle;
        //            QuickFix.FIX42.OrderCancelReplaceRequest ocrr = new QuickFix.FIX42.OrderCancelReplaceRequest();

        //            //Tag 37
        //            ocrr.OrderID = new OrderID(refObj.orderID);
        //            string lastClOrdID = string.Empty;

        //            var last = refObj.fromClient.Last();
        //            switch (last)
        //            {
        //                case OrderCancelRequest cancelOrder:
        //                    lastClOrdID = cancelOrder.ClOrdID.getValue();
        //                    break;
        //                case NewOrderSingle newOrder:
        //                    lastClOrdID = newOrder.ClOrdID.getValue();
        //                    break;
        //                case OrderCancelReplaceRequest replaceOrder:
        //                    lastClOrdID = replaceOrder.ClOrdID.getValue();
        //                    break;

        //            }
        //            //Tag 41
        //            ocrr.OrigClOrdID = new OrigClOrdID(lastClOrdID);
        //            //Tag 11
        //            long clOrdID = ClOrderIDGen.getNextClOrderID();
        //            ocrr.ClOrdID = new ClOrdID(clOrdID.ToString());
        //            // Tag1
        //            ocrr.Account = new Account(obj.accountNo);
        //            // Tag38
        //            ocrr.OrderQty = new OrderQty(decimal.Parse(info.modifyNumber));
        //            // Tag54
        //            ocrr.Side = QuerySide(info.buySale);




        //            // Tag40 ,不能用QueryOrdType(info.priceType);方法，有的客户端LME交易所不传值
        //            ocrr.OrdType = newOrderSingle.OrdType;
        //            info.priceType = newOrderSingle.OrdType.ToString();

        //            char ordType = Char.Parse(info.priceType);
        //            string symbol = newOrderSingle.Symbol.getValue();
        //            // Tag44
        //            if (ordType == OrdType.LIMIT || ordType == OrdType.STOP_LIMIT)
        //            {
        //                decimal prx = CodeTransfer_TT.toGlobexPrx(info.modifyPrice, symbol);
        //                ocrr.Price = new Price(prx);
        //            }


        //            // StopPx
        //            if (ordType == OrdType.STOP || ordType == OrdType.STOP_LIMIT)
        //            {
        //                decimal prx = CodeTransfer_TT.toGlobexPrx(info.modifyTriggerPrice, symbol);
        //                ocrr.StopPx = new StopPx(prx);
        //            }
        //            //tag 77
        //            ocrr.OpenClose = new OpenClose('O');
        //            //tag 59
        //            ocrr.TimeInForce = newOrderSingle.TimeInForce;

        //            // Tag11028
        //            ocrr.ManualOrderIndicator = moi;





        //            refObj.addClientReq(ocrr);


        //            ret = tradeApp.Send(ocrr);

        //            if (!ret)
        //            {
        //                ModifyResponseInfo minfo = new ModifyResponseInfo();
        //                minfo.orderNo = info.orderNo;
        //                obj.errorMsg = "can not connect to TT server!";
        //                obj.infoT = minfo.MyToString();
        //                obj.exchangeCode = info.exchangeCode;
        //                obj.accountNo = info.accountNo;
        //                obj.errorCode = ErrorCode.ERR_ORDER_0016;
        //                obj.code = CommandCode.MODIFY;

        //                TradeServerFacade.SendString(obj);
        //            }
        //            //else
        //            //{
        //            //    //AuditTrailMgr.addMsg(ocrr, obj);
        //            //}
        //        }
        //        else
        //        {
        //            Logger.Info($"cancelReplaceOrderWork 订单没找到：{info.MyToString()},newOrderClOrdID:{newOrderClOrdID}");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //去掉汉字
        //        string msg = Regex.IsMatch(ex.Message, @"[\u4e00-\u9fa5]") ? "server exception" : $"server exception:{ex.Message}";
        //        obj.errorMsg = msg;
        //        obj.errorCode = ErrorCode.ERR_ORDER_0004;
        //        obj.code = CommandCode.ORDER;
        //        TradeServerFacade.SendString(obj);
        //        Logger.Error(ex.ToString());
        //    }
        //}



        //public void doCancelRepalceTask(object stateObj)
        //{
        //    try
        //    {
        //        NetInfo obj = (NetInfo)stateObj;
        //        ModifyInfo info = new ModifyInfo();
        //        info.MyReadString(obj.infoT);

        //        long clOrdID = Convert.ToInt64(info.orderNo);
        //        RefObj refObj;
        //        bool ret = xReference.TryGetValue(clOrdID, out refObj);
        //        if (ret)
        //        {
        //            refObj.bizSynLock.Reset();
        //            refObj.bizSynLock.WaitOne();
        //            cancelReplaceOrderWork(obj, info);
        //        }
        //        else
        //        {
        //            NotFoundOrderCancelReplaceOperation(obj, info);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.ToString());
        //    }
        //}




        private Side QuerySide(string zdSide)
        {
            //Side side = null;
            //if (zdSide == "1")
            //    side = new Side(Side.BUY);
            //else if (zdSide == "2")
            //    side = new Side(Side.SELL);

            //return side;

            return new Side(char.Parse(zdSide));
        }

        private string QuerySide(Side side)
        {
            //string sideStr = "";
            //if (side.getValue() == Side.BUY)
            //{
            //    sideStr = "1";
            //}   
            //else
            //{
            //    sideStr = "2";
            //}
            //return sideStr;

            return side.getValue().ToString();
        }

        public string ConvertToZDOrdType(string ttOrdType)
        {
            //客户端用的是FIX 7X和 新TT的FIX版本 不一样
            //两个版本的OrderType值1和2反了，所以
            //此处为了兼容客户端，避免忘记修改
            //TAG40:OrdType
            string zdOrdType = ttOrdType;
            switch (ttOrdType)
            {
                case "1":
                    zdOrdType = "2";
                    break;
                case "2":
                    zdOrdType = "1";
                    break;
                case "4":
                    zdOrdType = "3";
                    break;
                case "3":
                    break;
                case "":
                    break;
            }
            return zdOrdType;


        }

        public string ConvertToTTOrdType(string zdOrdType)
        {
            string ttOrderType = zdOrdType;
            switch (zdOrdType)
            {
                case "1":
                    ttOrderType = "2";
                    break;
                case "2":
                    ttOrderType = "1";
                    break;
                case "3":
                    ttOrderType = "4";
                    break;
                case "4":
                    break;
                default:
                    throw new Exception($"Unsupported OrdType Value :{zdOrdType}");
            }
            return ttOrderType;
        }
        public string ConvertToZDTimeInForce(string ttTimeInForce)
        {
            //TAG59:TimeInForce
            //新TT和老TT不一样
            string zdTimeInForce = ttTimeInForce;
            switch (ttTimeInForce)
            {
                case "0"://当日有效
                    zdTimeInForce = "1";
                    break;
                case "1"://永久有效
                    zdTimeInForce = "2";
                    break;
                case "3"://IOC
                    zdTimeInForce = "4";
                    break;
                case "4":// Fill Or Kill (FOK)
                    zdTimeInForce = "5";
                    break;
            }
            return zdTimeInForce;
        }

        /// <summary>
        /// 如果程序中途重启，之前未完成的订单保从磁盘文件重新加载
        /// 此时NetInfo为空（之前代码逻辑持久化）。
        /// 注：此时就解决不了港交所的T+1时间内的ZDTimeInForce，因为NetInfo丢失。
        /// </summary>
        /// <param name="refObj"></param>
        /// <param name="ttTimeInForce"></param>
        /// <returns></returns>
        public string ConvertToZDTimeInForce(RefObj refObj, string ttTimeInForce)
        {
            if (refObj.NetInfo != null)
            {
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.MyReadString(refObj.NetInfo.infoT);
                return orderInfo.validDate;
            }
            else
            {
                return ConvertToZDTimeInForce(ttTimeInForce);
            }

        }

        public string ConvertToTTTimeInForce(string zdTimeInForce)
        {
            string ttTimeInForce = zdTimeInForce;
            switch (zdTimeInForce)
            {
                case "1"://当日有效
                    ttTimeInForce = "0";
                    break;
                case "2"://永久有效
                    ttTimeInForce = "1";
                    break;
                case "4"://IOC
                    ttTimeInForce = "3";
                    break;
                case "5":// Fill Or Kill (FOK)
                    ttTimeInForce = "4";
                    break;
                case "W":
                    break;
                default:
                    throw new Exception($"Unsupported TimeInForce Value :{zdTimeInForce}");
            }
            return ttTimeInForce;
        }
    }
}
