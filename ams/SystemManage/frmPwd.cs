using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ams.Common;
namespace ams.Associator
{
    public partial class frmPwd : Form
    {
        public frmPwd()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOldPwd.Text.Trim().Length == 0)
                    throw new Exception("请输入老密码");
                if (txtPwd.Text.Trim().Length == 0)
                    throw new Exception("请输入新密码");
                if (txtPwdConfirm.Text.Trim().Length == 0)
                    throw new Exception("请输入新密码确认");
                if (!txtOldPwd.Text.Equals(GlobalParams.oper.vcOperPwd))
                    throw new Exception("输入的老密码不正确");
                if (!txtPwd.Text.Equals(txtPwdConfirm.Text))
                    throw new Exception("输入的新密码和新密码确认不一致");
                if (txtOldPwd.Text.Equals(txtPwd.Text))
                    throw new Exception("输入的新密码和老密码一样");
                using (AMSEntities amsContext = new AMSEntities())
                {
                    //tbOper oper = GlobalParams.oper;
                    GlobalParams.oper.vcOperPwd = txtPwd.Text;
                    tbOper oper = amsContext.tbOper.FirstOrDefault(o => o.vcOperID == GlobalParams.oper.vcOperID);
                    oper.vcOperPwd = txtPwd.Text;
                    amsContext.SaveChanges();
                    MessageBox.Show(this, "密码修改成功", "修改密码");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "修改密码");
            }
        }
    }
}
