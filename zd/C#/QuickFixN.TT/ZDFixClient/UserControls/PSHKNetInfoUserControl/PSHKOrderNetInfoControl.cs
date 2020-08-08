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


        #region 下单
        private void btnNewOrderSingle_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo netInfo = new CommonClassLib.NetInfo();


            //netInfo.code = CommandCode.ORDER;

            netInfo.code = TradeBaseDataConfig.GetCommandCode(ConfigurationManager.AppSettings["ITradeService"].ToString(), ZDFixService.Service.ZDCommon.CommandType.Order);
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

            Order?.Invoke(netInfo);


        }
        #endregion
    }
}
