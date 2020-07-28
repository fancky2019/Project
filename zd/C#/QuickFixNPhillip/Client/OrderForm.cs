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

namespace Client
{
    public partial class OrderForm : Form
    {
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






            TradeClientAppService.Instance.ExecutionReport += (p) =>
              {

                  this.BeginInvoke((MethodInvoker)(() =>
                  {
                      if (!string.IsNullOrEmpty(p))
                      {
                          this.lbMsgs.Items.Add(p);
                      }

                  }));
              };
        }

        private void lbMsgs_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Control) && e.KeyCode == Keys.C)
            {
                Clipboard.SetDataObject(this.lbMsgs.SelectedItem.ToString());
            }
        }

        private void lbMsgs_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(orderInfo));
                    break;
                case "CANCEL01":
                    CancelResponseInfo cancelInfo = new CancelResponseInfo();
                    cancelInfo.MyReadString(netinfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(cancelInfo));
                    break;
                case "MODIFY01":
                    OrderResponseInfo modifyInfo = new OrderResponseInfo();
                    modifyInfo.MyReadString(netinfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(modifyInfo));
                    break;
                default:
                    MessageBox.Show("订单指令有误！");
                    return;
            }
            this.rtbNetInfo.Text = sb.ToString();
        }

        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void btnNewOrderSingle_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo netInfo = new CommonClassLib.NetInfo();
            CommonClassLib.OrderInfo orderInfo = new CommonClassLib.OrderInfo();

            netInfo.code = CommandCode.ORDER;
            //tag1:zd上手号
            netInfo.accountNo = "ZD_001";
            netInfo.systemCode = $"SystemCode{DateTime.Now.GetTimeStamp()}";
            //tag 50  
            netInfo.todayCanUse = "0047";

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

            StopwatchHelper.Instance.Stopwatch.Restart();
            TradeClientAppService.Instance.Order(netInfo);
            //"ORDER001@@SystemCode1595929502113@0047@@ZD_001@@@@&@@@@@ICE@BRN2012@1@1@42.59@@1@@@42.59@1@@@@0"

        }

        private void btnOrderCancelRequest_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo netInfo = new CommonClassLib.NetInfo();
            CommonClassLib.CancelInfo cancelInfo = new CommonClassLib.CancelInfo();

            netInfo.code = CommandCode.CANCEL;
            //tag1:zd上手号
            netInfo.accountNo = "ZD_001";
            netInfo.systemCode = this.txtOrderCancelSystemCode.Text.Trim();

            //tag 50  
            netInfo.todayCanUse = "0047";


            cancelInfo.orderNo = this.txtCancelOrderClOrderID.Text.Trim();

            netInfo.infoT = cancelInfo.MyToString();
            //globexCommu.CancelOrder(obj,  info, tifDict[combTIF.Text]);
            TradeClientAppService.Instance.Order(netInfo);
        }
    }
}
