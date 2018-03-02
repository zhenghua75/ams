using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;
using ams.Common;

namespace ams.Associator
{
    public partial class frmAssInfoQuery : frmBase
    {
        public frmAssInfoQuery()
        {
            InitializeComponent();
        }

        private void frmAssInfoQuery_Load(object sender, EventArgs e)
        {
            txtPTAssCount.Text = "0";
            txtPTSumCharge.Text = "0";
            txtPTSumIG.Text = "0";
            txtJKAssCount.Text = "0";
            txtJKSumCharge.Text = "0";
            txtJKSumIG.Text = "0";
            dtpBegin.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;

            tbCommCode allcomm = new tbCommCode();
            allcomm.vcCommCode = "";
            allcomm.vcCommName = "全部";
            List<tbCommCode> AssLevel = GlobalParams.CommCode.Where(al => al.vcCommSign == "AL").OrderBy(al => al.vcCommCode).ToList();
            AssLevel.Insert(0, allcomm);
            cmbAssLevel.DataSource = AssLevel;
            cmbAssLevel.DisplayMember = "vcCommName";
            cmbAssLevel.ValueMember = "vcCommCode";

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
                string strAssLevel = cmbAssLevel.SelectedValue.ToString();
                DateTime dtbegin = new DateTime(dtpBegin.Value.Year, dtpBegin.Value.Month, dtpBegin.Value.Day, 0, 0, 0);
                DateTime dtend = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59);
                using (AMSEntities amsContext = new AMSEntities())
                {
                    var queryresult = from a in amsContext.vwAssInfoRecord where a.dtOperDate >= dtbegin && a.dtOperDate <= dtend select a;
                    if (strCardID != "")
                        queryresult = queryresult.Where(a => a.vcAssCardID == strCardID);
                    if (strAssName != "")
                        queryresult = queryresult.Where(a => a.vcAssName.StartsWith(strAssName) || a.vcAssName.EndsWith(strAssName));
                    if (strAssLevel!="")
                        queryresult = queryresult.Where(a => a.vcAssLevel == strAssLevel);

                    queryresult = queryresult.OrderBy(a => a.vcAssCardID);
                    
                    int sumPTCount=0;
                    decimal sumPTCharge=0;
                    decimal sumPTIg=0;
                    int sumJKCount=0;
                    decimal sumJKCharge=0;
                    decimal sumJKIg=0;
                    foreach (var dr in queryresult)
                    {
                        tbCommCode asslevel = GlobalParams.CommCode.FirstOrDefault(al => al.vcCommSign == "AL" && al.vcCommCode == dr.vcAssLevel);
                        dr.vcAssLevel = asslevel.vcCommName;
                        if (dr.cCardState.Trim() == "")
                        {
                            dr.cCardState = "";
                        }
                        else
                        {
                            tbCommCode assCardState = GlobalParams.CommCode.FirstOrDefault(al => al.vcCommSign == "CST" && al.vcCommCode == dr.cCardState);
                            dr.cCardState = assCardState.vcCommName;
                        }
                        tbCommCode assPutCardFlag = GlobalParams.CommCode.FirstOrDefault(al => al.vcCommSign == "AST" && al.vcCommCode == dr.vcAssState);
                        dr.vcAssState = assPutCardFlag.vcCommName;

                        if (dr.vcAssLevel == "普通会员")
                        {
                            sumPTCount++;
                            sumPTCharge += (decimal)dr.nBalance;
                            sumPTIg += (decimal)dr.iIgValue;
                        }
                        if (dr.vcAssLevel == "金卡会员")
                        {
                            sumJKCount++;
                            sumJKCharge += (decimal)dr.nBalance;
                            sumJKIg += (decimal)dr.iIgValue;
                        }
                    }

                    dgvResult.DataSource = queryresult;
                    dgvResult.Columns["iAssID"].HeaderText = "会员ID";
                    dgvResult.Columns["vcAssCardID"].HeaderText = "会员卡号";
                    dgvResult.Columns["vcAssName"].HeaderText = "会员名称";
                    dgvResult.Columns["vcAssLevel"].HeaderText = "会员级别";
                    dgvResult.Columns["nBalance"].HeaderText = "卡余额";
                    dgvResult.Columns["iIgValue"].HeaderText = "积分";
                    dgvResult.Columns["FillSum"].HeaderText = "充值总额";
                    dgvResult.Columns["FillCount"].HeaderText = "充值次数";
                    dgvResult.Columns["FillAvg"].HeaderText = "平均充值";
                    dgvResult.Columns["ConsSum"].HeaderText = "消费总额";
                    dgvResult.Columns["ConsCount"].HeaderText = "消费次数";
                    dgvResult.Columns["ConsAvg"].HeaderText = "平均消费";
                    dgvResult.Columns["FillProm"].HeaderText = "赠金总额";
                    dgvResult.Columns["cAssSex"].HeaderText = "性别";
                    dgvResult.Columns["vcMobile"].HeaderText = "手机号码";
                    dgvResult.Columns["iAssAge"].HeaderText = "年龄";
                    dgvResult.Columns["vcAssNbr"].HeaderText = "身份证号";
                    dgvResult.Columns["vcPassPort"].HeaderText = "护照号码";
                    dgvResult.Columns["vcLinkAddress"].HeaderText = "家庭联系地址";
                    dgvResult.Columns["vcLinkPhone"].HeaderText = "家庭联系电话";
                    dgvResult.Columns["vcPostID"].HeaderText = "住址邮编";
                    dgvResult.Columns["vcEmail"].HeaderText = "Email";
                    dgvResult.Columns["vcCompanyName"].HeaderText = "公司名称";
                    dgvResult.Columns["vcComPhone"].HeaderText = "公司电话";
                    dgvResult.Columns["vcComFax"].HeaderText = "公司传真";
                    dgvResult.Columns["vcComAddress"].HeaderText = "公司地址";
                    dgvResult.Columns["vcComPostID"].HeaderText = "公司邮编";
                    dgvResult.Columns["dtCardPutDate"].HeaderText = "发卡日期";
                    dgvResult.Columns["cCardState"].HeaderText = "卡状态";
                    dgvResult.Columns["dtCardEffDate"].HeaderText = "卡生效日期";
                    dgvResult.Columns["dtCardExpDate"].HeaderText = "卡失效日期";
                    dgvResult.Columns["vcAssState"].HeaderText = "是否发卡";
                    dgvResult.Columns["dtInAssDate"].HeaderText = "入会日期";
                    dgvResult.Columns["vcComments"].HeaderText = "备注";
                    dgvResult.Columns["dtOperDate"].HeaderText = "操作日期";

                    dgvResult.Columns["iAssID"].Visible = false;
                    dgvResult.Columns.Remove("iAssID");

                    txtPTAssCount.Text = sumPTCount.ToString();
                    txtPTSumCharge.Text = sumPTCharge.ToString();
                    txtPTSumIG.Text = sumPTIg.ToString();
                    txtJKAssCount.Text = sumJKCount.ToString();
                    txtJKSumCharge.Text = sumJKCharge.ToString();
                    txtJKSumIG.Text = sumJKIg.ToString();
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
                            if (dgvResult.Columns[i].HeaderText == "卡余额" || dgvResult.Columns[i].HeaderText == "积分" || dgvResult.Columns[i].HeaderText == "充值总额" || dgvResult.Columns[i].HeaderText == "充值次数" || dgvResult.Columns[i].HeaderText == "平均充值" || dgvResult.Columns[i].HeaderText == "消费总额" || dgvResult.Columns[i].HeaderText == "消费次数" || dgvResult.Columns[i].HeaderText == "平均消费" || dgvResult.Columns[i].HeaderText == "赠金总额")
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
                this.ExportToExcel(table,"会员综合资料统计表");
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
