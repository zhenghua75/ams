using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Objects;
using System.Text;
using System.Windows.Forms;
using ams.Common;
using System.Reflection;
namespace ams.Associator
{

    public static class MyExtenders
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            //Create the columns in the DataTable
            foreach (PropertyInfo pi in pia)
            {
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }
            //Populate the table
            foreach (T item in collection)
            {
                DataRow dr = dt.NewRow();
                dr.BeginEdit();
                foreach (PropertyInfo pi in pia)
                {
                    dr[pi.Name] = pi.GetValue(item, null);
                }
                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }

    public partial class frmParaSet : Form
    {
        public frmParaSet()
        {
            InitializeComponent();
        }
        private AMSEntities amsContext;
        private void frmParaSet_Load(object sender, EventArgs e)
        {
            try
            {
                amsContext = new AMSEntities();
                BindGoods();
                
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        #region 消费项
        private void BindGoods()
        {
             var goods = GetGoods();
             this.usParaSet1.MyAMSEntities = amsContext;
             this.usParaSet1.MyDataSource = goods;
             this.usParaSet1.MyDataGridView.CellValidating +=new DataGridViewCellValidatingEventHandler(MyDataGridView_CellValidating);
             this.usParaSet1.MyReload.Click += new EventHandler(MyReload_Click);


             this.usParaSet1.MyDataGridView.Columns["vcGoodsCode"].DisplayIndex = 0;
             this.usParaSet1.MyDataGridView.Columns["vcGoodsName"].DisplayIndex = 1;
             this.usParaSet1.MyDataGridView.Columns["nGoodsPrice"].DisplayIndex = 2;
             this.usParaSet1.MyDataGridView.Columns["iRateFloor"].DisplayIndex = 3;
             this.usParaSet1.MyDataGridView.Columns["iRateFloor"].Visible = false;
             this.usParaSet1.MyDataGridView.Columns["vcGoodsComments"].DisplayIndex = 4;

             this.usParaSet1.MyDataGridView.Columns["vcGoodsCode"].HeaderText = "消费项编码";
             this.usParaSet1.MyDataGridView.Columns["vcGoodsName"].HeaderText = "消费项名称";
             this.usParaSet1.MyDataGridView.Columns["nGoodsPrice"].HeaderText = "单价";
             this.usParaSet1.MyDataGridView.Columns["iRateFloor"].HeaderText = "折扣最低限";
             this.usParaSet1.MyDataGridView.Columns["vcGoodsComments"].HeaderText = "备注";
        }
        private object GetGoods()
        {
            return 
                from item in amsContext.tbGoods orderby item.vcGoodsCode select item;       
        }
        void MyReload_Click(object sender, EventArgs e)
        {
            amsContext = new AMSEntities();
            var goods = GetGoods();//from item in amsContext.tbGoods orderby item.vcGoodsCode select item;
            this.usParaSet1.MyAMSEntities = amsContext;
            this.usParaSet1.MyDataSource = goods;
            Helper.ShowInfo(this, "数据已重新加载");
        }

        void MyDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.usParaSet1.MyDataGridView.Rows[e.RowIndex].IsNewRow) return;
            if (this.usParaSet1.MyDataGridView.Columns[e.ColumnIndex].HeaderText == "消费项编码")
            {
               
                string strValue = e.FormattedValue.ToString();
                if (!strValue.StartsWith("G"))
                {
                    MessageBox.Show(this, "消费项编码需以G开头", "数据格式不正确");
                    e.Cancel = true;
                    //this.usParaSet1.MyDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "消费项编码需以G开头";
                    //e.Cancel = false;
                }
                else if (strValue.Length != 5)
                {
                    MessageBox.Show(this, "消费项编码必须为5位", "数据格式不正确");
                    e.Cancel = true;
                    //this.usParaSet1.MyDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "消费项编码必须为5位";
                    //e.Cancel = false;
                }
            }
            if (this.usParaSet1.MyDataGridView.Columns[e.ColumnIndex].HeaderText == "消费项名称")
            {
                if(string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    MessageBox.Show(this, "请输入消费项名称", "数据格式不正确");
                    e.Cancel = true;
                    //this.usParaSet1.MyDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "请输入消费项名称";
                    //e.Cancel = false;
                }
                else if (e.FormattedValue.ToString().Length > 15)
                {
                    MessageBox.Show(this, "消费项名称最长为15位", "数据格式不正确");
                    e.Cancel = true;
                    //this.usParaSet1.MyDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "消费项名称最长为15位";
                    //e.Cancel = false;
                }
            }
        }

