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
    public partial class frmBillReprint : Form
    {
        public frmBillReprint()
        {
            InitializeComponent();
        }

        private void frmBillReprint_Load(object sender, EventArgs e)
        {
            dtpBegin.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;

            dgvResult.AllowUserToAddRows = false;
            dgvResult.AllowUserToDeleteRows = false;
            dgvResult.AllowUserToResizeRows = true;
            dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvResult.ReadOnly = true;
            dgvResult.MultiSelect = false;
            dgvResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                    var queryresult = from a in amsContext.tbBillInvoice join b in amsContext.tbCommCode on a.vcBillType equals b.vcCommCode where a.dtCreateDate >= dtBegin && a.dtCreateDate <= dtEnd && a.vcEffFlag=="1" && b.vcCommSign == "BIT" select new { a.iBillNo, a.vcAssName, a.vcAssCardID, a.dtCreateDate, a.dtPrintDate, a.vcOperName, a.dTotalFee, a.vcPrintFlag, b.vcCommName };
                    if (strCardID != "")
                        queryresult = queryresult.Where(a => a.vcAssCardID == strCardID);
                    if (strAssName != "")
                        queryresult = queryresult.Where(a => a.vcAssName.StartsWith(strAssName) || a.vcAssName.EndsWith(strAssName));
                    if (BillNo > 0)
                        queryresult = queryresult.Where(a => a.iBillNo == BillNo);

                    queryresult = queryresult.OrderBy(a => a.vcAssCardID).ThenBy(a => a.iBillNo);

                    dgvResult.DataSource = queryresult;
                    dgvResult.Columns["iBillNo"].HeaderText = "帐单号";
                    dgvResult.Columns["vcAssName"].HeaderText = "会员名称";
                    dgvResult.Columns["vcAssCardID"].HeaderText = "会员卡号";
                    dgvResult.Columns["dtCreateDate"].HeaderText = "创建日期";
                    dgvResult.Columns["dtPrintDate"].HeaderText = "上次打印日期";
                    dgvResult.Columns["vcOperName"].HeaderText = "操作员";
                    dgvResult.Columns["dTotalFee"].HeaderText = "总金额";
                    dgvResult.Columns["vcPrintFlag"].HeaderText = "打印次数";
                    dgvResult.Columns["vcCommName"].HeaderText = "帐单类型";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "查询异常");
            }
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.SelectedRows.Count!=1)
                    throw new Exception("请先选中一条帐单记录");
                string strBillNo=dgvResult.SelectedRows[0].Cells[0].Value.ToString().Trim();
                long lbillno = 0;
                if (strBillNo == "")
                    throw new Exception("帐单流水有误");
                else
                    lbillno = long.Parse(strBillNo);
                using (AMSEntities amsContext = new AMSEntities())
                {
                    tbBillInvoice bl1 = amsContext.tbBillInvoice.FirstOrDefault(a => a.iBillNo == lbillno);
                    var querylist = from a in amsContext.tbBillList where a.iBillNo == lbillno select a;

                    if(bl1==null||querylist==null)
                        throw new Exception("获取帐单信息出错");

                    List<tbBillList> list = new List<tbBillList>();
                    foreach(var dr in querylist)
                    {
                        tbBillList billList = new tbBillList();
                        billList.iBillNo = dr.iBillNo;
                        billList.iNO = dr.iNO;
                        billList.vcGoodsName = dr.vcGoodsName;
                        billList.iCount = dr.iCount;
                        billList.vcUnit = dr.vcUnit;
                        billList.nPrice = dr.nPrice;
                        billList.nRate = dr.nRate;
                        billList.nCharge = dr.nCharge;
                        list.Add(billList);
                    }

                    tbBillInvoice billInvoice = new tbBillInvoice();
                    billInvoice.iBillNo = bl1.iBillNo;
                    billInvoice.vcAssName = bl1.vcAssName;
                    billInvoice.vcAssCardID = bl1.vcAssCardID;
                    billInvoice.dtCreateDate = bl1.dtCreateDate;
                    billInvoice.dtPrintDate = bl1.dtPrintDate;
                    billInvoice.dLastCharge = bl1.dLastCharge;
                    billInvoice.dLastIg = bl1.dLastIg;
                    billInvoice.vcLinkSerial = bl1.vcLinkSerial;
                    billInvoice.vcOperID = bl1.vcOperID;
                    billInvoice.vcOperName = bl1.vcOperName;
                    billInvoice.dSumFee = bl1.dSumFee;
                    billInvoice.dServiceFee = bl1.dServiceFee;
                    billInvoice.dMealFee = bl1.dMealFee;
                    billInvoice.dTotalFee = bl1.dTotalFee;
                    billInvoice.dCharge = bl1.dCharge;
                    billInvoice.dIgValue = bl1.dIgValue;
                    billInvoice.dIgGet = bl1.dIgGet;
                    billInvoice.vcPrintFlag = bl1.vcPrintFlag;
                    billInvoice.vcBillType = bl1.vcBillType;
                    billInvoice.vcEffFlag = bl1.vcEffFlag;
                    List<tbBillInvoice> invoice = new List<tbBillInvoice>();
                    invoice.Add(billInvoice);

                    Helper.MyPrint(invoice, list);
                    bl1.vcPrintFlag = (int.Parse(bl1.vcPrintFlag) + 1).ToString();
                    bl1.dtPrintDate = DateTime.Now;
                    Helper.Save(amsContext);
                }
                Helper.ShowInfo(this, "重新打印帐单成功");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "打印异常");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
