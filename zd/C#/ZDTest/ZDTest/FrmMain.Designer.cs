namespace ZDTest
{
    partial class FrmMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLoginTest = new System.Windows.Forms.Button();
            this.btnOpenDirectory = new System.Windows.Forms.Button();
            this.panelUc = new System.Windows.Forms.Panel();
            this.panelUserControl = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLoadMemoryDta = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudUserAccount = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panelUc.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserAccount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLoginTest);
            this.panel1.Controls.Add(this.btnOpenDirectory);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 64);
            this.panel1.TabIndex = 0;
            // 
            // btnLoginTest
            // 
            this.btnLoginTest.Location = new System.Drawing.Point(3, 9);
            this.btnLoginTest.Name = "btnLoginTest";
            this.btnLoginTest.Size = new System.Drawing.Size(75, 47);
            this.btnLoginTest.TabIndex = 38;
            this.btnLoginTest.Text = "登录测试";
            this.btnLoginTest.UseVisualStyleBackColor = true;
            this.btnLoginTest.Click += new System.EventHandler(this.btnLoginTest_Click);
            // 
            // btnOpenDirectory
            // 
            this.btnOpenDirectory.Location = new System.Drawing.Point(720, 12);
            this.btnOpenDirectory.Name = "btnOpenDirectory";
            this.btnOpenDirectory.Size = new System.Drawing.Size(68, 23);
            this.btnOpenDirectory.TabIndex = 37;
            this.btnOpenDirectory.Text = "目录";
            this.btnOpenDirectory.UseVisualStyleBackColor = true;
            this.btnOpenDirectory.Click += new System.EventHandler(this.btnOpenDirectory_Click);
            // 
            // panelUc
            // 
            this.panelUc.Controls.Add(this.panelUserControl);
            this.panelUc.Controls.Add(this.panel3);
            this.panelUc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUc.Location = new System.Drawing.Point(0, 64);
            this.panelUc.Name = "panelUc";
            this.panelUc.Size = new System.Drawing.Size(800, 386);
            this.panelUc.TabIndex = 1;
            // 
            // panelUserControl
            // 
            this.panelUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUserControl.Location = new System.Drawing.Point(0, 55);
            this.panelUserControl.Name = "panelUserControl";
            this.panelUserControl.Size = new System.Drawing.Size(800, 331);
            this.panelUserControl.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 55);
            this.panel3.TabIndex = 0;
            // 
            // btnLoadMemoryDta
            // 
            this.btnLoadMemoryDta.Location = new System.Drawing.Point(720, 20);
            this.btnLoadMemoryDta.Name = "btnLoadMemoryDta";
            this.btnLoadMemoryDta.Size = new System.Drawing.Size(75, 23);
            this.btnLoadMemoryDta.TabIndex = 5;
            this.btnLoadMemoryDta.Text = "加载数据";
            this.btnLoadMemoryDta.UseVisualStyleBackColor = true;
            this.btnLoadMemoryDta.Click += new System.EventHandler(this.btnLoadMemoryDta_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(251, 24);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.btnLogOut.TabIndex = 4;
            this.btnLogOut.Text = "Logout";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(155, 24);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户数:";
            // 
            // nudUserAccount
            // 
            this.nudUserAccount.Location = new System.Drawing.Point(65, 24);
            this.nudUserAccount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudUserAccount.Name = "nudUserAccount";
            this.nudUserAccount.Size = new System.Drawing.Size(72, 21);
            this.nudUserAccount.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadMemoryDta);
            this.groupBox1.Controls.Add(this.nudUserAccount);
            this.groupBox1.Controls.Add(this.btnLogOut);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 55);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelUc);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.panel1.ResumeLayout(false);
            this.panelUc.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudUserAccount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelUc;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudUserAccount;
        private System.Windows.Forms.Button btnOpenDirectory;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnLoginTest;
        private System.Windows.Forms.Button btnLoadMemoryDta;
        private System.Windows.Forms.Panel panelUserControl;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

