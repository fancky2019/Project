using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthCommon;
using QuickFix.FIX42;
using System.Threading;
using System.IO;
using QuickFix.Fields;
using QuickFix;
using CommonClassLib.ContractTransfer;
using System.Globalization;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using TTMarketAdapter.Utilities;

namespace TTMarketAdapter
{
    /// <summary>
    /// 和TT通信类（登录、配置文件数据初始化、合约数据获取保存）
    /// </summary>
    class TTPriceCommunication
    {

        private bool _isTestMode = false;
        private MarketApp _marketApp = null;
        private List<SecurityDefinition> _downLoadSecurityDefinition = null;
        volatile int _currentCount = 0;

        /// <summary>
        /// 合约定义请求ID
        /// </summary>
        private int _sdReqIDCnt = 1;
        /// <summary>
        /// 行情数据请求ID
        /// </summary>
        private int _mdReqIDCnt = 1;
        private string lastFuturesReqID = null;
        private string lastSpreadsReqID = null;
        private string lastOptionsReqID = null;
        public int FiltedMonths { get; set; }
        // private const string _configFile = @"config\Quickfix_TTMarketAdapter.cfg";


        /// <summary>
        /// @"config\TT_Secu.dat";
        /// </summary>
        //private string _secu_File = @"config\TT_Secu.dat";
        //public string SECU_OPT_FILE { get => @"config\TT_Secu_Opt.dat"; private set=> SECU_FILE = value; }
        //private string SECU_OPT_FILE { get => @"config\TT_Secu_Opt.dat"; }
        //private string SECU_OPT_FILE => @"config\TT_Secu_Opt.dat";
        //private string _secu_OPT_FILE = @"config\TT_Secu_Opt.dat";


