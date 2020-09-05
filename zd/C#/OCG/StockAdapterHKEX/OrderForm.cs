using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AuthCommon;
using CommonClassLib;
using QuickFix.Fields;
using StockAdapterHKEX;
using StockAdapterHKEX.Common;

namespace StockAdapterHKEX
{
    public partial class OrderForm : Form
    {


        private Log _logger = null;
        private HKEXCommunication globexCommu = null;
        private Dictionary<string, string> orderTypeDict = new Dictionary<string, string>();
        private Dictionary<string, string> orderSideDict = new Dictionary<string, string>();
        private Dictionary<string, string> tifDict = new Dictionary<string, string>();

        public OrderForm(HKEXCommunication globexCommu)
        {
            InitializeComponent();
            this.globexCommu = globexCommu;
            _logger =LogManager.GetLogger("StockAdapterHKEX");
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            this.txtSysCodeFilePath.Text = Path.Combine(Application.StartupPath, "UnTradedSysCodes.txt");
            this.lblTOSCount.Text = globexCommu.messageThrottle.ToString();
            this.nudCancelCountPerSecond.Value = (decimal)(globexCommu.messageThrottle - 1);
            this.nudCancelCountPerSecond.Maximum = globexCommu.messageThrottle;
            this.nudCancelCountPerSecond.Minimum = 1;
            orderTypeDict.Add("Market", "1");
            orderTypeDict.Add("Limit", "2");

            foreach (string orderType in orderTypeDict.Keys)
            {
                combOrderType.Items.Add(orderType);
            }

            combOrderType.SelectedIndex = 1;


            tifDict.Add("增强限价盘<0>[EL: Enhace limit]", "7");
            tifDict.Add("竞价限价盘<9>[ALO: Auction limit Order]", "5");
            //竞价盘不要价格
            tifDict.Add("竞价盘<9>[AO: Auction Order]", "6");
            tifDict.Add("IOC<3> Type [Only Test]", "3");
            tifDict.Add("FOK<4> Type [Only Test]", "4");

            foreach (string tif in tifDict.Keys)
            {
                combTIF.Items.Add(tif);
            }

            combTIF.SelectedIndex = 1;


            orderSideDict.Add("BUY", "1");
            orderSideDict.Add("SELL", "2");
            orderSideDict.Add("Sell Short", "5");

            foreach (string orderSide in orderSideDict.Keys)
            {
                combSide.Items.Add(orderSide);
            }
            combSide.SelectedIndex = 0;
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo netInfo = new CommonClassLib.NetInfo();
            CommonClassLib.OrderInfo info = new CommonClassLib.OrderInfo();

            //obj.code = CommandCode.ORDER;
            netInfo.code = Command.OrderStockHK;
            netInfo.accountNo = "06984001";
            netInfo.systemCode = "system1";
            netInfo.todayCanUse = tbTag50ID.Text;
            netInfo.exchangeCode = tbSymbol.Text;

            info.exchangeCode = tbSymbol.Text;
            info.code = tbSecurity.Text;
            info.orderPrice = tbPrice.Text;
            info.orderNumber = tbQty.Text;
            info.buySale = orderSideDict[combSide.Text];
            //订单类型用priceType区分。值是TIF，后续会转化。抓狂的设计
            //走真实流程测试：globexCommu.PlaceOrder(obj);
            info.priceType = tifDict[combTIF.Text];
            //直接发送FixMessage：globexCommu.PlaceOrder(obj, info);
            //info.priceType = orderTypeDict[combOrderType.Text];
            info.validDate = tifDict[combTIF.Text];

            //Code for OCG simulator start
            info.userType = (checkBox1.Checked ? "1" : "2") + "|" + textBox2.Text + "|" + textBox1.Text + "|" + (checkBox3.Checked ? "1" : "2");
            //Code for OCG simulator end
            //TAG1093 碎股
            if (checkBox3.Checked)
            {
                info.htsType = "1";
            }
            netInfo.infoT = info.MyToString();

            //直接测试下单功能
            //globexCommu.PlaceOrder(obj, info);

            //真实流程
            globexCommu.doOrder(netInfo);


            //  var orderInfoStr = @"OrdeStHK@00000@00015270JA000268@8206675@@CO020990001@HKEX@@8206675@@&HKEX_1@@CO020990001@123456@P@HKEX@4.HK@2@75000@0.186@@7@@@@1@0@@@@@@@@@1";
            //  NetInfo netInfoOrder = new NetInfo();
            //  netInfoOrder.MyReadString(orderInfoStr);
            //  OrderInfo orderInfo = new OrderInfo();
            //  orderInfo.MyReadString(netInfoOrder.infoT);
            //  orderInfo.code = "4";
            //  orderInfo.orderPrice = "100";
            ////  globexCommu.doOrder(netInfoOrder);

            //  globexCommu.PlaceOrder(netInfoOrder, orderInfo);
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            //for (int i = 1; i <= 10; i++)
            //{
            CommonClassLib.NetInfo obj = new CommonClassLib.NetInfo();
            CommonClassLib.CancelInfo info = new CommonClassLib.CancelInfo();
            //obj.code = CommandCode.CANCEL;
            obj.code = Command.CancelStockHK;
            obj.accountNo = "06984001";
            obj.systemCode = "system1";
            obj.todayCanUse = tbTag50ID.Text;
            obj.exchangeCode = tbSymbol.Text;

            info.exchangeCode = tbSymbol.Text;
            info.code = tbSecurity.Text;
            info.orderPrice = tbPrice.Text;
            info.orderNumber = tbQty.Text;
            info.buySale = orderSideDict[combSide.Text];
            //info.priceType = orderTypeDict[combOrderType.Text];
            info.priceType = tifDict[combTIF.Text];
            info.orderNo = tbClOrderID.Text;

            //Code for OCG simulator start
            info.userType = (checkBox1.Checked ? "1" : "2") + "|" + textBox2.Text + "|" + textBox1.Text + "|" + (checkBox3.Checked ? "1" : "2");
            //Code for OCG simulator end

            obj.infoT = info.MyToString();
            ////直接测试下单功能
            //globexCommu.cancelOrderWork(obj);




            //真实流程
            globexCommu.doOrder(obj);
            //string str = "CancStHK@@system1@@@06984001@HKEX@@@&@2|c x|7533|2@@@@@10033329@HKEX@4@1@1000@100@@@7@@@@@@@";
            //NetInfo ni = new NetInfo();
            //ni.MyReadString(str);
            //CancelInfo ci = new CancelInfo();
            //ci.MyReadString(ni.infoT);
            //globexCommu.doOrder(ni);
            //批量撤单测试

            //实盘调用逻辑
            // globexCommu.doOrder(obj);
            //globexCommu.doOrder(ni);
            //}

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ////批量改单测试
            //for (int i = 1; i <= 10; i++)
            //{
            CommonClassLib.NetInfo obj = new CommonClassLib.NetInfo();
            CommonClassLib.ModifyInfo info = new CommonClassLib.ModifyInfo();

            //obj.code = CommandCode.MODIFY;
            obj.code = Command.ModifyStockHK;
            obj.accountNo = "06984001";
            obj.systemCode = "system1";
            obj.todayCanUse = tbTag50ID.Text;
            obj.exchangeCode = tbSymbol.Text;

            info.exchangeCode = tbSymbol.Text;
            info.code = tbSecurity.Text;
            info.modifyPrice = tbPrice.Text;
            info.modifyNumber = tbQty.Text;
            info.buySale = orderSideDict[combSide.Text];
            //info.priceType = orderTypeDict[combOrderType.Text];
            info.priceType = tifDict[combTIF.Text];
            info.validDate = tifDict[combTIF.Text];
            info.orderNo = tbClOrderID.Text;

            //Code for OCG simulator start
            info.userType = (checkBox1.Checked ? "1" : "2") + "|" + textBox2.Text + "|" + textBox1.Text + "|" + (checkBox3.Checked ? "1" : "2");
            //Code for OCG simulator end

            obj.infoT = info.MyToString();
            ////直接测试下单功能
            //globexCommu.cancelReplaceOrderWork(obj);

            //真实流程
            globexCommu.doOrder(obj);
            //info.orderNumber = i.ToString();
            //obj.infoT = info.MyToString();
            ////实盘调用逻辑
            //globexCommu.doOrder(obj);

        }





