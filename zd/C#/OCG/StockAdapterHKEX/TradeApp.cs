using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix;
using QuickFix.Fields;
using CommonClassLib;
using StockAdapterHKEX;
using StockAdapterHKEX.Common;
using AuthCommon;
using Log = StockAdapterHKEX.Common.Log;

namespace StockAdapterHKEX
{

    public class ExectutionEventArgs : EventArgs
    {
        private QuickFix.FIX50.ExecutionReport execReport;

        public ExectutionEventArgs(QuickFix.FIX50.ExecutionReport message)
        {
            execReport = message;
        }

        public QuickFix.FIX50.ExecutionReport ExecReportObject
        {
            get { return execReport; }
            set { value = execReport; }
        }
    }

    // OrderCancelReject
    public class OrderCancelRejectEventArgs : EventArgs
    {
        private QuickFix.FIX50.OrderCancelReject cancelRejMsg;

        public OrderCancelRejectEventArgs(QuickFix.FIX50.OrderCancelReject message)
        {
            cancelRejMsg = message;
        }

        public QuickFix.FIX50.OrderCancelReject CancelRejMsg
        {
            get { return cancelRejMsg; }
            set { value = cancelRejMsg; }
        }
    }

    public class BusinessMessageRejectEventArgs : EventArgs
    {
        private QuickFix.FIX50.BusinessMessageReject businessRejMsg;

        public BusinessMessageRejectEventArgs(QuickFix.FIX50.BusinessMessageReject message)
        {
            businessRejMsg = message;
        }

        public QuickFix.FIX50.BusinessMessageReject BusinessRejMsg
        {
            get { return businessRejMsg; }
            set { value = businessRejMsg; }
        }
    }


    public class SecurityDefEventArgs : EventArgs
    {
        private QuickFix.FIX50.SecurityDefinition securityDefMsg;

        public SecurityDefEventArgs(QuickFix.FIX50.SecurityDefinition message)
        {
            securityDefMsg = message;
        }

        public QuickFix.FIX50.SecurityDefinition SecurityDefMsg
        {
            get { return securityDefMsg; }
            set { value = securityDefMsg; }
        }
    }

    public class UserResponseEventArgs : EventArgs
    {
        private QuickFix.FIX50.UserResponse userResponseMsg;

        public UserResponseEventArgs(QuickFix.FIX50.UserResponse message)
        {
            userResponseMsg = message;
        }

        public QuickFix.FIX50.UserResponse UserResponseMsg
        {
            get { return userResponseMsg; }
            set { value = userResponseMsg; }
        }
    }

    public class TradeApp : QuickFix.MessageCracker, QuickFix.IApplication
    {
        public Session _session = null;

        public QuickFix.SessionID ActiveSessionID { get; set; }
        public QuickFix.SessionSettings MySessionSettings { get; set; }

        private ICustomFixStrategy _strategy = null;

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

        //public event Action<QuickFix.FIX50.ExecutionReport> Fix42ExecReportEvent;

        public TradeApp(QuickFix.SessionSettings settings)
            : this(settings, new NullFixStrategy())
        { }

        public TradeApp(QuickFix.SessionSettings settings, ICustomFixStrategy strategy)
        {
            _strategy = strategy;
            ActiveSessionID = null;
            MySessionSettings = settings;
        }

        public void Start()
        {
            Console.WriteLine("TradeApp::Start() called");
            if (Initiator.IsStopped)
                Initiator.Start();
            else
                Console.WriteLine("(already started)");
        }

        public void Stop()
        {
            Console.WriteLine("TradeApp::Stop() called");
            Initiator.Stop();
        }

        public void OnCreate(QuickFix.SessionID sessionID)
        {
            Console.WriteLine("## OnCreate: " + sessionID.ToString());
            _session = Session.LookupSession(sessionID);
        }

        public bool SendMessage(Message m)
        {
            if (_session != null)
            {
                return _session.Send(m);
            }

            else
            {
                // This probably won't ever happen.
                Console.WriteLine("Can't send message: session not created.");
                return false;
            }
        }


        #region Application Members

        public void FromAdmin(QuickFix.Message message, QuickFix.SessionID sessionID)
        {
            Console.WriteLine("## FromAdmin: " + message.ToString());
            string msgType = message.Header.GetField(Tags.MsgType);
            if (QuickFix.Fields.MsgType.LOGON == msgType ||
                QuickFix.Fields.MsgType.LOGOUT == msgType ||
                QuickFix.Fields.MsgType.REJECT == msgType)
            {
                //AuditTrailMgr.addMsg(message, null);

                #region  修改密码成功保存修改后的密码
                /*
                 * 修改密码成功之后保存修改后的密码。
                 * If the client’s Logon message included the field NewEncryptedPassword and the client is authenticated,
                 * the CCCG will respond with a Logon message with SessionStatus (1409) set to 1 = Session Password Changed.
                 * 修改密码成功返回的Message:
                 * 8=FIXT.1.1 9=108 35=A 49=HKEXCCCO 56=CO02099301 34=114 52=20191022-07:29:32.524 1128=9 98=0 108=20 789=2 1409=1 1137=9 464=N 10=091 
                 */
                var sessionStatus = new QuickFix.Fields.SessionStatus();
                if (message.IsSetField(sessionStatus))
                {
                    var val = message.GetField(sessionStatus).getValue();
                    if (val == 1)//是修改密码操作
                    {
                        AlterPassword.Instance.SaveOldPasswords();

                        CfgManager cfgManager = CfgManager.getInstance("StockAdapterHKEX.exe");
                        string[] user_passwd = cfgManager.SessionAndPsw.Split(',');
                        //ZDLogger logger = ZDLoggerFactory.getSynWriteLogger("StockAdapterHKEX.log");
                        Log logger = LogManager.GetLogger("StockAdapterHKEX");

                        var newPasswd = AlterPassword.Instance.UsedPasswords.First().Password;

                        string newSessionAndPsw = $"{user_passwd[0]},{newPasswd}";
                        //cfgManager.UpdateConfig("SessionAndPsw", newSessionAndPsw, "StockAdapterHKEX.exe.config");
                        //cfgManager.UpdateConfigCompatibility("SessionAndPsw", newSessionAndPsw, "StockAdapterHKEX.exe.config");
                        cfgManager.SessionAndPsw = newSessionAndPsw;
                        cfgManager.save();
                        logger.WriteLog( $"修改密码成功:旧密码{user_passwd[1]}-->新密码:{newPasswd}");
                        cfgManager.SessionAndPsw = newSessionAndPsw;
                    }
                }
                #endregion
            }
            if (TradeServerFacade.getCommuServer() != null)
            {
                if (QuickFix.Fields.MsgType.LOGON == msgType)
                {
                    TradeServerFacade.getCommuServer().onUpperEvent(9000, "session logon@0");
                }
                else if (QuickFix.Fields.MsgType.LOGOUT == msgType)
                {
                    TradeServerFacade.getCommuServer().onUpperEvent(9000, "session logout@1");
                }
            }

            HKEXFixStrategy.nextExpectedMsgSeqNum = message.Header.GetInt(Tags.MsgSeqNum);
        }

