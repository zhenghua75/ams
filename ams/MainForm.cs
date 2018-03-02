using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ams.Common;
using ams.Associator;
using ams.SystemManage;
using System.Diagnostics;

namespace ams
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.MdiClient bgMDIClient;
        private frmPrivilege privilege;
        private frmOper operManage;
        private frmParaSet paraSet;
        private frmAssociator associator;
        private frmAssociatorCard associatorCard;
        private frmAssCons assConsumption;
        private frmConsRollBack consrollback;
        private frmAssIgExchange assIgExchange;
        private frmFillQuery fillfeeQuery;
        private frmAssInfoQuery assinfoquery;
        private frmConsQuery consquery;
        private frmIgQuery igquery;
        private frmConsSum conssum;
        private frmBusiLogQuery busilogquery;
        private frmHelp fhelp;
        private frmBillReprint billreprint;

        public MainForm()
        {
            InitializeComponent();
        }

        
        #region 视图
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }
        #endregion

        #region 窗口
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }        
        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion
        private void MainForm_Load(object sender, EventArgs e)
        {
            MDI_Set();
            SetStatus();
            SetPrivilege();
        }
        private void SetStatus()
        {
            this.statusStrip.Items.Add("欢迎使用会员管理系统");
            this.statusStrip.Items.Add(new ToolStripSeparator());
            this.statusStrip.Items.Add(GlobalParams.oper.vcOperName);
            this.statusStrip.Items.Add(new ToolStripSeparator());
            this.statusStrip.Items.Add(GlobalParams.strClass);
            this.statusStrip.Items.Add(new ToolStripSeparator());
            this.statusStrip.Items.Add(DateTime.Now.ToString("yyyy年MM月dd日"));
        }
        #region 设置权限
        private void SetPrivilege()
        {
            try
            {
                string strLimitCode = GlobalParams.oper.vcLimit;
                foreach (ToolStripMenuItem tsi in this.menuStrip.Items)
                {
                    foreach (ToolStripItem tsmi in tsi.DropDownItems)
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
                }

                foreach (ToolStripItem tsmi in this.toolStrip.Items)
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
                this.registerToolStripMenuItem.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
        private bool IsLimit(tbOperLimit ol)
        {
            foreach (tbOperLimit l in GlobalParams.OperLimit)
            {
                if (ol.vcLimitCode == l.vcLimitCode && ol.vcMenu1 == l.vcMenu1 && ol.vcMenu2 == l.vcMenu2)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region 设置背景
        private void MDI_Set()
        {
            foreach (System.Windows.Forms.Control myControl in this.Controls)
            {
                if (myControl.GetType().ToString() == "System.Windows.Forms.MdiClient")
                {
                    bgMDIClient = (System.Windows.Forms.MdiClient)myControl;
                    break;
                }
            }
            if (bgMDIClient.ClientSize.Width > 0 && bgMDIClient.ClientSize.Height > 0)
            {
                
                //Assembly asm = Assembly.GetExecutingAssembly();
                //Stream imgStream = asm.GetManifestResourceStream("ams.Resources.Logo.jpg");
                Bitmap MDIbg_Image = new Bitmap(Properties.Resources.ground);//new Bitmap("logo.jpg");
                System.Drawing.Bitmap myImg = new Bitmap(bgMDIClient.ClientSize.Width, bgMDIClient.ClientSize.Height);
                System.Drawing.Graphics myGraphics = System.Drawing.Graphics.FromImage(myImg);
                myGraphics.Clear(this.BackColor);

                //标志       
                myGraphics.Clear(this.BackColor);
                //定位
                int myX, myY;
                myX = (myImg.Width - MDIbg_Image.Width) / 2;
                myY = (myImg.Height - MDIbg_Image.Height) / 2;
                myGraphics.DrawImage(MDIbg_Image, myX, myY, MDIbg_Image.Width, MDIbg_Image.Height);
                bgMDIClient.BackgroundImage = myImg;
                myGraphics.Dispose();
            }
        }
        #endregion
        #region 退出
        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        private void PrivilegeManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //操作员权限
            if (privilege == null || privilege.IsDisposed)
            {
                privilege = new frmPrivilege();
                privilege.MdiParent = this;
                privilege.WindowState = FormWindowState.Maximized;
                privilege.Show();
            }
            else
            {
                privilege.Activate();
            }
        }

        private void OperManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //操作员管理
            if (operManage == null || operManage.IsDisposed)
            {
                operManage = new frmOper();
                operManage.MdiParent = this;
                operManage.WindowState = FormWindowState.Maximized;
                operManage.Show();
            }
            else
            {
                operManage.Activate();
            }
        }

        private void pwdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPwd pwd = new frmPwd();
            pwd.ShowDialog(this);
        }

        private void paraSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //参数设置
            if (paraSet == null || paraSet.IsDisposed)
            {
                paraSet = new frmParaSet();
                paraSet.MdiParent = this;
                paraSet.WindowState = FormWindowState.Maximized;
                paraSet.Show();
            }
            else
            {
                paraSet.Activate();
            }

        }

        private void BasicDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (associator == null || associator.IsDisposed)
            {
                associator = new frmAssociator();
                associator.MdiParent = this;
                associator.WindowState = FormWindowState.Maximized;
                associator.Show();
            }
            else
            {
                associator.Activate();
            }
        }

        private void AssCardSetMenu_Click(object sender, EventArgs e)
        {
            if (associatorCard == null || associatorCard.IsDisposed)
            {
                associatorCard = new frmAssociatorCard();
                associatorCard.MdiParent = this;
                associatorCard.WindowState = FormWindowState.Maximized;
                associatorCard.Show();
            }
            else
            {
                associatorCard.Activate();
            }
        }

        private void ConsumptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (assConsumption == null || assConsumption.IsDisposed)
            {
                assConsumption = new frmAssCons();
                assConsumption.MdiParent = this;
                assConsumption.WindowState = FormWindowState.Maximized;
                assConsumption.Show();
            }
            else
            {
                assConsumption.Activate();
            }
        }

        private void ConsBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (consrollback == null || consrollback.IsDisposed)
            {
                consrollback = new frmConsRollBack();
                consrollback.MdiParent = this;
                consrollback.WindowState = FormWindowState.Normal;
                consrollback.StartPosition = FormStartPosition.CenterScreen;
                consrollback.Show();
            }
            else
            {
                consrollback.Activate();
            }
        }

        private void IntegralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (assIgExchange == null || assIgExchange.IsDisposed)
            {
                assIgExchange = new frmAssIgExchange();
                assIgExchange.MdiParent = this;
                assIgExchange.WindowState = FormWindowState.Normal;
                assIgExchange.StartPosition = FormStartPosition.CenterScreen;
                assIgExchange.Show();
            }
            else
            {
                assIgExchange.Activate();
            }
        }

        private void FillReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fillfeeQuery == null || fillfeeQuery.IsDisposed)
            {
                fillfeeQuery = new frmFillQuery();
                fillfeeQuery.MdiParent = this;
                fillfeeQuery.WindowState = FormWindowState.Maximized;
                fillfeeQuery.StartPosition = FormStartPosition.CenterScreen;
                fillfeeQuery.Show();
            }
            else
            {
                fillfeeQuery.Activate();
            }
        }

        private void menuAddAss_Click(object sender, EventArgs e)
        {
            frmAss ass = new frmAss();
            ass.OperType = "ADD";
            ass.MinimizeBox = false;
            ass.MaximizeBox = false;
            ass.ShowDialog();
        }

        private void menuModifyCardPwd_Click(object sender, EventArgs e)
        {
            frmModifyCardPwd frmass = new frmModifyCardPwd();
            frmass.MinimizeBox = false;
            frmass.MaximizeBox = false;
            frmass.ShowDialog();
        }

        private void menuCardFillFee_Click(object sender, EventArgs e)
        {
            frmCardFillFee fillFee = new frmCardFillFee();
            fillFee.ControlBox = false;
            fillFee.ShowDialog();
        }

        private void AssInfoReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (assinfoquery == null || assinfoquery.IsDisposed)
            {
                assinfoquery = new frmAssInfoQuery();
                assinfoquery.MdiParent = this;
                assinfoquery.WindowState = FormWindowState.Maximized;
                assinfoquery.StartPosition = FormStartPosition.CenterScreen;
                assinfoquery.Show();
            }
            else
            {
                assinfoquery.Activate();
            }
        }

        private void ConsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (consquery == null || consquery.IsDisposed)
            {
                consquery = new frmConsQuery();
                consquery.MdiParent = this;
                consquery.WindowState = FormWindowState.Maximized;
                consquery.StartPosition = FormStartPosition.CenterScreen;
                consquery.Show();
            }
            else
            {
                consquery.Activate();
            }
        }

        private void IgReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (igquery == null || igquery.IsDisposed)
            {
                igquery = new frmIgQuery();
                igquery.MdiParent = this;
                igquery.WindowState = FormWindowState.Maximized;
                igquery.StartPosition = FormStartPosition.CenterScreen;
                igquery.Show();
            }
            else
            {
                igquery.Activate();
            }
        }

        private void ClassReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conssum == null || conssum.IsDisposed)
            {
                conssum = new frmConsSum();
                conssum.MdiParent = this;
                conssum.WindowState = FormWindowState.Maximized;
                conssum.StartPosition = FormStartPosition.CenterScreen;
                conssum.Show();
            }
            else
            {
                conssum.Activate();
            }
        }

        private void BusiLogReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (busilogquery == null || busilogquery.IsDisposed)
            {
                busilogquery = new frmBusiLogQuery();
                busilogquery.MdiParent = this;
                busilogquery.WindowState = FormWindowState.Maximized;
                busilogquery.StartPosition = FormStartPosition.CenterScreen;
                busilogquery.Show();
            }
            else
            {
                busilogquery.Activate();
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fhelp == null || fhelp.IsDisposed)
            {
                fhelp = new frmHelp();
                fhelp.MdiParent = this;
                fhelp.WindowState = FormWindowState.Maximized;
                fhelp.StartPosition = FormStartPosition.CenterScreen;
                fhelp.Show();
            }
            else
            {
                fhelp.Activate();
            }
        }

        private void autoUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("AutoUpdate.exe");
        }

        private void BillPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (billreprint == null || billreprint.IsDisposed)
            {
                billreprint = new frmBillReprint();
                billreprint.MdiParent = this;
                billreprint.WindowState = FormWindowState.Maximized;
                billreprint.StartPosition = FormStartPosition.CenterScreen;
                billreprint.Show();
            }
            else
            {
                billreprint.Activate();
            }
        }
    }
}
