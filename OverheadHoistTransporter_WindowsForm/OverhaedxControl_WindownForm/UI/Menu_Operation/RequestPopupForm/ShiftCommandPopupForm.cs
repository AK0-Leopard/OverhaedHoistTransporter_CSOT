//*********************************************************************************
//      ShiftCommandPopupForm.cs
//*********************************************************************************
// File Name: ShiftCommandPopupForm.cs
// Description: Shift Command Popup Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/08/22           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using NLog;
using System;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Menu_System
{
    public partial class ShiftCommandPopupForm : Form
    {
        #region 公用參數設定
        TarnferCMDViewObj cmdID = null;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion 公用參數設定

        public ShiftCommandPopupForm()
        {
            try
            {
                InitializeComponent();
                uc_TransferCommand1.CloseFormEvent += Uc_TransferCommand1_CloseFormEvent;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void readCmdID(TarnferCMDViewObj cmd_id)
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

        private void Uc_TransferCommand1_CloseFormEvent(object sender, EventArgs e)
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

        private void ShiftCommandPopupForm_Load(object sender, EventArgs e)
        {
            try
            {
                uc_TransferCommand1.SetTitleName("Shift Command", "Shift Vehicle ID");
                uc_TransferCommand1.initUI(cmdID, BCAppConstants.SubPageIdentifier.TRANSFER_SHIFT_COMMAND);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ShiftCommandPopupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                uc_TransferCommand1.unRegisterEvent_MCSCommandShift();
                this.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
