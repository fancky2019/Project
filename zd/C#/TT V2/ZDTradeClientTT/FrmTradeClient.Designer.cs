namespace ZDTradeClientTT
{
    partial class FrmTradeClient
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.ckTestMode = new System.Windows.Forms.CheckBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnOrderForm = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnOpenDic = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCreateOrderModelFile = new System.Windows.Forms.Button();
            this.btnNetInfo = new System.Windows.Forms.Button();
            this.txtNetInfo = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(18, 43);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(63, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // ckTestMode
            // 
            this.ckTestMode.AutoSize = true;
            this.ckTestMode.Location = new System.Drawing.Point(18, 21);
            this.ckTestMode.Name = "ckTestMode";
            this.ckTestMode.Size = new System.Drawing.Size(78, 16);
            this.ckTestMode.TabIndex = 5;
            this.ckTestMode.Text = "test mode";
            this.ckTestMode.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(18, 77);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(63, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // btnOrderForm
            // 
            this.btnOrderForm.Enabled = false;
            this.btnOrderForm.Location = new System.Drawing.Point(121, 43);
            this.btnOrderForm.Name = "btnOrderForm";
            this.btnOrderForm.Size = new System.Drawing.Size(81, 23);
            this.btnOrderForm.TabIndex = 3;
            this.btnOrderForm.Text = "Order form";
            this.btnOrderForm.UseVisualStyleBackColor = true;
            this.btnOrderForm.Click += new System.EventHandler(this.btnOrderForm_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(8, 19);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(115, 23);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnOpenDic
            // 
            this.btnOpenDic.Location = new System.Drawing.Point(313, 19);
            this.btnOpenDic.Name = "btnOpenDic";
            this.btnOpenDic.Size = new System.Drawing.Size(75, 23);
            this.btnOpenDic.TabIndex = 18;
            this.btnOpenDic.Text = "目录";
            this.btnOpenDic.UseVisualStyleBackColor = true;
            this.btnOpenDic.Click += new System.EventHandler(this.btnOpenDic_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(399, 238);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.ckTestMode);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Controls.Add(this.btnOrderForm);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(391, 212);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Order";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtNetInfo);
            this.tabPage2.Controls.Add(this.btnNetInfo);
            this.tabPage2.Controls.Add(this.btnCreateOrderModelFile);
            this.tabPage2.Controls.Add(this.btnTest);
            this.tabPage2.Controls.Add(this.btnOpenDic);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(391, 212);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tools";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCreateOrderModelFile
            // 
            this.btnCreateOrderModelFile.Location = new System.Drawing.Point(8, 62);
            this.btnCreateOrderModelFile.Name = "btnCreateOrderModelFile";
            this.btnCreateOrderModelFile.Size = new System.Drawing.Size(115, 23);
            this.btnCreateOrderModelFile.TabIndex = 19;
            this.btnCreateOrderModelFile.Text = "生成合约配置文件";
            this.btnCreateOrderModelFile.UseVisualStyleBackColor = true;
            this.btnCreateOrderModelFile.Click += new System.EventHandler(this.BtnCreateOrderModelFile_Click);
            // 
            // btnNetInfo
            // 
            this.btnNetInfo.Location = new System.Drawing.Point(308, 124);
            this.btnNetInfo.Name = "btnNetInfo";
            this.btnNetInfo.Size = new System.Drawing.Size(75, 23);
            this.btnNetInfo.TabIndex = 20;
            this.btnNetInfo.Text = "NetInfo";
            this.btnNetInfo.UseVisualStyleBackColor = true;
            this.btnNetInfo.Click += new System.EventHandler(this.btnNetInfo_Click);
            // 
            // txtNetInfo
            // 
            this.txtNetInfo.Location = new System.Drawing.Point(8, 125);
            this.txtNetInfo.Name = "txtNetInfo";
            this.txtNetInfo.Size = new System.Drawing.Size(294, 21);
            this.txtNetInfo.TabIndex = 21;
            this.txtNetInfo.Text = "ORDER001@20200630000002@000726281T000002@100091@@ZD_001@ICE@@100091@&ZD_001@@ZD_0" +
    "01@888888@C@ICE@BRN2009@1@1@42@@1@@@0.0@1@1@@0@0@BRN@2009@@@@@@@@@@@@0@";
            // 
            // FrmTradeClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 238);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "FrmTradeClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TT7 Trade Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        //private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnOrderForm;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.CheckBox ckTestMode;
        private System.Windows.Forms.Button btnOpenDic;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCreateOrderModelFile;
        private System.Windows.Forms.Button btnNetInfo;
        private System.Windows.Forms.TextBox txtNetInfo;
    }
}

