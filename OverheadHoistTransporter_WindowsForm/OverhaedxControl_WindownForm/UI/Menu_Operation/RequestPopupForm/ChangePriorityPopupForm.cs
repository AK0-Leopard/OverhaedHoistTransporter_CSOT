//*********************************************************************************
//      ChangePriorityPopupForm.cs
//*********************************************************************************
// File Name: ChangePriorityPopupForm.cs
// Description: Change Priority Popup Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/08/22           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using NLog;
using System;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Menu_Operation.RequestPopForm
{
    public partial class ChangePriorityPopupForm : Form
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        string cmdID = string.Empty;
        #endregion 公用參數設定

        public ChangePriorityPopupForm()
        {
            try
            {
                InitializeComponent();
                uc_ChangePriority1.CloseFormEvent += Uc_ChangePriority1_CloseFormEvent;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_ChangePriority1_CloseFormEvent(object sender, EventArgs e)
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

        public void readCmdID(string cmd_id)
        {
            try
            {
                cmdID = cmd_id;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ChangePriorityPopupForm_Load(object sender, EventArgs e)
        {
            try
            {
                uc_ChangePriority1.initUI(cmdID);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ChangePriorityPopupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                uc_ChangePriority1.unRegisterEvent();
                this.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
