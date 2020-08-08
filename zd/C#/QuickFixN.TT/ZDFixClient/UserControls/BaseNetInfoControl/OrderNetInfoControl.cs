using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ZDFixClient.UserControls.BaseNetInfoControl
{
    public partial class OrderNetInfoControl : UserControl
    {
        #region  私有字段
        /// <summary>
        /// tag 40
        /// </summary>
        protected Dictionary<string, string> _orderTypeDict = new Dictionary<string, string>();
        /// <summary>
        /// tag 54
        /// </summary>
        protected Dictionary<string, string> _sideDict = new Dictionary<string, string>();
        /// <summary>
        /// tag 59
        /// </summary>
        protected Dictionary<string, string> _timeInForceDict = new Dictionary<string, string>();
        #endregion

        public OrderNetInfoControl()
        {
            InitializeComponent();



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


    }
}
