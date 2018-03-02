using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;
using System.Text.RegularExpressions;
using System.Data.Common;
using ams.Common;

namespace ams.Associator
{
    public partial class frmAssIgExchange : Form
    {
        public frmAssIgExchange()
        {
            InitializeComponent();
        }

        private void frmAssIgExchange_Load(object sender, EventArgs e)
        {
            try
            {
                using (AMSEntities amsContext = new AMSEntities())
                {
                    ObjectQuery<tbCommCode> IgExchange = amsContext.tbCommCode.OrderBy("it.vcCommCode").Where("it.vcCommSign='EX'");
                    cmbIgEXGoods.DisplayMember = "vcCommName";
                    cmbIgEXGoods.ValueMember = "vcCommCode";
                    cmbIgEXGoods.DataSource = IgExchange;
                }

                btnExOk.Enabled = false;
                btnPrint.Enabled = false;
                btnAdd.Enabled = false;
                btnDel.Enabled = false;
                txtIgSum.Text = "0";
                txtBillNo.Text = "";
                txtAssID.Text = "";

                DataTable dtIgExList = new DataTable();
                dtIgExList.Columns.Add("积分兑换项目");
                dtIgExList.Columns.Add("兑换分值");
                dtIgExList.Columns.Add("备注");

                dgvIgEx.DataSource = dtIgExList;
                dgvIgEx.AllowUserToAddRows = false;
                dgvIgEx.AllowUserToDeleteRows = false;
                dgvIgEx.AllowUserToResizeRows = false;
                dgvIgEx.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvIgEx.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvIgEx.ReadOnly = true;
                dgvIgEx.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvIgEx.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "加载异常");
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            int ret = 0;
            try
            {
                //调用读卡函数
                string strAssCardID = "";
                ret = Helper.ReadCard(ref strAssCardID);
                //strAssCardID = "00104";//测试用

                if (ret != 0)
                    throw new Exception("卡操作异常");

                if (strAssCardID == "")
                    throw new Exception("所刷卡卡号为空，请检查该卡片是否正确或被损坏！");

                using (AMSEntities amsContext = new AMSEntities())
                {
                    tbAssociatorCard asscard1 = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.vcAssCardID == strAssCardID);
                    if (asscard1 == null)
                        throw new Exception("该会员卡信息不存在，请检查！");
                    if (asscard1.cCardState != "1")
                        throw new Exception("该会员卡处于非正常在用状态，不能进行积分兑换！");
                    tbIntegral intg1 = amsContext.tbIntegral.FirstOrDefault(ig => ig.iAssID == asscard1.iAssID && ig.vcAssCardID == asscard1.vcAssCardID);
                    if (intg1 == null)
                        throw new Exception("获取会员卡余额和积分错误，请重试！");
                    tbAssociator ass1 = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == asscard1.iAssID);
                    if (ass1 == null)
                        throw new Exception("会员基本信息不存在，请重试！");
                    string strGoodsCode = cmbIgEXGoods.SelectedValue.ToString();
                    tbCommCode IgExGoods1 = amsContext.tbCommCode.FirstOrDefault(cc => cc.vcCommSign=="EX" && cc.vcCommCode == strGoodsCode);
                    if (IgExGoods1 == null)
                    {
                        txtIgExCost.Text = "";
                        throw new Exception("获取积分兑换项目异常！");
                    }
                    else
                    {
                        txtIgExCost.Text = IgExGoods1.vcComments;
                    }
                    txtCardID.Text = asscard1.vcAssCardID;
                    txtAssID.Text = asscard1.iAssID.ToString();
                    txtAssName.Text = ass1.vcAssName;
                    txtCurCharge.Text = intg1.nBalance.ToString();
                    txtCurIg.Text = intg1.iIgValue.ToString();
                    cmbIgEXGoods.Focus();
                    btnAdd.Enabled = true;
                    btnDel.Enabled = true;
                    btnPrint.Enabled = true;
                    btnRead.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "刷卡错误");
                Helper.ShowError(this, Helper.ReadCardError(ret));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strIgExCost=txtIgExCost.Text.Trim();
                if (strIgExCost == "")
                    throw new Exception("积分兑换分值不能为空！");
                if (!Regex.IsMatch(strIgExCost, @"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
                    throw new Exception("积分兑换分值必须是数字，请检查参数配置是否正确！");
                decimal deCurIg = decimal.Parse(txtCurIg.Text.Trim());
                decimal deIgSum = decimal.Parse(txtIgSum.Text.Trim());
                decimal denewig = decimal.Parse(txtIgExCost.Text.Trim());
                if (deCurIg < deIgSum + denewig)
                    throw new Exception("当前积分不足于兑换此项目！");
                DataTable dtexlist = (DataTable)dgvIgEx.DataSource;
                DataRow drnew = dtexlist.NewRow();
                drnew["积分兑换项目"] = cmbIgEXGoods.Text.Trim();
                drnew["兑换分值"] = txtIgExCost.Text.Trim();
                drnew["备注"] = txtComments.Text.Trim();
                dtexlist.Rows.Add(drnew);
                txtIgSum.Text = (deIgSum + denewig).ToString();
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "增加异常");
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvIgEx.Rows.Count==0||dgvIgEx.CurrentRow.Index < 0)
                    throw new Exception("请选中要删除的记录！");
                DataTable dtexlist = (DataTable)dgvIgEx.DataSource;
                decimal deIgSum = decimal.Parse(txtIgSum.Text.Trim());
                decimal denewig = decimal.Parse(dtexlist.Rows[dgvIgEx.CurrentRow.Index]["兑换分值"].ToString());
                dtexlist.Rows.RemoveAt(dgvIgEx.CurrentRow.Index);
                txtIgSum.Text = (deIgSum - denewig).ToString();
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "删除异常");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //需要将生成的发票流水写到txtBillNo文本框中

            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                if (dgvIgEx.Rows.Count <= 0)
                    throw new Exception("没有任何兑换记录");
                using (AMSEntities amsContext = new AMSEntities())
                {
                    string strAssCardID = txtCardID.Text.Trim();
                    int strAssID = int.Parse(txtAssID.Text.Trim());
                    decimal deTolIg = decimal.Parse(txtIgSum.Text.Trim());
                    tbIntegral intg1 = amsContext.tbIntegral.FirstOrDefault(ig => ig.iAssID == strAssID && ig.vcAssCardID == strAssCardID);
                    if (intg1 == null)
                        throw new Exception("会员余额积分异常！");
                    if (intg1.iIgValue < deTolIg)
                        throw new Exception("当前积分余额不足，请重新刷卡后再试！");

                    conn = amsContext.Connection;
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    using (trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        //事务数据操作或其它一致性操作
                        tbBillSerialNo serialNo = new tbBillSerialNo();
                        serialNo.vcFill = "0";
                        amsContext.AddTotbBillSerialNo(serialNo);
                        Helper.Save(amsContext);

                        List<tbBillList> list = new List<tbBillList>();
                        DataTable dtIgList = (DataTable)dgvIgEx.DataSource;
                        for (int i = 0; i < dtIgList.Rows.Count; i++)
                        {
                            tbBillList billList = new tbBillList();
                            billList.iBillNo = serialNo.iSerialNo;//lSerialNo;
                            billList.iNO = i + 1;
                            billList.vcGoodsName = dtIgList.Rows[i]["积分兑换项目"].ToString();
                            billList.iCount = 1;
                            billList.vcUnit = "";
                            billList.nPrice = decimal.Parse(dtIgList.Rows[i]["兑换分值"].ToString());
                            billList.nRate = 1;
                            billList.nCharge = decimal.Parse(dtIgList.Rows[i]["兑换分值"].ToString());
                            amsContext.AddTotbBillList(billList);
                            list.Add(billList);
                        }

                        DateTime dtoperdate = DateTime.Now;
                        tbBillInvoice billInvoice = new tbBillInvoice();
                        billInvoice.iBillNo = serialNo.iSerialNo;
                        billInvoice.vcAssName = txtAssName.Text.Trim();
                        billInvoice.vcAssCardID = strAssCardID;
                        billInvoice.dtCreateDate = dtoperdate;
                        billInvoice.dtPrintDate = dtoperdate;
                        billInvoice.dLastCharge = intg1.nBalance;
                        billInvoice.dLastIg = intg1.iIgValue;
                        billInvoice.vcOperID = GlobalParams.oper.vcOperID;
                        billInvoice.vcOperName = GlobalParams.oper.vcOperName;
                        billInvoice.dSumFee = deTolIg;
                        billInvoice.dServiceFee = 0;
                        billInvoice.dMealFee = 0;
                        billInvoice.dTotalFee = deTolIg;
                        billInvoice.dCharge = intg1.nBalance;
                        billInvoice.dIgValue = (decimal)intg1.iIgValue - deTolIg;
                        billInvoice.dIgGet = -deTolIg;
                        billInvoice.vcPrintFlag = "1";
                        billInvoice.vcBillType = "BI003";
                        billInvoice.vcEffFlag = "0";
                        amsContext.AddTotbBillInvoice(billInvoice);

                        Helper.Save(amsContext);

                        List<tbBillInvoice> invoice = new List<tbBillInvoice>();
                        invoice.Add(billInvoice);

                        Helper.MyPrint(invoice, list);
                        trans.Commit();

                        txtBillNo.Text = serialNo.iSerialNo.ToString();
                    }
                }
                cmbIgEXGoods.Enabled = false;
                txtComments.Enabled = false;
                btnAdd.Enabled = false;
                btnDel.Enabled = false;
                btnPrint.Enabled = false;
                btnRead.Enabled = false;
                btnExOk.Enabled = true;
                Helper.ShowInfo(this, "打印回执成功");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "打印异常");
            }
        }

        private void btnExOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvIgEx.Rows.Count == 0)
                    throw new Exception("没有任何积分兑换记录，请重新输入！");
                string strBillNo = txtBillNo.Text.Trim();
                if (strBillNo == "")
                    throw new Exception("请先打印回执！");
                int AssID=int.Parse(txtAssID.Text.Trim());
                string strCardID=txtCardID.Text.Trim();
                int IgSum=int.Parse(txtIgSum.Text.Trim());
                bool continueflg = true;
                using (AMSEntities amsContext = new AMSEntities())
                {
                    tbAssociatorCard asscard1 = amsContext.tbAssociatorCard.FirstOrDefault(ac =>ac.iAssID==AssID && ac.vcAssCardID == strCardID);
                    while (continueflg)
                    {
                        frmInputBox inbox = new frmInputBox("请输入会员卡密码：", "PWD");
                        inbox.ShowDialog();
                        if (GlobalParams.strInputBoxMes == "Cancel")
                        {
                            GlobalParams.strInputBoxMes = "";
                            return;
                        }
                        else
                        {
                            if (GlobalParams.strInputBoxMes == "")
                                MessageBox.Show("会员卡密码不能为空，请重新输入！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                if (asscard1 == null)
                                    MessageBox.Show("会员信息有误，请重试！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (GlobalParams.strInputBoxMes != asscard1.vcAssPwd)
                                    MessageBox.Show("输入的会员卡密码错误，请重试！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                    continueflg = false;
                            }
                        }
                    }

                    tbIntegral intg1 = amsContext.tbIntegral.FirstOrDefault(ig => ig.iAssID == AssID && ig.vcAssCardID == strCardID);
                    if (intg1 == null)
                        throw new Exception("获取会员卡积分错误，请重试！");
                    if(intg1.iIgValue<IgSum)
                        throw new Exception("当前积分余额不足，请重新读卡后重试！");
                    long billNo=long.Parse(strBillNo);
                    tbBillInvoice bill1=amsContext.tbBillInvoice.FirstOrDefault(bl=>bl.iBillNo==billNo);
                    if(bill1==null)
                        throw new Exception("帐单不存在，请重试！");

                    long igserialNo = 0;
                    using (AMSEntities amscontext2 = new AMSEntities())
                    {
                        tbIgSerialNo igserial1 = new tbIgSerialNo();
                        igserial1.vcFill = "0";
                        amscontext2.AddTotbIgSerialNo(igserial1);
                        amscontext2.SaveChanges();
                        igserialNo = igserial1.iSerialNo;
                    }

                    DateTime dtoperdate = DateTime.Now;
                    intg1.iIgValue = intg1.iIgValue - IgSum;

                    ObjectStateEntry ose = amsContext.ObjectStateManager.GetObjectStateEntry(intg1);
                    DataTable dtIgList=(DataTable)dgvIgEx.DataSource;
                    int lastig = int.Parse(ose.OriginalValues["iIgValue"].ToString());
                    for (int i = 0; i < dtIgList.Rows.Count;i++ )
                    {
                        tbIntegralLog intglog1 = new tbIntegralLog();
                        intglog1.iIgSerial = igserialNo;
                        intglog1.iNo = i+1;
                        intglog1.iAssID = intg1.iAssID;
                        intglog1.vcAssCardID = intg1.vcAssCardID;
                        intglog1.iIgLast = lastig;
                        intglog1.dtIgDate = dtoperdate;
                        intglog1.vcIgName = dtIgList.Rows[i]["积分兑换项目"].ToString();
                        intglog1.vcIgType = "IG002";
                        intglog1.iIgGet = -(int.Parse(dtIgList.Rows[i]["兑换分值"].ToString()));
                        intglog1.iIgArrival = lastig + intglog1.iIgGet;
                        intglog1.iLinkCons = null;
                        intglog1.vcOperID = GlobalParams.oper.vcOperID;
                        intglog1.vcComments = dtIgList.Rows[i]["备注"].ToString();
                        amsContext.AddTotbIntegralLog(intglog1);
                        lastig = (int)intglog1.iIgArrival;
                    }

                    bill1.vcLinkSerial = igserialNo;
                    bill1.vcEffFlag = "1";

                    tbBusiLog busilog1 = new tbBusiLog();
                    busilog1.vcAssName = txtAssName.Text.Trim();
                    busilog1.vcAssCardID = intg1.vcAssCardID;
                    busilog1.vcLinkSerial = igserialNo;
                    busilog1.vcOperType = "OT013";
                    busilog1.vcOperID = GlobalParams.oper.vcOperID;
                    busilog1.vcOperName = GlobalParams.oper.vcOperName;
                    busilog1.dtOperDate = dtoperdate;
                    busilog1.iAssID = intg1.iAssID;
                    amsContext.AddTotbBusiLog(busilog1);

                    amsContext.SaveChanges();

                    btnExOk.Enabled = false;
                    btnPrint.Enabled = false;
                    btnAdd.Enabled = false;
                    btnDel.Enabled = false;
                    btnRead.Enabled = true;
                    txtIgSum.Text = "0";
                    txtBillNo.Text = "";
                    txtAssID.Text = "";
                    txtCardID.Text = "";
                    txtAssName.Text = "";
                    txtCurCharge.Text = "";
                    txtCurIg.Text = "";
                    DataTable dtIgExList = new DataTable();
                    dtIgExList.Columns.Add("积分兑换项目");
                    dtIgExList.Columns.Add("兑换分值");
                    dtIgExList.Columns.Add("备注");
                    dgvIgEx.DataSource = dtIgExList;

                    MessageBox.Show("本次积分兑换成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "积分兑换异常");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbIgEXGoods_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbIgEXGoods.Items.Count > 0)
                {
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        string strGoodsCode = cmbIgEXGoods.SelectedValue.ToString();
                        tbCommCode IgExGoods1 = amsContext.tbCommCode.FirstOrDefault(cc => cc.vcCommSign == "EX" && cc.vcCommCode == strGoodsCode);
                        if (IgExGoods1 == null)
                        {
                            txtIgExCost.Text = "";
                            throw new Exception("获取积分兑换项目异常，请检查积分兑换项目的参数配置是否正确！");
                        }
                        else
                        {
                            txtIgExCost.Text = IgExGoods1.vcComments;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "刷新异常");
            }
        }
    }
}
