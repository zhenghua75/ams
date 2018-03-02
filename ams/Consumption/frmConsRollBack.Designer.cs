namespace ams.Associator
{
    partial class frmConsRollBack
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
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAssName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCurIG = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCurCharge = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConsDate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConsFee = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnRollBack = new System.Windows.Forms.Button();
            this.dgvConsList = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.txtConsSerial = new System.Windows.Forms.TextBox();
            this.txtAssID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(66, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "帐单返销只允许返销最后一次消费帐单记录。\r\n如果在此之后做过积分兑换，则不能返销。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "帐单号";
            // 
            // txtBillNo
            // 
            this.txtBillNo.Location = new System.Drawing.Point(295, 100);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.ReadOnly = true;
            this.txtBillNo.Size = new System.Drawing.Size(100, 21);
            this.txtBillNo.TabIndex = 2;
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(82, 63);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(100, 21);
            this.txtCardID.TabIndex = 0;
            this.txtCardID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCardID_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "会员卡号";
            // 
            // txtAssName
            // 
            this.txtAssName.Location = new System.Drawing.Point(82, 100);
            this.txtAssName.Name = "txtAssName";
            this.txtAssName.ReadOnly = true;
            this.txtAssName.Size = new System.Drawing.Size(100, 21);
            this.txtAssName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "会员姓名";
            // 
            // txtCurIG
            // 
            this.txtCurIG.Location = new System.Drawing.Point(295, 138);
            this.txtCurIG.Name = "txtCurIG";
            this.txtCurIG.ReadOnly = true;
            this.txtCurIG.Size = new System.Drawing.Size(100, 21);
            this.txtCurIG.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "当前积分";
            // 
            // txtCurCharge
            // 
            this.txtCurCharge.Location = new System.Drawing.Point(82, 138);
            this.txtCurCharge.Name = "txtCurCharge";
            this.txtCurCharge.ReadOnly = true;
            this.txtCurCharge.Size = new System.Drawing.Size(100, 21);
            this.txtCurCharge.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "当前余额";
            // 
            // txtConsDate
            // 
            this.txtConsDate.Location = new System.Drawing.Point(295, 175);
            this.txtConsDate.Name = "txtConsDate";
            this.txtConsDate.ReadOnly = true;
            this.txtConsDate.Size = new System.Drawing.Size(100, 21);
            this.txtConsDate.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(236, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "消费时间";
            // 
            // txtConsFee
            // 
            this.txtConsFee.Location = new System.Drawing.Point(82, 175);
            this.txtConsFee.Name = "txtConsFee";
            this.txtConsFee.ReadOnly = true;
            this.txtConsFee.Size = new System.Drawing.Size(100, 21);
            this.txtConsFee.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "消费金额";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 221);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "消费商品如下：";
            // 
            // btnRollBack
            // 
            this.btnRollBack.Location = new System.Drawing.Point(334, 202);
            this.btnRollBack.Name = "btnRollBack";
            this.btnRollBack.Size = new System.Drawing.Size(61, 23);
            this.btnRollBack.TabIndex = 7;
            this.btnRollBack.Text = "返销";
            this.btnRollBack.UseVisualStyleBackColor = true;
            this.btnRollBack.Click += new System.EventHandler(this.btnRollBack_Click);
            // 
            // dgvConsList
            // 
            this.dgvConsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsList.Location = new System.Drawing.Point(12, 238);
            this.dgvConsList.Name = "dgvConsList";
            this.dgvConsList.RowTemplate.Height = 23;
            this.dgvConsList.Size = new System.Drawing.Size(397, 188);
            this.dgvConsList.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(188, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "<-输入会员卡号，回车";
            // 
            // txtConsSerial
            // 
            this.txtConsSerial.Location = new System.Drawing.Point(160, 206);
            this.txtConsSerial.Name = "txtConsSerial";
            this.txtConsSerial.ReadOnly = true;
            this.txtConsSerial.Size = new System.Drawing.Size(62, 21);
            this.txtConsSerial.TabIndex = 19;
            this.txtConsSerial.Visible = false;
            // 
            // txtAssID
            // 
            this.txtAssID.Location = new System.Drawing.Point(242, 206);
            this.txtAssID.Name = "txtAssID";
            this.txtAssID.ReadOnly = true;
            this.txtAssID.Size = new System.Drawing.Size(71, 21);
            this.txtAssID.TabIndex = 20;
            this.txtAssID.Visible = false;
            // 
            // frmConsRollBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 433);
            this.Controls.Add(this.txtAssID);
            this.Controls.Add(this.txtConsSerial);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dgvConsList);
            this.Controls.Add(this.btnRollBack);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtConsDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtConsFee);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCurIG);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCurCharge);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAssName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBillNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsRollBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帐单返销";
            this.Load += new System.EventHandler(this.frmConsRollBack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAssName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCurIG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCurCharge;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtConsDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtConsFee;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnRollBack;
        private System.Windows.Forms.DataGridView dgvConsList;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtConsSerial;
        private System.Windows.Forms.TextBox txtAssID;
    }
}