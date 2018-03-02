namespace ams.Associator
{
    partial class frmParaSet
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
            this.components = new System.ComponentModel.Container();
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.usParaSet1 = new ams.Associator.usParaSet();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.usParaSet2 = new ams.Associator.usParaSet();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.usParaSet3 = new ams.Associator.usParaSet();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAK = new System.Windows.Forms.NumericUpDown();
            this.txtAgf = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAF = new System.Windows.Forms.CheckBox();
            this.usParaSet4 = new ams.Associator.usParaSet();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.usParaSet5 = new ams.Associator.usParaSet();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.usParaSet6 = new ams.Associator.usParaSet();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgf)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(628, 382);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.usParaSet1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(620, 357);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "消费项设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // usParaSet1
            // 
            this.usParaSet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usParaSet1.Location = new System.Drawing.Point(3, 3);
            this.usParaSet1.MyAMSEntities = null;
            this.usParaSet1.Name = "usParaSet1";
            this.usParaSet1.Size = new System.Drawing.Size(614, 351);
            this.usParaSet1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.usParaSet2);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(620, 357);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "会员级别与消费项相关设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // usParaSet2
            // 
            this.usParaSet2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usParaSet2.Location = new System.Drawing.Point(3, 3);
            this.usParaSet2.MyAMSEntities = null;
            this.usParaSet2.Name = "usParaSet2";
            this.usParaSet2.Size = new System.Drawing.Size(614, 351);
            this.usParaSet2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.usParaSet3);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(620, 357);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "充值优惠参数";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // usParaSet3
            // 
            this.usParaSet3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usParaSet3.Location = new System.Drawing.Point(0, 0);
            this.usParaSet3.MyAMSEntities = null;
            this.usParaSet3.Name = "usParaSet3";
            this.usParaSet3.Size = new System.Drawing.Size(620, 357);
            this.usParaSet3.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Controls.Add(this.usParaSet4);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(620, 357);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "积分兑换项目设置";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtAK);
            this.panel1.Controls.Add(this.txtAgf);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chkAF);
            this.panel1.Location = new System.Drawing.Point(116, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 82);
            this.panel1.TabIndex = 1;
            // 
            // txtAK
            // 
            this.txtAK.Location = new System.Drawing.Point(271, 13);
            this.txtAK.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.txtAK.Name = "txtAK";
            this.txtAK.Size = new System.Drawing.Size(120, 21);
            this.txtAK.TabIndex = 7;
            // 
            // txtAgf
            // 
            this.txtAgf.Location = new System.Drawing.Point(145, 45);
            this.txtAgf.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtAgf.Name = "txtAgf";
            this.txtAgf.Size = new System.Drawing.Size(120, 21);
            this.txtAgf.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(299, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "自动提醒积分分值：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "补发卡工本费金额：";
            // 
            // chkAF
            // 
            this.chkAF.AutoSize = true;
            this.chkAF.Location = new System.Drawing.Point(15, 13);
            this.chkAF.Name = "chkAF";
            this.chkAF.Size = new System.Drawing.Size(120, 16);
            this.chkAF.TabIndex = 0;
            this.chkAF.Text = "是否启用自动更新";
            this.chkAF.UseVisualStyleBackColor = true;
            // 
            // usParaSet4
            // 
            this.usParaSet4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.usParaSet4.Location = new System.Drawing.Point(107, 118);
            this.usParaSet4.MyAMSEntities = null;
            this.usParaSet4.Name = "usParaSet4";
            this.usParaSet4.Size = new System.Drawing.Size(429, 206);
            this.usParaSet4.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.usParaSet5);
            this.tabPage5.Location = new System.Drawing.Point(4, 21);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(620, 357);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "操作员折扣率定义";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // usParaSet5
            // 
            this.usParaSet5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usParaSet5.Location = new System.Drawing.Point(3, 3);
            this.usParaSet5.MyAMSEntities = null;
            this.usParaSet5.Name = "usParaSet5";
            this.usParaSet5.Size = new System.Drawing.Size(614, 351);
            this.usParaSet5.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.usParaSet6);
            this.tabPage6.Location = new System.Drawing.Point(4, 21);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(620, 357);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "会员级别";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // usParaSet6
            // 
            this.usParaSet6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usParaSet6.Location = new System.Drawing.Point(3, 3);
            this.usParaSet6.MyAMSEntities = null;
            this.usParaSet6.Name = "usParaSet6";
            this.usParaSet6.Size = new System.Drawing.Size(614, 351);
            this.usParaSet6.TabIndex = 0;
            // 
            // frmParaSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 382);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmParaSet";
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.frmParaSet_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmParaSet_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgf)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private usParaSet usParaSet1;
        private usParaSet usParaSet2;
        private usParaSet usParaSet3;
        private usParaSet usParaSet4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown txtAgf;
        private System.Windows.Forms.NumericUpDown txtAK;
        private System.Windows.Forms.TabPage tabPage5;
        private usParaSet usParaSet5;
        private System.Windows.Forms.TabPage tabPage6;
        private usParaSet usParaSet6;
    }
}