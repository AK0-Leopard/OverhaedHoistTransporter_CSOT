//*********************************************************************************
//      UserAccountMgt.cs
//*********************************************************************************
// File Name: UserAccountMgt.cs
// Description: User Account Management
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/07/29           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using NLog;
using System;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Menu_System
{
    public partial class UserAccountMgt : Form
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion 公用參數設定

        public UserAccountMgt()
        {
            try
            {
                InitializeComponent();
                uc_AccountManagement1.CloseFormEvent += Uc_AccountManagement_CloseFormEvent;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_AccountManagement_CloseFormEvent(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void UserAccountMgt_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                uc_AccountManagement1.unRegisterEvent();
                this.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
