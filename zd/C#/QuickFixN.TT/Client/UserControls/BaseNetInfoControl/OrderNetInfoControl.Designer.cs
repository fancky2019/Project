namespace Client.UserControls.BaseNetInfoControl
{
    partial class OrderNetInfoControl
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
            this.nudDisplayQty = new System.Windows.Forms.NumericUpDown();
            this.nudMinQty = new System.Windows.Forms.NumericUpDown();
            this.nudQrdQty = new System.Windows.Forms.NumericUpDown();
            this.txtStopPx = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSecurityAltID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSecurityExchange = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSide = new System.Windows.Forms.ComboBox();
            this.cmbTimeInForce = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbOrderType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQrdQty)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudDisplayQty);
            this.panel1.Controls.Add(this.nudMinQty);
            this.panel1.Controls.Add(this.nudQrdQty);
            this.panel1.Controls.Add(this.txtStopPx);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtPrice);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtSecurityAltID);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtSecurityExchange);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbSide);
            this.panel1.Controls.Add(this.cmbTimeInForce);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbOrderType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 286);
            this.panel1.TabIndex = 0;
            // 
            // nudDisplayQty
            // 
            this.nudDisplayQty.Location = new System.Drawing.Point(169, 207);
            this.nudDisplayQty.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudDisplayQty.Name = "nudDisplayQty";
            this.nudDisplayQty.Size = new System.Drawing.Size(120, 21);
            this.nudDisplayQty.TabIndex = 61;
            // 
            // nudMinQty
            // 
            this.nudMinQty.Location = new System.Drawing.Point(169, 180);
            this.nudMinQty.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudMinQty.Name = "nudMinQty";
            this.nudMinQty.Size = new System.Drawing.Size(120, 21);
            this.nudMinQty.TabIndex = 60;
            // 
            // nudQrdQty
            // 
            this.nudQrdQty.Location = new System.Drawing.Point(169, 153);
            this.nudQrdQty.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudQrdQty.Name = "nudQrdQty";
            this.nudQrdQty.Size = new System.Drawing.Size(120, 21);
            this.nudQrdQty.TabIndex = 59;
            this.nudQrdQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtStopPx
            // 
            this.txtStopPx.Location = new System.Drawing.Point(169, 261);
            this.txtStopPx.Name = "txtStopPx";
            this.txtStopPx.Size = new System.Drawing.Size(121, 21);
            this.txtStopPx.TabIndex = 57;
            this.txtStopPx.Text = "42.59";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(92, 264);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 12);
            this.label12.TabIndex = 56;
            this.label12.Text = "StopPx(99):";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(168, 234);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(121, 21);
            this.txtPrice.TabIndex = 55;
            this.txtPrice.Text = "42.59";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(97, 237);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 54;
            this.label10.Text = "Price(44):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 12);
            this.label8.TabIndex = 53;
            this.label8.Text = "DisplayQty(1138):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(85, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 52;
            this.label5.Text = "MinQty(110):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 51;
            this.label4.Text = "OrdQty(38):";
            // 
            // txtSecurityAltID
            // 
            this.txtSecurityAltID.Location = new System.Drawing.Point(169, 126);
            this.txtSecurityAltID.Name = "txtSecurityAltID";
            this.txtSecurityAltID.Size = new System.Drawing.Size(121, 21);
            this.txtSecurityAltID.TabIndex = 50;
            this.txtSecurityAltID.Text = "BRN Dec20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 49;
            this.label3.Text = "SecurityAltID(455):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 48;
            this.label2.Text = "SecurityExchange(207):";
            // 
            // txtSecurityExchange
            // 
            this.txtSecurityExchange.Location = new System.Drawing.Point(169, 101);
            this.txtSecurityExchange.Name = "txtSecurityExchange";
            this.txtSecurityExchange.Size = new System.Drawing.Size(121, 21);
            this.txtSecurityExchange.TabIndex = 47;
            this.txtSecurityExchange.Text = "ICE";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 46;
            this.label7.Text = "Side(54):";
            // 
            // cmbSide
            // 
            this.cmbSide.FormattingEnabled = true;
            this.cmbSide.Location = new System.Drawing.Point(169, 72);
            this.cmbSide.Name = "cmbSide";
            this.cmbSide.Size = new System.Drawing.Size(121, 20);
            this.cmbSide.TabIndex = 45;
            // 
            // cmbTimeInForce
            // 
            this.cmbTimeInForce.FormattingEnabled = true;
            this.cmbTimeInForce.Location = new System.Drawing.Point(169, 42);
            this.cmbTimeInForce.Name = "cmbTimeInForce";
            this.cmbTimeInForce.Size = new System.Drawing.Size(121, 20);
            this.cmbTimeInForce.TabIndex = 44;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 43;
            this.label6.Text = "TimeInForce(59):";
            // 
            // cmbOrderType
            // 
            this.cmbOrderType.FormattingEnabled = true;
            this.cmbOrderType.Location = new System.Drawing.Point(169, 16);
            this.cmbOrderType.Name = "cmbOrderType";
            this.cmbOrderType.Size = new System.Drawing.Size(121, 20);
            this.cmbOrderType.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "OrdType(40):";
            // 
            // OrderNetInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "OrderNetInfoControl";
            this.Size = new System.Drawing.Size(322, 440);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQrdQty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.NumericUpDown nudDisplayQty;
        protected System.Windows.Forms.NumericUpDown nudMinQty;
        protected System.Windows.Forms.NumericUpDown nudQrdQty;
        protected System.Windows.Forms.TextBox txtStopPx;
        protected System.Windows.Forms.TextBox txtPrice;
        protected System.Windows.Forms.TextBox txtSecurityAltID;
        protected System.Windows.Forms.TextBox txtSecurityExchange;
        protected System.Windows.Forms.ComboBox cmbSide;
        protected System.Windows.Forms.ComboBox cmbTimeInForce;
        protected System.Windows.Forms.ComboBox cmbOrderType;
    }
}
