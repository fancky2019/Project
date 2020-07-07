using CommonClassLib;
using Demos.OpenResource.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TT.Common;
using TTMarketAdapter;
using TTMarketAdapter.Model;
using TTMarketAdapter.Utilities;

namespace ZDTradeClientTT
{
    public partial class FrmTradeClient : Form, CommunicationServer
    {
        public FrmTradeClient()
        {
            InitializeComponent();
        }


        private TTCommunication globexCommu = null;
        //private ICustomFixStrategy fixStrategy = null;

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                globexCommu.disconnectGlobex(() =>
                {
                    if (globexCommu != null)
                    {
                        globexCommu.shutdown();
                    }
                });
                //this.BeginInvoke((MethodInvoker)(() =>
                //{
                //    this.btnStart.Enabled = true;
                //    btnStop.Enabled = false;
                //    btnOrderForm.Enabled = false;
                //}));
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Info(ex.ToString());
            }
            finally
            {
                this.btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnOrderForm.Enabled = false;
            }


        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //fixStrategy = new CMEFixStrategy();
            //CfgManager cfgManager = CfgManager.getInstance(null);
            TradeServerFacade.setCommuServer(this);
            globexCommu = new TTCommunication();
            //  globexCommu.ConfigFile = @"config\Quickfix_ZDTradeClient.cfg";
            globexCommu.init(ckTestMode.Checked);
            globexCommu.connectGlobex(() =>
            {

                this.BeginInvoke((MethodInvoker)(() =>
                {
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    btnOrderForm.Enabled = true;
                }));

            });
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //CfgManager cfgManager = CfgManager.getInstance("ZDTradeClientTT.exe");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.btnStop.Enabled)
            {
                for (int i = 1; i <= 3; i++)
                {
                    this.btnStop.BackColor = Color.Red;
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(100);
                    this.btnStop.BackColor = Color.Transparent;
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(100);
                }
                e.Cancel = true;
                return;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Process current = Process.GetCurrentProcess();

            //ProcessThreadCollection allThreads = current.Threads;
            //var actived = allThreads.Cast<ProcessThread>().Where(t => t.ThreadState == System.Diagnostics.ThreadState.Running).ToList();


            //Logger.Dispose();
            Environment.Exit(0);
        }

        //delegate void ControlCallback();

        //private void onConnect()
        //{
        //    if (btnStartCMEClient.InvokeRequired)
        //    {
        //        ControlCallback d = new ControlCallback(onConnect);
        //        btnStartCMEClient.Invoke(d);
        //        //btnStopCMEClient.Invoke(d);
        //    }
        //    else
        //    {
        //        btnStartCMEClient.Enabled = false;
        //        btnStopCMEClient.Enabled = true;
        //    }
        //}

        private OrderForm orderForm = null;
        private void btnOrderForm_Click(object sender, EventArgs e)
        {
            if (orderForm == null || orderForm.IsDisposed)
            {
                orderForm = new OrderForm(globexCommu);
            }

            orderForm.Show();
        }



        #region  实现CommunicationServer接口:将执行结果显示到listbox上

        public void writeLog(int logLevel, string logContent)
        {
            //server.writeLog(logLevel, logContent);
            TT.Common.NLogUtility.Info(logLevel + ":\t" + logContent);
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
            orderForm.lbMsgs_addItem(code + ": " + msg);
        }

        public EventTriggerV3 getEventTrigger()
        {
            return null;
        }
        #endregion



        private string JsonStringFormat(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

        private string JsonSerializeObject(Object obj)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringWriter textWriter = new StringWriter();
            JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            serializer.Serialize(jsonWriter, obj);
            return textWriter.ToString();
        }



