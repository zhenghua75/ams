using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace ams
{
    [RunInstaller(true)]
    public partial class amswinsetup : Installer
    {
        public amswinsetup()
        {
            InitializeComponent();
        }

        //public override void Install(IDictionary stateSaver)
        //{
        //    base.Install(stateSaver);
        //        //System.Configuration.ConfigurationManager.ConnectionStrings["AMSEntities"].ConnectionString = string.Format("Data Source={0};Initial Catalog={3};User ID={1};Password={0};MultipleActiveResultSets=False", this.Context.Parameters["SERVER"].ToString(),
        //        //                    this.Context.Parameters["UID"].ToString(), this.Context.Parameters["PWD"].ToString(), this.Context.Parameters["DBNAME"].ToString());


        //        try
        //        {
        //            //C:\Program Files\M&W\福宝会员管理系统\
        //            //throw new Exception(this.Context.Parameters["targetdir"].ToString());
        //            System.IO.FileInfo fileInfo = new System.IO.FileInfo(this.Context.Parameters["targetdir"].ToString() + "ams.exe.config");
        //            if (!fileInfo.Exists)
        //            {
        //                throw new InstallException("没有找到配置文件");
        //            }

        //            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
        //            xmlDocument.Load(fileInfo.FullName);
        //            //System.Xml.XmlNode node;
        //            bool bFoundIt = false;
        //            foreach (System.Xml.XmlNode node in xmlDocument["configuration"]["connectionStrings"])
        //            {
        //                if (node.Name == "add")
        //                {
        //                    if (node.Attributes.GetNamedItem("name").Value == "AMSEntities")
        //                    {
        //                        node.Attributes.GetNamedItem("connectionString").Value =
        //                            string.Format(@"metadata=res://*/AMS_FB.csdl|res://*/AMS_FB.ssdl|res://*/AMS_FB.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source={0};Initial Catalog={3};User ID={1};Password={2};MultipleActiveResultSets=False&quot;", this.Context.Parameters["server"].ToString(),
        //                            this.Context.Parameters["uid"].ToString(), this.Context.Parameters["pwd"].ToString(), this.Context.Parameters["database"].ToString());
        //                        bFoundIt = true;
        //                    }
        //                }
        //            }
        //            if (!bFoundIt)
        //            {
        //                throw new InstallException("ams.exe.config 文件没有包含连接字符串设置");
        //            }
        //            xmlDocument.Save(fileInfo.FullName);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //        }
        //}
    }
}
