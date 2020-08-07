using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.UserControls.BaseNetInfoControl;
using Client.Service.MemoryDataManager;
using System.Configuration;
using Client.Service.ZDCommon;
using Client.Models;
using CommonClassLib;

namespace Client.UserControls.TTNetInfoControl
{
    public partial class TTCancelNetInfoControl : CancelNetInfoControl
    {
        public event Action<NetInfo> CancelOrder;
        public TTCancelNetInfoControl()
        {
            InitializeComponent();
        }


        #region 撤单
        private void btnOrderCancelRequest_Click(object sender, EventArgs e)
        {

            CommonClassLib.NetInfo netInfo = null;
            if (MemoryData.Orders.TryGetValue(this.txtOrderCancelSystemCode.Text.Trim(), out Order order))
            {
                //netInfo = order.OrderNetInfo.CloneWithNewCode("", CommandCode.CANCEL);
                netInfo = order.OrderNetInfo.Clone();
                netInfo.code = TradeBaseDataConfig.GetCommandCode(ConfigurationManager.AppSettings["ITradeService"].ToString(), Service.ZDCommon.CommandType.Cancel);

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

            CancelOrder?.Invoke(netInfo);
        }
        #endregion
    }
}
