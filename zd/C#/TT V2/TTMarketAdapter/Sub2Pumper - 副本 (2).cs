﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using AuthCommon;
using CommonClassLib.ContractTransfer;
using System.Diagnostics;
using System.Configuration;
using QuickFix.FIX42;
using TTMarketAdapter.Utilities;
using TT.Common;
using QuickFix.Fields;

namespace TTMarketAdapter
{

    public class MyDataPool
    {
        public static ObjectPool<byte[]> Byte1024Pool = new ObjectPool<byte[]>(() => new byte[1024]);
        public static ObjectPool<StringBuilder> StringBuilderPool = new ObjectPool<StringBuilder>(() => new StringBuilder());

    }


    /// <summary>
    /// 发送数据
    /// </summary>
    class Sub2Pumper
    {
        private static EndPoint[] destEndPointArr = null;
        private static Socket udpSendSocket = null;

        private static string dataDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
        public const char ZD_RECENT_DATA = (char)2;
        public const char ZD_LATEST_DATA = 'Z';
        public const char ZD_TRADE = '2';
        public const char ZD_BOOK_DATA = 'X';
        public const char ZD_LAST_TRADE = 'Y';

        public const char ZD_BID = 'B';
        public const char ZD_OFFER = 'O';


        public const string SOURCE_ID = "15";

        private const string FRACT_FORMAT1 = "{0:0.#########}";
        private const string FRACT_FORMAT2 = "{0:#.#####}";
        //  private static ZDLogger debugLog = new SynWriteLogger("Debug.log");

        public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        static Sub2Pumper()
        {
            udpSendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            int port = Convert.ToInt32(Configurations.MulticastPort);

            string[] udpTargetIP = Configurations.MulticastIP.Split(';');
            destEndPointArr = new EndPoint[udpTargetIP.Length];
            for (int i = 0; i < destEndPointArr.Length; i++)
                destEndPointArr[i] = new IPEndPoint(IPAddress.Parse(udpTargetIP[i]), port);

            //  debugLog.setLogLevel(ZDLogger.LVL_DEBUG);
        }


        //private static ObjectPool<LastTradeBean> tradeDataObjPool = MyDataPool.tradeDataObjPool;

        //public static void pumpTrade(LastTradeBean lastTradeBean)
        //{
        //    if (GlobalData.marketSwitchOff) return;

        //    OrderBookALL orderBookAll = lastTradeBean.orderBookAll;

        //    CodeTransferInfo cti = CodeTransfer_CME.getZDCodeInfoByUpperCode(orderBookAll.securityId);
        //    if (cti == null)
        //        return;

        //    StringBuilder sb = MyDataPool.StringBuilderPool.GetObject();
        //    sb.Append(cti.exchangeCode).Append('@').Append(cti.zdCode).Append('@');           //交易所代码和合约代码

        //    if (orderBookAll.franctionalBase != 0)
        //    {
        //        //sb.Append('@', 4)
        //        sb.Append(formatFracPrx(orderBookAll, lastTradeBean.tradePrice)).Append('@') //最新价
        //        .Append(lastTradeBean.tradeQuantity).Append('@')                              //现手
        //        .Append(0).Append('@');  //成交量
        //    }
        //    else
        //    {
        //        //sb.Append('@', 4)
        //        sb.Append((lastTradeBean.tradePrice == 0F) ? String.Empty : String.Format(orderBookAll.prxFormat, lastTradeBean.tradePrice)).Append('@') //最新价
        //        .Append(lastTradeBean.tradeQuantity).Append('@')                              //现手
        //        .Append(0).Append('@');  //成交量
        //    }

        //    tradeDataObjPool.PutObject(lastTradeBean);

        //    sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")).Append('@')
        //        .Append(lastTradeBean.packetTimestamp).Append('@')
        //        .Append(ZD_LAST_TRADE); //Last change flag

        //    string strMarket = sb.ToString();
        //    //Console.WriteLine(strMarket);
        //    sb.Clear();
        //    MyDataPool.StringBuilderPool.PutObject(sb);
        //    sendUDPPacket(strMarket);
        //}


        private static int logCnt1 = 0;
        private static int logCnt2 = 0;
        private static int logCnt3 = 0;

