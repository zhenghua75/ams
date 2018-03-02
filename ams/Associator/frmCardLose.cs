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
    public partial class frmCardLose : Form
    {        
        public tbAssociator ass { get; set; }
        public tbAssociatorCard assCard { get; set; }
        public tbIntegral ig { get; set; }
        private tbBillInvoice billInvoice=null;

        public frmCardLose()
        {
            InitializeComponent();
        }
        #region 关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            //假如已打印回执未充值给出提示不是否关闭窗体
            if (billInvoice != null)
            {
                if (MessageBox.Show(this, "挂失未完成，并已打印回执，是否要退出挂失操作？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //打印回执
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                if (ass == null || assCard == null || ig == null)
                    throw new Exception("请首先查询会员，并选择某个会员卡进行会员卡挂失操作");
                if (ass.vcAssState != ConstApp.AST_1)
                    throw new Exception("此会员未发卡无法挂失");
                if (assCard.cCardState != ConstApp.CST_1)
                    throw new Exception("此会员卡不在正常在用状态，无法挂失");
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

                        tbBillList billList = new tbBillList();
                        billList.iBillNo = serialNo.iSerialNo;
                        billList.iCount = 1;
                        billList.iNO = 1;
                        billList.nCharge = 0;
                        billList.nPrice = 0;
                        billList.nRate = 1;
                        billList.vcGoodsName = "会员卡挂失";
                        //billList.vcUnit = "";
                        amsContext.AddTotbBillList(billList);

                        billInvoice = new tbBillInvoice();
                        billInvoice.dCharge = 0;// ig.nBalance + iFillFee + iExtraFee;
                        billInvoice.dIgGet = 0;
                        billInvoice.dIgValue = ig.iIgValue;
                        billInvoice.dLastCharge = ig.nBalance;
                        billInvoice.dLastIg = ig.iIgValue;
                        billInvoice.dMealFee = 0;
                        billInvoice.dServiceFee = 0;
                        billInvoice.dSumFee = 0;// iFillFee + iExtraFee;
                        billInvoice.dtCreateDate = DateTime.Now;
                        billInvoice.dTotalFee = 0;// iFillFee;
                        billInvoice.dtPrintDate = DateTime.Now;
                        billInvoice.iBillNo = serialNo.iSerialNo;
                        billInvoice.vcAssCardID = assCard.vcAssCardID;
                        billInvoice.vcAssName = txtAssName.Text;
                        billInvoice.vcBillType = "BI004";
                        billInvoice.vcEffFlag = "0";
                        billInvoice.vcLinkSerial = null;
                        billInvoice.vcOperName = GlobalParams.oper.vcOperName;
                        billInvoice.vcOperID = GlobalParams.oper.vcOperID;
                        billInvoice.vcPrintFlag = "1";
                        //billInvoice.vcReserve = "";     
                        amsContext.AddTotbBillInvoice(billInvoice);

                        Helper.Save(amsContext);
                        //打印帐单-----------------------------------
                        List<tbBillInvoice> invoice = new List<tbBillInvoice>();
                        invoice.Add(billInvoice);
                        List<tbBillList> list = new List<tbBillList>();
                        list.Add(billList);                        
                        Helper.MyPrint(invoice, list);
                        trans.Commit();
                    }
                }

                Helper.ShowInfo(this, "打印回执成功，请输入会员卡密码，进行挂失操作");
            }
            catch (Exception ex)
            {
                billInvoice = null;
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
        #region 导入
        private void frmCardLose_Load(object sender, EventArgs e)
        {
            try
            {
                if (ass == null || assCard == null||ig==null)
                    throw new Exception("请首先查询会员，并选择某个会员卡进行会员卡挂失操作");
                txtCardID.Text = assCard.vcAssCardID;
                txtBalance.Text = ig.nBalance.ToString();
                txtAssName.Text = ass.vcAssName;
                txtAssNbr.Text = ass.vcAssNbr;
                txtCompanyName.Text = ass.vcCompanyName;
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
        #region 挂失
        private void btnCardLose_Click(object sender, EventArgs e)
        {
            //挂失
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                if (string.IsNullOrEmpty(txtPwd.Text))
                    throw new Exception("请首先输入会员卡密码");
                if (!assCard.vcAssPwd.Equals(txtPwd.Text))
                    throw new Exception("会员卡密码不正确");
                if (ass == null || assCard == null||ig==null)
                    throw new Exception("请首先查询会员，并选择某个会员卡进行会员卡挂失操作");
                if (billInvoice == null)
                    throw new Exception("请首先打印回执");
                DialogResult dr = MessageBox.Show(this, "请确认：\n是否要挂失卡号：" +assCard.vcAssCardID, "提示",MessageBoxButtons.YesNo);
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
                            tbBusiLog busiLog = new tbBusiLog();
                            busiLog.dtOperDate = DateTime.Now;
                            busiLog.iAssID = assCard.iAssID;
                            busiLog.vcAssCardID = assCard.vcAssCardID;
                            busiLog.vcAssName = txtAssName.Text;
                            busiLog.vcOperName = GlobalParams.oper.vcOperName;
                            busiLog.vcOperID = GlobalParams.oper.vcOperID;
                            busiLog.vcOperType = "0T002";
                            busiLog.vcLinkSerial = billInvoice.iBillNo;
                            amsContext.AddTotbBusiLog(busiLog);
                            Helper.Save(amsContext);

                            tbAssociatorCard assCardm = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.iAssID == assCard.iAssID && ac.vcAssCardID == assCard.vcAssCardID);
                            if (assCardm == null)
                                throw new Exception("未找到会员卡信息");
                            if (assCardm.cCardState != ConstApp.CST_1)
                                throw new Exception("会员卡不在正常使用状态，无法挂失");
                            assCardm.cCardState = "2";
                            assCardm.dtCardExpDate = DateTime.Now;
                            assCardm.vcOperID = GlobalParams.oper.vcOperID;
                            assCardm.dtOperDate = DateTime.Now;

                            if (billInvoice == null)
                                throw new Exception("请首先打印回执");
                            tbBillInvoice billInvoicem = amsContext.tbBillInvoice.FirstOrDefault(b => b.iBillNo == billInvoice.iBillNo);
                            billInvoicem.vcEffFlag = "1";
                            billInvoicem.vcLinkSerial = busiLog.iSerial;

                            Helper.Save(amsContext);
                            trans.Commit();
                        }
                    }

                    billInvoice = null;
                    ass = null;
                    assCard = null;
                    ig = null;
                    MessageBox.Show(this, "会员卡挂失成功", "提示");
                }               
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
        #region 密码
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
                }
            }
            catch (Exception ex)
            {
                //异常操作
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
    }
}
