namespace ams.Associator
{
    partial class frmAssIgExchange
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
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.txtAssName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurCharge = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurIg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbIgEXGoods = new System.Windows.Forms.ComboBox();
            this.txtIgExCost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvIgEx = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnExOk = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIgSum = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.txtAssID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIgEx)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "会员卡号";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(130, 18);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.ReadOnly = true;
            this.txtCardID.Size = new System.Drawing.Size(121, 21);
            this.txtCardID.TabIndex = 9;
            // 
            // txtAssName
            // 
            this.txtAssName.Location = new System.Drawing.Point(365, 18);
            this.txtAssName.Name = "txtAssName";
            this.txtAssName.ReadOnly = true;
            this.txtAssName.Size = new System.Drawing.Size(122, 21);
            this.txtAssName.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "会员姓名";
            // 
            // txtCurCharge
            // 
            this.txtCurCharge.Location = new System.Drawing.Point(130, 53);
            this.txtCurCharge.Name = "txtCurCharge";
            this.txtCurCharge.ReadOnly = true;
            this.txtCurCharge.Size = new System.Drawing.Size(121, 21);
            this.txtCurCharge.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "当前余额";
            // 
            // txtCurIg
            // 
            this.txtCurIg.Location = new System.Drawing.Point(365, 53);
            this.txtCurIg.Name = "txtCurIg";
            this.txtCurIg.ReadOnly = true;
            this.txtCurIg.Size = new System.Drawing.Size(122, 21);
            this.txtCurIg.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(306, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "当前积分";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "积分兑换项目";
            // 
            // cmbIgEXGoods
            // 
            this.cmbIgEXGoods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIgEXGoods.FormattingEnabled = true;
            this.cmbIgEXGoods.Location = new System.Drawing.Point(114, 109);
            this.cmbIgEXGoods.Name = "cmbIgEXGoods";
            this.cmbIgEXGoods.Size = new System.Drawing.Size(188, 20);
            this.cmbIgEXGoods.TabIndex = 1;
            this.cmbIgEXGoods.SelectedIndexChanged += new System.EventHandler(this.cmbIgEXGoods_SelectedIndexChanged);
            // 
            // txtIgExCost
            // 
            this.txtIgExCost.Location = new System.Drawing.Point(401, 108);
            this.txtIgExCost.Name = "txtIgExCost";
            this.txtIgExCost.ReadOnly = true;
            this.txtIgExCost.Size = new System.Drawing.Size(109, 21);
            this.txtIgExCost.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(342, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "兑换分值";
            // 
            // dgvIgEx
            // 
            this.dgvIgEx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIgEx.Location = new System.Drawing.Point(17, 215);
            this.dgvIgEx.Name = "dgvIgEx";
            this.dgvIgEx.RowTemplate.Height = 23;
            this.dgvIgEx.Size = new System.Drawing.Size(438, 188);
            this.dgvIgEx.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(473, 257);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "<<增加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(473, 307);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(60, 23);
            this.btnDel.TabIndex = 5;
            this.btnDel.Text = "删除>>";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(48, 418);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(60, 23);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "读卡";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnExOk
            // 
            this.btnExOk.Location = new System.Drawing.Point(163, 418);
            this.btnExOk.Name = "btnExOk";
            this.btnExOk.Size = new System.Drawing.Size(60, 23);
            this.btnExOk.TabIndex = 7;
            this.btnExOk.Text = "兑换";
            this.btnExOk.UseVisualStyleBackColor = true;
            this.btnExOk.Click += new System.EventHandler(this.btnExOk_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(291, 418);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(68, 23);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "打印回执";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(427, 418);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(66, 141);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComments.Size = new System.Drawing.Size(236, 68);
            this.txtComments.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "备注";
            // 
            // txtIgSum
            // 
            this.txtIgSum.Location = new System.Drawing.Point(401, 177);
            this.txtIgSum.Name = "txtIgSum";
            this.txtIgSum.ReadOnly = true;
            this.txtIgSum.Size = new System.Drawing.Size(109, 21);
            this.txtIgSum.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(318, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "使用积分合计";
            // 
            // txtBillNo
            // 
            this.txtBillNo.Location = new System.Drawing.Point(344, 141);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.ReadOnly = true;
            this.txtBillNo.Size = new System.Drawing.Size(54, 21);
            this.txtBillNo.TabIndex = 24;
            this.txtBillNo.Visible = false;
            // 
            // txtAssID
            // 
            this.txtAssID.Location = new System.Drawing.Point(433, 141);
            this.txtAssID.Name = "txtAssID";
            this.txtAssID.ReadOnly = true;
            this.txtAssID.Size = new System.Drawing.Size(54, 21);
            this.txtAssID.TabIndex = 25;
            this.txtAssID.Visible = false;
            // 
            // frmAssIgExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 457);
            this.Controls.Add(this.txtAssID);
            this.Controls.Add(this.txtBillNo);
            this.Controls.Add(this.txtIgSum);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExOk);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvIgEx);
            this.Controls.Add(this.txtIgExCost);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbIgEXGoods);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCurIg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCurCharge);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAssName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAssIgExchange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "积分兑换";
            this.Load += new System.EventHandler(this.frmAssIgExchange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIgEx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.TextBox txtAssName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCurCharge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurIg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbIgEXGoods;
        private System.Windows.Forms.TextBox txtIgExCost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvIgEx;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnExOk;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIgSum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.TextBox txtAssID;
    }
}