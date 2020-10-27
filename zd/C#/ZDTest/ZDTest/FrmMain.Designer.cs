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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbDB = new System.Windows.Forms.CheckBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nudUserAccountSkip = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadMemoryDta = new System.Windows.Forms.Button();
            this.nudUserAccountTake = new System.Windows.Forms.NumericUpDown();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panelUc.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserAccountSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserAccountTake)).BeginInit();
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
            this.panelUserControl.Location = new System.Drawing.Point(0, 117);
            this.panelUserControl.Name = "panelUserControl";
            this.panelUserControl.Size = new System.Drawing.Size(800, 269);
            this.panelUserControl.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 117);
            this.panel3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbDB);
            this.groupBox1.Controls.Add(this.btnRead);
            this.groupBox1.Controls.Add(this.btnView);
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nudUserAccountSkip);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnLoadMemoryDta);
            this.groupBox1.Controls.Add(this.nudUserAccountTake);
            this.groupBox1.Controls.Add(this.btnLogOut);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 117);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录";
            // 
            // cbDB
            // 
            this.cbDB.AutoSize = true;
            this.cbDB.Location = new System.Drawing.Point(119, 13);
            this.cbDB.Name = "cbDB";
            this.cbDB.Size = new System.Drawing.Size(108, 16);
            this.cbDB.TabIndex = 15;
            this.cbDB.Text = "使用数据库数据";
            this.cbDB.UseVisualStyleBackColor = true;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(698, 48);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(90, 23);
            this.btnRead.TabIndex = 13;
            this.btnRead.Text = "加载用户数据";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(626, 48);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(44, 23);
            this.btnView.TabIndex = 12;
            this.btnView.Text = "...";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.AllowDrop = true;
            this.txtFilePath.Location = new System.Drawing.Point(119, 50);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(501, 21);
            this.txtFilePath.TabIndex = 11;
            this.txtFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtFilePath_DragDrop);
            this.txtFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtFilePath_DragEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "用户文件路径：";
            // 
            // nudUserAccountSkip
            // 
            this.nudUserAccountSkip.Location = new System.Drawing.Point(232, 86);
            this.nudUserAccountSkip.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudUserAccountSkip.Name = "nudUserAccountSkip";
            this.nudUserAccountSkip.Size = new System.Drawing.Size(72, 21);
            this.nudUserAccountSkip.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Skip";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Take";
            // 
            // btnLoadMemoryDta
            // 
            this.btnLoadMemoryDta.Location = new System.Drawing.Point(698, 83);
            this.btnLoadMemoryDta.Name = "btnLoadMemoryDta";
            this.btnLoadMemoryDta.Size = new System.Drawing.Size(90, 23);
            this.btnLoadMemoryDta.TabIndex = 5;
            this.btnLoadMemoryDta.Text = "加载数据";
            this.btnLoadMemoryDta.UseVisualStyleBackColor = true;
            this.btnLoadMemoryDta.Click += new System.EventHandler(this.btnLoadMemoryDta_Click);
            // 
            // nudUserAccountTake
            // 
            this.nudUserAccountTake.Location = new System.Drawing.Point(119, 86);
            this.nudUserAccountTake.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudUserAccountTake.Name = "nudUserAccountTake";
            this.nudUserAccountTake.Size = new System.Drawing.Size(72, 21);
            this.nudUserAccountTake.TabIndex = 1;
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(416, 83);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.btnLogOut.TabIndex = 4;
            this.btnLogOut.Text = "Logout";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户数：";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(323, 84);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserAccountSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserAccountTake)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelUc;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudUserAccountTake;
        private System.Windows.Forms.Button btnOpenDirectory;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnLoginTest;
        private System.Windows.Forms.Button btnLoadMemoryDta;
        private System.Windows.Forms.Panel panelUserControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudUserAccountSkip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.CheckBox cbDB;
    }
}

