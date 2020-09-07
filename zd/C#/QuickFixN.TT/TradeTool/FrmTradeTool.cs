using CommonClassLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeTool.Common;
using TradeTool.Model;
using TradeTool.Service;

namespace TradeTool
{
    public partial class FrmTradeTool : Form
    {
        public FrmTradeTool()
        {
            InitializeComponent();
        }

        private void FrmTradeTool_Load(object sender, EventArgs e)
        {
            this.txtClientInPath.Text = @"C:\Users\Administrator\Desktop\2020-07-17\ClientIn_20200717.log";
            this.dgvClientIn.Columns["LogTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
            this.dgvClientIn.AutoGenerateColumns = false;
        }

        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void btnClientIn_Click(object sender, EventArgs e)
        {
            //DateTime contractDate = DateTime.ParseExact(timeStr, "yyyyMMdd-HH:mm:ss.fff", CultureInfo.InvariantCulture);
            //var content = new ClintInToClintLog().ReadClientInData(@"C:\Users\Administrator\Desktop\2020-07-17\ClientIn_20200717.log");

            var clientInDatas = new ClintInToClintLog().ReadClientInData(this.txtClientInPath.Text.Trim());
            var clientInPath = this.txtClientInPath.Text.Trim().Replace("ClientIn", "ToClient");
            var toClientDatas = new ClintInToClintLog().ReadToClientData(clientInPath);
            this.dgvClientIn.DataSource = null;
            this.dgvClientIn.DataSource = clientInDatas;
            this.dgvToClient.DataSource = null;
            this.dgvToClient.DataSource = toClientDatas;
        }


        private void btnView_Click(object sender, EventArgs e)
        {

            openFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            openFileDialog.Filter = "txt files (*.log)|*.log";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtClientInPath.Text = this.openFileDialog.FileName;
            }
        }



        private void dgvClientIn_MouseDown(object sender, MouseEventArgs e)
        {
            var selected = this.dgvClientIn.SelectedRows;
        }

        private void dgvClientIn_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Control) && e.KeyCode == Keys.C)
            {
                if(this.dgvClientIn.SelectedRows.Count>0)
                {
                    var selectRow = this.dgvClientIn.SelectedRows[0];
                    var netInfo = selectRow.DataBoundItem as ClientInLog;
                    Clipboard.SetDataObject(netInfo.NetInfo.MyToString());
                }
             
            }
        }

        private void dgvToClient_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Control) && e.KeyCode == Keys.C)
            {
                if (this.dgvClientIn.SelectedRows.Count > 0)
                {
                    var selectRow = this.dgvClientIn.SelectedRows[0];
                    var netInfo = selectRow.DataBoundItem as ClientInLog;
                    Clipboard.SetDataObject(netInfo.NetInfo.MyToString());
                }

            }
        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNetInfo.Text.Trim()))
            {
                return;
            }
            NetInfo netInfo = new NetInfo();
          
            netInfo.MyReadString(this.txtNetInfo.Text.Trim());
            //this.rtbNetInfo.Text = MessagePackUtility.SerializeToJson(netinfo);
            //this.rtbNetInfo.Text = MessagePackUtility.SerializeToJson(NewtonsoftHelper.JsonSerializeObjectFormat(netinfo));


            StringBuilder sb = new StringBuilder();
            sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(netInfo));
            sb.Append("\r\n");
            //var command = netInfoStr.Substring(0, 8);
            switch (netInfo.code)
            {
                case "ORDER001":
                case "OrdeStHK":
                    OrderResponseInfo orderInfo = new OrderResponseInfo();
                    orderInfo.MyReadString(netInfo.infoT);
                    //@@@@@ICE@BRN2012@1@1@42.59@@1@@@42.59@1@@@@0
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(orderInfo));
                    break;
                case "CANCST01":
                case "CancStHK":
                    CancelResponseInfo cancelInfo = new CancelResponseInfo();
                    cancelInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(cancelInfo));
                    break;
                case "MODIFY01":
                case "ModiStHK":
                    OrderResponseInfo modifyInfo = new OrderResponseInfo();
                    modifyInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(modifyInfo));
                    break;
                case "FILCST01":
                case "FillStHK":
                    FilledResponseInfo filledResponseInfo = new FilledResponseInfo();
                    filledResponseInfo.MyReadString(netInfo.infoT);
                    sb.Append(NewtonsoftHelper.JsonSerializeObjectFormat(filledResponseInfo));
                    break;
                default:
                    MessageBox.Show("订单指令有误！");
                    return;
            }
            this.rtbNetInfo.Text = sb.ToString();
        }

      
    }
}
