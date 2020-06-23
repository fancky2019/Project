using AuthCommon;
using Newtonsoft.Json;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX42;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TT.Common;

namespace TTMarketAdapter
{
    /// <summary>
    /// 处理接收到TT的快照和增量数据
    /// </summary>
    public class OrderBookMgr
    {
        public static OrderBookMgr Instance { get; set; }
        public Dictionary<string, OrderBookALL> orderBookDict = null;

        private OrderBookALL ladderBookAll = new OrderBookALL(5, 2, 1f);

        /// <summary>
        /// 所有品种总成交金额 ，Key:zdCode ,Value:TradingVolume
        /// </summary>
        public Dictionary<string, Decimal> TradingVolumeDic = null;

        public const string OrderBookAllData = @"config\OrderBookAllData.data";

        static OrderBookMgr()
        {
            if (Instance == null)
            {
                Instance = new OrderBookMgr();
            }
        }

        private bool _init = false;
        public bool InitCompleted
        {
            get
            {
                return _init;
            }
        }
        public void Init()
        {
            TT.Common.NLogUtility.Info($"OrderBookMgr Init");
            if (_init)
            {
                TT.Common.NLogUtility.Info("OrderBookMgr Init return");
                return;
            }
            _init = true;

            //fancky add 2017-12-12  读取昨结算价和今结算价
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config/PreveTodaySett.txt");
            Dictionary<string, string[]> preTodaySettDic = new Dictionary<string, string[]>();
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    try
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine().Trim();
                            if (line != null && line.Trim().Length != 0)
                            {
                                string[] fields = line.Split(',');
                                string securityId = fields[0];
                                string new_preveSett = fields[2];
                                string new_TodaySett = fields[3];
                                if (!preTodaySettDic.ContainsKey(securityId))
                                {
                                    preTodaySettDic.Add(securityId, new string[] { securityId, new_preveSett, new_TodaySett });
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TT.Common.NLogUtility.Error(ex.ToString());
                    }
                }
            }

            ladderBookAll = new OrderBookALL(5, 2, 1f);

            orderBookDict = new Dictionary<string, OrderBookALL>();



            foreach (string securityID in CodeTransfer_TT.tt2ZdMapping.Keys)
            {
                CommonClassLib.ContractTransfer.CodeBean codeBean = CodeTransfer_TT.tt2ZdMapping[securityID];
                try
                {
                    SecurityDefinition sd = CodeTransfer_TT.zd2TTMapping[codeBean.zdCode];
                    float tickSize = (float)sd.ExchTickSize.getValue();

                    OrderBookALL orderBookAll = new OrderBookALL(5, 2, tickSize);
                    orderBookAll.codeBean = codeBean;

                    //fancky add 2017-12-12
                    //赋值昨、今结算价
                    if (preTodaySettDic.TryGetValue(securityID, out string[] preTodaySettArr))
                    {
                        orderBookAll.new_preveSett = double.Parse(preTodaySettArr[1]);
                        orderBookAll.new_TodaySett = double.Parse(preTodaySettArr[2]);
                    }

                    //orderBookAll.securityId = securityID;
                    orderBookDict.Add(securityID, orderBookAll);
                }
                catch (Exception ex)
                {
                    TT.Common.NLogUtility.Error(ex.ToString());
                }
            }

            //processHKEXData();
            LoadOrderBookAllData();

            //初始化成交量
            TradingVolumeDic = new Dictionary<string, Decimal>();
            var tradingVolumeDatas = TxtFile.ReadTxtFile(@"config/TradeVolumeData.txt");
            tradingVolumeDatas.ForEach(p =>
                {
                    try
                    {
                        var arr = p.Split(',');
                        TradingVolumeDic.Add(arr[0], decimal.Parse(arr[1]));
                    }
                    catch (Exception ex)
                    {
                        TT.Common.NLogUtility.Error("加载TradeVolumeData.txt文件出错！");
                        TT.Common.NLogUtility.Error(ex.ToString());
                    }

                });

            TT.Common.NLogUtility.Info($"orderBookDict.Count={orderBookDict.Count}");

        }


