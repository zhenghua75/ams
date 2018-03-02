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
    public partial class frmBusiLogQuery : frmBase
    {
        public frmBusiLogQuery()
        {
            InitializeComponent();
        }

        private void frmBusiLogQuery_Load(object sender, EventArgs e)
        {
            dtpBegin.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;

            tbOper operall = new tbOper();
            operall.vcOperID = "全部";
            operall.vcOperName = "全部";
            List<tbOper> operlist = GlobalParams.Opers;
            operlist.Insert(0, operall);
            cmbOper.DataSource = operlist;
            cmbOper.DisplayMember = "vcOperName";
            cmbOper.ValueMember = "vcOperID";

            dgvResult.AllowUserToAddRows = false;
            dgvResult.AllowUserToDeleteRows = false;
            dgvResult.AllowUserToResizeRows = true;
            dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvResult.ReadOnly = true;
            dgvResult.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string strOper = cmbOper.SelectedValue.ToString();
                DateTime dtBegin = new DateTime(dtpBegin.Value.Year, dtpBegin.Value.Month, dtpBegin.Value.Day, 0, 0, 0);
                DateTime dtEnd = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59);
                using (AMSEntities amsContext = new AMSEntities())
                {
                    var queryresult = from a in amsContext.tbBusiLog join b in amsContext.tbCommCode on a.vcOperType equals b.vcCommCode where b.vcCommSign == "OT" && a.dtOperDate >= dtBegin && a.dtOperDate <= dtEnd select new { a.iSerial, a.vcAssName, a.vcAssCardID, a.vcLinkSerial, b.vcCommName, a.vcOperID,a.vcOperName, a.dtOperDate };
                    if (strOper != "全部")
                        queryresult = queryresult.Where(a => a.vcOperID == strOper);
                    queryresult = queryresult.OrderBy(a => a.iSerial);

                    dgvResult.DataSource = queryresult;
                    dgvResult.Columns["iSerial"].HeaderText = "操作流水";
                    dgvResult.Columns["vcAssName"].HeaderText = "会员姓名";
                    dgvResult.Columns["vcAssCardID"].HeaderText = "会员卡号";
                    dgvResult.Columns["vcLinkSerial"].HeaderText = "相关流水";
                    dgvResult.Columns["vcCommName"].HeaderText = "操作类型";
                    dgvResult.Columns["vcOperID"].HeaderText = "操作员ID";
                    dgvResult.Columns["vcOperName"].HeaderText = "操作员";
                    dgvResult.Columns["dtOperDate"].HeaderText = "操作日期";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "查询异常");
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string table = "";
                if (dgvResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvResult.Columns.Count; i++)
                    {
                        if (dgvResult.Columns.Count > 0)
                        {
                            table += this.replace(dgvResult.Columns[i].HeaderText) + " " + "varchar,";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("表格没有数据可以导出!", "系统提示", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                this.ExportToExcel(table, "操作员营业日志统计表");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "导出异常");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
