using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StockAdapterHKEX;
using CommonClassLib;
using System.Runtime.InteropServices;
using AuthCommon;
using QuickFix.FIX50;
using QuickFix.Fields;
using System.Collections.Concurrent;
using StockAdapterHKEX.Common;
using System.Threading.Tasks;
using System.Threading;

namespace StockAdapterHKEX
{
    public partial class Form1 : Form, CommunicationServer
    {
        [DllImport("Kernel32.dll")]
        static extern Boolean AllocConsole();
        public Form1()
        {
            if (!AllocConsole())
                MessageBox.Show("Failed");

            InitializeComponent();

        }

        private HKEXCommunication globexCommu = null;
        private ICustomFixStrategy fixStrategy = null;

        private void btnStart_Click(object sender, EventArgs e)
        {
            fixStrategy = new HKEXFixStrategy();
            CfgManager cfgManager = CfgManager.getInstance(null);


            TradeServerFacade.setCommuServer(this);
            globexCommu = new HKEXCommunication();
            globexCommu.ConfigFile = @"config\quickfix.cfg";

            globexCommu.init(fixStrategy, cbTestMode.Checked);
            globexCommu.connectGlobex();
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            globexCommu.disconnectGlobex();
        }


        delegate void ControlCallback();

        private void onConnect()
        {
            if (btnStart.InvokeRequired)
            {
                ControlCallback d = new ControlCallback(onConnect);
                btnStart.Invoke(d);
                //btnStop.Invoke(d);
            }
            else
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
        }

        private OrderForm orderForm = null;
        private void btnOrderForm_Click(object sender, EventArgs e)
        {
            if (orderForm == null || orderForm.IsDisposed)
            {
                orderForm = new OrderForm(globexCommu);
                TradeServerFacade.setCommuServer(this);
            }

            orderForm.Show();
        }

        public void writeLog(int logLevel, string logContent)
        {
            //server.writeLog(logLevel, logContent);
            Console.WriteLine(logLevel + ":\t" + logContent);
        }

        public void SendString(CommonClassLib.NetInfo obj)
        {
            orderForm.lbMsgs_addItem(obj.MyToString());
        }

        public void SendString(string str)
        {
            orderForm.lbMsgs_addItem(str);
        }

        public void onUpperEvent(int code, string msg)
        {
            //  orderForm.lbMsgs_addItem(code + ": " + msg);
        }

        public EventTrigger getEventTrigger()
        {
            return null;
        }


