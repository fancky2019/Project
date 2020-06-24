using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix.Fields;


namespace ZDTradeClientTT
{
    /// <summary>
    /// rainer 壳调用了
    /// </summary>
    public class CMEFixStrategy : ICustomFixStrategy
    {

        //#region ICustomFixStrategy Members

        //public QuickFix.SessionSettings SessionSettings { get; set; }

        //private CfgManager cfgManager = CfgManager.getInstance("ZDTradeClientTT.exe");
         

        //private TargetSubID targetSubID = null;
        ////private SenderLocationID senderLocationID = null;
        ////private ApplicationSystemName appSysName = null;
        ////private TradingSystemVersion tradingSysVersion = null;
        ////private ApplicationSystemVendor appSysVendor = null;
        //// SenderSubID(Tag50 ID) is dependent on order instructions, removed to GlobexCommunication
        //// For admin message
        //private SenderSubID senderSubID = null;

        //public CMEFixStrategy()
        //{
        //    targetSubID = new TargetSubID("G");
        //    //senderLocationID = new SenderLocationID(cfgManager.SenderLocationID);
        //    //appSysName = new ApplicationSystemName(cfgManager.ApplicationSystemName);
        //    //tradingSysVersion = new TradingSystemVersion(cfgManager.TradingSystemVersion);
        //    //appSysVendor = new ApplicationSystemVendor(cfgManager.ApplicationSystemVendor);
        //    senderSubID = new SenderSubID(cfgManager.SenderSubID);
        //}


        //public Dictionary<int, string> DefaultNewOrderSingleCustomFields
        //{ get { return new Dictionary<int, string>(); } }

        //public void ProcessToAdmin(QuickFix.Message msg, QuickFix.Session session)
        //{
        //    //msg.Header.SetField(senderSubID);
        //    msg.Header.SetField(new SendingTime(DateTime.UtcNow));
        //    //msg.Header.SetField(targetSubID);
        //    //msg.Header.SetField(senderLocationID);
            
        //    if (isMessageOfType(msg, MsgType.LOGON))
        //        addLogonField(msg);
        //}
        //public void ProcessToApp(QuickFix.Message msg, QuickFix.Session session)
        //{
        //    //msg.Header.SetField(senderSubID);
        //    msg.Header.SetField(new SendingTime(DateTime.UtcNow));
        //    msg.Header.SetField(targetSubID);
        //    //msg.Header.SetField(senderLocationID);

        //}

        //public void ProcessOrderCancelRequest(QuickFix.FIX42.NewOrderSingle nos, QuickFix.FIX42.OrderCancelRequest msg)
        //{
        //    // Do nothing, logic is placed at GlobexCommnunication.cs
        //}
        //public void ProcessOrderCancelReplaceRequest(QuickFix.FIX42.NewOrderSingle nos, QuickFix.FIX42.OrderCancelReplaceRequest msg)
        //{
        //    // Do nothing, logic is placed at GlobexCommnunication.cs
        //}

        //#endregion

        //private void addLogonField(QuickFix.Message message) 
        //{
        //    //message.Header.SetField(appSysName); //new ApplicationSystemName("DA_TRADING_PLATFORM"));
        //    //message.Header.SetField(tradingSysVersion); //new TradingSystemVersion("V1.0"));
        //    //message.Header.SetField(appSysVendor); //new ApplicationSystemVendor("DA_SOFT"));

        //    string password = cfgManager.SessionAndPsw.Split(',')[1];
        //    message.SetField(new RawDataLength(password.Length));
        //    message.SetField(new RawData(password));
        //    message.SetField(new HeartBtInt(30));
            
        //    //message.SetField(new ResetSeqNumFlag(true));
        //}

        //private bool isMessageOfType(QuickFix.Message message, String type)
        //{
        //    try
        //    {
        //        return type == message.Header.GetField(Tags.MsgType);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        return false; 
        //    }
        //}
    }
}
