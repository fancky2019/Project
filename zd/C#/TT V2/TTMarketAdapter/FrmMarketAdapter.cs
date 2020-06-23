using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AuthCommon;
using System.IO;
using System.Threading.Tasks;
using History.Lib;
using CommonClassLib;
using CommonClassLib.ContractTransfer;
using QuickFix.FIX42;
using QuickFix.Fields;
using System.Configuration;
using System.Globalization;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using TT.Common;
using TTMarketAdapter.Utilities;
using System.Runtime.Serialization.Formatters.Binary;

namespace TTMarketAdapter
{
    public partial class FrmMarketAdapter : Form
    {

        #region  字段
        //  private ZDLogger errorLogger = null;
        private TTPriceCommunication _tTPriceCommunication = null;

        /// <summary>
        ///key: eventID, value, list of zdProductCode
        /// </summary>
        private Dictionary<int, List<string>> _eventDict = null;

        FrmLogAnalysis _frmLogAnalysis;

        /// <summary>
        /// 避免多次注册job事件
        /// </summary>
        private bool _registerJob;
        string _preveTodaySett = @"config/PreveTodaySett.data";
        string _tradeVolumeData = @"config/TradeVolumeData.txt";

        #endregion

        public FrmMarketAdapter()
        {
            InitializeComponent();
            _eventDict = new Dictionary<int, List<string>>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Configurations.SecurityDefinitionFuture))
            {
                this.cbUpdateContract.Checked = true;
                this.cbUpdateContract.Enabled = false;
            }


            ////应用程序自动退出
            //Task.Factory.StartNew(() =>
            //{
            //    if (!string.IsNullOrEmpty(Configurations.ApplicationExitTime))
            //    {
            //        while (true)
            //        {
            //            TimeSpan timeSpan;
            //            if (TimeSpan.TryParse(Configurations.ApplicationExitTime, out timeSpan))
            //            {
            //                if (DateTime.Now.Hour == timeSpan.Hours && DateTime.Now.Minute == timeSpan.Minutes && DateTime.Now.Second == timeSpan.Seconds)
            //                {
            //                    if (!_serviceStopped)
            //                    {
            //                        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            //                        if (this.InvokeRequired)
            //                        {
            //                            this.BeginInvoke((MethodInvoker)(() =>
            //                            {
            //                                btnStop_Click(null, null);
            //                                autoResetEvent.Set();
            //                            }));
            //                            autoResetEvent.WaitOne(60000);
            //                        }
            //                        else
            //                        {
            //                            btnStop_Click(null, null);
            //                        }
            //                    }
            //                    Environment.Exit(0);
            //                    if (!_serviceStopped)
            //                    {
            //                        if (this.InvokeRequired)
            //                        {
            //                            this.Invoke((MethodInvoker)(() =>
            //                            {
            //                                btnStop_Click(null, null);
            //                            }));
            //                        }
            //                        else
            //                        {
            //                            btnStop_Click(null, null);
            //                        }
            //                    }

            //                }
            //            }

