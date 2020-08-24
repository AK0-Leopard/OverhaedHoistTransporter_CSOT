//*********************************************************************************
//      ChangePwdForm.cs
//*********************************************************************************
// File Name: ChangePwdForm.cs
// Description: Change Password Form
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
    public partial class ChangePwdForm : Form
    {
        #region 公用參數設定
        OHxCMainForm mainForm = null;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion 公用參數設定

        public ChangePwdForm(OHxCMainForm _mainForm)
        {
            try
            {
                InitializeComponent();
                mainForm = _mainForm;
                uc_PasswordChange1.CloseFormEvent += Uc_PasswordChange1_CloseFormEvent;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_PasswordChange1_CloseFormEvent(object sender, EventArgs e)
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

        private void ChangePwdForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                uc_PasswordChange1.unRegisterEvent();
                this.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
