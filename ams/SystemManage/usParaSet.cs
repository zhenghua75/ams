using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Linq;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ams.Common;
namespace ams.Associator
{
    public partial class usParaSet : UserControl
    {
        public usParaSet()
        {
            InitializeComponent();
        }

        public AMSEntities MyAMSEntities { get; set; }
        public BindingSource MyBindingSource
        {
            get
            {
                return this.bindingSource1;
            }
        }

        public ToolStripButton MyReload
        {
            get
            {
                return this.bindingNavigatorReLoadItem;
            }
        }
        public object MyDataSource
        {
            set
            {
                bindingSource1.DataSource = value;
                this.dataGridView1.DataSource = bindingSource1;
                this.bindingNavigator1.BindingSource = bindingSource1;
            }
        }
        public DataGridView MyDataGridView
        {
            get
            {
                return this.dataGridView1;
            }
        }

        private void usParaSet_Load(object sender, EventArgs e)
        {            
            //this.dataGridView1.AutoResizeColumns();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(this, e.Exception.Message, "数据格式不正确");            
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否保存？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    this.Validate();//进行自动验证
                    if (!this.dataGridView1.EndEdit())//结束编辑
                        throw new Exception("内容错误");
                     Helper.Save(MyAMSEntities);
                        MessageBox.Show(this, "保存成功", "提示");
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "提示");
                    ErrorLog.Write(ex);
                }

            }
        }

        private void bindingNavigatorRefreshItem_Click(object sender, EventArgs e)
        {
            this.dataGridView1.CancelEdit();
        }

        private void bindingNavigatorDeleteItem_MouseDown(object sender, MouseEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.bindingSource1.RemoveCurrent();
            }
        }

    }
}
