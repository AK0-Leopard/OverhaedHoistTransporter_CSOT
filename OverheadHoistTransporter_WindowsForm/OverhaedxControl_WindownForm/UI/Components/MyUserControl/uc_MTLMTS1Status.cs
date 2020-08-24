//*********************************************************************************
//      uc_MTLMTS1Status.cs
//*********************************************************************************
// File Name: uc_MTLMTS1Status.cs
// Description: uc_MTLMTS1Status Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date            Author       Request No.    Tag     Description
// ------------ -------------  -------------  ------  -----------------------------
// 2019/07/03    Xenia Tseng       N/A         N/A     Initial Release
//**********************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MirleGO_UIFrameWork.UI.uc_Button;
using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using NLog;
using System.Threading;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.App;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl
{
    public partial class uc_MTLMTS1Status : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        bool reflashUI = false;

        public uc_MTLMTS1Status()
        {
            InitializeComponent();
        }

        public void startupUI()
        {
            this.Width = 701;
            this.Height = 606;
        }

        private void tgsw_MTL_InterLock_MouseCaptureChanged(object sender, EventArgs e)
        {
            try
            {
                if (reflashUI)
                {
                    return;
                }
                if (tgsw_MTL_InterLock.Checked != false)
                {
                    var confirmResult = TipMessage_Request_Light.Show("Are you sure to change Interlock Mode ?");
                    if (confirmResult != DialogResult.No)
                    {
                        TipMessage_Type_Light.Show("Successfully Interlock Off", "Successfully close the MTL/MTS Interlock.", BCAppConstants.INFO_MSG);
                        p_MTL_Left_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTL_red;
                        p_MTL_Right_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTL_red;
                    }
                    else
                    {
                        tgsw_MTL_InterLock.Checked = true;
                    }
                }
                else
                {
                    var confirmResult = TipMessage_Request_Light.Show("Are you sure to change Interlock Mode ?");
                    if (confirmResult != DialogResult.Yes)
                    {
                        tgsw_MTL_InterLock.Checked = false;
                    }
                    else
                    {
                        TipMessage_Type_Light.Show("Successfully Interlock On", "Successfully open the MTL/MTS Interlock.", BCAppConstants.INFO_MSG);
                        p_MTL_Left_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTL_green;
                        p_MTL_Right_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTL_green;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void tgsw_MTS1_InterLock_MouseCaptureChanged(object sender, EventArgs e)
        {
            try
            {
                if (reflashUI)
                {
                    return;
                }
                if (tgsw_MTS1_InterLock.Checked != false)
                {
                    var confirmResult = TipMessage_Request_Light.Show("Are you sure to change Interlock Mode ?");
                    if (confirmResult != DialogResult.No)
                    {
                        TipMessage_Type_Light.Show("Successfully Interlock Off", "Successfully close the MTL/MTS Interlock.", BCAppConstants.INFO_MSG);
                        p_MTS1_Left_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTS_red;
                        p_MTS1_Right_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTS_red;
                    }
                    else
                    {
                        tgsw_MTS1_InterLock.Checked = true;
                    }
                }
                else
                {
                    var confirmResult = TipMessage_Request_Light.Show("Are you sure to change Interlock Mode ?");
                    if (confirmResult != DialogResult.Yes)
                    {
                        tgsw_MTS1_InterLock.Checked = false;
                    }
                    else
                    {
                        TipMessage_Type_Light.Show("Successfully Interlock", "Successfully open the MTL/MTS Interlock.", BCAppConstants.INFO_MSG);
                        p_MTS1_Left_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTS_green;
                        p_MTS1_Right_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTS_green;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_btn_LiftUpOn_Button_Click(object sender, EventArgs e)
        {
            p_MTL_UpDown_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTL_Up_On;
        }

        private void uc_btn_LiftUpOff_Button_Click(object sender, EventArgs e)
        {
            p_MTL_UpDown_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTL_Up_Off;
        }

        private void uc_btn_LiftDownOn_Button_Click(object sender, EventArgs e)
        {
            p_MTL_UpDown_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTL_Down_On;
        }

        private void uc_btn_LiftDownOff_Button_Click(object sender, EventArgs e)
        {
            p_MTL_UpDown_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTL_Down_Off;
        }
    }
}