        delegate void ControlCallback(string args);

        public void lbMsgs_addItem(string args)
        {

            if (lbMsgs.InvokeRequired)
            {
                ControlCallback d = new ControlCallback(lbMsgs_addItem);
                lbMsgs.Invoke(d, new object[] { args });
            }
            else
            {
                lbMsgs.Items.Add(args);
            }
        }

     

        private void btnOrderCreation_Click(object sender, EventArgs e)
        {
            QuickFix.FIX50.ExecutionReport execReport = new QuickFix.FIX50.ExecutionReport();
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
            QuickFix.FIX50.ExecutionReport execReport = new QuickFix.FIX50.ExecutionReport();
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
            QuickFix.FIX50.ExecutionReport execReport2 = new QuickFix.FIX50.ExecutionReport();
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
            QuickFix.FIX50.ExecutionReport execReport = new QuickFix.FIX50.ExecutionReport();
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
            QuickFix.FIX50.ExecutionReport execReport = new QuickFix.FIX50.ExecutionReport();
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

     
        private void btnQueryInstrument_Click(object sender, EventArgs e)
        {
            //globexCommu.queryInstrument();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            globexCommu.partyEntitlement();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            globexCommu.throttleEntitlement();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            globexCommu.resendRequest(Convert.ToInt16(textBox3.Text), Convert.ToInt16(textBox4.Text));
        }


        private void BtnCancelOderFromInfo_Click(object sender, EventArgs e)
        {

            QuickFix.FIX50.OrderCancelRequest orderCancelRequest = new QuickFix.FIX50.OrderCancelRequest();

            //tag 11
            orderCancelRequest.ClOrdID = new ClOrdID(ClOrderIDGen.getNextClOrderID().ToString());


            //tag 41
            orderCancelRequest.OrigClOrdID = new OrigClOrdID(this.txtClientOrderID.Text);


            var securityIDSource = new SecurityIDSource("8");
            var securityExchange = new SecurityExchange("XHKG");
            // Tag55
            orderCancelRequest.SecurityID = new SecurityID(this.txtSecurityID.Text);
            orderCancelRequest.SecurityIDSource = securityIDSource;
            orderCancelRequest.SecurityExchange = securityExchange;


            // Tag54
            orderCancelRequest.Side = new Side(this.txtSide.Text[0]);

            // Tag38
            orderCancelRequest.OrderQty = new OrderQty(decimal.Parse(this.txtOrderQty.Text));

            // Tag60
            orderCancelRequest.TransactTime = new TransactTime(DateTime.UtcNow);

            //Code for OCG simulator start

            //string[] p = info.userType.Split('|');

            //sessionID = p[2];

            //Code for OCG simulator end

            var cfgInstance = CfgManager.getInstance("StockAdapterHKEX.exe");
            var sessionID = cfgInstance.SessionAndPsw.Split(',')[0];
            QuickFix.FIX50.OrderCancelRequest.NoPartyIDsGroup noPartyIDsGroup = new QuickFix.FIX50.OrderCancelRequest.NoPartyIDsGroup();
            noPartyIDsGroup.PartyID = new PartyID(sessionID);
            noPartyIDsGroup.PartyIDSource = new PartyIDSource('D');
            noPartyIDsGroup.PartyRole = new PartyRole(1);
            orderCancelRequest.AddGroup(noPartyIDsGroup);

            var str = orderCancelRequest.ToString();
            globexCommu.tradeApp.SendMessage(orderCancelRequest);


        }

        private void btnCancelOrderCommand_Click(object sender, EventArgs e)
        {
            var cancelCommand = this.txtCancelOrder.Text.Trim();
            NetInfo netInfo = new NetInfo();
            netInfo.MyReadString(cancelCommand);
            globexCommu.cancelOrderWork(netInfo);
        }

        private void btnAmendOrderCommand_Click(object sender, EventArgs e)
        {
            var amendCommand = this.txtAmendOrder.Text.Trim();
            NetInfo netInfo = new NetInfo();
            netInfo.MyReadString(amendCommand);
            globexCommu.cancelReplaceOrderWork(netInfo);
        }



        private void btnSetUnPending_Click(object sender, EventArgs e)
        {

            if (long.TryParse(this.txtNewOrderSingleCliID.Text.Trim(), out long cliOrderID))
            {
                if (globexCommu.ClOrdIdIsPending.TryGetValue(cliOrderID, out _))
                {
                    globexCommu.ClOrdIdIsPending.TryRemove(cliOrderID, out _);
                    if (globexCommu.xReference.TryGetValue(cliOrderID, out RefObj refObj))
                    {
                        refObj.orderStatus = RefObj.ORD_INIT_STATUS;
                        // MessageBox.Show("设置成功！");
                    }
                    else
                    {
                        globexCommu.ClOrdIdIsPending.TryAdd(cliOrderID, true);
                        //  MessageBox.Show($"设置失败，ClOrdIdIsPending 没有找到 key {cliOrderID}！");
                    }
                }
                else
                {
                    //  MessageBox.Show($"设置失败，ClOrdIdIsPending 没有找到 key {cliOrderID}！");
                }
            }

        }

        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void btnPendingData_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(File.Open("PendingData.txt", FileMode.Create, FileAccess.ReadWrite), System.Text.Encoding.UTF8))
            {
                foreach (var key in globexCommu.ClOrdIdIsPending.Keys)
                {
                    sw.WriteLine(key);
                }
            }

        }

