using Client.FixUtility;
using Client.Models;
using Client.Service;
using Client.Utility;
using Client.Utility.MemoryDataManager;
using Client.Utility.MemoryDataManager.Persist;
using CommonClassLib;
using System;
using System.Collections.Concurrent;
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
        public FrmTradeClient()
        {
            InitializeComponent();
            this.btnStop.Enabled = false;
        }

        #region 基本
        private void btnStart_Click(object sender, EventArgs e)
        {

            TradeClientAppService.Instance.Start();
            TradeClientAppService.Instance.Logon += (msg =>
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

            TradeClientAppService.Instance.Logout += (msg =>
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
            MemoryData.AppStop = true;
            TradeClientAppService.Instance.Stop();

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
        #endregion

        #region  Test
        private void btnTest_Click(object sender, EventArgs e)
        {

            SQLitePersist sQLitePersist = new SQLitePersist();
            var msgTest = "testUpdate";
            var msgBytes = MessagePackUtility.Serialize<string>(msgTest);

            //sQLitePersist.InsertClientOderID("dssdsdsdsd");
            //SQLiteHelper.InsertOrder(msgBytes);
            SQLiteHelper.UpdateOrder(msgBytes);
            var selectBytes = SQLiteHelper.SelectOrder();
            var selectMsg = MessagePackUtility.Deserialize<string>(selectBytes);
            var strin = MemoryData.IPersist.GetType();
            /*
            20200804 16:37:30:669,259542210830[3] - 192.168.1.114:53114 { (len = 155)ORDER001@20200804000019@0007262813000041@100091@@ZD_001 @ICE@@100091@&ZD_001@@ZD_001@888888@C @ICE@BRN2010@1@1@43.52@@1@@@0.0@1@1@@0@0@BRN@2010@@@@@@@@@@@@0@}
            20200804 16:37:35:287,259552999586[3] - 192.168.1.114:53114 { (len = 164)MODIFY01@20200804000019@0007262813000041@100091@@ZD_001 @ICE@@100091@&ZD_001@@ZD_001@888888@1500000082@ICE @BRN2010@1@1@43.52@0@3@43.50@1@1@C@0.00@0.0@1@@@@@@@@@@@@0@}
            20200804 16:37:38:079,259559551496[3] - 192.168.1.114:53114 { (len = 156)CANCEL01@20200804000019@0007262813000041@100091@@ZD_001 @ICE@@@&ZD_001@192.168.1.207@ZD_001@888888@@0007262813000041@1500000082@ICE @BRN2010@1@1@@0@@@@C@@@@@@}
            20200804 16:38:23:912,259666676505[3] - 192.168.1.114:53114 { (len = 18)TEST0001@@@@@@@@@&}
            */
            string newOrderStr = "ORDER001@000263904J000019@000264094J000113@000363_01@@ZD_001@CBOE@@000363@&zdtmifid@000362@ZD_001@zdtmifid@C@CBOE@VX1908@1@1@15.25@@1@@@0.0@1@1@@0@0@VX@1908";
            var modifyOrderStr = "MODIFY01@20200804000019@0007262813000041@100091@@ZD_001 @ICE@@100091@&ZD_001@@ZD_001@888888@1500000082@ICE @BRN2010@1@1@43.52@0@3@43.50@1@1@C@0.00@0.0@1@@@@@@@@@@@@0@";
            var cancelOrderStr = "CANCEL01@20200804000019@0007262813000041@100091@@ZD_001 @ICE@@@&ZD_001@192.168.1.207@ZD_001@888888@@0007262813000041@1500000082@ICE @BRN2010@1@1@@0@@@@C@@@@@@";

            NetInfo ni = new NetInfo();
            ni.MyReadString(newOrderStr);
            ni.MyReadString(modifyOrderStr);
            ni.MyReadString(cancelOrderStr);

            try
            {
                //this.btnTest.Text = ConfigurationManager.AppSettings["RefreshTest"]?.ToString();
                Order o = new Order();
                var orders = new ConcurrentDictionary<string, Order>();
                orders.TryAdd("1", o);

                //var str = NewtonsoftHelper.SerializeObject(orders);
                //var o1 = NewtonsoftHelper.DeserializeObject<ConcurrentDictionary<string, Order>>(str);
                return;
            }
            catch (Exception ex)
            {

            }


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
            //order.NewOrderSingle = msgStr;

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
        #endregion

        #region OrderForm
        private OrderForm _orderForm;
        private void btnShowOrderForm_Click(object sender, EventArgs e)
        {
            if (_orderForm == null || _orderForm.IsDisposed)
            {
                _orderForm = new OrderForm();
            }
            _orderForm.Show();
        }
        #endregion
    }
}
