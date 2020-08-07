using Client.Service;
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
using Client.Service.MemoryDataManager;
using Client.Models;
using System.Threading;
using Client.Service.Base;
using Client.Service.ZDCommon;
using System.Configuration;
using Client.Service.ZDCommon;
using Client.UserControls.TTNetInfoControl;
using Client.UserControls.PSHKNetInfoUserControl;

namespace Client
{
    public partial class UtilityOrderForm : Form
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
        public UtilityOrderForm()
        {
            InitializeComponent();
        }


        private void UtilityOrderForm_Load(object sender, EventArgs e)
        {

            LoadNetInfoControls();


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

        private void  LoadNetInfoControls()
        {
            var trradeService = ConfigurationManager.AppSettings["ITradeService"].ToString();
            switch (trradeService)
            {
                case "TTTradeService":
                    TTOrderNetInfoControl tTOrderNetInfoControl = new TTOrderNetInfoControl();
                    tTOrderNetInfoControl.Order += Order;
                    TTModifyNetInfoControl tTModifyNetInfoControl = new TTModifyNetInfoControl();
                    tTModifyNetInfoControl.ModifyOrder += ModifyOrder;
                    TTCancelNetInfoControl tTCancelNetInfoControl = new TTCancelNetInfoControl();
                    tTCancelNetInfoControl.CancelOrder += CancelOrder;

               
                    this.orderTabPage.Controls.Add(tTOrderNetInfoControl);
                    tTOrderNetInfoControl.Dock = DockStyle.Fill;
                    this.modifyTabPage.Controls.Add(tTModifyNetInfoControl);
                    tTModifyNetInfoControl.Dock = DockStyle.Fill;
                    this.cancelTabPage.Controls.Add(tTCancelNetInfoControl);
                    tTCancelNetInfoControl.Dock = DockStyle.Fill;
                    break;
                case "PSHKTradeService":
                    PSHKOrderNetInfoControl pSHKOrderNetInfoControl = new PSHKOrderNetInfoControl();
                    pSHKOrderNetInfoControl.Order += Order;
                    PSHKModifyNetInfoControl pSHKModifyNetInfoControl = new PSHKModifyNetInfoControl();
                    pSHKModifyNetInfoControl.ModifyOrder += ModifyOrder;
                    PSHKCancelNetInfoControl pSHKCancelNetInfoControl = new PSHKCancelNetInfoControl();
                    pSHKCancelNetInfoControl.CancelOrder += CancelOrder;

      
                    this.orderTabPage.Controls.Add(pSHKOrderNetInfoControl);
                    pSHKOrderNetInfoControl.Dock = DockStyle.Fill;
                    this.modifyTabPage.Controls.Add(pSHKModifyNetInfoControl);
                    pSHKModifyNetInfoControl.Dock = DockStyle.Fill;
                    this.cancelTabPage.Controls.Add(pSHKCancelNetInfoControl);
                    pSHKCancelNetInfoControl.Dock = DockStyle.Fill;
                    break;
            }
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
                case "OrdeStHK":
                    OrderResponseInfo orderInfo = new OrderResponseInfo();
                    orderInfo.MyReadString(netinfo.infoT);
                    //@@@@@ICE@BRN2012@1@1@42.59@@1@@@42.59@1@@@@0
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(orderInfo));
                    break;
                case "CANCST01":
                case "CancStHK":
                    CancelResponseInfo cancelInfo = new CancelResponseInfo();
                    cancelInfo.MyReadString(netinfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(cancelInfo));
                    break;
                case "MODIFY01":
                case "ModiStHK":
                    OrderResponseInfo modifyInfo = new OrderResponseInfo();
                    modifyInfo.MyReadString(netinfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(modifyInfo));
                    break;
                case "FILCST01":
                case "FillStHK":
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
        private void Order(NetInfo netInfo)
        {
            TradeServiceFactory.ITradeService.Order(netInfo);

        }
        #endregion

        #region 改单
        private void ModifyOrder(NetInfo netInfo)
        {
            TradeServiceFactory.ITradeService.Order(netInfo);
        }
        #endregion

        #region 撤单
        private void CancelOrder(NetInfo netInfo)
        {

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
