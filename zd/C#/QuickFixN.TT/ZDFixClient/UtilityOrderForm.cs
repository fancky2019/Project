using ZDFixService.Service;
using ZDFixService.Service;
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
using ZDFixService.Service.MemoryDataManager;
using ZDFixService.Models;
using System.Threading;
using ZDFixService.Service.Base;
using ZDFixService.Service.ZDCommon;
using System.Configuration;
using ZDFixService.Service.ZDCommon;
using ZDFixClient.UserControls.TTNetInfoControl;
using ZDFixClient.UserControls.PSHKNetInfoUserControl;
using NLog;
using ZDFixService.Utility;
using System.Collections.Concurrent;

namespace ZDFixClient
{
    public partial class UtilityOrderForm : Form
    {
        #region  私有字段
        private static readonly Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
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

        private ConcurrentDictionary<string, NetInfo> _newOrderSingleNetInfos = new ConcurrentDictionary<string, NetInfo>();
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

        private void LoadNetInfoControls()
        {
            var type = TradeServiceFactory.ITradeService.GetType();
            var trradeService = type.Name;// ConfigurationManager.AppSettings["ITradeService"].ToString();
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

        private void ExecutionReport(string netInfoStr)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                if (!string.IsNullOrEmpty(netInfoStr))
                {
                    NetInfo netInfo = new NetInfo();

                    netInfo.MyReadString(netInfoStr);

                    //StringBuilder sb = new StringBuilder();
                    //sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(netinfo));
                    //sb.Append("\r\n");
                    //var command = netInfoStr.Substring(0, 8);

                    switch (netInfo.code)
                    {
                        case "ORDER001":
                        case "OrdeStHK":
                            //下单失败
                            if (netInfo.errorCode == ErrorCode.ERR_ORDER_0000)
                            {
                                _newOrderSingleNetInfos.TryRemove(netInfo.systemCode, out _);
                            }
                            break;
                        case "CANCST01":
                        case "CancStHK":
                            //撤单失败
                            if (netInfo.errorCode != ErrorCode.ERR_ORDER_0014)
                            {
                                _newOrderSingleNetInfos.TryRemove(netInfo.systemCode, out _);
                            }
                            break;
                        case "FILCST01":
                        case "FillStHK":
                            //此处待优化
                            //FilledResponseInfo filledResponseInfo = new FilledResponseInfo();
                            //filledResponseInfo.filledNumber==
                            ////完全成交
                            _newOrderSingleNetInfos.TryRemove(netInfo.systemCode, out _);
                            break;
                        default:
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(netInfoStr))
                {
                    this.lbMsgs.Items.Add(netInfoStr);
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
            NetInfo netInfo = new NetInfo();
            var netInfoStr = this.lbMsgs.SelectedItem.ToString();
            netInfo.MyReadString(netInfoStr);
            //this.rtbNetInfo.Text = MessagePackUtility.SerializeToJson(netinfo);
            //this.rtbNetInfo.Text = MessagePackUtility.SerializeToJson(NewtonsoftHelper.JsonSerializeObjectFormat(netinfo));


            StringBuilder sb = new StringBuilder();
            sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(netInfo));
            sb.Append("\r\n");
            //var command = netInfoStr.Substring(0, 8);
            switch (netInfo.code)
            {
                case "ORDER001":
                case "OrdeStHK":
                    OrderResponseInfo orderInfo = new OrderResponseInfo();
                    orderInfo.MyReadString(netInfo.infoT);
                    //@@@@@ICE@BRN2012@1@1@42.59@@1@@@42.59@1@@@@0
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(orderInfo));
                    break;
                case "CANCST01":
                case "CancStHK":
                    CancelResponseInfo cancelInfo = new CancelResponseInfo();
                    cancelInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(cancelInfo));
                    break;
                case "MODIFY01":
                case "ModiStHK":
                    OrderResponseInfo modifyInfo = new OrderResponseInfo();
                    modifyInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(modifyInfo));
                    break;
                case "FILCST01":
                case "FillStHK":
                    FilledResponseInfo filledResponseInfo = new FilledResponseInfo();
                    filledResponseInfo.MyReadString(netInfo.infoT);
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
            _newOrderSingleNetInfos.TryAdd(netInfo.systemCode, netInfo);
        }
        #endregion

        #region 改单
        private void ModifyOrder(ModifyInfo modifyInfo, string systemCode, string commandCode)
        {
            CommonClassLib.NetInfo netInfo = null;
            if (_newOrderSingleNetInfos.TryGetValue(systemCode, out NetInfo newOrderNetInfo))
            {
                netInfo = newOrderNetInfo.CloneWithNewCode("", commandCode);
                //    netInfo = order.OrderNetInfo.Clone();
                //    netInfo.code = TradeBaseDataConfig.GetCommandCode(ConfigurationManager.AppSettings["ITradeService"].ToString(), ZDFixService.Service.ZDCommon.CommandType.Modify);
            }
            else
            {
                MessageBox.Show($"没有找到系统号为:{systemCode}的订单");
                return;
            }

            netInfo.infoT = modifyInfo.MyToString();
            TradeServiceFactory.ITradeService.Order(netInfo);
        }
        #endregion

        #region 撤单
        private void CancelOrder(CancelInfo cancelInfo, string commandCode)
        {
            CommonClassLib.NetInfo netInfo = null;
            if (_newOrderSingleNetInfos.TryGetValue(cancelInfo.systemNo, out NetInfo newOrderNetInfo))
            {
                //netInfo = order.OrderNetInfo.CloneWithNewCode("", CommandCode.CANCEL);
                //netInfo = order.OrderNetInfo.Clone();
                //netInfo.code = TradeBaseDataConfig.GetCommandCode(ConfigurationManager.AppSettings["ITradeService"].ToString(), ZDFixService.Service.ZDCommon.CommandType.Cancel);

                netInfo = newOrderNetInfo.CloneWithNewCode("", commandCode);
            }
            else
            {
                MessageBox.Show($"没有找到系统号为:{cancelInfo.systemNo}的订单");
                return;
            }
            netInfo.infoT = cancelInfo.MyToString();
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
