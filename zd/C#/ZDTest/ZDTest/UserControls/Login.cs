using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.ViewModel;

namespace ZDTest.UserControls
{
    public partial class Login : UserControl
    {
        private List<User> _users;
        public List<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                this.dgvMemoryData.DataSource = null;
                this.dgvMemoryData.DataSource = value;
                _users = value;
            }
        }
        public Login()
        {
            InitializeComponent();
            _users = new List<User>();
            Users = new List<User>();
            this.dgvMemoryData.AutoGenerateColumns = false;
            this.dgvMemoryData.Columns["ConnectingTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
            this.dgvMemoryData.Columns["ConnectedTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
            this.dgvMemoryData.Columns["SendLoginCmdTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
            this.dgvMemoryData.Columns["ReceiveLogonTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            var clientNo = this.txtClientNo.Text.Trim();
            if (!string.IsNullOrEmpty(clientNo))
            {
                this.dgvMemoryData.DataSource = null;
                this.dgvMemoryData.DataSource = _users.Where(p => p.ClientNo.Contains(clientNo)).ToList();
            }
       
        }
    }
}
