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
using ZDFixClient.UserControls.TTNetInfoControl;
using ZDFixClient.UserControls.PSHKNetInfoUserControl;
using NLog;
using ZDFixService.Utility;
using System.Collections.Concurrent;
using ZDFixClient.SocketNettyClient;
using System.Net;
using ZDFixService.Service.RepairOrders;

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
        bool _console;
        ZDFixNettyClient _zDFixNettyClient = null;
        #endregion

        #region Constructor Load
        public UtilityOrderForm(bool console)
        {
            InitializeComponent();


            this._console = console;

            _zDFixNettyClient = new ZDFixNettyClient();


            this.FormClosing += (s, e) => _zDFixNettyClient.Close();

            if (console)
            {
                _zDFixNettyClient.ReceiveMsg += ExecutionReport;

                var ipPort = ConfigurationManager.AppSettings["FixServer"].ToString().Split(':');
                string ip = ipPort[0];
                string port = ipPort[1];
                var iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
                _zDFixNettyClient.Connect(iPEndPoint);

            }
        }


        private void UtilityOrderForm_Load(object sender, EventArgs e)
        {
            try
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


                //如果是控制台操作下单，就不用注册，避免重复注册ExecutionReport
                //窗体关闭要注销事件。
                if (!_console)
                {
                    TradeServiceFactory.ITradeService.ExecutionReport += ExecutionReport;
                }

            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }

        }

        private void LoadNetInfoControls()
        {
            var type = TradeServiceFactory.ITradeService.GetType();
            var trradeService = type.Name;
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

        public void ExecutionReport(string netInfoStr)
        {
            //维护客户端内存数据。
            if (!string.IsNullOrEmpty(netInfoStr))
            {
                NetInfo netInfo = new NetInfo();

                netInfo.MyReadString(netInfoStr);
                OrderInfo orderInfo = null;

                switch (netInfo.code)
                {
                    case "ORDER001":
                    case "OrdeStHK":
                        if (netInfo.errorCode != ErrorCode.SUCCESS)
                        {
                            _newOrderSingleNetInfos.TryRemove(netInfo.systemCode, out _);
                        }

                        break;
                    case "CANCST01":
                    case "CancStHK":
                        if (netInfo.errorCode == ErrorCode.SUCCESS)
                        {
                            _newOrderSingleNetInfos.TryRemove(netInfo.systemCode, out _);
                        }
                        break;
                    case "MODIFY01":
                    case "ModiStHK":

                        if (netInfo.errorCode == ErrorCode.SUCCESS)
                        {
                            NetInfo newOrderSingleNetInfo = null;
                            orderInfo = GetNewOrderSingleNetInfo(netInfo.systemCode, out newOrderSingleNetInfo);

                            OrderResponseInfo orderResponseInfo = new OrderResponseInfo();
                            orderResponseInfo.MyReadString(netInfo.infoT);

                            orderInfo.orderNumber = orderResponseInfo.orderNumber;
                            newOrderSingleNetInfo.infoT = orderInfo.MyToString();
                        }
                        break;
                    case "FILCST01":
                    case "FillStHK":
                        orderInfo = GetNewOrderSingleNetInfo(netInfo.systemCode, out _);
                        if (orderInfo == null)
                        {
                            return;
                        }
                        FilledResponseInfo filledResponseInfo = new FilledResponseInfo();
                        filledResponseInfo.MyReadString(netInfo.infoT);
                        if (filledResponseInfo.filledNumber == orderInfo.orderNumber)
                        {
                            _newOrderSingleNetInfos.TryRemove(netInfo.systemCode, out _);
                        }
                        break;
                    default:
                        MessageBox.Show("订单指令有误！");
                        return;
                }
            }

            //在创建窗口句柄之前，不能在控件上调用 Invoke 或 BeginInvoke。
            if (this.IsHandleCreated)
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    if (!string.IsNullOrEmpty(netInfoStr))
                    {
                        this.lbMsgs.Items.Add(netInfoStr);
                    }

                }));
            }
            else
            {
                _nLog.Info($"下单窗体已关闭 - {netInfoStr}");
            }

        }

        private OrderInfo GetNewOrderSingleNetInfo(string systemCode, out NetInfo newOrderSingleNetInfo)
        {
            //重启_newOrderSingleNetInfos 丢失。客户端测试程序只做简单测试，
            _newOrderSingleNetInfos.TryGetValue(systemCode, out newOrderSingleNetInfo);
            if (newOrderSingleNetInfo == null)
            {
                return null;
            }
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.MyReadString(newOrderSingleNetInfo.infoT);
            return orderInfo;
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
            if (this.lbMsgs.SelectedItem == null)
            {
                return;
            }

            this.rtbNetInfo.Text = new NetInfo().ToResponseJson(this.lbMsgs.SelectedItem.ToString());
        }

        private void OrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_console)
            {
                //窗体关闭要注销事件。
                TradeServiceFactory.ITradeService.ExecutionReport -= ExecutionReport;
            }

        }
        #endregion

        #region 下单
        private void Order(NetInfo netInfo)
        {
            _newOrderSingleNetInfos.TryAdd(netInfo.systemCode, netInfo);

            if (!this._console)
            {
                TradeServiceFactory.ITradeService.Order(netInfo);
            }
            else
            {
                _zDFixNettyClient.SendMsg<SocketMessage<NetInfo>>(netInfo);

                //_zDFixNettyClient.SendMsg<string>(netInfo.MyToString());
            }

        }
        #endregion

        #region 改单
        private void ModifyOrder(ModifyInfo modifyInfo, string systemCode, string commandCode)
        {

            CommonClassLib.NetInfo netInfo = null;
            ////重启客户端内存数据丢失，从服务端找原单信息
            //if (MemoryData.Orders.TryGetValue(systemCode, out Order order))
            //{
            //    netInfo = order.OrderNetInfo.CloneWithNewCode("", commandCode);
            //}
            //else
            //{
            //    MessageBox.Show($"没有找到系统号为:{systemCode}的订单");
            //    return;
            //}


            //重启客户端内存数据丢失。
            if (_newOrderSingleNetInfos.TryGetValue(systemCode, out NetInfo newOrderNetInfo))
            {
                netInfo = newOrderNetInfo.CloneWithNewCode("", commandCode);
            }
            else
            {
                MessageBox.Show($"没有找到系统号为:{systemCode}的订单");
                return;
            }

            netInfo.infoT = modifyInfo.MyToString();

            if (!this._console)
            {
                TradeServiceFactory.ITradeService.Order(netInfo);
            }
            else
            {
                _zDFixNettyClient.SendMsg<SocketMessage<NetInfo>>(netInfo);
            }
        }
        #endregion

        #region 撤单
        private void CancelOrder(CancelInfo cancelInfo, string commandCode)
        {
            CommonClassLib.NetInfo netInfo = null;

            ////重启客户端内存数据丢失，从服务端找原单信息
            //if (MemoryData.Orders.TryGetValue(cancelInfo.systemNo, out Order order))
            //{
            //    netInfo = order.OrderNetInfo.CloneWithNewCode("", commandCode);
            //}
            //else
            //{
            //    MessageBox.Show($"没有找到系统号为:{cancelInfo.systemNo}的订单");
            //    return;
            //}


            //重启客户端内存数据丢失。
            if (_newOrderSingleNetInfos.TryGetValue(cancelInfo.systemNo, out NetInfo newOrderNetInfo))
            {
                netInfo = newOrderNetInfo.CloneWithNewCode("", commandCode);
            }
            else
            {
                MessageBox.Show($"没有找到系统号为:{cancelInfo.systemNo}的订单");
                return;
            }


            netInfo.infoT = cancelInfo.MyToString();
            if (!this._console)
            {
                TradeServiceFactory.ITradeService.Order(netInfo);
            }
            else
            {
                _zDFixNettyClient.SendMsg<SocketMessage<NetInfo>>(netInfo);
            }
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

        #region 
        #endregion

        #region SnowFlake
        private void btnResolve_Click(object sender, EventArgs e)
        {
            long id = long.Parse(this.txtID.Text.Trim());
            int workerIdBits = 10;
            var twepoch = dtpStartDate.Value.Ticks / 10000;
            var sequenceBits = (int)nudSequenceBits.Value;
            this.rtbID.Text = ResolveID(id, workerIdBits, twepoch, sequenceBits);
        }

        public string ResolveID(long id, int workerIdBits, long twepoch, int sequenceBits)
        {
            var bitStr = System.Convert.ToString(id, 2);
            int len = bitStr.Length;

            int timestampLength = len - workerIdBits - sequenceBits;
            int timestampStart = 0;
            int workerIdStart = timestampLength;
            int sequenceStart = workerIdStart + workerIdBits;

            string timestampBit = bitStr.Substring(timestampStart, timestampLength);
            string workerIdBit = bitStr.Substring(workerIdStart, workerIdBits);
            string sequenceBit = bitStr.Substring(sequenceStart, sequenceBits);

            int sequenceInt = Convert.ToInt32(sequenceBit, 2);
            int workerIdInt = Convert.ToInt32(workerIdBit, 2);
            long timestampLong = Convert.ToInt64(timestampBit, 2);

            var generateMillisecond = timestampLong + twepoch;
            string dateTime = new DateTime(generateMillisecond * 10000).ToString("yyyy-MM-dd HH:mm:ss fff");
            var anonymous = new { CreateTime = dateTime, WorkID = workerIdInt, Sequence = sequenceInt };
            var jsonAnonymousStr = NewtonsoftHelper.JsonSerializeObjectFormat(anonymous);
            return jsonAnonymousStr;
        }




        #endregion

        #endregion

        private void btnRepair_Click(object sender, EventArgs e)
        {
            //RepairOrder repairOrder = new RepairOrder();
            //repairOrder.Repair();
        }
    }
}
