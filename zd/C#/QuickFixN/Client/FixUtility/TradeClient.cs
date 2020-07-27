using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix;
using QuickFix.Fields;
using System.Threading.Tasks;

namespace Client.FixUtility
{
    /// <summary>
    /// 一、取消继承 QuickFix.MessageCracker - 反射，可读性差。
    /// </summary>
    class TradeClient : QuickFix.IApplication
    {
        // Debug, Info, Warn, Error and Fatal
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        private Session _session = null;
        private static TradeClient _instance = null;
        private static object _lockObj = new object();

        public static TradeClient Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new TradeClient();
                        }
                    }
                }
                return _instance;
            }
        }
        public QuickFix.Transport.SocketInitiator SocketInitiator { get; }

        public event Action<Message, SessionID> ReceiveMsgFromApp;
        public event Action<string> Logon;
        public event Action<string> LogOut;

        private TradeClient()
        {
            string tradeClientConfigPath = "Config/TradeClient.cfg";
            QuickFix.SessionSettings settings = new QuickFix.SessionSettings(tradeClientConfigPath);
            QuickFix.IMessageStoreFactory storeFactory = new QuickFix.FileStoreFactory(settings);
            QuickFix.ILogFactory logFactory = new QuickFix.FileLogFactory(settings);
            SocketInitiator = new QuickFix.Transport.SocketInitiator(this, storeFactory, settings, logFactory);
        }

        #region IApplication interface overrides

        public void OnCreate(SessionID sessionID)
        {
            _session = Session.LookupSession(sessionID);
        }

        public void OnLogon(SessionID sessionID)
        {
            //FIX.4.4:ZDDEV->EXECUTOR|
            var sendAndTargetIDs = sessionID.ToString().Split(':')[1];
            _nLog.Info($"Logon - { sendAndTargetIDs}");
            Logon?.Invoke(sendAndTargetIDs);


        }
        public void OnLogout(SessionID sessionID)
        {
            var sendAndTargetIDs = sessionID.ToString().Split(':')[1];
            _nLog.Info($"Logout - { sendAndTargetIDs}");
            LogOut?.Invoke(sendAndTargetIDs);
        }

        /// <summary>
        ///Admin Msg 35=0A12345n
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sessionID"></param>
        public void FromAdmin(Message message, SessionID sessionID)
        {
            _nLog.Info(message.ToString());
        }
        public void ToAdmin(Message message, SessionID sessionID)
        {
            message.Header.SetField(new SendingTime(DateTime.UtcNow));
            string msgType = message.Header.GetString(Tags.MsgType);
            if (QuickFix.Fields.MsgType.LOGON == msgType)
            {
                if(!string.IsNullOrEmpty(sessionID.SenderCompPsw))
                {
                    message.SetField(new RawDataLength(sessionID.SenderCompPsw.Length));
                    message.SetField(new RawData(sessionID.SenderCompPsw));
                }
             
                message.SetField(new HeartBtInt(int.Parse(sessionID.HeartBtInt)));
            }

        }

        /// <summary>
        /// Client receive messages from brker
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sessionID"></param>
        public void FromApp(Message message, SessionID sessionID)
        {
            this.ReceiveMsgFromApp?.Invoke(message, sessionID);
        }

        /// <summary>
        /// 发送到Broker
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sessionID"></param>
        public void ToApp(Message message, SessionID sessionID)
        {
            try
            {
                bool possDupFlag = false;
                if (message.Header.IsSetField(QuickFix.Fields.Tags.PossDupFlag))
                {
                    possDupFlag = QuickFix.Fields.Converters.BoolConverter.Convert(
                        message.Header.GetString(QuickFix.Fields.Tags.PossDupFlag)); /// FIXME
                }
                if (possDupFlag)
                    throw new DoNotSend();
            }
            catch (FieldNotFoundException)
            { }


        }
        #endregion




        public bool SendMessage(Message m)
        {
            if (!SocketInitiator.IsLoggedOn)
            {
                return false;
            }
            if (SocketInitiator == null)
            {
                return false;
            }

            return _session != null ? _session.Send(m) : false;

        }


        #region Message creation functions
        //private QuickFix.FIX44.NewOrderSingle QueryNewOrderSingle44()
        //{
        //    QuickFix.Fields.OrdType ordType = null;

        //    QuickFix.FIX44.NewOrderSingle newOrderSingle = new QuickFix.FIX44.NewOrderSingle(
        //        QueryClOrdID(),
        //        QuerySymbol(),
        //        QuerySide(),
        //        new TransactTime(DateTime.Now),
        //        ordType = QueryOrdType());

        //    newOrderSingle.Set(new HandlInst('1'));
        //    newOrderSingle.Set(QueryOrderQty());
        //    newOrderSingle.Set(QueryTimeInForce());
        //    if (ordType.getValue() == OrdType.LIMIT || ordType.getValue() == OrdType.STOP_LIMIT)
        //        newOrderSingle.Set(QueryPrice());
        //    if (ordType.getValue() == OrdType.STOP || ordType.getValue() == OrdType.STOP_LIMIT)
        //        newOrderSingle.Set(QueryStopPx());

        //    return newOrderSingle;
        //}

        //private QuickFix.FIX44.OrderCancelRequest QueryOrderCancelRequest44()
        //{
        //    QuickFix.FIX44.OrderCancelRequest orderCancelRequest = new QuickFix.FIX44.OrderCancelRequest(
        //        QueryOrigClOrdID(),
        //        QueryClOrdID(),
        //        QuerySymbol(),
        //        QuerySide(),
        //        new TransactTime(DateTime.Now));

        //    orderCancelRequest.Set(QueryOrderQty());
        //    return orderCancelRequest;
        //}

        //private QuickFix.FIX44.OrderCancelReplaceRequest QueryCancelReplaceRequest44()
        //{
        //    QuickFix.FIX44.OrderCancelReplaceRequest ocrr = new QuickFix.FIX44.OrderCancelReplaceRequest(
        //        QueryOrigClOrdID(),
        //        QueryClOrdID(),
        //        QuerySymbol(),
        //        QuerySide(),
        //        new TransactTime(DateTime.Now),
        //        QueryOrdType());

        //    ocrr.Set(new HandlInst('1'));
        //    if (QueryConfirm("New price"))
        //        ocrr.Set(QueryPrice());
        //    if (QueryConfirm("New quantity"))
        //        ocrr.Set(QueryOrderQty());

        //    return ocrr;
        //}

        //private QuickFix.FIX44.MarketDataRequest QueryMarketDataRequest44()
        //{
        //    MDReqID mdReqID = new MDReqID("MARKETDATAID");
        //    SubscriptionRequestType subType = new SubscriptionRequestType(SubscriptionRequestType.SNAPSHOT);
        //    MarketDepth marketDepth = new MarketDepth(0);

        //    QuickFix.FIX44.MarketDataRequest.NoMDEntryTypesGroup marketDataEntryGroup = new QuickFix.FIX44.MarketDataRequest.NoMDEntryTypesGroup();
        //    marketDataEntryGroup.Set(new MDEntryType(MDEntryType.BID));

        //    QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup symbolGroup = new QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup();
        //    symbolGroup.Set(new Symbol("LNUX"));

        //    QuickFix.FIX44.MarketDataRequest message = new QuickFix.FIX44.MarketDataRequest(mdReqID, subType, marketDepth);
        //    message.AddGroup(marketDataEntryGroup);
        //    message.AddGroup(symbolGroup);

        //    return message;
        //}
        #endregion

        #region field query private methods
        //private ClOrdID QueryClOrdID()
        //{
        //    Console.WriteLine();
        //    Console.Write("ClOrdID? ");
        //    return new ClOrdID(Console.ReadLine().Trim());
        //}

        //private OrigClOrdID QueryOrigClOrdID()
        //{
        //    Console.WriteLine();
        //    Console.Write("OrigClOrdID? ");
        //    return new OrigClOrdID(Console.ReadLine().Trim());
        //}

        //private Symbol QuerySymbol()
        //{
        //    Console.WriteLine();
        //    Console.Write("Symbol? ");
        //    return new Symbol(Console.ReadLine().Trim());
        //}

        //private Side QuerySide()
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("1) Buy");
        //    Console.WriteLine("2) Sell");
        //    Console.WriteLine("3) Sell Short");
        //    Console.WriteLine("4) Sell Short Exempt");
        //    Console.WriteLine("5) Cross");
        //    Console.WriteLine("6) Cross Short");
        //    Console.WriteLine("7) Cross Short Exempt");
        //    Console.Write("Side? ");
        //    string s = Console.ReadLine().Trim();

        //    char c = ' ';
        //    switch (s)
        //    {
        //        case "1": c = Side.BUY; break;
        //        case "2": c = Side.SELL; break;
        //        case "3": c = Side.SELL_SHORT; break;
        //        case "4": c = Side.SELL_SHORT_EXEMPT; break;
        //        case "5": c = Side.CROSS; break;
        //        case "6": c = Side.CROSS_SHORT; break;
        //        case "7": c = 'A'; break;
        //        default: throw new Exception("unsupported input");
        //    }
        //    return new Side(c);
        //}

        //private OrdType QueryOrdType()
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("1) Market");
        //    Console.WriteLine("2) Limit");
        //    Console.WriteLine("3) Stop");
        //    Console.WriteLine("4) Stop Limit");
        //    Console.Write("OrdType? ");
        //    string s = Console.ReadLine().Trim();

        //    char c = ' ';
        //    switch (s)
        //    {
        //        case "1": c = OrdType.MARKET; break;
        //        case "2": c = OrdType.LIMIT; break;
        //        case "3": c = OrdType.STOP; break;
        //        case "4": c = OrdType.STOP_LIMIT; break;
        //        default: throw new Exception("unsupported input");
        //    }
        //    return new OrdType(c);
        //}

        //private OrderQty QueryOrderQty()
        //{
        //    Console.WriteLine();
        //    Console.Write("OrderQty? ");
        //    return new OrderQty(Convert.ToDecimal(Console.ReadLine().Trim()));
        //}

        //private TimeInForce QueryTimeInForce()
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("1) Day");
        //    Console.WriteLine("2) IOC");
        //    Console.WriteLine("3) OPG");
        //    Console.WriteLine("4) GTC");
        //    Console.WriteLine("5) GTX");
        //    Console.Write("TimeInForce? ");
        //    string s = Console.ReadLine().Trim();

        //    char c = ' ';
        //    switch (s)
        //    {
        //        case "1": c = TimeInForce.DAY; break;
        //        case "2": c = TimeInForce.IMMEDIATE_OR_CANCEL; break;
        //        case "3": c = TimeInForce.AT_THE_OPENING; break;
        //        case "4": c = TimeInForce.GOOD_TILL_CANCEL; break;
        //        case "5": c = TimeInForce.GOOD_TILL_CROSSING; break;
        //        default: throw new Exception("unsupported input");
        //    }
        //    return new TimeInForce(c);
        //}

        //private Price QueryPrice()
        //{
        //    Console.WriteLine();
        //    Console.Write("Price? ");
        //    return new Price(Convert.ToDecimal(Console.ReadLine().Trim()));
        //}

        //private StopPx QueryStopPx()
        //{
        //    Console.WriteLine();
        //    Console.Write("StopPx? ");
        //    return new StopPx(Convert.ToDecimal(Console.ReadLine().Trim()));
        //}

        #endregion
    }
}
