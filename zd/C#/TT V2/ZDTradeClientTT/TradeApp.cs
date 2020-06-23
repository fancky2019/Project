using QuickFix.Fields;
using System;
using System.Collections.Concurrent;

namespace ZDTradeClientTT
{

    public class ExectutionEventArgs : EventArgs
    {
        private QuickFix.FIX42.ExecutionReport execReport;

        public ExectutionEventArgs(QuickFix.FIX42.ExecutionReport message)
        {
            execReport = message;
        }

        public QuickFix.FIX42.ExecutionReport ExecReportObject
        {
            get { return execReport; }
            set { value = execReport; }
        }
    }

    // OrderCancelReject
    public class OrderCancelRejectEventArgs : EventArgs
    {
        private QuickFix.FIX42.OrderCancelReject cancelRejMsg;

        public OrderCancelRejectEventArgs(QuickFix.FIX42.OrderCancelReject message)
        {
            cancelRejMsg = message;
        }

        public QuickFix.FIX42.OrderCancelReject CancelRejMsg
        {
            get { return cancelRejMsg; }
            set { value = cancelRejMsg; }
        }
    }


    public class SecurityDefEventArgs : EventArgs
    {
        private QuickFix.FIX42.SecurityDefinition securityDefMsg;

        public SecurityDefEventArgs(QuickFix.FIX42.SecurityDefinition message)
        {
            securityDefMsg = message;
        }

        public QuickFix.FIX42.SecurityDefinition SecurityDefMsg
        {
            get { return securityDefMsg; }
            set { value = securityDefMsg; }
        }
    }

    public class TradeApp : QuickFix.MessageCracker, QuickFix.IApplication
    {
        public QuickFix.SessionID ActiveSessionID { get; set; }
        public QuickFix.SessionSettings MySessionSettings { get; set; }

        //private ICustomFixStrategy _strategy = null;

        private QuickFix.IInitiator _initiator = null;
        public QuickFix.IInitiator Initiator
        {
            set
            {
                if (_initiator != null)
                    throw new Exception("You already set the initiator");
                _initiator = value;
            }
            get
            {
                if (_initiator == null)
                    throw new Exception("You didn't provide an initiator");
                return _initiator;
            }
        }

        public event EventHandler<EventArgs> LogonEvent;
        public event EventHandler<EventArgs> LogoutEvent;

        //public event Action<QuickFix.FIX42.ExecutionReport> Fix42ExecReportEvent;
        private BlockingCollection<QuickFix.FIX42.ExecutionReport> execReportBC = null;


   

        public TradeApp(QuickFix.SessionSettings settings, BlockingCollection<QuickFix.FIX42.ExecutionReport> execReportBC)
        {
            this.execReportBC = execReportBC;

            //_strategy = strategy;
            ActiveSessionID = null;
            MySessionSettings = settings;
        }

        public void Start()
        {
            TT.Common.NLogUtility.Info("TradeApp::Start() called");
            if (Initiator.IsStopped)
                Initiator.Start();
            else
                TT.Common.NLogUtility.Info("(already started)");
        }

        public void Stop()
        {
            TT.Common.NLogUtility.Info("TradeApp::Stop() called");
            Initiator.Stop();
        }

        /// <summary>
        /// Tries to send the message; throws if not logged on.
        /// </summary>
        /// <param name="m"></param>
        public bool Send(QuickFix.Message m)
        {
            //if (Initiator.IsLoggedOn() == false)
            if (!Initiator.IsLoggedOn)
            {
                //throw new Exception("Can't send a message.  We're not logged on.");
                return false;
            }
            if (ActiveSessionID == null)
            {
                //throw new Exception("Can't send a message.  ActiveSessionID is null (not logged on?).");
                return false;
            }

            return QuickFix.Session.SendToTarget(m, this.ActiveSessionID);
        }


        #region Application Members

        public void FromAdmin(QuickFix.Message message, QuickFix.SessionID sessionID)
        {
            //TT.Common.NLogUtility.Debug("## FromAdmin: " + message.ToString());
            string msgType = message.Header.GetField(Tags.MsgType);
            if (QuickFix.Fields.MsgType.LOGON == msgType ||
                QuickFix.Fields.MsgType.LOGOUT == msgType ||
                QuickFix.Fields.MsgType.REJECT == msgType)
            {
                //AuditTrailMgr.addMsg(message, null);
            }
        }

