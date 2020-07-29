using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTRecovery
{
    public partial class FrmTTRecovery : Form
    {
        public FrmTTRecovery()
        {
            InitializeComponent();
        }

        private void btnOpenDic_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {

        }
    }
}