        private void btnQuicklyCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                var systemCode = this.txtSystemCode.Text.Trim();
                //系统号撤单
                if (!string.IsNullOrEmpty(systemCode))
                {
                    CancelBySysCode(systemCode);
                }
                int cancelCountPerSecond = (int)this.nudCancelCountPerSecond.Value;
                if (cancelCountPerSecond >= globexCommu.messageThrottle)
                {
                    MessageBox.Show("每秒撤单量应小于TPS。");
                    return;
                }
                if (this.cbSysCodeFile.Checked)
                {
                    //系统号文件批量撤单
                    var filePath = txtSysCodeFilePath.Text.Trim();
                    List<string> sysCodeList = TxtFile.ReadTxtFile(filePath);
                    CancelBySysCode(sysCodeList.ToArray());
                }

                var startTime = this.dtpCancelStartTime.Value;
                var endTime = this.dtpCancelEndTime.Value;
                //根据时间段撤单：撤掉时间段内的所有未成交的单
                List<RefObj> canceledRefObj = new List<RefObj>();
                if (this.cbCancelByTime.Checked)
                {
                    foreach (var refObj in globexCommu.downReference.Values)
                    {
                        var latestExecutionReport = refObj.fromGlobex.Last();
                        var transactTime = Utils.toChinaLocalTime(latestExecutionReport.TransactTime.getValue());
                        //订单在撤单时间内。
                        if (transactTime >= startTime && transactTime <= endTime)
                        {
                            canceledRefObj.Add(refObj);
                        }
                    }

                }
                for (int i = 0; i < canceledRefObj.Count; i++)
                {
                    var refObj = canceledRefObj[i];
                    CancelOrderByRefObj(refObj);
                    if ((i % cancelCountPerSecond == 0))
                    {
                        Thread.Sleep(1000);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.WriteLog( ex.ToString());
            }

        }