        /// <summary>
        /// 发送交易量数据
        /// </summary>
        /// <param name="orderBookAll"></param>
        public static void pumpTrade(OrderBookALL orderBookAll, MarketDataIncrementalRefresh incrementalRefresh)
        {
            CodeBean cb = orderBookAll.codeBean;

            StringBuilder sb = MyDataPool.StringBuilderPool.GetObject();
            string prxFormat = orderBookAll.prxFormat;

            sb.Append(cb.zdExchg).Append('@').Append(cb.zdCode).Append('@')           //交易所代码和合约代码
                 .Append((orderBookAll.lastTrade.Price == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.lastTrade.Price)).Append('@') //最新价
                    .Append(orderBookAll.lastTrade.Quantity).Append('@')                              //现手量
                    .Append('@')    //成交量
                    .Append(DateTime.Now.ToString(DATETIME_FORMAT)).Append('@')
                    .Append(ZD_LAST_TRADE);


            string strMarket = sb.ToString();
            //Console.WriteLine(strMarket);
            sb.Clear();
            MyDataPool.StringBuilderPool.PutObject(sb);
            sendUDPPacket(strMarket);

            //TT.Common.SendMessageLog.Log(bool.Parse(Configurations.LogSendMsg?.ToLower()),orderBookAll.codeBean.zdProduct, orderBookAll.codeBean.zdCode, $"{Enum.GetName(typeof(MessageType), MessageType.Trade)} {strMarket}");
            var msgSeqNum = incrementalRefresh.Header.GetField(Tags.MsgSeqNum);
            if (Configurations.LogSendMsgSecurityTypes.Contains(orderBookAll.codeBean.contractType))
            {
                LogAsync.Log(orderBookAll.codeBean.zdProduct, orderBookAll.codeBean.zdCode, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")} {LogMessageType.Trade} MsgSeqNum:{msgSeqNum} - {strMarket}");
            }
        }

