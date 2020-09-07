﻿namespace TradeTool
{
    partial class FrmTradeTool
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvClientIn = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadClientInToClient = new System.Windows.Forms.Button();
            this.txtSystemCodes = new System.Windows.Forms.TextBox();
            this.txtClientInPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rtbNetInfo = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnResolve = new System.Windows.Forms.Button();
            this.txtNetInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnOpenDirectory = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.LogTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvToClient = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientIn)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToClient)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(806, 565);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.splitter1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(798, 539);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ClintInToClint";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvToClient);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(409, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 428);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ToClient";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(399, 108);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 428);
            this.splitter1.TabIndex = 12;
            this.splitter1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvClientIn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(3, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 428);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ClientIn";
            // 
            // dgvClientIn
            // 
            this.dgvClientIn.AllowUserToAddRows = false;
            this.dgvClientIn.AllowUserToDeleteRows = false;
            this.dgvClientIn.AllowUserToResizeRows = false;
            this.dgvClientIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientIn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LogTime,
            this.Column2,
            this.Column3});
            this.dgvClientIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClientIn.Location = new System.Drawing.Point(3, 17);
            this.dgvClientIn.MultiSelect = false;
            this.dgvClientIn.Name = "dgvClientIn";
            this.dgvClientIn.ReadOnly = true;
            this.dgvClientIn.RowTemplate.Height = 23;
            this.dgvClientIn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientIn.Size = new System.Drawing.Size(390, 408);
            this.dgvClientIn.TabIndex = 0;
            this.dgvClientIn.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvClientIn_KeyUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnLoadClientInToClient);
            this.panel1.Controls.Add(this.txtSystemCodes);
            this.panel1.Controls.Add(this.txtClientInPath);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 105);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "LogPath:";
            // 
            // btnLoadClientInToClient
            // 
            this.btnLoadClientInToClient.Location = new System.Drawing.Point(655, 52);
            this.btnLoadClientInToClient.Name = "btnLoadClientInToClient";
            this.btnLoadClientInToClient.Size = new System.Drawing.Size(59, 23);
            this.btnLoadClientInToClient.TabIndex = 0;
            this.btnLoadClientInToClient.Text = "Read";
            this.btnLoadClientInToClient.UseVisualStyleBackColor = true;
            this.btnLoadClientInToClient.Click += new System.EventHandler(this.btnClientIn_Click);
            // 
            // txtSystemCodes
            // 
            this.txtSystemCodes.Location = new System.Drawing.Point(79, 49);
            this.txtSystemCodes.Name = "txtSystemCodes";
            this.txtSystemCodes.Size = new System.Drawing.Size(539, 21);
            this.txtSystemCodes.TabIndex = 6;
            // 
            // txtClientInPath
            // 
            this.txtClientInPath.Location = new System.Drawing.Point(79, 15);
            this.txtClientInPath.Name = "txtClientInPath";
            this.txtClientInPath.ReadOnly = true;
            this.txtClientInPath.Size = new System.Drawing.Size(540, 21);
            this.txtClientInPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "SystemCode:";
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(655, 15);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(44, 23);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "...";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rtbNetInfo);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(798, 539);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "NetInfo";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // rtbNetInfo
            // 
            this.rtbNetInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbNetInfo.Location = new System.Drawing.Point(3, 71);
            this.rtbNetInfo.Name = "rtbNetInfo";
            this.rtbNetInfo.ReadOnly = true;
            this.rtbNetInfo.Size = new System.Drawing.Size(792, 465);
            this.rtbNetInfo.TabIndex = 17;
            this.rtbNetInfo.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnResolve);
            this.panel2.Controls.Add(this.txtNetInfo);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(792, 68);
            this.panel2.TabIndex = 16;
            // 
            // btnResolve
            // 
            this.btnResolve.Location = new System.Drawing.Point(664, 25);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(75, 23);
            this.btnResolve.TabIndex = 2;
            this.btnResolve.Text = "Resolve";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // txtNetInfo
            // 
            this.txtNetInfo.Location = new System.Drawing.Point(98, 25);
            this.txtNetInfo.Name = "txtNetInfo";
            this.txtNetInfo.Size = new System.Drawing.Size(551, 21);
            this.txtNetInfo.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "NetInfoStr:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnOpenDirectory);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(798, 539);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "通用";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnOpenDirectory
            // 
            this.btnOpenDirectory.Location = new System.Drawing.Point(618, 17);
            this.btnOpenDirectory.Name = "btnOpenDirectory";
            this.btnOpenDirectory.Size = new System.Drawing.Size(68, 23);
            this.btnOpenDirectory.TabIndex = 37;
            this.btnOpenDirectory.Text = "目录";
            this.btnOpenDirectory.UseVisualStyleBackColor = true;
            this.btnOpenDirectory.Click += new System.EventHandler(this.btnOpenDirectory_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // LogTime
            // 
            this.LogTime.DataPropertyName = "LogTime";
            this.LogTime.HeaderText = "LogTime";
            this.LogTime.Name = "LogTime";
            this.LogTime.ReadOnly = true;
            this.LogTime.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "SystemCode";
            this.Column2.HeaderText = "SystemCode";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 110;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Command";
            this.Column3.HeaderText = "Command";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // dgvToClient
            // 
            this.dgvToClient.AllowUserToAddRows = false;
            this.dgvToClient.AllowUserToDeleteRows = false;
            this.dgvToClient.AllowUserToResizeRows = false;
            this.dgvToClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToClient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvToClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvToClient.Location = new System.Drawing.Point(3, 17);
            this.dgvToClient.MultiSelect = false;
            this.dgvToClient.Name = "dgvToClient";
            this.dgvToClient.ReadOnly = true;
            this.dgvToClient.RowTemplate.Height = 23;
            this.dgvToClient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvToClient.Size = new System.Drawing.Size(380, 408);
            this.dgvToClient.TabIndex = 1;
            this.dgvToClient.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvToClient_KeyUp);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "LogTime";
            this.dataGridViewTextBoxColumn1.HeaderText = "LogTime";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "SystemCode";
            this.dataGridViewTextBoxColumn2.HeaderText = "SystemCode";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 110;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Command";
            this.dataGridViewTextBoxColumn3.HeaderText = "Command";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // FrmTradeTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 565);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmTradeTool";
            this.Text = "TradeTool";
            this.Load += new System.EventHandler(this.FrmTradeTool_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientIn)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvToClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnOpenDirectory;
        private System.Windows.Forms.Button btnLoadClientInToClient;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtClientInPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSystemCodes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvClientIn;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.TextBox txtNetInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbNetInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridView dgvToClient;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}

