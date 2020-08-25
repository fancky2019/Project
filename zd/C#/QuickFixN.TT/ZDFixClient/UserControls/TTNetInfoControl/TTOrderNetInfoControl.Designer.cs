namespace ZDFixClient.UserControls.TTNetInfoControl
{
    partial class TTOrderNetInfoControl
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
            this.btnNewOrderSingle = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStopPx = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQrdQty)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPrice
            // 
            this.txtPrice.Margin = new System.Windows.Forms.Padding(2);
            // 
            // txtSecurityAltID
            // 
            this.txtSecurityAltID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // txtSecurityExchange
            // 
            this.txtSecurityExchange.Margin = new System.Windows.Forms.Padding(2);
            // 
            // btnNewOrderSingle
            // 
            this.btnNewOrderSingle.Location = new System.Drawing.Point(211, 299);
            this.btnNewOrderSingle.Name = "btnNewOrderSingle";
            this.btnNewOrderSingle.Size = new System.Drawing.Size(78, 23);
            this.btnNewOrderSingle.TabIndex = 38;
            this.btnNewOrderSingle.Text = "下单";
            this.btnNewOrderSingle.UseVisualStyleBackColor = true;
            this.btnNewOrderSingle.Click += new System.EventHandler(this.btnNewOrderSingle_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(91, 266);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 57;
            this.label9.Text = "StopPx(99):";
            // 
            // txtStopPx
            // 
            this.txtStopPx.Location = new System.Drawing.Point(168, 263);
            this.txtStopPx.Name = "txtStopPx";
            this.txtStopPx.Size = new System.Drawing.Size(121, 21);
            this.txtStopPx.TabIndex = 58;
            this.txtStopPx.Text = "42.59";
            // 
            // TTOrderNetInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtStopPx);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnNewOrderSingle);
            this.Name = "TTOrderNetInfoControl";
            this.Load += new System.EventHandler(this.TTOrderNetInfoControl_Load);
            this.Controls.SetChildIndex(this.btnNewOrderSingle, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtStopPx, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQrdQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewOrderSingle;
        private System.Windows.Forms.Label label9;
        protected System.Windows.Forms.TextBox txtStopPx;
    }
}
