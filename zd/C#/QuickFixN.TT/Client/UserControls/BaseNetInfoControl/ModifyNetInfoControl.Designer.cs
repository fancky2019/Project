﻿namespace Client.UserControls.BaseNetInfoControl
{
    partial class ModifyNetInfoControl
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
            this.txtAmendStopPrice = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.nudAmendQty = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAmendClOrderID = new System.Windows.Forms.TextBox();
            this.txtAmendSysCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAmendPrice = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmendQty)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtAmendStopPrice);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.nudAmendQty);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtAmendClOrderID);
            this.panel1.Controls.Add(this.txtAmendSysCode);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtAmendPrice);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 172);
            this.panel1.TabIndex = 0;
            // 
            // txtAmendStopPrice
            // 
            this.txtAmendStopPrice.Location = new System.Drawing.Point(153, 142);
            this.txtAmendStopPrice.Name = "txtAmendStopPrice";
            this.txtAmendStopPrice.Size = new System.Drawing.Size(121, 21);
            this.txtAmendStopPrice.TabIndex = 67;
            this.txtAmendStopPrice.Text = "42.59";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(68, 148);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 12);
            this.label15.TabIndex = 66;
            this.label15.Text = "StopPx(99):";
            // 
            // nudAmendQty
            // 
            this.nudAmendQty.Location = new System.Drawing.Point(153, 78);
            this.nudAmendQty.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudAmendQty.Name = "nudAmendQty";
            this.nudAmendQty.Size = new System.Drawing.Size(120, 21);
            this.nudAmendQty.TabIndex = 65;
            this.nudAmendQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(68, 78);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 64;
            this.label14.Text = "OrdQty(38):";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(50, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 63;
            this.label13.Text = "ClOrderID(11):";
            // 
            // txtAmendClOrderID
            // 
            this.txtAmendClOrderID.Location = new System.Drawing.Point(152, 49);
            this.txtAmendClOrderID.Name = "txtAmendClOrderID";
            this.txtAmendClOrderID.Size = new System.Drawing.Size(121, 21);
            this.txtAmendClOrderID.TabIndex = 62;
            this.txtAmendClOrderID.Text = "1000001";
            // 
            // txtAmendSysCode
            // 
            this.txtAmendSysCode.Location = new System.Drawing.Point(152, 17);
            this.txtAmendSysCode.Name = "txtAmendSysCode";
            this.txtAmendSysCode.Size = new System.Drawing.Size(121, 21);
            this.txtAmendSysCode.TabIndex = 61;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(68, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 60;
            this.label9.Text = "SystemCode:";
            // 
            // txtAmendPrice
            // 
            this.txtAmendPrice.Location = new System.Drawing.Point(153, 113);
            this.txtAmendPrice.Name = "txtAmendPrice";
            this.txtAmendPrice.Size = new System.Drawing.Size(121, 21);
            this.txtAmendPrice.TabIndex = 59;
            this.txtAmendPrice.Text = "42.59";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(98, 113);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 58;
            this.label11.Text = "Price:";
            // 
            // ModifyNetInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ModifyNetInfoControl";
            this.Size = new System.Drawing.Size(322, 440);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmendQty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        protected System.Windows.Forms.TextBox txtAmendStopPrice;
        protected System.Windows.Forms.NumericUpDown nudAmendQty;
        protected System.Windows.Forms.TextBox txtAmendClOrderID;
        protected System.Windows.Forms.TextBox txtAmendSysCode;
        protected System.Windows.Forms.TextBox txtAmendPrice;
    }
}