        #endregion

        #region 会员级别
        private void BindAL()
        {
            var al = GetAL();
            this.usParaSet6.MyAMSEntities = amsContext;
            this.usParaSet6.MyDataSource = al;
            this.usParaSet6.MyReload.Click += new EventHandler(MyReload_Click6);

            this.usParaSet6.MyDataGridView.DefaultValuesNeeded += new DataGridViewRowEventHandler(MyDataGridView_DefaultValuesNeeded);

            this.usParaSet6.MyDataGridView.Columns["vcCommCode"].HeaderText = "会员级别代码";
            this.usParaSet6.MyDataGridView.Columns["vcCommName"].HeaderText = "会员级别名称";
            //this.usParaSet6.MyDataGridView.Columns["vcCommSign"].HeaderText = "会员级别";
            //this.usParaSet6.MyDataGridView.Columns["vcComments"].HeaderText = "会员级别";

            this.usParaSet6.MyDataGridView.Columns["ID"].Visible = false;
            this.usParaSet6.MyDataGridView.Columns["vcCommSign"].Visible = false;
            this.usParaSet6.MyDataGridView.Columns["vcComments"].Visible = false;
            this.usParaSet6.MyDataGridView.AutoGenerateColumns = false;
            this.usParaSet6.MyDataGridView.Columns["ID"].DisplayIndex = 0;
            this.usParaSet6.MyDataGridView.Columns["vcCommCode"].DisplayIndex = 1;
            this.usParaSet6.MyDataGridView.Columns["vcCommName"].DisplayIndex = 2;
            this.usParaSet6.MyDataGridView.Columns["vcCommSign"].DisplayIndex = 3;
            this.usParaSet6.MyDataGridView.Columns["vcComments"].DisplayIndex = 4;

        }

        void MyDataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["vcCommSign"].Value = "AL";
            e.Row.Cells["vcComments"].Value = "会员级别";
        }

        private object GetAL()
        {            
            return 
                from item in amsContext.tbCommCode
                        where item.vcCommSign == "AL"
                        orderby item.vcCommSign, item.vcCommCode
                        select item;
        }
        void MyReload_Click6(object sender, EventArgs e)
        {
            amsContext = new AMSEntities();
            var al = GetAL();
            this.usParaSet6.MyAMSEntities = amsContext;
            this.usParaSet6.MyDataSource = al;
            Helper.ShowInfo(this, "数据已重新加载");
        }
        #endregion
        #region 会员级别有消费相关参数
        private void BindGoodsRate()
        {
            this.usParaSet5.MyDataGridView.AutoGenerateColumns = true;
            //amsContext = new AMSEntities();
            var goodsRate = GetGoodsRate();
            this.usParaSet2.MyAMSEntities = amsContext;
            this.usParaSet2.MyDataSource = goodsRate;
            this.usParaSet2.MyReload.Click += new EventHandler(MyReload_Click2);

            //this.usParaSet2.MyDataGridView.Columns.Remove("vcAssLevel");
            //this.usParaSet2.MyDataGridView.Columns.Remove("vcGoodsCode");

            var goods =
                from item in amsContext.tbGoods orderby item.vcGoodsCode select item;

            DataGridViewComboBoxColumn GoodsCodecolumn = new DataGridViewComboBoxColumn();

            GoodsCodecolumn.Name = "vcGoodsCodeComments";
            GoodsCodecolumn.DataPropertyName = "vcGoodsCode";
            GoodsCodecolumn.HeaderText = "消费项";
            GoodsCodecolumn.ValueMember = "vcGoodsCode";
            GoodsCodecolumn.DisplayMember = "vcGoodsName";
            GoodsCodecolumn.DataSource = goods;
            //GoodsCodecolumn.Width = 200;
            //GoodsCodecolumn.DropDownWidth = 400;
            var commcode =
                from item in amsContext.tbCommCode where item.vcCommSign == "AL" orderby item.vcCommCode select item;
            DataGridViewComboBoxColumn AssLevelcolumn = new DataGridViewComboBoxColumn();
            
                AssLevelcolumn.Name = "vcAssLevelComments";
                AssLevelcolumn.DataPropertyName = "vcAssLevel";
                AssLevelcolumn.HeaderText = "会员级别";
                AssLevelcolumn.ValueMember = "vcCommCode";
                AssLevelcolumn.DisplayMember = "vcCommName";
                AssLevelcolumn.DataSource = commcode;
                //AssLevelcolumn.Width = 200;
                //AssLevelcolumn.DropDownWidth = 400;

            this.usParaSet2.MyDataGridView.Columns.Add(AssLevelcolumn);
            this.usParaSet2.MyDataGridView.Columns.Add(GoodsCodecolumn);
            this.usParaSet5.MyDataGridView.AutoGenerateColumns = false;

            this.usParaSet2.MyDataGridView.Columns["nGoodsRate"].HeaderText = "折扣下限";
            this.usParaSet2.MyDataGridView.Columns["vcComments"].HeaderText = "描述";
            this.usParaSet2.MyDataGridView.Columns["vcGoodsCode"].DisplayIndex = 0;
            this.usParaSet2.MyDataGridView.Columns["vcGoodsCode"].Visible = false;
            this.usParaSet2.MyDataGridView.Columns["vcGoodsCodeComments"].DisplayIndex = 1;
            this.usParaSet2.MyDataGridView.Columns["vcAssLevel"].DisplayIndex = 2;
            this.usParaSet2.MyDataGridView.Columns["vcAssLevel"].Visible = false;
            this.usParaSet2.MyDataGridView.Columns["vcAssLevelComments"].DisplayIndex = 3;
            this.usParaSet2.MyDataGridView.Columns["nGoodsRate"].DisplayIndex = 4;
            this.usParaSet2.MyDataGridView.Columns["vcComments"].DisplayIndex = 5;

        }

