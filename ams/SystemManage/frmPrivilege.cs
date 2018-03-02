using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ams.Common;
namespace ams.Associator
{
    public partial class frmPrivilege : Form
    {
        public frmPrivilege()
        {
            InitializeComponent();
        }

        private void SetFunc()
        {
            treeView2.CheckBoxes = true;
            treeView2.BeginUpdate();
            treeView2.Nodes.Clear();
            var menu1 = from item in ConstApp.strmenu where item[0]=="0" select item;
            foreach (string[] strMenu1 in menu1)
            {
                TreeNode tn = new TreeNode();
                tn.Text = strMenu1[3];
                tn.Tag = strMenu1[2];                
                var menu2 = from item in ConstApp.strmenu where item[0] == "1" && item[1]==strMenu1[2] select item;
                foreach (string[] strMenu2 in menu2)
                {
                    TreeNode tn2 = new TreeNode();
                    tn2.Text = strMenu2[3];
                    tn2.Tag = strMenu2[1] + strMenu2[2];
                    tn.Nodes.Add(tn2);
                }
                treeView2.Nodes.Add(tn);
            }
            treeView2.EndUpdate();
            treeView2.ExpandAll();

            
        }
        private void SetRole(AMSEntities amsContext)
        {
            
            IOrderedQueryable<tbLimit> limit = amsContext.tbLimit.OrderBy(l => l.vcLimitCode);
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            TreeNode tnRole = new TreeNode();
            tnRole.Name = "limit";
            tnRole.Text = "角色";
            tnRole.ImageIndex = 0;
            tnRole.ContextMenuStrip = this.contextMenuStrip1;
            foreach (tbLimit li in limit)
            {
               
                TreeNode tn = new TreeNode();
                tn.Name = li.vcLimitCode;
                tn.Text = li.vcLimitName;
                tn.ImageIndex = -1;
                tn.ContextMenuStrip = this.contextMenuStrip2;
                tnRole.Nodes.Add(tn);
            }
            treeView1.Nodes.Add(tnRole);
            treeView1.EndUpdate();
            treeView1.ExpandAll();
           
        }

        private void frmPrivilege_Load(object sender, EventArgs e)
        {
            using (AMSEntities amsContext = new AMSEntities())
            {
                try
                {
                    SetFunc();
                    SetRole(amsContext);
                }
                catch (Exception ex)
                {
                    ErrorLog.Write(this, ex, "数据初始化出错");
                }
            }
           
        }

        private void addRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //添加角色
            frmAddRole addRole = new frmAddRole();
            addRole.ShowDialog(this);
            frmPrivilege_Load(null, null);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //e.Node.Name
            if (e.Node.Name != "limit")
            {
                using (AMSEntities amsContext = new AMSEntities())
                {
                    try
                    {
                        IQueryable<tbOperLimit> limit = amsContext.tbOperLimit.Where(l => l.vcLimitCode == e.Node.Name);
                        foreach (TreeNode tn in treeView2.Nodes)
                        {
                            int i = 0;
                            foreach (TreeNode tn2 in tn.Nodes)
                            {
                                string strMenu = Convert.ToString(tn2.Tag);
                                tbOperLimit lt = limit.FirstOrDefault(l => l.vcMenu1==strMenu.Substring(0,1) && l.vcMenu2 == strMenu.Substring(1,1));
                                if (lt == null)
                                    tn2.Checked = false;
                                else
                                {
                                    i++;
                                    tn2.Checked = true;
                                }
                            }
                            if (i > 0)
                                tn.Checked = true;
                            else
                                tn.Checked = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.Write(this, ex);
                    }
                }
            }
        }

        private void treeView2_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag.ToString().Length > 1 && treeView1.SelectedNode.Name !="limit")
            {
                tbOperLimit ol = new tbOperLimit();
                ol.vcLimitCode = treeView1.SelectedNode.Name;
                ol.vcMenu1 = e.Node.Tag.ToString().Substring(0, 1);
                ol.vcMenu2 = e.Node.Tag.ToString().Substring(1, 1);
                using (AMSEntities amsContext = new AMSEntities())
                {
                    try
                    {
                        //amsContext.s
                        tbOperLimit ol2 = amsContext.tbOperLimit.FirstOrDefault(l => l.vcLimitCode == ol.vcLimitCode && l.vcMenu1 == ol.vcMenu1 && l.vcMenu2 == ol.vcMenu2);

                        if (e.Node.Checked)
                        {
                            if (ol2 == null)
                            {
                                amsContext.AddTotbOperLimit(ol);
                                amsContext.SaveChanges();
                            }
                        }
                        else
                        {
                            if (ol2 != null)
                            {
                                amsContext.DeleteObject(ol2);
                                amsContext.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.Write(this, ex);
                    }
                   
                }
            }
        }

        private void deleteRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //删除角色
            try
            {
                TreeNode tn = treeView1.SelectedNode;
                if (tn.Name == "OP001")
                    throw new Exception("管理员权限为系统权限，不能删除！");

                DialogResult dr = MessageBox.Show(this, "是否删除\"" + tn.Text + "\"角色", "删除角色", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {

                    using (AMSEntities amsContext = new AMSEntities())
                    {



                        //tbLimit limit = new tbLimit();
                        //limit.vcLimitCode = tn.Name;
                        //limit.vcLimitName = tn.Text;

                        tbLimit limit = amsContext.tbLimit.FirstOrDefault(l => l.vcLimitCode == tn.Name);
                        amsContext.DeleteObject(limit);

                        var opers = amsContext.tbOperLimit.Where(ol => ol.vcLimitCode == tn.Name);
                        foreach (tbOperLimit operLimit in opers)
                        {
                            amsContext.DeleteObject(operLimit);
                        }
                        amsContext.SaveChanges();
                        SetRole(amsContext);
                        MessageBox.Show(this, "角色删除成功", "删除角色");

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }
    }
}
