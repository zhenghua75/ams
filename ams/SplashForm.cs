using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ams.Common;
namespace ams
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }
        public void InitParams()
        {
            try
            {
                using (AMSEntities amsContext = new AMSEntities())
                {
                    GlobalParams gp = new GlobalParams(amsContext);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write(this, ex);
            }
        }
    }
}
