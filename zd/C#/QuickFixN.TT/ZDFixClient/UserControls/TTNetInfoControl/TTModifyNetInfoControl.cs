using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZDFixService.Service.ZDCommon;
using ZDFixService.Models;
using ZDFixService.Service.MemoryDataManager;
using System.Configuration;
using CommonClassLib;
using ZDFixClient.UserControls.BaseNetInfoControl;

namespace ZDFixClient.UserControls.TTNetInfoControl
{
    public partial class TTModifyNetInfoControl : ModifyNetInfoControl
    {
        public event Action<NetInfo> ModifyOrder;
        public TTModifyNetInfoControl()
        {
            InitializeComponent();
        }


        #region 改单
        private void btnAmendOrder_Click(object sender, EventArgs e)
        {
            CommonClassLib.NetInfo netInfo = null;
            if (MemoryData.Orders.TryGetValue(this.txtAmendSysCode.Text.Trim(), out Order order))
            {
                //netInfo = order.OrderNetInfo.CloneWithNewCode("", CommandCode.MODIFY);
                netInfo = order.OrderNetInfo.Clone();
                netInfo.code = TradeBaseDataConfig.GetCommandCode(ConfigurationManager.AppSettings["ITradeService"].ToString(), ZDFixService.Service.ZDCommon.CommandType.Modify);
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

            ModifyOrder?.Invoke(netInfo);
        }
        #endregion
    }
}
