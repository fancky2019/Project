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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.btnResolve = new System.Windows.Forms.Button();
            this.rtbID = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.la = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.nudSequenceBits = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.testTabControl.SuspendLayout();
            this.OutSideTabControl.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSequenceBits)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(835, 530);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "工具";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(835, 530);
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
            this.lbMsgs.Size = new System.Drawing.Size(504, 314);
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
            this.rtbNetInfo.Size = new System.Drawing.Size(504, 210);
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
            this.orderTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.orderTabPage.Size = new System.Drawing.Size(317, 498);
            this.orderTabPage.TabIndex = 0;
            this.orderTabPage.Text = "下单";
            this.orderTabPage.UseVisualStyleBackColor = true;
            // 
            // modifyTabPage
            // 
            this.modifyTabPage.Location = new System.Drawing.Point(4, 22);
            this.modifyTabPage.Name = "modifyTabPage";
            this.modifyTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.modifyTabPage.Size = new System.Drawing.Size(317, 500);
            this.modifyTabPage.TabIndex = 1;
            this.modifyTabPage.Text = "改单";
            this.modifyTabPage.UseVisualStyleBackColor = true;
            // 
            // cancelTabPage
            // 
            this.cancelTabPage.Location = new System.Drawing.Point(4, 22);
            this.cancelTabPage.Name = "cancelTabPage";
            this.cancelTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.cancelTabPage.Size = new System.Drawing.Size(317, 500);
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
            this.OutSideTabControl.Size = new System.Drawing.Size(843, 556);
            this.OutSideTabControl.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(829, 524);
            this.tabControl1.TabIndex = 39;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnExportOrders);
            this.tabPage3.Controls.Add(this.btnOpenDirectory);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(821, 498);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "工具";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            this.tabPage4.Size = new System.Drawing.Size(821, 498);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "SnowFlake";
            this.tabPage4.UseVisualStyleBackColor = true;
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
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(161, 44);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(189, 21);
            this.txtID.TabIndex = 1;
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
            // rtbID
            // 
            this.rtbID.Location = new System.Drawing.Point(161, 76);
            this.rtbID.Name = "rtbID";
            this.rtbID.Size = new System.Drawing.Size(368, 270);
            this.rtbID.TabIndex = 3;
            this.rtbID.Text = "";
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
            // la
            // 
            this.la.AutoSize = true;
            this.la.Location = new System.Drawing.Point(356, 14);
            this.la.Name = "la";
            this.la.Size = new System.Drawing.Size(65, 12);
            this.la.TabIndex = 6;
            this.la.Text = "StartDate:";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "ID:";
            // 
            // UtilityOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 556);
            this.Controls.Add(this.OutSideTabControl);
            this.MaximizeBox = false;
            this.Name = "UtilityOrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrderForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrderForm_FormClosing);
            this.Load += new System.EventHandler(this.UtilityOrderForm_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.testTabControl.ResumeLayout(false);
            this.OutSideTabControl.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSequenceBits)).EndInit();
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
    }
}