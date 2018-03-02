namespace ams.Associator
{
    partial class frmCardFillFee
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAssData = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnFillFee = new System.Windows.Forms.Button();
            this.btnReadCard = new System.Windows.Forms.Button();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtExtraFee = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFillFee = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAssCardID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAssName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFillFee)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnAssData);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnFillFee);
            this.panel1.Controls.Add(this.btnReadCard);
            this.panel1.Controls.Add(this.txtComments);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtSum);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtExtraFee);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtFillFee);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtBalance);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtAssCardID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtAssName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 241);
            this.panel1.TabIndex = 0;
            // 
            // btnAssData
            // 
            this.btnAssData.Location = new System.Drawing.Point(394, 15);
            this.btnAssData.Name = "btnAssData";
            this.btnAssData.Size = new System.Drawing.Size(75, 23);
            this.btnAssData.TabIndex = 18;
            this.btnAssData.Text = "会员资料";
            this.btnAssData.UseVisualStyleBackColor = true;
            this.btnAssData.Click += new System.EventHandler(this.btnAssData_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(322, 196);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(228, 196);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 16;
            this.btnPrint.Text = "打印回执";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnFillFee
            // 
            this.btnFillFee.Location = new System.Drawing.Point(124, 197);
            this.btnFillFee.Name = "btnFillFee";
            this.btnFillFee.Size = new System.Drawing.Size(75, 23);
            this.btnFillFee.TabIndex = 15;
            this.btnFillFee.Text = "充值";
            this.btnFillFee.UseVisualStyleBackColor = true;
            this.btnFillFee.Click += new System.EventHandler(this.btnFillFee_Click);
            // 
            // btnReadCard
            // 
            this.btnReadCard.Location = new System.Drawing.Point(28, 197);
            this.btnReadCard.Name = "btnReadCard";
            this.btnReadCard.Size = new System.Drawing.Size(75, 23);
            this.btnReadCard.TabIndex = 14;
            this.btnReadCard.Text = "读卡";
            this.btnReadCard.UseVisualStyleBackColor = true;
            this.btnReadCard.Click += new System.EventHandler(this.btnReadCard_Click);
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(95, 113);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(293, 51);
            this.txtComments.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "备注：";
            // 
            // txtSum
            // 
            this.txtSum.Enabled = false;
            this.txtSum.Location = new System.Drawing.Point(288, 82);
            this.txtSum.Name = "txtSum";
            this.txtSum.Size = new System.Drawing.Size(100, 21);
            this.txtSum.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(231, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "总金额：";
            // 
            // txtExtraFee
            // 
            this.txtExtraFee.Enabled = false;
            this.txtExtraFee.Location = new System.Drawing.Point(93, 82);
            this.txtExtraFee.Name = "txtExtraFee";
            this.txtExtraFee.Size = new System.Drawing.Size(100, 21);
            this.txtExtraFee.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "赠送金额：";
            // 
            // txtFillFee
            // 
            this.txtFillFee.Location = new System.Drawing.Point(288, 50);
            this.txtFillFee.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.txtFillFee.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtFillFee.Name = "txtFillFee";
            this.txtFillFee.Size = new System.Drawing.Size(100, 21);
            this.txtFillFee.TabIndex = 7;
            this.txtFillFee.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtFillFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFillFee_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "充值金额：";
            // 
            // txtBalance
            // 
            this.txtBalance.Enabled = false;
            this.txtBalance.Location = new System.Drawing.Point(93, 50);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(100, 21);
            this.txtBalance.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "当前余额：";
            // 
            // txtAssCardID
            // 
            this.txtAssCardID.Enabled = false;
            this.txtAssCardID.Location = new System.Drawing.Point(288, 16);
            this.txtAssCardID.Name = "txtAssCardID";
            this.txtAssCardID.Size = new System.Drawing.Size(100, 21);
            this.txtAssCardID.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "会员卡号：";
            // 
            // txtAssName
            // 
            this.txtAssName.Enabled = false;
            this.txtAssName.Location = new System.Drawing.Point(93, 17);
            this.txtAssName.Name = "txtAssName";
            this.txtAssName.Size = new System.Drawing.Size(100, 21);
            this.txtAssName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "会员姓名：";
            // 
            // frmCardFillFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 266);
            this.Controls.Add(this.panel1);
            this.Name = "frmCardFillFee";
            this.Text = "会员卡充值";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFillFee)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAssName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAssCardID;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtFillFee;
        private System.Windows.Forms.TextBox txtExtraFee;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnFillFee;
        private System.Windows.Forms.Button btnReadCard;
        private System.Windows.Forms.Button btnAssData;
    }
}