using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.mirle.ibg3k0.ohxc.winform;

namespace OverhaedxControl_WindownForm
{
    static class Program
    {
        private static OHxCMainForm _mainForm;

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;                                  //A0.12
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);  //A0.12
            Application.ThreadException += Application_ThreadException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new com.mirle.ibg3k0.ohxc.winform.OHxCMainForm());
            //Application.Run(new com.mirle.ibg3k0.bc.winform.UI.UAS.LoginPopupForm());
            //Application.Run(new com.mirle.ibg3k0.bc.winform.UI.ChangePwdForm());
            //Application.Run(new com.mirle.ibg3k0.bc.winform.UI.UAS.ExitSystemPopupForm());
            //Application.Run(new com.mirle.ibg3k0.bc.winform.UI.ChangePwdForm());
            //Application.Run(new com.mirle.ibg3k0.ohxc.winform.UI.UserAccountMgt());
            //Application.Run(new com.mirle.ibg3k0.bc.winform.UI.LogoutPopupForm());
            //Application.Run(new com.mirle.ibg3k0.bc.winform.UI.frm_Query(_mainForm));
            //Application.Run(new com.mirle.ibg3k0.bc.winform.UI.frm_Maintenance(_mainForm));

        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs args)
        {
            Exception e = args.Exception;
            NLog.LogManager.GetCurrentClassLogger().Error(e, "UnhandException - Application.ThreadException:");
        }
        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            NLog.LogManager.GetCurrentClassLogger().Error(e, "UnhandledException:");
        }


    }
}