        private void CancelBySysCode(params string[] sysCodeList)
        {
            int cancelCountPerSecond = (int)this.nudCancelCountPerSecond.Value;
            for (int i = 0; i < sysCodeList.Length; i++)
            {
                var systemCode = sysCodeList[i];
                foreach (var refObj in globexCommu.downReference.Values)
                {
                    if (refObj.lastSendInfo == null)
                    {
                        continue;
                    }
                    if (refObj.lastSendInfo.systemCode == systemCode)
                    {
                        CancelOrderByRefObj(refObj);
                        break;
                    }
                }
                if ((i % cancelCountPerSecond == 0))
                {
                    Thread.Sleep(1000);
                }
            }

        }

        private void CancelOrderByRefObj(RefObj refObj)
        {
            //_logger.log(3, refObj.lastSendInfo.MyToString());
            CommonClassLib.NetInfo netInfo = new NetInfo();
            netInfo.MyReadString(refObj.lastSendInfo.MyToString());//深拷贝，不能操作失败而修改源数据
            CommonClassLib.CancelInfo cancelInfo = new CommonClassLib.CancelInfo();
            string code = "";
            string orderNumber = "";
            string buySale = "";
            switch (netInfo.code)
            {
                case "OrdeStHK":
                    var orderInfo = new OrderInfo();
                    orderInfo.MyReadString(netInfo.infoT);
                    code = orderInfo.code;
                    orderNumber = orderInfo.orderNumber;
                    buySale = orderInfo.buySale;
                    break;
                case "ModiStHK":
                    var modifyInfo = new ModifyInfo();
                    modifyInfo.MyReadString(netInfo.infoT);
                    code = modifyInfo.code;
                    orderNumber = modifyInfo.modifyNumber;
                    buySale = modifyInfo.buySale;
                    break;
            }
            //_logger.log(3, $"orderNumber:{orderNumber}");
            netInfo.code = Command.CancelStockHK;

            //tag 11
            cancelInfo.orderNo = refObj.clOrderID;
            //tag 48
            cancelInfo.code = code;
            //tag 38
            cancelInfo.orderNumber = orderNumber;
            //tag 54
            cancelInfo.buySale = buySale;


            netInfo.infoT = cancelInfo.MyToString();
            //直接测试下单功能
            globexCommu.cancelOrderWork(netInfo);
        }