        //登录成功，初始化合约
        private Action _initCompleted;
        /// <summary>
        /// 是否更新合约
        /// </summary>
        bool _updateContract = false;

        ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        #region  init
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTestMode"></param>
        /// <param name="initCompleted">登录成功，初始化合约完成回调</param>
        /// <param name="updateContract"></param>
        public TTPriceCommunication(bool isTestMode, Action initCompleted, bool updateContract = false, int filtedMonths = 50)
        {
            try
            {
                this._initCompleted = initCompleted;
                this._isTestMode = isTestMode;
                this._updateContract = updateContract;
                _downLoadSecurityDefinition = new List<SecurityDefinition>();

                this.FiltedMonths = filtedMonths;
                if (_isTestMode)
                {
                    LoadSecurityDefinitionFromFile();
                }

                #region  TT配置
                // FIX app settings and related
                QuickFix.SessionSettings settings = new QuickFix.SessionSettings(Configurations.Instance.QuickFixConfig);
                // FixV1.5 -begin
                QuickFix.IMessageStoreFactory storeFactory = new QuickFix.FileStoreFactory(settings);
                QuickFix.ILogFactory logFactory = new QuickFix.FileLogFactory(settings);
                _marketApp = new MarketApp(settings);

                QuickFix.IInitiator initiator = new QuickFix.Transport.SocketInitiator(_marketApp, storeFactory, settings, logFactory);
                _marketApp.Initiator = initiator;
                // FixV1.5 -end
                //接收tt推送的合约定义    SecurityDefinition 数据
                _marketApp.SecurityDefEvent += securityDefEventHandler;

                //登录成功回调--取合约数据、保存合约数据
                _marketApp.LogonEvent += new EventHandler<EventArgs>(onLogonEvent);

                #endregion

            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        #region  初始化合约数据从本地文件
        /// <summary>
        /// 初始化合约数据从本地文件
        /// </summary>
        public void LoadSecurityDefinitionFromFile()
        {
            GlobalData.allSecuriryList?.Clear();
            CodeTransfer_TT.zd2TTMapping?.Clear();
            CodeTransfer_TT.tt2ZdMapping?.Clear();
            List<QuickFix.FIX42.SecurityDefinition> futSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            List<QuickFix.FIX42.SecurityDefinition> spreadSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            List<QuickFix.FIX42.SecurityDefinition> optionSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();

            QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            dd.Load(Configurations.Instance.FIX42);

            if (File.Exists(Configurations.Instance.SecurityDefinitionFuture))
            {
                ReadFile(Configurations.Instance.SecurityDefinitionFuture);
                TT.Common.NLogUtility.Info("init instrument from SECU_FILE file data completed.");
            }

            if (File.Exists(Configurations.Instance.SecurityDefinitionOption))
            {
                ReadFile(Configurations.Instance.SecurityDefinitionOption);
                TT.Common.NLogUtility.Info("init instrument from SECU_OPT_FILE file data completed.");
            }
            void ReadFile(string fileName)
            {
                //isInsturmentDownload = true;
                using (StreamReader sReader = new StreamReader(File.Open(fileName, FileMode.Open), System.Text.Encoding.ASCII))
                {
                    while (!sReader.EndOfStream)
                    {
                        string oneLine = sReader.ReadLine().Trim();
                        if (string.IsNullOrEmpty(oneLine))
                            continue;

                        QuickFix.FIX42.SecurityDefinition secDef = new QuickFix.FIX42.SecurityDefinition();
                        secDef.FromString(oneLine, false, null, dd);
                        var securityType = secDef.SecurityType.getValue();
                        switch (securityType)
                        {
                            case "FUT":
                                futSecuDefList.Add(secDef);
                                break;
                            case "MLEG":
                                spreadSecuDefList.Add(secDef);
                                break;
                            case "OPT":
                                optionSecuDefList.Add(secDef);
                                break;
                        }

                    }
                }
            }
            TTMarketAdapter.CodeTransfer_TT.addSecurity("FUT", futSecuDefList);
            TTMarketAdapter.CodeTransfer_TT.addSecurity("MLEG", spreadSecuDefList);
            TTMarketAdapter.CodeTransfer_TT.addSecurity("OPT", optionSecuDefList);
            TT.Common.NLogUtility.Info("init instrument  file   completed");




            OrderBookMgr.Instance.Init();
            _initCompleted?.Invoke();
            CodeTransfer_TT.initPrxFactor(true);
        }

        #endregion

        #endregion

        #region 建立断开Fix 连接
        public void ConnectFixAdapter()
        {
            if (!_isTestMode)
                _marketApp.Start();
        }

        public void disconnectFixAdapter()
        {
            if (!_isTestMode)
                _marketApp.Stop();
        }
        #endregion

        #region   登录成功回调--如果合约文件不存在，下载保存合约文件
        /// <summary>
        /// 登录成功回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void onLogonEvent(object sender, EventArgs e)
        {
            try
            {
                TT.Common.NLogUtility.Info("logo on!");
                //不阻塞主线程
                Task.Factory.StartNew(() =>
                {
                    if (_updateContract)
                    {
                        TT.Common.NLogUtility.Info($"DownloadAndSaveContracts. time={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
                        DownloadAndSaveContracts();
                        //等待合约下载保存完成
                        manualResetEvent.WaitOne();
                        _updateContract = false;
                    }

                    //  TT.Common.Log.Info<TTPriceCommunication>("initSecurityDefinitionFromFile after saveSecurityDefinitionToFile ");
                    //此处逻辑待优化
                    LoadSecurityDefinitionFromFile();

                    //TT.Common.Log.Info<TTPriceCommunication>("请求行情快照和增量数据...... ");
                    //请求合约（增量、快照）数据
                    //foreach (SecurityDefinition sd in CodeTransfer_TT.zd2TTMapping.Values)
                    //{
                    //    SubscribeMarketData(sd);
                    //}
                });

            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }
        #endregion

        #region  发送获取合约数据请求

        public void DownloadAndSaveContracts()
        {
            Task.Factory.StartNew(() =>
            {
                SendSecurityDefinitionRequest();
            });
            Task.Factory.StartNew(() =>
            {
                SaveSecurityDefinitionToFile();
            });
        }

        /// <summary>
        /// 登陆成功之后，发送下载合约数据请求
        /// </summary>
        /// <param name="stateObj"></param>
        public void SendSecurityDefinitionRequest()
        {
            try
            {
                //  Thread.Sleep(4000);


                string strFutures = Configurations.Instance.TargetFutures;
                if (string.IsNullOrEmpty(strFutures))
                {
                    TT.Common.NLogUtility.Info("Waring: no futures configured");
                }
                else
                {
                    TT.Common.NLogUtility.Info("Download futures beginning... ");

                    string[] arrFutures = strFutures.Split(';');

                    SecurityType futureSecuType = new SecurityType("FUT");
                    for (int i = 0; i < arrFutures.Length; i++)
                    {
                        //  List<SecurityDefinition> secuList = new List<SecurityDefinition>();
                        string secReqId = _sdReqIDCnt.ToString();
                        _sdReqIDCnt++;

                        //securityReqDict.Add(secReqId, secuList);
                        string[] arrExchgPrdcd = arrFutures[i].Split(',');
                        querySecurityDefinition(secReqId, arrExchgPrdcd[0], arrExchgPrdcd[1], futureSecuType);
                    }

                    lastFuturesReqID = (_sdReqIDCnt - 1).ToString();
                }

                if (string.IsNullOrEmpty(Configurations.Instance.TargetSpreads))
                {
                    TT.Common.NLogUtility.Info("Waring: no spreads configured");
                }
                else
                {
                    TT.Common.NLogUtility.Info("Download spreads beginning... ");

                    string[] arrSpreads = Configurations.Instance.TargetSpreads.Split(';');

                    SecurityType spreadsSecuType = new SecurityType("MLEG");
                    for (int i = 0; i < arrSpreads.Length; i++)
                    {
                        //   List<SecurityDefinition> secuList = new List<SecurityDefinition>();
                        string secReqId = _sdReqIDCnt.ToString();
                        _sdReqIDCnt++;

                        //  securityReqDict.Add(secReqId, secuList);
                        string[] arrExchgPrdcd = arrSpreads[i].Split(',');
                        querySecurityDefinition(secReqId, arrExchgPrdcd[0], arrExchgPrdcd[1], spreadsSecuType);
                    }

                    lastSpreadsReqID = (_sdReqIDCnt - 1).ToString();
                }

                //添加期权 20180130   fancky
                if (string.IsNullOrEmpty(Configurations.Instance.TargetOptions))
                {
                    TT.Common.NLogUtility.Info("Waring: no TargetOptions configured");
                }
                else
                {
                    TT.Common.NLogUtility.Info("Download TargetOptions beginning... ");

                    string[] arrOptions = Configurations.Instance.TargetOptions.Split(';');

                    SecurityType spreadsSecuType = new SecurityType("OPT");
                    for (int i = 0; i < arrOptions.Length; i++)
                    {
                        //  List<SecurityDefinition> secuList = new List<SecurityDefinition>();
                        string secReqId = _sdReqIDCnt.ToString();
                        _sdReqIDCnt++;

                        //   securityReqDict.Add(secReqId, secuList);
                        string[] arrExchgPrdcd = arrOptions[i].Split(',');
                        querySecurityDefinition(secReqId, arrExchgPrdcd[0], arrExchgPrdcd[1], spreadsSecuType);
                        TT.Common.NLogUtility.Info($"REQUEST {arrExchgPrdcd[0]}");
                    }

                    lastOptionsReqID = (_sdReqIDCnt - 1).ToString();
                }

            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 发送获取合约数据请求
        /// </summary>
        /// <param name="secReqId"></param>
        /// <param name="exchg"></param>
        /// <param name="symbol"></param>
        /// <param name="secuType"></param>
        public void querySecurityDefinition(string secReqId, string exchg, string symbol, SecurityType secuType)
        {
            try
            {
                QuickFix.FIX42.SecurityDefinitionRequest secDefReq = new QuickFix.FIX42.SecurityDefinitionRequest();
                secDefReq.SecurityReqID = new SecurityReqID(secReqId);
                secDefReq.SecurityExchange = new SecurityExchange(exchg);
                secDefReq.Symbol = new Symbol(symbol); //"ES"));
                secDefReq.SecurityType = secuType; //new SecurityType("FUT"));

                bool ret = _marketApp.Send(secDefReq);

                if (!ret)
                {
                    TT.Common.NLogUtility.Info(string.Format("queryInstrument() fail: symbol:{0}, secuType:{1}", symbol, secuType.getValue()));
                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }

        }
        #endregion

        #region  接收合约数据--合约定义

        /// <summary>
        ///  接收合约 （SecurityDefinition） 数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void securityDefEventHandler(SecurityDefinition secuDefMsg)
        {
            try
            {
                if (secuDefMsg.IsSetText())
                {
                    TT.Common.NLogUtility.Info(secuDefMsg.ToString());
                    return;
                }
                //if (secuDefMsg.SecurityType.getValue() == "MLEG" && secuDefMsg.Symbol.getValue() == "6A")
                //{

                //}
                _currentCount++;
                //QuickFix.FIX42.SecurityDefinition secuDefMsg = e.SecurityDefMsg;
                // errorLogger.log(3, secuDefMsg.ToString());
                // Todo...
                //Console.WriteLine("secu id:" + secuDefMsg.SecurityID + ", symbol:" + secuDefMsg.Symbol + ", " + secuDefMsg.MaturityMonthYear);

                string secReqID = secuDefMsg.SecurityReqID.getValue();
                //if (securityReqDict.ContainsKey(secReqID))
                //{
                // errorLogger.log(3, $"securityReqDict.ContainsKey(secReqID) secReqID={secReqID}");
                //     List<SecurityDefinition> secuList = new List<SecurityDefinition>();// securityReqDict[secReqID];

                int totalNumSecus = secuDefMsg.TotalNumSecurities.getValue();
                string responseID = secuDefMsg.SecurityResponseID.getValue();

                string currentSecuIdx = responseID.Split(':')[1];
                int intCurrentSecuIdx = int.Parse(currentSecuIdx);

                //// filter out non 3M contracts of LME
                if (secuDefMsg.SecurityExchange.getValue() == "LME")
                {
                    Group g2 = secuDefMsg.GetGroup(2, Tags.NoSecurityAltID);
                    var secuAltID = g2.GetString(Tags.SecurityAltID);
                    if (secuAltID.IndexOf("3M") > -1)
                    {
                        _downLoadSecurityDefinition.Add(secuDefMsg);
                        TT.Common.NLogUtility.Info($" redeive {_downLoadSecurityDefinition.Count} contracts!");
                    }
                }
                else
                {



                    //int intSecReqID = int.Parse(secReqID);
                    var securityType = secuDefMsg.SecurityType.getValue();
                    //if (intSecReqID < SPREADS_REQ_BEGIN)
                    if (securityType == "FUT")
                    {
                        //fancky  修改 20180518
                        //过滤只取月的合约，如果没有tag:18211,直接添加
                        if (!secuDefMsg.IsSetDeliveryTerm())//如果没有设置18211
                            _downLoadSecurityDefinition.Add(secuDefMsg);
                        else
                        {
                            if (secuDefMsg.DeliveryTerm.getValue() == 'M')//只添加M的合约
                            {
                                _downLoadSecurityDefinition.Add(secuDefMsg);
                                TT.Common.NLogUtility.Info($" redeive {_downLoadSecurityDefinition.Count} contracts!");
                            }
                        }
                    }
                    //else if (intSecReqID < OPTIONS_REQ_BEGIN)
                    else if (securityType == "MLEG")
                    {
                        //// here is spread
                        //int legCnt = secuDefMsg.NoRelatedSym.getValue();
                        int legCnt = secuDefMsg.NoLegs.getValue();

                        // We can only support two legs spread
                        if (legCnt == 2)
                        {

                            Group g1 = secuDefMsg.GetGroup(1, Tags.NoLegs);
                            Group g2 = secuDefMsg.GetGroup(2, Tags.NoLegs);



                            if (g1.GetField(Tags.LegDeliveryTerm) == "M" && g1.GetString(Tags.LegDeliveryTerm) == "M")
                            {
                                //FUT，MLEG
                                if (g1.GetField(Tags.LegSecurityType) != "OPT" && g2.GetField(Tags.LegSecurityType) != "OPT")
                                {
                                    _downLoadSecurityDefinition.Add(secuDefMsg);
                                    TT.Common.NLogUtility.Info($" redeive {_downLoadSecurityDefinition.Count} contracts!");
                                }
                            }
                        }
                    }
                    else  //OPT
                    {

                        TT.Common.NLogUtility.Info($" redeive {_downLoadSecurityDefinition.Count} contracts!");
                        _downLoadSecurityDefinition.Add(secuDefMsg);
                    }

                    //int futCount = _downLoadSecurityDefinition.Where(p => p.SecurityType.getValue() == "FUT").Count();
                    //if (intCurrentSecuIdx + 1 == totalNumSecus)
                    //{
                    //    ////将接收到的数据保存到内存 GlobalData.allSecuriryList
                    //    //CodeTransfer_TT.addSecurity(secReqID, secuList);
                    //    string msg = "secReqID=" + secReqID + " downland completed!";
                    //    Console.WriteLine(msg);

                    //    if (secReqID == lastFuturesReqID)
                    //        Logger.Info("Securities for future download completed!");
                    //    else if (secReqID == lastSpreadsReqID)
                    //        Logger.Info("Securities for spread download completed!");
                    //    else if (secReqID == lastOptionsReqID)
                    //        Logger.Info("Securities for option download completed!");

                    //    // ThreadPool.QueueUserWorkItem(new WaitCallback(saveInstrumentToFile));
                    //    Logger.Info($" redeive {totalNumSecus} contracts!");
                    //}

                }
                //else
                //{
                //    Logger.Info(string.Format("reqId:{0} not find in securityReqDict, symbol:{1}", secReqID, secuDefMsg.Symbol));
                //}

            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(secuDefMsg.ToString());
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }
        #endregion

        #region 保存合约定义数据
        /// <summary>
        /// 保存合约  （SecurityDefinition）数据
        /// </summary>
        /// <param name="stateObj"></param>
        private void SaveSecurityDefinitionToFile()
        {
            try
            {
                int _lastCount = 0;
                int _sameCountTimes = 0;
                while (true)
                {
                    if (_currentCount != 0)
                    {
                        if (_currentCount != _lastCount)
                        {
                            _lastCount = _currentCount;
                            _sameCountTimes = 0;
                            Thread.Sleep(100);
                            continue;
                        }
                        else
                        {
                            _sameCountTimes++;
                            TT.Common.NLogUtility.Info($"_sameCountTimes ={ _sameCountTimes}");
                            if (_sameCountTimes == 3)
                            {
                                if (_downLoadSecurityDefinition.Count == 0)
                                {
                                    Thread.Sleep(100);
                                    _sameCountTimes = 0;
                                    continue;
                                }
                                TT.Common.NLogUtility.Info("starting save contracts......");
                                break;
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(500);
                        continue;
                    }

                    TT.Common.NLogUtility.Info($"Thread waits 2 seconds");
                    Thread.Sleep(2000);
                }

                SaveDownLoadContracts();
                _currentCount = 0;
                _downLoadSecurityDefinition.Clear();

                #region
                //Console.WriteLine("starting save contracts......");

                //Console.WriteLine("starting compare contracts......");
                //Logger.Info("compareContract:    starting compare old contracts");
                //List<QuickFix.FIX42.SecurityDefinition> futSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
                //List<QuickFix.FIX42.SecurityDefinition> spreadSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
                //List<QuickFix.FIX42.SecurityDefinition> optionSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
                //QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
                //dd.Load(@"config\FIX42.xml");
                //if (File.Exists(SECU_FILE))
                //{
                //    ReadFile(SECU_FILE);
                //    Logger.Info("compareContract:   init instrument from SECU_FILE file data completed.");
                //}

                //if (File.Exists(SECU_OPT_FILE))
                //{
                //    ReadFile(SECU_OPT_FILE);
                //    Logger.Info("compareContract:    init instrument from SECU_OPT_FILE file data completed.");
                //}
                ////待优化
                //void ReadFile(string fileName)
                //{

                //    using (StreamReader sReader = new StreamReader(File.Open(fileName, FileMode.Open), System.Text.Encoding.ASCII))
                //    {
                //        while (!sReader.EndOfStream)
                //        {
                //            string oneLine = sReader.ReadLine().Trim();
                //            if (string.IsNullOrEmpty(oneLine))
                //                continue;

                //            QuickFix.FIX42.SecurityDefinition secDef = new QuickFix.FIX42.SecurityDefinition();
                //            secDef.FromString(oneLine, false, null, dd);

                //            if (secDef.SecurityType.getValue() == "FUT")
                //                futSecuDefList.Add(secDef);
                //            else if (secDef.SecurityType.getValue() == "MLEG")
                //                spreadSecuDefList.Add(secDef);
                //            else if (secDef.SecurityType.getValue() == "OPT")
                //            {
                //                optionSecuDefList.Add(secDef);
                //            }
                //        }
                //    }
                //}


                ////老合约
                //List<QuickFix.FIX42.SecurityDefinition> oldSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
                //oldSecuDefList.AddRange(futSecuDefList);
                //oldSecuDefList.AddRange(spreadSecuDefList);
                //oldSecuDefList.AddRange(optionSecuDefList);
                ////老合约未过期的
                //var unExpiredDic = new Dictionary<string, SecurityDefinition>();
                //var oldDic = ZDCodeSecurityDefinitionDic(oldSecuDefList);
                //foreach (var key in oldDic.Keys)
                //{
                //    if (!IsExpiredContract(key, oldDic[key]))
                //    {
                //        unExpiredDic.Add(key, oldDic[key]);
                //    }
                //}
                //Logger.Info($"compareContract: unExpiredDic.Count={unExpiredDic.Keys.Count}");
                ////总的合约
                //List<QuickFix.FIX42.SecurityDefinition> totalSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
                //totalSecuDefList.AddRange(GlobalData.allSecuriryList);
                //var newDic = ZDCodeSecurityDefinitionDic(totalSecuDefList);
                //foreach (var key in unExpiredDic.Keys)
                //{
                //    if (!newDic.ContainsKey(key))
                //    {
                //        Logger.Info($"compareContract:  can't get contract {key}");
                //        Logger.Info(oldDic[key].ToString());
                //        totalSecuDefList.Add(oldDic[key]);
                //    }
                //}
                //Console.WriteLine("comparing contracts is completed......");
                //GlobalData.allSecuriryList = totalSecuDefList;
                //Logger.Info("compareContract:   backup old contract files.");
                ////备份之前文件
                //var extension = Path.GetExtension(SECU_FILE);
                //if (File.Exists(SECU_FILE))
                //{
                //    Logger.Info("compareContract:   backup " + SECU_FILE);
                //    File.Move(SECU_FILE, @"config\" + Path.GetFileNameWithoutExtension(SECU_FILE) + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + extension);
                //}
                //if (File.Exists(SECU_OPT_FILE))
                //{
                //    Logger.Info("compareContract:   backup " + SECU_OPT_FILE);
                //    File.Move(SECU_OPT_FILE, @"config\" + Path.GetFileNameWithoutExtension(SECU_OPT_FILE) + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + extension);
                //}
                //Console.WriteLine("backup old contract files completed......");



                //Logger.Info($"start save contract file, GlobalData.allSecuriryList.Count={GlobalData.allSecuriryList.Count}!");
                //var futList = new List<SecurityDefinition>();
                //var spreadList = new List<SecurityDefinition>();
                //var optList = new List<SecurityDefinition>();
                //GlobalData.allSecuriryList.ForEach(p =>
                //{
                //    var type = p.SecurityType.getValue();
                //    switch (type)
                //    {
                //        case "FUT":
                //            futList.Add(p);
                //            break;
                //        case "MLEG":
                //            spreadList.Add(p);
                //            break;
                //        case "OPT":
                //            optList.Add(p);
                //            break;
                //        default:
                //            Logger.Info($"default type={type}");
                //            break;
                //    }
                //});
                //Console.WriteLine($"before Filtrate Contracts, futList:{futList.Count} spreadList:{spreadList.Count} optList:{optList.Count}");
                //Logger.Info($"before Filtrate Contracts,  futList:{futList.Count} spreadList:{spreadList.Count} optList:{optList.Count}");
                //Console.WriteLine("starting Filtrate Contracts ......");
                //FiltrateAndSaveContracts(futList, spreadList, optList);
                //manualResetEvent.Set();
                #endregion
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        private void SaveDownLoadContracts()
        {

            TT.Common.NLogUtility.Info("starting save contracts......");

            TT.Common.NLogUtility.Info("starting compare contracts......");
            TT.Common.NLogUtility.Info("compareContract:    starting compare old contracts");
            List<QuickFix.FIX42.SecurityDefinition> futSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            List<QuickFix.FIX42.SecurityDefinition> spreadSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            List<QuickFix.FIX42.SecurityDefinition> optionSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            dd.Load(Configurations.Instance.FIX42);
            if (File.Exists(Configurations.Instance.SecurityDefinitionFuture))
            {
                ReadFile(Configurations.Instance.SecurityDefinitionFuture);
                TT.Common.NLogUtility.Info("compareContract:   init instrument from SECU_FILE file data completed.");
            }

            if (File.Exists(Configurations.Instance.SecurityDefinitionOption))
            {
                ReadFile(Configurations.Instance.SecurityDefinitionOption);
                TT.Common.NLogUtility.Info("compareContract:    init instrument from SECU_OPT_FILE file data completed.");
            }
            //待优化
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
                            futSecuDefList.Add(secDef);
                        else if (secDef.SecurityType.getValue() == "MLEG")
                            spreadSecuDefList.Add(secDef);
                        else if (secDef.SecurityType.getValue() == "OPT")
                        {
                            optionSecuDefList.Add(secDef);
                        }
                    }
                }
            }


            //老合约
            List<QuickFix.FIX42.SecurityDefinition> oldSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            oldSecuDefList.AddRange(futSecuDefList);
            oldSecuDefList.AddRange(spreadSecuDefList);
            oldSecuDefList.AddRange(optionSecuDefList);
            //老合约未过期的
            var unExpiredDic = new Dictionary<string, SecurityDefinition>();
            var oldDic = ZDCodeSecurityDefinitionDic(oldSecuDefList);
            foreach (var key in oldDic.Keys)
            {
                if (!IsExpiredContract(key, oldDic[key]))
                {
                    unExpiredDic.Add(key, oldDic[key]);
                }
            }
            TT.Common.NLogUtility.Info($"compareContract: unExpiredDic.Count={unExpiredDic.Keys.Count}");
            //总的合约
            List<QuickFix.FIX42.SecurityDefinition> totalSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            //totalSecuDefList.AddRange(GlobalData.allSecuriryList);
            totalSecuDefList.AddRange(_downLoadSecurityDefinition);
            var newDic = ZDCodeSecurityDefinitionDic(totalSecuDefList);
            foreach (var key in unExpiredDic.Keys)
            {
                if (!newDic.ContainsKey(key))
                {
                    TT.Common.NLogUtility.Info($"compareContract:  can't get contract {key}");
                    TT.Common.NLogUtility.Info(oldDic[key].ToString());
                    totalSecuDefList.Add(oldDic[key]);
                }
            }
            TT.Common.NLogUtility.Info("comparing contracts is completed......");
            //GlobalData.allSecuriryList = totalSecuDefList;
            TT.Common.NLogUtility.Info("compareContract:   backup old contract files.");
            //备份之前文件
            var extension = Path.GetExtension(Configurations.Instance.SecurityDefinitionFuture);
            if (File.Exists(Configurations.Instance.SecurityDefinitionFuture))
            {
                TT.Common.NLogUtility.Info("compareContract:   backup " + Configurations.Instance.SecurityDefinitionFuture);
                File.Move(Configurations.Instance.SecurityDefinitionFuture, @"config\" + Path.GetFileNameWithoutExtension(Configurations.Instance.SecurityDefinitionFuture) + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + extension);
            }
            if (File.Exists(Configurations.Instance.SecurityDefinitionOption))
            {
                TT.Common.NLogUtility.Info("compareContract:   backup " + Configurations.Instance.SecurityDefinitionOption);
                File.Move(Configurations.Instance.SecurityDefinitionOption, @"config\" + Path.GetFileNameWithoutExtension(Configurations.Instance.SecurityDefinitionOption) + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + extension);
            }
            TT.Common.NLogUtility.Info("backup old contract files completed......");



            TT.Common.NLogUtility.Info($"start save contract file, GlobalData.allSecuriryList.Count={GlobalData.allSecuriryList.Count}!");
            var futList = new List<SecurityDefinition>();
            var spreadList = new List<SecurityDefinition>();
            var optList = new List<SecurityDefinition>();
            totalSecuDefList.ForEach(p =>
            {
                var type = p.SecurityType.getValue();
                switch (type)
                {
                    case "FUT":
                        futList.Add(p);
                        break;
                    case "MLEG":
                        spreadList.Add(p);
                        break;
                    case "OPT":
                        optList.Add(p);
                        break;
                    default:
                        TT.Common.NLogUtility.Info($"default type={type}");
                        break;
                }
            });
            TT.Common.NLogUtility.Info($"before Filtrate Contracts, futList:{futList.Count} spreadList:{spreadList.Count} optList:{optList.Count}");
            TT.Common.NLogUtility.Info("starting Filtrate Contracts ......");
            FiltrateAndSaveContracts(futList, spreadList, optList);
            manualResetEvent.Set();
        }

        private Dictionary<string, SecurityDefinition> ZDCodeSecurityDefinitionDic(List<SecurityDefinition> list)
        {
            Dictionary<string, SecurityDefinition> dic = new Dictionary<string, SecurityDefinition>();
            list.ForEach(p =>
            {

                try
                {
                    CodeBean codeBean = new CodeBean();
                    string upperExchg = p.SecurityExchange.getValue();
                    string upperPrdCd = p.Symbol.getValue();
                    string uppperKey = upperExchg + "," + upperPrdCd;

                    string securityType = p.SecurityType.getValue();
                    switch (securityType)
                    {
                        case "FUT":
                            if (CodeTransfer_TT.mismatchPrdCd.ContainsKey(uppperKey))
                                codeBean.zdProduct = CodeTransfer_TT.mismatchPrdCd[uppperKey];
                            else
                                codeBean.zdProduct = upperPrdCd;
                            CodeTransfer_TT.transferFutureToZDCode(codeBean, p);
                            break;
                        case "MLEG":
                            string zdCode = null;
                            string zdPrdCd = null;
                            if (CodeTransfer_TT.mismatchPrdCd.ContainsKey(uppperKey))
                            {
                                zdPrdCd = CodeTransfer_TT.mismatchPrdCd[uppperKey];
                                zdCode = zdPrdCd + "_S";
                                codeBean.zdProduct = zdCode;
                            }
                            else
                            {
                                zdPrdCd = upperPrdCd;
                                zdCode = zdPrdCd + "_S";
                                codeBean.zdProduct = zdCode;
                            }
                            CodeTransfer_TT.transferSpreadToZDCode(zdCode, codeBean, p);
                            break;
                        case "OPT":
                            int putOrCall = p.PutOrCall.getValue();
                            if (CodeTransfer_TT.mismatchPrdCd.ContainsKey(uppperKey))
                                codeBean.zdProduct = CodeTransfer_TT.mismatchPrdCd[uppperKey];
                            else
                                codeBean.zdProduct = upperPrdCd;
                            string zdProduct = codeBean.zdProduct + "_" + (putOrCall == 0 ? "P" : "C");
                            codeBean.upperExchg = upperExchg;
                            codeBean.upperProduct = upperPrdCd;
                            codeBean.zdProduct = zdProduct;
                            codeBean.contractType = securityType;
                            CodeTransfer_TT.transferOptionToZDCode(codeBean, p);
                            break;
                    }

                    if (!string.IsNullOrEmpty(codeBean.zdCode))
                    {
                        if (!dic.Keys.Contains(codeBean.zdCode))
                        {
                            dic.Add(codeBean.zdCode, p);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            });
            return dic;
        }

        private bool IsExpiredContract(string contract, SecurityDefinition sd)
        {
            bool result = true;

            try
            {
                //if (contract.EndsWith("3M"))
                //    return false;
                //else
                //{

                //    //if (contract.IndexOf("_S") > 0)
                //    //{
                //    //    //NK_S1505-1903
                //    //    int idx = contract.Length - 9;
                //    //    string leg1 = contract.Substring(idx, 4);
                //    //    string leg2 = contract.Substring(idx + 5, 4);

                //    //    return isExpiredLeg(leg1) || isExpiredLeg(leg2);
                //    //}
                //    //else if (contract.Contains("_C") || contract.Contains("_P"))
                //    //{

                //    //    QuickFix.Group g = sd.GetGroup(1, Tags.NoEvents);
                //    //    //866=20181217
                //    //    string eventDate = g.GetString(Tags.EventDate);
                //    //    return DateTime.ParseExact(eventDate, "yyyyMMdd", CultureInfo.InvariantCulture) > DateTime.Now;
                //    //}
                //    //else
                //    //{
                //    //    int idx = contract.Length - 4;
                //    //    string leg1 = contract.Substring(idx, 4);
                //    //    return isExpiredLeg(leg1);
                //    //}


                //}

                //541=20190528
                var maturityDateStr = sd.MaturityDate.getValue();
                DateTime maturityDate = DateTime.ParseExact(maturityDateStr, "yyyyMMdd", CultureInfo.InvariantCulture);
                result = maturityDate < DateTime.Now;
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(sd.ToString());
                TT.Common.NLogUtility.Error(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 过滤合约保存使得（FUT+SPREAD<=2000,OPT<=1000)
        /// </summary>
        /// <param name="futureList"></param>
        /// <param name="spreadList"></param>
        /// <param name="optionList"></param>
        void FiltrateAndSaveContracts(List<SecurityDefinition> futureList, List<SecurityDefinition> spreadList, List<SecurityDefinition> optionList)
        {

            //Server.MapPath("/")    返回路径为：E:\wwwroot
            List<SecurityDefinition> mathchSpreadList = new List<SecurityDefinition>();
            List<SecurityDefinition> mathchOptList = optionList;// new List<SecurityDefinition>();

            //int addMonth = (int)nudMonths.Value;

            DateTime later = DateTime.Now.AddMonths(FiltedMonths);

            //过滤spread
            int spreadLaterMonth = FiltedMonths;
            while (true)
            {
                mathchSpreadList = GetSpecifyMonthContracts(spreadList);
                if (mathchSpreadList.Count + futureList.Count > 2000)
                {
                    later = DateTime.Now.AddMonths(--spreadLaterMonth);
                }
                else
                {
                    break;
                }
            }

            //过滤opt
            later = DateTime.Now.AddMonths(FiltedMonths);
            int optLaterMonth = FiltedMonths;
            while (true)
            {
                mathchOptList = GetSpecifyMonthContracts(optionList);
                if (mathchOptList.Count > 10000)
                {
                    later = DateTime.Now.AddMonths(--optLaterMonth);
                }
                else
                {
                    break;
                }
            }


            List<SecurityDefinition> GetSpecifyMonthContracts(List<SecurityDefinition> sourceList)
            {
                List<SecurityDefinition> mathchList = new List<SecurityDefinition>();
                //排序，防止接收到的合约是不是按照合约到期日排序的
                sourceList = sourceList.OrderBy(p => p.GetField(Tags.MaturityMonthYear)).ToList();
                foreach (SecurityDefinition sd in sourceList)
                {

                    //合约到期时间
                    var maturityYearMonth = string.Empty;
                    if (sd.IsSetMaturityMonthYear())
                    {
                        maturityYearMonth = sd.GetField(Tags.MaturityMonthYear);
                    }
                    else if (sd.IsSetContractYearMonth())
                    {
                        maturityYearMonth = sd.GetField(Tags.ContractYearMonth);
                    }
                    else
                    {
                        //GetGroup  第一个参数索引从1开始
                        QuickFix.Group group = sd.GetGroup(1, Tags.NoLegs);
                        if (group.IsSetField(Tags.LegContractYearMonth))
                        {
                            maturityYearMonth = group.GetString(Tags.LegContractYearMonth);
                        }
                        else
                        {
                            //log  here
                            continue;
                        }
                    }
                    DateTime maturity = DateTime.ParseExact(maturityYearMonth, "yyyyMM", CultureInfo.InvariantCulture);
                    if (later >= maturity)
                    {
                        mathchList.Add(sd);
                    }
                    else
                    {

                    }
                }
                return mathchList;
            }

            TT.Common.NLogUtility.Info(" Filtrate Contracts completed......");
            TT.Common.NLogUtility.Info($"after Filtrate Contracts, futList:{futureList.Count} spreadList:{mathchSpreadList.Count} optList:{mathchOptList.Count}");
            TT.Common.NLogUtility.Info($"after Filtrate Contracts, futList:{futureList.Count} spreadList:{mathchSpreadList.Count} optList:{mathchOptList.Count}");
            string futFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config/TT_Secu.dat");
            string optFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config/TT_Secu_Opt.dat");

            TT.Common.NLogUtility.Info("Beginning saving filtrated contracts ......");
            TT.Common.NLogUtility.Info("Beginning saving filtrated contracts ......");
            WriteDada(futFileName, futureList.Concat(mathchSpreadList).ToList());
            WriteDada(optFileName, mathchOptList);
            void WriteDada(string fileName, List<SecurityDefinition> data)
            {
                if (data.Count > 0)
                {
                    using (StreamWriter sw = new StreamWriter(File.Open(fileName, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
                    {
                        data.ForEach(p =>
                        {
                            sw.WriteLine(p);
                        });
                    }
                }
            }
            TT.Common.NLogUtility.Info("Saving filtrated contracts  completed......");

            //  System.Diagnostics.Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config"));
        }
        #endregion

        #region 发送取合约（增量、快照）数据请求(tag=269) reqMarketIncremental
        /// <summary>
        /// 订阅行情数据
        ///  发送取合约（增量、快照）数据请求
        /// </summary>
        /// <param name="sd"></param>
        public void SubscribeMarketData(SecurityDefinition sd)
        {
            try
            {

                MarketDataRequest mdr = new MarketDataRequest();
                mdr.MDReqID = new MDReqID(_mdReqIDCnt.ToString());
                _mdReqIDCnt++;
                ////TAG：116
                //mdr.OnBehalfOfSubID = new OnBehalfOfSubID(Configurations.OnBehalfOfSubID);
                MarketDataRequest.NoRelatedSymGroup symGrp = new MarketDataRequest.NoRelatedSymGroup();
                symGrp.SetField(sd.Symbol);
                symGrp.SetField(sd.SecurityExchange);
                symGrp.SetField(sd.SecurityID);
                symGrp.SetField(sd.SecurityType);


                mdr.AddGroup(symGrp);

                //---------------------------------------------

                mdr.SubscriptionRequestType = new SubscriptionRequestType(SubscriptionRequestType.SNAPSHOT_PLUS_UPDATES);
                mdr.MarketDepth = new MarketDepth(5);
                mdr.AggregatedBook = new AggregatedBook(AggregatedBook.YES);
                mdr.MDUpdateType = new MDUpdateType(MDUpdateType.INCREMENTAL_REFRESH);

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp1 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp1.MDEntryType = new MDEntryType(MDEntryType.BID);

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp2 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp2.MDEntryType = new MDEntryType(MDEntryType.OFFER);

                /*
                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp3 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp3.MDEntryType = new MDEntryType(MDEntryType.IMPLIED_BID);

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp4 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp4.MDEntryType = new MDEntryType(MDEntryType.IMPLIED_ASK);
                */

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp5 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp5.MDEntryType = new MDEntryType(MDEntryType.TRADE);

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp7 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp7.MDEntryType = new MDEntryType(MDEntryType.OPENING_PRICE);

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp8 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp8.MDEntryType = new MDEntryType(MDEntryType.TRADING_SESSION_HIGH_PRICE);

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp9 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp9.MDEntryType = new MDEntryType(MDEntryType.TRADING_SESSION_LOW_PRICE);

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp10 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp10.MDEntryType = new MDEntryType(MDEntryType.SETTLEMENT_PRICE);

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp11 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp11.MDEntryType = new MDEntryType(MDEntryType.CLOSING_PRICE);

                /*
                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp12 = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeGrp12.MDEntryType = new MDEntryType(MDEntryType.INDICATIVE_SETTLE);
                */

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp12 = new MarketDataRequest.NoMDEntryTypesGroup();
                //entryTypeGrp12.MDEntryType = new MDEntryType(MDEntryType.INDICATIVE_BID);
                entryTypeGrp12.MDEntryType = new MDEntryType(MDEntryType.IMPLIED_BID);

                MarketDataRequest.NoMDEntryTypesGroup entryTypeGrp13 = new MarketDataRequest.NoMDEntryTypesGroup();
                //entryTypeGrp13.MDEntryType = new MDEntryType(MDEntryType.INDICATIVE_ASK);
                entryTypeGrp13.MDEntryType = new MDEntryType(MDEntryType.IMPLIED_ASK);

                // MDEntryType="B" 
                MarketDataRequest.NoMDEntryTypesGroup entryTypeTradeVolume = new MarketDataRequest.NoMDEntryTypesGroup();
                entryTypeTradeVolume.MDEntryType = new MDEntryType(MDEntryType.TRADE_VOLUME);



                mdr.AddGroup(entryTypeGrp1);
                mdr.AddGroup(entryTypeGrp2);
                /*
                mdr.AddGroup(entryTypeGrp3);
                mdr.AddGroup(entryTypeGrp4);
                */

                mdr.AddGroup(entryTypeGrp5);
                mdr.AddGroup(entryTypeGrp7);
                mdr.AddGroup(entryTypeGrp8);
                mdr.AddGroup(entryTypeGrp9);
                mdr.AddGroup(entryTypeGrp10);

                mdr.AddGroup(entryTypeGrp11);
                mdr.AddGroup(entryTypeGrp12);
                mdr.AddGroup(entryTypeGrp13);
                mdr.AddGroup(entryTypeTradeVolume);

                bool ret = _marketApp.Send(mdr);

                if (!ret)
                {
                    TT.Common.NLogUtility.Info("reqMarket() fail");
                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }
        #endregion

    }
}
