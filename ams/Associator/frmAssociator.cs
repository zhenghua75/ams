using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Objects;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ams.Common;
using System.Linq.Expressions;
namespace ams.Associator
{
    public partial class frmAssociator : Form
    {
        //private AMSEntities amsContext = new AMSEntities();
        public frmAssociator()
        {
            InitializeComponent();
        }

        private void frmAssociator_Load(object sender, EventArgs e)
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
                        {
                            tsmi.Visible = false;
                            if (tsmi.Text == "卡密码修改")
                                btnInitCardPwd.Visible = false;
                        }
                        else
                        {
                            tsmi.Visible = true;
                            if (tsmi.Text == "卡密码修改")
                                btnInitCardPwd.Visible = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
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

            tbOper aOper = new tbOper();
            aOper.vcOperID = "%";
            aOper.vcOperName = "*";
            List<tbOper> operList = GlobalParams.Opers.OrderBy(oo=>oo.vcOperName).ToList();
            operList.Insert(0, aOper);
            cmbOper.DataSource = operList;
            cmbOper.DisplayMember = "vcOperName";
            cmbOper.ValueMember = "vcOperID";

            dtpBeginInDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            dtpBeginOperDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            dtpEndInDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            dtpEndOperDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        private void BindAssociator(object associator)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.DataSource = associator;

            #region 添加列
            DataGridViewComboBoxColumn alcolumn = new DataGridViewComboBoxColumn();
            alcolumn.Name = "vcAssLevelComments";
            alcolumn.DataPropertyName = "vcAssLevel";
            alcolumn.HeaderText = "会员级别";
            alcolumn.ValueMember = "vcCommCode";
            alcolumn.DisplayMember = "vcCommName";
            alcolumn.DataSource = GlobalParams.CommCode.Where(cc => cc.vcCommSign == "AL").ToList();

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
            dataGridView1.Columns.Add(astcolumn);
            #endregion

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

            dataGridView1.Columns["vcComments"].HeaderText = "备注";
            dataGridView1.Columns["vcComments"].Visible = false;
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

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns["vcAssCardID"].HeaderText = "会员卡号";

            dataGridView1.Columns["vcAssCardID"].DisplayIndex = 0;
            dataGridView1.Columns["vcAssName"].DisplayIndex = 1;
            dataGridView1.Columns["cAssSex"].DisplayIndex = 2;
            dataGridView1.Columns["iAssAge"].DisplayIndex = 3;
            dataGridView1.Columns["vcLinkPhone"].DisplayIndex = 4;
            dataGridView1.Columns["vcMobile"].DisplayIndex = 5;
            dataGridView1.Columns["vcCompanyName"].DisplayIndex = 6;
            dataGridView1.Columns["vcComPhone"].DisplayIndex = 7;


            dataGridView1.Columns["dtInAssDate"].DisplayIndex = 8;
            dataGridView1.Columns["vcAssLevel"].DisplayIndex = 9;
            dataGridView1.Columns["vcAssStateComments"].DisplayIndex = 10;
            dataGridView1.Columns["vcOperIDComments"].DisplayIndex = 11;
            dataGridView1.Columns["dtOperDate"].DisplayIndex = 12;

            dataGridView1.Columns["dtInAssDate"].DefaultCellStyle.Format = "yyyy年MM月dd日";
            dataGridView1.Columns["dtOperDate"].DefaultCellStyle.Format = "yyyy年MM月dd日";
            #endregion

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show(this, e.Exception.Message, "数据格式不正确");
            try
            {
            }
            catch (Exception)
            {
            }
            
        }
        #region 操作
        private void btnAddAss_Click(object sender, EventArgs e)
        {
            frmAss ass = new frmAss();
            ass.OperType = "ADD";
            ass.MinimizeBox = false;
            ass.MaximizeBox = false;
            ass.ShowDialog();
        }

