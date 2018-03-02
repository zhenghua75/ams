using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace ams.rpt
{
    /// <summary>
    /// Summary description for rptBillList.
    /// </summary>
    public partial class rptBillList : DataDynamics.ActiveReports.ActiveReport3
    {
        private System.Drawing.Font ftHeading1 = new System.Drawing.Font("SimSun", 16, System.Drawing.FontStyle.Bold);
        private System.Drawing.Font ftHeading2 = new System.Drawing.Font("SimSun", 14, System.Drawing.FontStyle.Bold);
        private System.Drawing.Font ftHeading3 = new System.Drawing.Font("SimSun", 10, System.Drawing.FontStyle.Bold);
        private System.Drawing.Font ftNormal = new System.Drawing.Font("SimSun", 9);
        public rptBillList()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            ApplyStyle();
        }
        public void ApplyStyle()
        {
            int i, j;
            DataDynamics.ActiveReports.ARControl arc;

            #region Apply Style on Report

            //设置Margins.Left和Margins.Right，使报表内容居中
            if (this.PageSettings.Orientation.Equals(PageOrientation.Landscape))
                this.PageSettings.Margins.Left = (this.PageSettings.PaperHeight - this.PrintWidth) / 2;
            else
                this.PageSettings.Margins.Left = (this.PageSettings.PaperWidth - this.PrintWidth) / 2;
            this.PageSettings.Margins.Right = this.PageSettings.Margins.Left;

            for (j = 0; j < this.Sections.Count; j++)
            {
                for (i = 0; i < this.Sections[j].Controls.Count; i++)
                {
                    arc = (DataDynamics.ActiveReports.ARControl)this.Sections[j].Controls[i];
                    if (arc is DataDynamics.ActiveReports.TextBox)
                    {
                        DataDynamics.ActiveReports.TextBox tb = (DataDynamics.ActiveReports.TextBox)arc;
                        switch (tb.ClassName)
                        {
                            case "Heading1":
                                tb.Font = ftHeading1;
                                break;
                            case "Heading2":
                                tb.Font = ftHeading2;
                                break;
                            case "Heading3":
                                tb.Font = ftHeading3;
                                break;
                            case "Normal":
                                tb.Font = ftNormal;
                                break;
                        }
                    }
                    else if (arc is DataDynamics.ActiveReports.Label)
                    {
                        DataDynamics.ActiveReports.Label lb = (DataDynamics.ActiveReports.Label)arc;
                        switch (lb.ClassName)
                        {
                            case "Heading1":
                                lb.Font = ftHeading1;
                                //设置Heading1居中
                                lb.Left = 0;
                                lb.Top = 0;
                                if (this.PageSettings.Orientation.Equals(PageOrientation.Landscape))
                                    lb.Width = this.PageSettings.PaperHeight - this.PageSettings.Margins.Left - this.PageSettings.Margins.Right;
                                else
                                    lb.Width = this.PageSettings.PaperWidth - this.PageSettings.Margins.Left - this.PageSettings.Margins.Right;
                                lb.Alignment = TextAlignment.Center;
                                break;
                            case "Heading2":
                                lb.Font = ftHeading2;
                                break;
                            case "Heading3":
                                lb.Font = ftHeading3;
                                break;
                            case "Normal":
                                lb.Font = ftNormal;
                                break;
                        }
                    }
                }
            }
            #endregion

        }
    }
}
