using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ams.Common;
using System.Data.Objects;
namespace ams.Associator
{
    public partial class frmOper : Form
    {
        public frmOper()
        {
            InitializeComponent();
        }

        private void frmOper_Load(object sender, EventArgs e)
        {
            try
            {
                treeView1.BeginUpdate();
                treeView1.Nodes.Clear();
                using (AMSEntities amsContext = new AMSEntities())
                {
                    cmbRole.DataSource = amsContext.tbLimit;
                    cmbRole.DisplayMember = "vcLimitName";
                    cmbRole.ValueMember = "vcLimitCode";
                    
                    foreach (tbLimit l in amsContext.tbLimit)
                    {
                        TreeNode tn = new TreeNode();
                        tn.Name = l.vcLimitCode;
                        tn.Text = l.vcLimitName;
                        tn.Tag = "role";
                        
                        treeView1.Nodes.Add(tn);
                    }                                   
                }
                using (AMSEntities amsContext = new AMSEntities())
                {
                    cmbOperLevel.DataSource = amsContext.tbOperLevel;
                    cmbOperLevel.DisplayMember = "vcLevelName";
                    cmbOperLevel.ValueMember = "vcOperLevel";
                }
                //很奇怪的问题，数据库从2008改为2000后，分开写才成功，
                using (AMSEntities amsContext = new AMSEntities())
                {
                    foreach (TreeNode tn in treeView1.Nodes)
                    {
                        foreach (tbOper o in amsContext.tbOper)
                        {
                            if (o.vcLimit == tn.Name)
                            {
                                TreeNode tn2 = new TreeNode();
                                tn2.Name = o.vcOperID;
                                tn2.Text = o.vcOperName;
                                tn2.Tag = "oper";
                                tn.Nodes.Add(tn2);
                            }
                        }
                    }
                }
                treeView1.EndUpdate();
                treeView1.ExpandAll();     
                txtOperID.Text = "";
                txtOperID.Enabled = true;
                txtOperName.Text = "";
                txtOperPwd.Text = "";
                txtOperPwdConfirm.Text = "";
                btnAdd.Text = "添加";

                btnDelete.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string strType = e.Node.Tag.ToString();
                if (strType == "role")
                {
                    txtOperID.Text = "";
                    txtOperID.Enabled = true;
                    txtOperName.Text = "";
                    txtOperPwd.Text = "";
                    txtOperPwdConfirm.Text = "";
                    btnAdd.Text = "添加";
                    cmbRole.SelectedValue = e.Node.Name;
                    //btnDelete.Visible = false;
                    //btnAdd.Tag = "tag";
                }
                if (strType == "oper")
                {
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        tbOper oper = amsContext.tbOper.FirstOrDefault(o => o.vcOperID == e.Node.Name);
                        txtOperID.Text = oper.vcOperID;
                        txtOperID.Enabled = false;
                        txtOperName.Text = oper.vcOperName;
                        txtOperPwd.Text = oper.vcOperPwd;
                        txtOperPwdConfirm.Text = oper.vcOperPwd;
                        cmbRole.SelectedValue = oper.vcLimit;
                        cmbOperLevel.SelectedValue = oper.vcOperLevel;
                        btnAdd.Text = "修改";
                        btnDelete.Visible = true;
                        //btnDelete.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "选择角色出错");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOperID.Text.Trim().Length == 0)
                    throw new Exception("请输入操作员帐号");
                if (txtOperName.Text.Trim().Length == 0)
                    throw new Exception("请输入操作员名称");
                if (txtOperPwd.Text.Trim().Length == 0)
                    throw new Exception("请输入密码");
                if (txtOperPwdConfirm.Text.Trim().Length == 0)
                    throw new Exception("请输入密码确认");
                if (!txtOperPwd.Text.Equals(txtOperPwdConfirm.Text))
                    throw new Exception("确认密码不一致");
                using (AMSEntities amsContext = new AMSEntities())
                {
                    
                    switch (btnAdd.Text)
                    {
                        case "添加":
                            tbOper eOper = amsContext.tbOper.FirstOrDefault(o => o.vcOperID == txtOperID.Text || o.vcOperName == txtOperName.Text);
                            if (eOper != null)
                                throw new Exception("相同名称或者(帐号的操作员已存在");
                            tbOper nOper = new tbOper();                                                   
                            nOper.vcLimit = cmbRole.SelectedValue.ToString();
                            nOper.vcOperID = txtOperID.Text;
                            nOper.vcOperLevel = cmbOperLevel.SelectedValue.ToString();//"OL001";
                            nOper.vcOperName = txtOperName.Text;
                            nOper.vcOperPwd = txtOperPwd.Text;
                            nOper.iFlag = 0;
                            amsContext.AddTotbOper(nOper);                          
                            amsContext.SaveChanges();
                            MessageBox.Show(this, "操作员添加成功", "添加操作员");
                            break;
                        case "修改":
                            int i = amsContext.tbOper.Count(o => o.vcOperName == txtOperName.Text );
                            if (i > 1)
                                throw new Exception("相同名称的操作员已存在");
                            tbOper oper = amsContext.tbOper.FirstOrDefault(o => o.vcOperID == txtOperID.Text);
                            oper.vcLimit = cmbRole.SelectedValue.ToString();
                            oper.vcOperName = txtOperName.Text;
                            oper.vcOperPwd = txtOperPwd.Text;
                            oper.vcOperLevel = cmbOperLevel.SelectedValue.ToString();
                            amsContext.SaveChanges();
                            MessageBox.Show(this, "操作员修改成功", "修改操作员");
                            break;
                    }
                }
                frmOper_Load(null, null);
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "操作员");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strType = btnAdd.Text;
            switch (strType)
            {
                case "添加":
                    txtOperID.Text = "";
                    txtOperID.Enabled = true;
                    txtOperName.Text = "";
                    txtOperPwd.Text = "";
                    txtOperPwdConfirm.Text = "";
                    break;
                case "修改":
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        tbOper oper = amsContext.tbOper.FirstOrDefault(o => o.vcOperID == txtOperID.Text);
                        //txtOperID.Text = oper.vcOperID;
                        //txtOperID.Enabled = false;
                        txtOperName.Text = oper.vcOperName;
                        txtOperPwd.Text = oper.vcOperPwd;
                        txtOperPwdConfirm.Text = oper.vcOperPwd;
                        cmbRole.SelectedValue = oper.vcLimit;
                    }
                    break;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOperID.Text.Equals("admin"))
                    throw new Exception("系统管理员帐户不能注销");
                DialogResult dr = MessageBox.Show(this, "是否注销\"" + txtOperName.Text + "\"操作员", "注销操作员", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        tbOper oper = amsContext.tbOper.FirstOrDefault(o => o.vcOperID == txtOperID.Text);
                        //oper.vcLimit = cmbRole.SelectedValue.ToString();
                        //oper.vcOperName = txtOperName.Text;
                        //oper.vcOperPwd = txtOperPwd.Text;
                        //oper.vcOperLevel = cmbOperLevel.SelectedValue.ToString();
                        oper.iFlag = 1;
                        oper.dtExpDate = DateTime.Now;
                        amsContext.SaveChanges();
                        MessageBox.Show(this, "操作员注销成功", "注销操作员");

                    }
                    //frmOper_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "操作员");
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
