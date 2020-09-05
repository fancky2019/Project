namespace StockAdapterHKEX
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTest = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOrderForm = new System.Windows.Forms.Button();
            this.cbTestMode = new System.Windows.Forms.CheckBox();
            this.btnRejectTest = new System.Windows.Forms.Button();
            this.btnNetInfo = new System.Windows.Forms.Button();
            this.btnOpenDirectory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(150, 206);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(87, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(54, 40);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(54, 69);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnOrderForm
            // 
            this.btnOrderForm.Location = new System.Drawing.Point(54, 132);
            this.btnOrderForm.Name = "btnOrderForm";
            this.btnOrderForm.Size = new System.Drawing.Size(110, 23);
            this.btnOrderForm.TabIndex = 3;
            this.btnOrderForm.Text = "order form";
            this.btnOrderForm.UseVisualStyleBackColor = true;
            this.btnOrderForm.Click += new System.EventHandler(this.btnOrderForm_Click);
            // 
            // cbTestMode
            // 
            this.cbTestMode.AutoSize = true;
            this.cbTestMode.Location = new System.Drawing.Point(54, 18);
            this.cbTestMode.Name = "cbTestMode";
            this.cbTestMode.Size = new System.Drawing.Size(72, 16);
            this.cbTestMode.TabIndex = 4;
            this.cbTestMode.Text = "测试模式";
            this.cbTestMode.UseVisualStyleBackColor = true;
            // 
            // btnRejectTest
            // 
            this.btnRejectTest.Location = new System.Drawing.Point(51, 206);
            this.btnRejectTest.Name = "btnRejectTest";
            this.btnRejectTest.Size = new System.Drawing.Size(75, 23);
            this.btnRejectTest.TabIndex = 45;
            this.btnRejectTest.Text = "RejectTest";
            this.btnRejectTest.UseVisualStyleBackColor = true;
            this.btnRejectTest.Click += new System.EventHandler(this.btnRejectTest_Click);
            // 
            // btnNetInfo
            // 
            this.btnNetInfo.Location = new System.Drawing.Point(51, 235);
            this.btnNetInfo.Name = "btnNetInfo";
            this.btnNetInfo.Size = new System.Drawing.Size(75, 23);
            this.btnNetInfo.TabIndex = 46;
            this.btnNetInfo.Text = "NetInfo";
            this.btnNetInfo.UseVisualStyleBackColor = true;
            this.btnNetInfo.Click += new System.EventHandler(this.BtnNetInfo_Click);
            // 
            // btnOpenDirectory
            // 
            this.btnOpenDirectory.Location = new System.Drawing.Point(150, 235);
            this.btnOpenDirectory.Name = "btnOpenDirectory";
            this.btnOpenDirectory.Size = new System.Drawing.Size(87, 23);
            this.btnOpenDirectory.TabIndex = 47;
            this.btnOpenDirectory.Text = "目录";
            this.btnOpenDirectory.UseVisualStyleBackColor = true;
            this.btnOpenDirectory.Click += new System.EventHandler(this.BtnOpenDirectory_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 281);
            this.Controls.Add(this.btnOpenDirectory);
            this.Controls.Add(this.btnNetInfo);
            this.Controls.Add(this.btnRejectTest);
            this.Controls.Add(this.cbTestMode);
            this.Controls.Add(this.btnOrderForm);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnTest);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnOrderForm;
        private System.Windows.Forms.CheckBox cbTestMode;
        private System.Windows.Forms.Button btnRejectTest;
        private System.Windows.Forms.Button btnNetInfo;
        private System.Windows.Forms.Button btnOpenDirectory;
    }
}

