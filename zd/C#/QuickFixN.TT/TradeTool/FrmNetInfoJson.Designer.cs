namespace TradeTool
{
    partial class FrmNetInfoJson
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
            this.rtbNetInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbNetInfo
            // 
            this.rtbNetInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbNetInfo.Location = new System.Drawing.Point(0, 0);
            this.rtbNetInfo.Name = "rtbNetInfo";
            this.rtbNetInfo.ReadOnly = true;
            this.rtbNetInfo.Size = new System.Drawing.Size(614, 612);
            this.rtbNetInfo.TabIndex = 15;
            this.rtbNetInfo.Text = "";
            // 
            // FrmNetInfoJson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 612);
            this.Controls.Add(this.rtbNetInfo);
            this.MaximizeBox = false;
            this.Name = "FrmNetInfoJson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmNetInfoJson";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbNetInfo;
    }
}