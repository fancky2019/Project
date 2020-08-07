namespace Client
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
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.testTabControl.SuspendLayout();
            this.OutSideTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnExportOrders);
            this.tabPage2.Controls.Add(this.btnOpenDirectory);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1116, 666);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "工具";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnExportOrders
            // 
            this.btnExportOrders.Location = new System.Drawing.Point(33, 19);
            this.btnExportOrders.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportOrders.Name = "btnExportOrders";
            this.btnExportOrders.Size = new System.Drawing.Size(100, 29);
            this.btnExportOrders.TabIndex = 38;
            this.btnExportOrders.Text = "导出订单";
            this.btnExportOrders.UseVisualStyleBackColor = true;
            this.btnExportOrders.Click += new System.EventHandler(this.btnExportOrders_Click);
            // 
            // btnOpenDirectory
            // 
            this.btnOpenDirectory.Location = new System.Drawing.Point(991, 19);
            this.btnOpenDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenDirectory.Name = "btnOpenDirectory";
            this.btnOpenDirectory.Size = new System.Drawing.Size(91, 29);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1116, 666);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "测试";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbMsgs
            // 
            this.lbMsgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMsgs.FormattingEnabled = true;
            this.lbMsgs.HorizontalScrollbar = true;
            this.lbMsgs.ItemHeight = 15;
            this.lbMsgs.Location = new System.Drawing.Point(437, 4);
            this.lbMsgs.Margin = new System.Windows.Forms.Padding(4);
            this.lbMsgs.Name = "lbMsgs";
            this.lbMsgs.Size = new System.Drawing.Size(675, 396);
            this.lbMsgs.TabIndex = 21;
            this.lbMsgs.SelectedIndexChanged += new System.EventHandler(this.lbMsgs_SelectedIndexChanged);
            this.lbMsgs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbMsgs_KeyUp);
            // 
            // rtbNetInfo
            // 
            this.rtbNetInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbNetInfo.Location = new System.Drawing.Point(437, 400);
            this.rtbNetInfo.Margin = new System.Windows.Forms.Padding(4);
            this.rtbNetInfo.Name = "rtbNetInfo";
            this.rtbNetInfo.ReadOnly = true;
            this.rtbNetInfo.Size = new System.Drawing.Size(675, 262);
            this.rtbNetInfo.TabIndex = 14;
            this.rtbNetInfo.Text = "";
            // 
            // testTabControl
            // 
            this.testTabControl.Controls.Add(this.orderTabPage);
            this.testTabControl.Controls.Add(this.modifyTabPage);
            this.testTabControl.Controls.Add(this.cancelTabPage);
            this.testTabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.testTabControl.Location = new System.Drawing.Point(4, 4);
            this.testTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.testTabControl.Name = "testTabControl";
            this.testTabControl.SelectedIndex = 0;
            this.testTabControl.Size = new System.Drawing.Size(433, 658);
            this.testTabControl.TabIndex = 12;
            // 
            // orderTabPage
            // 
            this.orderTabPage.Location = new System.Drawing.Point(4, 25);
            this.orderTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.orderTabPage.Name = "orderTabPage";
            this.orderTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.orderTabPage.Size = new System.Drawing.Size(425, 629);
            this.orderTabPage.TabIndex = 0;
            this.orderTabPage.Text = "下单";
            this.orderTabPage.UseVisualStyleBackColor = true;
            // 
            // modifyTabPage
            // 
            this.modifyTabPage.Location = new System.Drawing.Point(4, 25);
            this.modifyTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.modifyTabPage.Name = "modifyTabPage";
            this.modifyTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.modifyTabPage.Size = new System.Drawing.Size(425, 629);
            this.modifyTabPage.TabIndex = 1;
            this.modifyTabPage.Text = "改单";
            this.modifyTabPage.UseVisualStyleBackColor = true;
            // 
            // cancelTabPage
            // 
            this.cancelTabPage.Location = new System.Drawing.Point(4, 25);
            this.cancelTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.cancelTabPage.Name = "cancelTabPage";
            this.cancelTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.cancelTabPage.Size = new System.Drawing.Size(425, 629);
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
            this.OutSideTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.OutSideTabControl.Name = "OutSideTabControl";
            this.OutSideTabControl.SelectedIndex = 0;
            this.OutSideTabControl.Size = new System.Drawing.Size(1124, 695);
            this.OutSideTabControl.TabIndex = 0;
            // 
            // UtilityOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 695);
            this.Controls.Add(this.OutSideTabControl);
            this.Margin = new System.Windows.Forms.Padding(4);
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
    }
}