        private object GetGoodsRate()
        {
            //return
            //var gs = from goods in amsContext.tbGoods select goods.vcGoodsCode;
            var gr = from item in amsContext.tbGoodsRate
                     join gs in amsContext.tbGoods on item.vcGoodsCode equals gs.vcGoodsCode //--into gc
                     //from item2 in gc.DefaultIfEmpty()
                     orderby item.vcGoodsCode, item.vcAssLevel, item.nGoodsRate
                     select item;
            return gr;
            //string strQueryString = "SELECT VALUE gr FROM AMSEntities.tbGoodsRate AS gr WHERE gr.vcGoodsCode in (SELECT gs.vcGoodsCode FROM AMSEntities.tbGoods AS gs)";
            //ObjectQuery<tbGoodsRate> gr = new ObjectQuery<tbGoodsRate>(strQueryString, amsContext);
            //return gr;
        }
        void MyReload_Click2(object sender, EventArgs e)
        {
            amsContext = new AMSEntities();
            var goodsRate = GetGoodsRate();
            this.usParaSet2.MyAMSEntities = amsContext;
            this.usParaSet2.MyDataSource = goodsRate;
            Helper.ShowInfo(this, "数据已重新加载");
        }
        #endregion

        #region 充值优惠参数
        private void BindFillProm()
        {
            var fillProm = GetFillProm();
            this.usParaSet3.MyAMSEntities = amsContext;
            this.usParaSet3.MyDataSource = fillProm;
            this.usParaSet3.MyReload.Click += new EventHandler(MyReload_Click3);

            this.usParaSet3.MyDataGridView.Columns["ID"].HeaderText = "编号";
            this.usParaSet3.MyDataGridView.Columns["ID"].Visible = false;
            this.usParaSet3.MyDataGridView.Columns["iFloor"].HeaderText = "充值下限";
            this.usParaSet3.MyDataGridView.Columns["iCelling"].HeaderText = "充值上限";
            this.usParaSet3.MyDataGridView.Columns["iRate"].HeaderText = "优惠比例";
            this.usParaSet3.MyDataGridView.Columns["vcComments"].HeaderText = "备注";
        }
        private object GetFillProm()
        {
            return 
                from item in amsContext.tbFillProm orderby item.ID select item;
        }
        void MyReload_Click3(object sender, EventArgs e)
        {
            amsContext = new AMSEntities();
            var fillProm = GetFillProm();
            this.usParaSet3.MyAMSEntities = amsContext;
            this.usParaSet3.MyDataSource = fillProm;
            Helper.ShowInfo(this, "数据已重新加载");
        }
        #endregion

