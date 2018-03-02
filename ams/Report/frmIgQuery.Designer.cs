namespace ams.Associator
{
    partial class frmIgQuery
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEndIg = new System.Windows.Forms.TextBox();
            this.txtBeginIg = new System.Windows.Forms.TextBox();
            this.chbIg = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAssName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCurIG = new System.Windows.Forms.DataGridView();
            this.dgvIgLog = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurIG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIgLog)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEndIg);
            this.groupBox1.Controls.Add(this.txtBeginIg);
            this.groupBox1.Controls.Add(this.chbIg);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnExcel);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtAssName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCardID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(944, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // txtEndIg
            // 
            this.txtEndIg.Location = new System.Drawing.Point(245, 61);
            this.txtEndIg.Name = "txtEndIg";
            this.txtEndIg.Size = new System.Drawing.Size(128, 21);
            this.txtEndIg.TabIndex = 15;
            this.txtEndIg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEndIg_KeyPress);
            // 
            // txtBeginIg
            // 
            this.txtBeginIg.Location = new System.Drawing.Point(88, 61);
            this.txtBeginIg.Name = "txtBeginIg";
            this.txtBeginIg.Size = new System.Drawing.Size(128, 21);
            this.txtBeginIg.TabIndex = 14;
            this.txtBeginIg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBeginIg_KeyPress);
            // 
            // chbIg
            // 
            this.chbIg.AutoSize = true;
            this.chbIg.Location = new System.Drawing.Point(22, 63);
            this.chbIg.Name = "chbIg";
            this.chbIg.Size = new System.Drawing.Size(60, 16);
            this.chbIg.TabIndex = 13;
            this.chbIg.Text = "积分段";
            this.chbIg.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(680, 59);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(561, 59);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExcel.TabIndex = 11;
            this.btnExcel.Text = "导出";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(449, 59);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 10;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(222, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "至";
            // 
            // txtAssName
            // 
            this.txtAssName.Location = new System.Drawing.Point(310, 23);
            this.txtAssName.Name = "txtAssName";
            this.txtAssName.Size = new System.Drawing.Size(128, 21);
            this.txtAssName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "会员姓名";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(88, 23);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(128, 21);
            this.txtCardID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "会员卡号";
            // 
            // dgvCurIG
            // 
            this.dgvCurIG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurIG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCurIG.Location = new System.Drawing.Point(0, 100);
            this.dgvCurIG.Name = "dgvCurIG";
            this.dgvCurIG.RowTemplate.Height = 23;
            this.dgvCurIG.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCurIG.Size = new System.Drawing.Size(170, 423);
            this.dgvCurIG.TabIndex = 7;
            this.dgvCurIG.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCurIG_CellDoubleClick);
            // 
            // dgvIgLog
            // 
            this.dgvIgLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIgLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvIgLog.Location = new System.Drawing.Point(170, 100);
            this.dgvIgLog.Name = "dgvIgLog";
            this.dgvIgLog.RowTemplate.Height = 23;
            this.dgvIgLog.Size = new System.Drawing.Size(774, 423);
            this.dgvIgLog.TabIndex = 8;
            // 
            // frmIgQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 523);
            this.Controls.Add(this.dgvCurIG);
            this.Controls.Add(this.dgvIgLog);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmIgQuery";
            this.Text = "会员积分查询统计";
            this.Load += new System.EventHandler(this.frmIgQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurIG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIgLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAssName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCurIG;
        private System.Windows.Forms.DataGridView dgvIgLog;
        private System.Windows.Forms.CheckBox chbIg;
        private System.Windows.Forms.TextBox txtEndIg;
        private System.Windows.Forms.TextBox txtBeginIg;
    }
}