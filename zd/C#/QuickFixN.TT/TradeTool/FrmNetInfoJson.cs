using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeTool
{
    public partial class FrmNetInfoJson : Form
    {
        public FrmNetInfoJson(string json)
        {
            InitializeComponent();
            this.rtbNetInfo.ReadOnly = true;
            this.rtbNetInfo.Text = json;
        }
    }
}
