﻿namespace ZDFixClient.UserControls.TTNetInfoControl
{
    partial class TTCancelNetInfoControl
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
            this.SuspendLayout();
            // 
            // btnOrderCancelRequest
            // 
            this.btnOrderCancelRequest.Location = new System.Drawing.Point(258, 132);
            this.btnOrderCancelRequest.Margin = new System.Windows.Forms.Padding(4);
            this.btnOrderCancelRequest.Name = "btnOrderCancelRequest";
            this.btnOrderCancelRequest.Size = new System.Drawing.Size(104, 29);
            this.btnOrderCancelRequest.TabIndex = 46;
            this.btnOrderCancelRequest.Text = "撤单";
            this.btnOrderCancelRequest.UseVisualStyleBackColor = true;
            this.btnOrderCancelRequest.Click += new System.EventHandler(this.btnOrderCancelRequest_Click);
            // 
            // TTCancelNetInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOrderCancelRequest);
            this.Name = "TTCancelNetInfoControl";
            this.Controls.SetChildIndex(this.btnOrderCancelRequest, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOrderCancelRequest;
    }
}
