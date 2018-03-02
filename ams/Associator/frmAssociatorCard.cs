using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ams.Common;
using System.Linq.Expressions;
using System.Data.Common;
namespace ams.Associator
{
    public partial class frmAssociatorCard : Form
    {
        //private AMSEntities amsContext = new AMSEntities();
        public frmAssociatorCard()
        {
            InitializeComponent();
        }

        private void frmAssociatorCard_Load(object sender, EventArgs e)
        {
            try
            {
                SetPrivilege();
                InitFrm();
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #region 设置权限
        private void SetPrivilege()
        {
            try
            {
                string strLimitCode = GlobalParams.oper.vcLimit;

                foreach (ToolStripItem tsmi in this.toolStrip1.Items)
                {
                    string[] menu = ConstApp.strmenu.FirstOrDefault(l => l[3] == tsmi.Text.Trim());
                    if (menu != null && menu[0] == "1")
                    {
                        string strMenu1 = menu[1];
                        string strMenu2 = menu[2];
                        if (GlobalParams.OperLimit.SingleOrDefault(ol => ol.vcLimitCode == strLimitCode && ol.vcMenu1.Trim() == strMenu1 && ol.vcMenu2.Trim() == strMenu2) == null)
                            tsmi.Visible = false;
                        else
                            tsmi.Visible = true;
                    }
                }

                btnCardCallback.Visible = false;

            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
        #region 初始化控件
        private void InitFrm()
        {
            tbCommCode comm = new tbCommCode();
            comm.vcCommCode = "%";
            comm.vcCommName = "*";

            List<tbCommCode> alcomm = GlobalParams.CommCode.Where(cc => cc.vcCommSign == "AL").ToList();
            alcomm.Insert(0, comm);
            cmbAssLevel.DataSource = alcomm;
            cmbAssLevel.DisplayMember = "vcCommName";
            cmbAssLevel.ValueMember = "vcCommCode";

            List<tbCommCode> astcomm = GlobalParams.CommCode.Where(cc => cc.vcCommSign == "AST").ToList();
            astcomm.Insert(0, comm);
            cmbAssState.DataSource = astcomm;
            cmbAssState.DisplayMember = "vcCommName";
            cmbAssState.ValueMember = "vcCommCode";

            List<tbCommCode> cstcomm = GlobalParams.CommCode.Where(cc => cc.vcCommSign == "CST").ToList();
            cstcomm.Insert(0, comm);
            cmbCardState.DataSource = cstcomm;
            cmbCardState.DisplayMember = "vcCommName";
            cmbCardState.ValueMember = "vcCommCode";

            tbOper aOper = new tbOper();
            aOper.vcOperID = "%";
            aOper.vcOperName = "*";

            List<tbOper> operList = GlobalParams.Opers.OrderBy(oo => oo.vcOperName).ToList();
            operList.Insert(0, aOper);
            cmbOper.DataSource = operList;
            cmbOper.DisplayMember = "vcOperName";
            cmbOper.ValueMember = "vcOperID";

            dtpBeginInDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            dtpBeginOperDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            dtpEndInDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            dtpEndOperDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        #endregion
        #region 绑定Grid
        private void BindAssociator(object associator)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.DataSource = associator;

            DataGridViewComboBoxColumn alcolumn = new DataGridViewComboBoxColumn();
            alcolumn.Name = "vcAssLevelComments";
            alcolumn.DataPropertyName = "vcAssLevel";
            alcolumn.HeaderText = "会员级别";
            alcolumn.ValueMember = "vcCommCode";
            alcolumn.DisplayMember = "vcCommName";
            alcolumn.DataSource = GlobalParams.CommCode.Where(cc => cc.vcCommSign == "AL").ToList();

            DataGridViewComboBoxColumn cstcolumn = new DataGridViewComboBoxColumn();
            cstcolumn.Name = "cCardStateComments";
            cstcolumn.DataPropertyName = "cCardState";
            cstcolumn.HeaderText = "会员卡状态";
            cstcolumn.ValueMember = "vcCommCode";
            cstcolumn.DisplayMember = "vcCommName";
            cstcolumn.DataSource = GlobalParams.CommCode.Where(cc => cc.vcCommSign == "CST").ToList();

            DataGridViewComboBoxColumn astcolumn = new DataGridViewComboBoxColumn();
            astcolumn.Name = "vcAssStateComments";
            astcolumn.DataPropertyName = "vcAssState";
            astcolumn.HeaderText = "会员状态";
            astcolumn.ValueMember = "vcCommCode";
            astcolumn.DisplayMember = "vcCommName";
            astcolumn.DataSource = GlobalParams.CommCode.Where(cc => cc.vcCommSign == "AST").ToList();

            DataGridViewComboBoxColumn opercolumn = new DataGridViewComboBoxColumn();
            opercolumn.Name = "vcOperIDComments";
            opercolumn.DataPropertyName = "vcOperID";
            opercolumn.HeaderText = "操作员";
            opercolumn.ValueMember = "vcOperID";
            opercolumn.DisplayMember = "vcOperName";
            opercolumn.DataSource = GlobalParams.Opers;
            
            dataGridView1.Columns.Add(opercolumn);
            dataGridView1.Columns.Add(alcolumn);
            dataGridView1.Columns.Add(cstcolumn);
            dataGridView1.Columns.Add(astcolumn);
            #region 列定义
            dataGridView1.Columns["cAssSex"].HeaderText = "性别";
            //dataGridView1.Columns["cAssSex"].Visible = false;            
            dataGridView1.Columns["dtInAssDate"].HeaderText = "入会日期";
            //dataGridView1.Columns["dtInAssDate"].Visible = false;            
            dataGridView1.Columns["dtOperDate"].HeaderText = "操作日期";
            dataGridView1.Columns["iAssAge"].HeaderText = "年龄";
            dataGridView1.Columns["iAssAge"].Visible = false;



            dataGridView1.Columns["iAssID"].HeaderText = "会员ID";
            dataGridView1.Columns["iAssID"].Visible = false;
            dataGridView1.Columns["vcAssLevel"].HeaderText = "会员级别";
            dataGridView1.Columns["vcAssLevel"].Visible = false;
            dataGridView1.Columns["vcAssName"].HeaderText = "姓名";

            dataGridView1.Columns["vcAssNbr"].HeaderText = "身份证";
            dataGridView1.Columns["vcAssNbr"].Visible = false;

            dataGridView1.Columns["vcAssState"].HeaderText = "会员状态";
            dataGridView1.Columns["vcAssState"].Visible = false;
            dataGridView1.Columns["vcAssType"].HeaderText = "无意义";
            dataGridView1.Columns["vcAssType"].Visible = false;
            dataGridView1.Columns["vcComAddress"].HeaderText = "公司地址";
            dataGridView1.Columns["vcComAddress"].Visible = false;
            dataGridView1.Columns["vcComFax"].HeaderText = "传真";
            dataGridView1.Columns["vcComFax"].Visible = false;
            //if (dataGridView1.Columns.Contains("vcComments"))
            //{
                dataGridView1.Columns["vcComments"].HeaderText = "备注";
                //dataGridView1.Columns["vcComments"].Visible = false;
            //}
            dataGridView1.Columns["vcCompanyName"].HeaderText = "公司名称";
            dataGridView1.Columns["vcComPhone"].HeaderText = "公司电话";
            //dataGridView1.Columns["vcComPhone"].Visible = false;
            dataGridView1.Columns["vcComPostID"].HeaderText = "公司邮编";
            dataGridView1.Columns["vcComPostID"].Visible = false;
            dataGridView1.Columns["vcEmail"].HeaderText = "电子邮件";
            dataGridView1.Columns["vcEmail"].Visible = false;

            dataGridView1.Columns["vcLinkAddress"].HeaderText = "联系地址";
            dataGridView1.Columns["vcLinkAddress"].Visible = false;
            dataGridView1.Columns["vcLinkPhone"].HeaderText = "联系电话";
            //dataGridView1.Columns["vcLinkPhone"].Visible = false;
            dataGridView1.Columns["vcMobile"].HeaderText = "手机";
            dataGridView1.Columns["vcOperID"].HeaderText = "操作员";
            dataGridView1.Columns["vcOperID"].Visible = false;
            dataGridView1.Columns["vcPassport"].HeaderText = "护照号码";
            dataGridView1.Columns["vcPassport"].Visible = false;

            dataGridView1.Columns["vcPostID"].HeaderText = "邮政编码";
            dataGridView1.Columns["vcPostID"].Visible = false;

            dataGridView1.Columns["vcAssCardID"].HeaderText = "会员卡号";
            dataGridView1.Columns["vcAssPwd"].HeaderText = "会员密码";
            dataGridView1.Columns["vcAssPwd"].Visible = false;
            dataGridView1.Columns["dtCardPutDate"].HeaderText = "发卡日期";
            dataGridView1.Columns["cCardState"].HeaderText = "会员卡状态";
            dataGridView1.Columns["cCardState"].Visible = false;
            dataGridView1.Columns["dtCardEffDate"].HeaderText = "卡生效日期";
            dataGridView1.Columns["dtCardExpDate"].HeaderText = "卡失效日期";
            dataGridView1.Columns["vcCardLevel"].HeaderText = "会员卡级别";
            dataGridView1.Columns["vcCardLevel"].Visible = false;

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns["vcAssCardID"].DisplayIndex = 0;
            dataGridView1.Columns["vcAssName"].DisplayIndex = 1;
            dataGridView1.Columns["cAssSex"].DisplayIndex = 2;
            dataGridView1.Columns["iAssAge"].DisplayIndex = 3;
            dataGridView1.Columns["vcLinkPhone"].DisplayIndex = 4;
            dataGridView1.Columns["vcMobile"].DisplayIndex = 5;
            dataGridView1.Columns["vcCompanyName"].DisplayIndex = 6;
            dataGridView1.Columns["vcComPhone"].DisplayIndex = 7;


            dataGridView1.Columns["dtInAssDate"].DisplayIndex = 8;
            dataGridView1.Columns["vcAssLevelComments"].DisplayIndex = 9;
            dataGridView1.Columns["vcAssStateComments"].DisplayIndex = 10;
            dataGridView1.Columns["cCardStateComments"].DisplayIndex = 11;
            dataGridView1.Columns["vcOperIDComments"].DisplayIndex = 12;
            dataGridView1.Columns["dtOperDate"].DisplayIndex = 13;
            dataGridView1.Columns["dtCardPutDate"].DisplayIndex = 14;
            dataGridView1.Columns["dtCardEffDate"].DisplayIndex = 15;
            dataGridView1.Columns["dtCardExpDate"].DisplayIndex = 16;
            dataGridView1.Columns["vcComments"].DisplayIndex = 17;
            dataGridView1.Columns["dtInAssDate"].DefaultCellStyle.Format = "yyyy年MM月dd日";
            dataGridView1.Columns["dtOperDate"].DefaultCellStyle.Format = "yyyy年MM月dd日";
            dataGridView1.Columns["dtCardPutDate"].DefaultCellStyle.Format = "yyyy年MM月dd日";
            dataGridView1.Columns["dtCardEffDate"].DefaultCellStyle.Format = "yyyy年MM月dd日";
            dataGridView1.Columns["dtCardExpDate"].DefaultCellStyle.Format = "yyyy年MM月dd日";
            #endregion

        }
        #endregion
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Helper.ShowError(this, e.Exception.Message);
            try
            {
            }
            catch (Exception)
            {
            }
        }
        #region 查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string strQueryString = "";
                strQueryString += "SELECT VALUE assCard "
                    + "FROM AMSEntities.vwAssCard AS assCard WHERE "
                + " assCard.vcAssCardID like '%" + txtAssCardID.Text + "'";
                if (!cmbCardState.Text.Equals("*"))
                    strQueryString += " AND assCard.cCardState = @cCardState ";
                if (!cmbAssLevel.Text.Equals("*"))
                    strQueryString += " AND assCard.vcAssLevel like @vcAssLevel";
                if (!cmbAssState.Text.Equals("*"))
                    strQueryString += " AND assCard.vcAssType like @vcAssType";
                if (!cmbOper.Text.Equals("*"))
                    strQueryString +=  " AND assCard.vcOperID like @vcOperID";
                if (chkBeginInAssDate.Checked)
                    strQueryString += " AND assCard.dtInAssDate >= @dtpBeginInAssDate";
                if (chkEndInAssDate.Checked)
                    strQueryString += " AND assCard.dtInAssDate <= @dtpEndInAssDate";
                if (chkBeginOperDate.Checked)
                    strQueryString += " AND assCard.dtOperDate >= @dtpBeginOperDate";
                if (chkEndOperDate.Checked)
                    strQueryString += " AND assCard.dtOperDate <= @dtpEndOperDate";
                using (AMSEntities amsContext = new AMSEntities())
                {
                    //数据操作
                    ObjectQuery<vwAssCard> ass = new ObjectQuery<vwAssCard>(strQueryString, amsContext, MergeOption.NoTracking);
                    if (!cmbCardState.Text.Equals("*"))
                        ass.Parameters.Add(new ObjectParameter("cCardState", cmbCardState.SelectedValue));
                    if (!cmbAssLevel.Text.Equals("*"))
                        ass.Parameters.Add(new ObjectParameter("vcAssLevel", cmbAssLevel.SelectedValue));
                    if (!cmbAssState.Text.Equals("*"))
                        ass.Parameters.Add(new ObjectParameter("vcAssType", cmbAssState.SelectedValue));
                    if (!cmbOper.Text.Equals("*"))
                        ass.Parameters.Add(new ObjectParameter("vcOperID", cmbOper.SelectedValue));
                    if (chkBeginInAssDate.Checked)
                        ass.Parameters.Add(new ObjectParameter("dtpBeginInAssDate", dtpBeginInDate.Value));
                    if (chkEndInAssDate.Checked)
                        ass.Parameters.Add(new ObjectParameter("dtpEndInAssDate", dtpEndInDate.Value));
                    if (chkBeginOperDate.Checked)
                        ass.Parameters.Add(new ObjectParameter("dtpBeginOperDate", dtpBeginOperDate.Value));
                    if (chkEndOperDate.Checked)
                        ass.Parameters.Add(new ObjectParameter("dtpEndOperDate", dtpEndOperDate.Value));

                    if (string.IsNullOrEmpty(txtSearch.Text))
                        BindAssociator(ass.ToList());
                    else
                    {
                        var query = Search(ass, t => new
                        {
                            t.vcAssName,
                            t.cAssSex,
                            t.vcAssNbr,
                            t.vcPassport,
                            t.vcLinkAddress,
                            t.vcLinkPhone,
                            //t.iAssAge,
                            t.vcPostID,
                            t.vcEmail,
                            t.vcMobile,
                            t.vcCompanyName,
                            t.vcComAddress,
                            t.vcComPhone,
                            t.vcComFax
                            ,
                            t.vcComPostID,
                            t.vcComments
                        }, new[] { txtSearch.Text });
                        BindAssociator(query.ToList());
                    }
                }


            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }

        IQueryable<MyTable> Search<MyTable, MyField>(IQueryable<MyTable> query, Expression<Func<MyTable, MyField>> arrField, string[] arrKeyword)
        {
            Expression rule = null;
            foreach (var strKey in arrKeyword)
            {
                if (strKey.Trim() == "") continue;
                foreach (var field in ((NewExpression)arrField.Body).Arguments)
                {
                    
                    //var expr = Expression.Call(field, typeof(string).GetMethod("Contains"), Expression.Constant(strKey));
                    var expr = Expression.Call(field, typeof(string).GetMethod("Contains"), Expression.Constant(strKey));
                    if (rule == null)
                    {
                        rule = expr;
                        continue;
                    }
                    rule = Expression.Or(rule, expr);
                }
            }
            if (rule == null) return query;
            return query.Where((Expression<Func<MyTable, bool>>)Expression.Lambda(rule, arrField.Parameters[0]));
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //其它操作
                using (AMSEntities amsContext = new AMSEntities())
                {
                    //数据操作
                    var query = from item in amsContext.vwAssCard select item;
                    query = Search(query, t => new
                    {
                        t.vcAssName,
                        t.cAssSex,
                        t.vcAssNbr,
                        t.vcPassport,
                        t.vcLinkAddress,
                        t.vcLinkPhone,
                        //t.iAssAge,
                        t.vcPostID,
                        t.vcEmail,
                        t.vcMobile,
                        t.vcCompanyName,
                        t.vcComAddress,
                        t.vcComPhone,
                        t.vcComFax
                        ,
                        t.vcComPostID,
                        t.vcComments
                    }, new[] { txtSearch.Text });
                    BindAssociator(query);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
        #region 双击GRID
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        vwAssCard assCard = dataGridView1.SelectedRows[0].DataBoundItem as vwAssCard;
                        tbAssociator ass = null;
                        using (AMSEntities amsContext = new AMSEntities())
                        {
                            //数据操作
                            ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == assCard.iAssID);
                        }
                        if (ass == null)
                            throw new Exception("未找到会员信息");
                        frmAss frmass = new frmAss();
                        frmass.ass = ass;
                        frmass.OperType = "DETAIL";
                        frmass.MinimizeBox = false;
                        frmass.MaximizeBox = false;
                        frmass.ShowDialog();
                    }
                    else
                        Helper.ShowInfo(this, "请查询或搜索会员，并选择会员");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }

        }
        #endregion
        #region 充值
        private void btnCardFillFee_Click(object sender, EventArgs e)
        {
            frmCardFillFee fillFee = new frmCardFillFee();            
            fillFee.ControlBox = false;
            fillFee.ShowDialog();
        }
        #endregion 
        #region 挂失
        private void btnCardLose_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    vwAssCard ass1 = dataGridView1.SelectedRows[0].DataBoundItem as vwAssCard;
                    tbAssociator ass = null;
                    tbAssociatorCard assCard = null;
                    tbIntegral ig = null;
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        //数据操作
                        ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == ass1.iAssID);
                        assCard = amsContext.tbAssociatorCard.FirstOrDefault(a => a.iAssID == ass1.iAssID && a.vcAssCardID == ass1.vcAssCardID);
                        ig = amsContext.tbIntegral.FirstOrDefault(i => i.iAssID == ass1.iAssID && i.vcAssCardID == ass1.vcAssCardID);
                    }
                    if (ass == null)
                        throw new Exception("未找到会员信息");
                    if (assCard == null)
                        throw new Exception("未找到会员卡信息");                    
                    if (assCard.cCardState != ConstApp.CST_1)
                        throw new Exception("此会员卡无法做挂失操作");
                    if(ass.vcAssState!= ConstApp.AST_1)
                        throw new Exception("此会员无法做挂失操作");
                    if (ig == null)
                        throw new Exception("获取会员积分信息错误无法做挂失");
                    frmCardLose cardLose = new frmCardLose();                    
                    cardLose.ass = ass;
                    cardLose.assCard = assCard;
                    cardLose.ig = ig;
                    cardLose.ControlBox = false;
                    cardLose.ShowDialog();

                }
                else
                    Helper.ShowInfo(this, "请查询或搜索会员，并选择会员");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
        #region 补卡
        private void btnCardAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    vwAssCard ass = dataGridView1.SelectedRows[0].DataBoundItem as vwAssCard;
                   
                    if (ass.cCardState != "2")
                        throw new Exception("此会员卡无法做补卡操作");

                    frmCardAdd cardAdd = new frmCardAdd();
                    cardAdd.AssCardID = ass.vcAssCardID;
                    cardAdd.AssID = ass.iAssID;
                    cardAdd.MinimizeBox = false;
                    cardAdd.MaximizeBox = false;
                    cardAdd.ShowDialog();

                }
                else
                    MessageBox.Show(this, "请查询或搜索会员，并选择会员", "提示");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
        #region 解挂
        private void btnCardFree_Click(object sender, EventArgs e)
        {
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    vwAssCard ass1 = dataGridView1.SelectedRows[0].DataBoundItem as vwAssCard;
                    if (ass1.cCardState != ConstApp.CST_2)
                        throw new Exception("此会员卡无法做解挂操作");
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
                            

                            tbAssociator ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == ass1.iAssID);
                            tbAssociatorCard assCard = amsContext.tbAssociatorCard.FirstOrDefault(a => a.iAssID == ass1.iAssID && a.vcAssCardID == ass1.vcAssCardID);
                            tbIntegral ig = amsContext.tbIntegral.FirstOrDefault(i => i.vcAssCardID == assCard.vcAssCardID && i.iAssID == assCard.iAssID);

                            if (ass == null)
                                throw new Exception("未找到会员信息");
                            if (assCard == null)
                                throw new Exception("未找到会员卡信息");
                            
                            if (ig == null)
                                throw new Exception("获取会员卡积分错误，请重试");
                            string strInfo = "会员卡号：" + assCard.vcAssCardID
                                + "\n会员姓名：" + ass.vcAssName
                                + "\n当前余额：" + ig.nBalance
                                + "\n身份证号：" + ass.vcAssNbr
                                + "\n公司名称：" + ass.vcCompanyName;
                            if (DialogResult.Yes == MessageBox.Show(this, strInfo, "解挂信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                            {
                                assCard.cCardState = ConstApp.CST_1;
                                assCard.dtCardExpDate = null;
                                assCard.vcOperID = GlobalParams.oper.vcOperID;
                                assCard.dtOperDate = DateTime.Now;

                                tbBusiLog busiLog = new tbBusiLog();
                                busiLog.iAssID = ass.iAssID;
                                busiLog.dtOperDate = DateTime.Now;
                                busiLog.vcAssCardID = assCard.vcAssCardID;
                                busiLog.vcAssName = ass.vcAssName;
                                busiLog.vcOperName = GlobalParams.oper.vcOperName;
                                busiLog.vcOperID = GlobalParams.oper.vcOperID;
                                busiLog.vcOperType = "OT014";
                                //busiLog.vcLinkSerial = billInvoice.iBillNo;
                                amsContext.AddTotbBusiLog(busiLog);
                                Helper.Save(amsContext);
                                trans.Commit();
                                Helper.ShowInfo(this, "会员卡解挂成功");
                            }
                        }
                    }

                }
                else
                    Helper.ShowInfo(this, "请查询或搜索会员，并选择会员");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        #endregion
        #region 退卡
        private void btnCardReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    vwAssCard ass1 = dataGridView1.SelectedRows[0].DataBoundItem as vwAssCard;
                    tbAssociator ass = null;
                    tbAssociatorCard assCard = null;
                    tbIntegral ig = null;
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        //数据操作
                        ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == ass1.iAssID);
                        assCard = amsContext.tbAssociatorCard.FirstOrDefault(a => a.iAssID == ass1.iAssID && a.vcAssCardID == ass1.vcAssCardID);
                        ig = amsContext.tbIntegral.FirstOrDefault(i => i.iAssID == ass1.iAssID && i.vcAssCardID == ass1.vcAssCardID);
                    }
                    if (ass == null)
                        throw new Exception("未找到会员信息");
                    if (assCard == null)
                        throw new Exception("未找到会员卡信息");
                    if (assCard.cCardState != "1")
                        throw new Exception("此会员卡无法做退卡操作");
                    if (ig == null)
                        throw new Exception("未找到会员积分信息");

                    frmCardReturn cardReturn = new frmCardReturn();
                    cardReturn.ass = ass;
                    cardReturn.assCard = assCard;
                    cardReturn.ig = ig;
                    cardReturn.ControlBox = false;
                    cardReturn.ShowDialog();

                }
                else
                    Helper.ShowInfo(this, "请查询或搜索会员，并选择会员");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #endregion
        #region 回收
        private void btnCardCallback_Click(object sender, EventArgs e)
        {
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    vwAssCard ass1 = dataGridView1.SelectedRows[0].DataBoundItem as vwAssCard;
                    if (ass1.cCardState != ConstApp.CST_0)
                        throw new Exception("此会员卡无法做卡回收操作");
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
                            

                            tbAssociator ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == ass1.iAssID);
                            tbAssociatorCard assCard = amsContext.tbAssociatorCard.FirstOrDefault(a => a.iAssID == ass1.iAssID && a.vcAssCardID == ass1.vcAssCardID);
                            //tbIntegral ig = amsContext.tbIntegral.FirstOrDefault(i => i.vcAssCardID == assCard.vcAssCardID && i.iAssID == assCard.iAssID);

                            if (ass == null)
                                throw new Exception("未找到会员信息");
                            if (assCard == null)
                                throw new Exception("未找到会员卡信息");
                            
                            //if (ig == null)
                            //    throw new Exception("获取会员卡积分错误，请重试");
                            string strInfo = "会员卡号：" + assCard.vcAssCardID
                                + "\n会员姓名：" + ass.vcAssName
                                //+ "\n当前余额：" + ig.nBalance
                                + "\n身份证号：" + ass.vcAssNbr
                                + "\n公司名称：" + ass.vcCompanyName;
                            if (DialogResult.Yes == MessageBox.Show(this, strInfo, "卡回收信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                            {
                                assCard.cCardState = ConstApp.CST_4;
                                assCard.dtCardExpDate = DateTime.Now;
                                assCard.vcOperID = GlobalParams.oper.vcOperID;
                                assCard.dtOperDate = DateTime.Now;

                                tbBusiLog busiLog = new tbBusiLog();
                                busiLog.dtOperDate = DateTime.Now;
                                busiLog.iAssID = assCard.iAssID;
                                busiLog.vcAssCardID = assCard.vcAssCardID;
                                busiLog.vcAssName = ass.vcAssName;
                                busiLog.vcOperName = GlobalParams.oper.vcOperName;
                                busiLog.vcOperID = GlobalParams.oper.vcOperID;
                                busiLog.vcOperType = "OT006";
                                //busiLog.vcLinkSerial = billInvoice.iBillNo;
                                amsContext.AddTotbBusiLog(busiLog);
                                Helper.Save(amsContext);

                                //写卡，卡为原始状态
                                trans.Commit();
                                Helper.ShowInfo(this, "会员卡回收成功");
                            }
                        }
                    }

                }
                else
                    Helper.ShowInfo(this, "请查询或搜索会员，并选择会员");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
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
