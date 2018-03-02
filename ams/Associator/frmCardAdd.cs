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
    public partial class frmCardAdd : Form
    {
        public string AssCardID { get; set; }
        public long AssID { get; set; }

        private tbAssociator ass;
        private tbAssociatorCard assCard;
        private tbIntegral ig;
        private decimal dBusiFee;
        private tbBillInvoice billInvoice;
        public frmCardAdd()
        {
            InitializeComponent();
        }
        #region 关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            //假如已打印回执未充值给出提示不是否关闭窗体
            if (billInvoice != null)
            {
                if (MessageBox.Show(this, "补卡未完成，并已打印回执，是否要退出补卡操作？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {

            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                if (ass == null || assCard == null || ig == null)
                    throw new Exception("请首先查询会员，并选择某个会员卡进行会员卡挂失操作");
                using (AMSEntities amsContext = new AMSEntities())
                {
                    conn = amsContext.Connection;
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    using (trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        tbBillSerialNo serialNo = new tbBillSerialNo();
                        serialNo.vcFill = "0";
                        amsContext.AddTotbBillSerialNo(serialNo);
                        Helper.Save(amsContext);

                        tbBillList list1 = new tbBillList();
                        list1.iBillNo = serialNo.iSerialNo;
                        list1.iCount = 1;
                        list1.iNO = 1;
                        list1.nCharge = dBusiFee;
                        list1.nPrice = dBusiFee;
                        list1.nRate = 0;
                        list1.vcGoodsName = "补发卡工本费";
                        list1.vcUnit = "";
                        amsContext.AddTotbBillList(list1);

                        billInvoice = new tbBillInvoice();
                        billInvoice.dCharge = ig.nBalance;// +iFillFee + iExtraFee;
                        billInvoice.dIgGet = 0;
                        billInvoice.dIgValue = ig.iIgValue;
                        billInvoice.dLastCharge = ig.nBalance;
                        billInvoice.dLastIg = ig.iIgValue;
                        billInvoice.dMealFee = 0;
                        billInvoice.dServiceFee = 0;
                        billInvoice.dSumFee = dBusiFee;
                        billInvoice.dtCreateDate = DateTime.Now;
                        billInvoice.dTotalFee = dBusiFee;
                        billInvoice.dtPrintDate = DateTime.Now;
                        billInvoice.iBillNo = serialNo.iSerialNo;//lSerialNo;
                        billInvoice.vcAssCardID = assCard.vcAssCardID;
                        billInvoice.vcAssName = ass.vcAssName;//txtAssName.Text;
                        billInvoice.vcBillType = "BI005";
                        billInvoice.vcEffFlag = "0";
                        //billInvoice.vcLinkSerial = "";
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
                        list.Add(list1);
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
            finally
            {
                if(conn!=null)
                    conn.Close();
            }

        }
        #endregion
        #region 导入
        private void frmCardAdd_Load(object sender, EventArgs e)
        {
            try
            {
                cmbAssLevel.DataSource = GlobalParams.CommCode.Where(cc => cc.vcCommSign == ConstApp.AST).ToList();
                cmbAssLevel.DisplayMember = "vcCommName";
                cmbAssLevel.ValueMember = "vcCommCode";

                cmbCardState.DataSource = GlobalParams.CommCode.Where(cc => cc.vcCommSign == ConstApp.CST).ToList();
                cmbCardState.DisplayMember = "vcCommName";
                cmbCardState.ValueMember = "vcCommCode";

                List<tbCommCode> agf = GlobalParams.CommCode.Where(cc => cc.vcCommSign == ConstApp.AGF).ToList<tbCommCode>();
                if (agf.Count < 1)
                    throw new Exception("补发卡工本费参数有误，请检查参数设置！");
                tbCommCode ccagf = agf[0];
                if (!Helper.IsNum(ccagf.vcCommCode.Trim()))
                    throw new Exception("补发卡工本费参数有误，请检查参数设置！");
                dBusiFee = Convert.ToDecimal(ccagf.vcCommCode.Trim());
                if (string.IsNullOrEmpty(AssCardID))
                    throw new Exception("请首先查询选择可进行补发卡的会员卡信息");
                lblAgf.Text = dBusiFee.ToString();
                using (AMSEntities amsContext = new AMSEntities())
                {
                   
                    assCard = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.vcAssCardID == AssCardID && ac.iAssID == AssID);
                    if (assCard == null)
                        throw new Exception("未找到相应的会员卡信息，请重试");
                    ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == AssID);
                    if (ass == null)
                        throw new Exception("未找到相应的会员信息，请重试");
                    ig = amsContext.tbIntegral.FirstOrDefault(i => i.iAssID == AssID && i.vcAssCardID == AssCardID);
                    txtAssCardID.Text = assCard.vcAssCardID;
                    txtAssName.Text = ass.vcAssName;
                    if (ig != null)
                    {
                        txtBalance.Text = ig.nBalance.ToString();
                        txtIg.Text = ig.iIgValue.ToString();
                    }
                    cmbAssLevel.SelectedValue = ass.vcAssState;
                    cmbCardState.SelectedValue = assCard.cCardState;

                    dtpPutCardDate.Value = Convert.ToDateTime(assCard.dtCardPutDate);
                    dtpExpDate.Value = Convert.ToDateTime(assCard.dtCardExpDate);
                }
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = false;
                btnCardAdd.Enabled = false;
                lblCardAdd.Visible = false;
                txtCardAdd.Visible = false;
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
        #region 补卡
        private void btnCardAdd_Click(object sender, EventArgs e)
        {
            DbConnection conn = null;
            DbTransaction trans = null;
            int ret = 0;
            try
            {
                if (string.IsNullOrEmpty(txtCardAdd.Text))
                    throw new Exception("请输入补卡卡号");
                if(txtCardAdd.Text.Length <5)
                    throw new Exception("会员卡卡号必须是5位");
                if (ass == null || assCard == null || ig == null)
                    throw new Exception("请首先查询会员，并选择某个会员卡进行会员卡挂失操作");
                if (billInvoice == null)
                    throw new Exception("请首先打印回执");
                using (AMSEntities amsContext = new AMSEntities())
                {
                    conn = amsContext.Connection;
                    conn.Open();
                    int count = amsContext.tbAssociatorCard.Count(ac => ac.vcAssCardID == txtCardAdd.Text);
                    if (count > 0)
                        throw new Exception(txtCardAdd.Text+"此会员卡号已在用");
                    //起事务
                    using (trans = conn.BeginTransaction())
                    {

                        //更新老卡
                        tbAssociatorCard oldAssCard = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.vcAssCardID == assCard.vcAssCardID && ac.iAssID == assCard.iAssID);
                        if (oldAssCard == null)
                            throw new Exception("未找到老卡信息");
                        oldAssCard.cCardState = ConstApp.CST_3;
                        oldAssCard.vcOperID = GlobalParams.oper.vcOperID;
                        oldAssCard.dtOperDate = DateTime.Now;
                        oldAssCard.vcComments = "新卡号：" + txtCardAdd.Text;//txtAssCardID.Text;

                        //插入新卡
                        tbAssociatorCard newAssCard = new tbAssociatorCard();
                        newAssCard.cCardState = "1";
                        newAssCard.dtCardEffDate = DateTime.Now;
                        newAssCard.dtCardPutDate = DateTime.Now;
                        newAssCard.iAssID = assCard.iAssID;
                        newAssCard.vcAssCardID = txtCardAdd.Text;//txtAssCardID.Text;
                        newAssCard.vcAssPwd = "123456";
                        newAssCard.vcCardLevel = assCard.vcCardLevel;
                        newAssCard.vcOperID = GlobalParams.oper.vcOperID;
                        newAssCard.dtOperDate = DateTime.Now;
                        newAssCard.vcComments = "老卡号：" + oldAssCard.vcAssCardID;
                        amsContext.AddTotbAssociatorCard(newAssCard);

                        ////插入一条充值记录，充值金额为老卡的余额，vcOperType=‘OT003’
                        //tbFillFee fillFee = new tbFillFee();
                        //fillFee.dtFillDate = DateTime.Now;
                        //fillFee.iAssID = assCard.iAssID;//assCard.iAssID;//Convert.ToInt64(AssID);
                        ////fillFee.iFillSerial
                        //fillFee.nBalance = ig.nBalance;// +iFillFee + iExtraFee;
                        //fillFee.nFillFee = ig.nBalance;//Convert.ToDecimal(iFillFee);
                        //fillFee.nLastBalance = 0;//ig.nBalance;
                        //fillFee.nPromFee = 0;// iExtraFee;
                        //fillFee.vcAssCardID = txtAssCardID.Text;// AssCardID;
                        ////fillFee.vcComments = txtComments.Text;
                        //fillFee.vcOperID = GlobalParams.oper.vcOperID;
                        //fillFee.vcOperType = "OT003";
                        //amsContext.AddTotbFillFee(fillFee);


                        

                        tbIntegral newIg = new tbIntegral();
                        newIg.iAssID = Convert.ToInt32(newAssCard.iAssID);
                        newIg.iIgValue = ig.iIgValue;
                        newIg.nBalance = ig.nBalance;
                        newIg.vcAssCardID = newAssCard.vcAssCardID;
                        amsContext.AddTotbIntegral(newIg);

                        tbIntegral oldIg = amsContext.tbIntegral.FirstOrDefault(i => i.vcAssCardID == ig.vcAssCardID && i.iAssID == ig.iAssID);
                        if (oldIg == null)
                            throw new Exception("未找到老卡积分信息");
                        oldIg.nBalance = 0;
                        oldIg.iIgValue = 0;

                        tbBusiFee busiFee = new tbBusiFee();
                        busiFee.dtOperDate = DateTime.Now;
                        busiFee.iAssID = newAssCard.iAssID;
                        //busiFee.iSerial = 
                        busiFee.nFee = dBusiFee;
                        busiFee.vcAssCardID = newAssCard.vcAssCardID;
                        busiFee.vcFeeCode = "GB001";
                        busiFee.vcOperID = GlobalParams.oper.vcOperID;
                        busiFee.vcSvcCode = "OT003";
                        amsContext.AddTotbBusiFee(busiFee);

                        Helper.Save(amsContext);//保存获取流水号
                        tbBillInvoice updateBillInvoice = amsContext.tbBillInvoice.FirstOrDefault(b => b.iBillNo == billInvoice.iBillNo);
                        if (updateBillInvoice == null)
                            throw new Exception("获取帐单出错");
                        updateBillInvoice.vcEffFlag = "1";
                        updateBillInvoice.vcLinkSerial = busiFee.iSerial;//fillFee.iFillSerial;

                        tbBusiLog busiLog = new tbBusiLog();
                        busiLog.iAssID = ass.iAssID;
                        busiLog.dtOperDate = DateTime.Now;
                        busiLog.vcAssCardID = assCard.vcAssCardID;
                        busiLog.vcAssName = ass.vcAssName;
                        busiLog.vcOperName = GlobalParams.oper.vcOperName;
                        busiLog.vcOperID = GlobalParams.oper.vcOperID;
                        busiLog.vcOperType = "0T003";
                        busiLog.vcLinkSerial = busiFee.iSerial; //fillFee.iFillSerial;
                        amsContext.AddTotbBusiLog(busiLog);

                        Helper.Save(amsContext);

                        //卡操作--------------------------------------------------------------------------
                        ret = Helper.PutCard(txtCardAdd.Text);
                        if (ret != 0)
                            throw new Exception("卡操作异常");
                        trans.Commit();
                    }
                }
                ass = null;
                assCard = null;
                ig = null;
                billInvoice = null;

                Helper.ShowInfo(this, "补发卡成功");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
                Helper.ShowError(this, Helper.PutCardError(ret));
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        #endregion
    }
}
