using Client.Service;
using Client.Utility;
using CommonClassLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Utility.MemoryDataManager;
using Client.Models;
using System.Threading;
using Client.Service.Base;
using Client.Service.ZDCommon;

namespace Client
{
    public partial class OrderForm : Form
    {
        #region  私有字段
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// tag 40
        /// </summary>
        private Dictionary<string, string> _orderTypeDict = new Dictionary<string, string>();
        /// <summary>
        /// tag 54
        /// </summary>
        private Dictionary<string, string> _sideDict = new Dictionary<string, string>();
        /// <summary>
        /// tag 59
        /// </summary>
        private Dictionary<string, string> _timeInForceDict = new Dictionary<string, string>();
        #endregion

        #region Constructor Load
        public OrderForm()
        {
            InitializeComponent();
        }


        private void OrderForm_Load(object sender, EventArgs e)
        {
            _orderTypeDict.Add("MARKET", "1");
            _orderTypeDict.Add("LIMIT", "2");
            _orderTypeDict.Add("STOP", "3");
            _orderTypeDict.Add("STOP_LIMIT", "4");
            _orderTypeDict.Add("MARKET_ON_CLOSE", "5");
            _orderTypeDict.Add("LIMIT_ON_CLOSE", "B");
            _orderTypeDict.Add("MARKET_WITH_LEFT_OVER_AS_LIMIT", "K");
            _orderTypeDict.Add("MARKET_LIMIT_MARKET_LEFT_OVER_AS_LIMIT", "Q");
            _orderTypeDict.Add("STOP_MARKET_TO_LIMIT", "S");

            this.cmbOrderType.Items.AddRange(_orderTypeDict.Keys.ToArray());
            cmbOrderType.SelectedIndex = 1;



            _timeInForceDict.Add("DAY", "0");
            _timeInForceDict.Add("GOOD_TILL_CANCEL", "1");
            _timeInForceDict.Add("AT_THE_OPENING", "2");
            _timeInForceDict.Add("IMMEDIATE_OR_CANCEL", "3");
            _timeInForceDict.Add("FILL_OR_KILL", "4");
            _timeInForceDict.Add("GOOD_TILL_CROSSING", "5");
            _timeInForceDict.Add("GOOD_TILL_DATE", "6");
            _timeInForceDict.Add("AT_THE_CLOSE", "7");
            _timeInForceDict.Add("GOOD_THROUGH_CROSSING", "8");
            _timeInForceDict.Add("AT_CROSSING", "9");
            _timeInForceDict.Add("GOOD_IN_SESSION", "V");
            _timeInForceDict.Add("DAY_PLUS", "W");
            _timeInForceDict.Add("GOOD_TILL_CANCEL_PLUS", "X");
            _timeInForceDict.Add("GOOD_TILL_DATE_PLUS", "Y");

            this.cmbTimeInForce.Items.AddRange(_timeInForceDict.Keys.ToArray());
            cmbTimeInForce.SelectedIndex = 0;


            _sideDict.Add("BUY", "1");
            _sideDict.Add("SELL", "2");
            this.cmbSide.Items.AddRange(_sideDict.Keys.ToArray());
            cmbSide.SelectedIndex = 0;


            //TradeServiceFactory.ITradeService.ExecutionReport += (p) =>
            //  {
            //      //在创建窗口句柄之前，不能在控件上调用 Invoke 或 BeginInvoke。
            //      if (this.IsHandleCreated)
            //      {
            //          this.BeginInvoke((MethodInvoker)(() =>
            //          {
            //              if (!string.IsNullOrEmpty(p))
            //              {
            //                  this.lbMsgs.Items.Add(p);
            //              }

            //          }));
            //      }
            //  };

            //窗体关闭要注销事件。
            TradeServiceFactory.ITradeService.ExecutionReport += ExecutionReport;


        }
        #endregion

        #region  ExecutionReport

