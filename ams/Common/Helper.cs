using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Windows.Forms;
using DataDynamics.ActiveReports.Viewer;
using System.Reflection;
using System.Collections;
using ams.rpt;
using System.Runtime.InteropServices;
using Card;
namespace ams.Common
{
    class Helper
    {
        public static void Save(AMSEntities amsContext)
        {
            try
            {
                amsContext.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {                
                throw new Exception("并发冲突，请重载数据");
            }
            catch (UpdateException uex)
            {
                if(uex.InnerException == null)
                    throw new Exception("更新错误：" + uex.Message);
                else
                    throw new Exception("更新错误：" + uex.InnerException.Message);
            }
            catch (Exception ex)
            {
                ErrorLog.Write(ex);
                throw ex;
            }
        }

        public static bool IsNum(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                    return false;
            }
            return true;
        }

        public static void ShowInfo(IWin32Window owner, string strText)
        {
            MessageBox.Show(owner, strText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public static void ShowError(IWin32Window owner, string strText)
        {
            MessageBox.Show(owner, strText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="MyDataSource">数据源</param>
        /// <param name="MyFormat">格式文件</param>
        public static void MyPrint(object MyDataSource, string MyFormat)
        {
            rpxBase rpt = new rpxBase();
            //rpt.SetLicense("admin,admin,123456,VF4UHHMJ4WOHHV77I8HH");
            rpt.LoadLayout(MyFormat);
            rpt.DataSource = MyDataSource;
            rpt.Run();
            rpt.Document.Print(false, true, true);
        }
        /// <summary>
        /// 打印带预览
        /// </summary>
        /// <param name="MyDataSource">数据源</param>
        /// <param name="MyFormat">格式文件</param>
        /// <param name="showPreview">是否预览</param>
        public static void MyPrint(object MyDataSource, string MyFormat,bool showPreview)
        {
            //Properties.Resources.ground
            rpxBase rpt = new rpxBase();
            //rpt.SetLicense("admin,admin,123456,VF4UHHMJ4WOHHV77I8HH");
            rpt.LoadLayout(MyFormat);
            rpt.DataSource = MyDataSource;
            rpt.Run();
            if (showPreview)
            {
                frmPrintBase preview = new frmPrintBase();
                preview.MyRpt = rpt;
                preview.MyPreview();
                preview.ShowDialog();
            }
            else
            {
                rpt.Document.Print(false, true, true);
            }
           
        }
        public static bool MyPrint(List<tbBillInvoice> invoice, List<tbBillList> list)
        {
            for (int i = 0; i < invoice.Count; i++)
            {
                tbBillInvoice bill = invoice[i];
                string strTypeName = "";
                switch (bill.vcBillType)
                {
                    case "BI001":
                        strTypeName = "会员卡消费帐单";
                        break;
                    case "BI002":
                        strTypeName = "会员卡充值帐单";
                        break;
                    case "BI003":
                        strTypeName = "会员卡积分兑换帐单";
                        break;
                    case "BI004":
                        strTypeName = "会员卡挂失帐单";
                        break;
                    case "BI005":
                        strTypeName = "会员卡补卡帐单";
                        break;
                    case "BI006":
                        strTypeName = "会员卡退卡帐单";
                        break;                    
                }
                bill.vcBillType = strTypeName;
            }
            rptBIModle rpt = new rptBIModle(list);
            rpt.SetLicense("admin,admin,123456,VF4UHHMJ4WOHHV77I8HH");
            string baseDir = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath;
            rpt.LoadLayout(baseDir+"//rpx//BIModle.rpx");
            rpt.DataSource = invoice;
            rpt.Run();
            return rpt.Document.Print(false, true, false);
        }

        //[DllImport("Card.dll", EntryPoint = "PutCard", SetLastError = true,
        //     CharSet = CharSet.Auto, ExactSpelling = false,
        //     CallingConvention = CallingConvention.StdCall)]        
        //public static extern int PutCard(string strAssCardNo);
        //[DllImport("Card.dll", EntryPoint = "ReadCard", SetLastError = true,
        //    CharSet = CharSet.Auto, ExactSpelling = false,
        //    CallingConvention = CallingConvention.StdCall)]
        //public static extern int ReadCard(ref string ReadCard);

        public static int PutCard(string strAssCardNo)
        {
             Card.M1CardClass m1 = new M1CardClass();
             return m1.PutCard(strAssCardNo);
        }
        public static int ReadCard(ref string strAssCardNo)
        {
            Card.M1CardClass m1 = new M1CardClass();
            return m1.ReadCard(ref strAssCardNo);
        }
        public static string PutCardError(int ret)
        {
           
            string strRet = "";
            switch (ret)
            {
                case 1:
                    strRet = "设备初始化端口失败";
                    break;
                case 2:
                    strRet = "装载密码出错_A";
                    break;
                case 3:
                    strRet = "装载密码出错_B";
                    break;
                case 4:
                    strRet = "获取会员卡序号出错";
                    break;
                case 5:
                    strRet = "该卡片不属于本系统所使用";
                    break;
                case 6:
                    strRet = "密码验证错误";
                    break;
                case 7:
                    strRet = "发卡初始化失败";
                    break;
                case 8:
                    strRet = "发卡初始化失败";
                    break;
                case 9:
                    strRet = "读数据失败";
                    break;
                case 10:
                    strRet = "发卡操作失败，该卡暂不要使用";
                    break;
                default:
                    strRet = "其他异常";
                    break;
            }
            return strRet;
        }

        public static string ReadCardError(int ret)
        {
            string strRet = "";
            switch (ret)
            {
                case 1:
                    strRet = "设备初始化端口失败";
                    break;
                case 2:
                    strRet = "装载密码出错_B";
                    break;
                case 3:
                    strRet = "获取会员卡序号出错";
                    break;
                case 4:
                    strRet = "该卡片不属于本系统所使用";
                    break;
                case 5:
                    strRet = "装载密码出错_A";
                    break;
                case 6:
                    strRet = "密码不正确";
                    break;
                case 7:
                    strRet = "读取卡号错误";
                    break;
                case 8:
                    strRet = "所读卡卡号为空";
                    break;
                default:
                    strRet = "其他异常";
                    break;
            }
            return strRet;
        }
    }
}
