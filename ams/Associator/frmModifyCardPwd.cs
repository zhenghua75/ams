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
    public partial class frmModifyCardPwd : Form
    {
        private tbAssociator ass;
        private tbAssociatorCard assCard;

        public frmModifyCardPwd()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
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
                        if (string.IsNullOrEmpty(txtAssCardID.Text))
                            throw new Exception("请首先读取卡号");
                        if (string.IsNullOrEmpty(txtOldPwd.Text))
                            throw new Exception("请输入原密码");
                        if (string.IsNullOrEmpty(txtNewPwd.Text))
                            throw new Exception("请输入新密码");
                        if (string.IsNullOrEmpty(txtNewPwdConfirm.Text))
                            throw new Exception("请输入新密码确认");
                        if (!txtNewPwd.Text.Equals(txtNewPwdConfirm.Text))
                            throw new Exception("新密码和新密码确认不一致");
                        if (txtOldPwd.Text.Equals(txtNewPwd.Text))
                            throw new Exception("原密码和新密码一直！");

                        var assCardm = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.vcAssCardID == assCard.vcAssCardID && ac.iAssID==assCard.iAssID);
                        if (assCardm == null)
                            throw new Exception("无此会员卡！");
                        if (assCardm.cCardState != ConstApp.CST_1)
                            throw new Exception("此会员卡未在正常使用状态，无法修改密码！");
                        if (!txtOldPwd.Text.Equals(assCardm.vcAssPwd))
                            throw new Exception("原密码不正确");                       

                        assCardm.vcAssPwd = txtNewPwd.Text;
                        assCardm.vcOperID = GlobalParams.oper.vcOperID;

                        tbBusiLog busiLog = new tbBusiLog();
                        busiLog.dtOperDate = DateTime.Now;
                        busiLog.iAssID = assCard.iAssID;
                        busiLog.vcAssCardID = assCard.vcAssCardID;
                        busiLog.vcAssName = ass.vcAssName;
                        busiLog.vcOperName = GlobalParams.oper.vcOperName;
                        busiLog.vcOperID = GlobalParams.oper.vcOperID;
                        busiLog.vcOperType = "OT010";
                        amsContext.AddTotbBusiLog(busiLog);

                        Helper.Save(amsContext);
                        trans.Commit();
                    }
                    Helper.ShowInfo(this,"修改密码成功");
                }
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

        private void btnReadCard_Click(object sender, EventArgs e)
        {
            int ret = 0;
            try
            {
                //读取会员卡卡号
                //开始卡操作----------------------------------
                string strCardNo = "";
                ret = Helper.ReadCard(ref strCardNo);
                if (ret != 0)
                    throw new Exception("卡操作异常");
                txtAssCardID.Text = strCardNo;

                //txtAssCardID.Text = "00105";

                if (string.IsNullOrEmpty(txtAssCardID.Text))
                    throw new Exception("请首先读取会员卡卡号");
                    
                using (AMSEntities amsContext = new AMSEntities())
                {
                    //数据操作
                    assCard = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.vcAssCardID == txtAssCardID.Text);
                    if (assCard == null)
                        throw new Exception("无此会员卡");
                    if (assCard.cCardState != "1")
                        throw new Exception("此会员不在正常使用状态，不能修改密码！");
                    ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == assCard.iAssID);
                    if (ass == null)
                        throw new Exception("此会员卡无会员在使用");
                }
                //其它操作
                btnOK.Enabled = true;
                btnDetail.Enabled = true;            
            }
            catch (Exception ex)
            {
                //异常操作        
                ErrorLog.Write(this, ex);
                Helper.ShowError(this, Helper.PutCardError(ret));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (ass == null || assCard == null)
                    throw new Exception("请首先读取会员卡");
                frmAss frmass = new frmAss();
                frmass.ass = ass;
                frmass.OperType = "DETAIL";
                frmass.MinimizeBox = false;
                frmass.MaximizeBox = false;
                frmass.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }

        private void frmModifyCardPwd_Load(object sender, EventArgs e)
        {
            txtAssCardID.Enabled = false;
            btnOK.Enabled = false;
            btnDetail.Enabled = false;
        }
    }
}
