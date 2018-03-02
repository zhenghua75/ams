namespace ams.Associator
{
    partial class frmCardAdd
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
            this.txtCardAdd = new System.Windows.Forms.MaskedTextBox();
            this.lblCardAdd = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAgf = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCardAdd = new System.Windows.Forms.Button();
            this.dtpExpDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpPutCardDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIg = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCardState = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbAssLevel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAssName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAssCardID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCardAdd);
            this.panel1.Controls.Add(this.lblCardAdd);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lblAgf);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnCardAdd);
            this.panel1.Controls.Add(this.dtpExpDate);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.dtpPutCardDate);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtIg);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtBalance);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbCardState);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbAssLevel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtAssName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtAssCardID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 251);
            this.panel1.TabIndex = 0;
            // 
            // txtCardAdd
            // 
            this.txtCardAdd.Location = new System.Drawing.Point(156, 159);
            this.txtCardAdd.Mask = "99999";
            this.txtCardAdd.Name = "txtCardAdd";
            this.txtCardAdd.Size = new System.Drawing.Size(100, 21);
            this.txtCardAdd.TabIndex = 23;
            this.txtCardAdd.ValidatingType = typeof(int);
            // 
            // lblCardAdd
            // 
            this.lblCardAdd.AutoSize = true;
            this.lblCardAdd.Location = new System.Drawing.Point(88, 163);
            this.lblCardAdd.Name = "lblCardAdd";
            this.lblCardAdd.Size = new System.Drawing.Size(65, 12);
            this.lblCardAdd.TabIndex = 22;
            this.lblCardAdd.Text = "补卡卡号：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label11.Location = new System.Drawing.Point(233, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "元";
            // 
            // lblAgf
            // 
            this.lblAgf.AutoSize = true;
            this.lblAgf.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblAgf.Location = new System.Drawing.Point(177, 135);
            this.lblAgf.Name = "lblAgf";
            this.lblAgf.Size = new System.Drawing.Size(23, 12);
            this.lblAgf.TabIndex = 20;
            this.lblAgf.Text = "500";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(79, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "补发卡工本费为：";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(243, 186);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(162, 186);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 17;
            this.btnPrint.Text = "打印回执";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCardAdd
            // 
            this.btnCardAdd.Location = new System.Drawing.Point(81, 186);
            this.btnCardAdd.Name = "btnCardAdd";
            this.btnCardAdd.Size = new System.Drawing.Size(75, 23);
            this.btnCardAdd.TabIndex = 16;
            this.btnCardAdd.Text = "补发卡";
            this.btnCardAdd.UseVisualStyleBackColor = true;
            this.btnCardAdd.Click += new System.EventHandler(this.btnCardAdd_Click);
            // 
            // dtpExpDate
            // 
            this.dtpExpDate.Enabled = false;
            this.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpDate.Location = new System.Drawing.Point(273, 92);
            this.dtpExpDate.Name = "dtpExpDate";
            this.dtpExpDate.Size = new System.Drawing.Size(100, 21);
            this.dtpExpDate.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(207, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "失效日期：";
            // 
            // dtpPutCardDate
            // 
            this.dtpPutCardDate.Enabled = false;
            this.dtpPutCardDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPutCardDate.Location = new System.Drawing.Point(81, 98);
            this.dtpPutCardDate.Name = "dtpPutCardDate";
            this.dtpPutCardDate.Size = new System.Drawing.Size(100, 21);
            this.dtpPutCardDate.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "发卡日期：";
            // 
            // txtIg
            // 
            this.txtIg.Enabled = false;
            this.txtIg.Location = new System.Drawing.Point(273, 61);
            this.txtIg.Name = "txtIg";
            this.txtIg.Size = new System.Drawing.Size(100, 21);
            this.txtIg.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "剩余积分：";
            // 
            // txtBalance
            // 
            this.txtBalance.Enabled = false;
            this.txtBalance.Location = new System.Drawing.Point(81, 66);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(100, 21);
            this.txtBalance.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "卡余额：";
            // 
            // cmbCardState
            // 
            this.cmbCardState.Enabled = false;
            this.cmbCardState.FormattingEnabled = true;
            this.cmbCardState.Location = new System.Drawing.Point(273, 35);
            this.cmbCardState.Name = "cmbCardState";
            this.cmbCardState.Size = new System.Drawing.Size(100, 20);
            this.cmbCardState.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(207, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "卡状态：";
            // 
            // cmbAssLevel
            // 
            this.cmbAssLevel.Enabled = false;
            this.cmbAssLevel.FormattingEnabled = true;
            this.cmbAssLevel.Location = new System.Drawing.Point(81, 39);
            this.cmbAssLevel.Name = "cmbAssLevel";
            this.cmbAssLevel.Size = new System.Drawing.Size(100, 20);
            this.cmbAssLevel.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "会员级别：";
            // 
            // txtAssName
            // 
            this.txtAssName.Enabled = false;
            this.txtAssName.Location = new System.Drawing.Point(273, 10);
            this.txtAssName.Name = "txtAssName";
            this.txtAssName.Size = new System.Drawing.Size(100, 21);
            this.txtAssName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "会员姓名：";
            // 
            // txtAssCardID
            // 
            this.txtAssCardID.Enabled = false;
            this.txtAssCardID.Location = new System.Drawing.Point(81, 10);
            this.txtAssCardID.Name = "txtAssCardID";
            this.txtAssCardID.Size = new System.Drawing.Size(100, 21);
            this.txtAssCardID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "会员卡号：";
            // 
            // frmCardAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 276);
            this.Controls.Add(this.panel1);
            this.Name = "frmCardAdd";
            this.Text = "会员卡补卡";
            this.Load += new System.EventHandler(this.frmCardAdd_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAssName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAssCardID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCardState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbAssLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpPutCardDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpExpDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCardAdd;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAgf;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox txtCardAdd;
        private System.Windows.Forms.Label lblCardAdd;
    }
}