using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonClassLib;


namespace ZDTradeCommunication
{
    public class TtTrade : TradeInterface
    {

        private  CommunicationServer communicationServer = null;
        private ZDTradeClientTT.TTCommunication ttCommu = null;
        private UIBridge uiBridge = null;

        public void init(CommunicationServer communicationServer, UIBridge uiBridge)
        {
            this.communicationServer = communicationServer;
            ZDTradeClientTT.TradeServerFacade.setCommuServer(communicationServer);
            this.uiBridge = uiBridge;

            ttCommu = new ZDTradeClientTT.TTCommunication();
            ttCommu.ConfigFile = @"config\quickfix.cfg";
            bool isTestMode = false;
            ttCommu.init(new ZDTradeClientTT.CMEFixStrategy(), isTestMode);

            ttCommu.tradeApp.LogonEvent += new EventHandler<EventArgs>(tradeApp_LogonEvent);
            ttCommu.tradeApp.LogoutEvent += new EventHandler<EventArgs>(tradeApp_LogoutEvent);

        }

        

        private void tradeApp_LogonEvent(object sender, EventArgs e)
        {
            uiBridge.setStateInfo("iLink interface logon");
            //EventDesc@SeverityLevel
            communicationServer.onUpperEvent(90000, "Fix session logon@0");
        }

        private void tradeApp_LogoutEvent(object sender, EventArgs e)
        {
            uiBridge.setStateInfo("iLink interface logout");
            //EventDesc@SeverityLevel
            communicationServer.onUpperEvent(90000, "Fix session logout@1");
        }

        public void connectCME()
        {
            ttCommu.connectGlobex();
        }

        public void disconnectCME()
        {
            ttCommu.disconnectGlobex();
        }

        public void start()
        {
            ttCommu.connectGlobex();
        }

        public void stop()
        {
            if (SwitchPannel.ilink_isInDayHalted)
                ttCommu.persistDayRefObj();

            disconnectCME();
            ttCommu.shutdown();
        }

        public void openConfigForm()
        {
        }

        public string getDisplayInfo()
        {
            StringBuilder sb = new StringBuilder();

            ZDTradeClientTT.CfgManager cfgManager = ZDTradeClientTT.CfgManager.getInstance("ZDTradeClientTT.exe");
            string sessionCfg = cfgManager.SessionAndPsw;
            if(sessionCfg != null && sessionCfg.Length > 3)
            {

                string clearFirm = "Unkown";
                try
                {
                    clearFirm = cfgManager.ClearFirm;
                }
                catch (Exception ex) { }

                string[] arrStr = cfgManager.SessionAndPsw.Split(',');
                string sessionID = arrStr[0];
                sb.Append("配置文件：ZDTradeClientTT.exe.config").Append("\r\n")
                    .Append("Clear Firm:").Append(clearFirm).Append("\r\n")
                    .Append("Session ID: ").Append(sessionID);
            }

            return sb.ToString();
        }

        public void doOrder(NetInfo netInfo)
        {
            try
            {
                //下单信息
                if (netInfo.code == CommandCode.ORDER)
                {
                    //下单
                    ttCommu.PlaceOrder(netInfo);
                }
                else if (netInfo.code == CommandCode.CANCEL)//撤单信息
                {
                    //撤单
                    ttCommu.CancelOrder(netInfo);
                }
                else if (netInfo.code == CommandCode.MODIFY)
                {
                    ttCommu.CancelReplaceOrder(netInfo);
                }
            }
            catch (Exception ex)
            {
                communicationServer.writeLog(AuthCommon.ZDLogger.LVL_ERROR, ex.ToString());
            }
        }

        public bool handleUpperEvent(int code, string msg)
        {
            return false;
        }
    }
}
