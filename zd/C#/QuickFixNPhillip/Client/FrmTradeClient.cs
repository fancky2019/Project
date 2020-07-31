using Client.FixUtility;
using Client.Models;
using Client.Service;
using Client.Utility;
using CommonClassLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    partial class FrmTradeClient : Form
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        TradeClientAppService _tradeClientAppService = null;
        public FrmTradeClient()
        {
            InitializeComponent();
            this.btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            TradeClient.Instance.SocketInitiator.Start();
            TradeClient.Instance.Logon += (msg =>
              {
                  if (this.InvokeRequired)
                  {
                      this.BeginInvoke((MethodInvoker)(() =>
                      {
                          this.btnStart.Enabled = false;
                          this.btnStop.Enabled = true;
                      }));
                  }
                  else
                  {
                      this.btnStart.Enabled = false;
                      this.btnStop.Enabled = true;
                  }

              });

            TradeClient.Instance.LogOut += (msg =>
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)(() =>
                    {
                        this.btnStart.Enabled = true;
                        this.btnStop.Enabled = false;
                    }));
                }
                else
                {
                    this.btnStart.Enabled = true;
                    this.btnStop.Enabled = false;
                }

            });



        }

        private void btnOpenDic_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        internal void btnStop_Click(object sender, EventArgs e)
        {
            TradeClient.Instance.SocketInitiator.Stop();

            TxtFile.SaveTxtFile(ConfigurationManager.AppSettings["OrderIDFilePath"].ToString(), new List<string> { MemoryDataManager.LastOrderID.ToString() });
        }

        private void FrmTradeClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.btnStop.Enabled)
            {
                for (int i = 1; i <= 3; i++)
                {
                    this.btnStop.BackColor = Color.Red;
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(100);
                    this.btnStop.BackColor = Color.Transparent;
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(100);
                }
                e.Cancel = true;
                return;
            }
        }


        private void btnTest_Click(object sender, EventArgs e)
        {
            this.btnTest.Text = ConfigurationManager.AppSettings["RefreshTest"]?.ToString();

            return;
            //Order order = new Order();
            //order.ClientNumber = "dsdsfaadsdas";
            //order.ClientID = 100;
            //new RedisQueue().SaveToRedis<Order>(order);
            Order order = new Order();
            NetInfo netInfo = new NetInfo();
            var orderStr = "ORDER001@@SystemCode1595929502113@0047@@ZD_001@@@@&@@@@@ICE@BRN2012@1@1@42.59@@1@@@42.59@1@@@@0";
            netInfo.MyReadString(orderStr);
            order.OrderNetInfo = netInfo;
            order.SystemCode = order.OrderNetInfo.systemCode;
            string msgStr = "8=FIX.4.2|9=195|35=D|34=53|49=7G9100N|50=rainer|52=20120419-06:20:46.453|56=CME|57=G|142=CN|1=account1|11=15780321875000|21=1|38=30|40=2|44=12.5|54=1|55=90|59=0|60=20120419-06:20:33.859|107=0EJM2|167=FUT|1028=Y|10=053|";
            msgStr = msgStr.Replace('|', (char)1);
            QuickFix.FIX42.Message msg = new QuickFix.FIX42.NewOrderSingle();
            msg.FromString(msgStr, false, null, null);
            order.NewOrderSingle = msgStr;

            //for (int i = 0; i < 11; i++)
            //{
            //    StopwatchHelper.Instance.Stopwatch.Restart();
            //    MessagePackUtility.Serialize<Order>(order);
            //    StopwatchHelper.Instance.Stop();
            //    _nLog.Info($"Serialize1 - {StopwatchHelper.Instance.Stopwatch.ElapsedMilliseconds}");
            //}




            //var bytes = MessagePackUtility.Serialize<Order>(order);
            //Random random = new Random();
            //for (int i = 0; i < 11; i++)
            //{
            //    order.ClientID = random.Next(10000, 100000).ToString();
            //    StopwatchHelper.Instance.Stopwatch.Restart();


            //    RedisHelper.SetCurrentClientOrderIDAndSysytemCode(order.SystemCode, order.ClientID, order.ClientID);
            //    RedisHelper.SaveOrder(order);
            //    StopwatchHelper.Instance.Stop();
            //    _nLog.Info($"Save To Redis - {StopwatchHelper.Instance.Stopwatch.ElapsedMilliseconds}");
            //}


            //RedisHelper.GetQueueClient();

            //StopwatchHelper.Instance.Stopwatch.Restart();
        }

        private OrderForm _orderForm;
        private void btnShowOrderForm_Click(object sender, EventArgs e)
        {
            if (_orderForm == null || _orderForm.IsDisposed)
            {
                _orderForm = new OrderForm();
            }
            _orderForm.Show();
        }
    }
}
