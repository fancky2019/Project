using Client.FixUtility;
using Client.Models;
using Client.Service;
using Client.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    partial class FrmTradeClient : Form
    {
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

        private void btnStop_Click(object sender, EventArgs e)
        {
            TradeClient.Instance.SocketInitiator.Stop();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //Order order = new Order();
            //order.ClientNumber = "dsdsfaadsdas";
            //order.ClientID = 100;
            //new RedisQueue().SaveToRedis<Order>(order);
        }

        private OrderForm _orderForm;
        private void btnShowOrderForm_Click(object sender, EventArgs e)
        {
            if(_orderForm==null|| _orderForm.IsDisposed)
            {
                _orderForm = new OrderForm();
            }
            _orderForm.Show();
        }
    }
}
