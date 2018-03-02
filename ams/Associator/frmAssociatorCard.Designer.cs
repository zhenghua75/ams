namespace ams.Associator
{
    partial class frmAssociatorCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssociatorCard));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkEndInAssDate = new System.Windows.Forms.CheckBox();
            this.chkBeginInAssDate = new System.Windows.Forms.CheckBox();
            this.chkEndOperDate = new System.Windows.Forms.CheckBox();
            this.chkBeginOperDate = new System.Windows.Forms.CheckBox();
            this.cmbCardState = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAssCardID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndOperDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndInDate = new System.Windows.Forms.DateTimePicker();
            this.cmbOper = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpBeginOperDate = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cmbAssState = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbAssLevel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpBeginInDate = new System.Windows.Forms.DateTimePicker();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnCardFillFee = new System.Windows.Forms.ToolStripButton();
            this.btnCardLose = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCardAdd = new System.Windows.Forms.ToolStripButton();
            this.btnCardFree = new System.Windows.Forms.ToolStripButton();
            this.btnCardReturn = new System.Windows.Forms.ToolStripButton();
            this.btnCardCallback = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chkEndInAssDate);
            this.panel1.Controls.Add(this.chkBeginInAssDate);
            this.panel1.Controls.Add(this.chkEndOperDate);
            this.panel1.Controls.Add(this.chkBeginOperDate);
            this.panel1.Controls.Add(this.cmbCardState);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtAssCardID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtpEndOperDate);
            this.panel1.Controls.Add(this.dtpEndInDate);
            this.panel1.Controls.Add(this.cmbOper);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dtpBeginOperDate);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.cmbAssState);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbAssLevel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpBeginInDate);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Location = new System.Drawing.Point(8, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(669, 137);
            this.panel1.TabIndex = 2;
            // 
            // chkEndInAssDate
            // 
            this.chkEndInAssDate.AutoSize = true;
            this.chkEndInAssDate.Location = new System.Drawing.Point(4, 101);
            this.chkEndInAssDate.Name = "chkEndInAssDate";
            this.chkEndInAssDate.Size = new System.Drawing.Size(96, 16);
            this.chkEndInAssDate.TabIndex = 31;
            this.chkEndInAssDate.Text = "入会结束日期";
            this.chkEndInAssDate.UseVisualStyleBackColor = true;
            // 
            // chkBeginInAssDate
            // 
            this.chkBeginInAssDate.AutoSize = true;
            this.chkBeginInAssDate.Location = new System.Drawing.Point(4, 72);
            this.chkBeginInAssDate.Name = "chkBeginInAssDate";
            this.chkBeginInAssDate.Size = new System.Drawing.Size(96, 16);
            this.chkBeginInAssDate.TabIndex = 30;
            this.chkBeginInAssDate.Text = "入会开始日期";
            this.chkBeginInAssDate.UseVisualStyleBackColor = true;
            // 
            // chkEndOperDate
            // 
            this.chkEndOperDate.AutoSize = true;
            this.chkEndOperDate.Location = new System.Drawing.Point(4, 43);
            this.chkEndOperDate.Name = "chkEndOperDate";
            this.chkEndOperDate.Size = new System.Drawing.Size(96, 16);
            this.chkEndOperDate.TabIndex = 29;
            this.chkEndOperDate.Text = "操作结束日期";
            this.chkEndOperDate.UseVisualStyleBackColor = true;
            // 
            // chkBeginOperDate
            // 
            this.chkBeginOperDate.AutoSize = true;
            this.chkBeginOperDate.Location = new System.Drawing.Point(4, 14);
            this.chkBeginOperDate.Name = "chkBeginOperDate";
            this.chkBeginOperDate.Size = new System.Drawing.Size(96, 16);
            this.chkBeginOperDate.TabIndex = 28;
            this.chkBeginOperDate.Text = "操作开始日期";
            this.chkBeginOperDate.UseVisualStyleBackColor = true;
            // 
            // cmbCardState
            // 
            this.cmbCardState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardState.FormattingEnabled = true;
            this.cmbCardState.Location = new System.Drawing.Point(329, 100);
            this.cmbCardState.Name = "cmbCardState";
            this.cmbCardState.Size = new System.Drawing.Size(117, 20);
            this.cmbCardState.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(249, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "会员卡状态：";
            // 
            // txtAssCardID
            // 
            this.txtAssCardID.Location = new System.Drawing.Point(544, 13);
            this.txtAssCardID.Name = "txtAssCardID";
            this.txtAssCardID.Size = new System.Drawing.Size(117, 21);
            this.txtAssCardID.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(464, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "会员卡卡号：";
            // 
            // dtpEndOperDate
            // 
            this.dtpEndOperDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndOperDate.Location = new System.Drawing.Point(103, 40);
            this.dtpEndOperDate.Name = "dtpEndOperDate";
            this.dtpEndOperDate.Size = new System.Drawing.Size(121, 21);
            this.dtpEndOperDate.TabIndex = 20;
            this.dtpEndOperDate.Value = new System.DateTime(2009, 3, 9, 0, 0, 0, 0);
            // 
            // dtpEndInDate
            // 
            this.dtpEndInDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndInDate.Location = new System.Drawing.Point(103, 98);
            this.dtpEndInDate.Name = "dtpEndInDate";
            this.dtpEndInDate.Size = new System.Drawing.Size(121, 21);
            this.dtpEndInDate.TabIndex = 19;
            this.dtpEndInDate.Value = new System.DateTime(2009, 3, 9, 0, 0, 0, 0);
            // 
            // cmbOper
            // 
            this.cmbOper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOper.FormattingEnabled = true;
            this.cmbOper.Location = new System.Drawing.Point(329, 72);
            this.cmbOper.Name = "cmbOper";
            this.cmbOper.Size = new System.Drawing.Size(117, 20);
            this.cmbOper.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(273, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "操作员：";
            // 
            // dtpBeginOperDate
            // 
            this.dtpBeginOperDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBeginOperDate.Location = new System.Drawing.Point(103, 12);
            this.dtpBeginOperDate.Name = "dtpBeginOperDate";
            this.dtpBeginOperDate.Size = new System.Drawing.Size(121, 21);
            this.dtpBeginOperDate.TabIndex = 15;
            this.dtpBeginOperDate.Value = new System.DateTime(2009, 3, 9, 0, 0, 0, 0);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(544, 76);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 13;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cmbAssState
            // 
            this.cmbAssState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssState.FormattingEnabled = true;
            this.cmbAssState.Location = new System.Drawing.Point(325, 43);
            this.cmbAssState.Name = "cmbAssState";
            this.cmbAssState.Size = new System.Drawing.Size(121, 20);
            this.cmbAssState.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "会员状态：";
            // 
            // cmbAssLevel
            // 
            this.cmbAssLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssLevel.FormattingEnabled = true;
            this.cmbAssLevel.Location = new System.Drawing.Point(325, 14);
            this.cmbAssLevel.Name = "cmbAssLevel";
            this.cmbAssLevel.Size = new System.Drawing.Size(121, 20);
            this.cmbAssLevel.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(259, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "会员级别：";
            // 
            // dtpBeginInDate
            // 
            this.dtpBeginInDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBeginInDate.Location = new System.Drawing.Point(103, 69);
            this.dtpBeginInDate.Name = "dtpBeginInDate";
            this.dtpBeginInDate.Size = new System.Drawing.Size(121, 21);
            this.dtpBeginInDate.TabIndex = 8;
            this.dtpBeginInDate.Value = new System.DateTime(2009, 3, 9, 0, 0, 0, 0);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(466, 45);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(195, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // btnCardFillFee
            // 
            this.btnCardFillFee.Image = ((System.Drawing.Image)(resources.GetObject("btnCardFillFee.Image")));
            this.btnCardFillFee.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardFillFee.Name = "btnCardFillFee";
            this.btnCardFillFee.Size = new System.Drawing.Size(85, 22);
            this.btnCardFillFee.Text = "会员卡充值";
            this.btnCardFillFee.Click += new System.EventHandler(this.btnCardFillFee_Click);
            // 
            // btnCardLose
            // 
            this.btnCardLose.Image = ((System.Drawing.Image)(resources.GetObject("btnCardLose.Image")));
            this.btnCardLose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardLose.Name = "btnCardLose";
            this.btnCardLose.Size = new System.Drawing.Size(85, 22);
            this.btnCardLose.Text = "会员卡挂失";
            this.btnCardLose.ToolTipText = "会员卡挂失：操作会员卡状态为【正常在用】的会员卡";
            this.btnCardLose.Click += new System.EventHandler(this.btnCardLose_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCardFillFee,
            this.btnCardLose,
            this.btnCardAdd,
            this.btnCardFree,
            this.btnCardReturn,
            this.btnCardCallback});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(668, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCardAdd
            // 
            this.btnCardAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnCardAdd.Image")));
            this.btnCardAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardAdd.Name = "btnCardAdd";
            this.btnCardAdd.Size = new System.Drawing.Size(85, 22);
            this.btnCardAdd.Text = "会员卡补卡";
            this.btnCardAdd.ToolTipText = "会员卡补卡：操作会员卡状态为【挂失】的会员卡";
            this.btnCardAdd.Click += new System.EventHandler(this.btnCardAdd_Click);
            // 
            // btnCardFree
            // 
            this.btnCardFree.Image = ((System.Drawing.Image)(resources.GetObject("btnCardFree.Image")));
            this.btnCardFree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardFree.Name = "btnCardFree";
            this.btnCardFree.Size = new System.Drawing.Size(85, 22);
            this.btnCardFree.Text = "会员卡解挂";
            this.btnCardFree.ToolTipText = "会员卡解挂：操作会员卡状态为【挂失】的会员卡";
            this.btnCardFree.Click += new System.EventHandler(this.btnCardFree_Click);
            // 
            // btnCardReturn
            // 
            this.btnCardReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnCardReturn.Image")));
            this.btnCardReturn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardReturn.Name = "btnCardReturn";
            this.btnCardReturn.Size = new System.Drawing.Size(85, 22);
            this.btnCardReturn.Text = "会员卡退卡";
            this.btnCardReturn.ToolTipText = "会员卡退卡：操作会员卡状态在【正常在用】的会员卡";
            this.btnCardReturn.Click += new System.EventHandler(this.btnCardReturn_Click);
            // 
            // btnCardCallback
            // 
            this.btnCardCallback.Image = ((System.Drawing.Image)(resources.GetObject("btnCardCallback.Image")));
            this.btnCardCallback.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardCallback.Name = "btnCardCallback";
            this.btnCardCallback.Size = new System.Drawing.Size(85, 22);
            this.btnCardCallback.Text = "会员卡回收";
            this.btnCardCallback.ToolTipText = "会员卡回收：操作会员卡状态为【退卡】的会员卡";
            this.btnCardCallback.Click += new System.EventHandler(this.btnCardCallback_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(668, 155);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Location = new System.Drawing.Point(9, 147);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(668, 180);
            this.panel2.TabIndex = 3;
            // 
            // frmAssociatorCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 346);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frmAssociatorCard";
            this.Text = "会员卡资料设置";
            this.Load += new System.EventHandler(this.frmAssociatorCard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpEndOperDate;
        private System.Windows.Forms.DateTimePicker dtpEndInDate;
        private System.Windows.Forms.ComboBox cmbOper;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpBeginOperDate;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cmbAssState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbAssLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpBeginInDate;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ToolStripButton btnCardFillFee;
        private System.Windows.Forms.ToolStripButton btnCardLose;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnCardAdd;
        private System.Windows.Forms.ToolStripButton btnCardFree;
        private System.Windows.Forms.ToolStripButton btnCardReturn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton btnCardCallback;
        private System.Windows.Forms.TextBox txtAssCardID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCardState;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkBeginOperDate;
        private System.Windows.Forms.CheckBox chkEndInAssDate;
        private System.Windows.Forms.CheckBox chkBeginInAssDate;
        private System.Windows.Forms.CheckBox chkEndOperDate;
    }
}