        #region 积分兑换项目
        private void BindCommCode()
        {
            var CommCode = GetCommCode();
            this.usParaSet4.MyAMSEntities = amsContext;
            this.usParaSet4.MyDataSource = CommCode;
            //this.usParaSet4.MyDataGridView.CellValidating += new DataGridViewCellValidatingEventHandler(MyDataGridView_CellValidating);
            this.usParaSet4.MyReload.Click += new EventHandler(MyReload_Click4);

            this.usParaSet4.MyDataGridView.Columns["ID"].Visible = false;
            this.usParaSet4.MyDataGridView.Columns["vcCommSign"].Visible = false;

            this.usParaSet4.MyDataGridView.Columns["vcCommCode"].HeaderText = "兑换项目代码";
            this.usParaSet4.MyDataGridView.Columns["vcCommName"].HeaderText = "兑换项目名称";
            this.usParaSet4.MyDataGridView.Columns["vcCommSign"].HeaderText = "兑换项目";            
            this.usParaSet4.MyDataGridView.Columns["vcComments"].HeaderText = "兑换项目分值";

            //this.usParaSet4.MyDataGridView.Columns["vcCommSign"].

        }
        private object GetCommCode()
        {
            return
                from item in amsContext.tbCommCode
                where item.vcCommSign == "EX" 
                orderby item.vcCommSign, item.vcCommCode select item;
        }
        void MyReload_Click4(object sender, EventArgs e)
        {
            amsContext = new AMSEntities();
            var CommCode = GetCommCode();
            this.usParaSet4.MyAMSEntities = amsContext;
            this.usParaSet4.MyDataSource = CommCode;
            Helper.ShowInfo(this, "数据已重新加载");
        }
        #endregion

        #region 操作员折扣率定义
        private void BindOperLevel()
        {
            this.usParaSet5.MyDataGridView.AutoGenerateColumns = true;
            var operLevel = GetOperLevel();
            this.usParaSet5.MyAMSEntities = amsContext;
            this.usParaSet5.MyDataSource = operLevel;
            this.usParaSet5.MyDataGridView.CellValidating += new DataGridViewCellValidatingEventHandler(MyDataGridView_CellValidating5);
            this.usParaSet5.MyReload.Click += new EventHandler(MyReload_Click5);

            this.usParaSet5.MyDataGridView.AutoGenerateColumns = false;
            this.usParaSet5.MyDataGridView.Columns["vcLevelName"].DisplayIndex = 0;
            this.usParaSet5.MyDataGridView.Columns["vcOperLevel"].DisplayIndex = 1;            
            this.usParaSet5.MyDataGridView.Columns["iRateFloor"].DisplayIndex = 2;                        

            this.usParaSet5.MyDataGridView.Columns["vcLevelName"].HeaderText = "折扣名称";
            this.usParaSet5.MyDataGridView.Columns["vcOperLevel"].HeaderText = "折扣编码";
            this.usParaSet5.MyDataGridView.Columns["iRateFloor"].HeaderText = "折扣最低限";            
        }
        private object GetOperLevel()
        {
            return
                from item in amsContext.tbOperLevel orderby item.vcOperLevel select item;
        }
        void MyReload_Click5(object sender, EventArgs e)
        {
            amsContext = new AMSEntities();
            var operLevel = GetOperLevel();
            this.usParaSet5.MyAMSEntities = amsContext;
            this.usParaSet5.MyDataSource = operLevel;
            Helper.ShowInfo(this, "数据已重新加载");
        }