        /// <summary>
        /// 发送快照、增量数据到二级行情服务器
        /// </summary>
        /// <param name="orderBookAll"></param>
        public static void pumpSnapshotQuickFast(OrderBookALL orderBookAll,  string logHeader)
        {

            try
            {
                //CodeTransferInfo cti = CodeTransfer_TT.getZDCodeInfoByUpperCode(orderBookAll.securityId);
                //if (cti == null)
                //    return;

                CodeBean cb = orderBookAll.codeBean;

                if (orderBookAll.franctionalBase != 0)
                {
                    pumpSnapshotFracPrxQuickFast(orderBookAll, logHeader);
                    return;
                }

                //买或卖价全为空不发
                if ((orderBookAll.GBXOrderBook.bidList[0].Price == 0 &&
                    orderBookAll.GBXOrderBook.bidList[1].Price == 0 &&
                    orderBookAll.GBXOrderBook.bidList[2].Price == 0 &&
                    orderBookAll.GBXOrderBook.bidList[3].Price == 0 &&
                    orderBookAll.GBXOrderBook.bidList[4].Price == 0) ||
                  (orderBookAll.GBXOrderBook.askList[0].Price == 0 &&
                  orderBookAll.GBXOrderBook.askList[1].Price == 0 &&
                  orderBookAll.GBXOrderBook.askList[2].Price == 0 &&
                  orderBookAll.GBXOrderBook.askList[3].Price == 0 &&
                  orderBookAll.GBXOrderBook.askList[4].Price == 0))
                {
                    return;
                }

                StringBuilder sb = null;

                #region lock region
                //lock (orderBookAll)
                //{
                OrderBook gbxBook = orderBookAll.GBXOrderBook;
                OrderBook gbiBook = orderBookAll.GBIOrderBook;

                PriceDetail[] bidList = gbxBook.bidList;
                PriceDetail[] askList = gbxBook.askList;
                ///*
                //if (bidList[0].Price == bidList[1].Price ||
                //    bidList[1].Price == bidList[2].Price ||
                //    bidList[2].Price == bidList[3].Price ||
                //    bidList[3].Price == bidList[4].Price) return;

                //if (askList[0].Price == askList[1].Price ||
                //    askList[1].Price == askList[2].Price ||
                //    askList[2].Price == askList[3].Price ||
                //    askList[3].Price == askList[4].Price) return;
                //*/
                //if (bidList[0].Price >= askList[0].Price && askList[0].Price != 0)
                //{
                //    return;
                //}

                sb = MyDataPool.StringBuilderPool.GetObject();
                string prxFormat = orderBookAll.prxFormat;

                /*
                 * 
                 * 
                 * @正想索引下标从0开始。
                 * 
                 * 
                 */
                sb.Append(cb.zdExchg).Append('@').Append(cb.zdCode).Append('@')           //交易所代码和合约代码    @2
                    .Append((bidList[0].Price == 0D) ? String.Empty : String.Format(prxFormat, bidList[0].Price)).Append('@')   //买1价  @3
                    .Append(bidList[0].Quantity).Append('@')                              //买1量   @4
                    .Append((askList[0].Price == 0D) ? String.Empty : String.Format(prxFormat, askList[0].Price)).Append('@') //卖1价  @5
                    .Append(askList[0].Quantity).Append('@');                              //卖1量  @6
                sb.Append((orderBookAll.lastTrade.Price == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.lastTrade.Price)).Append('@') //最新价   @7
                    .Append(orderBookAll.lastTrade.Quantity).Append('@');                              //现手  （最新价的成交量）   @8
                sb.Append((orderBookAll.tsHighPrice == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.tsHighPrice)).Append('@')  //最高价   @9
                    .Append((orderBookAll.tsLowPrice == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.tsLowPrice)).Append('@')   //最低价   @10
                    .Append((orderBookAll.openingPrice == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.openingPrice)).Append('@')//开盘价   @11
                    .Append((orderBookAll.settPrice == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.settPrice)).Append('@')//昨结算价   @12
                    .Append('@')                                                                          //收盘价  为空  @13
                    .Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append('@')                     //@14
                    .Append(orderBookAll.tradeVolume).Append('@')                                         //成交量       @15
                    .Append(orderBookAll.openInterest).Append('@');                                       //持仓量       @16

                if (GlobalData.optionSecurityIDSet.Contains(orderBookAll.securityId))
                {

                    sb.Append((bidList[1].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[1].Price)).Append('@')//买价2
                        .Append((bidList[2].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[2].Price)).Append('@')//买价3
                        .Append('@', 2)
                        .Append(bidList[1].Quantity).Append('@')                            //买量2
                        .Append(bidList[2].Quantity).Append('@')                            //买量3
                        .Append('@', 2);

                    sb.Append((askList[1].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[1].Price)).Append('@')//卖价2
                        .Append((askList[2].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[2].Price)).Append('@')//卖价3
                        .Append('@', 2)
                        .Append(askList[1].Quantity).Append('@')                          //卖量2
                        .Append(askList[2].Quantity).Append('@')                          //卖量3
                        .Append('@', 2);
                }
                else
                {
                    sb.Append((bidList[1].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[1].Price)).Append('@')//买价2        @17
                        .Append((bidList[2].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[2].Price)).Append('@')//买价3      @18
                        .Append((bidList[3].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[3].Price)).Append('@')//买价4      @19
                        .Append((bidList[4].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[4].Price)).Append('@')//买价5      @20
                        .Append(bidList[1].Quantity).Append('@')                            //买量2                                           @21
                        .Append(bidList[2].Quantity).Append('@')                            //买量3                                           @22
                        .Append(bidList[3].Quantity).Append('@')                            //买量4                                           @23
                        .Append(bidList[4].Quantity).Append('@');                           //买量5                                           @24

                    sb.Append((askList[1].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[1].Price)).Append('@')//卖价2        @25
                        .Append((askList[2].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[2].Price)).Append('@')//卖价3      @26
                        .Append((askList[3].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[3].Price)).Append('@')//卖价4      @27
                        .Append((askList[4].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[4].Price)).Append('@')//卖价5      @28
                        .Append(askList[1].Quantity).Append('@')                          //卖量2                                             @29
                        .Append(askList[2].Quantity).Append('@')                          //卖量3                                             @30
                        .Append(askList[3].Quantity).Append('@')                          //卖量4                                             @31
                        .Append(askList[4].Quantity).Append('@');                         //卖量5                                             @32

                }


                if (gbiBook != null)
                {
                    PriceDetail gbiOffer = gbiBook.askList[0];
                    PriceDetail gbiBid = gbiBook.bidList[0];
                    if (gbiBid.Price != 0) // && gbiBid.Price < askList[0].Price)
                    {
                        //sb.Append((gbiBid.Price == 0F) ? String.Empty : String.Format(prxFormat, gbiBid.Price)).Append('@')//隐含买价
                        sb.Append(String.Format(prxFormat, gbiBid.Price)).Append('@')//隐含买价
                                .Append((gbiBid.Quantity == 0F) ? String.Empty : gbiBid.Quantity.ToString()).Append('@'); //隐含买量
                    }
                    else
                        sb.Append('@', 2);

                    if (gbiOffer.Price != 0) // && bidList[0].Price < gbiOffer.Price)
                    {
                        //sb.Append((gbiOffer.Price == 0F) ? String.Empty : String.Format(prxFormat, gbiOffer.Price)).Append('@')//隐含卖价
                        sb.Append(String.Format(prxFormat, gbiOffer.Price)).Append('@')//隐含卖价
                                .Append((gbiOffer.Quantity == 0F) ? String.Empty : gbiOffer.Quantity.ToString()).Append('@');//隐含卖量
                    }
                    else
                        sb.Append('@', 2);

                    /*
                    if (gbiBid.Price >= gbiOffer.Price && gbiOffer.Price != 0)
                    {
                        if (logCnt1 < 300)
                        {
                            debugLog.log(ZDLogger.LVL_CRITCAL, cb.zdCode + ": gbiBid.Price >= gbiOffer.Price");
                            logCnt1++;
                        }
                    }
                    else if (gbiBid.Price >= askList[0].Price && askList[0].Price != 0)
                    {
                        if (logCnt2 < 300)
                        {
                            debugLog.log(ZDLogger.LVL_CRITCAL, cb.zdCode + ": gbiBid.Price >= askList[0].Price");
                            logCnt2++;
                        }

                    }
                    else if (bidList[0].Price >= askList[0].Price && askList[0].Price != 0)
                    {

                        if (logCnt3 < 300)
                        {
                            debugLog.log(ZDLogger.LVL_CRITCAL, cb.zdCode + ": bidList[0].Price >= askList[0].Price");
                            logCnt3++;
                        }
                    }
                    */

                }
                else
                    sb.Append('@', 4);

                //}
                #endregion

                //sb.Append(orderBookAll.sendTimestamp).Append('@');
                //char dataFlag = ' ';
                //if (orderBookAll.tradeVolume > orderBookAll.lastTradeVol)
                //    dataFlag = ZD_TRADE;
                //else
                char dataFlag = ZD_LATEST_DATA;

                sb.Append(dataFlag).Append('@');   // Z  @37

                sb.Append('@')  //交易所数据时间戳    //@38
                        .Append(SOURCE_ID).Append('@'); //40

                //总成交金额
                sb.Append((orderBookAll.TradingVolume == 0) ? String.Empty : String.Format(prxFormat, orderBookAll.TradingVolume)).Append('@');//总成交金额  @40,  @-3：倒数第三个

                sb.Append((orderBookAll.new_preveSett == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.new_preveSett)).Append('@')//昨结算价  @41,  @-2：倒数第二个
                        .Append((orderBookAll.new_TodaySett == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.new_TodaySett)).Append('@');//今结算价  @42  @-1：倒数第一个

                sb.Append(dataFlag);//Z 44

                orderBookAll.lastTradeVol = orderBookAll.tradeVolume;

                string strMarket = sb.ToString();
                //Console.WriteLine(strMarket);
                sb.Clear();
                MyDataPool.StringBuilderPool.PutObject(sb);
                sendUDPPacket(strMarket);

                //Common.Log(orderBookAll.codeBean.zdProduct, orderBookAll.codeBean.zdCode, $"{new StackTrace().GetFrame(1).GetMethod().Name} {strMarket}");
                //TT.Common.SendMessageLog.Log(bool.Parse(Configurations.LogSendMsg?.ToLower()), orderBookAll.codeBean.zdProduct, orderBookAll.codeBean.zdCode, $"{Enum.GetName(typeof(MessageType),messageType)} {strMarket}");
                if (Configurations.LogSendMsgSecurityTypes.Contains(orderBookAll.codeBean.contractType))
                {
                    LogAsync.Log(orderBookAll.codeBean.zdProduct, orderBookAll.codeBean.zdCode, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")} {logHeader} - {strMarket}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void pumpSnapshotFracPrxQuickFast(OrderBookALL orderBookAll, string logHeader)
        {
            StringBuilder sb = null;

            #region lock region
            //lock (orderBookAll)
            //{
            int fractionBase = orderBookAll.franctionalBase;
            OrderBook gbxBook = orderBookAll.GBXOrderBook;
            OrderBook gbiBook = orderBookAll.GBIOrderBook;

            PriceDetail[] bidList = gbxBook.bidList;
            PriceDetail[] askList = gbxBook.askList;

            //if (bidList[0].Price >= askList[0].Price && askList[0].Price != 0)
            //{
            //    return;
            //}

            sb = MyDataPool.StringBuilderPool.GetObject();

            CodeBean cb = orderBookAll.codeBean;

            sb.Append(cb.zdExchg).Append('@').Append(cb.zdCode).Append('@')           //交易所代码和合约代码
                .Append(formatFracPrx(orderBookAll, bidList[0].Price)).Append('@')   //买价
                .Append(bidList[0].Quantity).Append('@')                              //买量
                .Append(formatFracPrx(orderBookAll, askList[0].Price)).Append('@') //卖价
                .Append(askList[0].Quantity).Append('@')                              //卖量
                .Append(formatFracPrx(orderBookAll, orderBookAll.lastTrade.Price)).Append('@') //最新价
                .Append(orderBookAll.lastTrade.Quantity).Append('@')                              //现手
                .Append(formatFracPrx(orderBookAll, orderBookAll.tsHighPrice)).Append('@')  //最高价
                .Append(formatFracPrx(orderBookAll, orderBookAll.tsLowPrice)).Append('@')   //最低价
                .Append(formatFracPrx(orderBookAll, orderBookAll.openingPrice)).Append('@')//开盘价
                .Append(formatFracPrx(orderBookAll, orderBookAll.settPrice)).Append('@')//昨结算价
                .Append('@')                                                                          //收盘价
                .Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append('@')
                .Append(orderBookAll.tradeVolume).Append('@')                                         //成交量
                .Append(orderBookAll.openInterest).Append('@');                                       //持仓量

            if (GlobalData.optionSecurityIDSet.Contains(orderBookAll.securityId))
            {
                sb.Append(formatFracPrx(orderBookAll, bidList[1].Price)).Append('@')//买价2
                    .Append(formatFracPrx(orderBookAll, bidList[2].Price)).Append('@')//买价3
                    .Append('@', 2)
                    .Append(bidList[1].Quantity).Append('@')                            //买量2
                    .Append(bidList[2].Quantity).Append('@')                            //买量3
                    .Append('@', 2);

                sb.Append(formatFracPrx(orderBookAll, askList[1].Price)).Append('@')//卖价2
                    .Append(formatFracPrx(orderBookAll, askList[2].Price)).Append('@')//卖价3
                    .Append('@', 2)
                    .Append(askList[1].Quantity).Append('@')                          //卖量2
                    .Append(askList[2].Quantity).Append('@')                          //卖量3
                    .Append('@', 2);
            }
            else
            {
                sb.Append(formatFracPrx(orderBookAll, bidList[1].Price)).Append('@')//买价2
                    .Append(formatFracPrx(orderBookAll, bidList[2].Price)).Append('@')//买价3
                    .Append(formatFracPrx(orderBookAll, bidList[3].Price)).Append('@')//买价4
                    .Append(formatFracPrx(orderBookAll, bidList[4].Price)).Append('@')//买价5
                    .Append(bidList[1].Quantity).Append('@')                            //买量2
                    .Append(bidList[2].Quantity).Append('@')                            //买量3
                    .Append(bidList[3].Quantity).Append('@')                            //买量4
                    .Append(bidList[4].Quantity).Append('@');                           //买量5

                sb.Append(formatFracPrx(orderBookAll, askList[1].Price)).Append('@')//卖价2
                    .Append(formatFracPrx(orderBookAll, askList[2].Price)).Append('@')//卖价3
                    .Append(formatFracPrx(orderBookAll, askList[3].Price)).Append('@')//卖价4
                    .Append(formatFracPrx(orderBookAll, askList[4].Price)).Append('@')//卖价5
                    .Append(askList[1].Quantity).Append('@')                          //卖量2
                    .Append(askList[2].Quantity).Append('@')                          //卖量3
                    .Append(askList[3].Quantity).Append('@')                          //卖量4
                    .Append(askList[4].Quantity).Append('@');                         //卖量5

                if (gbiBook != null)
                {
                    PriceDetail gbiOffer = gbiBook.askList[0];
                    PriceDetail gbiBid = gbiBook.bidList[0];

                    if (gbiBid.Price != 0) // && gbiBid.Price < askList[0].Price)
                    {
                        sb.Append(formatFracPrx(orderBookAll, gbiBid.Price)).Append('@')//隐含买价
                                .Append((gbiBid.Quantity == 0F) ? String.Empty : gbiBid.Quantity.ToString()).Append('@'); //隐含买量
                    }
                    else
                        sb.Append('@', 2);

                    if (gbiOffer.Price != 0) // && bidList[0].Price < gbiOffer.Price)
                    {
                        sb.Append(formatFracPrx(orderBookAll, gbiOffer.Price)).Append('@')//隐含卖价
                                .Append((gbiOffer.Quantity == 0F) ? String.Empty : gbiOffer.Quantity.ToString()).Append('@');//隐含卖量
                    }
                    else
                        sb.Append('@', 2);
                }
                else
                    sb.Append('@', 4);

            }
            //}
            #endregion

            //sb.Append(orderBookAll.sendTimestamp).Append('@');
            char dataFlag = ' ';
            if (orderBookAll.tradeVolume > orderBookAll.lastTradeVol)
                dataFlag = ZD_TRADE;
            else
                dataFlag = ZD_LATEST_DATA;

            sb.Append(dataFlag).Append('@');

            sb.Append('@')  //交易所数据时间戳
                    .Append(SOURCE_ID).Append('@', 2);

            sb.Append(formatFracPrx(orderBookAll, orderBookAll.new_preveSett)).Append('@')//昨结算价
                    .Append(formatFracPrx(orderBookAll, orderBookAll.new_TodaySett)).Append('@');//今结算价

            sb.Append(dataFlag);

            orderBookAll.lastTradeVol = orderBookAll.tradeVolume;

            string strMarket = sb.ToString();
            //Console.WriteLine(strMarket);
            sb.Clear();
            MyDataPool.StringBuilderPool.PutObject(sb);
            sendUDPPacket(strMarket);
            //  Common.Log(orderBookAll.codeBean.zdProduct, orderBookAll.codeBean.zdCode, $"{new StackTrace().GetFrame(2).GetMethod().Name}_F {strMarket}");
            //TT.Common.SendMessageLog.Log(bool.Parse(Configurations.LogSendMsg?.ToLower()), orderBookAll.codeBean.zdProduct, orderBookAll.codeBean.zdCode, $"{Enum.GetName(typeof(MessageType), MessageType.FracPrx)} {strMarket}");
            if (Configurations.LogSendMsgSecurityTypes.Contains(orderBookAll.codeBean.contractType))
            {
                LogAsync.Log(orderBookAll.codeBean.zdProduct, orderBookAll.codeBean.zdCode, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")} {logHeader} - {strMarket}");
            }
        }

        public static void pumpBook(OrderBookALL orderBookAll)
        {
            //CodeTransferInfo cti = CodeTransfer_CME.getZDCodeInfoByUpperCode(orderBookAll.securityId);
            //if (cti == null)
            //    return;

            if (orderBookAll.franctionalBase != 0)
            {
                pumpBookFracPrx(orderBookAll);
                return;
            }

            StringBuilder sb = null;

            #region lock region
            //lock (orderBookAll)
            //{
            OrderBook gbxBook = orderBookAll.GBXOrderBook;
            OrderBook gbiBook = orderBookAll.GBIOrderBook;

            PriceDetail[] bidList = gbxBook.bidList;
            PriceDetail[] askList = gbxBook.askList;

            if (bidList[0].Price >= askList[0].Price && askList[0].Price != 0)
            {
                return;
            }

            CodeBean cb = orderBookAll.codeBean;

            sb = MyDataPool.StringBuilderPool.GetObject();
            string prxFormat = orderBookAll.prxFormat;

            sb.Append(cb.zdExchg).Append('@').Append(cb.zdCode).Append('@')           //交易所代码和合约代码
                .Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append('@')
                .Append(orderBookAll.tradeVolume).Append('@')
                .Append((bidList[0].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[0].Price)).Append('@')   //买价
                .Append(bidList[0].Quantity).Append('@')                              //买量
                .Append((askList[0].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[0].Price)).Append('@') //卖价
                .Append(askList[0].Quantity).Append('@');                              //卖量

            if (GlobalData.optionSecurityIDSet.Contains(orderBookAll.securityId))
            {

                sb.Append((bidList[1].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[1].Price)).Append('@')//买价2
                    .Append((bidList[2].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[2].Price)).Append('@')//买价3
                    .Append('@', 2)
                    .Append(bidList[1].Quantity).Append('@')                            //买量2
                    .Append(bidList[2].Quantity).Append('@')                            //买量3
                    .Append('@', 2);

                sb.Append((askList[1].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[1].Price)).Append('@')//卖价2
                    .Append((askList[2].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[2].Price)).Append('@')//卖价3
                    .Append('@', 2)
                    .Append(askList[1].Quantity).Append('@')                          //卖量2
                    .Append(askList[2].Quantity).Append('@')                          //卖量3
                    .Append('@', 2);
            }
            else
            {
                sb.Append((bidList[1].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[1].Price)).Append('@')//买价2
                    .Append((bidList[2].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[2].Price)).Append('@')//买价3
                    .Append((bidList[3].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[3].Price)).Append('@')//买价4
                    .Append((bidList[4].Price == 0F) ? String.Empty : String.Format(prxFormat, bidList[4].Price)).Append('@')//买价5
                    .Append(bidList[1].Quantity).Append('@')                            //买量2
                    .Append(bidList[2].Quantity).Append('@')                            //买量3
                    .Append(bidList[3].Quantity).Append('@')                            //买量4
                    .Append(bidList[4].Quantity).Append('@');                           //买量5

                sb.Append((askList[1].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[1].Price)).Append('@')//卖价2
                    .Append((askList[2].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[2].Price)).Append('@')//卖价3
                    .Append((askList[3].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[3].Price)).Append('@')//卖价4
                    .Append((askList[4].Price == 0F) ? String.Empty : String.Format(prxFormat, askList[4].Price)).Append('@')//卖价5
                    .Append(askList[1].Quantity).Append('@')                          //卖量2
                    .Append(askList[2].Quantity).Append('@')                          //卖量3
                    .Append(askList[3].Quantity).Append('@')                          //卖量4
                    .Append(askList[4].Quantity).Append('@');                         //卖量5

            }


            if (gbiBook != null)
            {
                PriceDetail gbiOffer = gbiBook.askList[0];
                PriceDetail gbiBid = gbiBook.bidList[0];
                if (gbiBid.Price != 0)
                {
                    sb.Append(String.Format(prxFormat, gbiBid.Price)).Append('@')//隐含买价
                            .Append((gbiBid.Quantity == 0F) ? String.Empty : gbiBid.Quantity.ToString()).Append('@'); //隐含买量
                }
                else
                    sb.Append('@', 2);

                if (gbiOffer.Price != 0)
                {
                    sb.Append(String.Format(prxFormat, gbiOffer.Price)).Append('@')//隐含卖价
                            .Append((gbiOffer.Quantity == 0F) ? String.Empty : gbiOffer.Quantity.ToString()).Append('@');//隐含卖量
                }
                else
                    sb.Append('@', 2);
            }
            else
                sb.Append('@', 4);

            //}
            #endregion

            //sb.Append(orderBookAll.sendTimestamp).Append('@');
            sb.Append(ZD_BOOK_DATA); //DOM flag

            string strMarket = sb.ToString();
            //Console.WriteLine(strMarket);
            sb.Clear();
            MyDataPool.StringBuilderPool.PutObject(sb);
            sendUDPPacket(strMarket);
        }

        public static void pumpBookFracPrx(OrderBookALL orderBookAll)
        {
            StringBuilder sb = null;


            int fractionBase = orderBookAll.franctionalBase;
            OrderBook gbxBook = orderBookAll.GBXOrderBook;
            OrderBook gbiBook = orderBookAll.GBIOrderBook;

            PriceDetail[] bidList = gbxBook.bidList;
            PriceDetail[] askList = gbxBook.askList;

            if (bidList[0].Price >= askList[0].Price && askList[0].Price != 0)
            {
                return;
            }

            sb = MyDataPool.StringBuilderPool.GetObject();

            CodeBean cb = orderBookAll.codeBean;
            sb.Append(cb.zdExchg).Append('@').Append(cb.zdCode).Append('@')           //交易所代码和合约代码
                .Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append('@')
                .Append(orderBookAll.tradeVolume).Append('@')
                .Append(formatFracPrx(orderBookAll, bidList[0].Price)).Append('@')   //买价
                .Append(bidList[0].Quantity).Append('@')                              //买量
                .Append(formatFracPrx(orderBookAll, askList[0].Price)).Append('@') //卖价
                .Append(askList[0].Quantity).Append('@');                              //卖量


            if (GlobalData.optionSecurityIDSet.Contains(orderBookAll.securityId))
            {
                sb.Append(formatFracPrx(orderBookAll, bidList[1].Price)).Append('@')//买价2
                    .Append(formatFracPrx(orderBookAll, bidList[2].Price)).Append('@')//买价3
                    .Append('@', 2)
                    .Append(bidList[1].Quantity).Append('@')                            //买量2
                    .Append(bidList[2].Quantity).Append('@')                            //买量3
                    .Append('@', 2);

                sb.Append(formatFracPrx(orderBookAll, askList[1].Price)).Append('@')//卖价2
                    .Append(formatFracPrx(orderBookAll, askList[2].Price)).Append('@')//卖价3
                    .Append('@', 2)
                    .Append(askList[1].Quantity).Append('@')                          //卖量2
                    .Append(askList[2].Quantity).Append('@')                          //卖量3
                    .Append('@', 2);
            }
            else
            {
                sb.Append(formatFracPrx(orderBookAll, bidList[1].Price)).Append('@')//买价2
                    .Append(formatFracPrx(orderBookAll, bidList[2].Price)).Append('@')//买价3
                    .Append(formatFracPrx(orderBookAll, bidList[3].Price)).Append('@')//买价4
                    .Append(formatFracPrx(orderBookAll, bidList[4].Price)).Append('@')//买价5
                    .Append(bidList[1].Quantity).Append('@')                            //买量2
                    .Append(bidList[2].Quantity).Append('@')                            //买量3
                    .Append(bidList[3].Quantity).Append('@')                            //买量4
                    .Append(bidList[4].Quantity).Append('@');                           //买量5

                sb.Append(formatFracPrx(orderBookAll, askList[1].Price)).Append('@')//卖价2
                    .Append(formatFracPrx(orderBookAll, askList[2].Price)).Append('@')//卖价3
                    .Append(formatFracPrx(orderBookAll, askList[3].Price)).Append('@')//卖价4
                    .Append(formatFracPrx(orderBookAll, askList[4].Price)).Append('@')//卖价5
                    .Append(askList[1].Quantity).Append('@')                          //卖量2
                    .Append(askList[2].Quantity).Append('@')                          //卖量3
                    .Append(askList[3].Quantity).Append('@')                          //卖量4
                    .Append(askList[4].Quantity).Append('@');                         //卖量5

                if (gbiBook != null)
                {
                    PriceDetail gbiOffer = gbiBook.askList[0];
                    PriceDetail gbiBid = gbiBook.bidList[0];

                    if (gbiBid.Price != 0)
                    {
                        sb.Append(formatFracPrx(orderBookAll, gbiBid.Price)).Append('@')//隐含买价
                                .Append((gbiBid.Quantity == 0F) ? String.Empty : gbiBid.Quantity.ToString()).Append('@'); //隐含买量
                    }
                    else
                        sb.Append('@', 2);

                    if (gbiOffer.Price != 0)
                    {
                        sb.Append(formatFracPrx(orderBookAll, gbiOffer.Price)).Append('@')//隐含卖价
                                .Append((gbiOffer.Quantity == 0F) ? String.Empty : gbiOffer.Quantity.ToString()).Append('@');//隐含卖量
                    }
                    else
                        sb.Append('@', 2);
                }
                else
                    sb.Append('@', 4);

            }

            //sb.Append(orderBookAll.sendTimestamp).Append('@');
            sb.Append(ZD_BOOK_DATA); //Last change flag

            string strMarket = sb.ToString();
            sb.Clear();
            MyDataPool.StringBuilderPool.PutObject(sb);
            sendUDPPacket(strMarket);
            TT.Common.SendMessageLog.Log(bool.Parse(Configurations.LogSendMsg?.ToLower()), orderBookAll.codeBean.zdProduct, orderBookAll.codeBean.zdCode, $"{new StackTrace().GetFrame(1).GetMethod().Name} {strMarket}");
        }

        public static string formatFracPrx(OrderBookALL orderBookAll, double rawPrx)
        {
            if (rawPrx == 0F)
            {
                return "";
            }
            else
            {
                string strPrx = String.Format(FRACT_FORMAT1, rawPrx);
                int pointIndex = strPrx.IndexOf('.');
                if (pointIndex < 0)
                    return String.Format(orderBookAll.prxFormat, rawPrx);

                string intPartPrx = strPrx.Substring(0, pointIndex);
                float decPrx = Convert.ToSingle(strPrx.Substring(pointIndex));
                double final = Math.Round(decPrx * orderBookAll.franctionalBase, 0, MidpointRounding.ToEven) / orderBookAll.franctionalBase * 0.32;
                string result = intPartPrx + String.Format(orderBookAll.prxFormat, final);
                return result;
            }
        }

        public static string formatFracPrx(OrderBookALL orderBookAll, decimal rawPrx)
        {
            if (rawPrx == Decimal.Zero)
            {
                return "";
            }
            else
            {
                string strPrx = String.Format(FRACT_FORMAT1, rawPrx);
                int pointIndex = strPrx.IndexOf('.');
                if (pointIndex < 0)
                    return String.Format(orderBookAll.prxFormat, rawPrx);

                string intPartPrx = strPrx.Substring(0, pointIndex);
                float decPrx = Convert.ToSingle(strPrx.Substring(pointIndex));
                double final = Math.Round(decPrx * orderBookAll.franctionalBase, 0, MidpointRounding.ToEven) / orderBookAll.franctionalBase * 0.32;
                string result = intPartPrx + String.Format(orderBookAll.prxFormat, final);
                return result;
            }
        }



        public static void sendUDPPacket(string strMarket)
        {
            //Console.WriteLine(DateTime.Now.ToString("MM-dd HH:mm:ss ") + strMarket);
            //Console.WriteLine(strMarket);
            int count = strMarket.Length;
            byte[] data = null;
            // Small data use reuseing buffer
            if (count < 1025)
            {
                data = MyDataPool.Byte1024Pool.GetObject();
                count = System.Text.ASCIIEncoding.ASCII.GetBytes(strMarket, 0, count, data, 0);

                //for (int i = 0; i < destEndPointArr.Length; i++)
                udpSendSocket.SendTo(data, 0, count, SocketFlags.None, destEndPointArr[0]);
                //udpSendSocket.SendTo(data, 0, count, SocketFlags.None, destEndPointArr[1]);
                MyDataPool.Byte1024Pool.PutObject(data);
            }
            else
            {
                EndPoint ep = destEndPointArr[0];
                // ep.AddressFamily
                data = System.Text.ASCIIEncoding.ASCII.GetBytes(strMarket);
                //for (int i = 0; i < destEndPointArr.Length; i++)
                udpSendSocket.SendTo(data, 0, count, SocketFlags.None, destEndPointArr[0]);
                //udpSendSocket.SendTo(data, 0, count, SocketFlags.None, destEndPointArr[1]);

            }
        }

    }


    //public enum MessageType : byte
    //{
    //    None,
    //    Trade,
    //    Incremental,
    //    SnapShot,
    //    FracPrx,
    //    EventHandler
    //}

    public class LogMessageType
    {
        public const string None = "None";
        public const string Trade = "Trade";
        public const string Incremental = "Incremental";
        public const string SnapShot = "SnapShot";
        public const string FracPrx = "FracPrx";
        public const string EventHandler = "EventHandler";
        public const string PreveSett = "PreveSett";
    }


}
