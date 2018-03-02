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
using System.IO;
using System.Drawing.Printing;


namespace ams.Associator
{
    public partial class frmCardFillFee : Form
    {
        private tbAssociator ass;
        private tbAssociatorCard assCard;
        private tbIntegral ig;
        private tbBillInvoice billInvoice;

        public frmCardFillFee()
        {
            InitializeComponent();
        }
        #region 关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            //假如已打印回执未充值给出提示不是否关闭窗体
            if (billInvoice != null)
            {
                if (MessageBox.Show(this, "充值未完成，并已打印回执，是否要退出充值操作？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
        #endregion

        #region 读卡
        private void btnReadCard_Click(object sender, EventArgs e)
        {
            int ret = 0;
            try
            {
                //从设备读取卡号信息
                //开始卡操作----------------------------------
                string strCardNo = "";
                ret = Helper.ReadCard(ref strCardNo);
                if (ret != 0)
                    throw new Exception("卡操作异常");
                //strCardNo = "00106";
                txtAssCardID.Text = strCardNo;
                if (string.IsNullOrEmpty(txtAssCardID.Text))
                    throw new Exception("请读取卡号");

                using (AMSEntities amsContext = new AMSEntities())
                {
                    //数据操作
                    assCard = amsContext.tbAssociatorCard.FirstOrDefault(a => a.vcAssCardID == txtAssCardID.Text && a.cCardState == "1");
                    if (assCard == null)
                        throw new Exception("该会员卡信息不存在，或不在正常使用状态，请重试！");
                    ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == assCard.iAssID);
                    if (ass == null)
                        throw new Exception("该会员卡的会员资料不存在，请检查！");
                    ig = amsContext.tbIntegral.FirstOrDefault(i => i.iAssID == assCard.iAssID && i.vcAssCardID == assCard.vcAssCardID);
                    if (ig == null)
                        throw new Exception("获取会员卡积分错误，请重试！");
                }
                //txtAssCardID.Text = assCard.vcAssCardID;
                txtAssName.Text = ass.vcAssName;
                txtBalance.Text = ig.nBalance.ToString();                
                txtFillFee.Focus();
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
                Helper.ShowError(this, Helper.PutCardError(ret));
            }
        }
        #endregion

        #region 充值
        private void btnFillFee_Click(object sender, EventArgs e)
        {
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                if (ass == null || assCard == null||ig==null)
                    throw new Exception("请首先读取会员卡信息！");
                if (billInvoice == null)
                    throw new Exception("请首先打印回执");
                int iFillFee = Convert.ToInt32(txtFillFee.Value);
                if (iFillFee < 1000)
                    throw new Exception("充值金额必须是大于1000元的整数");
                var fillProm = GlobalParams.FillProm.FirstOrDefault(f => f.iCelling > iFillFee && f.iFloor <= iFillFee);
                if (fillProm == null)
                    throw new Exception("获取优惠比率出错，请检查优惠比率配置是否正确！");
                int iExtraFee = iFillFee * fillProm.iRate / 100;
               
                string str = "请确认，是否在" + assCard.vcAssCardID + "卡上充入下面金额：\n充值金额：" + iFillFee.ToString() + "\n赠送金额：" + iExtraFee.ToString() + "\n最终充值金额：" + (iFillFee + iExtraFee).ToString();

                if (MessageBox.Show(this, str, "充值确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                            var igm = amsContext.tbIntegral.FirstOrDefault(i => i.vcAssCardID == assCard.vcAssCardID && i.iAssID == assCard.iAssID);
                            if (igm == null)
                                throw new Exception("获取会员卡积分错误，请重试！");

                            tbFillFee fillFee = new tbFillFee();
                            fillFee.dtFillDate = DateTime.Now;
                            fillFee.iAssID = assCard.iAssID;
                            //fillFee.iFillSerial
                            fillFee.nBalance = ig.nBalance + iFillFee + iExtraFee;
                            fillFee.nFillFee = Convert.ToDecimal(iFillFee);
                            fillFee.nLastBalance = ig.nBalance;
                            fillFee.nPromFee = iExtraFee;
                            fillFee.vcAssCardID = assCard.vcAssCardID;// AssCardID;
                            fillFee.vcComments = txtComments.Text;
                            fillFee.vcOperID = GlobalParams.oper.vcOperID;
                            fillFee.vcOperType = "OT002";
                            amsContext.AddTotbFillFee(fillFee);
                            Helper.Save(amsContext);

                            
                            igm.nBalance = Convert.ToDecimal(fillFee.nBalance);

                            if (billInvoice == null)
                                throw new Exception("获取账单出错,请先打印回执");

                            tbBillInvoice billInvoicem = amsContext.tbBillInvoice.FirstOrDefault(b => b.iBillNo == billInvoice.iBillNo);
                            billInvoicem.vcLinkSerial = fillFee.iFillSerial;
                            billInvoicem.vcEffFlag = "1";

                            tbBusiLog busiLog = new tbBusiLog();
                            busiLog.dtOperDate = DateTime.Now;
                            busiLog.iAssID = assCard.iAssID;
                            busiLog.vcAssCardID = assCard.vcAssCardID;
                            busiLog.vcAssName = txtAssName.Text;
                            busiLog.vcOperName = GlobalParams.oper.vcOperName;
                            busiLog.vcOperID = GlobalParams.oper.vcOperID;
                            busiLog.vcOperType = "0T002";
                            busiLog.vcLinkSerial = fillFee.iFillSerial;
                            amsContext.AddTotbBusiLog(busiLog);

                            Helper.Save(amsContext);
                            trans.Commit();
                        }
                    }
                    txtAssName.Text = "";
                    txtAssCardID.Text = "";
                    txtComments.Text = "";
                    txtBalance.Text = "";
                    txtExtraFee.Text = "";
                    txtFillFee.Value = 1000;
                    txtSum.Text = "";

                    ass = null;
                    assCard = null;
                    ig = null;
                    fillProm = null;
                    billInvoice = null;

                    Helper.ShowInfo(this, "会员卡充值成功！");
                }

                
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #endregion

        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                if (ass == null || assCard == null||ig==null)
                    throw new Exception("请首先读取会员卡信息！");                
                int iFillFee = Convert.ToInt32(txtFillFee.Value);
                if (iFillFee < 1000)
                    throw new Exception("充值金额必须是大于1000元的整数");
                var fillProm = GlobalParams.FillProm.FirstOrDefault(f => f.iCelling > iFillFee && f.iFloor <= iFillFee);
                if (fillProm == null)
                    throw new Exception("获取优惠比率出错，请检查优惠比率配置是否正确！");
                int iExtraFee = iFillFee * fillProm.iRate / 100;

                txtExtraFee.Text = iExtraFee.ToString();
                txtSum.Text = Convert.ToString(iFillFee + iExtraFee);

                if (assCard.cCardState != ConstApp.CST_1)
                    throw new Exception("此会员卡不在正常在用状态，无法充值");
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

                        tbBillList billList1 = new tbBillList();
                        billList1.iBillNo = serialNo.iSerialNo;//lSerialNo;
                        billList1.iCount = 1;
                        billList1.iNO = 1;
                        billList1.nCharge = Convert.ToDecimal(iFillFee);
                        billList1.nPrice = Convert.ToDecimal(iFillFee);
                        billList1.nRate = 1;
                        billList1.vcGoodsName = "会员充值";
                        billList1.vcUnit = "";
                        amsContext.AddTotbBillList(billList1);

                        tbBillList billList2 = new tbBillList();
                        billList2.iBillNo = serialNo.iSerialNo;
                        billList2.iCount = 1;
                        billList2.iNO = 2;
                        billList2.nCharge = Convert.ToDecimal(iExtraFee);
                        billList2.nPrice = Convert.ToDecimal(iExtraFee);
                        billList2.nRate = 1;
                        billList2.vcGoodsName = "赠款";
                        billList2.vcUnit = "";
                        amsContext.AddTotbBillList(billList2);

                        billInvoice = new tbBillInvoice();
                        billInvoice.dCharge = ig.nBalance + iFillFee + iExtraFee;
                        billInvoice.dIgGet = 0;
                        billInvoice.dIgValue = ig.iIgValue;
                        billInvoice.dLastCharge = ig.nBalance;
                        billInvoice.dLastIg = ig.iIgValue;
                        billInvoice.dMealFee = 0;
                        billInvoice.dServiceFee = 0;
                        billInvoice.dSumFee = iFillFee + iExtraFee;
                        billInvoice.dtCreateDate = DateTime.Now;
                        billInvoice.dTotalFee = iFillFee;
                        billInvoice.dtPrintDate = DateTime.Now;
                        billInvoice.iBillNo = serialNo.iSerialNo;
                        billInvoice.vcAssCardID = assCard.vcAssCardID;
                        billInvoice.vcAssName = txtAssName.Text;
                        billInvoice.vcBillType = "BI002";
                        billInvoice.vcEffFlag = "0";
                        billInvoice.vcOperName = GlobalParams.oper.vcOperName;
                        billInvoice.vcOperID = GlobalParams.oper.vcOperID;
                        billInvoice.vcPrintFlag = "1";
                        amsContext.AddTotbBillInvoice(billInvoice);

                        Helper.Save(amsContext);
                        //打印 会员卡充值帐单-------------------
                        //打印帐单-----------------------------------
                        List<tbBillInvoice> invoice = new List<tbBillInvoice>();
                        invoice.Add(billInvoice);
                        List<tbBillList> list = new List<tbBillList>();
                        list.Add(billList1);
                        list.Add(billList2);
                        Helper.MyPrint(invoice, list);
                        trans.Commit();
                    }
                }
                Helper.ShowInfo(this, "打印回执成功");
            }
            catch (Exception ex)
            {
                billInvoice = null;
                ErrorLog.Write(this, ex);
            }
        }
        #endregion

        #region 金额
        private void txtFillFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {                    
                    int iFillFee = Convert.ToInt32(txtFillFee.Value);
                    var fillProm = GlobalParams.FillProm.FirstOrDefault(f => f.iCelling > iFillFee && f.iFloor <= iFillFee);
                    if (fillProm == null)
                        throw new Exception("获取优惠比率出错，请检查优惠比率配置是否正确！");
                    int iExtraFee = iFillFee * fillProm.iRate / 100;

                    txtExtraFee.Text = iExtraFee.ToString();
                    txtSum.Text = Convert.ToString(iFillFee + iExtraFee);                   
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #endregion

        #region 查看
        private void btnAssData_Click(object sender, EventArgs e)
        {
            try
            {
                if (ass == null)
                    throw new Exception("请首先读取会员卡信息");
                frmAss frmass = new frmAss();
                frmass.ass = ass;
                frmass.OperType = "DETAIL";
                frmass.MinimizeBox = false;
                frmass.MaximizeBox = false;
                frmass.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
    }
}