        void MyDataGridView_CellValidating5(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.usParaSet5.MyDataGridView.Rows[e.RowIndex].IsNewRow) return;
            if (this.usParaSet5.MyDataGridView.Columns[e.ColumnIndex].HeaderText == "折扣编码")
            {

                string strValue = e.FormattedValue.ToString();
                if (!strValue.StartsWith("OL"))
                {
                    MessageBox.Show(this, "折扣编码需以OL开头", "数据格式不正确");
                    e.Cancel = true;
                    //this.usParaSet1.MyDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "消费项编码需以G开头";
                    //e.Cancel = false;
                }
                else if (strValue.Length != 5)
                {
                    MessageBox.Show(this, "折扣编码必须为5位", "数据格式不正确");
                    e.Cancel = true;
                    //this.usParaSet1.MyDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "消费项编码必须为5位";
                    //e.Cancel = false;
                }
            }
            if (this.usParaSet5.MyDataGridView.Columns[e.ColumnIndex].HeaderText == "折扣名称")
            {
                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    MessageBox.Show(this, "请输入折扣名称", "数据格式不正确");
                    e.Cancel = true;
                    //this.usParaSet1.MyDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "请输入消费项名称";
                    //e.Cancel = false;
                }
                else if (e.FormattedValue.ToString().Length > 10)
                {
                    MessageBox.Show(this, "折扣名称最长为10位", "数据格式不正确");
                    e.Cancel = true;
                    //this.usParaSet1.MyDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "消费项名称最长为15位";
                    //e.Cancel = false;
                }
            }
           
        }

        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tbCommCode af = amsContext.tbCommCode.FirstOrDefault(c => c.vcCommSign == "AF" && c.vcCommName == "Awoke");
                if (af != null)
                {
                    if (chkAF.Checked)
                        af.vcCommCode = "1";
                    else
                        af.vcCommCode = "0";
                }
                else
                {
                    af = new tbCommCode();
                    af.vcComments = "是否启用自动提醒";
                    af.vcCommName = "Awoke";
                    af.vcCommSign = "AF";
                    if (chkAF.Checked)
                        af.vcCommCode = "1";
                    else
                        af.vcCommCode = "0";
                    amsContext.AddTotbCommCode(af);
                }

                tbCommCode agf = amsContext.tbCommCode.FirstOrDefault(c => c.vcCommSign == "AGF" && c.vcCommName == "AgianFee");
                if (agf != null)
                {
                    agf.vcCommCode = txtAgf.Value.ToString();
                }
                else
                {
                    agf = new tbCommCode();
                    agf.vcComments = "补发卡工本费金额";
                    agf.vcCommName = "AgianFee";
                    agf.vcCommSign = "AGF";
                    agf.vcCommCode = txtAgf.Value.ToString();
                    amsContext.AddTotbCommCode(agf);
                }
                tbCommCode ak = amsContext.tbCommCode.FirstOrDefault(c => c.vcCommSign == "AK" && c.vcCommName == "Awoke");
                if (ak != null)
                {
                    ak.vcCommCode = txtAK.Value.ToString();
                }
                else
                {
                    ak = new tbCommCode();
                    ak.vcComments = "自动提醒积分分值";
                    ak.vcCommName = "Awoke";
                    ak.vcCommSign = "AK";
                    ak.vcCommCode = txtAK.Value.ToString();
                    amsContext.AddTotbCommCode(ak);
                }
                Helper.Save(amsContext);
                MessageBox.Show(this,"修改成功", "提示");
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            
            switch (e.TabPage.Name)
            {
                case "tabPage1":
                    BindGoods();
                    break;
                case "tabPage2":
                    BindGoodsRate();
                    break;
                case "tabPage3":
                    BindFillProm();
                    break;
                case "tabPage4":
                    tbCommCode af = amsContext.tbCommCode.FirstOrDefault(c => c.vcCommSign == "AF" && c.vcCommName == "Awoke");
                    if (af != null)
                    {
                        if (af.vcCommCode == "1")
                            chkAF.Checked = true;
                        else
                            chkAF.Checked = false;
                    }
                    else
                        chkAF.Checked = true;

                    tbCommCode agf = amsContext.tbCommCode.FirstOrDefault(c => c.vcCommSign == "AGF" && c.vcCommName == "AgianFee");
                    if (agf != null)
                        txtAgf.Value = Convert.ToDecimal(agf.vcCommCode);
                    tbCommCode ak = amsContext.tbCommCode.FirstOrDefault(c => c.vcCommSign == "AK" && c.vcCommName == "Awoke");
                    if (ak != null)
                        txtAK.Value = Convert.ToDecimal(ak.vcCommCode);
                    BindCommCode();
                    break;
                case "tabPage5":
                    BindOperLevel();
                    break;
                case "tabPage6":
                    BindAL();
                    break;
            }
        }

        private void frmParaSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (amsContext != null)
            //{
            //    amsContext.Dispose();
            //}
        }

    }
   
}

