//*********************************************************************************
//      AssignVehiclePopupForm.cs
//*********************************************************************************
// File Name: AssignVehiclePopupForm.cs
// Description: Assign Vehicle Popup Form
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
    public partial class AssignVehiclePopupForm : Form
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        TarnferCMDViewObj cmdID = null;
        #endregion 公用參數設定

        public AssignVehiclePopupForm()
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

        private void AssignVehiclePopupForm_Load(object sender, EventArgs e)
        {
            try
            {
                uc_TransferCommand1.SetTitleName("Assign Vehicle", "Assign Vehicle ID");
                uc_TransferCommand1.initUI(cmdID, BCAppConstants.SubPageIdentifier.TRANSFER_ASSIGN_VEHICLE);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void AssignVehiclePopupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                uc_TransferCommand1.unRegisterEvent_MCSCommandVehicleAssign();
                this.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