        public void LoadOrderBookAllData()
        {
            try
            {
                if (File.Exists(OrderBookAllData))
                {

                    DateTime dtNow = DateTime.Now;
                    int val = dtNow.Hour * 60 + dtNow.Minute;
                    bool isLoadHoldTradeVolume = false;
                    if (val > 16 * 60 + 30)
                        isLoadHoldTradeVolume = true;
                    using (StreamReader sReader = new StreamReader(File.Open(OrderBookAllData, FileMode.Open), System.Text.Encoding.ASCII))
                    {
                        while (!sReader.EndOfStream)
                        {
                            string oneLine = sReader.ReadLine().Trim();
                            if (string.IsNullOrEmpty(oneLine))
                                continue;

                            //ZDCode:SecurityID:PrevSettlPrx:T0TradeVol
                            //HSI1608:22286242:21679:19998
                            string[] strArr = oneLine.Split(':');

                            //if (strArr.Length < 4) continue;
                            //Console.WriteLine("load special data: " + oneLine);

                            //GlobalData.specialContractSet.Add(strArr[0]);
                            // GlobalData.specialSecurityIDSet.Add(strArr[1]);

                            OrderBookALL orderBookAll;
                            if (orderBookDict.TryGetValue(strArr[1], out orderBookAll))
                            {

                                double.TryParse(strArr[2], out orderBookAll.settPrice);

                                if (isLoadHoldTradeVolume)
                                    int.TryParse(strArr[3], out orderBookAll.holdTradeVolume);
                                else
                                    orderBookAll.holdTradeVolume = 0;

                                //fancky add 2019-01-11
                                orderBookAll.openingPrice = double.Parse(strArr[4]);
                                orderBookAll.tsHighPrice = double.Parse(strArr[5]);
                                orderBookAll.tsLowPrice = double.Parse(strArr[6]);

                            }
                        }
                    }
                    TT.Common.NLogUtility.Info($"Loading OrderBookAllData Complete!");
                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }
        //public const string HKEX_SPECIAL_DATA = @"config\hkex_data.txt";
        //public void processHKEXData()
        //{
        //try
        //{
        //    if (File.Exists(HKEX_SPECIAL_DATA))
        //    {

        //        DateTime dtNow = DateTime.Now;
        //        int val = dtNow.Hour * 60 + dtNow.Minute;
        //        bool isLoadHoldTradeVolume = false;
        //        if (val > 16 * 60 + 30)
        //            isLoadHoldTradeVolume = true;

        //        using (StreamReader sReader = new StreamReader(File.Open(HKEX_SPECIAL_DATA, FileMode.Open), System.Text.Encoding.ASCII))
        //        {
        //            while (!sReader.EndOfStream)
        //            {
        //                string oneLine = sReader.ReadLine().Trim();
        //                if (string.IsNullOrEmpty(oneLine))
        //                    continue;

        //                //ZDCode:SecurityID:PrevSettlPrx:T0TradeVol
        //                //HSI1608:22286242:21679:19998
        //                string[] strArr = oneLine.Split(':');

        //                if (strArr.Length < 4) continue;
        //                Console.WriteLine("load special data: " + oneLine);

        //                GlobalData.specialContractSet.Add(strArr[0]);
        //                GlobalData.specialSecurityIDSet.Add(strArr[1]);

        //                OrderBookALL orderBookAll;
        //                if (orderBookDict.TryGetValue(strArr[1], out orderBookAll))
        //                {
        //                    double.TryParse(strArr[2], out orderBookAll.settPrice);

        //                    if (isLoadHoldTradeVolume)
        //                        int.TryParse(strArr[3], out orderBookAll.holdTradeVolume);
        //                    else
        //                        orderBookAll.holdTradeVolume = 0;
        //                }
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Error: Process HKEX data(settlePrx,tradeVol)");
        //    Console.WriteLine(ex.ToString());
        //}
        //}

        #region 快照

        /// <summary>
        /// 快照 35=W
        /// </summary>
        /// <param name="fullReresh"></param>
        //public void processFullRefresh(MarketDataSnapshotFullRefresh fullReresh)
        public void processFullRefresh(MarketDataSnapshot fullReresh)
        {
            //  logger.log(3, $"processFullRefresh  {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            OrderBookALL ordBookAll;
            if (!orderBookDict.TryGetValue(fullReresh.SecurityID.getValue(), out ordBookAll))
                return;

            //if (fullReresh.IsSetField(Tags.TotalVolumeTraded))
            //    ordBookAll.tradeVolume = (int)fullReresh.TotalVolumeTraded.getValue();

            for (int i = 1; i <= fullReresh.NoMDEntries.getValue(); i++)
            {
                Group g = fullReresh.GetGroup(i, Tags.NoMDEntries);
                char entryType = g.GetChar(Tags.MDEntryType);

                #region switch block
                switch (entryType)
                {
                    case '0':
                        {
                            int lvl = g.GetInt(Tags.MDEntryPositionNo);
                            PriceDetail pd = ordBookAll.GBXOrderBook.bidList[lvl - 1];
                            pd.Price = (double)g.GetDecimal(Tags.MDEntryPx);
                            pd.Quantity = g.GetInt(Tags.MDEntrySize);
                        }
                        break;
                    case '1':
                        {
                            int lvl = g.GetInt(Tags.MDEntryPositionNo);
                            PriceDetail pd = ordBookAll.GBXOrderBook.askList[lvl - 1];
                            pd.Price = (double)g.GetDecimal(Tags.MDEntryPx);
                            pd.Quantity = g.GetInt(Tags.MDEntrySize);
                        }
                        break;

                    case 'Y':
                        //result = "ImpliedBid";
                        {
                            int lvl = g.GetInt(Tags.MDEntryPositionNo);
                            PriceDetail pd = ordBookAll.GBIOrderBook.bidList[lvl - 1];
                            pd.Price = (double)g.GetDecimal(Tags.MDEntryPx);
                            pd.Quantity = g.GetInt(Tags.MDEntrySize);
                        }
                        break;
                    case 'Z':
                        //result = "ImpliedAsk";
                        {
                            int lvl = g.GetInt(Tags.MDEntryPositionNo);
                            PriceDetail pd = ordBookAll.GBIOrderBook.askList[lvl - 1];
                            pd.Price = (double)g.GetDecimal(Tags.MDEntryPx);
                            pd.Quantity = g.GetInt(Tags.MDEntrySize);
                        }
                        break;

                    case '2':
                        //result = "trade";
                        {
                            PriceDetail pd = ordBookAll.lastTrade;
                            pd.Price = (double)g.GetDecimal(Tags.MDEntryPx);
                            pd.Quantity = g.GetInt(Tags.MDEntrySize);

                        }
                        break;

                    case '4':
                        // Opening price
                        ordBookAll.openingPrice = (double)g.GetDecimal(Tags.MDEntryPx);
                        break;

                    //market-data-snapshotfull-refresh-w
                    //5: Closing price
                    //6: Settlement price


                    // Changed by Rainer on 20150915 -begin
                    case '6':
                        // Closing price

                        //AuthCommon.SynWriteLogger processFullRefresh = new AuthCommon.SynWriteLogger("processFullRefresh.log");

                        //processFullRefresh.setLogLevel(ZDLogger.LVL_DEBUG);
                        //processFullRefresh.log(3, $"{ordBookAll.codeBean.zdProduct},settPrice={ordBookAll.settPrice}");

                        ordBookAll.settPrice = (double)g.GetDecimal(Tags.MDEntryPx);
                        //增量数据没有推送结算价，使用快照里的结算价。
                        //如果服务器重启，避免在数据清之后又给今结算赋相同的值
                        //如果今结算为零，每天不清结算价
                        if (ordBookAll.new_preveSett != ordBookAll.settPrice)
                        {
                            ordBookAll.new_TodaySett = ordBookAll.settPrice;
                            if (ordBookAll.new_preveSett == 0)
                            {
                                ordBookAll.new_preveSett = ordBookAll.settPrice;
                            }
                            TT.Common.NLogUtility.Info(string.Format("processFullRefresh :zdCode={0},ordBookAll.new_TodaySett={1}", ordBookAll.codeBean.zdCode, ordBookAll.new_TodaySett));
                        }
                        //if (ordBookAll.new_preveSett != 0)
                        //    ordBookAll.new_preveSett = ordBookAll.settPrice;
                        //processFullRefresh.log(3, $"{ordBookAll.codeBean.zdProduct},settPrice={ordBookAll.settPrice},new_preveSett={ordBookAll.new_preveSett }");
                        //processFullRefresh.Dispose();
                        break;
                    // Changed by Rainer on 20150915 -end

                    case '7':
                        //Trading session high price
                        ordBookAll.tsHighPrice = (double)g.GetDecimal(Tags.MDEntryPx);
                        break;
                    case '8':
                        //Trading session low price
                        ordBookAll.tsLowPrice = (double)g.GetDecimal(Tags.MDEntryPx);
                        break;
                    case 'A':
                        // Imbalance
                        ordBookAll.openInterest = (uint)g.GetInt(Tags.MDEntrySize);
                        break;
                    case 'B':
                        // Trade volume
                        ordBookAll.tradeVolume = g.GetInt(Tags.MDEntrySize);
                        break;
                    default:
                        //result = "other";
                        break;

                }
                #endregion

            }
            var symbol = fullReresh.GetString(Tags.Symbol);
            PrxFactor(ordBookAll, symbol);

            var msgSeqNum = fullReresh.Header.GetField(Tags.MsgSeqNum);
            Sub2Pumper.pumpSnapshotQuickFast(ordBookAll, $"{LogMessageType.SnapShot} MsgSeqNum:{msgSeqNum}");//快照发送


            //var msgSeqNum = fullReresh.Header.GetField(Tags.MsgSeqNum);
            //Sub2Pumper.pumpSnapshotQuickFast(ordBookAll, $"processFullRefresh MsgSeqNum:{msgSeqNum}");//快照发送
        }

        #endregion

        #region  增量
        //bool _logger = false;
        //PriceDetail _comparer = new PriceDetail();

        private bool _bidChnage = false;
        private bool _askChnage = false;

        /// <summary>
        /// 增量 35=X
        /// </summary>
        /// <param name="incrementalRefresh"></param>
        public void processIncrementalRefresh(MarketDataIncrementalRefresh incrementalRefresh)
        {
            ////注意组的索引从1开始
            //var securityType = incrementalRefresh.GetGroup(1, Tags.NoMDEntries).GetString(Tags.SecurityType);
            //if (securityType == "MLEG")
            //{

            //}

            //注意组的索引从1开始
            Group g = incrementalRefresh.GetGroup(1, Tags.NoMDEntries);
            string securityID = g.GetString(Tags.SecurityID);
            //OrderBookALL copyOrderBookALL;
            OrderBookALL orderBookALL;
            if (!orderBookDict.TryGetValue(securityID, out orderBookALL))
            {
                return;
            }
            //OrderBookALL orderBookALL =(OrderBookALL) orderBookALL1.Clone();
            _bidChnage = false;
            _askChnage = false;

            if (g.IsSetField(Tags.TotalVolumeTraded))
            {
                orderBookALL.tradeVolume = g.GetInt(Tags.TotalVolumeTraded);
            }

            string symbol = string.Empty;
            if (incrementalRefresh.IsSetField(Tags.Symbol))
            {
                symbol = incrementalRefresh.GetString(Tags.Symbol);
            }
            else if (g.IsSetField(Tags.Symbol))
            {
                symbol = g.GetString(Tags.Symbol);
            }
            processMDEntry(orderBookALL, g, symbol, incrementalRefresh);


            //Tag 268
            int repeatCnt = incrementalRefresh.NoMDEntries.getValue();
            if (repeatCnt > 1)
            {
                for (int i = 2; i <= repeatCnt; i++)
                {
                    g = incrementalRefresh.GetGroup(i, Tags.NoMDEntries);
                    processMDEntry(orderBookALL, g, symbol, incrementalRefresh);
                }
            }



            if (_bidChnage || _askChnage)
            {
                doInsertWork(orderBookALL, symbol);
            }





            ////买价从大到小排序，利用Framework默认的排序算法
            //orderBookALL.GBXOrderBook.bidList = orderBookALL.GBXOrderBook.bidList.OrderByDescending(p => p.Price).ToArray();
            ////卖价：从小到大排序，0排在最后，0不是最大，重写排序算法
            ////利用PriceDetail实现接口IComparable<PriceDetail>进行排序
            //orderBookALL.GBXOrderBook.askList = orderBookALL.GBXOrderBook.askList.OrderBy(p => p).ToArray();


            var msgSeqNum = incrementalRefresh.Header.GetField(Tags.MsgSeqNum);
            Sub2Pumper.pumpSnapshotQuickFast(orderBookALL, $"{LogMessageType.Incremental} MsgSeqNum:{msgSeqNum}");//增量发送

            //价格重的数据
            //var groupBy = orderBookALL.GBXOrderBook.askList.GroupBy(p => p.Price).Select(group => new { Price = group.Key, Count = group.Count() }).ToList();
            //var re = groupBy.Where(p => p.Price > 0 && p.Count > 1).ToList();
            //if(re.Count>0)
            //{
            //    var sss = JsonConvert.SerializeObject(orderBookALL.GBXOrderBook.askList);

            //    var sss1 = JsonConvert.SerializeObject(orderBookALL.GBXOrderBook.askList);
            //    TT.Common.Log.Info<OrderBookMgr>($"{symbol} {re.First().Price} {re.First().Count}");
            //    TT.Common.Log.Info<OrderBookMgr>(sss);
            //    TT.Common.Log.Info<OrderBookMgr>(sss1);
            //}


            orderBookDict[securityID] = orderBookALL;
            //Common.Log(ordBookAll.codeBean.zdProduct, ordBookAll.codeBean.zdCode, $"IncrementalRefresh:{sendStr}");
        }


        //// Mask defination
        ////  IA   IB   XA   XB
        //// [][] [][] [][] [][] 
        //public int xBidChange = 2;
        //public int xBidAfterChange = 1;
        //public int xAskChange = 8;
        //public int xAskAfterChange = 4;
        //public int iBidChange = 32;
        //public int iBidAfterChange = 16;
        //public int iAskChange = 128;
        //public int iAskAfterChange = 64;
        //public int changeMask = 0;



        private void doInsertWork(OrderBookALL ordBookAll, String symbol)
        {
            //if ((changeMask & xBidAfterChange) > 0)
            if (_bidChnage)
            {
                PriceDetail[] beforePriceDetail = ordBookAll.GBXOrderBook.bidList;
                PriceDetail[] afterPriceDetail = ladderBookAll.GBXOrderBook.bidList;
                doInsert(ref beforePriceDetail, ref afterPriceDetail, true, symbol);
                try
                {
                    //ordBookAll.GBXOrderBook.bidList = afterPriceDetail;
                    //拷贝前先清，防止拷贝前afterPriceDetail只有4个而 ordBookAll.GBXOrderBook.bidList有5个
                    //的就造成 ordBookAll.GBXOrderBook.bidList多个那个也拷贝进去的bug。
                    ordBookAll.GBXOrderBook.clearBidList();
                    for (int i = 0; i < afterPriceDetail.Length; i++)
                    {
                        PriceDetail pd = afterPriceDetail[i].Copy();
                        ordBookAll.GBXOrderBook.bidList[i] = pd;
                    }
                }
                catch (Exception ex)
                {
                    TT.Common.NLogUtility.Error($"afterPriceDetail.Length:{afterPriceDetail.Length}, ordBookAll.GBXOrderBook.bidList.Length{ordBookAll.GBXOrderBook.bidList.Length}");
                    TT.Common.NLogUtility.Error(ex.ToString());
                }
                //ladderBookAll.GBXOrderBook.bidList = beforePriceDetail;
                ladderBookAll.GBXOrderBook.clearBidList();
            }

            //if ((changeMask & xAskAfterChange) > 0)
            if (_askChnage)
            {
                PriceDetail[] beforePriceDetail = ordBookAll.GBXOrderBook.askList;
                PriceDetail[] afterPriceDetail = ladderBookAll.GBXOrderBook.askList;

                doInsert(ref beforePriceDetail, ref afterPriceDetail, false, symbol);

                try
                {
                    // 拷贝前先清，防止拷贝前
                    ordBookAll.GBXOrderBook.clearAskList();
                    //ordBookAll.GBXOrderBook.askList = afterPriceDetail;
                    for (int i = 0; i < afterPriceDetail.Length; i++)
                    {
                        PriceDetail pd = afterPriceDetail[i].Copy();
                        ordBookAll.GBXOrderBook.askList[i] = pd;
                    }
                }
                catch (Exception ex)
                {
                    TT.Common.NLogUtility.Error($"afterPriceDetail.Length:{afterPriceDetail.Length}, ordBookAll.GBXOrderBook.askList.Length{ordBookAll.GBXOrderBook.askList.Length}");
                    TT.Common.NLogUtility.Error(ex.ToString());
                }


                //ladderBookAll.GBXOrderBook.askList = beforePriceDetail;
                ladderBookAll.GBXOrderBook.clearAskList();
            }


            // removed on 2017082 -begin
            //if ((changeMask & iBidAfterChange) > 0)
            //{
            //    PriceDetail[] beforePriceDetail = ordBookAll.GBIOrderBook.bidList;
            //    PriceDetail[] afterPriceDetail = ladderBookAll.GBIOrderBook.bidList;

            //    doInsert(beforePriceDetail, afterPriceDetail, true, iBidAfterInstanceIdx);

            //    ordBookAll.GBIOrderBook.bidList = afterPriceDetail;
            //    ladderBookAll.GBIOrderBook.bidList = beforePriceDetail;
            //    ladderBookAll.GBIOrderBook.clearBidList();
            //}
            //if ((changeMask & iAskAfterChange) > 0)
            //{
            //    PriceDetail[] beforePriceDetail = ordBookAll.GBIOrderBook.askList;
            //    PriceDetail[] afterPriceDetail = ladderBookAll.GBIOrderBook.askList;

            //    doInsert(beforePriceDetail, afterPriceDetail, false, iAskAfterInstanceIdx);

            //    ordBookAll.GBIOrderBook.askList = afterPriceDetail;
            //    ladderBookAll.GBIOrderBook.askList = beforePriceDetail;
            //    ladderBookAll.GBIOrderBook.clearAskList();
            //}
            // removed on 2017082 -end
        }

        public void doInsert(ref PriceDetail[] beforePriceDetail, ref PriceDetail[] afterPriceDetail, bool isBid, String symbol)
        {
            //int j = afterInstanceIdx; // after instance index
            //for (int i = 0; i < beforePriceDetail.Length; i++)
            //{
            //    if (j == beforePriceDetail.Length) break;

            //    PriceDetail beforeItem = beforePriceDetail[i];
            //    if (beforeItem.ttFlag != MDUpdateAction.DELETE)
            //    {
            //        beforePriceDetail[i] = afterPriceDetail[j];
            //        afterPriceDetail[j++] = beforeItem;
            //    }
            //}
            var beforeList = beforePriceDetail.ToList();
            List<PriceDetail> beforeMerge = null;
            try
            {
                beforeMerge = beforePriceDetail.ToList().GroupBy(p => p.Price).Select(group => new PriceDetail { Price = group.Key, Quantity = group.Sum(p => p.Quantity) }).ToList();

            }
            catch (Exception ex)
            {

                TT.Common.NLogUtility.Error(ex.ToString());
                TT.Common.NLogUtility.Error($"beforePriceDetail:{JsonConvert.SerializeObject(beforePriceDetail)}");
                return;
            }
            //List<PriceDetail> beforeMerge = beforePriceDetail.ToList().GroupBy(p => p.Price).Select(group => new PriceDetail { Price = group.Key, Quantity = group.Sum(p => p.Quantity) }).ToList();
            beforeMerge.ForEach(p =>
            {
                p.ttFlag = beforeList.First(m => m.Price == p.Price).ttFlag;
            });

            List<PriceDetail> afterList = afterPriceDetail.ToList();
            var afterMerge = afterList.GroupBy(p => p.Price).Select(group => new PriceDetail { Price = group.Key, Quantity = group.Sum(p => p.Quantity) }).ToList();
            afterMerge.ForEach(p =>
            {
                p.ttFlag = afterList.First(m => m.Price == p.Price).ttFlag;
            });
            List<PriceDetail> priceDetails = new List<PriceDetail>();
            //for (int i = 0; i < beforeMerge.Count; i++)
            //{
            //    if (beforeMerge[i].ttFlag != MDUpdateAction.DELETE)
            //    {
            //        priceDetails.Add(beforeMerge[i]);
            //    }

            //}

            beforeMerge.ForEach(p =>
            {
                if (p.ttFlag != MDUpdateAction.DELETE)
                {
                    //避免产生有0的行情数据
                    if (p.Price != 0)
                    {
                        priceDetails.Add(p);
                    }

                }
            });

            afterMerge.ForEach(p =>
            {
                if (p.ttFlag == MDUpdateAction.NEW)
                {
                    //避免产生有0的行情数据
                    if (p.Price != 0)
                    {
                        priceDetails.Add(p);
                    }
                }

            });
            priceDetails = priceDetails.OrderBy(p => p.Price).ToList();
            if (priceDetails.Count > 5)
            {

                TT.Common.NLogUtility.Error($"symbol:{symbol}");
                TT.Common.NLogUtility.Error($"beforePriceDetail:{JsonConvert.SerializeObject(beforePriceDetail)}");
                TT.Common.NLogUtility.Error($"afterPriceDetail:{JsonConvert.SerializeObject(afterPriceDetail)}");
                TT.Common.NLogUtility.Error($"beforeMerge:{JsonConvert.SerializeObject(beforeMerge)}");
                TT.Common.NLogUtility.Error($"afterMerge:{JsonConvert.SerializeObject(afterMerge)}");
                TT.Common.NLogUtility.Error($"priceDetails:{JsonConvert.SerializeObject(priceDetails)}");



                var beforePriceDetailZero = beforePriceDetail.Where(p => p.Price == 0).Count();
                if (beforePriceDetailZero > 0)
                {
                    priceDetails.Reverse();
                    //避免报错：超过5个索引异常。
                    afterPriceDetail = priceDetails.Take(5).OrderBy(p => p.Price).ToArray();
                    TT.Common.NLogUtility.Error($"有价格为零的，逆序取前五个!");
                }
                else
                {
                    var groupCount = priceDetails.GroupBy(p => p.Price).Select(g => new { Price = g.Key, Count = g.Count() }).Where(p => p.Count > 1).ToList();
                    if (groupCount.Count > 0)
                    {
                        TT.Common.NLogUtility.Error($"priceDetails可以合并!");
                        var priceDetailsMerge = priceDetails.GroupBy(p => p.Price).Select(group => new PriceDetail { Price = group.Key, Quantity = group.Sum(p => p.Quantity) }).ToList();

                        groupCount.ForEach(p =>
                        {
                            //此处ttFlag肯定为:'0'
                            priceDetailsMerge.First(m => m.Price == p.Price).ttFlag = afterMerge.First(m => m.Price == p.Price).ttFlag;
                        });
                        if (priceDetailsMerge.Count > 5)
                        {
                            TT.Common.NLogUtility.Error($"priceDetails 合并后还是大于5，采取舍弃！");
                            TT.Common.NLogUtility.Error($"isBid：{isBid}。true 逆序，舍弃前：{JsonConvert.SerializeObject(afterPriceDetail)}");
                            //买逆序
                            if (isBid)
                            {
                                afterPriceDetail = priceDetails.OrderByDescending(p => p.Price).Take(5).ToArray();
                            }
                            else  //卖正序
                            {
                                afterPriceDetail = priceDetails.Take(5).ToArray();
                            }
                            TT.Common.NLogUtility.Error($"舍弃后：{JsonConvert.SerializeObject(afterPriceDetail)}");
                        }
                    }
                    else
                    {
                        TT.Common.NLogUtility.Error($"priceDetails 无法合并！，采取舍弃");
                        //买逆序
                        if (isBid)
                        {
                            afterPriceDetail = priceDetails.OrderByDescending(p => p.Price).Take(5).ToArray();
                        }
                        else  //卖正序
                        {
                            afterPriceDetail = priceDetails.Take(5).ToArray();
                        }
                        TT.Common.NLogUtility.Error($"isBid：{isBid}。true 逆序，舍弃后：{JsonConvert.SerializeObject(afterPriceDetail)}");

                    }

                }


            }
            else
            {
                afterPriceDetail = priceDetails.ToArray();
            }

            if (isBid)
            {
                afterPriceDetail = afterPriceDetail.OrderByDescending(p => p.Price).ToArray();
                // Array.Reverse(afterPriceDetail);
            }
        }

        #region   注释
        //public void doInsertBid(PriceDetail[] beforePriceDetail, PriceDetail[] afterPriceDetail)
        //{
        //    //*
        //    int j = 0; // after instance index
        //    if (afterPriceDetail[0].ttFlag == MDUpdateAction.NEW)
        //    {
        //        PriceDetail tempPD = null;
        //        newItemInAfterInstance.Price = afterPriceDetail[0].Price;
        //        newItemInAfterInstance.Quantity = afterPriceDetail[0].Quantity;
        //        newItemInAfterInstance.OrderCount = afterPriceDetail[0].OrderCount;
        //        for (int i = 0; i < beforePriceDetail.Length; i++)
        //        {
        //            PriceDetail beforeItem = beforePriceDetail[i];
        //            if (beforeItem.ttFlag != MDUpdateAction.DELETE)
        //            {
        //                if (beforeItem.Price > newItemInAfterInstance.Price)
        //                {
        //                    beforePriceDetail[i] = afterPriceDetail[j];
        //                    afterPriceDetail[j++] = beforeItem;
        //                }
        //                else if (beforeItem.Price !=0 && beforeItem.Price < newItemInAfterInstance.Price)
        //                {
        //                    tempPD = afterPriceDetail[j];
        //                    afterPriceDetail[j++] = newItemInAfterInstance;
        //                    newItemInAfterInstance = beforeItem;
        //                    beforePriceDetail[i] = tempPD;
        //                }
        //            }
        //        }

        //        tempPD = afterPriceDetail[j];
        //        afterPriceDetail[j] = newItemInAfterInstance;
        //        newItemInAfterInstance = tempPD;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < beforePriceDetail.Length; i++)
        //        {
        //            PriceDetail beforeItem = beforePriceDetail[i];
        //            if (beforeItem.ttFlag != MDUpdateAction.DELETE)
        //            {
        //                PriceDetail tempPD = afterPriceDetail[j];
        //                afterPriceDetail[j++] = beforeItem;
        //                beforePriceDetail[i] = tempPD;
        //            }
        //        }
        //    }


        //}

        //public void doInsertAsk(PriceDetail[] beforePriceDetail, PriceDetail[] afterPriceDetail)
        //{

        //    int j = 0; // after instance index
        //    if (afterPriceDetail[0].ttFlag == MDUpdateAction.NEW)
        //    {
        //        PriceDetail tempPD = null;
        //        newItemInAfterInstance.Price = afterPriceDetail[0].Price;
        //        newItemInAfterInstance.Quantity = afterPriceDetail[0].Quantity;
        //        newItemInAfterInstance.OrderCount = afterPriceDetail[0].OrderCount;

        //        for (int i = 0; i < beforePriceDetail.Length; i++)
        //        {
        //            PriceDetail beforeItem = beforePriceDetail[i];
        //            if (beforeItem.ttFlag != MDUpdateAction.DELETE)
        //            {
        //                if (beforeItem.Price != 0 && beforeItem.Price < newItemInAfterInstance.Price)
        //                {
        //                    beforePriceDetail[i] = afterPriceDetail[j];
        //                    afterPriceDetail[j++] = beforeItem;
        //                }
        //                else if (beforeItem.Price > newItemInAfterInstance.Price)
        //                {
        //                    tempPD = afterPriceDetail[j];
        //                    afterPriceDetail[j++] = newItemInAfterInstance;
        //                    newItemInAfterInstance = beforeItem;
        //                    beforePriceDetail[i] = tempPD;
        //                }
        //            }
        //        }

        //        tempPD = afterPriceDetail[j];
        //        afterPriceDetail[j] = newItemInAfterInstance;
        //        newItemInAfterInstance = tempPD;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < beforePriceDetail.Length; i++)
        //        {
        //            PriceDetail beforeItem = beforePriceDetail[i];
        //            if (beforeItem.ttFlag != MDUpdateAction.DELETE)
        //            {
        //                PriceDetail tempPD = afterPriceDetail[j];
        //                afterPriceDetail[j++] = beforeItem;
        //                beforePriceDetail[i] = tempPD;
        //            }
        //        }
        //    }
        //}
        #endregion



        private void processMDEntry(OrderBookALL ordBookAll, Group g, string symbol, MarketDataIncrementalRefresh incrementalRefresh)
        {
            //tag 269
            char entryType = g.GetChar(Tags.MDEntryType);
            //tag 279
            char updateAction = g.GetChar(Tags.MDUpdateAction);

            //269=B 成交量
            if (entryType == 'B')
            {
                ordBookAll.tradeVolume = g.GetInt(Tags.MDEntrySize);
            }


            // added on 2017082 -begin   Implied bid
            if (entryType == 'Y')// Implied bid
            {
                PriceDetail pdx = ordBookAll.GBIOrderBook.bidList[0];
                if (updateAction == MDUpdateAction.NEW || updateAction == MDUpdateAction.CHANGE)
                {
                    pdx.Price = (double)g.GetDecimal(Tags.MDEntryPx);
                    pdx.Quantity = g.GetInt(Tags.MDEntrySize);
                }
                else //if (updateAction == MDUpdateAction.DELETE)
                {
                    if (pdx.Price == (double)g.GetDecimal(Tags.MDEntryPx))
                    {
                        pdx.Price = 0;
                        pdx.Quantity = 0;
                    }
                }
                return;
            }
            else if (entryType == 'Z')//Implied ask
            {
                PriceDetail pdx = ordBookAll.GBIOrderBook.askList[0];

                if (updateAction == MDUpdateAction.NEW || updateAction == MDUpdateAction.CHANGE)
                {
                    pdx.Price = (double)g.GetDecimal(Tags.MDEntryPx);
                    pdx.Quantity = g.GetInt(Tags.MDEntrySize);
                }
                else //if (updateAction == MDUpdateAction.DELETE)
                {
                    if (pdx.Price == (double)g.GetDecimal(Tags.MDEntryPx))
                    {
                        pdx.Price = 0;
                        pdx.Quantity = 0;
                    }
                }
                return;
            }
            // added on 2017082 -end

            OrderBookALL targetOrdBookAll = null;

            int mDEntryPositionNo = -1;
            if (g.IsSetField(Tags.MDEntryPositionNo))
                mDEntryPositionNo = g.GetInt(Tags.MDEntryPositionNo) - 1;


            if (updateAction == MDUpdateAction.NEW && entryType != MDEntryType.TRADE)
            {
                targetOrdBookAll = ladderBookAll;
            }
            else
            {
                targetOrdBookAll = ordBookAll;
            }

            PriceDetail pd = null;


            switch (entryType)
            {
                case '0':
                case '1':

                    if (entryType == '0')
                    {
                        pd = targetOrdBookAll.GBXOrderBook.bidList[mDEntryPositionNo];
                        if (g.IsSetField(Tags.MDUpdateAction))
                        {
                            _bidChnage = true;
                        }
                    }
                    else
                    {
                        pd = targetOrdBookAll.GBXOrderBook.askList[mDEntryPositionNo];
                        if (g.IsSetField(Tags.MDUpdateAction))
                        {
                            _askChnage = true;
                        }
                    }

                    pd.Price = (double)g.GetDecimal(Tags.MDEntryPx);
                    pd.Quantity = g.GetInt(Tags.MDEntrySize);
                    pd.ttFlag = updateAction;
                    break;

                case '2':
                    //result = "trade";
                    //{
                    PriceDetail pdTrade = ordBookAll.lastTrade;
                    var tradePrice = (double)g.GetDecimal(Tags.MDEntryPx);
                    if (CodeTransfer_TT.PrdPrxFactorDict.Keys.Contains(symbol))
                    {
                        tradePrice *= (double)CodeTransfer_TT.PrdPrxFactorDict[symbol];
                    }
                    pdTrade.Price = tradePrice;
                    //pdTrade.Price = double.Parse(CodeTransfer_TT.toClientPrx( g.GetDecimal(Tags.MDEntryPx).ToString(), ordBookAll.codeBean.upperProduct)); 
                    pdTrade.Quantity = g.GetInt(Tags.MDEntrySize);
                    CalculateTradeVolume(ordBookAll, pdTrade);
                    Sub2Pumper.pumpTrade(ordBookAll, incrementalRefresh);
                    //}
                    break;

                case '4':
                    // Opening price
                    if (updateAction == MDUpdateAction.DELETE)
                        ordBookAll.openingPrice = 0;
                    else
                    {
                        ordBookAll.openingPrice = (double)g.GetDecimal(Tags.MDEntryPx);
                    }

                    break;


                // Changed by Rainer on 20150915 -begin
                case '5': // Closing price
                case '6': // Settlement price
                          // Changed by Rainer on 20150915 -end

                    if (updateAction != MDUpdateAction.DELETE)
                    {
                        //if (GlobalData.specialContractSet.Contains(ordBookAll.codeBean.zdCode))
                        //{
                        //    if (ordBookAll.holdSettPrice == 0)
                        //    {
                        //        ordBookAll.holdTradeVolume = ordBookAll.tradeVolume;
                        //    }

                        //    double temp = (double)g.GetDecimal(Tags.MDEntryPx);
                        //    if (temp != 0)
                        //        ordBookAll.holdSettPrice = temp;

                        //}
                        //else
                        //{
                        //result = "SettlPrx";
                        if (updateAction == MDUpdateAction.DELETE)
                            ordBookAll.settPrice = 0;
                        else
                            ordBookAll.settPrice = (double)g.GetDecimal(Tags.MDEntryPx);
                        //}
                        double price = (double)g.GetDecimal(Tags.MDEntryPx);
                        if (ordBookAll.new_preveSett != price)
                        {
                            ordBookAll.new_TodaySett = price;
                        }
                        TT.Common.NLogUtility.Info(string.Format("processIncrementalRefresh :zdCode={0},ordBookAll.new_TodaySett={1}", ordBookAll.codeBean.zdCode, ordBookAll.new_TodaySett));
                        //if (ordBookAll.new_preveSett == 0)
                        //    ordBookAll.new_preveSett = ordBookAll.new_TodaySett;
                    }

                    break;

                case '7':
                    //Trading session high price
                    if (updateAction == MDUpdateAction.DELETE)
                        ordBookAll.tsHighPrice = 0;
                    else
                        ordBookAll.tsHighPrice = (double)g.GetDecimal(Tags.MDEntryPx);
                    break;
                case '8':
                    //Trading session low price
                    if (updateAction == MDUpdateAction.DELETE)
                        ordBookAll.tsLowPrice = 0;
                    else
                        ordBookAll.tsLowPrice = (double)g.GetDecimal(Tags.MDEntryPx);
                    break;
                case 'A':
                    // Imbalance
                    if (updateAction == MDUpdateAction.DELETE)
                        ordBookAll.openInterest = 0;
                    else
                        ordBookAll.openInterest = (uint)g.GetInt(Tags.MDEntrySize);
                    break;

                default:
                    //result = "other";
                    break;

            }

            PrxFactor(ordBookAll, symbol);
        }
        /// <summary>
        /// 处理倍率
        /// </summary>
        /// <param name="orderBookALL"></param>
        private void PrxFactor(OrderBookALL orderBookALL, string symbol)
        {
            if (CodeTransfer_TT.PrdPrxFactorDict.Keys.Contains(symbol))
            {
                orderBookALL.openingPrice = orderBookALL.openingPrice * (double)CodeTransfer_TT.PrdPrxFactorDict[symbol];
                orderBookALL.settPrice = orderBookALL.settPrice * (double)CodeTransfer_TT.PrdPrxFactorDict[symbol];
                orderBookALL.new_TodaySett = orderBookALL.new_TodaySett * (double)CodeTransfer_TT.PrdPrxFactorDict[symbol];
                orderBookALL.new_preveSett = orderBookALL.new_preveSett * (double)CodeTransfer_TT.PrdPrxFactorDict[symbol];
                orderBookALL.tsHighPrice = orderBookALL.tsHighPrice * (double)CodeTransfer_TT.PrdPrxFactorDict[symbol];
                orderBookALL.tsLowPrice = orderBookALL.tsLowPrice * (double)CodeTransfer_TT.PrdPrxFactorDict[symbol];
            }
        }

        #endregion

        private void CalculateTradeVolume(OrderBookALL orderBookALL, PriceDetail priceDetail)
        {
            if (orderBookALL.codeBean.contractType == "FUT")
            {
                var supportedTradeVolumeProduct = Configurations.SupportedTradeVolumeProductsList.Find(p => p.ZDExchange == orderBookALL.codeBean.zdExchg &&
                                                   p.ZDProduct == orderBookALL.codeBean.zdProduct);
                if (!string.IsNullOrEmpty(supportedTradeVolumeProduct.ZDProduct))
                {
                    var tradingVolume = (decimal)(((double)priceDetail.Quantity) * priceDetail.Price) * supportedTradeVolumeProduct.Factor;
                    if (!this.TradingVolumeDic.Keys.Contains(orderBookALL.codeBean.zdCode))
                    {
                        this.TradingVolumeDic.Add(orderBookALL.codeBean.zdCode, tradingVolume);
                    }
                    else
                    {
                        this.TradingVolumeDic[orderBookALL.codeBean.zdCode] += tradingVolume;
                    }
                }

            }
        }
    }
}
