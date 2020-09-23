using CommonClassLib;
using System;
using System.Collections.Concurrent;
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
        Log _logger = LogManager.GetLogger("TradeTool");
        public FrmTradeTool()
        {
            InitializeComponent();
        }

        private void FrmTradeTool_Load(object sender, EventArgs e)
        {
            //SystemInformation.HorizontalScrollBarHeight;
            //SystemInformation.VerticalScrollBarWidth;
            //this.txtClientInPath.Text = @"C:\Users\Administrator\Desktop\2020-07-17\ClientIn_20200717.log";
            this.txtClientInPath.Text = @"C:\\Users\\Administrator\\Desktop\\TradeTool\\20200922\\ClientIn_20200922.log";
            this.dgvClientIn.Columns["ClientInLogTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
            this.dgvClientIn.AutoGenerateColumns = false;
            this.dgvToClient.Columns["ToClientLogTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
            this.dgvToClient.AutoGenerateColumns = false;
        }

        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void btnLoadClientInToClient_Click(object sender, EventArgs e)
        {
            //DateTime contractDate = DateTime.ParseExact(timeStr, "yyyyMMdd-HH:mm:ss.fff", CultureInfo.InvariantCulture);
            //var content = new ClintInToClintLog().ReadClientInData(@"C:\Users\Administrator\Desktop\2020-07-17\ClientIn_20200717.log");

            try
            {


                var clientInDatas = new ClintInToClintLog().ReadClientInData(this.txtClientInPath.Text.Trim());
                var toClientPath = this.txtClientInPath.Text.Trim().Replace("ClientIn", "ToClient");
                var toClientDatas = new ClintInToClintLog().ReadToClientData(toClientPath);

                var systemCodes = this.txtSystemCodes.Text.Trim();
                if (!string.IsNullOrEmpty(systemCodes))
                {
                    var systemCodeList = systemCodes.Split(';').ToList();
                    clientInDatas = clientInDatas.Where(p => systemCodeList.Contains(p.SystemCode)).ToList();
                    toClientDatas = toClientDatas.Where(p => systemCodeList.Contains(p.SystemCode)).ToList();
                }

                this.dgvClientIn.DataSource = null;
                this.dgvClientIn.DataSource = clientInDatas;
                this.dgvToClient.DataSource = null;
                this.dgvToClient.DataSource = toClientDatas;

                //生成order
                ConcurrentDictionary<string, Order> orders = new ConcurrentDictionary<string, Order>();
                Task.Run(() =>
                {
                    clientInDatas.ForEach(p =>
                    {
                        NetInfo netInfo = p.NetInfo;
                        if (netInfo.code == CommandCode.ORDER || netInfo.code == CommandCode.OrderStockHK)
                        {
                            //Order order = new Order();
                            //order.OrderNetInfo = netInfo;
                            //order.TempCommandCode = netInfo.code;

                        }
                        else if (netInfo.code == CommandCode.MODIFY || netInfo.code == CommandCode.ModifyStockHK)
                        {

                        }
                        else if (netInfo.code == CommandCode.CANCEL || netInfo.code == CommandCode.CancelStockHK)
                        {

                        }
                        else
                        {
                            throw new Exception("Can not find appropriate CommandCode");
                        }


                    });

                });
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex.ToString());
            }
        }

        //拖入，释放鼠标此事件发生
        private void txtClientInPath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] rs = (string[])e.Data.GetData(DataFormats.FileDrop);
                var filePath = rs[0];
                this.txtClientInPath.Text = filePath;
            }
        }

        //拖入发生此事件
        private void txtClientInPath_DragEnter(object sender, DragEventArgs e)
        {
            //只允许文件拖放
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] rs = (string[])e.Data.GetData(DataFormats.FileDrop);
                var filePath = rs[0];
                var fileName = Path.GetFileName(filePath);
                if (fileName.StartsWith("ClientIn"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
         
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
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
                if (this.dgvClientIn.SelectedRows.Count > 0)
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
                if (this.dgvToClient.SelectedRows.Count > 0)
                {
                    var selectRow = this.dgvToClient.SelectedRows[0];
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
            this.rtbNetInfo.Text = new NetInfo().ToResponseJson(this.txtNetInfo.Text.Trim());
        }

        private void btnResolveRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNetInfo.Text.Trim()))
            {
                return;
            }
            this.rtbNetInfo.Text = new NetInfo().ToRequestJson(this.txtNetInfo.Text.Trim());
        }

        private void ShowJson(NetInfo netInfo, bool request = true)
        {

            if (netInfo != null)
            {
                var json = request ? netInfo.ToRequestJson() : netInfo.ToResponseJson();
                FrmNetInfoJson frmNetInfoJson = new FrmNetInfoJson(json);
                frmNetInfoJson.Show();
            }
        }

        private NetInfo GetNetInfo(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var selectRow = dgv.SelectedRows[0];
                var netInfo = selectRow.DataBoundItem as ClientInLog;
                return netInfo.NetInfo;
            }
            return null;
        }

        private void dgvClientIn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //单击表头
            if (e.RowIndex == -1)
            {
                return;
            }
            ShowJson(GetNetInfo(this.dgvClientIn));
        }

        private void dgvToClient_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //单击表头
            if (e.RowIndex == -1)
            {
                return;
            }
            ShowJson(GetNetInfo(this.dgvToClient), false);
        }


    }
}
