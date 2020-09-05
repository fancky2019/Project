namespace StockAdapterHKEX
{
    partial class FrmE2ETest
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtSecurityID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTCR = new System.Windows.Forms.Button();
            this.txtLastQty = new System.Windows.Forms.TextBox();
            this.txtLastPx = new System.Windows.Forms.TextBox();
            this.txtTradeID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTradeReportID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtSecurityID);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.btnTCR);
            this.tabPage1.Controls.Add(this.txtLastQty);
            this.tabPage1.Controls.Add(this.txtLastPx);
            this.tabPage1.Controls.Add(this.txtTradeID);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtTradeReportID);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtSecurityID
            // 
            this.txtSecurityID.Location = new System.Drawing.Point(147, 164);
            this.txtSecurityID.Name = "txtSecurityID";
            this.txtSecurityID.Size = new System.Drawing.Size(100, 21);
            this.txtSecurityID.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "SecurityID(48):";
            // 
            // btnTCR
            // 
            this.btnTCR.Location = new System.Drawing.Point(147, 199);
            this.btnTCR.Name = "btnTCR";
            this.btnTCR.Size = new System.Drawing.Size(75, 23);
            this.btnTCR.TabIndex = 8;
            this.btnTCR.Text = "TCR";
            this.btnTCR.UseVisualStyleBackColor = true;
            this.btnTCR.Click += new System.EventHandler(this.btnTCR_Click);
            // 
            // txtLastQty
            // 
            this.txtLastQty.Location = new System.Drawing.Point(145, 130);
            this.txtLastQty.Name = "txtLastQty";
            this.txtLastQty.Size = new System.Drawing.Size(100, 21);
            this.txtLastQty.TabIndex = 7;
            // 
            // txtLastPx
            // 
            this.txtLastPx.Location = new System.Drawing.Point(145, 95);
            this.txtLastPx.Name = "txtLastPx";
            this.txtLastPx.Size = new System.Drawing.Size(100, 21);
            this.txtLastPx.TabIndex = 6;
            // 
            // txtTradeID
            // 
            this.txtTradeID.Location = new System.Drawing.Point(147, 61);
            this.txtTradeID.Name = "txtTradeID";
            this.txtTradeID.Size = new System.Drawing.Size(100, 21);
            this.txtTradeID.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "LastPx(31):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "TradeID(1003):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "LastQty(32):";
            // 
            // txtTradeReportID
            // 
            this.txtTradeReportID.Location = new System.Drawing.Point(147, 29);
            this.txtTradeReportID.Name = "txtTradeReportID";
            this.txtTradeReportID.Size = new System.Drawing.Size(100, 21);
            this.txtTradeReportID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "TradeReportID(571):";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FrmE2ETest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmE2ETest";
            this.Text = "FrmE2ETest";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtTradeReportID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTradeID;
        private System.Windows.Forms.TextBox txtLastQty;
        private System.Windows.Forms.TextBox txtLastPx;
        private System.Windows.Forms.Button btnTCR;
        private System.Windows.Forms.TextBox txtSecurityID;
        private System.Windows.Forms.Label label5;
    }
}