        private void btnModifyAss_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                vwAss ass1 = dataGridView1.SelectedRows[0].DataBoundItem as vwAss;
                tbAssociator ass = null;
                using (AMSEntities amsContext = new AMSEntities())
                {
                    ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == ass1.iAssID);
                }
                frmAss frmass = new frmAss();
                frmass.ass = ass;
                frmass.OperType = "MODIFY";
                frmass.MinimizeBox = false;
                frmass.MaximizeBox = false;
                frmass.ShowDialog();
            }
            else
                Helper.ShowInfo(this, "请查询或搜索会员，并选择会员");
        }

        private void btnDetailAss_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                vwAss ass1 = dataGridView1.SelectedRows[0].DataBoundItem as vwAss;
                tbAssociator ass = null;
                using (AMSEntities amsContext = new AMSEntities())
                {
                    ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == ass1.iAssID);
                }
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    vwAss ass1 = dataGridView1.SelectedRows[0].DataBoundItem as vwAss;
                    tbAssociator ass = null;
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == ass1.iAssID);
                    }
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
        #endregion
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                using (AMSEntities amsContext = new AMSEntities())
                {
                    string strQueryString = "";
                    strQueryString += "SELECT VALUE assCard "
                        + "FROM AMSEntities.vwAss AS assCard WHERE 1=1";
                    if (!cmbAssLevel.Text.Equals("*"))
                        strQueryString += " AND assCard.vcAssLevel like @vcAssLevel";
                    if (!cmbAssState.Text.Equals("*"))
                        strQueryString += " AND assCard.vcAssState like @vcAssState";
                    if (!cmbOper.Text.Equals("*"))
                        strQueryString += " AND assCard.vcOperID like @vcOperID";
                    if (chkBeginInAssDate.Checked)
                        strQueryString += " AND assCard.dtInAssDate >= @dtpBeginInAssDate";
                    if (chkEndInAssDate.Checked)
                        strQueryString += " AND assCard.dtInAssDate <= @dtpEndInAssDate";
                    if (chkBeginOperDate.Checked)
                        strQueryString += " AND assCard.dtOperDate >= @dtpBeginOperDate";
                    if (chkEndOperDate.Checked)
                        strQueryString += " AND assCard.dtOperDate <= @dtpEndOperDate";

                    ObjectQuery<vwAss> ass = new ObjectQuery<vwAss>(strQueryString, amsContext, MergeOption.NoTracking);
                    if (!cmbAssLevel.Text.Equals("*"))
                        ass.Parameters.Add(new ObjectParameter("vcAssLevel", cmbAssLevel.SelectedValue));
                    if (!cmbAssState.Text.Equals("*"))
                        ass.Parameters.Add(new ObjectParameter("vcAssState", cmbAssState.SelectedValue));
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

        private void btnAddCard_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    vwAss ass1 = dataGridView1.SelectedRows[0].DataBoundItem as vwAss;
                    if (ass1.vcAssState == "1")
                        throw new Exception("此会员已发卡，请重新查询或搜索会员");
                    //vwAss ass1 = dataGridView1.SelectedRows[0].DataBoundItem as vwAss;
                    tbAssociator ass = null;
                    using (AMSEntities amsContext = new AMSEntities())
                    {
                        ass = amsContext.tbAssociator.FirstOrDefault(a => a.iAssID == ass1.iAssID);
                    }
                    frmAddCard addcard = new frmAddCard();
                    addcard.ass = ass;
                    addcard.MinimizeBox = false;
                    addcard.MaximizeBox = false;
                    addcard.ShowDialog();
                }
                else
                    Helper.ShowInfo(this, "请查询或搜索会员，并选择会员");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }

        private void btnModifyCardPwd_Click(object sender, EventArgs e)
        {
            frmModifyCardPwd frmass = new frmModifyCardPwd();
            frmass.MinimizeBox = false;
            frmass.MaximizeBox = false;
            frmass.ShowDialog();
        }

        private void btnInitCardPwd_Click(object sender, EventArgs e)
        {
            frmInitCardPwd frmass = new frmInitCardPwd();
            frmass.MinimizeBox = false;
            frmass.MaximizeBox = false;
            frmass.ShowDialog();
        }
    }
}