        private void btnTest_Click(object sender, EventArgs e)
        {
            // 52 = 20200623 - 05:05:55.931
            //var timeStr = "20200623-05:05:55.931";
            //DateTime contractDate = DateTime.ParseExact(timeStr, "yyyyMMdd-HH:mm:ss.fff", CultureInfo.InvariantCulture);
            //contractDate = contractDate.AddHours(8);
            //var da = contractDate.Date;
            //return;


            return;
            NLogUtility.Debug("Debug1");
            NLogUtility.Info("NLogDemo info ");
            NLogUtility.Info("info2");
            NLogUtility.Warn("Warn3");
            try
            {
                int m = int.Parse("m");
            }
            catch (Exception ex)
            {
                NLogUtility.Error(ex.ToString());
            }
            TT.Common.NLogUtility.Info("UpdateMarketData(),existList.Count == 0,没有找到品种！");

            return;
            //string orderStr = @"ORDER001@20190903000014@00004314K1000006@A000916@@20675@LME@@A000916@&MAREX_JS@@20675@888888@C@LME@NI3M@2@1@18375@@1@@@0.0@2@1@@0@0@NI@3M";
            string orderStr = @"OrdeStHK@00000@00013622JJ000001@8206120@@20125191002@KRX@@8206120@@&Ebest_C@M@20125191002@ebest89@C@KRX@005930.KR@1@3@49150@@1@@@0.0@1@0@@@@0";
            NetInfo ni = new NetInfo();
            ni.MyReadString(orderStr);
            OrderInfo info = new OrderInfo();
            info.MyReadString(ni.infoT);
            var niJson = Newtonsoft.Json.JsonConvert.SerializeObject(ni);
            var oiJson = Newtonsoft.Json.JsonConvert.SerializeObject(info);

            var formatNetInfoStr = JsonSerializeObject(ni);
            var formatOrderInfoStr = JsonSerializeObject(info);

            return;

            int num = 0;
            //string messageLogFileName_ = "FIX.4.2-ZDDEV_SD-TT_PRICE.2019-08-01.messages.current";
            string messageLogFileName_ = "FIX.4.2-ZDDEV_SD-TT_PRICE.2019-08-01 16:01.messages.current";
            string logDateStr = messageLogFileName_.Substring(messageLogFileName_.Length - 33, 16);

            string todayMinStr = DateTime.Now.ToString("yyyy-MM-dd HH-mm");

            //string nowDateStr = DateTime.Now.ToString("yyyy-MM-dd");
            if (logDateStr != todayMinStr)
            {
                messageLogFileName_ = messageLogFileName_.Replace(logDateStr, todayMinStr);
            }
            System.Threading.Timer timer = new System.Threading.Timer((param) =>
            {
                Console.WriteLine(num++);
            }, null, 2 * 60 * 1000, 1 * 60 * 60 * 1000);


            return;
            string str21 = DateTime.Now.ToShortDateString();
            string str11 = DateTime.Now.ToString("yyyy-MM-dd");
            decimal dt = decimal.Parse("96.970");
            double dd = (double)dt;
            var n = 0;
            //   BRN Jul19--->BRN1907
            //var re = ZDTradeClientCommon.GetContract("BRN Jul19", "ICE");
            //var str = "201908";
            //DateTime contractDate = DateTime.ParseExact(str, "yyyyMM", CultureInfo.InvariantCulture);
            //var contract = "DX_S1806-1809";
            //var _sIndex = contract.IndexOf("_S");
            //var lefLeg = contract.Substring(_sIndex + 2, 4);
            //var rightLeg = contract.Substring(_sIndex + 2 + 4 + 1, 4);
            ////DX_S1806-1809 
            ////BRN1908
            ////匹配MLEG
            //var pattern = @".+(_S)\d{4}-\d{4}";
            //var r1 = Regex.IsMatch("BRN1908", pattern);
            //var r2 = Regex.IsMatch("DX_S1806-1809", pattern);
            //int m = 0;




            ////20180529-08:16:57.521
            //string pattern = @"\d{8}-\d{2}";
            //var b = Regex.IsMatch("20180529-08:16:57.521", pattern);


            //QuickFix.FIX42.NewOrderSingle[] newOSArr = new QuickFix.FIX42.NewOrderSingle[1000];
            //RefObj[] refObj = new RefObj[1000];

            //DateTime dtBase = DateTime.Now;

            //for (int i = 0; i < 1000; i++)
            //{
            //    long clOrdID = ClOrderIDGen.getNextClOrderID();

            //    refObj[i] = new RefObj();

            //    QuickFix.FIX42.NewOrderSingle oneOrder = new QuickFix.FIX42.NewOrderSingle();
            //    newOSArr[i] = oneOrder;
            //    oneOrder.SetField(new QuickFix.Fields.ClOrdID());
            //    oneOrder.SetField(new QuickFix.Fields.HandlInst());
            //    oneOrder.SetField(new QuickFix.Fields.Symbol());
            //    oneOrder.SetField(new QuickFix.Fields.Side());
            //    oneOrder.SetField(new QuickFix.Fields.TransactTime());
            //    oneOrder.SetField(new QuickFix.Fields.OrdType());

            //    oneOrder.SetField(new QuickFix.Fields.Account());
            //    oneOrder.SetField(new QuickFix.Fields.OrderQty());
            //    oneOrder.SetField(new QuickFix.Fields.Price());
            //    oneOrder.SetField(new QuickFix.Fields.TimeInForce());
            //    oneOrder.SetField(new QuickFix.Fields.ManualOrderIndicator());
            //    oneOrder.SetField(new QuickFix.Fields.StopPx());
            //    oneOrder.SetField(new QuickFix.Fields.MinQty());
            //    oneOrder.SetField(new QuickFix.Fields.SecurityType());
            //    oneOrder.SetField(new QuickFix.Fields.CustomerOrFirm());
            //    oneOrder.SetField(new QuickFix.Fields.ExpireDate());
            //    oneOrder.SetField(new ZDTradeClient.CtiCode());
            //    oneOrder.SetField(new QuickFix.Fields.MaturityMonthYear());
            //    oneOrder.SetField(new QuickFix.Fields.CFICode());


            //}

            //double milSeconds = DateTime.Now.Subtract(dtBase).TotalMilliseconds;
            //Console.WriteLine("It take {0} mili-seconds!", milSeconds);

            //*************************************
            //string msgStr = "8=FIX.4.2|9=195|35=D|34=53|49=7G9100N|50=rainer|52=20120419-06:20:46.453|56=CME|57=G|142=CN|1=account1|11=15780321875000|21=1|38=30|40=2|44=12.5|54=1|55=90|59=0|60=20120419-06:20:33.859|107=0EJM2|167=FUT|1028=Y|10=053|";
            //msgStr = msgStr.Replace('|', (char)1);
            //QuickFix.FIX42.Message msg = new QuickFix.FIX42.NewOrderSingle();
            //msg.FromString(msgStr, false, null, null);
            //int a = 0;


            //*************************************
            //CMEMarketAdapter.CodeTransfer_CME.initCommuSvrSecurity(@"E:\ZDSVN\DevelopmentDepartment\Source\CMEClient\CMEMarketAdapter\bin\Release\config\ZD_secdef.dat");
            //globexCommu.isTestMode = true;
            //globexCommu.strategy = fixStrategy;

            //Console.WriteLine(ClOrderIDGen.getNextClOrderID());
            //QuickFix.FIX42.Message msg = new QuickFix.FIX42.NewOrderSingle();
            //AuditTrail.AuditTrailMgr.addMsg(msg, null);

            //string temp = TTMarketAdapter.CodeTransfer_TT.getTargetFutures();
            //Dictionary<string, QuickFix.FIX42.SecurityDefinition> zd2TTMapping = TTMarketAdapter.CodeTransfer_TT.zd2TTMapping;


            //*
            //TTMarketAdapter.CodeTransfer_TT.initPrxFactor();


            //globexCommu = new TTCommunication();
            //globexCommu.initOneInstrument();

            /*
            string retStr = TTMarketAdapter.CodeTransfer_TT.toClientPrx(194025, "ES");
            decimal retDec = TTMarketAdapter.CodeTransfer_TT.toGlobexPrx("1940.25", "ES");

            string retStr2 = TTMarketAdapter.CodeTransfer_TT.toClientPrx("1300.09", "GC");
            decimal retDec2 = TTMarketAdapter.CodeTransfer_TT.toGlobexPrx("1300.08", "GC");

            string retStr3 = TTMarketAdapter.CodeTransfer_TT.toClientPrx("1300.09", "CA");
            decimal retDec3 = TTMarketAdapter.CodeTransfer_TT.toGlobexPrx("1300.08", "CA");


            string retStr4 = TTMarketAdapter.CodeTransfer_TT.toClientPrx("1300.09", "ZS");
            decimal retDec4 = TTMarketAdapter.CodeTransfer_TT.toGlobexPrx("1300.08", "ZS");
            */

            //fancky  test

            var str1 = @"8=FIX.4.29=0168735=849=TTORDER56=ZDDEV234=208250=38142=CN52=20190529-09:15:36.112129=LRui37=1dfd5f06-5f95-4b46-8498-99a40e297685198=13002486
526=224004107906527=1dfd5f06-5f95-4b46-8498-99a40e297685:211=800099010011=273453=12448=ICE452=13447=D448=DTS452=220447=D448=TTDTS452=76447=D448=649452=4
447=D448=TT 000452=24447=D448=tt_fix_api3|ZD000021452=44447=D448=ZD000021452=211447=D448=tt_fix_api3452=11447=D448=12345678452=32376=24447=P448=855468
452=1222376=24447=P448=3452=122376=24447=P448=ZD_001452=83447=D17=224004107906:1:Ack20=0150=018=239=01=ZD_00155=BRN48=11938392610675594563167=MLEG
762=Calendar200=201907541=20190731205=31207=ICE100=IFEU461=M15=USD54=138=240=244=1.259=0151=214=06=060=20190529-09:15:36.03582777=O442=31028=Y
18216=2240_38528=R10553=lirui@shzdsoft.com18220=1918221=MC454=5455=BRN FMU0019-BRN FMV0019456=98455=BRN Sep19-Oct19 Calendar456=97455=LCOU9-V9456=5455=5066778
456=8455=IPE e-Brent456=94957=1958=Text TT959=14960=ZD000021555=2600=BRN602=15449698534455326267603=96609=FUT610=201907611=2019073118314=31616=ICE
18100=IFEU608=F624=1623=1556=USD18212=M18224=201909604=5605=BRN FMU0019!606=98605=BRN Sep19606=97605=LCOU9606=5605=222473606=8605=IPE e-Brent606=94
600=BRN602=5860476469386595500603=96609=FUT610=201908611=2019083018314=30616=ICE18100=IFEU608=F624=2623=1556=USD18212=M
18224=201910604=5605=BRN FMV0019!606=98605=BRN Oct19606=97605=LCOV9606=5605=222472606=8605=IPE e-Brent606=9416999=ZD_001
1724=52593=22594=22595=N2594=32595=N16558=ZD00002116561=20190529-09:15:36.04982916117=1016612=LZVqVsgjqCTG94V3eoWzgl10=177";
            var str2 = @"8=FIX.4.29=0179235=849=TTORDER56=ZDDEV234=208350=38142=CN52=20190529-09:15:36.113129=LRui37=1dfd5f06-5f95-4b46-8498-99a40e297685198=13002486
526=224004107906527=1dfd5f06-5f95-4b46-8498-99a40e297685:311=800099010011=273453=12448=ICE452=13447=D448=DTS452=220447=D448=TTDTS452=76447=D448=649452=4
447=D448=TT 000452=24447=D448=tt_fix_api3|ZD000021452=44447=D448=ZD000021452=211447=D448=tt_fix_api3452=11447=D448=12345678452=32376=24447=P448=855468
452=1222376=24447=P448=3452=122376=24447=P448=ZD_001452=83447=D17=1300248820=0150=218=239=21=ZD_00155=BRN48=11938392610675594563167=MLEG762=Calendar
200=201907541=20190731205=31207=ICE100=IFEU461=M15=USD54=138=240=244=1.259=032=231=0.54151=014=26=0.5460=20190529-09:15:36.03582777=O442=31028=Y
18216=2240_38528=R10553=lirui@shzdsoft.com18220=1918221=MC454=5455=BRN FMU0019-BRN FMV0019456=98455=BRN Sep19-Oct19 Calendar456=97455=LCOU9-V9456=5455=5066778
456=8455=IPE e-Brent456=94957=1958=Text TT959=14960=ZD000021555=2600=BRN602=15449698534455326267603=96609=FUT610=201907611=2019073118314=31616=ICE
18100=IFEU608=F624=1623=1556=USD18212=M18224=201909604=5605=BRN FMU0019!606=98605=BRN Sep19606=97605=LCOU9606=5605=222473606=8605=IPE e-Brent
606=94600=BRN602=5860476469386595500603=96609=FUT610=201908611=2019083018314=30616=ICE18100=IFEU608=F624=2623=1556=USD18212=M18224=201910604=5
605=BRN FMV0019!606=98605=BRN Oct19606=97605=LCOV9606=5605=222472606=8605=IPE e-Brent606=9416999=ZD_0011724=58016=201905290915360358270000130024880005066778
851=22593=22594=22595=N2594=32595=N16558=ZD00002116561=20190529-09:15:36.04988216117=1016612=3NEWMaAxYycDWfalbpxgtw16611=1dfd5f06-5f95-4b46-8498-99a40e297685:310=121";
            var str3 = @"8=FIX.4.29=0125935=849=TTORDER56=ZDDEV234=208450=38142=CN52=20190529-09:15:36.113129=LRui37=1dfd5f06-5f95-4b46-8498-99a40e297685198=13002486
526=224004107906527=1300248811=800099010011=273453=12448=ICE452=13447=D448=DTS452=220447=D448=TTDTS452=76447=D448=649452=4447=D448=TT 000452=24447=D
448=tt_fix_api3|ZD000021452=44447=D448=ZD000021452=211447=D448=tt_fix_api3452=11447=D448=12345678452=32376=24447=P448=855468452=1222376=24447=P448=3
452=122376=24447=P448=ZD_001452=83447=D17=1300249120=0150=218=239=21=ZD_00155=BRN48=15449698534455326267167=FUT200=201907541=20190731205=31207=ICE
100=IFEU461=F15=USD18211=M54=138=240=244=1.259=032=231=63.93151=014=26=63.9360=20190529-09:15:36.03582777=O442=21028=Y18216=2240_38528=R
10553=lirui@shzdsoft.com18220=1918221=MC18223=201909454=6455=BRN FMU0019!456=98455=BRN Sep19456=97455=GB00H1JWRL76456=4455=LCOU9456=5455=222473456=8
455=IPE e-Brent456=94957=1958=Text TT959=14960=ZD00002116999=ZD_0011724=58016=201905290915360358270000130024910000222473851=22593=22594=22595=N2594=3
2595=N16558=ZD00002116561=20190529-09:15:36.04989716117=1016612=3iEXnVcL7Al1QrcI4z42Wr16623=116611=1dfd5f06-5f95-4b46-8498-99a40e297685:310=067";
            var str4 = @"8=FIX.4.29=0125835=849=TTORDER56=ZDDEV234=208550=38142=CN52=20190529-09:15:36.114129=LRui37=1dfd5f06-5f95-4b46-8498-99a40e297685198=13002486
526=224004107906527=1300248811=800099010011=273453=12448=ICE452=13447=D448=DTS452=220447=D448=TTDTS452=76447=D448=649452=4447=D448=TT 000452=24447=D
448=tt_fix_api3|ZD000021452=44447=D448=ZD000021452=211447=D448=tt_fix_api3452=11447=D448=12345678452=32376=24447=P448=855468452=1222376=24447=P448=3452=12
2376=24447=P448=ZD_001452=83447=D17=1300249320=0150=218=239=21=ZD_00155=BRN48=5860476469386595500167=FUT200=201908541=20190830205=30207=ICE100=IFEU461=F 15=USD
18211=M54=238=240=244=1.259=032=231=63.39151=014=26=63.3960=20190529-09:15:36.03582777=O442=21028=Y18216=2240_38528=R10553=lirui@shzdsoft.com
18220=1918221=MC18223=201910454=6455=BRN FMV0019!456=98455=BRN Oct19456=97455=GB00H1JWQN26456=4455=LCOV9456=5455=222472456=8455=IPE e-Brent456=94957=1
958=Text TT959=14960=ZD00002116999=ZD_0011724=58016=201905290915360358270000130024930000222472851=22593=22594=22595=N2594=32595=N16558=ZD000021
16561=20190529-09:15:36.04991016117=1016612=92VrV7IX7lq9MJeutve28R16623=116611=1dfd5f06-5f95-4b46-8498-99a40e297685:310=024";
            var str5 = @"8=FIX.4.29=0181335=849=TTORDER56=ZDDEV234=208650=38142=CN52=20190529-09:15:36.115129=LRui37=4dcd4e1e-7e40-48ac-a0e9-5b95b5c20e4b198=13001919
526=224004107418527=4dcd4e1e-7e40-48ac-a0e9-5b95b5c20e4b:3211=800057041=800056910011=179453=12448=ICE452=13447=D448=DTS452=220447=D448=TTDTS452=76447=D448=649
452=4447=D448=TT 000452=24447=D448=tt_fix_api3|TEST452=44447=D448=TEST452=211447=D448=tt_fix_api3452=11447=D448=12345678452=32376=24447=P448=855468
452=1222376=22447=P448=3452=122376=22447=P448=ZD_001452=83447=D17=1300248920=0150=218=239=21=ZD_00155=BRN48=16040928962469124249167=MLEG762=Calendar
200=201905541=20190531205=31207=ICE100=IFEU461=M15=USD54=138=1240=244=1.8259=032=231=1.82151=014=126=1.1783333333333360=20190529-09:15:36.03582777=O
442=31028=N18216=2240_38528=R10553=lirui@shzdsoft.com18220=1918221=MC454=5455=BRN FMN0019-BRN FMU0019456=98455=BRN Jul19-Sep19 Calendar456=97455=LCON9-U9456=5
455=5478827456=8455=IPE e-Brent456=94957=1958=Text TT959=14960=ZD000021555=2600=BRN602=10767719590284635279603=96609=FUT610=201905611=2019053118314=31
616=ICE18100=IFEU608=F624=1623=1556=USD18212=M18224=201907604=5605=BRN FMN0019!606=98605=BRN Jul19606=97605=LCON9606=5605=222468606=8605=IPE e-Brent
606=94600=BRN602=15449698534455326267603=96609=FUT610=201907611=2019073118314=31616=ICE18100=IFEU608=F624=2623=1556=USD18212=M18224=201909604=5605=BRN FMU0019!
606=98605=BRN Sep19606=97605=LCOU9606=5605=222473606=8605=IPE e-Brent606=9416999=ZD_0011724=58016=201905290915360358270000130024890005478827851=12593=22594=22595=N2594=3
2595=N16558=ZD00002116561=20190529-09:15:36.04994916117=1016612=LEEOTdkA4HAEErdPvKd93916611=4dcd4e1e-7e40-48ac-a0e9-5b95b5c20e4b:3210=002";
            var str6 = @"8=FIX.4.29=0126635=849=TTORDER56=ZDDEV234=208750=38142=CN52=20190529-09:15:36.115129=LRui37=4dcd4e1e-7e40-48ac-a0e9-5b95b5c20e4b198=13001919
526=224004107418527=1300248911=800057041=800056910011=179453=12448=ICE452=13447=D448=DTS452=220447=D448=TTDTS452=76447=D448=649452=4447=D448=TT 000
452=24447=D448=tt_fix_api3|TEST452=44447=D448=TEST452=211447=D448=tt_fix_api3452=11447=D448=12345678452=32376=24447=P448=855468452=1222376=22447=P
448=3452=122376=22447=P448=ZD_001452=83447=D17=1300249220=0150=218=239=21=ZD_00155=BRN48=10767719590284635279167=FUT200=201905541=20190531205=31207=ICE 100=IFEU
461=F15=USD18211=M54=138=1240=244=1.8259=032=231=65.75151=014=126=65.7560=20190529-09:15:36.03582777=O442=21028=N18216=2240_38528=R
10553=lirui@shzdsoft.com18220=1918221=MC18223=201907454=6455=BRN FMN0019!456=98455=BRN Jul19456=97455=GB00H1JWRG24456=4455=LCON9456=5455=222468456=8455=IPE e-Brent
456=94957=1958=Text TT959=14960=ZD00002116999=ZD_0011724=58016=201905290915360358270000130024920000222468851=12593=22594=22595=N2594=32595=N16558=ZD000021
16561=20190529-09:15:36.04996016117=1016612=8GTLSJ8Y7Ek87ijJOtN6F416623=116611=4dcd4e1e-7e40-48ac-a0e9-5b95b5c20e4b:3210=173";
            var str7 = @"8=FIX.4.29=0126635=849=TTORDER56=ZDDEV234=208850=38142=CN52=20190529-09:15:36.116129=LRui37=4dcd4e1e-7e40-48ac-a0e9-5b95b5c20e4b198=13001919
526=224004107418527=1300248911=800057041=800056910011=179453=12448=ICE452=13447=D448=DTS452=220447=D448=TTDTS452=76447=D448=649452=4447=D448=TT 000
452=24447=D448=tt_fix_api3|TEST452=44447=D448=TEST452=211447=D448=tt_fix_api3452=11447=D448=12345678452=32376=24447=P448=855468452=1222376=22447=P
448=3452=122376=22447=P448=ZD_001452=83447=D17=1300249120=0150=218=239=21=ZD_00155=BRN48=15449698534455326267167=FUT200=201907541=20190731205=31
207=ICE100=IFEU461=F15=USD18211=M54=238=1240=244=1.8259=032=231=63.93151=014=126=63.9360=20190529-09:15:36.03582777=O442=21028=N18216=2240_38528=R
10553=lirui@shzdsoft.com18220=1918221=MC18223=201909454=6455=BRN FMU0019!456=98455=BRN Sep19456=97455=GB00H1JWRL76456=4455=LCOU9456=5455=222473456=8
455=IPE e-Brent456=94957=1958=Text TT959=14960=ZD00002116999=ZD_0011724=58016=201905290915360358270000130024910000222473851=12593=22594=22595=N2594=32595=N
16558=ZD00002116561=20190529-09:15:36.05000916117=1016612=AKOx0fkgABrDZYeNjpBcrw16623=116611=4dcd4e1e-7e40-48ac-a0e9-5b95b5c20e4b:3210=242";


            List<string> strList = new List<string>() { str2, str3, str4, str5, str6, str7 };

            List<QuickFix.FIX42.ExecutionReport> list = new List<QuickFix.FIX42.ExecutionReport>();
            int i = 0;
            strList.ForEach(p =>
                    {
                        //try
                        //{
                        i++;
                        //QuickFix.FIX42.Message msg = new QuickFix.FIX42.ExecutionReport();
                        QuickFix.FIX42.ExecutionReport msg = new QuickFix.FIX42.ExecutionReport();
                        msg.FromString(p, false, null, null);
                        list.Add(msg);
                        //}
                        //catch(Exception ex)
                        //{

                        //}

                    });


            list.ForEach(p =>
            {
                globexCommu.replyFill(p);
            });






















            int a = 0;

        }

