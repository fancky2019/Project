namespace ZDFixClient.UserControls.BaseNetInfoControl
{
    partial class CancelNetInfoControl
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
            this.txtCancelOrderClOrderID = new System.Windows.Forms.TextBox();
            this.txtOrderCancelSystemCode = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCancelOrderClOrderID);
            this.panel1.Controls.Add(this.txtOrderCancelSystemCode);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 112);
            this.panel1.TabIndex = 0;
            // 
            // txtCancelOrderClOrderID
            // 
            this.txtCancelOrderClOrderID.Location = new System.Drawing.Point(203, 69);
            this.txtCancelOrderClOrderID.Margin = new System.Windows.Forms.Padding(4);
            this.txtCancelOrderClOrderID.Name = "txtCancelOrderClOrderID";
            this.txtCancelOrderClOrderID.Size = new System.Drawing.Size(160, 25);
            this.txtCancelOrderClOrderID.TabIndex = 49;
            this.txtCancelOrderClOrderID.Text = "1000001";
            // 
            // txtOrderCancelSystemCode
            // 
            this.txtOrderCancelSystemCode.Location = new System.Drawing.Point(203, 31);
            this.txtOrderCancelSystemCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtOrderCancelSystemCode.Name = "txtOrderCancelSystemCode";
            this.txtOrderCancelSystemCode.Size = new System.Drawing.Size(160, 25);
            this.txtOrderCancelSystemCode.TabIndex = 48;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(68, 73);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(119, 15);
            this.label21.TabIndex = 47;
            this.label21.Text = "ClOrderID(11):";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(100, 36);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(95, 15);
            this.label30.TabIndex = 46;
            this.label30.Text = "SystemCode:";
            // 
            // CancelNetInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "CancelNetInfoControl";
            this.Size = new System.Drawing.Size(430, 550);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label30;
        protected System.Windows.Forms.TextBox txtCancelOrderClOrderID;
        protected System.Windows.Forms.TextBox txtOrderCancelSystemCode;
    }
}
