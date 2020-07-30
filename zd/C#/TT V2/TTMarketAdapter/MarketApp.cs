using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix.Fields;
using AuthCommon;
using QuickFix.FIX42;
using System.Collections.Concurrent;
using System.Threading;
using System.Configuration;
using TT.Common;

namespace TTMarketAdapter
{
    /// <summary>
    /// TT通信类，接收TT发送过来的合约定义（合约数据）、快照、增量数据（结算价）
    /// </summary>
    public class MarketApp : QuickFix.MessageCracker, QuickFix.IApplication
    {
        public event EventHandler<EventArgs> LogonEvent;
        public event EventHandler<EventArgs> LogoutEvent;
        /// <summary>
        /// 接收合约委托回调
        /// </summary>
        public event Action<SecurityDefinition> SecurityDefEvent;

        private BlockingCollection<QuickFix.FIX42.MarketDataIncrementalRefresh> inceRefreshBC = null;
        public QuickFix.SessionID ActiveSessionID { get; set; }
        public QuickFix.SessionSettings MySessionSettings { get; set; }
        //  private ZDLogger errorLogger = null;

        object _lockObj = new object();
        private QuickFix.IInitiator _initiator = null;
        public QuickFix.IInitiator Initiator
        {
            set
            {
                if (_initiator == null)
                {
                    lock (_lockObj)
                    {
                        if (_initiator == null)
                        {
                            _initiator = value;
                        }
                    }
                }
            }
            get
            {
                return _initiator;
            }
        }

        public MarketApp(QuickFix.SessionSettings settings)
        {

            ActiveSessionID = null;
            MySessionSettings = settings;

            inceRefreshBC = new BlockingCollection<MarketDataIncrementalRefresh>();
        }



