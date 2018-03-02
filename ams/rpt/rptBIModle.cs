using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace ams.rpt
{    
    /// <summary>
    /// Summary description for rptBIModle.
    /// </summary>
    public partial class rptBIModle : DataDynamics.ActiveReports.ActiveReport3
    {      
        public rptBIModle(List<tbBillList> list)
        {
            InitializeComponent();
            clsMyItem item = new clsMyItem();
            item.MyList = list;
            this.AddNamedItem("item", item); 
        }
       
    }

    public  partial class clsMyItem
    {
        public clsMyItem()
        {
        }
        public List<tbBillList> MyList { get; set; }
    }

}
