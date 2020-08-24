//*********************************************************************************
//      uc_MTS2Status.cs
//*********************************************************************************
// File Name: uc_MTS2Status.cs
// Description: uc_MTS2Status Form
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
    public partial class uc_MTS2Status : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        bool reflashUI = false;

        public uc_MTS2Status()
        {
            InitializeComponent();
        }

        private void tgsw_MTS2_InterLock_MouseCaptureChanged(object sender, EventArgs e)
        {
            try
            {
                if (reflashUI)
                {
                    return;
                }
                if (tgsw_MTS2_InterLock.Checked != false)
                {
                    var confirmResult = TipMessage_Request_Light.Show("Are you sure to change Interlock Mode ?");
                    if (confirmResult != DialogResult.No)
                    {
                        TipMessage_Type_Light.Show("Successfully Interlock Off", "Successfully close the MTL/MTS Interlock.", BCAppConstants.INFO_MSG);
                        p_MTS2_Left_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTS_red;
                        p_MTS2_Right_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTS_red;
                    }
                    else
                    {
                        tgsw_MTS2_InterLock.Checked = true;
                    }
                }
                else
                {
                    var confirmResult = TipMessage_Request_Light.Show("Are you sure to change Interlock Mode ?");
                    if (confirmResult != DialogResult.Yes)
                    {
                        tgsw_MTS2_InterLock.Checked = false;
                    }
                    else
                    {
                        TipMessage_Type_Light.Show("Successfully Interlock", "Successfully open the MTL/MTS Interlock.", BCAppConstants.INFO_MSG);
                        p_MTS2_Left_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTS_green;
                        p_MTS2_Right_Signal.BackgroundImage = com.mirle.ibg3k0.ohxc.winform.Properties.Resources.MTS_green;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
