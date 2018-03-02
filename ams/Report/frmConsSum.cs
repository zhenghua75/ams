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
    public partial class frmConsSum : frmBase
    {
        public frmConsSum()
        {
            InitializeComponent();
        }

        private void frmConsSum_Load(object sender, EventArgs e)
        {
            dtpBegin.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;

            cmbQueryType.Items.Add("按班次");
            cmbQueryType.Items.Add("按操作员");
            cmbQueryType.Items.Add("按消费项目");
            cmbQueryType.SelectedIndex = 0;

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
                DateTime dtBegin = new DateTime(dtpBegin.Value.Year, dtpBegin.Value.Month, dtpBegin.Value.Day, 0, 0, 0);
                DateTime dtEnd = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59);
                string strType = cmbQueryType.Text.Trim();
                using (AMSEntities amsContext = new AMSEntities())
                {
                    switch (strType)
                    {
                        case "按班次":
                            var queryClass = from a in amsContext.tbConsumption where a.dtConsDate >= dtBegin && a.dtConsDate <= dtEnd group a.nConsCharge2 by new { a.vcClass, a.vcGoodsCode } into b orderby b.Key.vcClass, b.Key.vcGoodsCode select new { Class = b.Key.vcClass, GoodsName = b.Key.vcGoodsCode, SumFee = b.Sum() };
                            dgvResult.DataSource = queryClass;
                            dgvResult.Columns["Class"].HeaderText = "班次";
                            dgvResult.Columns["GoodsName"].HeaderText = "消费项目";
                            dgvResult.Columns["SumFee"].HeaderText = "消费总金额";
                            break;
                        case "按操作员":
                            var queryOper = from a in amsContext.tbConsumption where a.dtConsDate >= dtBegin && a.dtConsDate <= dtEnd group a.nConsCharge2 by new { a.vcOperID, a.vcGoodsCode } into b orderby b.Key.vcOperID, b.Key.vcGoodsCode select new { Oper = b.Key.vcOperID, GoodsName = b.Key.vcGoodsCode, SumFee = b.Sum() };
                            dgvResult.DataSource = queryOper;
                            dgvResult.Columns["Oper"].HeaderText = "操作员";
                            dgvResult.Columns["GoodsName"].HeaderText = "消费项目";
                            dgvResult.Columns["SumFee"].HeaderText = "消费总金额";
                            break;
                        case "按消费项目":
                            var queryConsName = from a in amsContext.tbConsumption where a.dtConsDate >= dtBegin && a.dtConsDate <= dtEnd group a.nConsCharge2 by a.vcGoodsCode into b orderby b.Key select new { GoodsName = b.Key, SumFee = b.Sum() };
                            dgvResult.DataSource = queryConsName;
                            dgvResult.Columns["GoodsName"].HeaderText = "消费项目";
                            dgvResult.Columns["SumFee"].HeaderText = "消费总金额";
                            break;
                    }
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
                            if (dgvResult.Columns[i].HeaderText == "消费总金额" )
                            {
                                table += this.replace(dgvResult.Columns[i].HeaderText) + " " + "double,";
                            }
                            else
                            {
                                table += this.replace(dgvResult.Columns[i].HeaderText) + " " + "varchar,";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("表格没有数据可以导出!", "系统提示", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                this.ExportToExcel(table, "会员消费分类统计表");
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