        private void btnTest_Click(object sender, EventArgs e)
        {
            var cfgInstance = CfgManager.getInstance("StockAdapterHKEX.exe");
            var re11111 = cfgInstance.IsPreOpenTime;
            return;
            string str = "00004.HK";
            string re = str.TrimStart('0');
            int mm = 0;
            //Environment.Exit(0);
            //var netInfoStr1 = @"CancStHK@00000@00011150LF000618@8205775@@CO020990001@HKEX@@@@0001261&HKEX_1@@CO020990001@123456@@00011150LF000618@12256072@HKEX@55056.HK@1@20000@0.101000@0@@7@@P@@@@@";
            //NetInfo ni = new NetInfo();
            //ni.MyReadString(netInfoStr1);
            //CancelInfo ci = new CancelInfo();
            //ci.MyReadString(ni.infoT);

            //QuickFix.DataDictionary.DataDictionary dd = new QuickFix.DataDictionary.DataDictionary();
            //dd.Load(@"config/FIX50SP2.xml");
            //string msgStr = @"8=FIXT.1.1|9=318|35=8|49=HKEXCO|56=CO02099001|34=490|52=20190826-01:32:46.669|1128=9|11=11890567|14=100000|17=N.Wr/.S.3c2bCNBA|150=F|31=0.04|32=10000|151=0|1093=2|574=4|453=2|448=9760|447=D|452=17|448=7650|447=D|452=1|37=48398851|38=100000|39=2|40=2|44=0.04|207=XHKG|48=53697|22=8|54=2|59=0|60=20190826-01:32:46.000|880=53697000000005|10=114|";
            //msgStr = msgStr.Replace('|', (char)1);
            //ExecutionReport executionReport = new ExecutionReport();
            //executionReport.FromString(msgStr, false, null, dd);

            //globexCommu.replyFill(executionReport);

            //CodeTransfer_HKEX.init();

            #region 下单
            //var orderInfoStr = @"OrdeStHK@00000@00015270JA000268@8206675@@CO020990001@HKEX@@8206675@@&HKEX_1@@CO020990001@123456@P@HKEX@22521.HK@2@75000@0.186@@7@@@@1@0@@@@@@@@@1";
            //NetInfo netInfoOrder = new NetInfo();
            //netInfoOrder.MyReadString(orderInfoStr);
            //OrderInfo orderInfo = new OrderInfo();
            //orderInfo.MyReadString(netInfoOrder.infoT);
            //globexCommu.doOrder(netInfoOrder);
            //#endregion

            //#region 撤单
            //var cancelOrderInfoStr = @"CancStHK@00000@00015270JA000268@8206675@@CO020990001@HKEX@@@@0007870&HKEX_1@@CO020990001@123456@@00015270JA000268@12816431@HKEX@22521.HK@2@75000@0.186000@0@@7@@P@@@@@";
            //NetInfo cancelOrderNetInfo = new NetInfo();
            //cancelOrderNetInfo.MyReadString(cancelOrderInfoStr);
            //CancelInfo cancelInfo = new CancelInfo();
            //orderInfo.MyReadString(cancelOrderNetInfo.infoT);
            //globexCommu.doOrder(cancelOrderNetInfo);


            //改单
            //var cancelReplaceStr = @"ModiStHK@@system1@@@06984001@HKEX@@@&@2|c x|7533|2@@@10033782@HKEX@17@1@@@@2000@12.500@@7@@@@@@@@@";
            //var cancelReplaceStr = @"ModiStHK@@system1@@@06984001@HKEX@@@&@2|c x|7533|2@@@10033785@HKEX@17@1@@@@2000@12.500@@7@@@@@@@@@";
            //NetInfo cancelReplaceNetInfo = new NetInfo();
            //cancelReplaceNetInfo.MyReadString(cancelReplaceStr);
            //ModifyInfo modifyInfo = new ModifyInfo();
            //modifyInfo.MyReadString(cancelReplaceNetInfo.infoT);


            //撤单返回
            //var cancelResponse1 = "CancStHK@@000053244K000029@000161@00000@456789@HKEX@@000161@&@456789@000053244K000029@10000233@10000233@HKEX@00016.HK@1@3000@118.3@0@0@@7@@2019-12-20@13:26:44@6@@@@@@@";
            //var cancelResponse2 = "CancStHK@@000053244K000030@000161@00000@456789@HKEX@@000161@&@456789@000053244K000030@@@HKEX@00016.HK@1@3000@118.3@0@0@@7@@2019-12-20@13:27:50@6@@@@@@@";
            //NetInfo netInfo1 = new NetInfo();
            //netInfo1.MyReadString(cancelResponse1);
            //CancelResponseInfo cancelResponseInfo1 = new CancelResponseInfo();
            //cancelResponseInfo1.MyReadString(netInfo1.infoT);

            //NetInfo netInfo2 = new NetInfo();
            //netInfo2.MyReadString(cancelResponse2);
            //CancelResponseInfo cancelResponseInfo2 = new CancelResponseInfo();
            //cancelResponseInfo2.MyReadString(netInfo2.infoT);
            int m = 0;
            #endregion

            #region 修改密码
            //for (int i = 1; i < 10; i++)
            //{
            //    if (StockAdapterHKEX.Common.AlterPassword.Instance.CheckShouldAlterPassword(out string newPassword))
            //    {
            //        if (i % 3 != 0)
            //        {
            //            continue;
            //        }
            //        var msgStr = @"8=FIXT.1.1|9=108|35=A|49=HKEXCCCO|56=CO02099301|34=114|52=20191022-07:29:32.524|1128=9|98=0|108=20|789=2|1409=1|1137=9|464=N|10=091|";
            //        msgStr = msgStr.Replace('|', (char)1);
            //        QuickFix.Message message = new QuickFix.Message();
            //        message.FromString(msgStr, false, null, null);
            //        var sessionStatus = new QuickFix.Fields.SessionStatus();
            //        if (message.IsSetField(sessionStatus))
            //        {
            //            var val = message.GetField(sessionStatus).getValue();
            //            if (val == 1)//是修改密码操作
            //            {
            //                AlterPassword.Instance.SaveOldPasswords();

            //                CfgManager cfgManager = CfgManager.getInstance(null);
            //                string[] user_passwd = cfgManager.SessionAndPsw.Split(',');
            //                ZDLogger globexIFLogger = ZDLoggerFactory.getSynWriteLogger("StockAdapterHKEX.log");

            //                var newPasswd = AlterPassword.Instance.UsedPasswords.First().Password;

            //                string newSessionAndPsw = $"{user_passwd[0]},{newPasswd}";
            //                cfgManager.UpdateConfig("SessionAndPsw", newSessionAndPsw, "StockAdapterHKEX.exe.config");
            //                globexIFLogger.log(ZDLogger.LVL_TRACE, $"修改密码成功:旧密码{user_passwd[1]}-->新密码:{newPasswd}");
            //                cfgManager.SessionAndPsw = newSessionAndPsw;
            //            }
            //        }
            //    }
            //}
            #endregion

            #region 修改配置文件

            //  CfgManager cfgManager = CfgManager.getInstance(null);
            //  var newPasswd = AlterPassword.Instance.UsedPasswords.First().Password;

            //  string newSessionAndPsw = $"{cfgManager.SessionAndPsw.Split(',')[0]},{AlterPassword.Instance.GetNewPassword()}";
            ////  cfgManager.UpdateConfigCompatibility("SessionAndPsw", newSessionAndPsw);

            //  cfgManager.SessionAndPsw = newSessionAndPsw;
            //  cfgManager.save();

            //  StockAdapterHKEX.CfgManager cfgInstance = StockAdapterHKEX.CfgManager.getInstance("StockAdapterHKEX.exe");


            //  cfgInstance.ClOrderID = (ClOrderIDGen.getNextClOrderID()+1).ToString();
            //  cfgInstance.save();

            #endregion

            #region TPS
            TPSQueue<NetInfo> tPSQueue = new TPSQueue<NetInfo>(10);
            tPSQueue.Cunsumer(p =>
            {
                //  globexIFLogger.log(ZDLogger.LVL_ERROR, "== dequeue, systemCode=" + p.systemCode);
                //globexIFLogger.log(ZDLogger.LVL_ERROR, $"Dequeue:{p.MyToString()}");
                //SendMsg(p);
            });

            Task.Run(() =>
            {
                Random random = new Random();
                NetInfo netInfo = new NetInfo();
                while (true)
                {
                    netInfo.infoT = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.fff")} Form1 Test";
                    tPSQueue.Producer(netInfo);
                    Thread.Sleep(random.Next(1, 200));
                }
            });
            #endregion

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (globexCommu != null)
                globexCommu.shutdown();
        }