        private void ExecutionReport(string netInfo)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                if (!string.IsNullOrEmpty(netInfo))
                {
                    this.lbMsgs.Items.Add(netInfo);
                }

            }));
        }
        private void lbMsgs_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Control) && e.KeyCode == Keys.C)
            {
                Clipboard.SetDataObject(this.lbMsgs.SelectedItem.ToString());
            }
        }

        /*
         * 50=0:ErrorCode.ERR_ORDER_0000;CommandCode.ORDER
         * 150=5:ErrorCode.ERR_ORDER_0016;CommandCode.MODIFY
         * 150=4:ErrorCode.ERR_ORDER_0014;CommandCode.CANCELCAST
         * 150=2:ErrorCode.SUCCESS;CommandCode.FILLEDCAST;
         */
        private void lbMsgs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbMsgs.SelectedItem == null)
            {
                return;
            }
            NetInfo netinfo = new NetInfo();
            var netInfoStr = this.lbMsgs.SelectedItem.ToString();
            netinfo.MyReadString(netInfoStr);
            //this.rtbNetInfo.Text = MessagePackUtility.SerializeToJson(netinfo);
            //this.rtbNetInfo.Text = MessagePackUtility.SerializeToJson(NewtonsoftHelper.JsonSerializeObjectFormat(netinfo));


            StringBuilder sb = new StringBuilder();
            sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(netinfo));
            sb.Append("\r\n");
            var command = netInfoStr.Substring(0, 8);
            switch (command)
            {
                case "ORDER001":
                    OrderResponseInfo orderInfo = new OrderResponseInfo();
                    orderInfo.MyReadString(netinfo.infoT);
                    //@@@@@ICE@BRN2012@1@1@42.59@@1@@@42.59@1@@@@0
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(orderInfo));
                    break;
                case "CANCST01":
                    CancelResponseInfo cancelInfo = new CancelResponseInfo();
                    cancelInfo.MyReadString(netinfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(cancelInfo));
                    break;
                case "MODIFY01":
                    OrderResponseInfo modifyInfo = new OrderResponseInfo();
                    modifyInfo.MyReadString(netinfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(modifyInfo));
                    break;
                case "FILCST01":
                    FilledResponseInfo filledResponseInfo = new FilledResponseInfo();
                    filledResponseInfo.MyReadString(netinfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(filledResponseInfo));
                    break;
                default:
                    MessageBox.Show("订单指令有误！");
                    return;
            }
            this.rtbNetInfo.Text = sb.ToString();
        }

        private void OrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //窗体关闭要注销事件。
            TradeServiceFactory.ITradeService.ExecutionReport -= ExecutionReport;
        }
        #endregion

        #region 下单
        private void btnNewOrderSingle_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo netInfo = new CommonClassLib.NetInfo();


            netInfo.code = CommandCode.ORDER;
            //tag1:zd上手号
            netInfo.accountNo = "ZD_001";
            netInfo.clientNo = "000365";
            netInfo.systemCode = $"SystemCode{DateTime.Now.GetTimeStamp()}";
            netInfo.localSystemCode = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            netInfo.exchangeCode = this.txtSecurityExchange.Text.Trim();
            //tag 50  
            netInfo.todayCanUse = "0047";

            CommonClassLib.OrderInfo orderInfo = new CommonClassLib.OrderInfo();
            orderInfo.exchangeCode = this.txtSecurityExchange.Text.Trim();
            orderInfo.code = this.txtSecurityAltID.Text;
            orderInfo.orderPrice = this.txtPrice.Text;
            orderInfo.orderNumber = this.nudQrdQty.Text;
            orderInfo.buySale = this._sideDict[this.cmbSide.Text];
            orderInfo.priceType = this._orderTypeDict[this.cmbOrderType.Text];
            orderInfo.validDate = this._timeInForceDict[this.cmbTimeInForce.Text];
            orderInfo.MinQty = this.nudMinQty.Text;
            orderInfo.triggerPrice = this.txtStopPx.Text.Trim();

            orderInfo.priceType = ZDUperTagValueConvert.ConvertToZDOrdType(orderInfo.priceType);
            orderInfo.validDate = ZDUperTagValueConvert.ConvertToZDTimeInForce(orderInfo.validDate);
            netInfo.infoT = orderInfo.MyToString();


            //var netInfoStr = "ORDER001@20200804000019@0007262813000041@100091@@ZD_001@ICE@@100091@&ZD_001@@ZD_001@888888@C@ICE@BRN2010@1@1@43.52@@1@@@0.0@1@1@@0@0@BRN@2010@@@@@@@@@@@@0@";
            //NetInfo ni = new NetInfo();
            //ni.MyReadString(netInfoStr);

            TradeServiceFactory.ITradeService.Order(netInfo);


        }
        #endregion

        #region 改单
        private void btnAmendOrder_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo netInfo = null;
            if (MemoryData.Orders.TryGetValue(this.txtAmendSysCode.Text.Trim(), out Order order))
            {
                netInfo = order.OrderNetInfo.CloneWithNewCode("", CommandCode.MODIFY);
            }
            else
            {
                MessageBox.Show($"没有找到系统号为:{this.txtAmendSysCode.Text.Trim()}的订单");
                return;
            }

            CommonClassLib.ModifyInfo modifyInfo = new CommonClassLib.ModifyInfo();
            modifyInfo.orderNo = this.txtAmendClOrderID.Text.Trim();
            modifyInfo.modifyNumber = this.nudAmendQty.Text;
            modifyInfo.modifyPrice = this.txtAmendPrice.Text.Trim();
            modifyInfo.modifyTriggerPrice = this.txtAmendStopPrice.Text.Trim();

            netInfo.infoT = modifyInfo.MyToString();

            //var modifyOrderStr="MODIFY01@20200804000019@0007262813000041@100091@@ZD_001 @ICE@@100091@&ZD_001@@ZD_001@888888@1500000082@ICE @BRN2010@1@1@43.52@0@3@43.50@1@1@C@0.00@0.0@1@@@@@@@@@@@@0@";
            //NetInfo ni = new NetInfo();
            //ni.MyReadString(modifyOrderStr);


            TradeServiceFactory.ITradeService.Order(netInfo);
        }
        #endregion

        #region 撤单
        private void btnOrderCancelRequest_Click(object sender, EventArgs e)
        {

            CommonClassLib.NetInfo netInfo = null;
            if (MemoryData.Orders.TryGetValue(this.txtOrderCancelSystemCode.Text.Trim(), out Order order))
            {
                netInfo = order.OrderNetInfo.CloneWithNewCode("", CommandCode.CANCEL);
            }
            else
            {
                MessageBox.Show($"没有找到系统号为:{this.txtOrderCancelSystemCode.Text.Trim()}的订单");
                return;
            }
            CommonClassLib.CancelInfo cancelInfo = new CommonClassLib.CancelInfo();

            cancelInfo.orderNo = this.txtCancelOrderClOrderID.Text.Trim();

            netInfo.infoT = cancelInfo.MyToString();


            // var cancelOrderStr = "CANCEL01@20200804000019@0007262813000041@100091@@ZD_001 @ICE@@@&ZD_001@192.168.1.207@ZD_001@888888@@0007262813000041@1500000082@ICE @BRN2010@1@1@@0@@@@C@@@@@@";
            //NetInfo ni = new NetInfo();
            //ni.MyReadString(cancelOrderStr);

            TradeServiceFactory.ITradeService.Order(netInfo);
        }
        #endregion

        #region 工具
        private void btnExportOrders_Click(object sender, EventArgs e)
        {

        }


        #region  目录
        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }
        #endregion

        #endregion

   
    }
}
