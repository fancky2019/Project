﻿namespace ZDFixClient.UserControls.PSHKNetInfoUserControl
{
    partial class PSHKOrderNetInfoControl
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
            this.btnNewOrderSingle.Location = new System.Drawing.Point(212, 266);
            this.btnNewOrderSingle.Name = "btnNewOrderSingle";
            this.btnNewOrderSingle.Size = new System.Drawing.Size(78, 23);
            this.btnNewOrderSingle.TabIndex = 38;
            this.btnNewOrderSingle.Text = "下单";
            this.btnNewOrderSingle.UseVisualStyleBackColor = true;
            this.btnNewOrderSingle.Click += new System.EventHandler(this.btnNewOrderSingle_Click);
            // 
            // PSHKOrderNetInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnNewOrderSingle);
            this.Name = "PSHKOrderNetInfoControl";
            this.Load += new System.EventHandler(this.PSHKOrderNetInfoControl_Load);
            this.Controls.SetChildIndex(this.btnNewOrderSingle, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQrdQty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNewOrderSingle;
    }
}