        private void btnSelectZDSecurities_Click(object sender, EventArgs e)
        {
            try
            {
                //CMEMarketAdapter.CodeTransfer_CME.selectZDSecuDefFile(this.tbCMESecurityFile.Text);
                MessageBox.Show("成功生成 直达合约文件! ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnOpenDic_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void BtnCreateOrderModelFile_Click(object sender, EventArgs e)
        {
            string fileName = @"config/OrderModel.csv";
            OrderModel.SaveToFile(GlobalData.OrderModelList, fileName);
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            System.Diagnostics.Process.Start(path);
        }

        private void btnNetInfo_Click(object sender, EventArgs e)
        {

            try
            {

                /*       
                 *                                                           ORDER001@20200630000002@000726281T000002@100091@@ZD_001@ICE@@100091@&ZD_001@@ZD_001@888888@C@ICE@BRN2009@1@1@42@@1@@@0.0@1@1@@0@0@BRN@2009@@@@@@@@@@@@0@
                 * 20200630 10:05:22:949 [3] - 192.168.1.114:61333 {(len=152)ORDER001@20200630000002@000726281T000002@100091@@ZD_001@ICE@@100091@&ZD_001@@ZD_001@888888@C@ICE@BRN2009@1@1@42@@1@@@0.0@1@1@@0@0@BRN@2009@@@@@@@@@@@@0@}
                 *20200630 10:05:53:567 [3] - 192.168.1.114:61333 {(len=153)CANCEL01@20200630000002@000726281T000002@100091@@ZD_001@ICE@@@&ZD_001@192.168.1.105@ZD_001@888888@@000726281T000002@1000007@ICE@BRN2009@1@1@@0@@@@C@@@@@@}
                 *20200630 10:05:57:748 [3] - 192.168.1.114:61333 {(len=152)ORDER001@20200630000003@000726281T000003@100091@@ZD_001@ICE@@100091@&ZD_001@@ZD_001@888888@C@ICE@BRN2009@1@1@42@@1@@@0.0@1@1@@0@0@BRN@2009@@@@@@@@@@@@0@}
                 *20200630 10:06:04:784 [3] - 192.168.1.114:61333 {(len=160)MODIFY01@20200629000002@000726281Q000002@100091@@ZD_001@CME@@100091@&ZD_001@@ZD_001@888888@1000009@CME@6A2007@1@1@42.00@0@2@42.00@1@1@C@0.00@0.0@2@@@@@@@@@@@@0@}
                 *20200630 10:06:27:437 [3] - 192.168.1.114:61333 {(len=160)MODIFY01@20200629000002@000726281Q000002@100091@@ZD_001@CME@@100091@&ZD_001@@ZD_001@888888@1000009@CME@6A2007@1@1@42.00@0@2@42.00@1@1@C@0.00@0.0@2@@@@@@@@@@@@0@}
                 */
                //var netInfoStr = "ORDER001@00055257I9000002@00064055U9000952@006380@@LME30225@XEurex@@006380@&FCS_C_TT_B@003656@LME30225@888888@C@XEurex@FDXM2006@1@1@0.0@@4@@@12726@1@1@@0@0@FDXM@2006@@@@@@@@@@@@@";
                //var netInfoStr = "ORDER001@20200630000002@000726281T000002@100091@@ZD_001@ICE@@100091@&ZD_001@@ZD_001@888888@C@ICE@BRN2009@1@1@42@@1@@@0.0@1@1@@0@0@BRN@2009@@@@@@@@@@@@0@";
                //var netInfoStr = "CANCEL01@20200630000002@000726281T000002@100091@@ZD_001@ICE@@@&ZD_001@192.168.1.105@ZD_001@888888@@000726281T000002@1000007@ICE@BRN2009@1@1@@0@@@@C@@@@@@";
                //var netInfoStr = "MODIFY01@20200629000002@000726281Q000002@100091@@ZD_001@CME@@100091@&ZD_001@@ZD_001@888888@1000009@CME@6A2007@1@1@42.00@0@2@42.00@1@1@C@0.00@0.0@2@@@@@@@@@@@@0@";

                var netInfoStr = this.txtNetInfo.Text.Trim();

                NetInfo netInfo = new NetInfo();
                netInfo.MyReadString(netInfoStr);
                //if(netInfo.MyToString()== netInfoStr)
                //{

                //}
                StringBuilder sb = new StringBuilder();
                sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(netInfo));
                sb.Append("\r\n");
                var command = netInfoStr.Substring(0, 8);
                switch (command)
                {
                    case "ORDER001":
                        OrderInfo orderInfo = new OrderInfo();
                        orderInfo.MyReadString(netInfo.infoT);
                        sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(orderInfo));
                        break;
                    case "CANCEL01":
                        CancelInfo cancelInfo = new CancelInfo();
                        cancelInfo.MyReadString(netInfo.infoT);
                        sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(cancelInfo));
                        break;
                    case "MODIFY01":
                        ModifyInfo modifyInfo = new ModifyInfo();
                        modifyInfo.MyReadString(netInfo.infoT);
                        sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(modifyInfo));
                        break;
                    default:
                        MessageBox.Show("订单指令有误！");
                        return;
                }

                string fileName = @"NetInfo.data";
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                TxtFile.SaveTxtFile(path, new List<string>() { sb.ToString() });
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }

        }
    }



}
