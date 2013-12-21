using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Configuration;
using Splasher;

namespace EbayMaster
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SplashForm.StartSplash(ConfigurationManager.AppSettings["SplashPath"].ToString(), Color.FromArgb(0, 127, 0));
            Application.UseWaitCursor = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