        /// <summary>
        /// 导出所有未成交的单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportUnTraded_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();

                foreach (var refObj in globexCommu?.downReference?.Values)
                {
                    //lastSendInfo如果未持久化，下次启动load此字段为null。
                    if (refObj.lastSendInfo == null)
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(refObj.lastSendInfo.systemCode))
                    {
                        continue;
                    }
                    //系统号,clientID
                    list.Add($"{refObj.lastSendInfo.systemCode},{refObj.clOrderID},{refObj.fromGlobex.Last()?.TransactTime.getValue().ToString("yyyy:MM:dd HH:mm:ss.fff")}");

                }

                string fileName = "UnTradedOrders.txt";
                TxtFile.SaveTxtFile(fileName, list, FileMode.Create);
                System.Diagnostics.Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex.ToString());
            }
        }

        private void btnExportedUntradedSysCode_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (var refObj in globexCommu?.downReference?.Values)
            {
                //lastSendInfo如果未持久化，下次启动load此字段为null。
                if (refObj.lastSendInfo == null)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(refObj.lastSendInfo.systemCode))
                {
                    continue;
                }
                //系统号,clientID
                list.Add($"{refObj.lastSendInfo.systemCode}");

            }
            list = list.Distinct().ToList();
            string fileName = "UnTradedSysCodes.txt";
            TxtFile.SaveTxtFile(fileName, list, FileMode.Create);
            System.Diagnostics.Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));
        }

        private void btnOpenSystemCode_Click(object sender, EventArgs e)
        {
            this.opdSysCode.InitialDirectory = Application.StartupPath;
            this.opdSysCode.Filter = "txt files (*.txt)|*.txt";
            this.opdSysCode.FilterIndex = 2;
            this.opdSysCode.RestoreDirectory = true;
            this.opdSysCode.FileName = "";
            if (this.opdSysCode.ShowDialog() == DialogResult.OK)
            {
                this.txtSysCodeFilePath.Text = this.opdSysCode.FileName;
            }
        }

        private void btnCancelByOrderResponse_Click(object sender, EventArgs e)
        {
            var orderResponse = this.txtOrderResponse.Text.Trim();
            NetInfo netInfo = new NetInfo();
            netInfo.MyReadString(orderResponse);
            OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
            orderResponseInfo.MyReadString(netInfo.infoT);
            CancelInfo cancelInfo = new CancelInfo();

            netInfo.code = Command.CancelStockHK;
            //tag 11
            cancelInfo.orderNo = orderResponseInfo.orderNo;
            //tag 48
            cancelInfo.code = orderResponseInfo.code;
            //tag 38
            cancelInfo.orderNumber = orderResponseInfo.orderNumber;
            //tag 54
            cancelInfo.buySale = orderResponseInfo.buySale;
            netInfo.infoT = cancelInfo.MyToString();
            //直接测试下单功能
            globexCommu.cancelOrderWork(netInfo);
        }

        private void btnCancelByAmendResponse_Click(object sender, EventArgs e)
        {
            var amendResponse = this.txtAmendResponse.Text.Trim();
            NetInfo netInfo = new NetInfo();
            netInfo.MyReadString(amendResponse);
            OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
            orderResponseInfo.MyReadString(netInfo.infoT);
            CancelInfo cancelInfo = new CancelInfo();

            netInfo.code = Command.CancelStockHK;
            //tag 11
            cancelInfo.orderNo = orderResponseInfo.orderNo;
            //tag 48
            cancelInfo.code = orderResponseInfo.code;
            //tag 38
            cancelInfo.orderNumber = orderResponseInfo.orderNumber;
            //tag 54
            cancelInfo.buySale = orderResponseInfo.buySale;
            netInfo.infoT = cancelInfo.MyToString();
            //直接测试下单功能
            globexCommu.cancelOrderWork(netInfo);
        }

        private void btnLostMemryDataCancelByOrderResponse_Click(object sender, EventArgs e)
        {
            var orderResponse = this.txtLostMemryDataCancelByOrderResponse.Text.Trim();
            NetInfo netInfo = new NetInfo();
            netInfo.MyReadString(orderResponse);
            OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
            orderResponseInfo.MyReadString(netInfo.infoT);
            CancelInfo cancelInfo = new CancelInfo();

            netInfo.code = Command.CancelStockHK;
            //tag 11
            cancelInfo.orderNo = orderResponseInfo.orderNo;
            //tag 48
            cancelInfo.code = orderResponseInfo.code;
            //tag 38
            cancelInfo.orderNumber = orderResponseInfo.orderNumber;
            //tag 54
            cancelInfo.buySale = orderResponseInfo.buySale;
            netInfo.infoT = cancelInfo.MyToString();


            RefObj refObj = new RefObj();
            refObj.clOrderID = orderResponseInfo.orderNo;
            globexCommu.downReference.TryAdd(orderResponseInfo.origOrderNo, refObj);
            string[] temp = { netInfo.accountNo, netInfo.systemCode, netInfo.clientNo, netInfo.localSystemCode, orderResponseInfo.code, netInfo.exchangeCode, orderResponseInfo.priceType, orderResponseInfo.validDate, "specialReferenceReserve" };
            refObj.strArray = temp;
            //直接测试下单功能
            globexCommu.CancelOrderByResponse(netInfo);
        }


        #region  E2E
        FrmE2ETest _frmE2ETest;
        private void btnTCR_Click(object sender, EventArgs e)
        {
            _frmE2ETest = new FrmE2ETest(globexCommu);
            _frmE2ETest.Show();

        }

        private void tbnMassCancelAll_Click(object sender, EventArgs e)
        {
            globexCommu.MassCancelAll();
        }

        private void btnMassCancelSecurityID_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo netInfo = new CommonClassLib.NetInfo();
            CommonClassLib.CancelInfo cancelInfo = new CommonClassLib.CancelInfo();

            cancelInfo.code = "66.HK";
            cancelInfo.exchangeCode = "HKEX";
            netInfo.infoT = cancelInfo.MyToString();
            globexCommu.MassCancelSecurityID(netInfo); 
        }

        private void btnMassCancelSegment_Click(object sender, EventArgs e)
        {
            globexCommu.MassCancelSegment();
        }
        #endregion


    }
}
