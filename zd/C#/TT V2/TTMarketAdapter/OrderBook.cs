using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonClassLib.ContractTransfer;
using System.Text.RegularExpressions;

namespace TTMarketAdapter
{


    public class PriceDetail : IComparable<PriceDetail>, IComparer<PriceDetail>, ICloneable
    {
        public double Price;        // FP number
        public int OrderCount;
        public int Quantity;        // does not apply to all price types
                                    /*   public char ttFlag='?';  */   // used for TT incremental price logic
        public char ttFlag = '?';
        public object Clone()
        {
            PriceDetail priceDetail = new PriceDetail();
            priceDetail.Price = this.Price;
            priceDetail.OrderCount = this.OrderCount;
            priceDetail.Quantity = this.Quantity;
            priceDetail.ttFlag = this.ttFlag;
            return priceDetail;
        }
        public PriceDetail Copy()
        {
            return (PriceDetail)Clone();
        }

        public bool IsZero()
        {
            return this.Price == 0 &&
                   this.Quantity == 0 &&
                   this.ttFlag == '?';
        }
        //public int Compare(PriceDetail x, PriceDetail y)
        //{
        //    if (x.Price > y.Price)
        //        return 1;
        //    else if (x.Price < y.Price)
        //        return -1;
        //    else
        //        return 0;
        //}

        public int CompareTo(PriceDetail y)
        {
            ////价格=0 排在最后
            //if(this.Price==0&&y.Price==0)
            //{
            //    return 0;
            //}
            //if(this.Price==0)
            //{
            //    return 1;
            //}
            //if (y.Price == 0)
            //{
            //    return -1;
            //}

            //if (this.Price > y.Price)
            //    return 1;
            //else if (this.Price < y.Price)
            //    return -1;
            //else
            //    return 0;

            //价格=0 排在最后
            if (this.Price == 0 && y.Price == 0)
            {
                return 0;
            }
            else if (this.Price == 0 && y.Price != 0)
            {
                return 1;
            }
            else if (this.Price != 0 && y.Price == 0)
            {
                return -1;
            }
            else
            {
                return this.Price.CompareTo(y.Price);
            }
        }

        //public static BidListComparer( List<PriceDetail> bidList)
        //{
        //    for(int i=0;i< bidList.Count-1;i++)
        //    {
        //        for (int j = i + 1;j<bidList.Count;j++)
        //        {
        //            if(bidList[i].Price)
        //        }
        //    }
        //}

        public int Compare(PriceDetail x, PriceDetail y)
        {
            if (x.Price == 0 && y.Price == 0)
            {
                return 0;
            }
            if (x.Price == 0)
            {
                return 1;
            }
            if (y.Price == 0)
            {
                return -1;
            }
            return x.Price.CompareTo(y.Price);

        }
    }


    public class OrderBook : ICloneable
    {
        public PriceDetail[] bidList;
        public PriceDetail[] askList;

        public int ChangeMask;
        public int depth;
        OrderBook()
        {

        }
        public object Clone()
        {
            OrderBook orderBook = new OrderBook();
            orderBook.ChangeMask = this.ChangeMask;
            orderBook.depth = this.depth;
            orderBook.bidList = new PriceDetail[this.bidList.Length];
            orderBook.askList = new PriceDetail[this.askList.Length];
            for (int i = 0; i < this.bidList.Length; i++)
            {
                orderBook.bidList[i] = this.bidList[i].Copy();
            }
            for (int i = 0; i < this.askList.Length; i++)
            {
                orderBook.askList[i] = this.askList[i].Copy();
            }

            return orderBook;
        }

        public OrderBook Copy()
        {
            return (OrderBook)Clone();
        }

        public OrderBook(int depth)
        {
            bidList = new PriceDetail[depth];
            askList = new PriceDetail[depth];
            this.depth = depth;

            for (int i = 0; i < depth; i++)
            {
                bidList[i] = new PriceDetail();
                askList[i] = new PriceDetail();
            }
        }

        public void clearData()
        {
            for (int i = 0; i < bidList.Length; i++)
            {
                bidList[i].Price = 0;
                bidList[i].Quantity = 0;
                bidList[i].ttFlag = '?';
            }

            for (int i = 0; i < askList.Length; i++)
            {
                askList[i].Price = 0;
                askList[i].Quantity = 0;
                askList[i].ttFlag = '?';
            }
        }

        public void clearBidList()
        {
            for (int i = 0; i < bidList.Length; i++)
            {
                bidList[i].Price = 0;
                bidList[i].Quantity = 0;
                bidList[i].ttFlag = '?';
            }
        }

        public void clearAskList()
        {
            for (int i = 0; i < askList.Length; i++)
            {
                askList[i].Price = 0;
                askList[i].Quantity = 0;
                askList[i].ttFlag = '?';
            }
        }


    }

