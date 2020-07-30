using QuickFix.Fields;
using QuickFix.FIX42;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZDTradeClientTT
{
    public partial class FrmOrderStatusRequest : Form
    {
        private TTCommunication _tTCommunication = null;
        public FrmOrderStatusRequest(TTCommunication tTCommunication)
        {
            InitializeComponent();
            this._tTCommunication = tTCommunication;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            OrderStatusRequest orderStatusRequest = new OrderStatusRequest();
            orderStatusRequest.Account = new Account(ZDTradeClientTTConfiurations.Instance.Account);
            if (!string.IsNullOrEmpty(this.txtClOrdId.Text.Trim()))
            {
                orderStatusRequest.ClOrdID = new ClOrdID(this.txtClOrdId.Text.Trim());
            }

            if (!string.IsNullOrEmpty(this.txtOrderId.Text.Trim()))
            {
                orderStatusRequest.OrderID = new OrderID();
            }
            _tTCommunication.tradeApp.Send(orderStatusRequest);

        }
    }
}