        public void ToAdmin(QuickFix.Message message, QuickFix.SessionID sessionID)
        {
            //_strategy.ProcessToAdmin(message, QuickFix.Session.LookupSession(sessionID));

            message.Header.SetField(new SendingTime(DateTime.UtcNow));
            string msgType = message.Header.GetField(Tags.MsgType);
            if (QuickFix.Fields.MsgType.LOGON == msgType)
            {
                AddLogonField(message);

            }
            //TT.Common.NLogUtility.Debug("## ToAdmin: " + message.ToString());
        }
        private void AddLogonField(QuickFix.Message message)
        {
            //message.Header.SetField(appSysName); //new ApplicationSystemName("DA_TRADING_PLATFORM"));
            //message.Header.SetField(tradingSysVersion); //new TradingSystemVersion("V1.0"));
            //message.Header.SetField(appSysVendor); //new ApplicationSystemVendor("DA_SOFT"));
            //CfgManager cfgManager = CfgManager.getInstance("ZDTradeClientTT.exe");
            string password = ZDTradeClientTTConfiurations.SessionAndPsw.Split(',')[1];
            message.SetField(new RawDataLength(password.Length));
            message.SetField(new RawData(password));
            message.SetField(new HeartBtInt(30));

            //message.SetField(new ResetSeqNumFlag(true));
        }
        /// <summary>
        /// 所有TT返回的信息都会先进入此方法，然后Crack反射执行注册进来的（OnMessage）方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sessionID"></param>
        public void FromApp(QuickFix.Message message, QuickFix.SessionID sessionID)
        {
            //Console.WriteLine("## FromApp: " + message.ToString());
            try
            {
                //AuditTrailMgr.addMsg(AuditTrailMgr.S7_FROM_CME, message);
                Crack(message, sessionID);
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        public void ToApp(QuickFix.Message message, QuickFix.SessionID sessionId)
        {
            try
            {
                //_strategy.ProcessToApp(message, QuickFix.Session.LookupSession(sessionId));
                //AuditTrailMgr.addMsg(AuditTrailMgr.S7_TO_CME, message);
                //Console.WriteLine("## ToApp: " + message.ToString());


                message.Header.SetField(new SendingTime(DateTime.UtcNow));
                message.Header.SetField(new TargetSubID("G"));
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        public void OnCreate(QuickFix.SessionID sessionID)
        {
            TT.Common.NLogUtility.Info("## OnCreate: " + sessionID.ToString());
        }

        public void OnLogon(QuickFix.SessionID sessionID)
        {
            this.ActiveSessionID = sessionID;
            //AuditTrailMgr.fixSessionID = sessionID;

            TT.Common.NLogUtility.Info(String.Format("==OnLogon: {0}==", this.ActiveSessionID.ToString()));
            if (LogonEvent != null)
                LogonEvent(null, null);
            //LogonEvent(this, new EventArgs());
        }

        public void OnLogout(QuickFix.SessionID sessionID)
        {
            // not sure how ActiveSessionID could ever be null, but it happened.
            string a = (this.ActiveSessionID == null) ? "null" : this.ActiveSessionID.ToString();
            TT.Common.NLogUtility.Info(String.Format("==OnLogout: {0}==", a));
            if (LogoutEvent != null)
                LogoutEvent(this, new EventArgs());
        }
        #endregion


        //public event EventHandler<ExectutionEventArgs> ExecutionEvent;
        public event EventHandler<OrderCancelRejectEventArgs> OrderCacnelRejectEvent;
        public event EventHandler<SecurityDefEventArgs> SecurityDefEvent;


        /// <summary>
        /// 35=8
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>
        public void OnMessage(QuickFix.FIX42.ExecutionReport msg, QuickFix.SessionID s)
        {
            /*
            if (ExecutionEvent != null)
                ExecutionEvent(this, new ExectutionEventArgs(msg));
            */

            try
            {
                execReportBC.Add(msg);
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 35=9
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>
        public void OnMessage(QuickFix.FIX42.OrderCancelReject msg, QuickFix.SessionID s)
        {
            if (OrderCacnelRejectEvent != null)
                OrderCacnelRejectEvent(this, new OrderCancelRejectEventArgs(msg));
        }

        /// <summary>
        /// 35=d
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>
        public void OnMessage(QuickFix.FIX42.SecurityDefinition msg, QuickFix.SessionID s)
        {
            if (SecurityDefEvent != null)
                SecurityDefEvent(this, new SecurityDefEventArgs(msg));
        }

        /// <summary>
        /// News 提示信息(35=B)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>
        public void OnMessage(QuickFix.FIX42.News msg, QuickFix.SessionID s)
        {

        }

        /// <summary>
        /// 合约拒绝(35=Y)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>
        public void OnMessage(QuickFix.FIX42.MarketDataRequestReject msg, QuickFix.SessionID s)
        {

        }

        /// <summary>
        /// 35=j
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>
        public void OnMessage(QuickFix.FIX42.BusinessMessageReject msg, QuickFix.SessionID s)
        { }
    }

}
