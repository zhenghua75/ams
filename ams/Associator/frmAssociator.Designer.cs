namespace ams.Associator
{
    partial class frmAssociator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssociator));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkEndInAssDate = new System.Windows.Forms.CheckBox();
            this.chkBeginInAssDate = new System.Windows.Forms.CheckBox();
            this.chkEndOperDate = new System.Windows.Forms.CheckBox();
            this.chkBeginOperDate = new System.Windows.Forms.CheckBox();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddAss = new System.Windows.Forms.ToolStripButton();
            this.btnModifyAss = new System.Windows.Forms.ToolStripButton();
            this.btnDetailAss = new System.Windows.Forms.ToolStripButton();
            this.btnAddCard = new System.Windows.Forms.ToolStripButton();
            this.btnModifyCardPwd = new System.Windows.Forms.ToolStripButton();
            this.btnInitCardPwd = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
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
            this.panel1.Location = new System.Drawing.Point(12, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 137);
            this.panel1.TabIndex = 0;
            // 
            // chkEndInAssDate
            // 
            this.chkEndInAssDate.AutoSize = true;
            this.chkEndInAssDate.Location = new System.Drawing.Point(7, 94);
            this.chkEndInAssDate.Name = "chkEndInAssDate";
            this.chkEndInAssDate.Size = new System.Drawing.Size(96, 16);
            this.chkEndInAssDate.TabIndex = 35;
            this.chkEndInAssDate.Text = "入会结束日期";
            this.chkEndInAssDate.UseVisualStyleBackColor = true;
            // 
            // chkBeginInAssDate
            // 
            this.chkBeginInAssDate.AutoSize = true;
            this.chkBeginInAssDate.Location = new System.Drawing.Point(7, 65);
            this.chkBeginInAssDate.Name = "chkBeginInAssDate";
            this.chkBeginInAssDate.Size = new System.Drawing.Size(96, 16);
            this.chkBeginInAssDate.TabIndex = 34;
            this.chkBeginInAssDate.Text = "入会开始日期";
            this.chkBeginInAssDate.UseVisualStyleBackColor = true;
            // 
            // chkEndOperDate
            // 
            this.chkEndOperDate.AutoSize = true;
            this.chkEndOperDate.Location = new System.Drawing.Point(7, 36);
            this.chkEndOperDate.Name = "chkEndOperDate";
            this.chkEndOperDate.Size = new System.Drawing.Size(96, 16);
            this.chkEndOperDate.TabIndex = 33;
            this.chkEndOperDate.Text = "操作结束日期";
            this.chkEndOperDate.UseVisualStyleBackColor = true;
            // 
            // chkBeginOperDate
            // 
            this.chkBeginOperDate.AutoSize = true;
            this.chkBeginOperDate.Location = new System.Drawing.Point(7, 7);
            this.chkBeginOperDate.Name = "chkBeginOperDate";
            this.chkBeginOperDate.Size = new System.Drawing.Size(96, 16);
            this.chkBeginOperDate.TabIndex = 32;
            this.chkBeginOperDate.Text = "操作开始日期";
            this.chkBeginOperDate.UseVisualStyleBackColor = true;
            // 
            // dtpEndOperDate
            // 
            this.dtpEndOperDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndOperDate.Location = new System.Drawing.Point(107, 33);
            this.dtpEndOperDate.Name = "dtpEndOperDate";
            this.dtpEndOperDate.Size = new System.Drawing.Size(122, 21);
            this.dtpEndOperDate.TabIndex = 20;
            this.dtpEndOperDate.Value = new System.DateTime(2009, 3, 9, 0, 0, 0, 0);
            // 
            // dtpEndInDate
            // 
            this.dtpEndInDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndInDate.Location = new System.Drawing.Point(107, 92);
            this.dtpEndInDate.Name = "dtpEndInDate";
            this.dtpEndInDate.Size = new System.Drawing.Size(121, 21);
            this.dtpEndInDate.TabIndex = 19;
            this.dtpEndInDate.Value = new System.DateTime(2009, 3, 9, 0, 0, 0, 0);
            // 
            // cmbOper
            // 
            this.cmbOper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOper.FormattingEnabled = true;
            this.cmbOper.Location = new System.Drawing.Point(329, 62);
            this.cmbOper.Name = "cmbOper";
            this.cmbOper.Size = new System.Drawing.Size(121, 20);
            this.cmbOper.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(273, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "操作员：";
            // 
            // dtpBeginOperDate
            // 
            this.dtpBeginOperDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBeginOperDate.Location = new System.Drawing.Point(108, 6);
            this.dtpBeginOperDate.Name = "dtpBeginOperDate";
            this.dtpBeginOperDate.Size = new System.Drawing.Size(121, 21);
            this.dtpBeginOperDate.TabIndex = 15;
            this.dtpBeginOperDate.Value = new System.DateTime(2009, 3, 9, 0, 0, 0, 0);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(480, 92);
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
            this.cmbAssState.Location = new System.Drawing.Point(329, 33);
            this.cmbAssState.Name = "cmbAssState";
            this.cmbAssState.Size = new System.Drawing.Size(121, 20);
            this.cmbAssState.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(263, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "会员状态：";
            // 
            // cmbAssLevel
            // 
            this.cmbAssLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssLevel.FormattingEnabled = true;
            this.cmbAssLevel.Location = new System.Drawing.Point(329, 6);
            this.cmbAssLevel.Name = "cmbAssLevel";
            this.cmbAssLevel.Size = new System.Drawing.Size(121, 20);
            this.cmbAssLevel.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "会员级别：";
            // 
            // dtpBeginInDate
            // 
            this.dtpBeginInDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBeginInDate.Location = new System.Drawing.Point(108, 62);
            this.dtpBeginInDate.Name = "dtpBeginInDate";
            this.dtpBeginInDate.Size = new System.Drawing.Size(121, 21);
            this.dtpBeginInDate.TabIndex = 8;
            this.dtpBeginInDate.Value = new System.DateTime(2009, 3, 9, 0, 0, 0, 0);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(265, 92);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(185, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Location = new System.Drawing.Point(13, 148);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(556, 179);
            this.panel2.TabIndex = 1;
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
            this.dataGridView1.Size = new System.Drawing.Size(556, 154);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddAss,
            this.btnModifyAss,
            this.btnDetailAss,
            this.btnAddCard,
            this.btnModifyCardPwd,
            this.btnInitCardPwd});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(556, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddAss
            // 
            this.btnAddAss.Image = ((System.Drawing.Image)(resources.GetObject("btnAddAss.Image")));
            this.btnAddAss.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddAss.Name = "btnAddAss";
            this.btnAddAss.Size = new System.Drawing.Size(73, 22);
            this.btnAddAss.Text = "添加会员";
            this.btnAddAss.Click += new System.EventHandler(this.btnAddAss_Click);
            // 
            // btnModifyAss
            // 
            this.btnModifyAss.Image = ((System.Drawing.Image)(resources.GetObject("btnModifyAss.Image")));
            this.btnModifyAss.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModifyAss.Name = "btnModifyAss";
            this.btnModifyAss.Size = new System.Drawing.Size(73, 22);
            this.btnModifyAss.Text = "修改会员";
            this.btnModifyAss.Click += new System.EventHandler(this.btnModifyAss_Click);
            // 
            // btnDetailAss
            // 
            this.btnDetailAss.Image = ((System.Drawing.Image)(resources.GetObject("btnDetailAss.Image")));
            this.btnDetailAss.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDetailAss.Name = "btnDetailAss";
            this.btnDetailAss.Size = new System.Drawing.Size(73, 22);
            this.btnDetailAss.Text = "会员资料";
            this.btnDetailAss.Click += new System.EventHandler(this.btnDetailAss_Click);
            // 
            // btnAddCard
            // 
            this.btnAddCard.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCard.Image")));
            this.btnAddCard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddCard.Name = "btnAddCard";
            this.btnAddCard.Size = new System.Drawing.Size(85, 22);
            this.btnAddCard.Text = "会员卡发卡";
            this.btnAddCard.ToolTipText = "会员卡发卡：操作会员状态为【未发卡】的会员";
            this.btnAddCard.Click += new System.EventHandler(this.btnAddCard_Click);
            // 
            // btnModifyCardPwd
            // 
            this.btnModifyCardPwd.Image = ((System.Drawing.Image)(resources.GetObject("btnModifyCardPwd.Image")));
            this.btnModifyCardPwd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModifyCardPwd.Name = "btnModifyCardPwd";
            this.btnModifyCardPwd.Size = new System.Drawing.Size(85, 22);
            this.btnModifyCardPwd.Text = "卡密码修改";
            this.btnModifyCardPwd.Click += new System.EventHandler(this.btnModifyCardPwd_Click);
            // 
            // btnInitCardPwd
            // 
            this.btnInitCardPwd.Image = ((System.Drawing.Image)(resources.GetObject("btnInitCardPwd.Image")));
            this.btnInitCardPwd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInitCardPwd.Name = "btnInitCardPwd";
            this.btnInitCardPwd.Size = new System.Drawing.Size(85, 22);
            this.btnInitCardPwd.Text = "卡密码重置";
            this.btnInitCardPwd.Click += new System.EventHandler(this.btnInitCardPwd_Click);
            // 
            // frmAssociator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 339);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmAssociator";
            this.Text = "会员资料设置";
            this.Load += new System.EventHandler(this.frmAssociator_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DateTimePicker dtpBeginInDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbAssLevel;
        private System.Windows.Forms.ComboBox cmbAssState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DateTimePicker dtpBeginOperDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddAss;
        private System.Windows.Forms.ToolStripButton btnModifyAss;
        private System.Windows.Forms.ToolStripButton btnDetailAss;
        private System.Windows.Forms.ToolStripButton btnAddCard;
        private System.Windows.Forms.ToolStripButton btnModifyCardPwd;
        private System.Windows.Forms.ComboBox cmbOper;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpEndInDate;
        private System.Windows.Forms.DateTimePicker dtpEndOperDate;
        private System.Windows.Forms.CheckBox chkEndInAssDate;
        private System.Windows.Forms.CheckBox chkBeginInAssDate;
        private System.Windows.Forms.CheckBox chkEndOperDate;
        private System.Windows.Forms.CheckBox chkBeginOperDate;
        private System.Windows.Forms.ToolStripButton btnInitCardPwd;


    }
}