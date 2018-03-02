using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ams.Common;
namespace ams.Associator
{
    public partial class frmCardReturn : Form
    {
        public tbAssociator ass { get; set; }
        public tbAssociatorCard assCard { get; set; }
        public tbIntegral ig{get;set;}
        private tbBillInvoice billInvoice;
        public frmCardReturn()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            //假如已打印回执未充值给出提示不是否关闭窗体
            if (billInvoice != null)
            {
                if (MessageBox.Show(this, "退卡未完成，并已打印回执，是否要退出退卡操作？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btnAssData_Click(object sender, EventArgs e)
        {
            frmAss frmass = new frmAss();
            frmass.ass = ass;
            frmass.OperType = "DETAIL";
            frmass.MinimizeBox = false;
            frmass.MaximizeBox = false;
            frmass.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //打印回执
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                using (AMSEntities amsContext = new AMSEntities())
                {
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

                        decimal dReturnFee = decimal.Round(ig.nBalance * 2 / 10, 2);
                        decimal dFee = ig.nBalance - dReturnFee;

                        tbBillList billList = new tbBillList();
                        billList.iBillNo = serialNo.iSerialNo;
                        billList.iCount = 1;
                        billList.iNO = 1;
                        billList.nCharge = dFee;// ig.nBalance;// -dReturnFee;
                        billList.nPrice = dFee;// ig.nBalance;// -dReturnFee;
                        billList.nRate = 1;
                        billList.vcGoodsName = "退还金额";
                        //billList.vcUnit = "";
                        amsContext.AddTotbBillList(billList);

                        tbBillList billList2 = new tbBillList();
                        billList2.iBillNo = serialNo.iSerialNo;
                        billList2.iCount = 1;
                        billList2.iNO = 2;
                        billList2.nCharge = dReturnFee;
                        billList2.nPrice = dReturnFee;
                        billList2.nRate = 1;
                        billList2.vcGoodsName = "退卡费";
                        //billList.vcUnit = "";
                        amsContext.AddTotbBillList(billList2);

                        billInvoice = new tbBillInvoice();
                        billInvoice.dCharge = ig.nBalance; //0;// ig.nBalance + iFillFee + iExtraFee;
                        billInvoice.dIgGet = 0;
                        billInvoice.dIgValue = 0;//ig.iIgValue;
                        billInvoice.dLastCharge = ig.nBalance;
                        billInvoice.dLastIg = ig.iIgValue;
                        billInvoice.dMealFee = 0;
                        billInvoice.dServiceFee = 0;
                        billInvoice.dSumFee = ig.nBalance; // dFee;// ig.nBalance - dReturnFee;//0;// iFillFee + iExtraFee;
                        billInvoice.dtCreateDate = DateTime.Now;
                        billInvoice.dTotalFee = ig.nBalance; //dFee;// ig.nBalance - dReturnFee; ;//0;// iFillFee;
                        billInvoice.dtPrintDate = DateTime.Now;
                        billInvoice.iBillNo = serialNo.iSerialNo;
                        billInvoice.vcAssCardID = assCard.vcAssCardID;
                        billInvoice.vcAssName = ass.vcAssName;//txtAssName.Text;
                        billInvoice.vcBillType = "BI006";
                        billInvoice.vcEffFlag = "0";
                        billInvoice.vcLinkSerial = null;
                        billInvoice.vcOperName = GlobalParams.oper.vcOperName;
                        billInvoice.vcOperID = GlobalParams.oper.vcOperID;
                        billInvoice.vcPrintFlag = "1";
                        //billInvoice.vcReserve = "";     
                        amsContext.AddTotbBillInvoice(billInvoice);

                        Helper.Save(amsContext);
                        //打印帐单-----------------------------------
                        //打印帐单-----------------------------------
                        List<tbBillInvoice> invoice = new List<tbBillInvoice>();
                        invoice.Add(billInvoice);
                        List<tbBillList> list = new List<tbBillList>();
                        list.Add(billList);
                        list.Add(billList2);
                        Helper.MyPrint(invoice, list);
                        trans.Commit();
                    }
                }
                btnPrint.Enabled = false;
                btnCardReturn.Enabled = false;
                txtPwd.Enabled = true;
                txtPwd.Focus();
                Helper.ShowInfo(this, "打印回执成功，请输入会员卡密码，进行退卡操作");
            }
            catch (Exception ex)
            {
                billInvoice = null;
                ErrorLog.Write(this, ex);
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    if (string.IsNullOrEmpty(txtPwd.Text))
                        throw new Exception("请输入用户密码！");
                    if (!txtPwd.Text.Equals(assCard.vcAssPwd))
                        throw new Exception("用户密码不正确！");

                    txtPwd.Enabled = false;
                    btnCardReturn.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                //异常操作
                ErrorLog.Write(this, ex);
            }
        }

        private void frmCardReturn_Load(object sender, EventArgs e)
        {
            try
            {
                if (ass == null || assCard == null||ig==null)
                    throw new Exception("请首先查询会员，并选择某个会员卡进行会员卡退卡操作");

                txtCardID.Text = assCard.vcAssCardID;
                txtBalance.Text = ig.nBalance.ToString();
                txtIg.Text = ig.iIgValue.ToString();

                decimal dReturnFee = decimal.Round(ig.nBalance * 2 / 10, 2);
                txtReturnFee.Text = dReturnFee.ToString();
                txtFee.Text = Convert.ToString(ig.nBalance-dReturnFee);

                txtReturnFee.Enabled = false;
                txtFee.Enabled = false;
                txtIg.Enabled = false;

                txtAssName.Text = ass.vcAssName;
                txtAssNbr.Text = ass.vcAssNbr;
                txtCompanyName.Text = ass.vcCompanyName;

                btnAssData.Enabled = true;

                btnPrint.Enabled = true;
                btnCardReturn.Enabled = false;

                txtCardID.Enabled = false;
                txtAssName.Enabled = false;
                txtBalance.Enabled = false;
                txtAssNbr.Enabled = false;
                txtCompanyName.Enabled = false;
                txtPwd.MaxLength = 6;
                txtPwd.Enabled = false;
                //txtPwd.Focus();
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = false;
                btnCardReturn.Enabled = false;
                btnAssData.Enabled = false;
                ErrorLog.Write(this, ex);
            }
        }

        private void btnCardReturn_Click(object sender, EventArgs e)
        {
            //退卡
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                if (string.IsNullOrEmpty(txtPwd.Text))
                    throw new Exception("请输入会员卡密码");
                if (!assCard.vcAssPwd.Equals(txtPwd.Text))
                    throw new Exception("会员卡密码不正确");
                DialogResult dr = MessageBox.Show(this, "请确认：\n退卡卡号：" + assCard.vcAssCardID, "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {

                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        conn = amsContext.Connection;
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        
                        using (trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                        {
                            //事务数据操作或其它一致性操作
                            decimal dReturnFee = decimal.Round(ig.nBalance * 2 / 10, 2);
                            decimal dFee = ig.nBalance - dReturnFee;

                            tbFillFee fillFee = new tbFillFee();
                            fillFee.dtFillDate = DateTime.Now;
                            fillFee.iAssID = assCard.iAssID;
                            //fillFee.iFillSerial
                            fillFee.nBalance = 0;// +iFillFee + iExtraFee;
                            fillFee.nFillFee = -ig.nBalance;//-dFee;//Convert.ToDecimal(iFillFee);
                            fillFee.nLastBalance = ig.nBalance;
                            fillFee.nPromFee = 0;// -dReturnFee;//iExtraFee;
                            fillFee.vcAssCardID = assCard.vcAssCardID;// AssCardID;
                            fillFee.vcComments = txtComments.Text;
                            fillFee.vcOperID = GlobalParams.oper.vcOperID;
                            fillFee.vcOperType = "OT004";
                            amsContext.AddTotbFillFee(fillFee);
                            Helper.Save(amsContext);

                            tbBusiFee busiFee = new tbBusiFee();
                            busiFee.dtOperDate = DateTime.Now;
                            busiFee.iAssID = assCard.iAssID;
                            //busiFee.iSerial = 
                            busiFee.nFee = dReturnFee;
                            busiFee.vcAssCardID = assCard.vcAssCardID;
                            busiFee.vcFeeCode = "GB002";
                            busiFee.vcOperID = GlobalParams.oper.vcOperID;
                            busiFee.vcSvcCode = "OT004";
                            amsContext.AddTotbBusiFee(busiFee);

                            tbIntegral igm = amsContext.tbIntegral.FirstOrDefault(i=>i.iAssID==ig.iAssID&&i.vcAssCardID==ig.vcAssCardID);
                            if(igm==null)
                                throw new Exception("未找到会员积分信息");
                            igm.nBalance = 0;
                            igm.iIgValue = 0;

                            tbBusiLog busiLog = new tbBusiLog();
                            busiLog.dtOperDate = DateTime.Now;
                            busiLog.iAssID = assCard.iAssID;
                            busiLog.vcAssCardID = assCard.vcAssCardID;
                            busiLog.vcAssName = txtAssName.Text;
                            busiLog.vcOperName = GlobalParams.oper.vcOperName;
                            busiLog.vcOperID = GlobalParams.oper.vcOperID;
                            busiLog.vcOperType = "OT004";
                            busiLog.vcLinkSerial = fillFee.iFillSerial;
                            amsContext.AddTotbBusiLog(busiLog);
                            

                            tbAssociator assm = amsContext.tbAssociator.FirstOrDefault(a=>a.iAssID==ass.iAssID);
                            if(assm==null)
                                throw new Exception("未找到会员信息");
                            assm.vcAssState=ConstApp.AST_0;
                            assm.dtOperDate = DateTime.Now;
                            assm.vcOperID = GlobalParams.oper.vcOperID;                            

                            tbAssociatorCard assCardm = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.iAssID == assCard.iAssID && ac.vcAssCardID == assCard.vcAssCardID);
                            if (assCardm == null)
                                throw new Exception("未找到会员卡信息");
                            //if (assCardm.cCardState != ConstApp.CST_1)
                            //    throw new Exception("会员卡不在正常使用状态，无法挂失");
                            assCardm.cCardState = ConstApp.CST_0;
                            assCardm.dtCardExpDate = DateTime.Now;
                            assCardm.vcOperID = GlobalParams.oper.vcOperID;
                            assCardm.dtOperDate = DateTime.Now;

                            if (billInvoice == null)
                                throw new Exception("请首先打印回执");
                            tbBillInvoice billInvoicem = amsContext.tbBillInvoice.FirstOrDefault(b => b.iBillNo == billInvoice.iBillNo);
                            billInvoicem.vcEffFlag = "1";
                            billInvoicem.vcLinkSerial = fillFee.iFillSerial;

                            Helper.Save(amsContext);
                            trans.Commit();
                        }
                    }

                    btnPrint.Enabled = false;
                    btnCardReturn.Enabled = false;
                    txtPwd.Enabled = false;
                    billInvoice = null;
                    MessageBox.Show(this, "会员卡退卡成功", "提示");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
    }
}
