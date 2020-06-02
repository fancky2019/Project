namespace TTMarketAdapter
{
    partial class FrmMarketAdapter
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.sfdOperations = new System.Windows.Forms.SaveFileDialog();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnFactor = new System.Windows.Forms.Button();
            this.btnLogAnalysis = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblMonth = new System.Windows.Forms.Label();
            this.nudMonths = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFiltrateContracts = new System.Windows.Forms.Button();
            this.btnOpenDic = new System.Windows.Forms.Button();
            this.btnExportOperations = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStatistics = new System.Windows.Forms.Label();
            this.btnLoadContracts = new System.Windows.Forms.Button();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnProductNoContract = new System.Windows.Forms.Button();
            this.btnEchangeProductSort = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnGetContracts = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOpeningPrice = new System.Windows.Forms.TextBox();
            this.txtzdCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNew_PreveSett = new System.Windows.Forms.TextBox();
            this.txtPreviousSettlementPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbUpdateContract = new System.Windows.Forms.CheckBox();
            this.cbDebugMode = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonths)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnFactor);
            this.tabPage4.Controls.Add(this.btnLogAnalysis);
            this.tabPage4.Controls.Add(this.btnTest);
            this.tabPage4.Controls.Add(this.lblMonth);
            this.tabPage4.Controls.Add(this.nudMonths);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.btnFiltrateContracts);
            this.tabPage4.Controls.Add(this.btnOpenDic);
            this.tabPage4.Controls.Add(this.btnExportOperations);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.lblStatistics);
            this.tabPage4.Controls.Add(this.btnLoadContracts);
            this.tabPage4.Controls.Add(this.btnStatistics);
            this.tabPage4.Controls.Add(this.btnProductNoContract);
            this.tabPage4.Controls.Add(this.btnEchangeProductSort);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(505, 212);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "小工具";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnFactor
            // 
            this.btnFactor.Location = new System.Drawing.Point(416, 35);
            this.btnFactor.Name = "btnFactor";
            this.btnFactor.Size = new System.Drawing.Size(86, 23);
            this.btnFactor.TabIndex = 24;
            this.btnFactor.Text = "更新倍率配置";
            this.btnFactor.UseVisualStyleBackColor = true;
            this.btnFactor.Click += new System.EventHandler(this.BtnFactor_Click);
            // 
            // btnLogAnalysis
            // 
            this.btnLogAnalysis.Location = new System.Drawing.Point(416, 6);
            this.btnLogAnalysis.Name = "btnLogAnalysis";
            this.btnLogAnalysis.Size = new System.Drawing.Size(86, 23);
            this.btnLogAnalysis.TabIndex = 23;
            this.btnLogAnalysis.Text = "行情日志分析";
            this.btnLogAnalysis.UseVisualStyleBackColor = true;
            this.btnLogAnalysis.Click += new System.EventHandler(this.BtnLogAnalysis_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(424, 145);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 22;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(185, 188);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(101, 12);
            this.lblMonth.TabIndex = 21;
            this.lblMonth.Text = "最终今后月份数：";
            // 
            // nudMonths
            // 
            this.nudMonths.Location = new System.Drawing.Point(93, 151);
            this.nudMonths.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMonths.Name = "nudMonths";
            this.nudMonths.Size = new System.Drawing.Size(73, 21);
            this.nudMonths.TabIndex = 20;
            this.nudMonths.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "今后月份数：";
            // 
            // btnFiltrateContracts
            // 
            this.btnFiltrateContracts.Location = new System.Drawing.Point(187, 151);
            this.btnFiltrateContracts.Name = "btnFiltrateContracts";
            this.btnFiltrateContracts.Size = new System.Drawing.Size(141, 23);
            this.btnFiltrateContracts.TabIndex = 18;
            this.btnFiltrateContracts.Text = "过滤合约数据";
            this.btnFiltrateContracts.UseVisualStyleBackColor = true;
            this.btnFiltrateContracts.Click += new System.EventHandler(this.btnFiltrateContracts_Click);
            // 
            // btnOpenDic
            // 
            this.btnOpenDic.Location = new System.Drawing.Point(424, 177);
            this.btnOpenDic.Name = "btnOpenDic";
            this.btnOpenDic.Size = new System.Drawing.Size(75, 23);
            this.btnOpenDic.TabIndex = 17;
            this.btnOpenDic.Text = "目录";
            this.btnOpenDic.UseVisualStyleBackColor = true;
            this.btnOpenDic.Click += new System.EventHandler(this.btnOpenDic_Click);
            // 
            // btnExportOperations
            // 
            this.btnExportOperations.Location = new System.Drawing.Point(25, 122);
            this.btnExportOperations.Name = "btnExportOperations";
            this.btnExportOperations.Size = new System.Drawing.Size(141, 23);
            this.btnExportOperations.TabIndex = 6;
            this.btnExportOperations.Text = "导出期权合约文件";
            this.btnExportOperations.UseVisualStyleBackColor = true;
            this.btnExportOperations.Click += new System.EventHandler(this.btnExportOperations_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(185, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(209, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "*若Start按钮已启动，请勿点击此按钮";
            // 
            // lblStatistics
            // 
            this.lblStatistics.AutoSize = true;
            this.lblStatistics.Location = new System.Drawing.Point(185, 40);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(41, 12);
            this.lblStatistics.TabIndex = 4;
            this.lblStatistics.Text = "label7";
            // 
            // btnLoadContracts
            // 
            this.btnLoadContracts.Location = new System.Drawing.Point(25, 6);
            this.btnLoadContracts.Name = "btnLoadContracts";
            this.btnLoadContracts.Size = new System.Drawing.Size(141, 23);
            this.btnLoadContracts.TabIndex = 3;
            this.btnLoadContracts.Text = "加载合约数据";
            this.btnLoadContracts.UseVisualStyleBackColor = true;
            this.btnLoadContracts.Click += new System.EventHandler(this.btnLoadContracts_Click);
            // 
            // btnStatistics
            // 
            this.btnStatistics.Location = new System.Drawing.Point(25, 35);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(141, 23);
            this.btnStatistics.TabIndex = 2;
            this.btnStatistics.Text = "统计合约分类";
            this.btnStatistics.UseVisualStyleBackColor = true;
            this.btnStatistics.Click += new System.EventHandler(this.btnStatistics_Click);
            // 
            // btnProductNoContract
            // 
            this.btnProductNoContract.Location = new System.Drawing.Point(25, 64);
            this.btnProductNoContract.Name = "btnProductNoContract";
            this.btnProductNoContract.Size = new System.Drawing.Size(141, 23);
            this.btnProductNoContract.TabIndex = 1;
            this.btnProductNoContract.Text = "没有取到合约的品种";
            this.btnProductNoContract.UseVisualStyleBackColor = true;
            this.btnProductNoContract.Click += new System.EventHandler(this.btnProductNoContract_Click);
            // 
            // btnEchangeProductSort
            // 
            this.btnEchangeProductSort.Location = new System.Drawing.Point(25, 93);
            this.btnEchangeProductSort.Name = "btnEchangeProductSort";
            this.btnEchangeProductSort.Size = new System.Drawing.Size(141, 23);
            this.btnEchangeProductSort.TabIndex = 0;
            this.btnEchangeProductSort.Text = "交易所分组";
            this.btnEchangeProductSort.UseVisualStyleBackColor = true;
            this.btnEchangeProductSort.Click += new System.EventHandler(this.btnEchangeProductSort_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnGetContracts);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(505, 212);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "盘中操作";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnGetContracts
            // 
            this.btnGetContracts.Location = new System.Drawing.Point(427, 6);
            this.btnGetContracts.Name = "btnGetContracts";
            this.btnGetContracts.Size = new System.Drawing.Size(75, 23);
            this.btnGetContracts.TabIndex = 18;
            this.btnGetContracts.Text = " 取合约";
            this.btnGetContracts.UseVisualStyleBackColor = true;
            this.btnGetContracts.Click += new System.EventHandler(this.BtnGetContracts_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtOpeningPrice);
            this.groupBox2.Controls.Add(this.txtzdCode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnSet);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNew_PreveSett);
            this.groupBox2.Controls.Add(this.txtPreviousSettlementPrice);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 164);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "开盘价：";
            // 
            // txtOpeningPrice
            // 
            this.txtOpeningPrice.Location = new System.Drawing.Point(68, 102);
            this.txtOpeningPrice.Name = "txtOpeningPrice";
            this.txtOpeningPrice.Size = new System.Drawing.Size(106, 21);
            this.txtOpeningPrice.TabIndex = 22;
            // 
            // txtzdCode
            // 
            this.txtzdCode.Location = new System.Drawing.Point(68, 20);
            this.txtzdCode.Name = "txtzdCode";
            this.txtzdCode.Size = new System.Drawing.Size(106, 21);
            this.txtzdCode.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "合约：";
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(103, 135);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(77, 23);
            this.btnSet.TabIndex = 16;
            this.btnSet.Text = "设置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnNew_PreveSett_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "前结算价：";
            // 
            // txtNew_PreveSett
            // 
            this.txtNew_PreveSett.Location = new System.Drawing.Point(68, 47);
            this.txtNew_PreveSett.Name = "txtNew_PreveSett";
            this.txtNew_PreveSett.Size = new System.Drawing.Size(106, 21);
            this.txtNew_PreveSett.TabIndex = 15;
            // 
            // txtPreviousSettlementPrice
            // 
            this.txtPreviousSettlementPrice.Location = new System.Drawing.Point(68, 74);
            this.txtPreviousSettlementPrice.Name = "txtPreviousSettlementPrice";
            this.txtPreviousSettlementPrice.Size = new System.Drawing.Size(106, 21);
            this.txtPreviousSettlementPrice.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "今结算价：";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(505, 212);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "首页";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbUpdateContract);
            this.groupBox1.Controls.Add(this.cbDebugMode);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 100);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "启动/停止服务";
            // 
            // cbUpdateContract
            // 
            this.cbUpdateContract.AutoSize = true;
            this.cbUpdateContract.Checked = true;
            this.cbUpdateContract.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUpdateContract.Location = new System.Drawing.Point(11, 20);
            this.cbUpdateContract.Name = "cbUpdateContract";
            this.cbUpdateContract.Size = new System.Drawing.Size(72, 16);
            this.cbUpdateContract.TabIndex = 17;
            this.cbUpdateContract.Text = "更新合约";
            this.cbUpdateContract.UseVisualStyleBackColor = true;
            // 
            // cbDebugMode
            // 
            this.cbDebugMode.AutoSize = true;
            this.cbDebugMode.Location = new System.Drawing.Point(108, 20);
            this.cbDebugMode.Name = "cbDebugMode";
            this.cbDebugMode.Size = new System.Drawing.Size(72, 16);
            this.cbDebugMode.TabIndex = 5;
            this.cbDebugMode.Text = "调试模式";
            this.cbDebugMode.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(11, 42);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(72, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(11, 71);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(72, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(513, 238);
            this.tabControl1.TabIndex = 17;
            // 
            // FrmMarketAdapter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 238);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "FrmMarketAdapter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TTMarketAdapter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonths)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog sfdOperations;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnFactor;
        private System.Windows.Forms.Button btnLogAnalysis;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.NumericUpDown nudMonths;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFiltrateContracts;
        private System.Windows.Forms.Button btnOpenDic;
        private System.Windows.Forms.Button btnExportOperations;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblStatistics;
        private System.Windows.Forms.Button btnLoadContracts;
        private System.Windows.Forms.Button btnStatistics;
        private System.Windows.Forms.Button btnProductNoContract;
        private System.Windows.Forms.Button btnEchangeProductSort;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPreviousSettlementPrice;
        private System.Windows.Forms.TextBox txtzdCode;
        private System.Windows.Forms.TextBox txtNew_PreveSett;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbUpdateContract;
        private System.Windows.Forms.CheckBox cbDebugMode;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGetContracts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOpeningPrice;
    }
}