            //        }
            //    }
            //});
        }

        #region  启动TT服务
        /// <summary>
        /// 启动TT服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Configurations.LogCacheSize, out int cacheSize))
            {
                LogAsync.CacheSize = cacheSize;
            }

            if (int.TryParse(Configurations.TimerInterval, out int timerInterval))
            {
                LogAsync.TimerInterval = timerInterval;
            }

            if (Configurations.NewMDBool)
            {
                MDSocket.GetInstance();
            }

            _tTPriceCommunication = new TTPriceCommunication(cbDebugMode.Checked, () =>
             {
                 //期货更新
                 if (!_registerJob)
                 {
                     UpdateMarketData();
                     _registerJob = true;
                 }
                 else
                 {
                     ReloginSubscribeMarketData();
                 }

                 btnExportOperations_Click(null, null);

             }, this.cbUpdateContract.Checked, (int)nudMonths.Value);
            cbDebugMode.Enabled = false;

            //连接TT服务器
            _tTPriceCommunication.ConnectFixAdapter();
            TT.Common.NLogUtility.Info("启动服务！");

            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
        }
        #endregion
        //EventTriggerV3 eventTrigger = null;
        //交易所品种的开盘时间
        List<(string TTExchange, string TTProduct, TimeSpan MarketTime)> exchangeProductOpeningTime = new List<(string TTExchange, string TTProduct, TimeSpan MarketTime)>();

        #region 结算价定时清除任务、保存结算价、压缩发送日志
        /// <summary>
        /// 结算价定时清除任务、保存结算价、压缩发送日志
        /// </summary>
        private void UpdateMarketData()
        {
            Task.Run(() =>
            {
                try
                {
                    //从TT获取的品种
                    //  CfgManager cfgManager = CfgManager.getInstance(null);
                    Dictionary<string, TNewDayTime> zdProducts = new Dictionary<string, TNewDayTime>();
                    //if (!string.IsNullOrEmpty(Configurations.TargetFutures))
                    //{
                    //
                    var futures = string.IsNullOrEmpty(Configurations.TargetFutures) ? null : Configurations.TargetFutures.Split(';');
                    var spreads = string.IsNullOrEmpty(Configurations.TargetSpreads) ? null : Configurations.TargetSpreads.Split(';');
                    var options = string.IsNullOrEmpty(Configurations.TargetOptions) ? null : Configurations.TargetOptions.Split(';');


                    List<(string TTExchange, string TTProduct, SecurityTypeEnum SecurityType)>

                  exchangeProducts = new List<(string TTExchange, string TTProduct, SecurityTypeEnum SecurityType)>();

                    if (futures != null)
                    {
                        futures.ToList().ForEach(p =>
                        {
                            var items = p.Split(',').ToArray();
                            exchangeProducts.Add((items[0], items[1], SecurityTypeEnum.FUT));
                        });
                    }
                    if (spreads != null)
                    {
                        spreads.ToList().ForEach(p =>
                        {
                            var items = p.Split(',').ToArray();
                            exchangeProducts.Add((items[0], items[1], SecurityTypeEnum.MLEG));

                        });
                    }
                    if (options != null)
                    {
                        options.ToList().ForEach(p =>
                        {

                            var items = p.Split(',').ToArray();
                            exchangeProducts.Add((items[0], items[1], SecurityTypeEnum.OPT));

                        });
                    }




                    //   var itemArr = futures.Concat(spreads).Concat(options).ToArray();
                    //for (int i = 0; i < exchangeProducts.Count; i++)
                    exchangeProducts.ForEach(p =>
                    {
                        //if (p.TTProduct == "CC")
                        //{

                        //}
                        var config = $"{p.TTExchange},{p.TTProduct}";
                        //string[] fieldArr = products[i].Split(',');
                        //获取映射的productCode
                        string zdProductCd = null;
                        if (!CodeTransfer_TT.mismatchPrdCd.TryGetValue(config, out zdProductCd))//如果没有找到，TT的产品名称就和公司的一样，直接用。
                        {
                            //zdProductCd = fieldArr[1];
                            zdProductCd = p.TTProduct;
                        }

                        //获取映射的交易所名称
                        string exchangeName;
                        if (!CodeTransfer_TT.prdExchgDict.TryGetValue(zdProductCd, out exchangeName))//如果没有找到，TT的交易所名称就和公司的一样，直接用。
                        {
                            //exchangeName = fieldArr[0];
                            exchangeName = p.TTExchange;
                        }
                        //TT所有的品种对应公司的品种的总数据
                        if (!zdProducts.ContainsKey(zdProductCd))
                        {
                            TNewDayTime oneNewDay = new TNewDayTime();
                            oneNewDay.ExchgName = exchangeName;
                            oneNewDay.ProductCode = zdProductCd;
                            oneNewDay.SecurityType = p.SecurityType;
                            zdProducts.Add(zdProductCd, oneNewDay);
                        }

                    });


                    //}

                    List<TNewDayTime> tNewDayTimeList = DBHandlerFactory.ITNewDayTimeDBHandler.GetTNewDayTimeList();
                    List<TNewDayTime> existList = new List<TNewDayTime>();
                    //var t = tNewDayTimeList.Find(n => n.ProductCode == "BRN");

                    foreach (var product in zdProducts)
                    {
                        //if (product.Value.ProductCode == "OVS2")
                        //{

                        //}
                        var tNewDayTime = tNewDayTimeList.Where(p => p.ProductCode == product.Key).FirstOrDefault();
                        if (tNewDayTime != null)
                        {

                            if (product.Value.ExchgName == tNewDayTime.ExchgName)
                            {
                                tNewDayTime.SecurityType = product.Value.SecurityType;
                                existList.Add(tNewDayTime);
                            }
                            else
                            {
                                TT.Common.NLogUtility.Info($"{product.Value.ExchgName},{product.Key}配置和TNewDayTime配置{tNewDayTime.ExchgName},{tNewDayTime.ProductCode}不同！");
                            }
                        }
                        else
                        {
                            TT.Common.NLogUtility.Info($"{product.Key}品种名称映射没有配置或者和TNewDayTime配置不同！");
                        }
                    }


                    if (existList.Count == 0)
                    {
                        TT.Common.NLogUtility.Info("UpdateMarketData(),existList.Count == 0,没有找到品种！");
                        return;
                    }


                    ////交易所品种的开盘时间
                    //List<(string TTExchange, string TTProduct, TimeSpan MarketTime)> exchangeProductOpeningTime = new List<(string TTExchange, string TTProduct, TimeSpan MarketTime)>();


                    History.Lib.ProfileService.Start(Configurations.FutureConnectStr);
                    //int m = 5;
                    //获取交易时间
                    existList.ForEach(p =>
                    {

                        try
                        {
                            var getOpenTimeProductCode = p.ProductCode;
                            //如果是期权，添加_P或者_C取开盘时间
                            if (p.SecurityType == SecurityTypeEnum.OPT)
                            {
                                //
                                //var ttInfo = Configurations.GetTTProductExchange(p.ProductCode, SecurityTypeEnum.OPT);
                                //var optFutSpot = Configurations.OptFutSpotList.Find(m => m.TTProduct == ttInfo.TTProduct);
                                //getOpenTimeProductCode = optFutSpot.Future;
                                getOpenTimeProductCode = $"{getOpenTimeProductCode}_P";
                            }
                            var marketTime = ProfileService.GetProductOpenTime(getOpenTimeProductCode);
                            TT.Common.NLogUtility.Info($"{getOpenTimeProductCode} - MarketTime:{marketTime}");

                            //获取交易时间并减去偏移量得到更新时间
                            p.MarketTime = marketTime.Add(new TimeSpan(0, p.OffsetMinute, 0));

                            //if (p.ProductCode == "XT")
                            //{

                            //}

                            var tt = Configurations.GetTTProductExchange(p.ProductCode, p.SecurityType);

                            //var tt = Configurations.GetTTProductExchange(p.ProductCode, SecurityTypeEnum.None);
                            exchangeProductOpeningTime.Add((tt.TTExchange, tt.TTProduct, marketTime.Add(new TimeSpan(0, -15, 0))));
                            //p.MarketTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + 2, DateTime.Now.Second + m);
                            //m += 5;
                        }
                        catch (Exception iex)
                        {
                            TT.Common.NLogUtility.Info($"{p.ProductCode}取开盘时间报错，数据库可能没有配置,导致无法订阅行情！");
                            TT.Common.NLogUtility.Error(iex.ToString());
                            TT.Common.NLogUtility.Error(p.ProductCode + " is not found by ProfileService for open time!");
                        }
                    });

                    History.Lib.ProfileService.Stop();

                    EventTriggerV3 eventTrigger = new EventTriggerV3();
                    //获取不重复的交易时间
                    //var timeDistinctList = existList.GroupBy(p => p.MarketTime).Select(p => p.Key).ToList();
                    var timeDistinctList = existList.Select(p => p.MarketTime).Distinct().OrderBy(p => p).ToList();

                    TT.Common.NLogUtility.Info("清结算价商品时间：");
                    //为相同的更新时间的品种设置相同的ID
                    for (int i = 1; i <= timeDistinctList.Count; i++)
                    {
                        //该时间点的所有行情数据
                        var currentTimeData = existList.Where(p => p.MarketTime == timeDistinctList[i - 1]).ToList();
                        if (currentTimeData.Count == 0)
                        {
                            continue;
                        }

                        List<string> productList = new List<string>();
                        //为行情数据设置ID
                        for (int j = 0; j < currentTimeData.Count; j++)
                        {
                            currentTimeData[j].ID = i;
                            productList.Add(currentTimeData[j].ProductCode);
                        }
                        if (!_eventDict.Keys.Contains(i))
                        {
                            //打印时间点的产品
                            //  sw.WriteLine($"time={timeDistinctList[i - 1]},id={i},ProductCodeList={string.Join(",", productList)}");

                            TT.Common.NLogUtility.Info($"time={timeDistinctList[i - 1]},id={i},ProductCodeList={string.Join(",", productList)}");
                            //设置每周重置时间，每周2,3,4,5更新
                            CommonClassLib.EventDetail ed = new CommonClassLib.EventDetail();
                            ed.eventID = i;
                            ed.eventHandler = EventHandler;
                            ed.timeSpan = timeDistinctList[i - 1];
                            //ed.timeSpan = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute+2, DateTime.Now.Second);
                            eventTrigger.registerEvent(ed);
                            _eventDict.Add(i, productList);
                        }
                    }
                    //eventTrigger.ready();
                    TT.Common.NLogUtility.Info("注册清结算价事件完成!");


                    TT.Common.NLogUtility.Info("行情订阅商品时间：");
                    var notSubscribed = CodeTransfer_TT.zd2TTMapping.Values.ToList();

                    var mm = exchangeProductOpeningTime.Where(p => p.TTProduct == "OVS2").FirstOrDefault();
                    //订阅行情数据
                    var exchangeProductOpeningTimeDistinct = exchangeProductOpeningTime.Select(p => p.MarketTime).Distinct().OrderBy(p => p).ToList();

                    TimeSpan timeSpanNow = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    List<SecurityDefinition> subscribedSecurityDefinitions = new List<SecurityDefinition>();
                    //为相同的更新时间的品种设置相同的ID
                    for (int i = 1; i <= exchangeProductOpeningTimeDistinct.Count; i++)
                    {
                        //该时间点的所有行情数据
                        var currentTimeDatas = exchangeProductOpeningTime.Where(p => p.MarketTime == exchangeProductOpeningTimeDistinct[i - 1]).ToList();
                        if (currentTimeDatas.Count == 0)
                        {
                            continue;
                        }


                        List<string> productList = currentTimeDatas.Select(p => p.TTProduct).ToList();

                        //已开盘的合约，立即订阅
                        List<SecurityDefinition> currentTimeSecurityDefinitions = new List<SecurityDefinition>();


                        //为行情数据设置ID
                        for (int j = 0; j < currentTimeDatas.Count; j++)
                        {
                            var securityDefinitions = GetProductSecurityDefinition(currentTimeDatas[j].TTExchange, currentTimeDatas[j].TTProduct);
                            currentTimeSecurityDefinitions.AddRange(securityDefinitions);
                        }

                        notSubscribed = notSubscribed.Except(currentTimeSecurityDefinitions).ToList();
                        //立即订阅
                        if (timeSpanNow > exchangeProductOpeningTimeDistinct[i - 1])
                        {
                            //SubscribeMarketData(currentTimeSecurityDefinitions);
                            subscribedSecurityDefinitions.AddRange(currentTimeSecurityDefinitions);
                        }

                        //else
                        //{
                        TT.Common.NLogUtility.Info($"行情订阅 time={exchangeProductOpeningTimeDistinct[i - 1]},id={i},ProductCodeList={string.Join(",", productList)}");
                        //设置每周重置时间，每周2,3,4,5更新
                        CommonClassLib.EventDetail ed = new CommonClassLib.EventDetail();
                        ed.eventID = i + 3000;
                        ed.eventHandler = (id) =>
                        {
                            TT.Common.NLogUtility.Info($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 订阅事件执行，订阅品种 ProductCodeList={string.Join(",", productList)} SecurityDefinitionCount={currentTimeSecurityDefinitions.Count}");
                            SubscribeMarketData(currentTimeSecurityDefinitions);

                        };

                        //var testTimeSpan = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + i, DateTime.Now.Second);
                        //ed.timeSpan = testTimeSpan;

                        ed.timeSpan = exchangeProductOpeningTimeDistinct[i - 1];
                        eventTrigger.registerEvent(ed);

                        //}

                    }

                    HashSet<string> notSubscribedProducts = new HashSet<string>();
                    if (notSubscribed.Count > 0)
                    {
                        notSubscribed.ForEach(p =>
                        {
                            notSubscribedProducts.Add(p.Symbol.getValue());
                        });

                    }
                    if (notSubscribedProducts.Count > 0)
                    {
                        var str = string.Join(",", notSubscribedProducts.ToArray());
                        TT.Common.NLogUtility.Info($"未订阅的品种:{str}。");
                    }

                    notSubscribed?.ForEach(p =>
                    {
                        TT.Common.NLogUtility.Info($"未订阅：{p.ToString()}");
                    });
                    TT.Common.NLogUtility.Info("注册订阅事件完成!");
                    SubscribeMarketData(subscribedSecurityDefinitions);





                    //添加每天三点自动保存结算价
                    TimeSpan timeSpan;
                    if (TimeSpan.TryParse(Configurations.SaveSettlementPriceTime, out timeSpan))
                    {
                        CommonClassLib.EventDetail ed = new CommonClassLib.EventDetail();
                        ed.eventID = 999997;
                        ed.eventHandler = (id) =>
                        {
                            TT.Common.NLogUtility.Info($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}开始保存结算价到本地!");
                            SavePreveTodaySett();
                            TT.Common.NLogUtility.Info($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}保存结算价完成!");
                        };
                        ed.timeSpan = timeSpan;
                        eventTrigger.registerEvent(ed);
                        TT.Common.NLogUtility.Info("注册保存结算价事件完成!");
                    }
                    else
                    {
                        TT.Common.NLogUtility.Info("ApplicationExitTime 时间配置格式不正确!注册保存结算价事件失败!");
                    }

                    //每天晚上12点打包日志，并删除日志文件只保留打包的文件
                    if (!string.IsNullOrEmpty(Configurations.LogSendMsg) && !string.IsNullOrEmpty(Configurations.ZipLogTime))
                    {
                        TimeSpan zipLogTime;
                        if (TimeSpan.TryParse(Configurations.ZipLogTime, out zipLogTime))
                        {
                            CommonClassLib.EventDetail zipEventDetail = new CommonClassLib.EventDetail();
                            zipEventDetail.eventID = 999998;
                            zipEventDetail.eventHandler = (id) =>
                            {
                                Zip.ZipLog();
                                Zip.KeepingThirtyDaysLogs();
                            };
                            zipEventDetail.timeSpan = zipLogTime;
                            eventTrigger.registerEvent(zipEventDetail);
                            TT.Common.NLogUtility.Info("注册压缩发送日志任务完成!");

                        }
                        else
                        {
                            TT.Common.NLogUtility.Info("ZipLogTime 时间配置格式不正确!注册日志打包事件失败!");
                        }
                    }


                    ////添加每天上午杀进程保存SaveOrderBookALL时间
                    //TimeSpan timeSpanMorning;
                    //if (TimeSpan.TryParse(Configurations.ApplicationExitTimeMorning, out timeSpanMorning))
                    //{
                    //    CommonClassLib.EventDetail ed = new CommonClassLib.EventDetail();
                    //    ed.eventID = 999999;
                    //    ed.eventHandler = (id) =>
                    //    {
                    //        errorLogger.log(ZDLogger.LVL_DEBUG, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}开始SaveOrderBookALL!");
                    //        SaveOrderBookALL();
                    //        errorLogger.log(ZDLogger.LVL_DEBUG, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}保存完成SaveOrderBookALL!");

                    //    };
                    //    ed.timeSpan = timeSpanMorning;
                    //    eventTrigger.registerEvent(ed);
                    //    errorLogger.log(ZDLogger.LVL_DEBUG, "注册ApplicationExitTimeMorning保存SaveOrderBookALL事件完成!");
                    //}
                    //else
                    //{
                    //    errorLogger.log(ZDLogger.LVL_DEBUG, "ApplicationExitTimeMorning 时间配置格式不正确!注册保存结算价事件失败!");
                    //}


                    //重启系统
                    //应用程序自动退出
                    if (!string.IsNullOrEmpty(Configurations.RestartTime))
                    {
                        //TimeSpan restartTime;
                        if (TimeSpan.TryParse(Configurations.RestartTime, out TimeSpan restartTime))
                        {
                            CommonClassLib.EventDetail zipEventDetail = new CommonClassLib.EventDetail();
                            zipEventDetail.eventID = 999999;
                            zipEventDetail.eventHandler = (id) =>
                            {
                                if (this.btnStop.Enabled)
                                {
                                    if (this.InvokeRequired)
                                    {
                                        this.Invoke((MethodInvoker)(() =>
                                        {
                                            btnStop_Click(null, null);
                                        }));
                                    }
                                    else
                                    {
                                        btnStop_Click(null, null);
                                    }
                                    Thread.Sleep(1 * 1000);
                                }
                                AppReloader();
                                TT.Common.NLogUtility.Info("程序已重启!");
                            };

                            zipEventDetail.timeSpan = restartTime;
                            eventTrigger.registerEvent(zipEventDetail);
                            TT.Common.NLogUtility.Info("注册重启任务完成!");

                        }
                        else
                        {
                            TT.Common.NLogUtility.Info("RestartTime 时间配置格式不正确!注册重启事件失败!");
                        }
                    }

                    // T + 1开盘时间前清理内存的成交额数据
                    var supportedTradeVolumeProductsTimes = Configurations.SupportedTradeVolumeProductsList.Select(p => p.OpeningTime).Distinct().ToList();

                    int eventID = 1000;
                    supportedTradeVolumeProductsTimes.ForEach(p =>
                    {
                        if (TimeSpan.TryParse(p, out TimeSpan openingTime))
                        {
                            CommonClassLib.EventDetail zipEventDetail = new CommonClassLib.EventDetail();
                            zipEventDetail.eventID = eventID;
                            zipEventDetail.eventHandler = (id) =>
                            {
                                var currentList = Configurations.SupportedTradeVolumeProductsList.Where(i => i.OpeningTime == p).ToList();
                                currentList.ForEach(c =>
                                {
                                    for (int i = 0; i < OrderBookMgr.Instance.TradingVolumeDic.Keys.Count; i++)
                                    {
                                        string key = OrderBookMgr.Instance.TradingVolumeDic.Keys.ElementAt(i);
                                        if (TTMarketAdapterCommon.GetZDProduct(key, SecurityTypeEnum.FUT) == c.ZDProduct)
                                        {
                                            OrderBookMgr.Instance.TradingVolumeDic[key] = 0;
                                            TT.Common.NLogUtility.Info($"清理合约{key}的交易额任务完成!");
                                        }
                                    }
                                });

                            };

                            zipEventDetail.timeSpan = openingTime;
                            eventTrigger.registerEvent(zipEventDetail);

                            ++eventID;
                        }
                        else
                        {
                            TT.Common.NLogUtility.Info($"交易额任务时间{p}配置格式错误!");
                        }

                    });
                    TT.Common.NLogUtility.Info("注册清理交易额任务完成!");

                    eventTrigger.ready();
                    TT.Common.NLogUtility.Info("UpdateMarketData() Completed");
                }
                catch (Exception ex)
                {
                    TT.Common.NLogUtility.Error(ex.ToString());
                }

            });
        }

        private void EventHandler(int id)
        {
            //AuthCommon.SynWriteLogger eventHandlerProStart = new AuthCommon.SynWriteLogger("eventHandlerProStart.log");
            //eventHandlerProStart.setLogLevel(ZDLogger.LVL_DEBUG);
            try
            {
                DateTime time = DateTime.Now;
                DayOfWeek dw = DateTime.Now.DayOfWeek;
                //if ((dw == DayOfWeek.Monday && DateTime.Now.Hour < 12) || dw == DayOfWeek.Saturday || dw == DayOfWeek.Sunday)
                //{
                //    return;
                //}
                if (dw == DayOfWeek.Saturday || dw == DayOfWeek.Sunday)
                {
                    return;
                }
                TT.Common.NLogUtility.Info($"EventHandler started time={ time.ToString("yyyy-MM-dd HH:mm:ss")},id={id}");
                List<string> productCodeList;
                if (_eventDict.TryGetValue(id, out productCodeList))
                {
                    foreach (string productCd in productCodeList)
                    {
                        List<string> securityIdList;
                        if (CodeTransfer_TT.productToContractsDict.TryGetValue(productCd, out securityIdList))
                        {
                            foreach (string securityId in securityIdList)
                            {
                                //OrderBookALL oba;
                                if (OrderBookMgr.Instance.orderBookDict.TryGetValue(securityId, out OrderBookALL oba))
                                {
                                    TT.Common.NLogUtility.Info($"before eventhandler securityId={securityId},  zdcode={oba.codeBean?.zdCode},new_preveSett={oba.new_preveSett},new_TodaySett={oba.new_TodaySett}");
                                    if (oba.new_TodaySett == 0)//今结算为零，不清
                                    {
                                        TT.Common.NLogUtility.Info($"今结算为零，不清！");
                                        continue;
                                    }
                                    //oba.lastTrade.Price = 0;
                                    //oba.lastTrade.Quantity = 0;

                                    //oba.openingPrice = 0;
                                    //oba.new_preveSett = oba.new_TodaySett;
                                    //oba.new_TodaySett = 0;
                                    //oba.tradeVolume = 0;
                                    //oba.tsHighPrice = 0;
                                    //oba.tsLowPrice = 0;

                                    //由于周一上午BRN早开盘2h，不清高低开价格。
                                    if (!((dw == DayOfWeek.Monday && DateTime.Now.Hour < 12) && productCd == "BRN"))
                                    {
                                        oba.tsHighPrice = 0;
                                        oba.tsLowPrice = 0;
                                        oba.openingPrice = 0;
                                    }
                                    oba.lastTrade.Price = 0;
                                    oba.lastTrade.Quantity = 0;


                                    oba.new_preveSett = oba.new_TodaySett;
                                    oba.new_TodaySett = 0;
                                    oba.tradeVolume = 0;

                                    TT.Common.NLogUtility.Info($"After eventhandler securityId={securityId}, zdcode={oba.codeBean.zdCode},new_preveSett={oba.new_preveSett},new_TodaySett={oba.new_TodaySett}");
                                    Sub2Pumper.pumpSnapshotQuickFast(oba, $"{LogMessageType.EventHandler} MsgSeqNum:0");//清结算价操作


                                    //发送F数据
                                    var strMarket = $"{oba.codeBean.zdExchg}@{oba.codeBean.zdProduct}@3";
                                    ZDMDMsgProtocol zDMDMsgProtocol = ZDMDMsgProtocol.GetInstance(); ;

                                    zDMDMsgProtocol.MsgType = ZDMDMsgType.ClearData;
                                    zDMDMsgProtocol.FiveLevelLength = 0;

                                    zDMDMsgProtocol.OneLevelLength = 0;
                                    zDMDMsgProtocol.TimeStamp = DateTime.UtcNow.GetTimeStamp();
                                    zDMDMsgProtocol.MsgBody = strMarket;

                                    var length = 12 + strMarket.Length;
                                    TCPData tCPData = TCPData.GetInstance();

                                    tCPData.MsgLength = (ushort)length;
                                    tCPData.ZDMDMsgProtocol = zDMDMsgProtocol;

                                    var bytes = tCPData.GetBytes();
                                    MDSocket.GetInstance().Send(bytes, length + 2);
                                    TT.Common.NLogUtility.Info(strMarket);


                                }
                                else
                                {
                                    TT.Common.NLogUtility.Info($"{securityId}:没有找到对应的OrderBookALL！");
                                }
                            }

                        }
                        else
                        {
                            TT.Common.NLogUtility.Info($"{productCd}:没有找到securityIdList！");
                        }
                    }
                    if (productCodeList.Count != 0)
                    {
                        TT.Common.NLogUtility.Info($"Eventhandler completed time={ time.ToString("yyyy-MM-dd HH:mm:ss.fff")},id={id},ProductCodeList={string.Join(",", productCodeList)}");
                    }
                }
                else
                {
                    TT.Common.NLogUtility.Info($"_eventDict:没有找到数据！_eventDict.Count={_eventDict.Count}");
                }

            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }

        }


        private List<SecurityDefinition> GetProductSecurityDefinition(string ttExhcange, string ttProduct)
        {
            List<SecurityDefinition> list = new List<SecurityDefinition>();

            list = CodeTransfer_TT.zd2TTMapping.Values.Where(p => p.Symbol.getValue() == ttProduct && p.SecurityExchange.getValue() == ttExhcange).ToList();
            return list;
        }


        private void SubscribeMarketData(List<SecurityDefinition> securityDefinitions)
        {
            securityDefinitions?.ForEach(p =>
            {
                _tTPriceCommunication.SubscribeMarketData(p);
            });
        }

        /// <summary>
        /// 重新登录TT，订阅那些已经开盘的合约。
        /// </summary>
        private void ReloginSubscribeMarketData()
        {
            //订阅行情数据
            var exchangeProductOpeningTimeDistinct = exchangeProductOpeningTime.Select(p => p.MarketTime).Distinct().OrderBy(p => p).ToList();

            TimeSpan timeSpanNow = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //为相同的更新时间的品种设置相同的ID
            for (int i = 1; i <= exchangeProductOpeningTimeDistinct.Count; i++)
            {
                //该时间点的所有行情数据
                var currentTimeDatas = exchangeProductOpeningTime.Where(p => p.MarketTime == exchangeProductOpeningTimeDistinct[i - 1]).ToList();
                if (currentTimeDatas.Count == 0)
                {
                    continue;
                }


                List<string> productList = currentTimeDatas.Select(p => p.TTProduct).ToList();

                List<SecurityDefinition> currentTimeSecurityDefinitions = new List<SecurityDefinition>();


                //为行情数据设置ID
                for (int j = 0; j < currentTimeDatas.Count; j++)
                {
                    var securityDefinitions = GetProductSecurityDefinition(currentTimeDatas[j].TTExchange, currentTimeDatas[j].TTProduct);
                    currentTimeSecurityDefinitions.AddRange(securityDefinitions);
                }
                //  notSubscribed = notSubscribed.Except(currentTimeSecurityDefinitions).ToList();
                //立即订阅
                if (timeSpanNow > exchangeProductOpeningTimeDistinct[i - 1])
                {
                    SubscribeMarketData(currentTimeSecurityDefinitions);

                }
            }
        }



        #endregion

        #region   停止TT服务
        /// <summary>
        /// 停止TT服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            _tTPriceCommunication?.disconnectFixAdapter();
            TT.Common.NLogUtility.Info("停止服务！");
            try
            {
                try
                {
                    SaveOrderBookALL();
                }
                catch (Exception ex)
                {
                    TT.Common.NLogUtility.Error(ex.ToString());
                }
                try
                {
                    SavePreveTodaySett();
                }
                catch (Exception ex)
                {
                    TT.Common.NLogUtility.Error(ex.ToString());
                }

                SaveTradeVolumeData();
                //NLogUtility.Shutdown();
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
            finally
            {
                _eventDict.Clear();
                this.btnStart.Enabled = true;
                this.btnStop.Enabled = false;
            }
        }

        private void SavePreveTodaySett()
        {
            //fancky  add  2017-12-12  写入昨今结算价
            if (OrderBookMgr.Instance?.orderBookDict?.Keys.Count > 0)
            {
                string path = Path.Combine(Application.StartupPath, _preveTodaySett);
                using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
                {
                    foreach (string key in OrderBookMgr.Instance.orderBookDict.Keys)
                    {
                        var item = OrderBookMgr.Instance.orderBookDict[key];
                        sw.WriteLine($"{key},{item.codeBean.zdCode},{item.new_preveSett},{item.new_TodaySett}");
                    }
                }
            }

        }

        private void SaveTradeVolumeData()
        {
            if (OrderBookMgr.Instance?.TradingVolumeDic?.Keys.Count > 0)
            {
                string path = Path.Combine(Application.StartupPath, _tradeVolumeData);
                using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
                {
                    foreach (string key in OrderBookMgr.Instance.TradingVolumeDic.Keys)
                    {
                        sw.WriteLine($"{key},{OrderBookMgr.Instance.TradingVolumeDic[key]}");
                    }
                }
            }
        }

        private void SaveOrderBookALL()
        {
            using (StreamWriter sWriter = new StreamWriter(File.Open(OrderBookMgr.OrderBookAllData, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
            {
                //foreach (string securityId in GlobalData.specialSecurityIDSet)
                foreach (string securityId in OrderBookMgr.Instance.orderBookDict.Keys)
                {
                    OrderBookALL oba;
                    //errorLogger.log(ZDLogger.LVL_DEBUG, $" OrderBookMgr.Instance.orderBookDict.Keys.Count{OrderBookMgr.Instance.orderBookDict.Keys.Count}");
                    if (OrderBookMgr.Instance.orderBookDict.TryGetValue(securityId, out oba))
                    {
                        //添加保存开、高、低价
                        if (oba.holdSettPrice == 0)
                            sWriter.WriteLine(oba.codeBean.zdCode + ":" + securityId + ":" + oba.settPrice + ":" + oba.holdTradeVolume + ":" + oba.openingPrice + ":" + oba.tsHighPrice + ":" + oba.tsLowPrice);
                        else
                            sWriter.WriteLine(oba.codeBean.zdCode + ":" + securityId + ":" + oba.holdSettPrice + ":" + oba.holdTradeVolume + ":" + oba.openingPrice + ":" + oba.tsHighPrice + ":" + oba.tsLowPrice);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdateMarketData();
            return;
            //DateTime contractDate = DateTime.ParseExact(str, "yyyyMM", CultureInfo.InvariantCulture);
            //var strrrr1 = string.Format("{0:00}", "7");
            //var strrrr = string.Format("{0:D2}", "7");
            //var ddddd = Configurations.OptionFutureMonthDic["DD"];
            int sdm = 0;
            //FractionalPrxBean fpb = new FractionalPrxBean();

            //fpb.denominator = 100;
            //fpb.factor = 8;
            //var inputPrx = 9722;
            //// To client
            ////9722 -> 9.7225
            //string strPrx = inputPrx.ToString();
            //int dotIdx = strPrx.IndexOf('.');
            //if (dotIdx > -1)
            //{
            //    strPrx = strPrx.Substring(0, dotIdx);
            //}


            //char fractionPrx = strPrx[strPrx.Length - 1];

            //decimal dDecimal = decimal.Parse(fractionPrx.ToString());
            //decimal tail = dDecimal / fpb.denominator;
            //decimal data = decimal.Parse(strPrx.Substring(0, strPrx.Length - 1)) + tail;

            //decimal result = data / fpb.factor;

            //return;
            MDSocket mDSocket = MDSocket.GetInstance();
            while (true)
            {
                if (mDSocket.IsConnected)
                {
                    var remoteEndPoint = mDSocket.receiveSocket.RemoteEndPoint;
                    var localEndPoint = mDSocket.receiveSocket.LocalEndPoint;
                    string localEndPointStr = localEndPoint.ToString();
                    //ICE@BRN2008@54.67@54.14@54.24@@58.24@57.51@@@@
                }
            }

            return;
            //   BitConverter.GetBytes()
            // BitConverter.GetBytes()
            var timeStamp = DateTime.UtcNow.GetTimeStamp();
            var len = timeStamp.ToString().Length;
            //1583253877413
            string mdStr = $"1D00[UNIXEP]CME@CL1907@52.96@27@@";
            var by = Encoding.ASCII.GetBytes(mdStr);
            ZDMDMsgProtocol zDMDMsgProtocol = ZDMDMsgProtocol.GetInstance();
            zDMDMsgProtocol.MsgType = ZDMDMsgType.TradeData;
            zDMDMsgProtocol.FiveLevelLength = 0;

            zDMDMsgProtocol.OneLevelLength = 0;
            zDMDMsgProtocol.TimeStamp = DateTime.UtcNow.GetTimeStamp();
            zDMDMsgProtocol.MsgBody = "CME@CL1907@52.96@27@@";

            TCPData tCPData = TCPData.GetInstance();

            tCPData.MsgLength = 33;
            tCPData.ZDMDMsgProtocol = zDMDMsgProtocol;


            var b1 = tCPData.GetBytes();
            string str = tCPData.BytesToHexString(b1);
            return;
            NLogUtility.Debug("Debug1");
            NLogUtility.Info("NLogDemo info ");
            NLogUtility.Info("info2");
            NLogUtility.Warn("Warn3");
            try
            {
                int m = int.Parse("m");
            }
            catch (Exception ex)
            {
                NLogUtility.Error(ex.ToString());
            }
            TT.Common.NLogUtility.Info("UpdateMarketData(),existList.Count == 0,没有找到品种！");

            return;
            //FileUsing.FileIsUsing("");
            /*
             {"Price":0.0,"OrderCount":0,"Quantity":0,"ttFlag":"0"},
            {"Price":16100.0,"OrderCount":0,"Quantity":6,"ttFlag":"0"},
            {"Price":16105.0,"OrderCount":0,"Quantity":4,"ttFlag":"0"},
            {"Price":16110.0,"OrderCount":0,"Quantity":2,"ttFlag":"0"},
            {"Price":16115.0,"OrderCount":0,"Quantity":3,"ttFlag":"0"},
            {"Price":16120.0,"OrderCount":0,"Quantity":2,"ttFlag":"1"}]
             */
            //PriceDetail pd = new PriceDetail()
            //{
            //    Price = 0.0,
            //    OrderCount = 0,
            //    Quantity = 0,
            //    ttFlag = '0'
            //};
            //PriceDetail pd1 = new PriceDetail()
            //{
            //    Price = 16100,
            //    OrderCount = 0,
            //    Quantity = 6,
            //    ttFlag = '0'
            //};
            //PriceDetail pd2 = new PriceDetail()
            //{
            //    Price = 16105,
            //    OrderCount = 0,
            //    Quantity = 4,
            //    ttFlag = '0'
            //};
            //PriceDetail pd3 = new PriceDetail()
            //{
            //    Price = 16110,
            //    OrderCount = 0,
            //    Quantity = 2,
            //    ttFlag = '0'
            //};
            //PriceDetail pd4 = new PriceDetail()
            //{
            //    Price = 16115,
            //    OrderCount = 0,
            //    Quantity = 3,
            //    ttFlag = '0'
            //};
            //PriceDetail pd5 = new PriceDetail()
            //{
            //    Price = 16120,
            //    OrderCount = 0,
            //    Quantity = 2,
            //    ttFlag = '1'
            //};
            //PriceDetail[] pdArray = new PriceDetail[] { pd, pd1, pd2, pd3, pd4, pd5 };
            //var list = pdArray.ToList();
            //var merge = list.GroupBy(p => p.Price).Select(group => new PriceDetail { Price = group.Key, Quantity = group.Sum(p => p.Quantity) }).ToList();
            //merge.ForEach(p =>
            //{
            //    p.ttFlag = list.First(m => m.Price == p.Price).ttFlag;
            //});

            //int x = 0;
            //int y = 1;
            //x |= y;
            //Console.WriteLine(x);

            #region old
            //  541 = 20190528
            //  DateTime dt = DateTime.ParseExact("20190528", "yyyyMMdd", CultureInfo.InvariantCulture);
            //GlobalData.orderBookMgr.changeMask |= GlobalData.orderBookMgr.xBidAfterChange;

            /*
            errorLogger = new SynWriteLogger("Error.log");

            ttPrxCommu = new TTPriceCommunication();
            ttPrxCommu.ConfigFile = @"config\quickfix_market.cfg";
            ttPrxCommu.init(errorLogger, false);
            */

            //*


            //string msgStr = "8=FIX.4.2|9=00535|35=W|49=TT_PRICE|56=GHFDXESORCL|34=3|52=20150226-03:08:43.896|55=CN|48=CNH15|10455=CNH15|167=FUT|207=SGX|15=USD|262=2|200=201503|18210=1|387=68866|268=14|269=0|290=1|270=10647.5|271=62|269=0|290=2|270=10645|271=125|269=0|290=3|270=10642.5|271=192|269=0|290=4|270=10640|271=245|269=0|290=5|270=10637.5|271=125|269=1|290=1|270=10650|271=5|269=1|290=2|270=10652.5|271=105|269=1|290=3|270=10655|271=174|269=1|290=4|270=10657.5|271=120|269=1|290=5|270=10660|271=99|269=2|270=10650|271=22|269=4|270=10667.5|269=7|270=10675|269=8|270=10492.5|10=112|";
            //msgStr = msgStr.Replace('|', (char)1);
            //QuickFix.FIX42.MarketDataSnapshotFullRefresh msg = new QuickFix.FIX42.MarketDataSnapshotFullRefresh();
            //msg.FromString(msgStr, false, null, dd);

            //GlobalData.orderBookMgr.processFullRefresh(msg);


            //string msgStr2 = "8=FIX.4.2|9=544|35=X|34=135|49=TT_PRICE|52=20150226-03:08:56.013|56=GHFDXESORCL|262=2|268=11|279=2|15=USD|48=CNH15|55=CN|167=FUT|200=201503|207=SGX|269=0|270=10652.5|271=1|290=1|387=69007|10455=CNH15|18210=1|279=1|269=0|270=10650|271=157|290=2|279=1|269=0|270=10645|271=185|290=4|279=1|269=0|270=10642.5|271=93|290=5|279=0|269=0|270=10640|271=241|279=0|269=1|270=10652.5|271=7|279=1|269=1|270=10655|271=265|290=1|279=1|269=1|270=10657.5|271=125|290=2|279=1|269=1|270=10660|271=108|290=3|279=1|269=1|270=10662.5|271=153|290=4|279=2|269=1|270=10665|271=57|290=5|10=079|";
            //msgStr2 = msgStr2.Replace('|', (char)1);
            //QuickFix.FIX42.MarketDataIncrementalRefresh msg2 = new QuickFix.FIX42.MarketDataIncrementalRefresh();
            //msg2.FromString(msgStr2, false, null, dd);
            //GlobalData.orderBookMgr.processIncrementalRefresh(msg2);

            /*
            QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            dd.Load(@"config\FIX42.xml");
            QuickFix.FIX42.MarketDataSnapshotFullRefresh wMsg = new QuickFix.FIX42.MarketDataSnapshotFullRefresh();
            QuickFix.FIX42.MarketDataIncrementalRefresh xMsg = new QuickFix.FIX42.MarketDataIncrementalRefresh();

            bool startFlag = false;
            string fileName = @"data\FIX.4.2-GHFDXESORCL-TT_PRICE.messages.current.log";
            int stepIntoSeq = 151;
            using (System.IO.StreamReader sReader = new System.IO.StreamReader(System.IO.File.Open(fileName, System.IO.FileMode.Open), System.Text.Encoding.ASCII))
            {
                while (!sReader.EndOfStream)
                {
                    string oneLine = sReader.ReadLine().Trim();
                    if (string.IsNullOrEmpty(oneLine))
                        continue;

                    if (startFlag)
                    {
                        string msgStr = oneLine.Substring(24);
                        xMsg.FromString(msgStr, false, null, dd);

                        if (xMsg.Header.GetChar(QuickFix.Fields.Tags.MsgType) == 'X')
                        {

                            if (xMsg.Header.GetInt(QuickFix.Fields.Tags.MsgSeqNum) == stepIntoSeq)
                            {
                                int a = 0;
                            }

                            GlobalData.orderBookMgr.processIncrementalRefresh(xMsg);
                        }

                        
                    }
                    else
                    if (oneLine.IndexOf("56=GHFDXESORCL" + (char)1 + "34=3") > 0)
                    {
                        string msgStr = oneLine.Substring(24);
                        wMsg.FromString(msgStr, false, null, dd);
                        startFlag = true;
                        GlobalData.orderBookMgr.processFullRefresh(wMsg);
                    }

                    

                }
            }
            */

            /*
            //GlobalData.orderBookMgr = new OrderBookMgr();


            QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            dd.Load(@"config\FIX42.xml");
            string msgStr3 = "8=FIX.4.2|9=192|35=X|34=170371|49=TT_PRICE|52=20150415-06:00:00.614|56=GHFDXESORCL|262=59|268=3|279=2|15=USD|48=TWM15|55=TW|167=FUT|200=201506|207=SGX|269=4|387=12|10455=TWM15|18210=1|279=2|269=7|279=2|269=8|10=105|";
            msgStr3 = msgStr3.Replace('|', (char)1);
            QuickFix.FIX42.MarketDataIncrementalRefresh msg3 = new QuickFix.FIX42.MarketDataIncrementalRefresh();
            msg3.FromString(msgStr3, false, null, dd);
            GlobalData.orderBookMgr.processIncrementalRefresh(msg3);

            // last repeat group has no tag290
            //8=FIX.4.2|9=00295|35=X|49=TT_PRICE|56=GHFDXESORCL|34=46|52=20150206-03:01:06.387|262=1|268=4|279=1|55=CN|48=CNG15|10455=CNG15|167=FUT|207=SGX|15=USD|200=201502|290=1|18210=1|269=0|270=10355|271=144|387=69254|279=1|290=4|269=0|270=10347.5|271=112|279=2|290=1|269=1|270=10357.5|271=12|279=0|269=1|270=10370|271=109|10=235|

            //// first repeat group has no tag290
            //string msgStr4 = "8=FIX.4.2|9=00258|35=X|49=TT_PRICE|56=GHFDXESORCL|34=30|52=20150206-03:01:04.939|262=1|268=3|279=0|55=CN|48=CNG15|10455=CNG15|167=FUT|207=SGX|15=USD|200=201502|18210=1|269=0|270=10355|271=4|387=69239|279=1|290=1|269=0|270=10352.5|271=165|279=2|290=5|269=0|270=10342.5|271=106|10=068|";
            //msgStr4 = msgStr4.Replace('|', (char)1);
            //QuickFix.FIX42.MarketDataIncrementalRefresh msg4 = new QuickFix.FIX42.MarketDataIncrementalRefresh();
            //msg4.FromString(msgStr4, false, null, dd);
            //GlobalData.orderBookMgr.processIncrementalRefresh(msg4);
            int a = 0;
            */


            /*
            //ttPrxCommu.reqMarket();
            for (int i = 0; i < GlobalData.allSecuriryList.Count; i++)
            {
                QuickFix.FIX42.MarketDataRequest.NoRelatedSymGroup symGrp = new QuickFix.FIX42.MarketDataRequest.NoRelatedSymGroup();
                QuickFix.FIX42.SecurityDefinition sd = GlobalData.allSecuriryList[i];
                ttPrxCommu.reqMarketIncremental(sd);
            }
            */

            /*
            PriceDetail[] beforePriceDetail = new PriceDetail[4];

            PriceDetail pd1 = new PriceDetail();
            pd1.Price = 96;
            pd1.ttFlag = QuickFix.Fields.MDUpdateAction.DELETE;

            PriceDetail pd2 = new PriceDetail();
            pd2.Price = 94;

            PriceDetail pd3 = new PriceDetail();
            pd3.Price = 95;

            PriceDetail pd4 = new PriceDetail();
            pd4.Price = 88;

            beforePriceDetail[0] = pd1;
            beforePriceDetail[1] = pd2;
            beforePriceDetail[2] = pd3;
            beforePriceDetail[3] = pd4;
            Array.Sort<PriceDetail>(beforePriceDetail);
            */
            /*
            PriceDetail[] afterPriceDetail = new PriceDetail[4];
            PriceDetail pd30 = new PriceDetail();
            pd30.Price = 94;
            pd30.ttFlag = QuickFix.Fields.MDUpdateAction.NEW;
            afterPriceDetail[0] = pd30;
            afterPriceDetail[1] = new PriceDetail();
            afterPriceDetail[2] = new PriceDetail();
            afterPriceDetail[3] = new PriceDetail();

            OrderBookMgr mgr = new OrderBookMgr();
            mgr.doInsertBid(beforePriceDetail, afterPriceDetail);
            */



            //CodeTransfer_TT.initPrxFactor(true);

            //decimal data = 9722;

            //string r1 = CodeTransfer_TT.toClientPrx(data, "ZS");
            //decimal r2 = CodeTransfer_TT.toGlobexPrx("9.7225", "ZS");


            //data = 9720;
            //string r3 = CodeTransfer_TT.toClientPrx(data, "ZS");
            //decimal r4 = CodeTransfer_TT.toGlobexPrx("9.72", "ZS");

            //decimal r5 = CodeTransfer_TT.toGlobexPrx("3.88375", "XC");

            //decimal r6 = CodeTransfer_TT.toGlobexPrx("4.0", "ZW");
            //Console.WriteLine("ZW:4.0  ->" + r6.ToString());


            //if (m == 1)
            //{
            //    this.btnTest.Text = m.ToString();
            //    m++;
            //    return;
            //}
            //if (this.btnStop.Enabled)
            //{
            //    if (this.InvokeRequired)
            //    {
            //        this.Invoke((MethodInvoker)(() =>
            //        {
            //            btnStop_Click(null, null);
            //        }));
            //    }
            //    else
            //    {
            //        btnStop_Click(null, null);
            //    }
            //    Thread.Sleep(1 * 1000);
            //}

            //string incrementMessage = @"8=FIX.4.2|9=00317|35=X|49=TT_PRICE|56=daintmd|34=4184|52=20190517-01:44:09.676|262=2|268=4|279=1|269=0|55=NI|167=FUT|200=201908|541=20190816|205=16|18211=D|48=11988531206950636299|207=LME|100=XLME|461=F|15=USD|270=12110|271=5|290=1|279=1|269=0|270=12105|271=16|290=2|279=1|269=1|270=12120|271=2|290=2|279=1|269=1|270=12125|271=7|290=3|10=108|";

            //string incrementMessage = @"8=FIX.4.2|9=281|35=X|34=80|49=TT_PRICE|52=20190520-03:16:43.203|56=daintmd|262=1|268=3|279=0|269=1|55=NI|167=FUT|200=201908|541=20190820|205=20|18211=D|48=11988531206950636299|207=LME|100=XLME|15=USD|270=12010|271=3|290=1|461=F|279=1|269=1|270=12015|271=9|290=1|279=2|269=1|270=12035|271=10|290=5|10=123|";

            //string incrementMessage = @"8=FIX.4.2|9=247|35=X|34=81|49=TT_PRICE|52=20190520-03:16:43.933|56=daintmd|262=1|268=2|279=1|269=1|55=NI|167=FUT|200=201908|541=20190820|205=20|18211=D|48=11988531206950636299|207=LME|100=XLME|15=USD|270=12015|271=11|290=2|461=F|279=1|269=1|270=12030|271=4|290=5|10=126|";
            #endregion

            #region  快照、增量数据调试（勾选调试模式：初始化内存数据）
            QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            dd.Load(Configurations.FIX42);

            ////string snapshotStr = @"8=FIX.4.2|9=00539|35=W|49=TT_PRICE|56=daintmd|34=2|52=20190812-02:09:01.078|262=1|55=NI|107=Nickel|460=12|167=FUT|200=201911|541=20191112|205=12|18211=D|48=11988531206950636299|207=LME|100=XLME|461=F|15=USD|268=14|269=0|270=15570|271=2|290=1|269=0|270=15565|271=2|290=2|269=0|270=15560|271=1|290=3|269=0|270=15555|271=1|290=4|269=0|270=15550|271=2|290=5|269=1|270=15590|271=6|290=1|269=1|270=15605|271=3|290=2|269=1|270=15610|271=1|290=3|269=1|270=15615|271=2|290=4|269=1|270=15630|271=6|290=5|269=B|271=4540|269=6|270=15550|269=7|270=16145|269=8|270=15500|10=120|";
            //var snapshotStr = @"8=FIX.4.2|9=00563|35=W|49=TT_PRICE|56=daintmd|34=10|52=20191202-02:13:12.021|262=1|55=CA|107=Copper-Grade A|460=12|167=FUT|200=202003|541=20200302|205=2|18211=D|48=12486318480675622682|207=LME|100=XLME|461=F|15=USD|268=15|269=0|270=5901.5|271=3|290=1|269=0|270=5901|271=8|290=2|269=0|270=5900.5|271=5|290=3|269=0|270=5900|271=4|290=4|269=0|270=5899.5|271=4|290=5|269=1|270=5902.5|271=2|290=1|269=1|270=5903|271=3|290=2|269=1|270=5903.5|271=3|290=3|269=1|270=5904|271=15|290=4|269=1|270=5904.5|271=3|290=5|269=B|271=879|269=6|270=5864|269=4|270=5887.5|269=7|270=5903|269=8|270=5886|10=247|";
            //snapshotStr = snapshotStr.Replace('|', (char)1);
            //MarketDataSnapshot marketDataSnapshot = new MarketDataSnapshot();
            //marketDataSnapshot.FromString(snapshotStr, false, null, dd);
            //OrderBookMgr.Instance.processFullRefresh(marketDataSnapshot);


            //string incrementMessage = @"8=8=FIX.4.2|9=00247|35=X|49=TT_PRICE|56=daintmd|34=13159|52=20191230-09:07:19.355|262=32|268=1|279=0|269=1|55=BRN|107=Brent Crude Futures|460=12|167=FUT|200=202005|541=20200529|205=29|18211=M|48=5125965359451543039|207=ICE|100=IFEU|461=F|15=USD|270=64.32|271=1|290=1|10=164|";
            //string msgStr3 = "8 =FIX.4.2|9=192|35=X|34=170371|49=TT_PRICE|52=20150415-06:00:00.614|56=GHFDXESORCL|262=59|268=3|279=2|15=USD|48=TWM15|55=TW|167=FUT|200=201506|207=SGX|269=4|387=12|10455=TWM15|18210=1|279=2|269=7|279=2|269=8|10=105|";

            //var incrementMessage = @"8=FIX.4.2|9=285|35=X|34=849845|49=TT_PRICE|52=20200102-16:00:19.938|56=daintmd|262=37|268=2|279=1|269=0|55=BRN|167=FUT|200=202101|541=20210129|205=29|18211=M|48=13992439098726819734|207=ICE|100=IFEU|15=USD|270=59.97|271=21|290=1|107=Brent Crude Futures|460=12|461=F|279=0|269=0|270=59.95|271=24|290=3|10=075|";

            List<string> incrementMessages = new List<string>();


            var fileName = @"C:\Users\Administrator\Desktop\Increment.txt";
            var fixLogs = TxtFile.ReadTxtFile(fileName);
            fixLogs.ForEach(msg =>
            {
                var incrementMsg = msg.Replace('|', (char)1);
                QuickFix.FIX42.MarketDataIncrementalRefresh marketDataIncrementalRefresh = new QuickFix.FIX42.MarketDataIncrementalRefresh();
                marketDataIncrementalRefresh.FromString(incrementMsg, false, null, dd);
                OrderBookMgr.Instance.processIncrementalRefresh(marketDataIncrementalRefresh);
            });
            //incrementMessage = incrementMessage.Replace('|', (char)1);
            //QuickFix.FIX42.MarketDataIncrementalRefresh marketDataIncrementalRefresh = new QuickFix.FIX42.MarketDataIncrementalRefresh();
            //marketDataIncrementalRefresh.FromString(incrementMessage, false, null, dd);
            //OrderBookMgr.Instance.processIncrementalRefresh(marketDataIncrementalRefresh);


            #endregion

            //this.btnTest.Text = "dsddssd";
            //AppReloader();
        }

        public void AppReloader()
        {
            //Start a new instance of the current program
            Process.Start(System.Windows.Forms.Application.ExecutablePath);

            //close the current application process
            Process.GetCurrentProcess().Kill();
        }




        private void ClearFix()
        {

            string[] dirArr = new string[] { QuickFix.SessionSettings.FILE_LOG_PATH, QuickFix.SessionSettings.FILE_STORE_PATH };
            string dateTime = DateTime.Now.ToString("_yyyyMMdd_HHmmss");
            for (int j = 0; j < dirArr.Length; j++)
            {
                if (!Directory.Exists(dirArr[j]))
                {
                    continue;
                }
                string[] strFiles = Directory.GetFiles(dirArr[j]);

                for (int i = 0; i < strFiles.Length; i++)
                {
                    string oneFile = strFiles[i];
                    if (oneFile.EndsWith(".log")
                        || oneFile.EndsWith(".body")
                        || oneFile.EndsWith(".header")
                        || oneFile.EndsWith(".seqnums")
                        )
                        File.Move(oneFile, oneFile + dateTime);
                }
            }
        }




        private void BtnGetContracts_Click(object sender, EventArgs e)
        {
            try
            {
                if (_tTPriceCommunication == null)
                {
                    return;
                }
                _tTPriceCommunication.DownloadAndSaveContracts();
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // CfgManager cfgManager = CfgManager.getInstance(null);
            if ("Yes".Equals(Configurations.AutoStart, StringComparison.CurrentCultureIgnoreCase))
            {
                btnStart_Click(sender, e);
            }
        }





        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!_serviceStopped)
            //{
            //    btnStop_Click(null, null);
            //}
            if (this.btnStop.Enabled)
            {
                if (this.tabControl1.SelectedIndex != 0)
                {
                    this.tabControl1.SelectedIndex = 0;
                }
                //System.Threading.Timer timer = new System.Threading.Timer(p =>
                //      {

                //      }, null, 0, 100);
                for (int i = 1; i <= 3; i++)
                {
                    this.btnStop.BackColor = Color.Red;
                    Application.DoEvents();
                    Thread.Sleep(100);
                    this.btnStop.BackColor = Color.Transparent;
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
                e.Cancel = true;
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        #region  设置结算价
        private void btnNew_PreveSett_Click(object sender, EventArgs e)
        {
            string zdCode = this.txtzdCode.Text.Trim();
            string sett = this.txtNew_PreveSett.Text.Trim();
            string previousSettbtnSetSettlementPrice = this.txtPreviousSettlementPrice.Text.Trim();
            string openingPrice = this.txtOpeningPrice.Text.Trim();
            if (string.IsNullOrEmpty(zdCode))
            {
                MessageBox.Show("请输入合约！");
                return;
            }
            if (string.IsNullOrEmpty(sett) && string.IsNullOrEmpty(previousSettbtnSetSettlementPrice) && string.IsNullOrEmpty(openingPrice))
            {
                MessageBox.Show("请输入要修改的字段值！");
                return;
            }

            OrderBookALL orderBookALL = OrderBookMgr.Instance.orderBookDict.Values.FirstOrDefault(p => p.codeBean.zdCode == zdCode);
            if (orderBookALL == null)
            {
                MessageBox.Show("没有找到该合约的数据！");
                return;
            }

            ////设置今结算
            //if (!string.IsNullOrEmpty(sett))
            //{
            //    double new_TodaySett = 0;
            //    if (double.TryParse(sett, out new_TodaySett))
            //    {
            //        if (orderBookALL.new_preveSett == new_TodaySett)
            //        {
            //            MessageBox.Show("设置今结算和昨结算价相同！");
            //            return;
            //        }
            //        orderBookALL.new_TodaySett = new_TodaySett;
            //    }
            //    else
            //    {
            //        MessageBox.Show("请输入正确的数值！");
            //        return;
            //    }
            //}

            ////设置昨结算
            //if (!string.IsNullOrEmpty(previousSettbtnSetSettlementPrice))
            //{
            //    double new_preveSett = 0;
            //    if (double.TryParse(previousSettbtnSetSettlementPrice, out new_preveSett))
            //    {
            //        orderBookALL.new_preveSett = new_preveSett;
            //    }
            //    else
            //    {
            //        MessageBox.Show("请输入正确的数值！");
            //        return;
            //    }
            //}
            //if(!string.IsNullOrEmpty(openingPrice))
            //{
            //    if(double.TryParse(openingPrice, out double newOpeningPrice))
            //    {
            //        orderBookALL.openingPrice = newOpeningPrice;
            //    }
            //    else
            //    {
            //        MessageBox.Show("请输入正确的数值！");
            //        return;
            //    }
            //}


            var settVerifyResult = VerifyDouble(sett);
            if (settVerifyResult.Success)
            {
                if (orderBookALL.new_preveSett == settVerifyResult.Value)
                {
                    MessageBox.Show("设置今结算和昨结算价相同！");
                    return;
                }
                orderBookALL.new_TodaySett = settVerifyResult.Value;
            }

            var previousVerify = VerifyDouble(previousSettbtnSetSettlementPrice);
            if (previousVerify.Success)
            {
                orderBookALL.new_preveSett = previousVerify.Value;
            }

            var openingPriceVerify = VerifyDouble(openingPrice);
            if (openingPriceVerify.Success)
            {
                orderBookALL.openingPrice = openingPriceVerify.Value;
            }
            Sub2Pumper.pumpSnapshotQuickFast(orderBookALL, $"{LogMessageType.PreveSett} MsgSeqNum:0");//发送到二级行情服务器
            MessageBox.Show("设置成功！");
        }

        private (bool Success, double Value) VerifyDouble(string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                if (double.TryParse(number, out double doubleValue))
                {
                    return (true, doubleValue);
                }
                else
                {
                    MessageBox.Show("请输入正确的数值！");
                    return (false, 0);
                }
            }
            return (false, 0);
        }

        #endregion

        #region  小工具

        #region 加载合约
        private void btnLoadContracts_Click(object sender, EventArgs e)
        {
            //品种映射
            string SECU_FILE = @"config\TT_Secu.dat";

            string SECU_OPT_FILE = @"config\TT_Secu_Opt.dat";
            List<QuickFix.FIX42.SecurityDefinition> futSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            List<QuickFix.FIX42.SecurityDefinition> spreadSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();
            List<QuickFix.FIX42.SecurityDefinition> optionSecuDefList = new List<QuickFix.FIX42.SecurityDefinition>();

            QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            dd.Load(Configurations.FIX42);

            if (File.Exists(SECU_FILE))
            {
                ReadFile(SECU_FILE);
                TT.Common.NLogUtility.Info("init instrument from SECU_FILE file data completed.");
            }

            if (File.Exists(SECU_OPT_FILE))
            {
                ReadFile(SECU_OPT_FILE);
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
            this.btnLoadContracts.Enabled = false;
        }
        #endregion

        #region   校验 TargetFutures里的配置没有取到合约的配置
        private void btnProductNoContract_Click(object sender, EventArgs e)
        {


            List<string> result = CodeTransfer_TT.tt2ZdMapping.Select(p => p.Value.upperProduct).Distinct().ToList();
            List<string> list1 = Configurations.TargetFutures.Split(';').ToList();
            List<string> uperCodeList = new List<string>();
            list1.ForEach(p =>
            {
                uperCodeList.Add(p.Split(',')[1]);
            });

            var except = uperCodeList.Except(result).ToList();
            //
            var str = string.Join(",", except);
            List<string> configList = new List<string>();

            list1.ForEach(m =>
            {

                var code = m.Split(',')[1];
                if (except.Contains(code))
                {
                    configList.Add(m);
                }
            });
            String configStr = string.Join(";", configList);

            string fileName = Path.Combine(GetLogDic(), "UpperNotFindContracts.txt");
            using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(configStr);
                }
            }
            System.Diagnostics.Process.Start(fileName);
        }
        #endregion

        #region 交易所分组
        /// <summary>
        /// 交易所分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEchangeProductSort_Click(object sender, EventArgs e)
        {

            var futureList = Configurations.TargetFutures.Split(';').ToList();
            var exchangeProductList = new List<(string Exchange, string Product)>();
            futureList.ForEach(p =>
            {
                var arr = p.Split(',');
                exchangeProductList.Add((arr[0], arr[1]));
            });
            var list = exchangeProductList.OrderBy(p => p.Exchange).ToList();

            StringBuilder sb = new StringBuilder();
            list.ForEach(p =>
            {
                sb.Append($"{p.Exchange}          {p.Product}\r\n");
            });



            string fileName = Path.Combine(GetLogDic(), "exchangeProducts.txt");
            using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(sb.ToString());
                }
            }
            System.Diagnostics.Process.Start(fileName);

        }

        private string GetLogDic()
        {
            string dateStr = DateTime.Now.ToString("yyyyMMdd");
            string dic = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Log\{dateStr}");
            if (!Directory.Exists(dic))
            {
                Directory.CreateDirectory(dic);
            }
            return dic;
        }

        #endregion

        #region 统计合约分类
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            int optCount = CodeTransfer_TT.tt2ZdMapping.Where(p => p.Value.contractType == "OPT").Select(p => p.Value).Count();
            int futCount = CodeTransfer_TT.tt2ZdMapping.Where(p => p.Value.contractType == "FUT").Select(p => p.Value).Count();
            int spCount = CodeTransfer_TT.tt2ZdMapping.Where(p => p.Value.contractType == "MLEG").Select(p => p.Value).Count();
            this.lblStatistics.Text = $"FUT:{futCount} SP:{spCount} OPT:{optCount}";
        }

        #endregion

        #region 导出期权合约文件
        /// <summary>
        /// 生成合约定义文件前进行了合约条数过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportOperations_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    this.btnExportOperations.Enabled = false;
                }));

            }

            //sfdOperations.Filter = "txt files (*.csv)|*.csv|(*.txt)|*.txt";
            //sfdOperations.RestoreDirectory = true;
            //if (sfdOperations.ShowDialog() == DialogResult.OK)
            //{
            //    SaveOperations(sfdOperations.FileName);
            //}

            Task.Factory.StartNew(() =>
            {
                SaveOperations();
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    this.btnExportOperations.Enabled = true;

                    if (sender != null)
                    {
                        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Options");
                        System.Diagnostics.Process.Start(path);
                    }
                }));
            });
        }
        public void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                TT.Common.NLogUtility.Error(e.ToString());
            }
        }
        private void SaveOperations()
        {
            string baseDic = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Options");
            if (!Directory.Exists(baseDic))
            {
                Directory.CreateDirectory(baseDic);
            }
            else
            {
                DelectDir(baseDic);
            }
            List<CodeBean> result = CodeTransfer_TT.tt2ZdMapping.Where(p => p.Value.contractType == "OPT").Select(p => p.Value).ToList();

            //if (!string.IsNullOrEmpty(this.txtOption.Text.Trim()))
            //{
            //    result = result.Where(p => p.upperProduct == this.txtOption.Text.Trim()).ToList();
            //}
            List<string> uperProductList = result.Select(p => p.upperProduct).Distinct().ToList();

            //获取所有的期权代码
            List<string> optionsStrList = result.Select(p => p.zdProduct).Distinct().ToList();

            StringBuilder where = new StringBuilder("( ");
            //获取期权代码信息
            for (int i = 0; i < optionsStrList.Count; i++)
            {
                where.Append($@"'{optionsStrList[i]}'");
                if (i == optionsStrList.Count - 1)
                {
                    where.Append(" )");
                }
                else
                {
                    where.Append(" ,");
                }
            }
            //            string selectCommand = $@" 
            //	       select  t2.FutureID, t.FCommodityNo FutureCode,t2.OptionID,t2.OptionCode,t2.OptionName,
            //             e.FExchangeNo ,t2.FCurrencyNo,c.[FName] CurrencyName from [ForeignShare].[dbo].[TCommodity] t
            //      join  (
            //				  SELECT [FCommodityID] OptionID,[FStrikeCommodityId] FutureID,[FCommodityNo] OptionCode ,
            //						 [FName] OptionName,[FCurrencyNo]
            //				  FROM [ForeignShare].[dbo].[TCommodity]
            //				  WHERE [FCommodityNo] IN {where.ToString()} 
            //			 )  t2  on t.[FCommodityID]=t2.FutureID
            //	 join [ForeignShare].[dbo].[TExchange] e  on t.[FExchangeID]=e.FExchangeID
            //     join [ForeignShare].[dbo].[TCurrency] c  on t.FCurrencyNo=c.FCurrencyNo

            //";
            //            var tCommodityList = DBHelper<TCommodity>.GetList(selectCommand);

            string contractCode = string.Empty; //合约编号,
            string contractName = string.Empty; //合约名称,
            string productCode = string.Empty;//商品编号 OVS2_C
            string productName = string.Empty; //商品名称
            string currencyCode = string.Empty;//货币编号
            string currencyName = string.Empty;//货币名称
            string tickPrice = string.Empty;//账面跳点价值
            string changeUnit = string.Empty;//最小变动单位
            string productStyle = string.Empty;//商品类型
            string exchangeCode = string.Empty;//市场编号
            string exchangeName = string.Empty;//市场名称
            string previousSettlement = string.Empty;//昨结算
            string contractYearMonth = string.Empty;//合约年月
            string bidUnit = string.Empty;//进价单位
            string productID = string.Empty;//商品ID
            string exchangeProductCode = string.Empty;//交易所商品代码
            string maturityDate = string.Empty;//合约到期日
            string lastDealDate = string.Empty;//最后交易日
            string maxOrderAmount = string.Empty;//最大下单量
            string contractSize = string.Empty;//合约大小
            string firstNoticeDate = string.Empty;//首次通知日
            string futuresProductID = string.Empty;//期货商品ID
            string futuresContract = string.Empty;//期货合约
            string excutePrice = string.Empty;//执行价
            string spotGoodsID = string.Empty;//现货商品ID
            string optionStyle = string.Empty;//期权类型
            string warningNoticeDate = string.Empty;  //警示通知日
            string eletronicDealEndDate = string.Empty; //电子交易截至日
            string minChangeUnit2 = string.Empty; //最小变动单位2
            string minChangeUnitConvertPoint = string.Empty; //最小变动单位转换点
            string futuresProduct = string.Empty;//期货商品
            string spotGoods = string.Empty;//现货商品
            StringBuilder sb = new StringBuilder();
            string path = string.Empty;
            uperProductList.ForEach(u =>
            {
                path = Path.Combine(baseDic, $"{u}.csv");
                sb.Append("合约编号,合约名称,商品编号,商品名称,货币编号,货币名称,账面跳点价值,最小变动单位,商品类型,市场编号,市场名称,昨日结算,合约年月,进价单位,商品ID,交易所商品代码,合约到期日,最后交易日,最大下单量,合约大小,首次通知日,期货商品ID,期货合约,执行价,现货商品ID,期权类型,警示通知日,电子交易截至日,最小变动单位2,最小变动单位转换点,期货商品,现货商品");
                WriterString(path, sb.ToString());
                sb.Clear();
                var currentProductList = result.Where(p => p.upperProduct == u).OrderBy(p => p.zdContractDate).ToList();
                //获取合约的中文名部分
                string chineseSubName = Configurations.ContractChineseNameList.Find(p => p.TTProduct == u).ContractChineseName;
                //获取期权的期货现货商品。
                var optFutSpot = Configurations.OptFutSpotList.Find(p => p.TTProduct == u);
                //获取ZD交易所
                var zdExchange = Configurations.GetZDExchangeProduct(u).ZDExchange;
                foreach (var p in currentProductList)
                {

                    try
                    {

                        SecurityDefinition securityDefinition = CodeTransfer_TT.zd2TTMapping[p.zdCode];

                        ////第二个455
                        //QuickFix.Group securityAltIDGrp = securityDefinition.GetGroup(2, Tags.NoSecurityAltID);
                        ////455=BRN Aug20 P76
                        //var securityAltID = securityAltIDGrp.GetString(Tags.SecurityAltID);
                        //if (securityAltID == "BRN Aug20 P76")
                        //{

                        //}

                        string[] namePCs = p.zdCode.Split('_');
                        //P还是C
                        string pOrC = namePCs[1].Substring(0, 1);
                        //p.zdCode OVS2_C1804 1
                        int index = p.zdCode.IndexOf("_");
                        //合约编号,1804 1
                        contractCode = p.zdCode.Substring(index + 2, p.zdCode.Length - index - 2);

                        //TCommodity tCommodity = tCommodityList.Find(m => m.OptionCode == p.zdProduct);
                        //if (tCommodity == null)
                        //{
                        //    break;
                        //}
                        //合约名称,
                        //contractName = $"{ tCommodity.OptionName}{ contractCode}";
                        contractName = $"{chineseSubName}{(pOrC == "P" ? "跌" : "涨")}{ contractCode}";
                        //商品编号 OVS2_C
                        productCode = p.zdProduct;
                        //商品名称  11号糖看涨1807 13.25
                        //productName = tCommodity.OptionName;
                        productName = $"{chineseSubName}{(pOrC == "P" ? "跌" : "涨")}";
                        //货币编号 TAG 15
                        //currencyCode = tCommodity.FCurrencyNo;
                        currencyCode = securityDefinition.Currency.getValue().ToString();
                        //货币名称
                        //currencyName = tCommodity.CurrencyName;
                        currencyName = Configurations.CurrencyNameDic[currencyCode]; ;
                        //账面跳点价值
                        tickPrice = securityDefinition.ExchPointValue.getValue().ToString();
                        //最小变动单位  16552
                        changeUnit = securityDefinition.ExchTickSize.getValue().ToString();
                        //商品类型 
                        productStyle = "O";
                        //市场编号
                        exchangeCode = zdExchange;// tCommodity.FExchangeNo;
                        //市场名称
                        exchangeName = zdExchange;// tCommodity.FExchangeNo;
                        //昨日结算
                        previousSettlement = "0";
                        //合约年月
                        contractYearMonth = p.zdContractDate;

                        //小数个数
                        int decimalCount = changeUnit.Length - changeUnit.IndexOf(".") - 1;
                        //进价单位 16552
                        bidUnit = Math.Pow(10, decimalCount).ToString();
                        //商品ID
                        //productID = tCommodity.FutureID.ToString();
                        productID = "-1";
                        //交易所商品代码 OVS2_C
                        exchangeProductCode = "";

                        //合约到期日 
                        maturityDate = securityDefinition.MaturityDate.getValue();
                        //最后交易日
                        lastDealDate = maturityDate;
                        // 最大下单量,
                        maxOrderAmount = "0";
                        //合约大小,
                        contractSize = "0";
                        //首次通知日 
                        firstNoticeDate = maturityDate;
                        //期货商品ID
                        //futuresProductID = tCommodity.OptionID.ToString();
                        futuresProductID = "-1";
                        //期货合约
                        futuresContract = p.zdContractDate;
                        //202009
                        //securityDefinition.ContractYearMonth.getValue().Substring(2);
                        #region   futuresContract 期货合约 

                        #region   old

                        //string dateStr = p.zdContractDate;
                        ////期货合约
                        //string year = dateStr.Substring(0, 2);
                        //int month = int.Parse(dateStr.Substring(2, 2));
                        //switch (p.upperProduct)
                        //{

                        //    case "DX":
                        //        if (month <= 3)
                        //        {
                        //            month = 3;
                        //            futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //        }
                        //        else if (month >= 4 && month <= 6)
                        //        {
                        //            month = 6;
                        //            futuresContract = year + string.Format("{0:00}", month);
                        //        }
                        //        else if (month >= 7 && month <= 9)
                        //        {
                        //            month = 9;
                        //            futuresContract = year + string.Format("{0:00}", month);
                        //        }
                        //        else
                        //        {
                        //            month = 12;
                        //            futuresContract = year + string.Format("{0:00}", month);
                        //        }
                        //        break;
                        //    case "KC":
                        //        switch (month)
                        //        {
                        //            case 1:
                        //            case 2:
                        //            case 3:
                        //                month = 3;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 4:
                        //            case 5:
                        //                month = 5;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 6:
                        //            case 7:
                        //                month = 7;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 8:
                        //            case 9:
                        //                month = 9;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 10:
                        //            case 11:
                        //            case 12:
                        //                month = 12;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //        }


                        //        break;
                        //    case "CT":
                        //        switch (month)
                        //        {
                        //            case 1:
                        //            case 2:
                        //            case 3:
                        //                month = 3;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 4:
                        //            case 5:
                        //                month = 5;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 6:
                        //            case 7:
                        //                month = 7;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 8:
                        //            case 10:
                        //                month = 10;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 9:
                        //            case 11:
                        //            case 12:
                        //                month = 12;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //        }
                        //        break;
                        //    case "SB":
                        //        switch (month)
                        //        {
                        //            case 1:
                        //            case 2:
                        //            case 3:
                        //                month = 3;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 4:
                        //            case 5:
                        //                month = 5;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 6:
                        //            case 7:
                        //                month = 7;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 8:
                        //            case 9:
                        //            case 10:
                        //                month = 10;
                        //                futuresContract = year + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            case 11:
                        //            case 12:
                        //                month = 3;
                        //                futuresContract = (int.Parse(year) + 1).ToString() + month.ToString("D2");// string.Format("{0:00}", month);
                        //                break;
                        //            default:
                        //                break;
                        //        }
                        //        break;
                        //    default:
                        //        //"BRN",OVS2
                        //        //期货合约
                        //        futuresContract = p.zdContractDate;// securityDefinition.ContractYearMonth.getValue().Substring(2);
                        //        break;
                        //}
                        #endregion

                        #region   new

                        if (Configurations.OptionFutureMonthDic.ContainsKey(u))
                        {
                            //202009
                            string dateStr = p.zdContractDate;
                            //期货合约
                            string year = dateStr.Substring(0, 2);
                            int month = int.Parse(dateStr.Substring(2, 2));
                            var monthStr = month + "";
                            //if (u == "CT" && month == 11)
                            //{

                            //}

                            var optionFutureMonthSubDic = Configurations.OptionFutureMonthDic[u];
                            var futureMonth = optionFutureMonthSubDic.First(d => d.Value.Contains(monthStr)).Key;
                            futuresContract = year + int.Parse(futureMonth).ToString("D2");
                        }
                        #endregion

                        #endregion
                        int emptyIndex = p.zdCode.LastIndexOf(' ');
                        //tag 202 执行价
                        excutePrice = p.zdCode.Substring(emptyIndex + 1, p.zdCode.Length - emptyIndex - 1);
                        //现货商品ID
                        //spotGoodsID = tCommodity.OptionID.ToString();
                        spotGoodsID = "-1";

                        //期权类型 c
                        optionStyle = pOrC == "C" ? "R" : "F";
                        //警示通知日
                        warningNoticeDate = maturityDate;
                        //电子交易截至日
                        eletronicDealEndDate = maturityDate;
                        //最小变动单位2 16552
                        minChangeUnit2 = changeUnit;
                        //最小变动单位转换点
                        minChangeUnitConvertPoint = "0";
                        //期货商品
                        //futuresProduct = tCommodity.FutureCode;
                        futuresProduct = optFutSpot.Future;

                        //现货商品
                        //spotGoods = tCommodity.FutureCode;
                        spotGoods = optFutSpot.Spot;

                        sb.Append(contractCode).Append(","); //合约编号,
                        sb.Append(contractName).Append(","); //合约名称,
                        sb.Append(productCode).Append(",");//商品编号 OVS2_C
                        sb.Append(productName).Append(","); //商品名称
                        sb.Append(currencyCode).Append(",");//货币编号
                        sb.Append(currencyName).Append(",");//货币名称
                        sb.Append(tickPrice).Append(",");//账面跳点价值
                        sb.Append(changeUnit).Append(",");//最小变动单位
                        sb.Append(productStyle).Append(",");//商品类型
                        sb.Append(exchangeCode).Append(",");//市场编号
                        sb.Append(exchangeName).Append(",");//市场名称
                        sb.Append(previousSettlement).Append(",");//昨结算
                        sb.Append(contractYearMonth).Append(",");//合约年月
                        sb.Append(bidUnit).Append(",");//进价单位
                        sb.Append(productID).Append(",");//商品ID
                        sb.Append(exchangeProductCode).Append(",");//交易所商品代码
                        sb.Append(maturityDate).Append(",");//合约到期日
                        sb.Append(lastDealDate).Append(",");//最后交易日
                        sb.Append(maxOrderAmount).Append(",");//最大下单量
                        sb.Append(contractSize).Append(",");//合约大小
                        sb.Append(firstNoticeDate).Append(",");//首次通知日
                        sb.Append(futuresProductID).Append(",");//期货商品ID
                        sb.Append(futuresContract).Append(",");//期货合约
                        sb.Append(excutePrice).Append(",");//执行价
                        sb.Append(spotGoodsID).Append(",");//现货商品ID
                        sb.Append(optionStyle).Append(",");//期权类型
                        sb.Append(warningNoticeDate).Append(",");  //警示通知日
                        sb.Append(eletronicDealEndDate).Append(","); //电子交易截至日
                        sb.Append(minChangeUnit2).Append(","); //最小变动单位2
                        sb.Append(minChangeUnitConvertPoint).Append(","); //最小变动单位转换点
                        sb.Append(futuresProduct).Append(",");   //期货商品
                        sb.Append(spotGoods).Append(","); //现货商品


                        WriterString(path, sb.ToString());


                        sb.Clear();
                        contractCode = string.Empty; //合约编号,
                        contractName = string.Empty; //合约名称,
                        productCode = string.Empty;//商品编号 OVS2_C
                        productName = string.Empty; //商品名称
                        currencyCode = string.Empty;//货币编号
                        currencyName = string.Empty;//货币名称
                        tickPrice = string.Empty;//账面跳点价值
                        changeUnit = string.Empty;//最小变动单位
                        productStyle = string.Empty;//商品类型
                        exchangeCode = string.Empty;//市场编号
                        exchangeName = string.Empty;//市场名称
                        previousSettlement = string.Empty;//昨结算
                        contractYearMonth = string.Empty;//合约年月
                        bidUnit = string.Empty;//进价单位
                        productID = string.Empty;//商品ID
                        exchangeProductCode = string.Empty;//交易所商品代码
                        maturityDate = string.Empty;//合约到期日
                        lastDealDate = string.Empty;//最后交易日
                        maxOrderAmount = string.Empty;//最大下单量
                        contractSize = string.Empty;//合约大小
                        firstNoticeDate = string.Empty;//首次通知日
                        futuresProductID = string.Empty;//期货商品ID
                        futuresContract = string.Empty;//期货合约
                        excutePrice = string.Empty;//执行价
                        spotGoodsID = string.Empty;//现货商品ID
                        optionStyle = string.Empty;//期权类型
                        warningNoticeDate = string.Empty;  //警示通知日
                        eletronicDealEndDate = string.Empty; //电子交易截至日
                        minChangeUnit2 = string.Empty; //最小变动单位2
                        minChangeUnitConvertPoint = string.Empty; //最小变动单位转换点
                        futuresProduct = string.Empty;//期货商品
                        spotGoods = string.Empty;//现货商品
                    }
                    catch (Exception ex)
                    {
                        TT.Common.NLogUtility.Error(ex.ToString());
                    }
                }
            });
        }

        private void SaveOptionStr(string path, string strValue)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Append, FileAccess.Write), System.Text.Encoding.Default))
            {
                sw.WriteLine(strValue);
            }
        }

        private void WriterString(string path, string strValue)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Append, FileAccess.Write), System.Text.Encoding.Default))
            {
                sw.WriteLine(strValue);
            }
        }
        #endregion

        #region 过滤合约数据
        private void btnFiltrateContracts_Click(object sender, EventArgs e)
        {
            //Server.MapPath("/")    返回路径为：E:\wwwroot
            List<SecurityDefinition> mathchSpreadList = new List<SecurityDefinition>();
            List<SecurityDefinition> mathchOptList = new List<SecurityDefinition>();

            List<SecurityDefinition> futureList = new List<SecurityDefinition>();
            List<SecurityDefinition> spreadList = new List<SecurityDefinition>();
            List<SecurityDefinition> optionList = new List<SecurityDefinition>();

            foreach (SecurityDefinition sd in CodeTransfer_TT.zd2TTMapping.Values)
            {
                var type = sd.SecurityType.getValue();
                switch (type)
                {

                    case "FUT":
                        futureList.Add(sd);
                        break;
                    case "MLEG":
                        spreadList.Add(sd);
                        break;
                    case "OPT":
                        optionList.Add(sd);
                        break;
                    default:
                        TT.Common.NLogUtility.Info($"default type={type}");
                        break;
                }
            }

            int addMonth = (int)nudMonths.Value;
            DateTime later = DateTime.Now.AddMonths(addMonth);

            //过滤spread
            int spreadLaterMonth = addMonth;
            while (true)
            {
                mathchSpreadList = GetspecifyMonthContracts(spreadList);
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
            later = DateTime.Now.AddMonths(addMonth + 20);
            int optLaterMonth = addMonth;
            while (true)
            {
                mathchOptList = GetspecifyMonthContracts(optionList);
                if (mathchOptList.Count > 10000)
                {
                    later = DateTime.Now.AddMonths(--optLaterMonth);
                }
                else
                {
                    break;
                }
            }
            this.lblMonth.Text += $"spread:{spreadLaterMonth} opt:{optLaterMonth}";

            List<SecurityDefinition> GetspecifyMonthContracts(List<SecurityDefinition> sourceList)
            {
                List<SecurityDefinition> mathchList = new List<SecurityDefinition>();
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
            //备份之前的合约
            string SECU_FILE = @"config\TT_Secu.dat";
            string SECU_OPT_FILE = @"config\TT_Secu_Opt.dat";
            var extension = Path.GetExtension(SECU_FILE);
            if (File.Exists(SECU_FILE))
            {
                TT.Common.NLogUtility.Info("compareContract:   backup " + SECU_FILE);
                File.Move(SECU_FILE, @"config\" + Path.GetFileNameWithoutExtension(SECU_FILE) + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + extension);
            }
            if (File.Exists(SECU_OPT_FILE))
            {
                TT.Common.NLogUtility.Info("compareContract:   backup " + SECU_OPT_FILE);
                File.Move(SECU_OPT_FILE, @"config\" + Path.GetFileNameWithoutExtension(SECU_OPT_FILE) + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + extension);
            }


            string futFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config/TT_Secu.dat");
            string optFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config/TT_Secu_Opt.dat");


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
            System.Diagnostics.Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config"));
        }
        #endregion

        private void btnDicLog_Click(object sender, EventArgs e)
        {
            SendMessageLog.Log(true, "JB", "JB1903", "FullRefresh:HKEX@HHI1803@12565@1@12566@3@12565@1@12629@12536@12594@@@2018-03-14 13:40:04@38485@0@12564@12563@12562@12561@7@5@12@5@12567@12568@12569@12570@4@14@7@15@12561@1@12568@1@Z@@15@@12741@@Z");
        }

        private void btnOpenDic_Click(object sender, EventArgs e)
        {
            //foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            //{
            //  var config=   ConfigurationManager.AppSettings[key];
            //}
            //exe
            //if (File.Exists(@"C:\Program Files (x86)\TeamViewer\TeamViewer.exe"))
            //{
            //    System.Diagnostics.Process.Start(@"C:\Program Files (x86)\TeamViewer\TeamViewer.exe");
            //}
            //快捷方式
            //if (File.Exists("C:\\Users\\Administrator\\Desktop\\工具\\TeamViewer 13.lnk"))
            //{
            //    System.Diagnostics.Process.Start("C:\\Users\\Administrator\\Desktop\\工具\\TeamViewer 13.lnk");
            //}
            //foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            //{
            //    var path = ConfigurationManager.AppSettings[key];
            //    if (path.EndsWith(".lnk") || path.EndsWith(".exe"))
            //    {
            //        if (File.Exists(path))
            //        {
            //            System.Diagnostics.Process.Start(path);
            //        }
            //    }
            //}
            //string path = "C:\\Users\\Administrator\\Desktop\\工具\\TeamViewer 13.lnk";
            //if (path.EndsWith(".lnk") || path.EndsWith(".exe"))
            //{
            //    if (File.Exists(path))
            //    {
            //        System.Diagnostics.Process.Start(path);
            //    }
            //}
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }


        private void BtnLogAnalysis_Click(object sender, EventArgs e)
        {
            if (_frmLogAnalysis == null || _frmLogAnalysis.IsDisposed)
            {
                _frmLogAnalysis = new FrmLogAnalysis();
            }

            if (_frmLogAnalysis.WindowState == FormWindowState.Minimized)
            {
                _frmLogAnalysis.WindowState = FormWindowState.Normal;
            }

            //  _frmLogAnalysis.TopMost = true;
            _frmLogAnalysis.Show();
        }

        private void BtnFactor_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var key in CodeTransfer_TT.PrdPrxFactorDict.Keys)
                {
                    if (CodeTransfer_TT.PrdPrxFactorDict[key] != 1m)
                    {
                        var factor = CodeTransfer_TT.PrdPrxFactorDict[key];
                        stringBuilder.Append($"{key}:{factor};");
                    }

                }
                string factorStr = stringBuilder.ToString().TrimEnd(';');

                Configurations.UpdateConfig("DisplayPrxFactor", factorStr, null);
                //刷新内存里配置变量的值。
                Configurations.DisplayPrxFactor = factorStr;
                //查看是否更新成功
                //var val = ConfigurationManager.AppSettings["DisplayPrxFactor"];
                NLogUtility.Info($"更新倍率配置(DisplayPrxFactor)：{factorStr}");
            }
            catch (Exception ex)
            {
                NLogUtility.Error(ex.ToString());
            }
        }






        #endregion





        //private Dictionary<string, SecurityDefinition> ZDCodeSecurityDefinitionDic(List<SecurityDefinition> list)
        //{
        //    Dictionary<string, SecurityDefinition> dic = new Dictionary<string, SecurityDefinition>();
        //    list.ForEach(p =>
        //    {
        //        CodeBean codeBean = new CodeBean();
        //        string upperExchg = p.SecurityExchange.getValue();
        //        string upperPrdCd = p.Symbol.getValue();
        //        string uppperKey = upperExchg + "," + upperPrdCd;

        //        string securityType = p.SecurityType.getValue();
        //        switch (securityType)
        //        {
        //            case "FUT":
        //                if (CodeTransfer_TT.mismatchPrdCd.ContainsKey(uppperKey))
        //                    codeBean.zdProduct = CodeTransfer_TT.mismatchPrdCd[uppperKey];
        //                else
        //                    codeBean.zdProduct = upperPrdCd;
        //                CodeTransfer_TT.transferFutureToZDCode(codeBean, p);
        //                break;
        //            case "MLEG":
        //                string zdCode = null;
        //                string zdPrdCd = null;
        //                if (CodeTransfer_TT.mismatchPrdCd.ContainsKey(uppperKey))
        //                {
        //                    zdPrdCd = CodeTransfer_TT.mismatchPrdCd[uppperKey];
        //                    zdCode = zdPrdCd + "_S";
        //                    codeBean.zdProduct = zdCode;
        //                }
        //                else
        //                {
        //                    zdPrdCd = upperPrdCd;
        //                    zdCode = zdPrdCd + "_S";
        //                    codeBean.zdProduct = zdCode;
        //                }
        //                CodeTransfer_TT.transferSpreadToZDCode(zdCode, codeBean, p);
        //                break;
        //            case "OPT":
        //                int putOrCall = p.PutOrCall.getValue();
        //                if (CodeTransfer_TT.mismatchPrdCd.ContainsKey(uppperKey))
        //                    codeBean.zdProduct = CodeTransfer_TT.mismatchPrdCd[uppperKey];
        //                else
        //                    codeBean.zdProduct = upperPrdCd;
        //                string zdProduct = codeBean.zdProduct + "_" + (putOrCall == 0 ? "P" : "C");
        //                codeBean.upperExchg = upperExchg;
        //                codeBean.upperProduct = upperPrdCd;
        //                codeBean.zdProduct = zdProduct;
        //                codeBean.contractType = securityType;
        //                CodeTransfer_TT.transferOptionToZDCode(codeBean, p);
        //                break;

        //        }

        //        if (!dic.Keys.Contains(codeBean.zdCode))
        //        {
        //            dic.Add(codeBean.zdCode, p);
        //        }
        //    });
        //    return dic;
        //}




    }
}
