namespace ZDFixClient
{
    partial class FrmTradeClient
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.btnOpenDic = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnShowOrderForm = new System.Windows.Forms.Button();
            this.cbConsole = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnOpenDic
            // 
            this.btnOpenDic.Location = new System.Drawing.Point(207, 12);
            this.btnOpenDic.Name = "btnOpenDic";
            this.btnOpenDic.Size = new System.Drawing.Size(75, 23);
            this.btnOpenDic.TabIndex = 19;
            this.btnOpenDic.Text = "目录";
            this.btnOpenDic.UseVisualStyleBackColor = true;
            this.btnOpenDic.Click += new System.EventHandler(this.btnOpenDic_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 58);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 20;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(207, 58);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 21;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnShowOrderForm
            // 
            this.btnShowOrderForm.Location = new System.Drawing.Point(12, 130);
            this.btnShowOrderForm.Name = "btnShowOrderForm";
            this.btnShowOrderForm.Size = new System.Drawing.Size(75, 23);
            this.btnShowOrderForm.TabIndex = 22;
            this.btnShowOrderForm.Text = "OrderForm";
            this.btnShowOrderForm.UseVisualStyleBackColor = true;
            this.btnShowOrderForm.Click += new System.EventHandler(this.btnShowOrderForm_Click);
            // 
            // cbConsole
            // 
            this.cbConsole.AutoSize = true;
            this.cbConsole.Location = new System.Drawing.Point(12, 108);
            this.cbConsole.Name = "cbConsole";
            this.cbConsole.Size = new System.Drawing.Size(66, 16);
            this.cbConsole.TabIndex = 23;
            this.cbConsole.Text = "Console";
            this.cbConsole.UseVisualStyleBackColor = true;
            // 
            // FrmTradeClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 195);
            this.Controls.Add(this.cbConsole);
            this.Controls.Add(this.btnShowOrderForm);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnOpenDic);
            this.Controls.Add(this.btnStart);
            this.MaximizeBox = false;
            this.Name = "FrmTradeClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmTradeClient.TT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTradeClient_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnOpenDic;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnShowOrderForm;
        private System.Windows.Forms.CheckBox cbConsole;
    }
}

