using Client.Service;
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
            netInfo.systemCode = "system1";
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
            TradeClientAppService.Instance.Order(netInfo);
        }
    }
}
