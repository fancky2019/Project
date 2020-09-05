using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockAdapterHKEX
{
    public partial class FrmE2ETest : Form
    {
        private HKEXCommunication _globexCommu = null;
        public FrmE2ETest(HKEXCommunication globexCommu)
        {
            InitializeComponent();
            this._globexCommu = globexCommu;
            this.txtTradeReportID.Text = "3355";
        }



        private void btnTCR_Click(object sender, EventArgs e)
        {
            _globexCommu.TCR(this.txtTradeReportID.Text.Trim(),this.txtTradeID.Text.Trim(),
                             this.txtLastPx.Text.Trim(),this.txtLastQty.Text.Trim(),txtSecurityID.Text.Trim());
        }
    }
}
