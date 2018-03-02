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
    public partial class frmAddRole : Form
    {
        public frmAddRole()
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
                if (txtLimitCode.Text == "" || txtLimitName.Text == "")
                    throw new Exception("角色代码和名称都不能为空");
                if (!txtLimitCode.Text.StartsWith("OP"))
                    throw new Exception("角色代码需以OP开头");

                using (AMSEntities amsContext = new AMSEntities())
                {

                    int i = amsContext.tbLimit.Count(l => l.vcLimitCode == txtLimitCode.Text || l.vcLimitName == txtLimitName.Text);
                    if (i > 0)
                        throw new Exception("相同角色代码或名称的角色已存在");
                    tbLimit limit = new tbLimit();
                    limit.vcLimitCode = txtLimitCode.Text;
                    limit.vcLimitName = txtLimitName.Text;
                    amsContext.AddTotbLimit(limit);
                    amsContext.SaveChanges();
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
    }
}
