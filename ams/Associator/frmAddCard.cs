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
    public partial class frmAddCard : Form
    {
        public frmAddCard()
        {
            InitializeComponent();
        }
        public tbAssociator ass { get; set; }
        private void btnAddCard_Click(object sender, EventArgs e)
        {
            DbConnection conn = null;
            DbTransaction trans = null;
            int ret = 0;
            try
            {
                if (ass == null)
                    throw new Exception("请首先查询并选择会员进行发卡！");
                if (txtCardNo.Text.Trim().Length < 5)
                    throw new Exception("请输入卡号，卡号必须5位");
                using (AMSEntities amsContext = new AMSEntities())
                {
                    
                    conn = amsContext.Connection;
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    int i = amsContext.tbAssociatorCard.Count(ac => ac.vcAssCardID == txtCardNo.Text);
                    if (i > 0)
                        throw new Exception(txtCardNo.Text + "此会员卡号已在用");
                    using (trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        //事务数据操作或其它一致性操作
                        tbAssociator assm = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == ass.iAssID);
                        if (assm == null)
                            throw new Exception("未找到发卡会员");
                        tbAssociatorCard assCard = new tbAssociatorCard();
                        assCard.cCardState = "1";
                        assCard.dtCardEffDate = DateTime.Now;
                        //assCard.dtCardExpDate = DateTime.Now;
                        assCard.dtCardPutDate = DateTime.Now;
                        assCard.iAssID = ass.iAssID;
                        assCard.vcAssCardID = txtCardNo.Text;
                        assCard.vcAssPwd = "123456";
                        assCard.vcCardLevel = "*";
                        assCard.vcOperID = GlobalParams.oper.vcOperID;
                        assCard.dtOperDate = DateTime.Now;
                        amsContext.AddTotbAssociatorCard(assCard);

                        tbIntegral ig = new tbIntegral();
                        ig.iAssID = Convert.ToInt32(ass.iAssID);
                        ig.iIgValue = 0;
                        ig.nBalance = 0;
                        ig.vcAssCardID = txtCardNo.Text;
                        amsContext.AddTotbIntegral(ig);

                        assm.vcAssState = "1";
                        assm.vcOperID = GlobalParams.oper.vcOperID;
                        assm.dtOperDate = DateTime.Now;

                        tbBusiLog busiLog = new tbBusiLog();
                        busiLog.iAssID = ass.iAssID;
                        busiLog.dtOperDate = DateTime.Now;
                        busiLog.iAssID = ass.iAssID;
                        busiLog.vcAssCardID = txtCardNo.Text;
                        busiLog.vcAssName = ass.vcAssName;
                        busiLog.vcLinkSerial = null;
                        busiLog.vcOperName = GlobalParams.oper.vcOperName;
                        busiLog.vcOperID = GlobalParams.oper.vcOperID;
                        busiLog.vcOperType = "OT001";
                        amsContext.AddTotbBusiLog(busiLog);

                        Helper.Save(amsContext);
                        //开始卡操作----------------------------------
                        ret = Helper.PutCard(txtCardNo.Text);
                        if (ret != 0)
                            throw new Exception("卡操作异常");
                        trans.Commit();             
                    }
                    DialogResult dr = MessageBox.Show(this, "发卡成功,是否继续发卡？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        ass = amsContext.tbAssociator.FirstOrDefault(a => a.vcAssState == ConstApp.AST_0);
                        if (ass == null)                        
                            throw new Exception("无未发卡会员，请退出发卡界面");
                    }
                }
                

            }
            catch (Exception ex)
            {
                //异常操作
                ErrorLog.Write(this, ex);
                Helper.ShowError(this, Helper.PutCardError(ret));
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
