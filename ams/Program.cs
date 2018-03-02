using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ams
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //单实例设置
            bool isAppRunning = false;
            System.Threading.Mutex mutex = new System.Threading.Mutex(
                true,
                System.Diagnostics.Process.GetCurrentProcess().ProcessName,
                out isAppRunning);
            if (!isAppRunning)
            {
                MessageBox.Show("会员管理系统已经在运行了，请不要重复运行！");
                Environment.Exit(1);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                SplashForm splash = new SplashForm();
                splash.ShowInTaskbar = false;
                //splash.Show();
                Application.DoEvents();
                splash.InitParams();
                splash.Close();
                LoginForm login = new LoginForm();
                login.ShowDialog();
                if (login.DialogResult == DialogResult.OK)
                {
                    Application.Run(new MainForm());
                }
            }
        }
    }
}
