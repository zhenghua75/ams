using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;
using System.Data.Common;
using System.Data.Linq;
using ams.Common;

namespace ams.Associator
{
    public partial class frmFillQuery : frmBase
    {
        public frmFillQuery()
        {
            InitializeComponent();
        }

        private void frmFillQuery_Load(object sender, EventArgs e)
        {
            dtpBegin.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
            txtSumCount.Text = "0";
            txtSumFee.Text = "0";
            txtSumProm.Text = "0";

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
                string strCardID = txtCardID.Text.Trim();
                string strAssName = txtAssName.Text.Trim();
                long BillNo = 0;
                if (txtBillNo.Text.Trim() != "")
                    BillNo = long.Parse(txtBillNo.Text.Trim());
                DateTime dtBegin = new DateTime(dtpBegin.Value.Year, dtpBegin.Value.Month, dtpBegin.Value.Day, 0, 0, 0);
                DateTime dtEnd = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59);
                using (AMSEntities amsContext = new AMSEntities())
                {
                    //原查询方法：采用多表联合查询。
                    // var queryresult = from a in amsContext.tbFillFee join b in amsContext.tbAssociator on a.iAssID equals b.iAssID 
                    //                  join c in (from bill in amsContext.tbBillInvoice where bill.vcLinkSerial!=null && bill.vcEffFlag=="1"  select new { bill.iBillNo,bill.vcLinkSerial }) on a.iFillSerial equals c.vcLinkSerial
                    //                  where (a.vcOperType == "OT001" || a.vcOperType == "OT002") && a.dtFillDate >= dtBegin && a.dtFillDate <= dtEnd 
                    //                  select new { c.iBillNo, a.iFillSerial, a.vcAssCardID, b.vcAssName, a.nLastBalance, a.nFillFee, a.nPromFee, a.nBalance, a.dtFillDate, a.vcOperID, a.vcComments };
                    //if(strCardID!="")
                    //    queryresult=queryresult.Where(c=>c.vcAssCardID==strCardID);
                    //if(strAssName!="")
                    //    queryresult=queryresult.Where(c=>c.vcAssName.StartsWith(strAssName) || c.vcAssName.EndsWith(strAssName));
                    //if (BillNo > 0)
                    //    queryresult = queryresult.Where(c => c.iBillNo == BillNo);
                    
                    //新查询方法，新建一个查询视图，并将视图映射为实体，查询时只直接查询实体就行

                    var queryresult = from a in amsContext.vwAssFillRecord where a.dtFillDate >= dtBegin && a.dtFillDate <= dtEnd select a;
                    if (strCardID != "")
                        queryresult = queryresult.Where(a => a.vcAssCardID == strCardID);
                    if (strAssName != "")
                        queryresult = queryresult.Where(a => a.vcAssName.StartsWith(strAssName) || a.vcAssName.EndsWith(strAssName));
                    if (BillNo > 0)
                        queryresult = queryresult.Where(a => a.iBillNo == BillNo);

                    queryresult = queryresult.OrderBy(a => a.iBillNo);

                    decimal deSumFee = 0;
                    decimal deSumProm = 0;
                    foreach(var dr in queryresult)
                    {
                        deSumFee += (decimal)dr.nFillFee;
                        deSumProm += (decimal)dr.nPromFee;
                    }

                    dgvResult.DataSource = queryresult;
                    dgvResult.Columns["iBillNo"].HeaderText = "帐单号";
                    dgvResult.Columns["iFillSerial"].HeaderText = "充值流水";
                    dgvResult.Columns["vcAssCardID"].HeaderText = "会员卡号";
                    dgvResult.Columns["vcAssName"].HeaderText = "会员名称";
                    dgvResult.Columns["nLastBalance"].HeaderText = "充值前余额";
                    dgvResult.Columns["nFillFee"].HeaderText = "充值金额";
                    dgvResult.Columns["nPromFee"].HeaderText = "赠款金额";
                    dgvResult.Columns["nBalance"].HeaderText = "充值后余额";
                    dgvResult.Columns["dtFillDate"].HeaderText = "充值时间";
                    dgvResult.Columns["vcOperID"].HeaderText = "操作员";
                    dgvResult.Columns["vcComments"].HeaderText = "备注";

                    txtSumCount.Text = dgvResult.Rows.Count.ToString();
                    txtSumFee.Text = deSumFee.ToString();
                    txtSumProm.Text = deSumProm.ToString();
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
                            if (dgvResult.Columns[i].HeaderText == "充值前余额" || dgvResult.Columns[i].HeaderText == "充值金额" || dgvResult.Columns[i].HeaderText == "赠款金额" || dgvResult.Columns[i].HeaderText == "充值后余额")
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
                this.ExportToExcel(table,"会员充值统计表");
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
