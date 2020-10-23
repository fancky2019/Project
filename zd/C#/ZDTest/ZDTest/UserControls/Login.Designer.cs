namespace ZDTest.UserControls
{
    partial class Login
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvMemoryData = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClientNo = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConnectingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConnectedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SendLoginCmdTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiveLogonTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemoryData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.txtClientNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(729, 70);
            this.panel1.TabIndex = 0;
            // 
            // dgvMemoryData
            // 
            this.dgvMemoryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMemoryData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.ConnectingTime,
            this.ConnectedTime,
            this.SendLoginCmdTime,
            this.ReceiveLogonTime});
            this.dgvMemoryData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMemoryData.Location = new System.Drawing.Point(0, 70);
            this.dgvMemoryData.Name = "dgvMemoryData";
            this.dgvMemoryData.RowTemplate.Height = 23;
            this.dgvMemoryData.Size = new System.Drawing.Size(729, 523);
            this.dgvMemoryData.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ClientNo:";
            // 
            // txtClientNo
            // 
            this.txtClientNo.Location = new System.Drawing.Point(106, 25);
            this.txtClientNo.Name = "txtClientNo";
            this.txtClientNo.Size = new System.Drawing.Size(100, 21);
            this.txtClientNo.TabIndex = 1;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(546, 29);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(639, 29);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ClientNo";
            this.Column1.HeaderText = "ClientNo";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Login";
            this.Column2.HeaderText = "Login";
            this.Column2.Name = "Column2";
            // 
            // ConnectingTime
            // 
            this.ConnectingTime.DataPropertyName = "ConnectingTime";
            this.ConnectingTime.HeaderText = "ConnectingTime";
            this.ConnectingTime.Name = "ConnectingTime";
            // 
            // ConnectedTime
            // 
            this.ConnectedTime.DataPropertyName = "ConnectedTime";
            this.ConnectedTime.HeaderText = "ConnectedTime";
            this.ConnectedTime.Name = "ConnectedTime";
            // 
            // SendLoginCmdTime
            // 
            this.SendLoginCmdTime.DataPropertyName = "SendLoginCmdTime";
            this.SendLoginCmdTime.HeaderText = "SendLoginCmdTime";
            this.SendLoginCmdTime.Name = "SendLoginCmdTime";
            this.SendLoginCmdTime.Width = 120;
            // 
            // ReceiveLogonTime
            // 
            this.ReceiveLogonTime.DataPropertyName = "ReceiveLogonTime";
            this.ReceiveLogonTime.HeaderText = "ReceiveLogonTime";
            this.ReceiveLogonTime.Name = "ReceiveLogonTime";
            this.ReceiveLogonTime.Width = 120;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvMemoryData);
            this.Controls.Add(this.panel1);
            this.Name = "Login";
            this.Size = new System.Drawing.Size(729, 593);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemoryData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvMemoryData;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtClientNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConnectingTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConnectedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn SendLoginCmdTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiveLogonTime;
    }
}
