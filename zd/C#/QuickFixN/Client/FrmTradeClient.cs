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
        }
        private void btnStart_Click(object sender, EventArgs e)
        {

            TradeClient.Instance.SocketInitiator.Start();
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
