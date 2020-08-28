using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZDFixClient.UserControls.BaseNetInfoControl;
using CommonClassLib;
using ZDFixService.Service.ZDCommon;
using System.Configuration;
using ZDFixService.Service;
using ZDFixService.Utility;
namespace ZDFixClient.UserControls.PSHKNetInfoUserControl
{
    public partial class PSHKOrderNetInfoControl : OrderNetInfoControl
    {
        public event Action<NetInfo> Order;
        public PSHKOrderNetInfoControl()
        {
            InitializeComponent();
        }

        private void PSHKOrderNetInfoControl_Load(object sender, EventArgs e)
        {
            this.txtSecurityExchange.Text = "HKEX";
            this.txtSecurityAltID.Text = "0002.HK";
            this.nudQrdQty.Value = 2000;
            this.txtPrice.Text = "105";

        }

        #region 下单
        private void btnNewOrderSingle_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo netInfo = new CommonClassLib.NetInfo();


            netInfo.code = CommandCode.OrderStockHK;

            //netInfo.code = TradeBaseDataConfig.GetCommandCode(ConfigurationManager.AppSettings["ITradeService"].ToString(), ZDFixService.Service.ZDCommon.CommandType.Order);

            //tag1:上手号  9111111/I5555
            netInfo.accountNo = "I5555";
            //tag109
            //netInfo.clientNo = "000365";
            netInfo.clientNo = "C005";

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
            //orderInfo.triggerPrice = this.txtStopPx.Text.Trim();

            //客户端传的是反的
            orderInfo.priceType = orderInfo.priceType == "2" ? "1" : "2";
            //orderInfo.priceType = ZDUperTagValueConvert.ConvertToZDOrdType(orderInfo.priceType);
            //orderInfo.validDate = ZDUperTagValueConvert.ConvertToZDTimeInForce(orderInfo.validDate);
            netInfo.infoT = orderInfo.MyToString();


            //var netInfoStr = "ORDER001@20200804000019@0007262813000041@100091@@ZD_001@ICE@@100091@&ZD_001@@ZD_001@888888@C@ICE@BRN2010@1@1@43.52@@1@@@0.0@1@1@@0@0@BRN@2010@@@@@@@@@@@@0@";
            //NetInfo ni = new NetInfo();
            //ni.MyReadString(netInfoStr);

            //var netInfoStr = $"OrdeStHK@SAM3@000209191P000003@1@@00020919@NASD@@C005@@0033433&C005@@00020919@@C@@000002.HK@1@1@499.50@@1@@@@A@@@@@@@@@@@@@@@@@@0@";
            //netInfo.MyReadString(netInfoStr);
            //netInfo.accountNo = "I5555";
            Order?.Invoke(netInfo);




        }
        #endregion


    }
}
