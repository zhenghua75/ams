using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ams.Common;
namespace ams.Associator
{
    public partial class frmAss : Form
    {
        public string OperType { get; set; }
        public tbAssociator ass { get; set; }
        public frmAss()
        {
            InitializeComponent();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitAss()
        {
            //性别
            cmbAssSex.DataSource = GlobalParams.strSexes;
            //会员级别           
            cmbAssLevel.DataSource = GlobalParams.CommCode.Where(cc => cc.vcCommSign == "AL").ToList();
            cmbAssLevel.DisplayMember = "vcCommName";
            cmbAssLevel.ValueMember = "vcCommCode";
            if (OperType != "ADD")
            {
                //tbAssociator ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == AssID);
                if (ass != null)
                    SetAssociator(ass);
                else
                    throw new Exception("未找到会员");
            }

            this.dtpInDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));            
        }

        private void frmAss_Load(object sender, EventArgs e)
        {
            try
            {
               
                switch (OperType)
                {
                    case "ADD":
                        btnAdd.Visible = true;
                        btnAdd.Text = "添加";
                        break;
                    case "MODIFY":
                        btnAdd.Visible = true;
                        btnAdd.Text = "修改";
                        break;
                    case "DETAIL":
                        btnAdd.Visible = false;
                        btnAdd.Text = "细节";
                        break;
                }
                InitAss();
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #region 获取或设置界面值
        private tbAssociator GetAssociator()
        {
            tbAssociator ass = new tbAssociator();
            ass.cAssSex = cmbAssSex.Text;
            ass.dtInAssDate = dtpInDate.Value;
            ass.dtOperDate = DateTime.Now;
            ass.iAssAge = Convert.ToInt32(txtAssAge.Value);
            ass.vcAssLevel = cmbAssLevel.SelectedValue.ToString();
            ass.vcAssName = txtAssName.Text;
            ass.vcAssNbr = txtAssNbr.Text;
            ass.vcAssState = ConstApp.AST_0;//"1";
            ass.vcAssType = "001";
            ass.vcComAddress = txtComAddress.Text;
            ass.vcComFax = txtFax.Text;
            ass.vcComments = txtComments.Text;
            ass.vcCompanyName = txtCompanyName.Text;
            ass.vcComPhone = txtComPhone.Text;
            ass.vcComPostID = txtComPostID.Text;
            ass.vcEmail = txtEmail.Text;
            ass.vcLinkAddress = txtLinkAddress.Text;
            ass.vcLinkPhone = txtLinkPhone.Text;
            ass.vcMobile = txtMobile.Text;
            ass.vcOperID = GlobalParams.oper.vcOperID;
            ass.vcPassport = txtPassport.Text;
            ass.vcPostID = txtPostID.Text;
            return ass;
        }

        private tbAssociator GetAssociator(tbAssociator ass)
        {
            //tbAssociator ass = new tbAssociator();
            ass.cAssSex = cmbAssSex.Text;
            ass.dtInAssDate = dtpInDate.Value;
            ass.dtOperDate = DateTime.Now;
            ass.iAssAge = Convert.ToInt32(txtAssAge.Value);
            ass.vcAssLevel = cmbAssLevel.SelectedValue.ToString();
            ass.vcAssName = txtAssName.Text;
            ass.vcAssNbr = txtAssNbr.Text;
            ass.vcAssState = ConstApp.AST_0;//"1";
            ass.vcAssType = "001";
            ass.vcComAddress = txtComAddress.Text;
            ass.vcComFax = txtFax.Text;
            ass.vcComments = txtComments.Text;
            ass.vcCompanyName = txtCompanyName.Text;
            ass.vcComPhone = txtComPhone.Text;
            ass.vcComPostID = txtComPostID.Text;
            ass.vcEmail = txtEmail.Text;
            ass.vcLinkAddress = txtLinkAddress.Text;
            ass.vcLinkPhone = txtLinkPhone.Text;
            ass.vcMobile = txtMobile.Text;
            ass.vcOperID = GlobalParams.oper.vcOperID;
            ass.vcPassport = txtPassport.Text;
            ass.vcPostID = txtPostID.Text;
            return ass;
        }

        private void SetAssociator(tbAssociator ass)
        {
            cmbAssSex.Text = ass.cAssSex;
            dtpInDate.Value = Convert.ToDateTime(ass.dtInAssDate);
            txtAssAge.Value = Convert.ToDecimal(ass.iAssAge);
            cmbAssLevel.SelectedValue = ass.vcAssLevel;
            txtAssName.Text = ass.vcAssName;
            txtAssNbr.Text = ass.vcAssNbr;
            txtComAddress.Text = ass.vcComAddress;
            txtFax.Text = ass.vcComFax;
            txtComments.Text = ass.vcComments;
            txtComPhone.Text = ass.vcComPhone;
            txtComPostID.Text = ass.vcComPostID;
            txtEmail.Text = ass.vcEmail;
            txtLinkAddress.Text = ass.vcLinkAddress;
            txtLinkPhone.Text = ass.vcLinkPhone;
            txtMobile.Text = ass.vcMobile;
            txtPassport.Text = ass.vcPassport;
            txtPostID.Text = ass.vcPostID;

            txtCompanyName.Text = ass.vcCompanyName;
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                string strText = "";
                if (string.IsNullOrEmpty(txtAssName.Text))
                    throw new Exception("请输入会员姓名");
                using (AMSEntities amsContext = new AMSEntities())
                {
                    conn = amsContext.Connection;
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }                    
                    using (trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        //事务数据操作或其它一致性操作
                        tbBusiLog busiLog = new tbBusiLog();
                        busiLog.dtOperDate = DateTime.Now;                        
                        busiLog.vcOperName = GlobalParams.oper.vcOperName;
                        busiLog.vcOperID = GlobalParams.oper.vcOperID;

                        switch (OperType)
                        {
                            case "ADD":
                                tbAssociator assn = GetAssociator();
                                amsContext.AddTotbAssociator(assn);
                                busiLog.iAssID = assn.iAssID;
                                busiLog.vcAssName = assn.vcAssName;
                                busiLog.vcOperType = "OT007";
                                amsContext.AddTotbBusiLog(busiLog);

                                Helper.Save(amsContext);
                                strText = "会员添加成功";
                                break;
                            case "MODIFY":
                                tbAssociator assm = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == ass.iAssID);
                                if (assm == null)
                                    throw new Exception("未找到会员");
                                assm = GetAssociator(assm);
                                busiLog.iAssID = assm.iAssID;
                                busiLog.vcAssName = assm.vcAssName;
                                busiLog.vcOperType = "OT008";
                                amsContext.AddTotbBusiLog(busiLog);


                                Helper.Save(amsContext);
                                strText="会员修改成功";
                                break;
                        }
                        trans.Commit();
                    }
                }
                Helper.ShowInfo(this, strText);
            }
            catch (Exception ex)
            {
                //异常操作
                ErrorLog.Write(this, ex);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
    }
}
