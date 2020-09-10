namespace ZDFixClient
{
    partial class UtilityOrderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRepair = new System.Windows.Forms.Button();
            this.txtClientInPath = new System.Windows.Forms.TextBox();
            this.btnView = new System.Windows.Forms.Button();
            this.txtSystemCodes = new System.Windows.Forms.TextBox();
            this.btnLoadClientInToClient = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.nudSequenceBits = new System.Windows.Forms.NumericUpDown();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.la = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbID = new System.Windows.Forms.RichTextBox();
            this.btnResolve = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnExportOrders = new System.Windows.Forms.Button();
            this.btnOpenDirectory = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbMsgs = new System.Windows.Forms.ListBox();
            this.rtbNetInfo = new System.Windows.Forms.RichTextBox();
            this.testTabControl = new System.Windows.Forms.TabControl();
            this.orderTabPage = new System.Windows.Forms.TabPage();
            this.modifyTabPage = new System.Windows.Forms.TabPage();
            this.cancelTabPage = new System.Windows.Forms.TabPage();
            this.OutSideTabControl = new System.Windows.Forms.TabControl();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSequenceBits)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.testTabControl.SuspendLayout();
            this.OutSideTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(847, 530);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "工具";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(841, 524);
            this.tabControl1.TabIndex = 39;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.panel1);
            this.tabPage5.Controls.Add(this.txtSystemCodes);
            this.tabPage5.Controls.Add(this.btnLoadClientInToClient);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(833, 498);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Orders修复";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnRepair);
            this.panel1.Controls.Add(this.txtClientInPath);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(833, 105);
            this.panel1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "LogClientInPath:";
            // 
            // btnRepair
            // 
            this.btnRepair.Location = new System.Drawing.Point(743, 13);
            this.btnRepair.Name = "btnRepair";
            this.btnRepair.Size = new System.Drawing.Size(75, 23);
            this.btnRepair.TabIndex = 0;
            this.btnRepair.Text = "Repair";
            this.btnRepair.UseVisualStyleBackColor = true;
            this.btnRepair.Click += new System.EventHandler(this.btnRepair_Click);
            // 
            // txtClientInPath
            // 
            this.txtClientInPath.Location = new System.Drawing.Point(132, 15);
            this.txtClientInPath.Name = "txtClientInPath";
            this.txtClientInPath.ReadOnly = true;
            this.txtClientInPath.Size = new System.Drawing.Size(540, 21);
            this.txtClientInPath.TabIndex = 3;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(678, 13);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(44, 23);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "...";
            this.btnView.UseVisualStyleBackColor = true;
            // 
            // txtSystemCodes
            // 
            this.txtSystemCodes.Location = new System.Drawing.Point(132, 122);
            this.txtSystemCodes.Name = "txtSystemCodes";
            this.txtSystemCodes.Size = new System.Drawing.Size(539, 21);
            this.txtSystemCodes.TabIndex = 6;
            // 
            // btnLoadClientInToClient
            // 
            this.btnLoadClientInToClient.Location = new System.Drawing.Point(704, 120);
            this.btnLoadClientInToClient.Name = "btnLoadClientInToClient";
            this.btnLoadClientInToClient.Size = new System.Drawing.Size(59, 23);
            this.btnLoadClientInToClient.TabIndex = 0;
            this.btnLoadClientInToClient.Text = "Read";
            this.btnLoadClientInToClient.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "SystemCode:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.nudSequenceBits);
            this.tabPage4.Controls.Add(this.dtpStartDate);
            this.tabPage4.Controls.Add(this.la);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.rtbID);
            this.tabPage4.Controls.Add(this.btnResolve);
            this.tabPage4.Controls.Add(this.txtID);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(833, 498);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "SnowFlake";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "ID:";
            // 
            // nudSequenceBits
            // 
            this.nudSequenceBits.Location = new System.Drawing.Point(161, 10);
            this.nudSequenceBits.Name = "nudSequenceBits";
            this.nudSequenceBits.Size = new System.Drawing.Size(189, 21);
            this.nudSequenceBits.TabIndex = 8;
            this.nudSequenceBits.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "yyyy-MM-dd";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(427, 10);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(102, 21);
            this.dtpStartDate.TabIndex = 7;
            this.dtpStartDate.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // la
            // 
            this.la.AutoSize = true;
            this.la.Location = new System.Drawing.Point(356, 14);
            this.la.Name = "la";
            this.la.Size = new System.Drawing.Size(65, 12);
            this.la.TabIndex = 6;
            this.la.Text = "StartDate:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "SequenceBits:";
            // 
            // rtbID
            // 
            this.rtbID.Location = new System.Drawing.Point(161, 76);
            this.rtbID.Name = "rtbID";
            this.rtbID.Size = new System.Drawing.Size(368, 270);
            this.rtbID.TabIndex = 3;
            this.rtbID.Text = "";
            // 
            // btnResolve
            // 
            this.btnResolve.Location = new System.Drawing.Point(454, 42);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(75, 23);
            this.btnResolve.TabIndex = 2;
            this.btnResolve.Text = "Resolve";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(161, 44);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(189, 21);
            this.txtID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ResolvedJson:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnExportOrders);
            this.tabPage3.Controls.Add(this.btnOpenDirectory);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(833, 498);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "工具";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnExportOrders
            // 
            this.btnExportOrders.Location = new System.Drawing.Point(19, 22);
            this.btnExportOrders.Name = "btnExportOrders";
            this.btnExportOrders.Size = new System.Drawing.Size(75, 23);
            this.btnExportOrders.TabIndex = 38;
            this.btnExportOrders.Text = "导出订单";
            this.btnExportOrders.UseVisualStyleBackColor = true;
            this.btnExportOrders.Click += new System.EventHandler(this.btnExportOrders_Click);
            // 
            // btnOpenDirectory
            // 
            this.btnOpenDirectory.Location = new System.Drawing.Point(722, 22);
            this.btnOpenDirectory.Name = "btnOpenDirectory";
            this.btnOpenDirectory.Size = new System.Drawing.Size(68, 23);
            this.btnOpenDirectory.TabIndex = 36;
            this.btnOpenDirectory.Text = "目录";
            this.btnOpenDirectory.UseVisualStyleBackColor = true;
            this.btnOpenDirectory.Click += new System.EventHandler(this.btnOpenDirectory_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbMsgs);
            this.tabPage1.Controls.Add(this.rtbNetInfo);
            this.tabPage1.Controls.Add(this.testTabControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(847, 530);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "测试";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbMsgs
            // 
            this.lbMsgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMsgs.FormattingEnabled = true;
            this.lbMsgs.HorizontalScrollbar = true;
            this.lbMsgs.ItemHeight = 12;
            this.lbMsgs.Location = new System.Drawing.Point(328, 3);
            this.lbMsgs.Name = "lbMsgs";
            this.lbMsgs.Size = new System.Drawing.Size(516, 314);
            this.lbMsgs.TabIndex = 21;
            this.lbMsgs.SelectedIndexChanged += new System.EventHandler(this.lbMsgs_SelectedIndexChanged);
            this.lbMsgs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbMsgs_KeyUp);
            // 
            // rtbNetInfo
            // 
            this.rtbNetInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbNetInfo.Location = new System.Drawing.Point(328, 317);
            this.rtbNetInfo.Name = "rtbNetInfo";
            this.rtbNetInfo.ReadOnly = true;
            this.rtbNetInfo.Size = new System.Drawing.Size(516, 210);
            this.rtbNetInfo.TabIndex = 14;
            this.rtbNetInfo.Text = "";
            // 
            // testTabControl
            // 
            this.testTabControl.Controls.Add(this.orderTabPage);
            this.testTabControl.Controls.Add(this.modifyTabPage);
            this.testTabControl.Controls.Add(this.cancelTabPage);
            this.testTabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.testTabControl.Location = new System.Drawing.Point(3, 3);
            this.testTabControl.Name = "testTabControl";
            this.testTabControl.SelectedIndex = 0;
            this.testTabControl.Size = new System.Drawing.Size(325, 524);
            this.testTabControl.TabIndex = 12;
            // 
            // orderTabPage
            // 
            this.orderTabPage.Location = new System.Drawing.Point(4, 22);
            this.orderTabPage.Name = "orderTabPage";
            this.orderTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.orderTabPage.Size = new System.Drawing.Size(317, 498);
            this.orderTabPage.TabIndex = 0;
            this.orderTabPage.Text = "下单";
            this.orderTabPage.UseVisualStyleBackColor = true;
            // 
            // modifyTabPage
            // 
            this.modifyTabPage.Location = new System.Drawing.Point(4, 22);
            this.modifyTabPage.Name = "modifyTabPage";
            this.modifyTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.modifyTabPage.Size = new System.Drawing.Size(317, 498);
            this.modifyTabPage.TabIndex = 1;
            this.modifyTabPage.Text = "改单";
            this.modifyTabPage.UseVisualStyleBackColor = true;
            // 
            // cancelTabPage
            // 
            this.cancelTabPage.Location = new System.Drawing.Point(4, 22);
            this.cancelTabPage.Name = "cancelTabPage";
            this.cancelTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.cancelTabPage.Size = new System.Drawing.Size(317, 498);
            this.cancelTabPage.TabIndex = 2;
            this.cancelTabPage.Text = "撤单";
            this.cancelTabPage.UseVisualStyleBackColor = true;
            // 
            // OutSideTabControl
            // 
            this.OutSideTabControl.Controls.Add(this.tabPage1);
            this.OutSideTabControl.Controls.Add(this.tabPage2);
            this.OutSideTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutSideTabControl.Location = new System.Drawing.Point(0, 0);
            this.OutSideTabControl.Name = "OutSideTabControl";
            this.OutSideTabControl.SelectedIndex = 0;
            this.OutSideTabControl.Size = new System.Drawing.Size(855, 556);
            this.OutSideTabControl.TabIndex = 0;
            // 
            // UtilityOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 556);
            this.Controls.Add(this.OutSideTabControl);
            this.MaximizeBox = false;
            this.Name = "UtilityOrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrderForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrderForm_FormClosing);
            this.Load += new System.EventHandler(this.UtilityOrderForm_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSequenceBits)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.testTabControl.ResumeLayout(false);
            this.OutSideTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnExportOrders;
        private System.Windows.Forms.Button btnOpenDirectory;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox lbMsgs;
        private System.Windows.Forms.RichTextBox rtbNetInfo;
        private System.Windows.Forms.TabControl testTabControl;
        private System.Windows.Forms.TabPage orderTabPage;
        private System.Windows.Forms.TabPage modifyTabPage;
        private System.Windows.Forms.TabPage cancelTabPage;
        private System.Windows.Forms.TabControl OutSideTabControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbID;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label la;
        private System.Windows.Forms.NumericUpDown nudSequenceBits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnRepair;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSystemCodes;
        private System.Windows.Forms.TextBox txtClientInPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnLoadClientInToClient;
    }
}