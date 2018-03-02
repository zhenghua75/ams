using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;
using System.Data.Common;
using ams.Common;

namespace ams.Associator
{
    public partial class frmAssCons : Form
    {
        public frmAssCons()
        {
            InitializeComponent();
        }


        private void frmAssCons_Load(object sender, EventArgs e)
        {
            try
            {
                cmbGoodsName.DataSource = GlobalParams.Goods;
                cmbGoodsName.DisplayMember = "vcGoodsName";
                cmbGoodsName.ValueMember = "vcGoodsCode";

                cmbGoodsName.Enabled = false;
                txtRate.Enabled = false;
                txtPrice.Enabled = false;
                txtCount.Enabled = false;
                txtComments.Enabled = false;
                txtPrompt.Enabled = false;
                btnRollback.Enabled = false;
                btnOk.Enabled = false;
                btnAdd.Enabled = false;
                btnPrint.Enabled = false;
                txtTolCharge.Text = "0";
                txtTolCount.Text = "0";

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
                        throw new Exception("该会员卡处于非正常在用状态，不能消费！");
                    tbIntegral intg1 = amsContext.tbIntegral.FirstOrDefault(ig => ig.iAssID == asscard1.iAssID && ig.vcAssCardID == asscard1.vcAssCardID);
                    if (intg1 == null)
                        throw new Exception("获取会员卡余额和积分错误，请重试！");
                    tbAssociator ass1 = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == asscard1.iAssID);
                    if (ass1 == null)
                        throw new Exception("会员基本信息不存在，请重试！");
                    string strGoodsCode = cmbGoodsName.SelectedValue.ToString();
                    tbGoodsRate goodrrate1 = amsContext.tbGoodsRate.FirstOrDefault(gr => gr.vcGoodsCode == strGoodsCode && gr.vcAssLevel == ass1.vcAssLevel);
                    tbOperLevel operlev = amsContext.tbOperLevel.FirstOrDefault(g => g.vcOperLevel == GlobalParams.oper.vcOperLevel);
                    if (goodrrate1 == null)
                    {
                        if (operlev == null)
                        {
                            txtRate.Text = "100";
                            txtPrompt.Text = "折扣参数不全";
                        }
                        else
                        {
                            txtRate.Text = operlev.iRateFloor.ToString();
                            txtPrompt.Text = "";
                        }
                    }
                    else
                    {
                        if (operlev == null)
                        {
                            txtRate.Text = goodrrate1.nGoodsRate.ToString();
                            txtPrompt.Text = goodrrate1.vcComments;
                        }
                        else if (operlev.iRateFloor < goodrrate1.nGoodsRate)
                        {
                            txtRate.Text =  operlev.iRateFloor.ToString();
                            txtPrompt.Text = "";
                        }
                        else
                        {
                            txtRate.Text = goodrrate1.nGoodsRate.ToString();
                            txtPrompt.Text = goodrrate1.vcComments;
                        }
                    }
                    txtCardID.Text = asscard1.vcAssCardID;
                    txtAssName.Text = ass1.vcAssName;
                    txtAssLevelCode.Text = ass1.vcAssLevel;
                    GlobalParams gp = new GlobalParams(amsContext);
                    tbCommCode alcomm = GlobalParams.CommCode.FirstOrDefault(cc => cc.vcCommSign == "AL" && cc.vcCommCode == ass1.vcAssLevel);
                    txtAssLevel.Text = alcomm.vcCommName;
                    txtCurCharge.Text = intg1.nBalance.ToString();
                    txtCurIG.Text = intg1.iIgValue.ToString();
                    tbGoods goods1 = GlobalParams.Goods.FirstOrDefault(gd => gd.vcGoodsCode == strGoodsCode);
                    if (goods1 == null)
                        txtPrice.Text = "0";
                    else
                        txtPrice.Text = goods1.nGoodsPrice.Value.ToString();
                    txtPrice.Enabled = true;
                    txtRate.Enabled = true;
                    txtCount.Text = "1";
                    txtCount.Enabled = true;
                    txtRate.Focus();
                    cmbGoodsName.Enabled = true;
                    txtComments.Enabled = true;
                    btnRollback.Enabled = true;
                    btnAdd.Enabled = true;
                    btnPrint.Enabled = true;
                    btnOk.Enabled = false;
                    btnRead.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "刷卡错误");
                Helper.ShowError(this, Helper.ReadCardError(ret));
            }
        }

        private void cmbGoodsName_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (AMSEntities amsContext = new AMSEntities())
            {
                try
                {
                    if (txtCardID.Text.Trim()!=""&&txtAssLevelCode.Text.Trim() == "")
                        throw new Exception("会员信息有误，请重新刷卡！");
                    txtRate.Text = "0";
                    txtPrice.Text = "0";
                    txtPrompt.Text = "";
                    string strGoodsCode = cmbGoodsName.SelectedValue.ToString();
                    tbGoods goods1 = GlobalParams.Goods.FirstOrDefault(gd => gd.vcGoodsCode == strGoodsCode);
                    if (goods1 == null)
                        txtPrice.Text = "0";
                    else
                        txtPrice.Text = goods1.nGoodsPrice.Value.ToString();

                    tbGoodsRate goodrrate1 = amsContext.tbGoodsRate.FirstOrDefault(gr => gr.vcGoodsCode == strGoodsCode && gr.vcAssLevel == txtAssLevelCode.Text.Trim());
                    tbOperLevel operlev = amsContext.tbOperLevel.FirstOrDefault(g => g.vcOperLevel==GlobalParams.oper.vcOperLevel);
                    if (goodrrate1 == null)
                    {
                        if (operlev == null)
                        {
                            txtRate.Text = "100";
                            txtPrompt.Text = "折扣参数不全";
                        }
                        else
                        {
                            txtRate.Text = operlev.iRateFloor.ToString();
                            txtPrompt.Text = "";
                        }
                    }
                    else
                    {
                        if (operlev == null)
                        {
                            txtRate.Text = goodrrate1.nGoodsRate.ToString();
                            txtPrompt.Text = goodrrate1.vcComments;
                        }
                        else if (operlev.iRateFloor < goodrrate1.nGoodsRate)
                        {
                            txtRate.Text =  operlev.iRateFloor.ToString();
                            txtPrompt.Text = "";
                        }
                        else
                        {
                            txtRate.Text = goodrrate1.nGoodsRate.ToString();
                            txtPrompt.Text = goodrrate1.vcComments;
                        }
                    }
                    txtRate.Focus();
                }
                catch (Exception ex)
                {
                    ErrorLog.Write(this, ex, "获取消费项目信息异常");
                    this.Close();
                }
            }
        }

        private void txtRate_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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
                    string strRate = txtRate.Text.Trim();
                    if (strRate == "")
                        throw new Exception("折扣不能为空！");
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        string strGoodsCode = cmbGoodsName.SelectedValue.ToString();
                        int ratefloor = 0;
                        tbGoodsRate goodrrate1 = amsContext.tbGoodsRate.FirstOrDefault(gr => gr.vcGoodsCode == strGoodsCode && gr.vcAssLevel == txtAssLevelCode.Text.Trim());
                        tbOperLevel operlev = amsContext.tbOperLevel.FirstOrDefault(g => g.vcOperLevel == GlobalParams.oper.vcOperLevel);
                        if (goodrrate1 == null)
                        {
                            if (operlev == null)
                            {
                                ratefloor = 100;
                            }
                            else
                            {
                                ratefloor = operlev.iRateFloor.Value;
                            }
                        }
                        else
                        {
                            if (operlev == null)
                            {
                                ratefloor = goodrrate1.nGoodsRate;
                            }
                            else if (operlev.iRateFloor < goodrrate1.nGoodsRate)
                            {
                                ratefloor =  operlev.iRateFloor.Value;
                            }
                            else
                            {
                                ratefloor = goodrrate1.nGoodsRate;
                            }
                        }

                        if (int.Parse(strRate) < ratefloor)
                        {
                            txtRate.Text = ratefloor.ToString();
                            throw new Exception("你输入的折扣已经超出了最低下限！");
                        }
                        if (int.Parse(strRate) >= 1000)
                        {
                            txtRate.Text = ratefloor.ToString();
                            throw new Exception("折扣不能超出1000%！");
                        }

                        txtPrice.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "校验错误");
                txtRate.Focus();
            }
        }

        private void txtPrice_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 13)
                {
                    if (e.KeyChar == 8 || e.KeyChar == 46)
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
                    string strPrice = txtPrice.Text.Trim();
                    if (strPrice == "")
                        throw new Exception("单价不能为空！");
                    
                    txtCount.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "校验错误");
                txtPrice.Focus();
            }
        }

        private void txtCount_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            string strFlag = "";
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
                    string strRate = txtRate.Text.Trim();
                    if (strRate == "")
                    {
                        strFlag = "1";
                        throw new Exception("折扣不能为空！");
                    }
                    decimal deRate = decimal.Parse(strRate);

                    string strPrice = txtPrice.Text.Trim();
                    if (strPrice == "")
                    {
                        strFlag = "2";
                        throw new Exception("单价不能为空！");
                    }
                    decimal dePrice = decimal.Parse(strPrice);

                    string strCount = txtCount.Text.Trim();
                    if (strCount == "")
                    {
                        strFlag = "3";
                        throw new Exception("数量不能为空！");
                    }
                    decimal deCount = decimal.Parse(strCount);

                    string strCurCharge = txtCurCharge.Text.Trim();
                    decimal deCurCharge=0;
                    if (strCurCharge == "")
                    {
                        throw new Exception("会员当前余额有误，请重新刷卡！");
                    }
                    else
                    {
                        deCurCharge = decimal.Parse(strCurCharge);
                    }

                    decimal deTolCharge = decimal.Parse(txtTolCharge.Text.Trim());

                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        string strGoodsCode = cmbGoodsName.SelectedValue.ToString();
                        int ratefloor = 0;
                        tbGoodsRate goodrrate1 = amsContext.tbGoodsRate.FirstOrDefault(gr => gr.vcGoodsCode == strGoodsCode && gr.vcAssLevel == txtAssLevelCode.Text.Trim());
                        tbOperLevel operlev = amsContext.tbOperLevel.FirstOrDefault(g => g.vcOperLevel == GlobalParams.oper.vcOperLevel);
                        if (goodrrate1 == null)
                        {
                            if (operlev == null)
                            {
                                ratefloor = 100;
                            }
                            else
                            {
                                ratefloor = operlev.iRateFloor.Value;
                            }
                        }
                        else
                        {
                            if (operlev == null)
                            {
                                ratefloor = goodrrate1.nGoodsRate;
                            }
                            else if (operlev.iRateFloor < goodrrate1.nGoodsRate)
                            {
                                ratefloor =  operlev.iRateFloor.Value;
                            }
                            else
                            {
                                ratefloor = goodrrate1.nGoodsRate;
                            }
                        }

                        if (int.Parse(strRate) < ratefloor)
                        {
                            txtRate.Text = ratefloor.ToString();
                            strFlag = "1";
                            throw new Exception("你输入的折扣已经超出了最低下限！");
                        }
                        if (int.Parse(strRate) >= 1000)
                        {
                            txtRate.Text = ratefloor.ToString();
                            strFlag = "1";
                            throw new Exception("折扣不能超出1000%！");
                        }

                        if (deCount == 0)
                        {
                            strFlag = "3";
                            throw new Exception("数量必须大于0，请重新输入！");
                        }
                        if (deCurCharge == 0 || deCurCharge < (deTolCharge + dePrice * (deRate / 100) * deCount))
                            throw new Exception("当前余额不足于此笔消费，请充值！");

                        txtComments.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "校验错误");
                switch (strFlag)
                {
                    case "1":
                        txtRate.Focus();
                        break;
                    case "2":
                        txtPrice.Focus();
                        break;
                    case "3":
                        txtCount.Focus();
                        break;
                    default:
                        txtCount.Focus();
                        break;
                }
            }
        }

        private void txtComments_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            string strFlag = "";
            try
            {
                if (e.KeyChar == 13)
                {
                    string strRate = txtRate.Text.Trim();
                    if (strRate == "")
                    {
                        strFlag = "1";
                        throw new Exception("折扣不能为空！");
                    }
                    decimal deRate = decimal.Parse(strRate);

                    string strPrice = txtPrice.Text.Trim();
                    if (strPrice == "")
                    {
                        strFlag = "2";
                        throw new Exception("单价不能为空！");
                    }
                    decimal dePrice = decimal.Parse(strPrice);

                    string strCount = txtCount.Text.Trim();
                    if (strCount == "")
                    {
                        strFlag = "3";
                        throw new Exception("数量不能为空！");
                    }
                    decimal deCount = decimal.Parse(strCount);

                    string strCurCharge = txtCurCharge.Text.Trim();
                    decimal deCurCharge = 0;
                    if (strCurCharge == "")
                    {
                        throw new Exception("会员当前余额有误，请重新刷卡！");
                    }
                    else
                    {
                        deCurCharge = decimal.Parse(strCurCharge);
                    }

                    decimal deTolCharge = decimal.Parse(txtTolCharge.Text.Trim());

                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        string strGoodsCode = cmbGoodsName.SelectedValue.ToString();
                        int ratefloor = 0;
                        tbGoodsRate goodrrate1 = amsContext.tbGoodsRate.FirstOrDefault(gr => gr.vcGoodsCode == strGoodsCode && gr.vcAssLevel == txtAssLevelCode.Text.Trim());
                        tbOperLevel operlev = amsContext.tbOperLevel.FirstOrDefault(g => g.vcOperLevel == GlobalParams.oper.vcOperLevel);
                        if (goodrrate1 == null)
                        {
                            if (operlev == null)
                            {
                                ratefloor = 100;
                            }
                            else
                            {
                                ratefloor = operlev.iRateFloor.Value;
                            }
                        }
                        else
                        {
                            if (operlev == null)
                            {
                                ratefloor = goodrrate1.nGoodsRate;
                            }
                            else if (operlev.iRateFloor < goodrrate1.nGoodsRate)
                            {
                                ratefloor = operlev.iRateFloor.Value;
                            }
                            else
                            {
                                ratefloor = goodrrate1.nGoodsRate;
                            }
                        }

                        if (int.Parse(strRate) < ratefloor)
                        {
                            txtRate.Text = ratefloor.ToString();
                            strFlag = "1";
                            throw new Exception("你输入的折扣已经超出了最低下限！");
                        }
                        if (int.Parse(strRate) >= 1000)
                        {
                            txtRate.Text = ratefloor.ToString();
                            strFlag = "1";
                            throw new Exception("折扣不能超出1000%！");
                        }
                        if (deCount == 0)
                        {
                            strFlag = "3";
                            throw new Exception("数量必须大于0，请重新输入！");
                        }
                        decimal deOneSum=Math.Round((dePrice * (deRate / 100) * deCount),2);
                        if (deCurCharge == 0 || deCurCharge < (deTolCharge + deOneSum))
                            throw new Exception("当前余额不足于此笔消费，请充值！");
                        DataTable dtConsList = (DataTable)dgvConsList.DataSource;
                        if (dtConsList.Rows.Contains(new string[]{cmbGoodsName.Text.Trim(),strPrice,strRate}))
                        {
                            DataRow dr = dtConsList.Rows.Find(new string[] { cmbGoodsName.Text.Trim(), strPrice, strRate });
                            dr["数量"]=(decimal.Parse(dr["数量"].ToString())+deCount).ToString();
                            dr["单项合计"] = (decimal.Parse(dr["单项合计"].ToString()) + deOneSum).ToString();
                        }
                        else
                        {
                            DataRow dr = dtConsList.NewRow();
                            dr["消费项名称"] = cmbGoodsName.Text.Trim();
                            dr["单价"] = strPrice;
                            dr["折扣"] = strRate;
                            dr["数量"] = strCount;
                            dr["单项合计"] = deOneSum.ToString();
                            dr["备注"] = txtComments.Text.Trim();
                            dtConsList.Rows.Add(dr);
                        }
                        decimal deTolCountNew = 0;
                        decimal deTolChargeNew = 0;
                        foreach (DataRow drtmp in dtConsList.Rows)
                        {
                            deTolCountNew += decimal.Parse(drtmp["数量"].ToString());
                            deTolChargeNew += Math.Round(decimal.Parse(drtmp["单项合计"].ToString()),2);
                        }
                        txtTolCharge.Text = deTolChargeNew.ToString();
                        txtTolCount.Text = deTolCountNew.ToString();
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "校验错误");
                switch (strFlag)
                {
                    case "1":
                        txtRate.Focus();
                        break;
                    case "2":
                        txtPrice.Focus();
                        break;
                    case "3":
                        txtCount.Focus();
                        break;
                    default:
                        txtComments.Focus();
                        break;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strFlag = "";
            try
            {
                string strRate = txtRate.Text.Trim();
                if (strRate == "")
                {
                    strFlag = "1";
                    throw new Exception("折扣不能为空！");
                }
                decimal deRate = decimal.Parse(strRate);

                string strPrice = txtPrice.Text.Trim();
                if (strPrice == "")
                {
                    strFlag = "2";
                    throw new Exception("单价不能为空！");
                }
                decimal dePrice = decimal.Parse(strPrice);

                string strCount = txtCount.Text.Trim();
                if (strCount == "")
                {
                    strFlag = "3";
                    throw new Exception("数量不能为空！");
                }
                decimal deCount = decimal.Parse(strCount);

                string strCurCharge = txtCurCharge.Text.Trim();
                decimal deCurCharge = 0;
                if (strCurCharge == "")
                {
                    throw new Exception("会员当前余额有误，请重新刷卡！");
                }
                else
                {
                    deCurCharge = decimal.Parse(strCurCharge);
                }

                decimal deTolCharge = decimal.Parse(txtTolCharge.Text.Trim());

                using (AMSEntities amsContext = new AMSEntities())
                {
                    string strGoodsCode = cmbGoodsName.SelectedValue.ToString();
                    int ratefloor = 0;
                    tbGoodsRate goodrrate1 = amsContext.tbGoodsRate.FirstOrDefault(gr => gr.vcGoodsCode == strGoodsCode && gr.vcAssLevel == txtAssLevelCode.Text.Trim());
                    tbOperLevel operlev = amsContext.tbOperLevel.FirstOrDefault(g => g.vcOperLevel == GlobalParams.oper.vcOperLevel);
                    if (goodrrate1 == null)
                    {
                        if (operlev == null)
                        {
                            ratefloor = 100;
                        }
                        else
                        {
                            ratefloor = operlev.iRateFloor.Value;
                        }
                    }
                    else
                    {
                        if (operlev == null)
                        {
                            ratefloor = goodrrate1.nGoodsRate;
                        }
                        else if (operlev.iRateFloor < goodrrate1.nGoodsRate)
                        {
                            ratefloor = operlev.iRateFloor.Value;
                        }
                        else
                        {
                            ratefloor = goodrrate1.nGoodsRate;
                        }
                    }

                    if (int.Parse(strRate) < ratefloor)
                    {
                        txtRate.Text = ratefloor.ToString();
                        strFlag = "1";
                        throw new Exception("你输入的折扣已经超出了最低下限！");
                    }
                    if (int.Parse(strRate) >= 1000)
                    {
                        txtRate.Text = ratefloor.ToString();
                        strFlag = "1";
                        throw new Exception("折扣不能超出1000%！");
                    }
                    if (deCount == 0)
                    {
                        strFlag = "3";
                        throw new Exception("数量必须大于0，请重新输入！");
                    }
                    decimal deOneSum = Math.Round((dePrice * (deRate / 100) * deCount),2);
                    if (deCurCharge == 0 || deCurCharge < (deTolCharge + deOneSum))
                        throw new Exception("当前余额不足于此笔消费，请充值！");
                    DataTable dtConsList = (DataTable)dgvConsList.DataSource;
                    if (dtConsList.Rows.Contains(new string[] { cmbGoodsName.Text.Trim(), strPrice, strRate }))
                    {
                        DataRow dr = dtConsList.Rows.Find(new string[] { cmbGoodsName.Text.Trim(), strPrice, strRate });
                        dr["数量"] = (decimal.Parse(dr["数量"].ToString()) + deCount).ToString();
                        dr["单项合计"] = (decimal.Parse(dr["单项合计"].ToString()) + deOneSum).ToString();
                    }
                    else
                    {
                        DataRow dr = dtConsList.NewRow();
                        dr["消费项名称"] = cmbGoodsName.Text.Trim();
                        dr["单价"] = strPrice;
                        dr["折扣"] = strRate;
                        dr["数量"] = strCount;
                        dr["单项合计"] = deOneSum.ToString();
                        dr["备注"] = txtComments.Text.Trim();
                        dtConsList.Rows.Add(dr);
                    }
                    decimal deTolCountNew = 0;
                    decimal deTolChargeNew = 0;
                    foreach (DataRow drtmp in dtConsList.Rows)
                    {
                        deTolCountNew += decimal.Parse(drtmp["数量"].ToString());
                        deTolChargeNew += Math.Round(decimal.Parse(drtmp["单项合计"].ToString()),2);
                    }
                    txtTolCharge.Text = deTolChargeNew.ToString();
                    txtTolCount.Text = deTolCountNew.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "校验错误");
                switch (strFlag)
                {
                    case "1":
                        txtRate.Focus();
                        break;
                    case "2":
                        txtPrice.Focus();
                        break;
                    case "3":
                        txtCount.Focus();
                        break;
                    default:
                        txtComments.Focus();
                        break;
                }
            }
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvConsList.CurrentRow.Index >= 0)
                {
                    string strGoodsName = dgvConsList[0, dgvConsList.CurrentRow.Index].Value.ToString();
                    string strPrice = dgvConsList[1, dgvConsList.CurrentRow.Index].Value.ToString();
                    string strRate = dgvConsList[2, dgvConsList.CurrentRow.Index].Value.ToString();

                    DataTable dtConsList = (DataTable)dgvConsList.DataSource;
                    DataRow dr = dtConsList.Rows.Find(new string[] { strGoodsName, strPrice, strRate });
                    dr["数量"] = (decimal.Parse(dr["数量"].ToString()) - 1).ToString();
                    dr["单项合计"] = (decimal.Parse(dr["单项合计"].ToString()) - Math.Round((decimal.Parse(strPrice) * (decimal.Parse(strRate)/100)),2)).ToString();
                    if (dr["数量"].ToString() == "0")
                    {
                        dtConsList.Rows.Remove(dr);
                    }
                    decimal deTolCountNew = 0;
                    decimal deTolChargeNew = 0;
                    foreach (DataRow drtmp in dtConsList.Rows)
                    {
                        deTolCountNew += decimal.Parse(drtmp["数量"].ToString());
                        deTolChargeNew += Math.Round(decimal.Parse(drtmp["单项合计"].ToString()),2);
                    }
                    txtTolCharge.Text = deTolChargeNew.ToString();
                    txtTolCount.Text = deTolCountNew.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "系统异常");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //需要将生成的发票流水写到txtBillNo文本框中

            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                if (dgvConsList.Rows.Count <= 0)
                    throw new Exception("没有任何消费记录");
                using (AMSEntities amsContext = new AMSEntities())
                {
                    string strAssCardID = txtCardID.Text.Trim();
                    decimal deTolCharge = Math.Round(decimal.Parse(txtTolCharge.Text.Trim()),2);
                    tbAssociatorCard asscard1 = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.vcAssCardID == strAssCardID);
                    tbIntegral intg1 = amsContext.tbIntegral.FirstOrDefault(ig => ig.iAssID == asscard1.iAssID && ig.vcAssCardID == asscard1.vcAssCardID);
                    if (intg1 == null)
                        throw new Exception("会员余额积分异常！");
                    if (intg1.nBalance < deTolCharge)
                    {
                        MessageBox.Show("当前余额不足，请重新刷卡后再试！", "系统异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbGoodsName.Enabled = false;
                        txtRate.Enabled = false;
                        txtPrice.Enabled = false;
                        txtCount.Enabled = false;
                        txtComments.Enabled = false;
                        txtPrompt.Enabled = false;
                        btnRollback.Enabled = false;
                        btnOk.Enabled = false;
                        btnAdd.Enabled = false;
                        btnPrint.Enabled = false;
                        txtAssLevel.Text = "";
                        txtAssLevelCode.Text = "";
                        txtAssName.Text = "";
                        txtCardID.Text = "";
                        txtCurCharge.Text = "";
                        txtCurIG.Text = "";
                        txtBillNo.Text = "";
                        btnRead.Enabled = true;
                        return;
                    }

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
                        DataTable dtConsList=(DataTable)dgvConsList.DataSource;
                        for(int i=0;i<dtConsList.Rows.Count;i++)
                        {
                            tbBillList billList = new tbBillList();
                            billList.iBillNo = serialNo.iSerialNo;//lSerialNo;
                            billList.iNO = i + 1;
                            billList.vcGoodsName = dtConsList.Rows[i]["消费项名称"].ToString();
                            billList.iCount = int.Parse(dtConsList.Rows[i]["数量"].ToString());
                            billList.vcUnit = "";
                            billList.nPrice = decimal.Parse(dtConsList.Rows[i]["单价"].ToString());
                            billList.nRate = decimal.Parse(dtConsList.Rows[i]["折扣"].ToString()) / 100;
                            billList.nCharge = decimal.Parse(dtConsList.Rows[i]["单项合计"].ToString());
                            amsContext.AddTotbBillList(billList);
                            list.Add(billList);
                        }

                        decimal deIgRate=10;
                        DateTime dtoperdate = DateTime.Now;
                        tbBillInvoice billInvoice = new tbBillInvoice();
                        billInvoice.iBillNo = serialNo.iSerialNo;
                        billInvoice.vcAssName = txtAssName.Text.Trim();
                        billInvoice.vcAssCardID = asscard1.vcAssCardID;
                        billInvoice.dtCreateDate = dtoperdate;
                        billInvoice.dtPrintDate = dtoperdate;
                        billInvoice.dLastCharge = intg1.nBalance;
                        billInvoice.dLastIg = intg1.iIgValue;
                        billInvoice.vcOperID = GlobalParams.oper.vcOperID;
                        billInvoice.vcOperName = GlobalParams.oper.vcOperName;
                        billInvoice.dSumFee = deTolCharge;
                        billInvoice.dServiceFee = 0;
                        billInvoice.dMealFee = 0;
                        billInvoice.dTotalFee = deTolCharge;
                        billInvoice.dCharge = intg1.nBalance - deTolCharge;
                        billInvoice.dIgValue = (decimal)(intg1.iIgValue + int.Parse(Math.Floor(deTolCharge / deIgRate).ToString()));
                        billInvoice.dIgGet = (decimal)int.Parse(Math.Floor(deTolCharge / deIgRate).ToString());
                        billInvoice.vcPrintFlag = "1";
                        billInvoice.vcBillType = "BI001";
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
                cmbGoodsName.Enabled = false;
                txtRate.Enabled = false;
                txtPrice.Enabled = false;
                txtCount.Enabled = false;
                txtComments.Enabled = false;
                txtPrompt.Enabled = false;
                btnRollback.Enabled = false;
                btnOk.Enabled = true;
                btnAdd.Enabled = false;
                btnPrint.Enabled = false;
                btnRead.Enabled = false;
                Helper.ShowInfo(this, "打印回执成功");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex,"打印异常");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                bool continueflg = true;
                string strAssCardID = txtCardID.Text.Trim();
                string strBillNo=txtBillNo.Text.Trim();
                if (strBillNo == "")
                    throw new Exception("帐单流水号有误，请先打印帐单！");
                decimal deTolCharge = decimal.Parse(txtTolCharge.Text.Trim());
                if (dgvConsList.Rows.Count == 0)
                    throw new Exception("没有任何消费记录，请重新输入！");
                using (AMSEntities amsContext = new AMSEntities())
                {                  
                    tbAssociatorCard asscard1 = amsContext.tbAssociatorCard.FirstOrDefault(ac => ac.vcAssCardID == strAssCardID);
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
                    GlobalParams.strInputBoxMes = "";
                    tbIntegral intg1 = amsContext.tbIntegral.FirstOrDefault(ig => ig.iAssID == asscard1.iAssID && ig.vcAssCardID == asscard1.vcAssCardID);
                    if (intg1 == null)
                        throw new Exception("会员余额积分异常！");
                    if (intg1.nBalance < deTolCharge)
                    {
                        MessageBox.Show("当前余额不足，请重新刷卡后再试！", "系统异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbGoodsName.Enabled = false;
                        txtRate.Enabled = false;
                        txtPrice.Enabled = false;
                        txtCount.Enabled = false;
                        txtComments.Enabled = false;
                        txtPrompt.Enabled = false;
                        btnRollback.Enabled = false;
                        btnOk.Enabled = false;
                        btnAdd.Enabled = false;
                        btnPrint.Enabled = false;
                        txtAssLevel.Text = "";
                        txtAssLevelCode.Text = "";
                        txtAssName.Text = "";
                        txtCardID.Text = "";
                        txtCurCharge.Text = "";
                        txtCurIG.Text = "";
                        txtBillNo.Text = "";
                        btnRead.Enabled = true;
                        return;
                    }

                    long conserialNo=0; 
                    long igserialNo=0;
                    using (AMSEntities amscontext2 = new AMSEntities())
                    {
                        tbConsserialNo conserial1 = new tbConsserialNo();
                        conserial1.vcFill = "0";
                        amscontext2.AddTotbConsserialNo(conserial1);
                        tbIgSerialNo igserial1 = new tbIgSerialNo();
                        igserial1.vcFill = "0";
                        amscontext2.AddTotbIgSerialNo(igserial1);
                        amscontext2.SaveChanges();
                        conserialNo=conserial1.iSerialNo;
                        igserialNo=igserial1.iSerialNo;
                    }

                    DateTime dtoperdate = DateTime.Now;
                    decimal deIgRate=10;
                    intg1.nBalance = intg1.nBalance - deTolCharge;
                    intg1.iIgValue=intg1.iIgValue+int.Parse(Math.Floor(deTolCharge/deIgRate).ToString());
                    
                    ObjectStateEntry ose= amsContext.ObjectStateManager.GetObjectStateEntry(intg1);
                    tbIntegralLog intglog1 = new tbIntegralLog();
                    intglog1.iIgSerial = igserialNo;
                    intglog1.iNo = 1;
                    intglog1.iAssID = intg1.iAssID;
                    intglog1.vcAssCardID = intg1.vcAssCardID;
                    intglog1.iIgLast = int.Parse(ose.OriginalValues["iIgValue"].ToString());
                    intglog1.dtIgDate = dtoperdate;
                    intglog1.vcIgName = "0";
                    intglog1.vcIgType = "IG001";
                    intglog1.iIgGet = int.Parse(Math.Floor(deTolCharge / deIgRate).ToString());
                    intglog1.iIgArrival = int.Parse(ose.CurrentValues["iIgValue"].ToString());
                    intglog1.iLinkCons = conserialNo;
                    intglog1.vcOperID = GlobalParams.oper.vcOperID;
                    intglog1.vcComments = "消费积分";
                    amsContext.AddTotbIntegralLog(intglog1);

                    DataTable dtconslist = (DataTable)dgvConsList.DataSource;
                    for (int i=0; i < dtconslist.Rows.Count; i++)
                    {
                        tbConsumption cons1 = new tbConsumption();
                        cons1.iConsSerial = conserialNo;
                        cons1.iNO = i + 1;
                        cons1.iAssID = intg1.iAssID;
                        cons1.vcAssCardID = intg1.vcAssCardID;
                        cons1.vcGoodsCode = dtconslist.Rows[i]["消费项名称"].ToString();
                        cons1.nGoodsPrice = decimal.Parse(dtconslist.Rows[i]["单价"].ToString());
                        cons1.iConsCount = int.Parse(dtconslist.Rows[i]["数量"].ToString());
                        cons1.nLastBalance = decimal.Parse(ose.OriginalValues["nBalance"].ToString());
                        cons1.nConsCharge1 = cons1.nGoodsPrice * cons1.iConsCount;
                        cons1.nConsRate = decimal.Parse(dtconslist.Rows[i]["折扣"].ToString())/100;
                        cons1.nConsCharge2 = decimal.Parse(dtconslist.Rows[i]["单项合计"].ToString());
                        cons1.nBalance = decimal.Parse(ose.CurrentValues["nBalance"].ToString());
                        cons1.dtConsDate = dtoperdate;
                        cons1.dtOperDate = dtoperdate;
                        cons1.vcFlag = "0";
                        cons1.iLink = null;
                        cons1.vcOperID = GlobalParams.oper.vcOperID;
                        cons1.vcClass = GlobalParams.strClass;
                        cons1.vcComments = dtconslist.Rows[i]["备注"].ToString();
                        amsContext.AddTotbConsumption(cons1);
                    }

                    long billnolong = long.Parse(strBillNo);
                    tbBillInvoice bill1 = amsContext.tbBillInvoice.FirstOrDefault(bil => bil.iBillNo == billnolong);
                    if (bill1 == null)
                    {
                        txtBillNo.Text = "";
                        throw new Exception("帐单信息不存在，请重新打印帐单！");
                    }
                    bill1.vcLinkSerial = conserialNo;
                    bill1.vcEffFlag = "1";

                    tbBusiLog busilog1 = new tbBusiLog();
                    busilog1.vcAssName = txtAssName.Text.Trim();
                    busilog1.vcAssCardID = intg1.vcAssCardID;
                    busilog1.vcLinkSerial = conserialNo;
                    busilog1.vcOperType = "OT011";
                    busilog1.vcOperID = GlobalParams.oper.vcOperID;
                    busilog1.vcOperName = GlobalParams.oper.vcOperName;
                    busilog1.dtOperDate = dtoperdate;
                    busilog1.iAssID = intg1.iAssID;
                    amsContext.AddTotbBusiLog(busilog1);

                    amsContext.SaveChanges();

                    cmbGoodsName.Enabled = false;
                    txtRate.Enabled = false;
                    txtPrice.Enabled = false;
                    txtCount.Enabled = false;
                    txtComments.Enabled = false;
                    txtPrompt.Enabled = false;
                    txtAssLevel.Text = "";
                    txtAssLevelCode.Text = "";
                    txtAssName.Text = "";
                    txtCardID.Text = "";
                    txtCurCharge.Text = "";
                    txtCurIG.Text = "";
                    txtBillNo.Text = "";
                    txtTolCharge.Text = "0";
                    txtTolCount.Text = "0";
                    btnRollback.Enabled = false;
                    btnOk.Enabled = false;
                    btnAdd.Enabled = false;
                    btnPrint.Enabled = false;
                    btnRead.Enabled = true;

                    DataTable dtConsList = new DataTable();
                    dtConsList.Columns.Add("消费项名称");
                    dtConsList.Columns.Add("单价");
                    dtConsList.Columns.Add("折扣");
                    dtConsList.Columns.Add("数量");
                    dtConsList.Columns.Add("单项合计");
                    dtConsList.Columns.Add("备注");
                    dtConsList.PrimaryKey = new DataColumn[] { dtConsList.Columns["消费项名称"], dtConsList.Columns["单价"], dtConsList.Columns["折扣"] };
                    dgvConsList.DataSource = dtConsList;

                    MessageBox.Show("本次消费结帐成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "结帐异常");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAssCons_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    btnRead.PerformClick();
                    break;
                case Keys.F6:
                    btnAdd.PerformClick();
                    break;
                case Keys.F7:
                    btnOk.PerformClick();
                    break;
            }  
        }
    }
}
