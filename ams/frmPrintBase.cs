using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ams
{
    public partial class frmPrintBase : Form
    {
        /// <summary>
        /// 报表
        /// </summary>
        public rpxBase MyRpt { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public object MyDataSource { get; set; }
        /// <summary>
        /// 输出路径
        /// </summary>
        public string MyExportPath { get; set; }
        /// <summary>
        /// 输出格式 HTML  PDF RTF Text TIFF XLS
        /// </summary>
        public string MyExportFormat { get; set; }
        /// <summary>
        /// 报表布局文件
        /// </summary>
        public string MyLayOutPath { get; set; }
        public frmPrintBase()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化报表
        /// </summary>
        public void InitRpt()
        {
            MyRpt.LoadLayout(MyLayOutPath);
            MyRpt.DataSource = MyDataSource;
            MyRpt.Run();
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="showPrintDialog">是否显示打印对话框</param>
        /// <param name="showPrintProgressDialog">是否显示打印进度条</param>
        /// <param name="usePrintingThread">是否使用打印线程</param>
        public void MyPrint(bool showPrintDialog,bool showPrintProgressDialog,bool usePrintingThread)
        {           
            MyRpt.Document.Print(showPrintDialog, showPrintProgressDialog, usePrintingThread);
        }
        /// <summary>
        /// 导出
        /// </summary>
        public void MyExport()
        {           
            switch (MyExportFormat)
            {
                case "HTML":
                    this.htmlExport1.Export(MyRpt.Document, MyExportPath);
                    break;
                case "PDF":
                    this.pdfExport1.Export(MyRpt.Document, MyExportPath);
                    break;
                case "RTF":
                    this.rtfExport1.Export(MyRpt.Document, MyExportPath);
                    break;
                case "Text":
                    this.textExport1.Export(MyRpt.Document, MyExportPath);
                    break;
                case "TIFF":
                    this.tiffExport1.Export(MyRpt.Document, MyExportPath);
                    break;
                case "XLS":
                    this.xlsExport1.Export(MyRpt.Document, MyExportPath);
                    break;
            }

        }
        /// <summary>
        /// 打印预览初始化
        /// </summary>
        public void MyPreview()
        {
            this.viewer1.Document = MyRpt.Document;
        }
    }
}
