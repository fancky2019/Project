namespace ZDTradeClientTT
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.combOrderType = new System.Windows.Forms.ComboBox();
            this.combTIF = new System.Windows.Forms.ComboBox();
            this.combSide = new System.Windows.Forms.ComboBox();
            this.tbSymbol = new System.Windows.Forms.TextBox();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.tbQty = new System.Windows.Forms.TextBox();
            this.txtMinQty = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbMaxshow = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbClOrderID = new System.Windows.Forms.TextBox();
            this.btnAddOrder = new System.Windows.Forms.Button();
            this.btnCancelOrder = new System.Windows.Forms.Button();
            this.lbMsgs = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbTag50ID = new System.Windows.Forms.TextBox();
            this.btnOrderCreation = new System.Windows.Forms.Button();
            this.btnPartialFill = new System.Windows.Forms.Button();
            this.btnCompleteFill = new System.Windows.Forms.Button();
            this.btnReplyCancelled = new System.Windows.Forms.Button();
            this.btnReplyReject = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnQueryInstrument = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtTriggerPrice = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnOpenDirectory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Symbol/Exchange:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Security desc/Contract:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(82, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Order qty:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(94, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Min qty:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "Time in force:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(111, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "Side:";
            // 
            // combOrderType
            // 
            this.combOrderType.FormattingEnabled = true;
            this.combOrderType.Location = new System.Drawing.Point(150, 19);
            this.combOrderType.Name = "combOrderType";
            this.combOrderType.Size = new System.Drawing.Size(121, 20);
            this.combOrderType.TabIndex = 7;
            // 
            // combTIF
            // 
            this.combTIF.FormattingEnabled = true;
            this.combTIF.Location = new System.Drawing.Point(150, 43);
            this.combTIF.Name = "combTIF";
            this.combTIF.Size = new System.Drawing.Size(121, 20);
            this.combTIF.TabIndex = 8;
            // 
            // combSide
            // 
            this.combSide.FormattingEnabled = true;
            this.combSide.Location = new System.Drawing.Point(150, 68);
            this.combSide.Name = "combSide";
            this.combSide.Size = new System.Drawing.Size(121, 20);
            this.combSide.TabIndex = 9;
            // 
            // tbSymbol
            // 
            this.tbSymbol.Location = new System.Drawing.Point(150, 99);
            this.tbSymbol.Name = "tbSymbol";
            this.tbSymbol.Size = new System.Drawing.Size(121, 21);
            this.tbSymbol.TabIndex = 10;
            this.tbSymbol.Text = "ICE";
            // 
            // txtContract
            // 
            this.txtContract.Location = new System.Drawing.Point(150, 124);
            this.txtContract.Name = "txtContract";
            this.txtContract.Size = new System.Drawing.Size(121, 21);
            this.txtContract.TabIndex = 11;
            this.txtContract.Text = "DX_P2012 75.5";
            // 
            // tbQty
            // 
            this.tbQty.Location = new System.Drawing.Point(150, 149);
            this.tbQty.Name = "tbQty";
            this.tbQty.Size = new System.Drawing.Size(121, 21);
            this.tbQty.TabIndex = 12;
            this.tbQty.Text = "1";
            // 
            // txtMinQty
            // 
            this.txtMinQty.Location = new System.Drawing.Point(150, 174);
            this.txtMinQty.Name = "txtMinQty";
            this.txtMinQty.Size = new System.Drawing.Size(121, 21);
            this.txtMinQty.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(88, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "Max show:";
            // 
            // tbMaxshow
            // 
            this.tbMaxshow.Location = new System.Drawing.Point(150, 200);
            this.tbMaxshow.Name = "tbMaxshow";
            this.tbMaxshow.Size = new System.Drawing.Size(121, 21);
            this.tbMaxshow.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(81, 311);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "clOrderID:";
            // 
            // tbClOrderID
            // 
            this.tbClOrderID.Location = new System.Drawing.Point(150, 307);
            this.tbClOrderID.Name = "tbClOrderID";
            this.tbClOrderID.Size = new System.Drawing.Size(121, 21);
            this.tbClOrderID.TabIndex = 17;
            this.tbClOrderID.Text = "8000106";
            // 
            // btnAddOrder
            // 
            this.btnAddOrder.Location = new System.Drawing.Point(281, 17);
            this.btnAddOrder.Name = "btnAddOrder";
            this.btnAddOrder.Size = new System.Drawing.Size(78, 23);
            this.btnAddOrder.TabIndex = 18;
            this.btnAddOrder.Text = "下单";
            this.btnAddOrder.UseVisualStyleBackColor = true;
            this.btnAddOrder.Click += new System.EventHandler(this.btnAddOrder_Click);
            // 
            // btnCancelOrder
            // 
            this.btnCancelOrder.Location = new System.Drawing.Point(365, 17);
            this.btnCancelOrder.Name = "btnCancelOrder";
            this.btnCancelOrder.Size = new System.Drawing.Size(75, 23);
            this.btnCancelOrder.TabIndex = 19;
            this.btnCancelOrder.Text = "撤单";
            this.btnCancelOrder.UseVisualStyleBackColor = true;
            this.btnCancelOrder.Click += new System.EventHandler(this.btnCancelOrder_Click);
            // 
            // lbMsgs
            // 
            this.lbMsgs.FormattingEnabled = true;
            this.lbMsgs.HorizontalScrollbar = true;
            this.lbMsgs.ItemHeight = 12;
            this.lbMsgs.Location = new System.Drawing.Point(281, 46);
            this.lbMsgs.Name = "lbMsgs";
            this.lbMsgs.Size = new System.Drawing.Size(327, 124);
            this.lbMsgs.TabIndex = 20;
            this.lbMsgs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbMsgs_KeyUp);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(106, 229);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "Price:";
            // 
            // tbPrice
            // 
            this.tbPrice.Location = new System.Drawing.Point(150, 225);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(121, 21);
            this.tbPrice.TabIndex = 22;
            this.tbPrice.Text = "0.595";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(85, 285);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "Tag50 ID:";
            // 
            // tbTag50ID
            // 
            this.tbTag50ID.Location = new System.Drawing.Point(150, 278);
            this.tbTag50ID.Name = "tbTag50ID";
            this.tbTag50ID.Size = new System.Drawing.Size(121, 21);
            this.tbTag50ID.TabIndex = 24;
            this.tbTag50ID.Text = "0047";
            // 
            // btnOrderCreation
            // 
            this.btnOrderCreation.Location = new System.Drawing.Point(281, 199);
            this.btnOrderCreation.Name = "btnOrderCreation";
            this.btnOrderCreation.Size = new System.Drawing.Size(143, 23);
            this.btnOrderCreation.TabIndex = 25;
            this.btnOrderCreation.Text = "Replye Order Creation";
            this.btnOrderCreation.UseVisualStyleBackColor = true;
            this.btnOrderCreation.Click += new System.EventHandler(this.btnOrderCreation_Click);
            // 
            // btnPartialFill
            // 
            this.btnPartialFill.Location = new System.Drawing.Point(281, 229);
            this.btnPartialFill.Name = "btnPartialFill";
            this.btnPartialFill.Size = new System.Drawing.Size(143, 23);
            this.btnPartialFill.TabIndex = 26;
            this.btnPartialFill.Text = "Reply Partial Fill";
            this.btnPartialFill.UseVisualStyleBackColor = true;
            this.btnPartialFill.Click += new System.EventHandler(this.btnPartialFill_Click);
            // 
            // btnCompleteFill
            // 
            this.btnCompleteFill.Location = new System.Drawing.Point(281, 258);
            this.btnCompleteFill.Name = "btnCompleteFill";
            this.btnCompleteFill.Size = new System.Drawing.Size(143, 23);
            this.btnCompleteFill.TabIndex = 27;
            this.btnCompleteFill.Text = "Reply Complete Fill";
            this.btnCompleteFill.UseVisualStyleBackColor = true;
            this.btnCompleteFill.Click += new System.EventHandler(this.btnCompleteFill_Click);
            // 
            // btnReplyCancelled
            // 
            this.btnReplyCancelled.Location = new System.Drawing.Point(281, 287);
            this.btnReplyCancelled.Name = "btnReplyCancelled";
            this.btnReplyCancelled.Size = new System.Drawing.Size(143, 23);
            this.btnReplyCancelled.TabIndex = 28;
            this.btnReplyCancelled.Text = "Reply Cancelled";
            this.btnReplyCancelled.UseVisualStyleBackColor = true;
            this.btnReplyCancelled.Click += new System.EventHandler(this.btnReplyCancelled_Click);
            // 
            // btnReplyReject
            // 
            this.btnReplyReject.Location = new System.Drawing.Point(470, 200);
            this.btnReplyReject.Name = "btnReplyReject";
            this.btnReplyReject.Size = new System.Drawing.Size(138, 23);
            this.btnReplyReject.TabIndex = 29;
            this.btnReplyReject.Text = "Reply Reject";
            this.btnReplyReject.UseVisualStyleBackColor = true;
            this.btnReplyReject.Click += new System.EventHandler(this.btnReplyReject_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(446, 17);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 30;
            this.btnModify.Text = "改单";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnQueryInstrument
            // 
            this.btnQueryInstrument.Location = new System.Drawing.Point(470, 229);
            this.btnQueryInstrument.Name = "btnQueryInstrument";
            this.btnQueryInstrument.Size = new System.Drawing.Size(138, 23);
            this.btnQueryInstrument.TabIndex = 31;
            this.btnQueryInstrument.Text = "Query Instrument";
            this.btnQueryInstrument.UseVisualStyleBackColor = true;
            this.btnQueryInstrument.Click += new System.EventHandler(this.btnQueryInstrument_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(470, 258);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "Send Fix Msg";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTriggerPrice
            // 
            this.txtTriggerPrice.Location = new System.Drawing.Point(150, 252);
            this.txtTriggerPrice.Name = "txtTriggerPrice";
            this.txtTriggerPrice.Size = new System.Drawing.Size(121, 21);
            this.txtTriggerPrice.TabIndex = 33;
            this.txtTriggerPrice.Text = "59.30";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(64, 255);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 12);
            this.label12.TabIndex = 34;
            this.label12.Text = "TriggerPrice:";
            // 
            // btnOpenDirectory
            // 
            this.btnOpenDirectory.Location = new System.Drawing.Point(470, 287);
            this.btnOpenDirectory.Name = "btnOpenDirectory";
            this.btnOpenDirectory.Size = new System.Drawing.Size(138, 23);
            this.btnOpenDirectory.TabIndex = 35;
            this.btnOpenDirectory.Text = "目录";
            this.btnOpenDirectory.UseVisualStyleBackColor = true;
            this.btnOpenDirectory.Click += new System.EventHandler(this.btnOpenDirectory_Click);
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 352);
            this.Controls.Add(this.btnOpenDirectory);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtTriggerPrice);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnQueryInstrument);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnReplyReject);
            this.Controls.Add(this.btnReplyCancelled);
            this.Controls.Add(this.btnCompleteFill);
            this.Controls.Add(this.btnPartialFill);
            this.Controls.Add(this.btnOrderCreation);
            this.Controls.Add(this.tbTag50ID);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbPrice);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbMsgs);
            this.Controls.Add(this.btnCancelOrder);
            this.Controls.Add(this.btnAddOrder);
            this.Controls.Add(this.tbClOrderID);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbMaxshow);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtMinQty);
            this.Controls.Add(this.tbQty);
            this.Controls.Add(this.txtContract);
            this.Controls.Add(this.tbSymbol);
            this.Controls.Add(this.combSide);
            this.Controls.Add(this.combTIF);
            this.Controls.Add(this.combOrderType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "OrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrderForm";
            this.Load += new System.EventHandler(this.OrderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox combOrderType;
        private System.Windows.Forms.ComboBox combTIF;
        private System.Windows.Forms.ComboBox combSide;
        private System.Windows.Forms.TextBox tbSymbol;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.TextBox tbQty;
        private System.Windows.Forms.TextBox txtMinQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbMaxshow;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbClOrderID;
        private System.Windows.Forms.Button btnAddOrder;
        private System.Windows.Forms.Button btnCancelOrder;
        private System.Windows.Forms.ListBox lbMsgs;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbTag50ID;
        private System.Windows.Forms.Button btnOrderCreation;
        private System.Windows.Forms.Button btnPartialFill;
        private System.Windows.Forms.Button btnCompleteFill;
        private System.Windows.Forms.Button btnReplyCancelled;
        private System.Windows.Forms.Button btnReplyReject;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnQueryInstrument;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtTriggerPrice;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnOpenDirectory;
    }
}