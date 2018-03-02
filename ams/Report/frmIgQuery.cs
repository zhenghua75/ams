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
    public partial class frmIgQuery : Form
    {
        public frmIgQuery()
        {
            InitializeComponent();
        }

        private void frmIgQuery_Load(object sender, EventArgs e)
        {
            dgvCurIG.AllowUserToAddRows = false;
            dgvCurIG.AllowUserToDeleteRows = false;
            dgvCurIG.AllowUserToResizeRows = true;
            dgvCurIG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCurIG.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvCurIG.ReadOnly = true;
            dgvCurIG.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            dgvIgLog.AllowUserToAddRows = false;
            dgvIgLog.AllowUserToDeleteRows = false;
            dgvIgLog.AllowUserToResizeRows = true;
            dgvIgLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvIgLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvIgLog.ReadOnly = true;
            dgvIgLog.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string strCardID = txtCardID.Text.Trim();
                string strAssName = txtAssName.Text.Trim();
                string strBeginIg = txtBeginIg.Text.Trim();
                string strEndIg = txtEndIg.Text.Trim();
                int beginIg=0;
                int endIg=0;
                if (chbIg.Checked)
                {
                    if (strBeginIg == "" || strEndIg == "")
                    {
                        throw new Exception("请填写正确的积分段值！");
                    }
                    else
                    {
                        beginIg = int.Parse(strBeginIg);
                        endIg = int.Parse(strEndIg);
                    }
                }
                using (AMSEntities amsContext = new AMSEntities())
                {
                    var queryresult = from a in amsContext.tbIntegral join b in amsContext.tbAssociator on a.iAssID equals b.iAssID select new { b.vcAssName, a.vcAssCardID, a.nBalance, a.iIgValue, b.vcLinkPhone };
                    if (strCardID != "")
                        queryresult = queryresult.Where(a => a.vcAssCardID == strCardID);
                    if (strAssName != "")
                        queryresult = queryresult.Where(a => a.vcAssName.StartsWith(strAssName) || a.vcAssName.EndsWith(strAssName));
                    if (chbIg.Checked)
                        queryresult = queryresult.Where(a => a.iIgValue>=beginIg && a.iIgValue<=endIg);

                    queryresult = queryresult.OrderBy(a => a.vcAssCardID);

                    dgvCurIG.DataSource = queryresult;
                    dgvCurIG.Columns["vcAssName"].HeaderText = "会员姓名";
                    dgvCurIG.Columns["vcAssCardID"].HeaderText = "会员卡号";
                    dgvCurIG.Columns["nBalance"].HeaderText = "当前余额";
                    dgvCurIG.Columns["iIgValue"].HeaderText = "当前积分";
                    dgvCurIG.Columns["vcLinkPhone"].HeaderText = "联系电话";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "查询异常");
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCurIG_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    string strCardID = dgvCurIG[1, e.RowIndex].Value.ToString();
                    if (strCardID == "")
                        throw new Exception("获取会员积分日志异常！");
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        var iglog = from a in amsContext.vwAssIgLogRecord where a.vcAssCardID == strCardID select a;

                        iglog = iglog.OrderBy(a => a.dtIgDate);

                        dgvIgLog.DataSource = iglog;
                            dgvIgLog.Columns["iIgSerial"].HeaderText = "积分流水";
                        dgvIgLog.Columns["vcAssName"].HeaderText = "会员姓名";
                        dgvIgLog.Columns["vcAssCardID"].HeaderText = "会员卡号";
                        dgvIgLog.Columns["iIgLast"].HeaderText = "上次积分余额";
                        dgvIgLog.Columns["dtIgDate"].HeaderText = "本次积分日期";
                        dgvIgLog.Columns["vcIgName"].HeaderText = "兑换积分名称";
                        dgvIgLog.Columns["vcCommName"].HeaderText = "积分类型";
                        dgvIgLog.Columns["iIgGet"].HeaderText = "本次获得积分";
                        dgvIgLog.Columns["iIgArrival"].HeaderText = "本次积分余额";
                        dgvIgLog.Columns["iLinkCons"].HeaderText = "相关消费流水";
                        dgvIgLog.Columns["vcOperID"].HeaderText = "操作员";
                        dgvIgLog.Columns["vcComments"].HeaderText = "备注";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "查询异常");
            }
        }

        private void txtBeginIg_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
            {
                if (e.KeyChar == 8)
                {
                    return;
                }
                if (e.KeyChar < 48 || e.KeyChar > 57)
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void txtEndIg_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
            {
                if (e.KeyChar == 8)
                {
                    return;
                }
                if (e.KeyChar < 48 || e.KeyChar > 57)
                {
                    e.Handled = true;
                    return;
                }
            }
        }

    }
}