        private void btnRejectTest_Click(object sender, EventArgs e)
        {


            //****************** 
            fixStrategy = new HKEXFixStrategy();
            CfgManager cfgManager = CfgManager.getInstance(null);


            TradeServerFacade.setCommuServer(this);
            globexCommu = new HKEXCommunication();
            globexCommu.ConfigFile = @"config\quickfix.cfg";

            globexCommu.init(fixStrategy, true);





            //8=FIXT.1.1|9=110|35=j|49=HKEXCO|56=CO02099001|34=15724|52=20190418-07:23:23.989|1128=9|45=15557|372=F|379=10712227|380=8|58=10|10=250|
            string msgStr = "8=FIXT.1.1|9=110|35=j|49=HKEXCO|56=CO02099001|34=15724|52=20190418-07:23:23.989|1128=9|45=15557|372=D|379=10712227|380=8|58=10|10=250|";
            msgStr = msgStr.Replace('|', (char)1);
            BusinessMessageReject msg = new BusinessMessageReject();
            msg.FromString(msgStr, false, null, null);
            //&后是infoT
            //CancStHK@00000@00011150LI005221@8205775@@CO020990001@HKEX@@@@0045343&HKEX_1@@CO020990001@123456@@00011150LI005221@10712162@HKEX@66946.HK@1@500000@0.078000@0@@7@@P
            //NetInfo netInfo = new NetInfo();
            //netInfo.MyReadString("CancStHK@00000@00011150LI005221@8205775@@CO020990001@HKEX@@@@0045343&HKEX_1@@CO020990001@123456@@00011150LI005221@10712162@HKEX@66946.HK@1@500000@0.078000@0@@7@@P");

            NetInfo netInfo = new NetInfo();
            //netInfo.MyReadString("CancStHK@00000@00011150LI005221@8205775@@CO020990001@HKEX@@@@0045343&HKEX_1@@CO020990001@123456@@00011150LI005221@10712162@HKEX@66946.HK@1@500000@0.078000@0@@7@@P");
            netInfo.MyReadString("OrdeStHK@00000@00011520LI003818@8205831@@CO020990001@HKEX@@8205831@@&HKEX_1@@CO020990001@123456@P@HKEX@63118.HK@1@50000@0.112@@7@@@@1@0@@@@1");
            OrderInfo info = new OrderInfo();
            info.MyReadString(netInfo.infoT);
            RefObj refOb = new RefObj();
            refOb.lastSendInfo = netInfo;
            //  ConcurrentDictionary<long, RefObj> xReference = new ConcurrentDictionary<long, RefObj>();
            globexCommu.xReference.TryAdd(10712227, refOb);
            refOb.clOrderID = "10712226";
            refOb.addClientReq(msg);
            // test target function
            globexCommu.replyBusinessMessageReject(msg);


        }

        private void BtnNetInfo_Click(object sender, EventArgs e)
        {
            //{(len=140)OrdeStHK@00000@AS000000160648000780@AS00000016@@DU995392@HKEX@@AS00000016@@&b123@@DU995392@123@C@HKEX@00004.HK@1@18000@22.600@@7@@@@1@0@@@@0}
            //OrdeStHK@00000@AS000000160648000780@AS00000016@@DU995392@HKEX@@AS00000016@@&b123@@DU995392@123@C@HKEX@00004.HK@1@18000@22.600@@7@@@@1@0@@@@0
            NetInfo netInfo = new NetInfo();//&号前的字符串
            netInfo.MyReadString("OrdeStHK@00000@AS000000160648000780@AS00000016@@DU995392@HKEX@@AS00000016@@");

        }

        EventTriggerV3 CommunicationServer.getEventTrigger()
        {
            throw new NotImplementedException();
        }

        private void BtnOpenDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
