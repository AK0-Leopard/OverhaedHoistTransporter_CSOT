//*********************************************************************************
//      AboutPopupForm.cs
//*********************************************************************************
// File Name: AboutPopupForm.cs
// Description: About Popup Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/08/22           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using System;
using System.Windows.Forms;
using NLog;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Menu_Help
{
    public partial class AboutPopupForm : Form
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion 公用參數設定

        public AboutPopupForm()
        {
            try
            {
                InitializeComponent();
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void AboutPopupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