        public void Start()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(incrRefreshHandler));
            TT.Common.NLogUtility.Info("MarketApp::Start() called");
            if (Initiator.IsStopped)
                Initiator.Start();
            else
                TT.Common.NLogUtility.Info("(already started)");
        }

        /// <summary>
        /// 处理接收的增量数据
        /// </summary>
        /// <param name="stateInfo"></param>
        public void incrRefreshHandler(object stateInfo)
        {
            try
            {
                while (!inceRefreshBC.IsCompleted)
                {
                    if (!OrderBookMgr.Instance.InitCompleted)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    //MarketDataIncrementalRefresh incRefresh;
                    //if (inceRefreshBC.TryTake(out incRefresh, 500))
                    //{
                    //    try
                    //    {
                    //        OrderBookMgr.Instance.processIncrementalRefresh(incRefresh);
                    //    }
                    //    catch (Exception exi)
                    //    {
                    //        TT.Common.Log.Error<MarketApp>("Process fail: " + incRefresh.ToString());
                    //        TT.Common.Log.Error<MarketApp>(exi.ToString());
                    //    }
                    //}

                    foreach (var incRefresh in inceRefreshBC.GetConsumingEnumerable())
                    {
                        try
                        {
                            OrderBookMgr.Instance.processIncrementalRefresh(incRefresh);
                        }
                        catch (Exception exi)
                        {
                            TT.Common.NLogUtility.Error("Process fail: " + incRefresh.ToString());
                            TT.Common.NLogUtility.Error(exi.ToString());
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
                //TT.Common.Log.Error<MarketApp>(ex.StackTrace);
            }
        }

        public void Stop()
        {
            TT.Common.NLogUtility.Info("MarketApp::Stop() called");
            Initiator.Stop();

            try
            {
                inceRefreshBC.CompleteAdding();
                //inceRefreshBC = null;
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Tries to send the message; throws if not logged on.
        /// </summary>
        /// <param name="m"></param>
        public bool Send(QuickFix.Message m)
        {
            //if (Initiator.IsLoggedOn() == false)
            if (!Initiator.IsLoggedOn)
                throw new Exception("Can't send a message.  We're not logged on.");
            if (ActiveSessionID == null)
                throw new Exception("Can't send a message.  ActiveSessionID is null (not logged on?).");

            return QuickFix.Session.SendToTarget(m, this.ActiveSessionID);
        }


        #region IApplication Members
        private bool isMessageOfType(QuickFix.Message message, String type)
        {
            try
            {
                return type == message.Header.GetField(Tags.MsgType);
            }
            catch (Exception e)
            {
                TT.Common.NLogUtility.Error(e.ToString());
                return false;
            }
        }

        public void FromAdmin(QuickFix.Message message, QuickFix.SessionID sessionID)
        {
            TT.Common.NLogUtility.Debug("## FromAdmin: " + message.ToString());
            //string msgType = message.Header.GetField(Tags.MsgType);
            //if (QuickFix.Fields.MsgType.LOGON == msgType ||
            //    QuickFix.Fields.MsgType.LOGOUT == msgType ||
            //    QuickFix.Fields.MsgType.REJECT == msgType)
            //{
            //    //AuditTrailMgr.addMsg(message, null);
            //}
        }

        public void ToAdmin(QuickFix.Message message, QuickFix.SessionID sessionID)
        {
            //string msgType = message.Header.GetField(Tags.MsgType);
            //设置登录信息：从配置文件读取配置密码，账号别名
            if (isMessageOfType(message, MsgType.LOGON))
            {
                //CfgManager cfgManager = CfgManager.getInstance(null);
                if (!string.IsNullOrEmpty(Configurations.Instance.SessionAndPsw))
                {
                    string password = Configurations.Instance.SessionAndPsw.Split(',')[1];
                    message.SetField(new RawDataLength(password.Length));
                    message.SetField(new RawData(password));
                    message.SetField(new HeartBtInt(55));
                }
                message.Header.SetField(new OnBehalfOfSubID(Configurations.Instance.OnBehalfOfSubID));
            }

            TT.Common.NLogUtility.Debug("## ToAdmin: " + message.ToString());
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

                //var msgSeqNum = message.Header.GetField(Tags.MsgSeqNum);
                //var strFromApp = $"FromApp {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} MsgSeqNum:{msgSeqNum}";
                //BufferLog.LogAsync(strFromApp);
                Crack(message, sessionID);
                //var strCrack = $"Crack {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} MsgSeqNum:{msgSeqNum}";
                //BufferLog.LogAsync(strCrack);
            }
            catch (Exception e)
            {

                TT.Common.NLogUtility.Error(e.ToString() + Environment.NewLine + message.ToString());
            }
        }

        public void ToApp(QuickFix.Message message, QuickFix.SessionID sessionId)
        {
            //try
            //{
            //    //AuditTrailMgr.addMsg(AuditTrailMgr.S7_TO_CME, message);
            //    //Console.WriteLine("## ToApp: " + message.ToString());
            //}
            //catch (Exception e)
            //{
            //    TT.Common.Log.Error<MarketApp>(e.ToString());
            //}
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
                LogonEvent(this, new EventArgs());
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


        /// <summary>
        /// 合约定义--合约数据  35=d
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>
        public void OnMessage(QuickFix.FIX42.SecurityDefinition msg, QuickFix.SessionID s)
        {
            SecurityDefEvent?.Invoke(msg);
        }


        /// <summary>
        /// 快照35=w
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>

        public void OnMessage(QuickFix.FIX42.MarketDataSnapshot msg, QuickFix.SessionID s)
        {
            OrderBookMgr.Instance.processFullRefresh(msg);
        }


        /// <summary>
        /// 增量35=x
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>
        public void OnMessage(QuickFix.FIX42.MarketDataIncrementalRefresh msg, QuickFix.SessionID s)
        {
            //OrderBookMgr.Instance.processIncrementalRefresh(msg);
            inceRefreshBC.Add(msg);
        }


        /// <summary>
        /// 合约拒绝 35=Y
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>
        public void OnMessage(QuickFix.FIX42.MarketDataRequestReject msg, QuickFix.SessionID s)
        {
            TT.Common.NLogUtility.Error(msg.ToString());
        }

        /// <summary>
        /// News 提示信息(35=B)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="s"></param>
        public void OnMessage(QuickFix.FIX42.News msg, QuickFix.SessionID s)
        {

        }
    }
}
