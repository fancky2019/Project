using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonClassLib;
using QuickFix.Fields;

namespace ZDTradeClientTT
{
    public partial class OrderForm : Form
    {

        private TTCommunication globexCommu = null;
        private Dictionary<string, string> orderTypeDict = new Dictionary<string, string>();
        private Dictionary<string, string> orderSideDict = new Dictionary<string, string>();
        private Dictionary<string, string> tifDict = new Dictionary<string, string>();
        //CfgManager _cfgManager;
        public OrderForm(TTCommunication globexCommu)
        {
            InitializeComponent();
            this.globexCommu = globexCommu;
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            //_cfgManager = CfgManager.getInstance("ZDTradeClientTT.exe");
            orderTypeDict.Add("MARKET", "1");
            orderTypeDict.Add("LIMIT", "2");
            orderTypeDict.Add("STOP", "3");
            orderTypeDict.Add("STOP_LIMIT", "4");
            orderTypeDict.Add("MARKET_ON_CLOSE", "5");
            orderTypeDict.Add("LIMIT_ON_CLOSE", "B");
            orderTypeDict.Add("MARKET_WITH_LEFT_OVER_AS_LIMIT", "K");
            orderTypeDict.Add("MARKET_LIMIT_MARKET_LEFT_OVER_AS_LIMIT", "Q");
            orderTypeDict.Add("STOP_MARKET_TO_LIMIT", "S");
            //orderTypeDict.Add("Limit (post-only)", "P");



            combOrderType.Items.AddRange(orderTypeDict.Keys.ToArray());
            combOrderType.SelectedIndex = 1;


            combTIF.Items.Add("DAY");
            combTIF.Items.Add("GOOD_TILL_CANCEL");
            combTIF.Items.Add("AT_THE_OPENING");
            combTIF.Items.Add("IMMEDIATE_OR_CANCEL");
            combTIF.Items.Add("FILL_OR_KILL");
            combTIF.Items.Add("GOOD_TILL_CROSSING");
            combTIF.Items.Add("GOOD_TILL_DATE");
            combTIF.Items.Add("AT_THE_CLOSE");
            combTIF.Items.Add("GOOD_THROUGH_CROSSING");
            combTIF.Items.Add("AT_CROSSING");
            combTIF.Items.Add("GOOD_IN_SESSION");
            combTIF.Items.Add("DAY_PLUS");
            combTIF.Items.Add("GOOD_TILL_CANCEL_PLUS");
            combTIF.Items.Add("GOOD_TILL_DATE_PLUS");
            combTIF.Items.Add("GOOD_TILL_DATE");
            combTIF.SelectedIndex = 0;


            tifDict.Add("DAY", "0");
            tifDict.Add("GOOD_TILL_CANCEL", "1");
            tifDict.Add("AT_THE_OPENING", "2");
            tifDict.Add("IMMEDIATE_OR_CANCEL", "3");
            tifDict.Add("FILL_OR_KILL", "4");
            tifDict.Add("GOOD_TILL_CROSSING", "5");
            tifDict.Add("GOOD_TILL_DATE", "6");
            tifDict.Add("AT_THE_CLOSE", "7");
            tifDict.Add("GOOD_THROUGH_CROSSING", "8");
            tifDict.Add("AT_CROSSING", "9");
            tifDict.Add("GOOD_IN_SESSION", "V");
            tifDict.Add("DAY_PLUS", "W");
            tifDict.Add("GOOD_TILL_CANCEL_PLUS", "X");
            tifDict.Add("GOOD_TILL_DATE_PLUS", "Y");


            orderSideDict.Add("BUY", "1");
            orderSideDict.Add("SELL", "2");
            combSide.Items.AddRange(orderSideDict.Keys.ToArray());

            combSide.SelectedIndex = 0;
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {


            CommonClassLib.NetInfo obj = new CommonClassLib.NetInfo();
            CommonClassLib.OrderInfo info = new CommonClassLib.OrderInfo();

            obj.code = CommandCode.ORDER;
            //上手号
            //obj.accountNo = "ZD_001";
            obj.accountNo = ZDTradeClientTTConfiurations.Account;
            obj.systemCode = "system1";
            //tag 50  < add key = "SenderSubID" value = "ZD_123456" />
            obj.todayCanUse = tbTag50ID.Text;

            info.exchangeCode = tbSymbol.Text;
            info.code = txtContract.Text;
            info.orderPrice = tbPrice.Text;
            info.orderNumber = tbQty.Text;
            info.buySale = orderSideDict[combSide.Text];
            info.priceType = orderTypeDict[combOrderType.Text];
            info.validDate = tifDict[combTIF.Text];
            info.MinQty = txtMinQty.Text.Trim();
            info.triggerPrice = txtTriggerPrice.Text.Trim();



            ////客户端用的是FIX 7X和 新TT的FIX版本 不一样
            ////两个版本的OrderType值1和2反了，所以
            ////此处为了兼容客户端，避免忘记修改
            ////TAG40:OrdType
            //switch (info.priceType)
            //{
            //    case "1":
            //        info.priceType = "2";
            //        break;
            //    case "2":
            //        info.priceType = "1";
            //        break;
            //    case "4":
            //        info.priceType = "3";
            //        break;
            //    case "3":
            //        break;
            //    case "":
            //        break;
            //}

            ////TAG59:TimeInForce
            ////新TT和老TT不一样
            //switch (info.validDate)
            //{
            //    case "0"://当日有效
            //        info.validDate = "1";
            //        break;
            //    case "1"://永久有效
            //        info.validDate = "2";
            //        break;
            //    case "3"://IOC
            //        info.validDate = "4";
            //        break;
            //    case "4":// Fill Or Kill (FOK)
            //        info.validDate = "5";
            //        break;
            //}
            info.priceType = globexCommu.ConvertToZDOrdType(info.priceType);
            info.validDate = globexCommu.ConvertToZDTimeInForce(info.validDate);
            obj.infoT = info.MyToString();



            globexCommu.PlaceOrder(obj);
            return;



            //20190719 16:55:47:353 [3] - 192.168.1.225:52801 {(len=156)
            //string netInfoStr = "ORDER001@000263904J000019@000264094J000113@000363_01@@ZD_001@CBOE@@000363@&zdtmifid@000362@ZD_001@zdtmifid@C@CBOE@VX1908@1@1@15.25@@1@@@0.0@1@1@@0@0@VX@1908";
            //NetInfo ni = new NetInfo();
            //ni.MyReadString(netInfoStr);
            //globexCommu.PlaceOrder(ni);

            //string netInfoStr1 ="ORDER001@20190806000001@0002640946000001@000363@@ZD_001@ICE@@000363@&zdtmifid@@ZD_001@zdtmifid@C@ICE@BRN1912@1@1@59.25@@1@@@0.0@1@1@@0@0@BRN@1912";
            //NetInfo ni = new NetInfo();
            //ni.MyReadString(netInfoStr1);
            //globexCommu.PlaceOrder(ni);


        }

        public void lbMsgs_addItem(string args)
        {

            if (lbMsgs.InvokeRequired)
            {
                lbMsgs.Invoke((MethodInvoker)(() =>
                {
                    lbMsgs_addItem(args);
                }));
            }
            else
            {
                lbMsgs.Items.Add(args);
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo obj = new CommonClassLib.NetInfo();
            CommonClassLib.CancelInfo info = new CommonClassLib.CancelInfo();

            obj.code = CommandCode.CANCEL;
            obj.accountNo = ZDTradeClientTTConfiurations.Account;
            obj.systemCode = "system1";
            obj.todayCanUse = tbTag50ID.Text;

            info.exchangeCode = tbSymbol.Text;
            info.code = txtContract.Text;
            info.orderPrice = tbPrice.Text;
            info.orderNumber = tbQty.Text;
            info.buySale = orderSideDict[combSide.Text];
            info.priceType = orderTypeDict[combOrderType.Text];
            info.orderNo = tbClOrderID.Text;

            obj.infoT = info.MyToString();
            //globexCommu.CancelOrder(obj,  info, tifDict[combTIF.Text]);
            globexCommu.CancelOrder(obj);
        }

        private void btnOrderCreation_Click(object sender, EventArgs e)
        {
            QuickFix.FIX42.ExecutionReport execReport = new QuickFix.FIX42.ExecutionReport();
            ExectutionEventArgs eea = new ExectutionEventArgs(execReport);

            execReport.SetField(new OrdStatus(OrdStatus.NEW));
            execReport.SetField(new Account("account1"));
            execReport.SetField(new ClOrdID("10001"));
            execReport.SetField(new SecurityID("855988"));
            execReport.SetField(new Symbol("ZB"));
            execReport.SetField(new SecurityDesc("ZFH3"));

            execReport.SetField(new Side(Side.BUY));
            execReport.SetField(new OrdType(OrdType.LIMIT));
            execReport.SetField(new Price(new decimal(124.6015625)));
            execReport.SetField(new OrderQty(new decimal(11)));
            execReport.SetField(new TransactTime(DateTime.Now));
            execReport.SetField(new OrderID("20001"));

            execReport.Header.SetField(new SenderCompID("CME"));
            execReport.Header.SetField(new SenderSubID("0047"));

            globexCommu.onExecReportEvent(null, eea);
        }

        private int allQty = 11;
        private int partialFillQty = 0;

        private void btnPartialFill_Click(object sender, EventArgs e)
        {
            QuickFix.FIX42.ExecutionReport execReport = new QuickFix.FIX42.ExecutionReport();
            ExectutionEventArgs eea = new ExectutionEventArgs(execReport);

            partialFillQty = 3;
            execReport.SetField(new Account("account1"));
            execReport.SetField(new ClOrdID("10001"));
            execReport.SetField(new SecurityID("855988"));
            execReport.SetField(new Symbol("ZB"));
            execReport.SetField(new SecurityDesc("ZFH3"));

            execReport.SetField(new Side(Side.BUY));
            execReport.SetField(new OrdType(OrdType.LIMIT));
            execReport.SetField(new Price(new decimal(124.6015625)));
            execReport.SetField(new OrderQty(new decimal(allQty)));
            execReport.SetField(new TransactTime(DateTime.Now));
            execReport.SetField(new OrderID("20001"));

            execReport.Header.SetField(new SenderCompID("CME"));
            execReport.Header.SetField(new SenderSubID("0047"));

            execReport.SetField(new ExecID("30001"));
            execReport.SetField(new LastPx(new decimal(124.6015625)));
            execReport.SetField(new LastQty(new decimal(partialFillQty)));
            execReport.SetField(new CumQty(new decimal(partialFillQty)));
            execReport.SetField(new LeavesQty(new decimal(allQty - partialFillQty)));
            // Partial fill
            execReport.SetField(new OrdStatus(OrdStatus.PARTIALLY_FILLED));
            globexCommu.onExecReportEvent(null, eea);

        }

        private void btnCompleteFill_Click(object sender, EventArgs e)
        {
            QuickFix.FIX42.ExecutionReport execReport2 = new QuickFix.FIX42.ExecutionReport();
            ExectutionEventArgs eea2 = new ExectutionEventArgs(execReport2);

            execReport2.SetField(new Account("account1"));
            execReport2.SetField(new ClOrdID("10001"));
            execReport2.SetField(new SecurityID("855988"));
            execReport2.SetField(new Symbol("ZB"));
            execReport2.SetField(new SecurityDesc("ZFH3"));

            execReport2.SetField(new Side(Side.BUY));
            execReport2.SetField(new OrdType(OrdType.LIMIT));
            execReport2.SetField(new Price(new decimal(124.6015625)));
            execReport2.SetField(new OrderQty(new decimal(allQty)));
            execReport2.SetField(new TransactTime(DateTime.Now));
            execReport2.SetField(new OrderID("20001"));

            execReport2.Header.SetField(new SenderCompID("CME"));
            execReport2.Header.SetField(new SenderSubID("0047"));

            execReport2.SetField(new ExecID("30001"));
            execReport2.SetField(new LastPx(new decimal(124.6015625)));
            execReport2.SetField(new LastQty(new decimal(allQty - partialFillQty)));
            execReport2.SetField(new CumQty(new decimal(allQty)));
            execReport2.SetField(new LeavesQty(new decimal(0)));
            // Complete fill
            execReport2.SetField(new OrdStatus(OrdStatus.FILLED));
            globexCommu.onExecReportEvent(null, eea2);
        }

        private void btnReplyReject_Click(object sender, EventArgs e)
        {
            QuickFix.FIX42.ExecutionReport execReport = new QuickFix.FIX42.ExecutionReport();
            ExectutionEventArgs eea = new ExectutionEventArgs(execReport);

            execReport.SetField(new Account("account1"));
            execReport.SetField(new ClOrdID("10001"));
            execReport.SetField(new OrigClOrdID("10001"));

            execReport.SetField(new SecurityID("855988"));
            execReport.SetField(new Symbol("ZB"));
            execReport.SetField(new SecurityDesc("ZFH3"));

            execReport.SetField(new Side(Side.BUY));
            execReport.SetField(new OrdType(OrdType.LIMIT));
            execReport.SetField(new Price(new decimal(124.6015625)));
            execReport.SetField(new OrderQty(new decimal(allQty)));
            execReport.SetField(new TransactTime(DateTime.Now));

            execReport.Header.SetField(new SenderCompID("CME"));
            execReport.Header.SetField(new SenderSubID("0047"));

            execReport.SetField(new ExecID("30001"));
            execReport.SetField(new OrdRejReason(103));

            // Partial fill
            execReport.SetField(new OrdStatus(OrdStatus.REJECTED));
            globexCommu.onExecReportEvent(null, eea);
        }

        private void btnReplyCancelled_Click(object sender, EventArgs e)
        {
            QuickFix.FIX42.ExecutionReport execReport = new QuickFix.FIX42.ExecutionReport();
            ExectutionEventArgs eea = new ExectutionEventArgs(execReport);

            execReport.SetField(new Account("account1"));
            execReport.SetField(new ClOrdID("10002"));
            execReport.SetField(new OrigClOrdID("10001"));

            execReport.SetField(new SecurityID("855988"));
            execReport.SetField(new Symbol("ZB"));
            execReport.SetField(new SecurityDesc("ZFH3"));

            execReport.SetField(new Side(Side.BUY));
            execReport.SetField(new OrdType(OrdType.LIMIT));
            execReport.SetField(new Price(new decimal(124.6015625)));
            execReport.SetField(new OrderQty(new decimal(allQty)));
            execReport.SetField(new TransactTime(DateTime.Now));

            execReport.Header.SetField(new SenderCompID("CME"));
            execReport.Header.SetField(new SenderSubID("0047"));

            execReport.SetField(new OrdStatus(OrdStatus.CANCELED));
            globexCommu.onExecReportEvent(null, eea);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo obj = new CommonClassLib.NetInfo();
            CommonClassLib.ModifyInfo info = new CommonClassLib.ModifyInfo();

            obj.code = CommandCode.MODIFY;
            obj.accountNo = ZDTradeClientTTConfiurations.Account;
            obj.systemCode = "system1";
            obj.todayCanUse = tbTag50ID.Text;
            info.orderPrice = tbPrice.Text;
            info.exchangeCode = tbSymbol.Text;
            info.code = txtContract.Text;
            info.modifyPrice = tbPrice.Text;
            info.modifyNumber = tbQty.Text;
            info.buySale = orderSideDict[combSide.Text];
            info.priceType = orderTypeDict[combOrderType.Text];
            info.orderNo = tbClOrderID.Text;

            obj.infoT = info.MyToString();
            //globexCommu.CancelReplaceOrder(obj,info, tifDict[combTIF.Text]);
            globexCommu.CancelReplaceOrder(obj);
        }

        private void btnQueryInstrument_Click(object sender, EventArgs e)
        {
            //globexCommu.queryInstrument();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuickFix.Message msg = new QuickFix.Message("8=FIX.4.29=34335=D34=15749=DBECKMIFID52=20171115-06:57:5956=TTMIFIDOR1=dbeckmifid11=598731712440=LMA01316102=01338=540=244=54848=544473847=W54=255=IPE e-Gas Oil60=20051205-09:11:59167=FUT204=0207=ICE_IPE2593=22594=22595=Y2594=32595=Y453=3448=1452=1222376=24447=P448=1452=122376=24447=P448=987654452=32376=24447=P1724=510=083");
            globexCommu.tradeApp.Send(msg);
        }



        private void lbMsgs_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Control) && e.KeyCode == Keys.C)
            {
                Clipboard.SetDataObject(this.lbMsgs.SelectedItem.ToString());
            }
        }

        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
