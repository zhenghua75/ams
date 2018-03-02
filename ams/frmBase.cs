using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using ams.Common;

namespace ams
{
    public partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();
        }

        #region 导出到Excel
        //去掉DagaGrid的HeadText不符合做为列名称规范的字符
        protected string replace(string str)
        {
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("-", "");
            return str;
        }

        private string repText(string str)
        {
            str = str.Replace("'", "");
            return str;
        }

        protected void ExportToExcel(string table,string strTableName)
        {
            this.Cursor = Cursors.WaitCursor;
            OleDbConnection con = new OleDbConnection();
            bool sucess = false;
            string file = "";
            try
            {
                string strApp = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString();
                string strDir = strApp.Substring(0, strApp.LastIndexOf(@"\"));
                file = strDir + "\\XX报表.xls";
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
                con.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file + ";Extended Properties=Excel 8.0;";
                con.Open();
                OleDbCommand com = new OleDbCommand();
                com.Connection = con;
                string name="Table1";
                if(strTableName!="")
                    name = strTableName;
                foreach (System.Windows.Forms.Control ctl in this.Controls)
                {
                    if (ctl is System.Windows.Forms.DataGridView)
                    {
                        DataGridView dgv = ctl as DataGridView;
                        if (dgv.Rows.Count > 0)
                        {
                            if (table.Length > 0)
                                table = table.Substring(0, table.Length - 1);
                            table = "create table " + name + " (" + table + ")";
                            com.CommandText = table;
                            com.ExecuteNonQuery();//创建表结构
                            for (int i = 0; i < dgv.Rows.Count; i++)
                            {
                                string row = "";
                                for (int j = 0; j < dgv.Columns.Count; j++)
                                {
                                    if (dgv.Columns[j].Width > 0 && dgv.Rows[i].Cells[j].Value!=null)
                                        row += "'" + this.repText(dgv.Rows[i].Cells[j].Value.ToString()) + "',";
                                    else
                                        row += "'',";
                                }
                                row = row.Substring(0, row.Length - 1);
                                row = "insert into " + name + " values(" + row + ")";
                                com.CommandText = row;
                                com.ExecuteNonQuery();//插入数据
                            }
                            sucess = true;
                        }
                        else
                        {
                            MessageBox.Show("表格【" + name + "】没有数据可以导出!", "系统提示", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex, "导出出错，请重试！");
            }
            finally
            {
                con.Close();
                if (sucess)
                {
                    System.Diagnostics.ProcessStartInfo helpFile = new System.Diagnostics.ProcessStartInfo(file);
                    System.Diagnostics.Process.Start(helpFile);
                }
            }
            this.Cursor = Cursors.Default;
        }
        #endregion
    }
}
