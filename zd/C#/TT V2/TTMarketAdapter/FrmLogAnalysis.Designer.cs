namespace TTMarketAdapter
{
    partial class FrmLogAnalysis
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvMarketData = new System.Windows.Forms.DataGridView();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnAddLog = new System.Windows.Forms.Button();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReadLog = new System.Windows.Forms.Button();
            this.cbOpenPrice = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSettleMentPrice = new System.Windows.Forms.CheckBox();
            this.cbPreviousSettlementPrice = new System.Windows.Forms.CheckBox();
            this.cbBidPrice1 = new System.Windows.Forms.CheckBox();
            this.cbLatestPrice = new System.Windows.Forms.CheckBox();
            this.cbBidPrice2 = new System.Windows.Forms.CheckBox();
            this.cbBidPrice3 = new System.Windows.Forms.CheckBox();
            this.cbBidPrice4 = new System.Windows.Forms.CheckBox();
            this.cbBidPrice5 = new System.Windows.Forms.CheckBox();
            this.cbBidPriceQuantity1 = new System.Windows.Forms.CheckBox();
            this.cbBidPriceQuantity2 = new System.Windows.Forms.CheckBox();
            this.cbAskPrice1 = new System.Windows.Forms.CheckBox();
            this.cbBidPriceQuantity5 = new System.Windows.Forms.CheckBox();
            this.cbBidPriceQuantity4 = new System.Windows.Forms.CheckBox();
            this.cbBidPriceQuantity3 = new System.Windows.Forms.CheckBox();
            this.cbAskPrice2 = new System.Windows.Forms.CheckBox();
            this.cbAskPrice5 = new System.Windows.Forms.CheckBox();
            this.cbAskPrice4 = new System.Windows.Forms.CheckBox();
            this.cbAskPrice3 = new System.Windows.Forms.CheckBox();
            this.cbAskPriceQuantity1 = new System.Windows.Forms.CheckBox();
            this.cbAskPriceQuantity4 = new System.Windows.Forms.CheckBox();
            this.cbAskPriceQuantity3 = new System.Windows.Forms.CheckBox();
            this.cbAskPriceQuantity5 = new System.Windows.Forms.CheckBox();
            this.cbAskPriceQuantity2 = new System.Windows.Forms.CheckBox();
            this.cbQuantity = new System.Windows.Forms.CheckBox();
            this.cbBidPriceCompare = new System.Windows.Forms.CheckBox();
            this.cbAskPriceCompare = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearQueryCondition = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarketData)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvMarketData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 240);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 407);
            this.panel2.TabIndex = 1;
            // 
            // dgvMarketData
            // 
            this.dgvMarketData.AllowUserToAddRows = false;
            this.dgvMarketData.AllowUserToDeleteRows = false;
            this.dgvMarketData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMarketData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMarketData.Location = new System.Drawing.Point(0, 0);
            this.dgvMarketData.Name = "dgvMarketData";
            this.dgvMarketData.ReadOnly = true;
            this.dgvMarketData.RowHeadersWidth = 75;
            this.dgvMarketData.RowTemplate.Height = 23;
            this.dgvMarketData.Size = new System.Drawing.Size(800, 407);
            this.dgvMarketData.TabIndex = 0;
            this.dgvMarketData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DgvMarketData_RowPrePaint);
            this.dgvMarketData.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.DgvMarketData_RowStateChanged);
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(713, 198);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // btnAddLog
            // 
            this.btnAddLog.Location = new System.Drawing.Point(527, 6);
            this.btnAddLog.Name = "btnAddLog";
            this.btnAddLog.Size = new System.Drawing.Size(75, 23);
            this.btnAddLog.TabIndex = 1;
            this.btnAddLog.Text = "添加日志";
            this.btnAddLog.UseVisualStyleBackColor = true;
            this.btnAddLog.Click += new System.EventHandler(this.BtnAddLog_Click);
            // 
            // txtLogPath
            // 
            this.txtLogPath.Location = new System.Drawing.Point(89, 6);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new System.Drawing.Size(408, 21);
            this.txtLogPath.TabIndex = 2;
            this.txtLogPath.Text = "C:\\Users\\Administrator\\Desktop\\3.txt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "日志路径:";
            // 
            // btnReadLog
            // 
            this.btnReadLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadLog.Location = new System.Drawing.Point(713, 8);
            this.btnReadLog.Name = "btnReadLog";
            this.btnReadLog.Size = new System.Drawing.Size(75, 23);
            this.btnReadLog.TabIndex = 4;
            this.btnReadLog.Text = "读取日志";
            this.btnReadLog.UseVisualStyleBackColor = true;
            this.btnReadLog.Click += new System.EventHandler(this.BtnReadLog_Click);
            // 
            // cbOpenPrice
            // 
            this.cbOpenPrice.AutoSize = true;
            this.cbOpenPrice.Location = new System.Drawing.Point(89, 73);
            this.cbOpenPrice.Name = "cbOpenPrice";
            this.cbOpenPrice.Size = new System.Drawing.Size(60, 16);
            this.cbOpenPrice.TabIndex = 6;
            this.cbOpenPrice.Text = "开盘价";
            this.cbOpenPrice.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "异常数据:";
            // 
            // cbSettleMentPrice
            // 
            this.cbSettleMentPrice.AutoSize = true;
            this.cbSettleMentPrice.Location = new System.Drawing.Point(155, 72);
            this.cbSettleMentPrice.Name = "cbSettleMentPrice";
            this.cbSettleMentPrice.Size = new System.Drawing.Size(60, 16);
            this.cbSettleMentPrice.TabIndex = 8;
            this.cbSettleMentPrice.Text = "结算价";
            this.cbSettleMentPrice.UseVisualStyleBackColor = true;
            // 
            // cbPreviousSettlementPrice
            // 
            this.cbPreviousSettlementPrice.AutoSize = true;
            this.cbPreviousSettlementPrice.Location = new System.Drawing.Point(221, 73);
            this.cbPreviousSettlementPrice.Name = "cbPreviousSettlementPrice";
            this.cbPreviousSettlementPrice.Size = new System.Drawing.Size(72, 16);
            this.cbPreviousSettlementPrice.TabIndex = 9;
            this.cbPreviousSettlementPrice.Text = "前结算价";
            this.cbPreviousSettlementPrice.UseVisualStyleBackColor = true;
            // 
            // cbBidPrice1
            // 
            this.cbBidPrice1.AutoSize = true;
            this.cbBidPrice1.Location = new System.Drawing.Point(89, 95);
            this.cbBidPrice1.Name = "cbBidPrice1";
            this.cbBidPrice1.Size = new System.Drawing.Size(54, 16);
            this.cbBidPrice1.TabIndex = 10;
            this.cbBidPrice1.Text = "买1价";
            this.cbBidPrice1.UseVisualStyleBackColor = true;
            // 
            // cbLatestPrice
            // 
            this.cbLatestPrice.AutoSize = true;
            this.cbLatestPrice.Location = new System.Drawing.Point(299, 69);
            this.cbLatestPrice.Name = "cbLatestPrice";
            this.cbLatestPrice.Size = new System.Drawing.Size(60, 16);
            this.cbLatestPrice.TabIndex = 11;
            this.cbLatestPrice.Text = "最新价";
            this.cbLatestPrice.UseVisualStyleBackColor = true;
            // 
            // cbBidPrice2
            // 
            this.cbBidPrice2.AutoSize = true;
            this.cbBidPrice2.Location = new System.Drawing.Point(155, 95);
            this.cbBidPrice2.Name = "cbBidPrice2";
            this.cbBidPrice2.Size = new System.Drawing.Size(54, 16);
            this.cbBidPrice2.TabIndex = 12;
            this.cbBidPrice2.Text = "买2价";
            this.cbBidPrice2.UseVisualStyleBackColor = true;
            // 
            // cbBidPrice3
            // 
            this.cbBidPrice3.AutoSize = true;
            this.cbBidPrice3.Location = new System.Drawing.Point(221, 95);
            this.cbBidPrice3.Name = "cbBidPrice3";
            this.cbBidPrice3.Size = new System.Drawing.Size(54, 16);
            this.cbBidPrice3.TabIndex = 13;
            this.cbBidPrice3.Text = "买3价";
            this.cbBidPrice3.UseVisualStyleBackColor = true;
            // 
            // cbBidPrice4
            // 
            this.cbBidPrice4.AutoSize = true;
            this.cbBidPrice4.Location = new System.Drawing.Point(304, 95);
            this.cbBidPrice4.Name = "cbBidPrice4";
            this.cbBidPrice4.Size = new System.Drawing.Size(54, 16);
            this.cbBidPrice4.TabIndex = 14;
            this.cbBidPrice4.Text = "买4价";
            this.cbBidPrice4.UseVisualStyleBackColor = true;
            // 
            // cbBidPrice5
            // 
            this.cbBidPrice5.AutoSize = true;
            this.cbBidPrice5.Location = new System.Drawing.Point(384, 95);
            this.cbBidPrice5.Name = "cbBidPrice5";
            this.cbBidPrice5.Size = new System.Drawing.Size(54, 16);
            this.cbBidPrice5.TabIndex = 15;
            this.cbBidPrice5.Text = "买5价";
            this.cbBidPrice5.UseVisualStyleBackColor = true;
            // 
            // cbBidPriceQuantity1
            // 
            this.cbBidPriceQuantity1.AutoSize = true;
            this.cbBidPriceQuantity1.Location = new System.Drawing.Point(89, 117);
            this.cbBidPriceQuantity1.Name = "cbBidPriceQuantity1";
            this.cbBidPriceQuantity1.Size = new System.Drawing.Size(54, 16);
            this.cbBidPriceQuantity1.TabIndex = 16;
            this.cbBidPriceQuantity1.Text = "买1量";
            this.cbBidPriceQuantity1.UseVisualStyleBackColor = true;
            // 
            // cbBidPriceQuantity2
            // 
            this.cbBidPriceQuantity2.AutoSize = true;
            this.cbBidPriceQuantity2.Location = new System.Drawing.Point(155, 117);
            this.cbBidPriceQuantity2.Name = "cbBidPriceQuantity2";
            this.cbBidPriceQuantity2.Size = new System.Drawing.Size(54, 16);
            this.cbBidPriceQuantity2.TabIndex = 17;
            this.cbBidPriceQuantity2.Text = "买2量";
            this.cbBidPriceQuantity2.UseVisualStyleBackColor = true;
            // 
            // cbAskPrice1
            // 
            this.cbAskPrice1.AutoSize = true;
            this.cbAskPrice1.Location = new System.Drawing.Point(89, 139);
            this.cbAskPrice1.Name = "cbAskPrice1";
            this.cbAskPrice1.Size = new System.Drawing.Size(54, 16);
            this.cbAskPrice1.TabIndex = 18;
            this.cbAskPrice1.Text = "卖1价";
            this.cbAskPrice1.UseVisualStyleBackColor = true;
            // 
            // cbBidPriceQuantity5
            // 
            this.cbBidPriceQuantity5.AutoSize = true;
            this.cbBidPriceQuantity5.Location = new System.Drawing.Point(384, 117);
            this.cbBidPriceQuantity5.Name = "cbBidPriceQuantity5";
            this.cbBidPriceQuantity5.Size = new System.Drawing.Size(54, 16);
            this.cbBidPriceQuantity5.TabIndex = 19;
            this.cbBidPriceQuantity5.Text = "买5量";
            this.cbBidPriceQuantity5.UseVisualStyleBackColor = true;
            // 
            // cbBidPriceQuantity4
            // 
            this.cbBidPriceQuantity4.AutoSize = true;
            this.cbBidPriceQuantity4.Location = new System.Drawing.Point(304, 117);
            this.cbBidPriceQuantity4.Name = "cbBidPriceQuantity4";
            this.cbBidPriceQuantity4.Size = new System.Drawing.Size(54, 16);
            this.cbBidPriceQuantity4.TabIndex = 20;
            this.cbBidPriceQuantity4.Text = "买4量";
            this.cbBidPriceQuantity4.UseVisualStyleBackColor = true;
            // 
            // cbBidPriceQuantity3
            // 
            this.cbBidPriceQuantity3.AutoSize = true;
            this.cbBidPriceQuantity3.Location = new System.Drawing.Point(221, 117);
            this.cbBidPriceQuantity3.Name = "cbBidPriceQuantity3";
            this.cbBidPriceQuantity3.Size = new System.Drawing.Size(54, 16);
            this.cbBidPriceQuantity3.TabIndex = 21;
            this.cbBidPriceQuantity3.Text = "买3量";
            this.cbBidPriceQuantity3.UseVisualStyleBackColor = true;
            // 
            // cbAskPrice2
            // 
            this.cbAskPrice2.AutoSize = true;
            this.cbAskPrice2.Location = new System.Drawing.Point(155, 139);
            this.cbAskPrice2.Name = "cbAskPrice2";
            this.cbAskPrice2.Size = new System.Drawing.Size(54, 16);
            this.cbAskPrice2.TabIndex = 22;
            this.cbAskPrice2.Text = "卖2价";
            this.cbAskPrice2.UseVisualStyleBackColor = true;
            // 
            // cbAskPrice5
            // 
            this.cbAskPrice5.AutoSize = true;
            this.cbAskPrice5.Location = new System.Drawing.Point(384, 139);
            this.cbAskPrice5.Name = "cbAskPrice5";
            this.cbAskPrice5.Size = new System.Drawing.Size(54, 16);
            this.cbAskPrice5.TabIndex = 23;
            this.cbAskPrice5.Text = "卖5价";
            this.cbAskPrice5.UseVisualStyleBackColor = true;
            // 
            // cbAskPrice4
            // 
            this.cbAskPrice4.AutoSize = true;
            this.cbAskPrice4.Location = new System.Drawing.Point(304, 139);
            this.cbAskPrice4.Name = "cbAskPrice4";
            this.cbAskPrice4.Size = new System.Drawing.Size(54, 16);
            this.cbAskPrice4.TabIndex = 24;
            this.cbAskPrice4.Text = "卖4价";
            this.cbAskPrice4.UseVisualStyleBackColor = true;
            // 
            // cbAskPrice3
            // 
            this.cbAskPrice3.AutoSize = true;
            this.cbAskPrice3.Location = new System.Drawing.Point(221, 139);
            this.cbAskPrice3.Name = "cbAskPrice3";
            this.cbAskPrice3.Size = new System.Drawing.Size(54, 16);
            this.cbAskPrice3.TabIndex = 25;
            this.cbAskPrice3.Text = "卖3价";
            this.cbAskPrice3.UseVisualStyleBackColor = true;
            // 
            // cbAskPriceQuantity1
            // 
            this.cbAskPriceQuantity1.AutoSize = true;
            this.cbAskPriceQuantity1.Location = new System.Drawing.Point(89, 161);
            this.cbAskPriceQuantity1.Name = "cbAskPriceQuantity1";
            this.cbAskPriceQuantity1.Size = new System.Drawing.Size(54, 16);
            this.cbAskPriceQuantity1.TabIndex = 26;
            this.cbAskPriceQuantity1.Text = "卖1量";
            this.cbAskPriceQuantity1.UseVisualStyleBackColor = true;
            // 
            // cbAskPriceQuantity4
            // 
            this.cbAskPriceQuantity4.AutoSize = true;
            this.cbAskPriceQuantity4.Location = new System.Drawing.Point(304, 161);
            this.cbAskPriceQuantity4.Name = "cbAskPriceQuantity4";
            this.cbAskPriceQuantity4.Size = new System.Drawing.Size(54, 16);
            this.cbAskPriceQuantity4.TabIndex = 27;
            this.cbAskPriceQuantity4.Text = "卖4量";
            this.cbAskPriceQuantity4.UseVisualStyleBackColor = true;
            // 
            // cbAskPriceQuantity3
            // 
            this.cbAskPriceQuantity3.AutoSize = true;
            this.cbAskPriceQuantity3.Location = new System.Drawing.Point(221, 161);
            this.cbAskPriceQuantity3.Name = "cbAskPriceQuantity3";
            this.cbAskPriceQuantity3.Size = new System.Drawing.Size(54, 16);
            this.cbAskPriceQuantity3.TabIndex = 28;
            this.cbAskPriceQuantity3.Text = "卖3量";
            this.cbAskPriceQuantity3.UseVisualStyleBackColor = true;
            // 
            // cbAskPriceQuantity5
            // 
            this.cbAskPriceQuantity5.AutoSize = true;
            this.cbAskPriceQuantity5.Location = new System.Drawing.Point(384, 161);
            this.cbAskPriceQuantity5.Name = "cbAskPriceQuantity5";
            this.cbAskPriceQuantity5.Size = new System.Drawing.Size(54, 16);
            this.cbAskPriceQuantity5.TabIndex = 29;
            this.cbAskPriceQuantity5.Text = "卖5量";
            this.cbAskPriceQuantity5.UseVisualStyleBackColor = true;
            // 
            // cbAskPriceQuantity2
            // 
            this.cbAskPriceQuantity2.AutoSize = true;
            this.cbAskPriceQuantity2.Location = new System.Drawing.Point(155, 161);
            this.cbAskPriceQuantity2.Name = "cbAskPriceQuantity2";
            this.cbAskPriceQuantity2.Size = new System.Drawing.Size(54, 16);
            this.cbAskPriceQuantity2.TabIndex = 30;
            this.cbAskPriceQuantity2.Text = "卖2量";
            this.cbAskPriceQuantity2.UseVisualStyleBackColor = true;
            // 
            // cbQuantity
            // 
            this.cbQuantity.AutoSize = true;
            this.cbQuantity.Location = new System.Drawing.Point(384, 69);
            this.cbQuantity.Name = "cbQuantity";
            this.cbQuantity.Size = new System.Drawing.Size(48, 16);
            this.cbQuantity.TabIndex = 31;
            this.cbQuantity.Text = "现手";
            this.cbQuantity.UseVisualStyleBackColor = true;
            // 
            // cbBidPriceCompare
            // 
            this.cbBidPriceCompare.AutoSize = true;
            this.cbBidPriceCompare.Location = new System.Drawing.Point(89, 183);
            this.cbBidPriceCompare.Name = "cbBidPriceCompare";
            this.cbBidPriceCompare.Size = new System.Drawing.Size(162, 16);
            this.cbBidPriceCompare.TabIndex = 32;
            this.cbBidPriceCompare.Text = "买1>=买2>=买3>=买4>=买5";
            this.cbBidPriceCompare.UseVisualStyleBackColor = true;
            // 
            // cbAskPriceCompare
            // 
            this.cbAskPriceCompare.AutoSize = true;
            this.cbAskPriceCompare.Location = new System.Drawing.Point(89, 205);
            this.cbAskPriceCompare.Name = "cbAskPriceCompare";
            this.cbAskPriceCompare.Size = new System.Drawing.Size(162, 16);
            this.cbAskPriceCompare.TabIndex = 33;
            this.cbAskPriceCompare.Text = "卖1<=卖2<=卖3<=卖4<=卖5";
            this.cbAskPriceCompare.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 34;
            this.label3.Text = "数据类型:";
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(88, 39);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(121, 20);
            this.cmbDataType.TabIndex = 35;
            this.cmbDataType.SelectedIndexChanged += new System.EventHandler(this.CmbDataType_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClearQueryCondition);
            this.panel1.Controls.Add(this.cmbDataType);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbAskPriceCompare);
            this.panel1.Controls.Add(this.cbBidPriceCompare);
            this.panel1.Controls.Add(this.cbQuantity);
            this.panel1.Controls.Add(this.cbAskPriceQuantity2);
            this.panel1.Controls.Add(this.cbAskPriceQuantity5);
            this.panel1.Controls.Add(this.cbAskPriceQuantity3);
            this.panel1.Controls.Add(this.cbAskPriceQuantity4);
            this.panel1.Controls.Add(this.cbAskPriceQuantity1);
            this.panel1.Controls.Add(this.cbAskPrice3);
            this.panel1.Controls.Add(this.cbAskPrice4);
            this.panel1.Controls.Add(this.cbAskPrice5);
            this.panel1.Controls.Add(this.cbAskPrice2);
            this.panel1.Controls.Add(this.cbBidPriceQuantity3);
            this.panel1.Controls.Add(this.cbBidPriceQuantity4);
            this.panel1.Controls.Add(this.cbBidPriceQuantity5);
            this.panel1.Controls.Add(this.cbAskPrice1);
            this.panel1.Controls.Add(this.cbBidPriceQuantity2);
            this.panel1.Controls.Add(this.cbBidPriceQuantity1);
            this.panel1.Controls.Add(this.cbBidPrice5);
            this.panel1.Controls.Add(this.cbBidPrice4);
            this.panel1.Controls.Add(this.cbBidPrice3);
            this.panel1.Controls.Add(this.cbBidPrice2);
            this.panel1.Controls.Add(this.cbLatestPrice);
            this.panel1.Controls.Add(this.cbBidPrice1);
            this.panel1.Controls.Add(this.cbPreviousSettlementPrice);
            this.panel1.Controls.Add(this.cbSettleMentPrice);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbOpenPrice);
            this.panel1.Controls.Add(this.btnReadLog);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtLogPath);
            this.panel1.Controls.Add(this.btnAddLog);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 240);
            this.panel1.TabIndex = 0;
            // 
            // btnClearQueryCondition
            // 
            this.btnClearQueryCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearQueryCondition.Location = new System.Drawing.Point(713, 157);
            this.btnClearQueryCondition.Name = "btnClearQueryCondition";
            this.btnClearQueryCondition.Size = new System.Drawing.Size(75, 23);
            this.btnClearQueryCondition.TabIndex = 36;
            this.btnClearQueryCondition.Text = "清除条件";
            this.btnClearQueryCondition.UseVisualStyleBackColor = true;
            this.btnClearQueryCondition.Click += new System.EventHandler(this.BtnClearQueryCondition_Click);
            // 
            // FrmLogAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 647);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmLogAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLogAnalysis";
            this.Load += new System.EventHandler(this.FrmLogAnalysis_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarketData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvMarketData;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnAddLog;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReadLog;
        private System.Windows.Forms.CheckBox cbOpenPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbSettleMentPrice;
        private System.Windows.Forms.CheckBox cbPreviousSettlementPrice;
        private System.Windows.Forms.CheckBox cbBidPrice1;
        private System.Windows.Forms.CheckBox cbLatestPrice;
        private System.Windows.Forms.CheckBox cbBidPrice2;
        private System.Windows.Forms.CheckBox cbBidPrice3;
        private System.Windows.Forms.CheckBox cbBidPrice4;
        private System.Windows.Forms.CheckBox cbBidPrice5;
        private System.Windows.Forms.CheckBox cbBidPriceQuantity1;
        private System.Windows.Forms.CheckBox cbBidPriceQuantity2;
        private System.Windows.Forms.CheckBox cbAskPrice1;
        private System.Windows.Forms.CheckBox cbBidPriceQuantity5;
        private System.Windows.Forms.CheckBox cbBidPriceQuantity4;
        private System.Windows.Forms.CheckBox cbBidPriceQuantity3;
        private System.Windows.Forms.CheckBox cbAskPrice2;
        private System.Windows.Forms.CheckBox cbAskPrice5;
        private System.Windows.Forms.CheckBox cbAskPrice4;
        private System.Windows.Forms.CheckBox cbAskPrice3;
        private System.Windows.Forms.CheckBox cbAskPriceQuantity1;
        private System.Windows.Forms.CheckBox cbAskPriceQuantity4;
        private System.Windows.Forms.CheckBox cbAskPriceQuantity3;
        private System.Windows.Forms.CheckBox cbAskPriceQuantity5;
        private System.Windows.Forms.CheckBox cbAskPriceQuantity2;
        private System.Windows.Forms.CheckBox cbQuantity;
        private System.Windows.Forms.CheckBox cbBidPriceCompare;
        private System.Windows.Forms.CheckBox cbAskPriceCompare;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClearQueryCondition;
    }
}