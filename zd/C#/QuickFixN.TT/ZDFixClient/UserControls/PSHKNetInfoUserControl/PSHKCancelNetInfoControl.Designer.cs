namespace ZDFixClient.UserControls.PSHKNetInfoUserControl
{
    partial class PSHKCancelNetInfoControl
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
            this.btnOrderCancelRequest = new System.Windows.Forms.Button();
            this.cbConsole = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtCancelOrderClOrderID
            // 
            this.txtCancelOrderClOrderID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // txtOrderCancelSystemCode
            // 
            this.txtOrderCancelSystemCode.Margin = new System.Windows.Forms.Padding(2);
            // 
            // btnOrderCancelRequest
            // 
            this.btnOrderCancelRequest.Location = new System.Drawing.Point(195, 90);
            this.btnOrderCancelRequest.Name = "btnOrderCancelRequest";
            this.btnOrderCancelRequest.Size = new System.Drawing.Size(78, 23);
            this.btnOrderCancelRequest.TabIndex = 46;
            this.btnOrderCancelRequest.Text = "撤单";
            this.btnOrderCancelRequest.UseVisualStyleBackColor = true;
            this.btnOrderCancelRequest.Click += new System.EventHandler(this.btnOrderCancelRequest_Click);
            // 
            // cbConsole
            // 
            this.cbConsole.AutoSize = true;
            this.cbConsole.Location = new System.Drawing.Point(114, 94);
            this.cbConsole.Name = "cbConsole";
            this.cbConsole.Size = new System.Drawing.Size(66, 16);
            this.cbConsole.TabIndex = 51;
            this.cbConsole.Text = "Console";
            this.cbConsole.UseVisualStyleBackColor = true;
            // 
            // PSHKCancelNetInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbConsole);
            this.Controls.Add(this.btnOrderCancelRequest);
            this.Name = "PSHKCancelNetInfoControl";
            this.Controls.SetChildIndex(this.btnOrderCancelRequest, 0);
            this.Controls.SetChildIndex(this.cbConsole, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOrderCancelRequest;
        private System.Windows.Forms.CheckBox cbConsole;
    }
}
