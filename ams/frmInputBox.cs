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
    public partial class frmInputBox : Form
    {
        string strMessage = "";
        string strType = "";

        public frmInputBox(string strmes,string strTp)
        {
            InitializeComponent();
            strMessage = strmes;
            strType = strTp;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            GlobalParams.strInputBoxMes = txtInput.Text.Trim();
            this.Close();
        }

        private void frmInputBox_Load(object sender, EventArgs e)
        {
            txtContent.Text = strMessage;
            if (strType == "PWD")
                txtInput.PasswordChar = '*';
            txtInput.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GlobalParams.strInputBoxMes = "Cancel";
            this.Close();
        }
    }
}
