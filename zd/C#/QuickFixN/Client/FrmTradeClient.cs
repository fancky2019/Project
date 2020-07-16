using Client.FixUtility;
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
    public partial class FrmTradeClient : Form
    {
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
                  if(this.InvokeRequired)
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
    }
}
