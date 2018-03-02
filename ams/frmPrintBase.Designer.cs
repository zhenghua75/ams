namespace ams
{
    partial class frmPrintBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintBase));
            this.htmlExport1 = new DataDynamics.ActiveReports.Export.Html.HtmlExport();
            this.pdfExport1 = new DataDynamics.ActiveReports.Export.Pdf.PdfExport();
            this.rtfExport1 = new DataDynamics.ActiveReports.Export.Rtf.RtfExport();
            this.textExport1 = new DataDynamics.ActiveReports.Export.Text.TextExport();
            this.tiffExport1 = new DataDynamics.ActiveReports.Export.Tiff.TiffExport();
            this.xlsExport1 = new DataDynamics.ActiveReports.Export.Xls.XlsExport();
            this.panel1 = new System.Windows.Forms.Panel();
            this.viewer1 = new DataDynamics.ActiveReports.Viewer.Viewer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtfExport1
            // 
            this.rtfExport1.EnableShapes = false;
            // 
            // textExport1
            // 
            this.textExport1.Encoding = ((System.Text.Encoding)(resources.GetObject("textExport1.Encoding")));
            // 
            // tiffExport1
            // 
            this.tiffExport1.Dither = false;
            // 
            // xlsExport1
            // 
            this.xlsExport1.Tweak = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.viewer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 345);
            this.panel1.TabIndex = 0;
            // 
            // viewer1
            // 
            this.viewer1.BackColor = System.Drawing.SystemColors.Control;
            this.viewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer1.Document = new DataDynamics.ActiveReports.Document.Document("ARNet Document");
            this.viewer1.Location = new System.Drawing.Point(0, 0);
            this.viewer1.Name = "viewer1";
            this.viewer1.ReportViewer.CurrentPage = 0;
            this.viewer1.ReportViewer.MultiplePageCols = 3;
            this.viewer1.ReportViewer.MultiplePageRows = 2;
            this.viewer1.ReportViewer.ViewType = DataDynamics.ActiveReports.Viewer.ViewType.Normal;
            this.viewer1.Size = new System.Drawing.Size(473, 345);
            this.viewer1.TabIndex = 0;
            this.viewer1.TableOfContents.Text = "Table Of Contents";
            this.viewer1.TableOfContents.Width = 200;
            this.viewer1.TabTitleLength = 35;
            this.viewer1.Toolbar.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // frmPrintBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 345);
            this.Controls.Add(this.panel1);
            this.Name = "frmPrintBase";
            this.Text = "打印预览";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DataDynamics.ActiveReports.Export.Html.HtmlExport htmlExport1;
        private DataDynamics.ActiveReports.Export.Pdf.PdfExport pdfExport1;
        private DataDynamics.ActiveReports.Export.Rtf.RtfExport rtfExport1;
        private DataDynamics.ActiveReports.Export.Text.TextExport textExport1;
        private DataDynamics.ActiveReports.Export.Tiff.TiffExport tiffExport1;
        private DataDynamics.ActiveReports.Export.Xls.XlsExport xlsExport1;
        private System.Windows.Forms.Panel panel1;
        private DataDynamics.ActiveReports.Viewer.Viewer viewer1;

    }
}