        public void ToAdmin(QuickFix.Message message, QuickFix.SessionID sessionID)
        {
            _strategy.ProcessToAdmin(message, QuickFix.Session.LookupSession(sessionID));
            string msgType = message.Header.GetField(Tags.MsgType);
            if (QuickFix.Fields.MsgType.LOGON == msgType || QuickFix.Fields.MsgType.LOGOUT == msgType)
            {
                //CfgManager cfgInstance = CfgManager.getInstance("ZDTradeClientTT.exe");
                //message.Header.SetField(new SenderSubID(cfgInstance.SenderSubID));
                //AuditTrailMgr.addMsg(message, null);
            }

            Console.WriteLine("## ToAdmin: " + message.ToString());
        }

        /// <summary>
        /// 所有上手返回的信息都会先进入此方法，然后Crack反射执行注册进来的（OnMessage）方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sessionID"></param>
        public void FromApp(QuickFix.Message message, QuickFix.SessionID sessionID)
        {
            HKEXFixStrategy.nextExpectedMsgSeqNum = message.Header.GetInt(Tags.MsgSeqNum);

            //Console.WriteLine("## FromApp: " + message.ToString());
            try
            {
                //AuditTrailMgr.addMsg(AuditTrailMgr.S7_FROM_CME, message);
                Crack(message, sessionID);
            }
            catch (Exception e)
            {
                TradeServerFacade.writeLog(LogLevel.SYSTEMERROR, e.ToString());
            }
        }

        public void ToApp(QuickFix.Message message, QuickFix.SessionID sessionId)
        {
            try
            {
                _strategy.ProcessToApp(message, QuickFix.Session.LookupSession(sessionId));
                //AuditTrailMgr.addMsg(AuditTrailMgr.S7_TO_CME, message);
                //Console.WriteLine("## ToApp: " + message.ToString());
            }
            catch (Exception e)
            {
                TradeServerFacade.writeLog(LogLevel.SYSTEMERROR, e.ToString());
            }
        }



        public void OnLogon(QuickFix.SessionID sessionID)
        {
            this.ActiveSessionID = sessionID;
            //AuditTrailMgr.fixSessionID = sessionID;

            Console.WriteLine(String.Format("==OnLogon: {0}==", this.ActiveSessionID.ToString()));
            if (LogonEvent != null)
                LogonEvent(this, new EventArgs());
        }

        public void OnLogout(QuickFix.SessionID sessionID)
        {
            // not sure how ActiveSessionID could ever be null, but it happened.
            string a = (this.ActiveSessionID == null) ? "null" : this.ActiveSessionID.ToString();
            Console.WriteLine(String.Format("==OnLogout: {0}==", a));
            if (LogoutEvent != null)
                LogoutEvent(this, new EventArgs());
        }
        #endregion

        
        public event EventHandler<ExectutionEventArgs> ExecutionEvent;
        public event EventHandler<OrderCancelRejectEventArgs> OrderCacnelRejectEvent;
        public event EventHandler<BusinessMessageRejectEventArgs> BusinessMessageRejectEvent;
        public event EventHandler<UserResponseEventArgs> UserResponseEvent;

        public void OnMessage(QuickFix.FIX50.ExecutionReport msg, QuickFix.SessionID s)
        {
            if (ExecutionEvent != null)
                ExecutionEvent(this, new ExectutionEventArgs(msg));
        }

        public void OnMessage(QuickFix.FIX50.OrderCancelReject msg, QuickFix.SessionID s)
        {
            if (OrderCacnelRejectEvent != null)
                OrderCacnelRejectEvent(this, new OrderCancelRejectEventArgs(msg));
        }

        public void OnMessage(QuickFix.FIX50.BusinessMessageReject msg, QuickFix.SessionID s)
        {
            if (BusinessMessageRejectEvent != null)
                BusinessMessageRejectEvent(this, new BusinessMessageRejectEventArgs(msg));
        }

        public void OnMessage(QuickFix.FIX50.UserResponse msg, QuickFix.SessionID s)
        {
            if (UserResponseEvent != null)
                UserResponseEvent(this, new UserResponseEventArgs(msg));
        }

    }

}
