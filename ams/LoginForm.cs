using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using ams.Common;

namespace ams
{
    public partial class LoginForm : Form
    {        
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string strOperID = cmbOper.SelectedValue.ToString();
                string strOperPwd = txtOperPwd.Text;
                if (string.IsNullOrEmpty(strOperPwd))
                    throw new Exception("请输入密码");
                tbOper oper = GlobalParams.Opers.FirstOrDefault(o => o.vcOperID == strOperID && o.vcOperPwd == strOperPwd && o.iFlag==0);
                if (oper == null)
                    throw new Exception("用户名或密码错误");
                GlobalParams.oper = oper;
                GlobalParams.strClass = cmbClass.Text.Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                txtOperPwd.Text = "";
                ErrorLog.Write(this, ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {

                GlobalParams.strClass = cmbClass.Text;
                GlobalParams.strInputBoxMes = "";

                cmbOper.DataSource = GlobalParams.Opers.Where(o=>o.iFlag==0).ToList();
                cmbOper.DisplayMember = "vcOperName";
                cmbOper.ValueMember = "vcOperID";

                cmbClass.DataSource = GlobalParams.strClasses;

                btnOK.Enabled = true;
                txtOperPwd.Focus();
            }
            catch (Exception ex)
            {
                btnOK.Enabled = false;
                ErrorLog.Write(this, ex);
            }

        }
    }
}
