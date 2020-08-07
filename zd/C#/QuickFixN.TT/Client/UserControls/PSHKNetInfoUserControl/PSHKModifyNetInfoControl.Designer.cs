namespace Client.UserControls.PSHKNetInfoUserControl
{
    partial class PSHKModifyNetInfoControl
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
            this.btnAmendOrder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmendQty)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAmendOrder
            // 
            this.btnAmendOrder.Location = new System.Drawing.Point(259, 228);
            this.btnAmendOrder.Margin = new System.Windows.Forms.Padding(4);
            this.btnAmendOrder.Name = "btnAmendOrder";
            this.btnAmendOrder.Size = new System.Drawing.Size(104, 29);
            this.btnAmendOrder.TabIndex = 58;
            this.btnAmendOrder.Text = "改单";
            this.btnAmendOrder.UseVisualStyleBackColor = true;
            this.btnAmendOrder.Click += new System.EventHandler(this.btnAmendOrder_Click);
            // 
            // PSHKModifyNetInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAmendOrder);
            this.Name = "PSHKModifyNetInfoControl";
            this.Controls.SetChildIndex(this.btnAmendOrder, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudAmendQty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAmendOrder;
    }
}
