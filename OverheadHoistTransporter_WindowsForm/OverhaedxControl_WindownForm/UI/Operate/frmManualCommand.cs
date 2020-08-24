//*********************************************************************************
//      frmManualCommand.cs
//*********************************************************************************
// File Name: frmManualCommand.cs
// Description: Manual Command
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/05/16           Kevin                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using NLog;
using System;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Operate
{
    public partial class frmManualCommand : Form
    {
        #region 公用參數設定
        private static frmManualCommand form = null;
        static object lock_obj = new object();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion 公用參數設定

        public static frmManualCommand getInstance()
        {
            lock (lock_obj)
            {
                if (form == null || form.IsDisposed)
                {
                    form = new frmManualCommand();
                }
                else
                {
                    form.Focus();
                }
            }
            return form;
        }

        private frmManualCommand()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
