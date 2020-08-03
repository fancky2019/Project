namespace Client
{
    partial class OrderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbMsgs = new System.Windows.Forms.ListBox();
            this.rtbNetInfo = new System.Windows.Forms.RichTextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudDisplayQty = new System.Windows.Forms.NumericUpDown();
            this.nudMinQty = new System.Windows.Forms.NumericUpDown();
            this.nudQrdQty = new System.Windows.Forms.NumericUpDown();
            this.btnNewOrderSingle = new System.Windows.Forms.Button();
            this.txtStopPx = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSecurityAltID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSecurityExchange = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSide = new System.Windows.Forms.ComboBox();
            this.cmbTimeInForce = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbOrderType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAmendOrder = new System.Windows.Forms.Button();
            this.txtAmendPrice = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtCancelOrderClOrderID = new System.Windows.Forms.TextBox();
            this.txtOrderCancelSystemCode = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnOrderCancelRequest = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnOpenDirectory = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAmendSysCode = new System.Windows.Forms.TextBox();
            this.txtAmendClOrderID = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.nudAmendQty = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.txtAmendStopPrice = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQrdQty)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmendQty)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(843, 556);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbMsgs);
            this.tabPage1.Controls.Add(this.rtbNetInfo);
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(835, 530);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "测试";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbMsgs
            // 
            this.lbMsgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMsgs.FormattingEnabled = true;
            this.lbMsgs.HorizontalScrollbar = true;
            this.lbMsgs.ItemHeight = 12;
            this.lbMsgs.Location = new System.Drawing.Point(328, 3);
            this.lbMsgs.Name = "lbMsgs";
            this.lbMsgs.Size = new System.Drawing.Size(504, 314);
            this.lbMsgs.TabIndex = 21;
            this.lbMsgs.SelectedIndexChanged += new System.EventHandler(this.lbMsgs_SelectedIndexChanged);
            this.lbMsgs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbMsgs_KeyUp);
            // 
            // rtbNetInfo
            // 
            this.rtbNetInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbNetInfo.Location = new System.Drawing.Point(328, 317);
            this.rtbNetInfo.Name = "rtbNetInfo";
            this.rtbNetInfo.ReadOnly = true;
            this.rtbNetInfo.Size = new System.Drawing.Size(504, 210);
            this.rtbNetInfo.TabIndex = 14;
            this.rtbNetInfo.Text = "";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(325, 524);
            this.tabControl2.TabIndex = 12;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(317, 498);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "下单";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudDisplayQty);
            this.panel1.Controls.Add(this.nudMinQty);
            this.panel1.Controls.Add(this.nudQrdQty);
            this.panel1.Controls.Add(this.btnNewOrderSingle);
            this.panel1.Controls.Add(this.txtStopPx);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtPrice);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtSecurityAltID);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtSecurityExchange);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbSide);
            this.panel1.Controls.Add(this.cmbTimeInForce);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbOrderType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(311, 492);
            this.panel1.TabIndex = 12;
            // 
            // nudDisplayQty
            // 
            this.nudDisplayQty.Location = new System.Drawing.Point(156, 198);
            this.nudDisplayQty.Name = "nudDisplayQty";
            this.nudDisplayQty.Size = new System.Drawing.Size(120, 21);
            this.nudDisplayQty.TabIndex = 40;
            // 
            // nudMinQty
            // 
            this.nudMinQty.Location = new System.Drawing.Point(156, 171);
            this.nudMinQty.Name = "nudMinQty";
            this.nudMinQty.Size = new System.Drawing.Size(120, 21);
            this.nudMinQty.TabIndex = 39;
            // 
            // nudQrdQty
            // 
            this.nudQrdQty.Location = new System.Drawing.Point(156, 144);
            this.nudQrdQty.Name = "nudQrdQty";
            this.nudQrdQty.Size = new System.Drawing.Size(120, 21);
            this.nudQrdQty.TabIndex = 38;
            this.nudQrdQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnNewOrderSingle
            // 
            this.btnNewOrderSingle.Location = new System.Drawing.Point(199, 289);
            this.btnNewOrderSingle.Name = "btnNewOrderSingle";
            this.btnNewOrderSingle.Size = new System.Drawing.Size(78, 23);
            this.btnNewOrderSingle.TabIndex = 37;
            this.btnNewOrderSingle.Text = "下单";
            this.btnNewOrderSingle.UseVisualStyleBackColor = true;
            this.btnNewOrderSingle.Click += new System.EventHandler(this.btnNewOrderSingle_Click);
            // 
            // txtStopPx
            // 
            this.txtStopPx.Location = new System.Drawing.Point(156, 252);
            this.txtStopPx.Name = "txtStopPx";
            this.txtStopPx.Size = new System.Drawing.Size(121, 21);
            this.txtStopPx.TabIndex = 36;
            this.txtStopPx.Text = "42.59";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(79, 255);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 12);
            this.label12.TabIndex = 35;
            this.label12.Text = "StopPx(99):";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(155, 225);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(121, 21);
            this.txtPrice.TabIndex = 24;
            this.txtPrice.Text = "42.59";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(84, 228);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "Price(44):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "DisplayQty(1138):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(72, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "MinQty(110):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "OrdQty(38):";
            // 
            // txtSecurityAltID
            // 
            this.txtSecurityAltID.Location = new System.Drawing.Point(156, 117);
            this.txtSecurityAltID.Name = "txtSecurityAltID";
            this.txtSecurityAltID.Size = new System.Drawing.Size(121, 21);
            this.txtSecurityAltID.TabIndex = 16;
            this.txtSecurityAltID.Text = "BRN2012";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "SecurityAltID(455):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "SecurityExchange(207):";
            // 
            // txtSecurityExchange
            // 
            this.txtSecurityExchange.Location = new System.Drawing.Point(156, 92);
            this.txtSecurityExchange.Name = "txtSecurityExchange";
            this.txtSecurityExchange.Size = new System.Drawing.Size(121, 21);
            this.txtSecurityExchange.TabIndex = 13;
            this.txtSecurityExchange.Text = "ICE";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(91, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "Side(54):";
            // 
            // cmbSide
            // 
            this.cmbSide.FormattingEnabled = true;
            this.cmbSide.Location = new System.Drawing.Point(156, 63);
            this.cmbSide.Name = "cmbSide";
            this.cmbSide.Size = new System.Drawing.Size(121, 20);
            this.cmbSide.TabIndex = 11;
            // 
            // cmbTimeInForce
            // 
            this.cmbTimeInForce.FormattingEnabled = true;
            this.cmbTimeInForce.Location = new System.Drawing.Point(156, 34);
            this.cmbTimeInForce.Name = "cmbTimeInForce";
            this.cmbTimeInForce.Size = new System.Drawing.Size(121, 20);
            this.cmbTimeInForce.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "TimeInForce(59):";
            // 
            // cmbOrderType
            // 
            this.cmbOrderType.FormattingEnabled = true;
            this.cmbOrderType.Location = new System.Drawing.Point(156, 7);
            this.cmbOrderType.Name = "cmbOrderType";
            this.cmbOrderType.Size = new System.Drawing.Size(121, 20);
            this.cmbOrderType.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "OrdType(40):";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(317, 498);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "改单";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtAmendStopPrice);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.nudAmendQty);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtAmendClOrderID);
            this.panel2.Controls.Add(this.txtAmendSysCode);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.btnAmendOrder);
            this.panel2.Controls.Add(this.txtAmendPrice);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 492);
            this.panel2.TabIndex = 13;
            // 
            // btnAmendOrder
            // 
            this.btnAmendOrder.Location = new System.Drawing.Point(164, 203);
            this.btnAmendOrder.Name = "btnAmendOrder";
            this.btnAmendOrder.Size = new System.Drawing.Size(78, 23);
            this.btnAmendOrder.TabIndex = 37;
            this.btnAmendOrder.Text = "改单";
            this.btnAmendOrder.UseVisualStyleBackColor = true;
            this.btnAmendOrder.Click += new System.EventHandler(this.btnAmendOrder_Click);
            // 
            // txtAmendPrice
            // 
            this.txtAmendPrice.Location = new System.Drawing.Point(121, 115);
            this.txtAmendPrice.Name = "txtAmendPrice";
            this.txtAmendPrice.Size = new System.Drawing.Size(121, 21);
            this.txtAmendPrice.TabIndex = 24;
            this.txtAmendPrice.Text = "42.59";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(66, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "Price:";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.panel3);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(317, 498);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "撤单";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtCancelOrderClOrderID);
            this.panel3.Controls.Add(this.txtOrderCancelSystemCode);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.btnOrderCancelRequest);
            this.panel3.Controls.Add(this.label30);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(311, 492);
            this.panel3.TabIndex = 13;
            // 
            // txtCancelOrderClOrderID
            // 
            this.txtCancelOrderClOrderID.Location = new System.Drawing.Point(119, 34);
            this.txtCancelOrderClOrderID.Name = "txtCancelOrderClOrderID";
            this.txtCancelOrderClOrderID.Size = new System.Drawing.Size(121, 21);
            this.txtCancelOrderClOrderID.TabIndex = 40;
            this.txtCancelOrderClOrderID.Text = "1000001";
            // 
            // txtOrderCancelSystemCode
            // 
            this.txtOrderCancelSystemCode.Location = new System.Drawing.Point(119, 3);
            this.txtOrderCancelSystemCode.Name = "txtOrderCancelSystemCode";
            this.txtOrderCancelSystemCode.Size = new System.Drawing.Size(121, 21);
            this.txtOrderCancelSystemCode.TabIndex = 39;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(18, 37);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 12);
            this.label21.TabIndex = 38;
            this.label21.Text = "ClOrderID(11):";
            // 
            // btnOrderCancelRequest
            // 
            this.btnOrderCancelRequest.Location = new System.Drawing.Point(162, 93);
            this.btnOrderCancelRequest.Name = "btnOrderCancelRequest";
            this.btnOrderCancelRequest.Size = new System.Drawing.Size(78, 23);
            this.btnOrderCancelRequest.TabIndex = 37;
            this.btnOrderCancelRequest.Text = "撤单";
            this.btnOrderCancelRequest.UseVisualStyleBackColor = true;
            this.btnOrderCancelRequest.Click += new System.EventHandler(this.btnOrderCancelRequest_Click);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(42, 7);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(71, 12);
            this.label30.TabIndex = 1;
            this.label30.Text = "SystemCode:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnOpenDirectory);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(835, 530);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "工具";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnOpenDirectory
            // 
            this.btnOpenDirectory.Location = new System.Drawing.Point(759, 20);
            this.btnOpenDirectory.Name = "btnOpenDirectory";
            this.btnOpenDirectory.Size = new System.Drawing.Size(68, 23);
            this.btnOpenDirectory.TabIndex = 36;
            this.btnOpenDirectory.Text = "目录";
            this.btnOpenDirectory.UseVisualStyleBackColor = true;
            this.btnOpenDirectory.Click += new System.EventHandler(this.btnOpenDirectory_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 38;
            this.label9.Text = "SystemCode:";
            // 
            // txtAmendSysCode
            // 
            this.txtAmendSysCode.Location = new System.Drawing.Point(119, 19);
            this.txtAmendSysCode.Name = "txtAmendSysCode";
            this.txtAmendSysCode.Size = new System.Drawing.Size(121, 21);
            this.txtAmendSysCode.TabIndex = 40;
            // 
            // txtAmendClOrderID
            // 
            this.txtAmendClOrderID.Location = new System.Drawing.Point(120, 51);
            this.txtAmendClOrderID.Name = "txtAmendClOrderID";
            this.txtAmendClOrderID.Size = new System.Drawing.Size(121, 21);
            this.txtAmendClOrderID.TabIndex = 41;
            this.txtAmendClOrderID.Text = "1000001";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 42;
            this.label13.Text = "ClOrderID(11):";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(36, 81);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 43;
            this.label14.Text = "OrdQty(38):";
            // 
            // nudAmendQty
            // 
            this.nudAmendQty.Location = new System.Drawing.Point(121, 81);
            this.nudAmendQty.Name = "nudAmendQty";
            this.nudAmendQty.Size = new System.Drawing.Size(120, 21);
            this.nudAmendQty.TabIndex = 44;
            this.nudAmendQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(36, 150);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 12);
            this.label15.TabIndex = 45;
            this.label15.Text = "StopPx(99):";
            // 
            // txtAmendStopPrice
            // 
            this.txtAmendStopPrice.Location = new System.Drawing.Point(121, 145);
            this.txtAmendStopPrice.Name = "txtAmendStopPrice";
            this.txtAmendStopPrice.Size = new System.Drawing.Size(121, 21);
            this.txtAmendStopPrice.TabIndex = 46;
            this.txtAmendStopPrice.Text = "42.59";
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 556);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "OrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrderForm";
            this.Load += new System.EventHandler(this.OrderForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQrdQty)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudAmendQty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lbMsgs;
        private System.Windows.Forms.RichTextBox rtbNetInfo;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtStopPx;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSecurityAltID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSecurityExchange;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSide;
        private System.Windows.Forms.ComboBox cmbTimeInForce;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbOrderType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnNewOrderSingle;
        private System.Windows.Forms.NumericUpDown nudDisplayQty;
        private System.Windows.Forms.NumericUpDown nudMinQty;
        private System.Windows.Forms.NumericUpDown nudQrdQty;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAmendOrder;
        private System.Windows.Forms.TextBox txtAmendPrice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOrderCancelRequest;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnOpenDirectory;
        private System.Windows.Forms.TextBox txtCancelOrderClOrderID;
        private System.Windows.Forms.TextBox txtOrderCancelSystemCode;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtAmendClOrderID;
        private System.Windows.Forms.TextBox txtAmendSysCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudAmendQty;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtAmendStopPrice;
    }
}