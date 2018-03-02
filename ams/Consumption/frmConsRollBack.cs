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
    public partial class frmConsRollBack : Form
    {
        public frmConsRollBack()
        {
            InitializeComponent();
        }

        private void frmConsRollBack_Load(object sender, EventArgs e)
        {
            DataTable dtConsList = new DataTable();
            dtConsList.Columns.Add("消费项名称");
            dtConsList.Columns.Add("单价");
            dtConsList.Columns.Add("折扣");
            dtConsList.Columns.Add("数量");
            dtConsList.Columns.Add("单项合计");
            dtConsList.Columns.Add("备注");
            dtConsList.PrimaryKey = new DataColumn[] { dtConsList.Columns["消费项名称"], dtConsList.Columns["单价"], dtConsList.Columns["折扣"] };

            dgvConsList.DataSource = dtConsList;
            dgvConsList.AllowUserToAddRows = false;
            dgvConsList.AllowUserToDeleteRows = false;
            dgvConsList.AllowUserToResizeRows = false;
            dgvConsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvConsList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvConsList.ReadOnly = true;
            dgvConsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConsList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            btnRollBack.Enabled = false;
            txtCardID.Focus();
        }

        private void txtCardID_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
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
                else
                {
                    string strCardID = txtCardID.Text.Trim();
                    if (strCardID == "")
                        throw new Exception("会员卡号不能为空！");
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        tbAssociatorCard asscard1 = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.vcAssCardID == strCardID && ac.cCardState=="1");
                        if (asscard1 == null)
                            throw new Exception("会员信息不符，请检查是否此会员卡不是正常在用！");
                        tbAssociator ass1 = amsContext.tbAssociator.FirstOrDefault(ass => ass.iAssID == asscard1.iAssID);
                        if(ass1==null)
                            throw new Exception("会员信息不符，请检查是否此会员卡不是正常在用！");
                        tbIntegral intg1 = amsContext.tbIntegral.FirstOrDefault(ig => ig.iAssID == asscard1.iAssID && ig.vcAssCardID == asscard1.vcAssCardID);
                        if (intg1 == null)
                            throw new Exception("获取当前余额积分异常！");
                        var dtMaxIgDate = (from item in amsContext.tbIntegralLog where item.iAssID == asscard1.iAssID && item.vcAssCardID == asscard1.vcAssCardID && item.vcIgType == "IG002" select item.dtIgDate).Max();
                        string maxConsSerial="";
                        if (dtMaxIgDate != null)
                        {
                            maxConsSerial = (from item in amsContext.tbConsumption where item.iAssID==asscard1.iAssID && item.vcAssCardID==asscard1.vcAssCardID && item.dtConsDate > dtMaxIgDate && item.vcFlag == "0" select item.iConsSerial).Max().ToString();
                        }
                        else
                        {
                            maxConsSerial = (from item in amsContext.tbConsumption where item.iAssID == asscard1.iAssID && item.vcAssCardID == asscard1.vcAssCardID && item.vcFlag == "0" select item.iConsSerial).Max().ToString();
                        }
                        if (maxConsSerial == null || maxConsSerial == "")
                            throw new Exception("没有可返销的消费帐单记录！");

                        long longcons=long.Parse(maxConsSerial);
                        var conslist = from item in amsContext.tbConsumption where item.iConsSerial == longcons orderby item.iNO select new { item.vcGoodsCode, item.nGoodsPrice, item.nConsRate,item.iConsCount,  item.nConsCharge2,item.vcComments };
                        if (conslist == null)
                            throw new Exception("没有可返销的消费帐单记录！");
                        tbBillInvoice bill1 = amsContext.tbBillInvoice.FirstOrDefault(bl => bl.vcAssCardID == asscard1.vcAssCardID && bl.vcEffFlag == "1" && bl.vcLinkSerial == longcons);
                        if(bill1==null)
                            throw new Exception("没有可返销的消费帐单记录！");

                        txtAssName.Text = ass1.vcAssName;
                        txtBillNo.Text = bill1.iBillNo.ToString();
                        txtCurCharge.Text = intg1.nBalance.ToString();
                        txtCurIG.Text = intg1.iIgValue.ToString();
                        txtConsFee.Text = bill1.dTotalFee.ToString();
                        txtConsDate.Text = bill1.dtCreateDate.Value.ToShortDateString();
                        txtConsSerial.Text = maxConsSerial;
                        txtAssID.Text = asscard1.iAssID.ToString();

                        dgvConsList.DataSource = conslist;
                        dgvConsList.Columns["vcGoodsCode"].HeaderText = "消费项名称";
                        dgvConsList.Columns["nGoodsPrice"].HeaderText = "单价";
                        dgvConsList.Columns["nConsRate"].HeaderText = "折扣";
                        dgvConsList.Columns["iConsCount"].HeaderText = "数量";
                        dgvConsList.Columns["nConsCharge2"].HeaderText = "单项合计";
                        dgvConsList.Columns["vcComments"].HeaderText = "备注";

                        dgvConsList.AllowUserToAddRows = false;
                        dgvConsList.AllowUserToDeleteRows = false;
                        dgvConsList.AllowUserToResizeRows = false;
                        dgvConsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dgvConsList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                        dgvConsList.ReadOnly = true;
                        dgvConsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvConsList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                        btnRollBack.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "校验错误");
                txtCardID.Focus();
            }
        }

        private void btnRollBack_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dreocon = MessageBox.Show("请确定要将此消费帐单返销吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dreocon == DialogResult.Yes)
                {
                    long conserialNo = 0;
                    long igserialNo = 0;
                    using (AMSEntities amscontext2 = new AMSEntities())
                    {
                        tbConsserialNo conserial1 = new tbConsserialNo();
                        conserial1.vcFill = "0";
                        amscontext2.AddTotbConsserialNo(conserial1);
                        tbIgSerialNo igserial1 = new tbIgSerialNo();
                        igserial1.vcFill = "0";
                        amscontext2.AddTotbIgSerialNo(igserial1);
                        amscontext2.SaveChanges();
                        conserialNo = conserial1.iSerialNo;
                        igserialNo = igserial1.iSerialNo;
                    }

                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        DateTime dtoperdate = DateTime.Now;
                        long longassid = long.Parse(txtAssID.Text.Trim());
                        tbAssociatorCard asscard1 = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.iAssID == longassid && ac.cCardState == "1");
                        if (asscard1 == null)
                            throw new Exception("会员信息不符，请检查是否此会员卡不是正常在用，请重试！");
                        tbIntegral intg1 = amsContext.tbIntegral.FirstOrDefault(ig => ig.iAssID == asscard1.iAssID && ig.vcAssCardID == asscard1.vcAssCardID);
                        if (intg1 == null)
                            throw new Exception("获取当前余额积分异常，请重试！");
                        var dtMaxIgDate = (from item in amsContext.tbIntegralLog where item.iAssID == asscard1.iAssID && item.vcAssCardID == asscard1.vcAssCardID && item.vcIgType == "IG002" select item.dtIgDate).Max();
                        string maxConsSerial = "";
                        if (dtMaxIgDate != null)
                        {
                            maxConsSerial = (from item in amsContext.tbConsumption where item.iAssID == asscard1.iAssID && item.vcAssCardID == asscard1.vcAssCardID && item.dtConsDate > dtMaxIgDate && item.vcFlag == "0" select item.iConsSerial).Max().ToString();
                        }
                        else
                        {
                            maxConsSerial = (from item in amsContext.tbConsumption where item.iAssID == asscard1.iAssID && item.vcAssCardID == asscard1.vcAssCardID && item.vcFlag == "0" select item.iConsSerial).Max().ToString();
                        }
                        if (maxConsSerial == null || maxConsSerial == "")
                            throw new Exception("没有可返销的消费帐单记录，请重试！");

                        long longcons = long.Parse(maxConsSerial);
                        var conslist = from item in amsContext.tbConsumption where item.iConsSerial == longcons orderby item.iNO select item;
                        if (conslist == null)
                            throw new Exception("没有可返销的消费帐单记录！");
                        tbBillInvoice bill1 = amsContext.tbBillInvoice.FirstOrDefault(bl => bl.vcAssCardID == asscard1.vcAssCardID && bl.vcEffFlag == "1" && bl.vcLinkSerial == longcons);
                        if (bill1 == null)
                            throw new Exception("没有可返销的消费帐单记录！");
                        tbIntegralLog iglog1 = amsContext.tbIntegralLog.FirstOrDefault(igg => igg.iLinkCons == longcons);
                        if (iglog1 == null)
                            throw new Exception("没有可返销的消费帐单记录！");
                        if (bill1.iBillNo.ToString() != txtBillNo.Text.Trim() || maxConsSerial != txtConsSerial.Text.Trim())
                            throw new Exception("数据异常，请重试！");

                        intg1.nBalance = intg1.nBalance + (decimal)bill1.dTotalFee;
                        intg1.iIgValue = (int)intg1.iIgValue - (int)bill1.dIgGet;

                        ObjectStateEntry ose = amsContext.ObjectStateManager.GetObjectStateEntry(intg1);
                        foreach (var item in conslist)
                        {
                            item.vcFlag = "1";
                            item.iLink = conserialNo;
                            item.dtOperDate = dtoperdate;
                            item.vcComments += "-由操作员：" + GlobalParams.oper.vcOperName + "，返销";

                            tbConsumption cons1 = new tbConsumption();
                            cons1.iConsSerial = conserialNo;
                            cons1.iNO = item.iNO;
                            cons1.iAssID = item.iAssID;
                            cons1.vcAssCardID = item.vcAssCardID;
                            cons1.vcGoodsCode = item.vcGoodsCode;
                            cons1.nGoodsPrice = -item.nGoodsPrice;
                            cons1.iConsCount = item.iConsCount;
                            cons1.nLastBalance = decimal.Parse(ose.OriginalValues["nBalance"].ToString());
                            cons1.nConsCharge1 = -item.nConsCharge1;
                            cons1.nConsRate = item.nConsRate;
                            cons1.nConsCharge2 = -item.nConsCharge2;
                            cons1.nBalance = decimal.Parse(ose.CurrentValues["nBalance"].ToString());
                            cons1.dtConsDate = dtoperdate;
                            cons1.dtOperDate = dtoperdate;
                            cons1.vcFlag = "9";
                            cons1.iLink = item.iConsSerial;
                            cons1.vcOperID = GlobalParams.oper.vcOperID;
                            cons1.vcClass = GlobalParams.strClass;
                            cons1.vcComments = "";
                            amsContext.AddTotbConsumption(cons1);
                        }

                        iglog1.vcComments += "-本次积分已被返销";

                        tbIntegralLog ignew = new tbIntegralLog();
                        ignew.iIgSerial = igserialNo;
                        ignew.iNo = 1;
                        ignew.iAssID = iglog1.iAssID;
                        ignew.vcAssCardID = iglog1.vcAssCardID;
                        ignew.iIgLast = int.Parse(ose.OriginalValues["iIgValue"].ToString());
                        ignew.dtIgDate = dtoperdate;
                        ignew.vcIgName = "0";
                        ignew.vcIgType = "IG003";
                        ignew.iIgGet = -(int)bill1.dIgGet;
                        ignew.iIgArrival = int.Parse(ose.CurrentValues["iIgValue"].ToString());
                        ignew.iLinkCons = conserialNo;
                        ignew.vcOperID = GlobalParams.oper.vcOperID;
                        ignew.vcComments = "";
                        amsContext.AddTotbIntegralLog(ignew);

                        tbBusiLog busilog1 = new tbBusiLog();
                        busilog1.vcAssName = txtAssName.Text.Trim();
                        busilog1.vcAssCardID = intg1.vcAssCardID;
                        busilog1.vcLinkSerial = conserialNo;
                        busilog1.vcOperType = "OT012";
                        busilog1.vcOperID = GlobalParams.oper.vcOperID;
                        busilog1.vcOperName = GlobalParams.oper.vcOperName;
                        busilog1.dtOperDate = dtoperdate;
                        busilog1.iAssID = intg1.iAssID;
                        amsContext.AddTotbBusiLog(busilog1);

                        amsContext.SaveChanges();

                        txtCardID.Text = "";
                        txtAssName.Text = "";
                        txtBillNo.Text = "";
                        txtCurCharge.Text = "";
                        txtCurIG.Text = "";
                        txtConsFee.Text = "";
                        txtConsDate.Text = "";
                        txtAssID.Text = "";
                        txtConsSerial.Text = "";
                        btnRollBack.Enabled = false;
                        DataTable dtConsList = new DataTable();
                        dtConsList.Columns.Add("消费项名称");
                        dtConsList.Columns.Add("单价");
                        dtConsList.Columns.Add("折扣");
                        dtConsList.Columns.Add("数量");
                        dtConsList.Columns.Add("单项合计");
                        dtConsList.Columns.Add("备注");
                        dtConsList.PrimaryKey = new DataColumn[] { dtConsList.Columns["消费项名称"], dtConsList.Columns["单价"], dtConsList.Columns["折扣"] };
                        dgvConsList.DataSource = dtConsList;

                        MessageBox.Show("本次消费帐单返销成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtCardID.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "返销异常");
                this.Close();
            }
        }
    }
}
