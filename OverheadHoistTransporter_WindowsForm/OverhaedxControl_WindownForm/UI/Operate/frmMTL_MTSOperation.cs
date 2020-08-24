//*********************************************************************************
//      frmMTL_MTSOperation.cs
//*********************************************************************************
// File Name: frmMTL_MTSOperation.cs
// Description: MTL_MTS Operation
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
    public partial class frmMTL_MTSOperation : Form
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static frmMTL_MTSOperation form = null;
        static object lock_obj = new object();
        #endregion 公用參數設定

        public static frmMTL_MTSOperation getInstance(App.WindownApplication _app)
        {
            lock (lock_obj)
            {
                if (form == null || form.IsDisposed)
                {
                    form = new frmMTL_MTSOperation(_app);
                }
                else
                {
                    form.Focus();
                }
            }
            return form;
        }

        private frmMTL_MTSOperation(App.WindownApplication _app)
        {
            try
            {
                InitializeComponent();
                uc_MTLMTSInfo1.start(_app);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

    }
}
