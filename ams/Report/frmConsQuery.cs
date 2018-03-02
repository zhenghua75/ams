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
    public partial class frmConsQuery : frmBase
    {
        public frmConsQuery()
        {
            InitializeComponent();
        }

        private void frmConsQuery_Load(object sender, EventArgs e)
        {
            dtpBegin.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
            txtSumCount.Text = "0";
            txtSumFee.Text = "0";

            cmbClass.Items.Add("全部");
            cmbClass.Items.Add("早班");
            cmbClass.Items.Add("中班");
            cmbClass.Items.Add("晚班");
            cmbClass.SelectedIndex = 0;

            tbOper operall = new tbOper();
            operall.vcOperID = "全部";
            operall.vcOperName = "全部";
            List<tbOper> operlist = GlobalParams.Opers;
            operlist.Insert(0, operall);
            cmbOper.DataSource = operlist;
            cmbOper.DisplayMember = "vcOperName";
            cmbOper.ValueMember = "vcOperID";

            tbGoods goodsall = new tbGoods();
            goodsall.vcGoodsCode = "全部";
            goodsall.vcGoodsName = "全部";
            List<tbGoods> goodslist = GlobalParams.Goods;
            goodslist.Insert(0, goodsall);
            cmbConsName.DataSource = GlobalParams.Goods;
            cmbConsName.DisplayMember = "vcGoodsName";
            cmbConsName.ValueMember = "vcGoodsName";

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
                string strClass = cmbClass.Text.Trim();
                string strOper = cmbOper.SelectedValue.ToString();
                string strGoods = cmbConsName.Text.Trim();
                DateTime dtBegin = new DateTime(dtpBegin.Value.Year, dtpBegin.Value.Month, dtpBegin.Value.Day, 0, 0, 0);
                DateTime dtEnd = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59);
                using (AMSEntities amsContext = new AMSEntities())
                {
                    var queryresult = from a in amsContext.vwAssConsRecord where a.dtConsDate >= dtBegin && a.dtConsDate <= dtEnd select a;
                    if (strCardID != "")
                        queryresult = queryresult.Where(a => a.vcAssCardID == strCardID);
                    if (strAssName != "")
                        queryresult = queryresult.Where(a => a.vcAssName.StartsWith(strAssName) || a.vcAssName.EndsWith(strAssName));
                    if (BillNo > 0)
                        queryresult = queryresult.Where(a => a.iBillNo == BillNo);
                    if (strClass != "全部")
                        queryresult = queryresult.Where(a => a.vcClass == strClass);
                    if (strOper != "全部")
                        queryresult = queryresult.Where(a => a.vcOperID == strOper);
                    if (strGoods != "全部")
                        queryresult = queryresult.Where(a => a.vcGoodsCode == strGoods);

                    queryresult = queryresult.OrderBy(a => a.vcAssCardID).ThenBy(a => a.iConsSerial).ThenBy(a => a.iNO);

                    decimal deSumFee = 0;
                    foreach (var dr in queryresult)
                    {
                        deSumFee += (decimal)dr.nConsCharge2;
                    }

                    foreach (var dr in queryresult)
                    {
                        tbOper oper1 = GlobalParams.Opers.FirstOrDefault(o=>o.vcOperID==dr.vcOperID);
                        if(oper1!=null)
                            dr.vcOperID = oper1.vcOperName;
                    }

                    dgvResult.DataSource = queryresult;
                    dgvResult.Columns["iBillNo"].HeaderText = "帐单号";
                    dgvResult.Columns["iConsSerial"].HeaderText = "消费流水";
                    dgvResult.Columns["iNO"].HeaderText = "序号";
                    dgvResult.Columns["vcAssCardID"].HeaderText = "会员卡号";
                    dgvResult.Columns["vcAssName"].HeaderText = "会员姓名";
                    dgvResult.Columns["vcGoodsCode"].HeaderText = "消费项目";
                    dgvResult.Columns["nGoodsPrice"].HeaderText = "单价";
                    dgvResult.Columns["nLastBalance"].HeaderText = "消费前余额";
                    dgvResult.Columns["nConsCharge1"].HeaderText = "消费应收金额";
                    dgvResult.Columns["nConsRate"].HeaderText = "折扣";
                    dgvResult.Columns["nConsCharge2"].HeaderText = "消费实收金额";
                    dgvResult.Columns["nBalance"].HeaderText = "消费后余额";
                    dgvResult.Columns["dtConsDate"].HeaderText = "消费日期";
                    dgvResult.Columns["dtOperDate"].HeaderText = "操作日期";
                    dgvResult.Columns["vcFlag"].HeaderText = "消费记录状态";
                    dgvResult.Columns["iLink"].HeaderText = "相关返销流水";
                    dgvResult.Columns["vcOperID"].HeaderText = "操作员";
                    dgvResult.Columns["vcComments"].HeaderText = "备注ID";
                    dgvResult.Columns["vcClass"].HeaderText = "班次";

                    txtSumCount.Text = dgvResult.Rows.Count.ToString();
                    txtSumFee.Text = deSumFee.ToString();
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
                            if (dgvResult.Columns[i].HeaderText == "消费前余额" || dgvResult.Columns[i].HeaderText == "消费应收金额" || dgvResult.Columns[i].HeaderText == "折扣" || dgvResult.Columns[i].HeaderText == "消费实收金额" || dgvResult.Columns[i].HeaderText == "消费后余额")
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
                this.ExportToExcel(table, "会员消费统计表");
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