    /// <summary>
    /// TT处理后的TT数据Model
    /// </summary>
    public class OrderBookALL : ICloneable
    {
        private const int TRADE_COUNT = 10;
        public float prxFactor = 0f;
        public string prxFormat;
        public int franctionalBase = 0;

        public bool isFractionalPrx = false;
        public int mainDenominator = 0;
        public int subDenominator = 0;
        public int displayPrxPrecison = 0;
        public int multiNum = 0;

        /// <summary>
        /// 总成交金额
        /// </summary>
        public decimal TradingVolume
        {
            get
            {
               if( OrderBookMgr.Instance.TradingVolumeDic.TryGetValue(codeBean.zdCode,out decimal val))
                {
                    return val;
                }
                return 0;
            }
        }

        public PriceDetail lastTrade = new PriceDetail();

        public OrderBook GBXOrderBook;
        public OrderBook GBIOrderBook;

        /// <summary>
        /// 开盘价
        /// </summary>
        public double openingPrice;
        /// <summary>
        /// 结算价
        /// </summary>
        public double settPrice;

        //Added on 20160805 -begin
        public double holdSettPrice = 0;
        public int holdTradeVolume = 0;
        //Added on 20160805 -end

        /// <summary>
        /// 今结算价
        /// </summary>
        public double new_TodaySett = 0;
        /// <summary>
        /// 昨结算价
        /// </summary>
        public double new_preveSett = 0;

        /// <summary>
        /// 当日成交量
        /// </summary>
        public int tradeVolume;
        public int lastTradeVol;

        /// <summary>
        /// 持仓量
        /// </summary>
        public uint openInterest;
        /// <summary>
        /// 最高价
        /// </summary>
        public double tsHighPrice;
        /// <summary>
        ///最低价
        /// </summary>
        public double tsLowPrice;

        public long lastSendTick;
        public uint securityId;
        public int bookSendCnt = 0;

        public ulong sendTimestamp = 0;
        public uint rptSeq = 0;

        public bool isGoodForGBXBook = true;

        public CodeBean codeBean = null;

        public OrderBookALL(int outrightDepth, int impliedDepth, float prxFactor)
        {
            GBXOrderBook = new OrderBook(outrightDepth);
            if (impliedDepth != 0)
                GBIOrderBook = new OrderBook(impliedDepth);

            prxFormat = getPrxFormat(prxFactor);
        }
        OrderBookALL()
        {

        }

        public object Clone()
        {
            OrderBookALL orderBookALL = new OrderBookALL();
            orderBookALL.prxFactor = this.prxFactor;
            orderBookALL.prxFormat = this.prxFormat;
            orderBookALL.franctionalBase = this.franctionalBase;

            orderBookALL.isFractionalPrx = this.isFractionalPrx;
            orderBookALL.mainDenominator = this.mainDenominator;
            orderBookALL.subDenominator = this.subDenominator;
            orderBookALL.displayPrxPrecison = this.displayPrxPrecison;
            orderBookALL.multiNum = this.multiNum;

            orderBookALL.lastTrade = this.lastTrade.Copy();//

            orderBookALL.GBXOrderBook = this.GBXOrderBook.Copy();//
            orderBookALL.GBIOrderBook = this.GBIOrderBook.Copy();//

            orderBookALL.openingPrice = this.openingPrice;
            orderBookALL.settPrice = this.settPrice;

            orderBookALL.holdSettPrice = this.holdSettPrice;
            orderBookALL.holdTradeVolume = this.holdTradeVolume;
            orderBookALL.new_TodaySett = this.new_TodaySett;
            orderBookALL.new_preveSett = this.new_preveSett;

            orderBookALL.tradeVolume = this.tradeVolume;
            orderBookALL.lastTradeVol = this.lastTradeVol;

            orderBookALL.openInterest = this.openInterest;
            orderBookALL.tsHighPrice = this.tsHighPrice;
            orderBookALL.tsLowPrice = this.tsLowPrice;

            orderBookALL.lastSendTick = this.lastSendTick;
            orderBookALL.securityId = this.securityId;
            orderBookALL.bookSendCnt = this.bookSendCnt;

            orderBookALL.sendTimestamp = this.sendTimestamp;
            orderBookALL.rptSeq = this.rptSeq;

            orderBookALL.isGoodForGBXBook = this.isGoodForGBXBook;

            orderBookALL.codeBean = this.codeBean;// codeBean 此处不会改变
            return orderBookALL;
        }

        public OrderBookALL Copy()
        {
            return (OrderBookALL)this.Clone();
        }

        public string getPrxFormat(float prxFormat)
        {
            string input = prxFormat.ToString("0.########");
            string pattern = @"\d";
            string replacement = "0";
            Regex rgx = new Regex(pattern);
            string result = rgx.Replace(input, replacement);

            return "{0:" + result + "}";
        }

    }
}
