//*********************************************************************************
//      uc_TabSystemControl.cs
//*********************************************************************************
// File Name: uc_TabSystemControl.cs
// Description: 用於uc_SystemModeControl的System Control使用者控件
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date            Author       Request No.    Tag     Description
// ------------ -------------  -------------  ------  -----------------------------
// 2019/07/11    Xenia Tseng       N/A         N/A     Initial Release
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
using NLog;
using MirleGO_UIFrameWork.UI.uc_Button;
using com.mirle.ibg3k0.bc.winform.App;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl
{
    public partial class uc_TabSystemControl : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ohxc.winform.App.WindownApplication app = null;
        //*******************公用參數設定*******************

        public uc_TabSystemControl()
        {
            InitializeComponent();
            initUI();
            off_Button_Click();
            offline_Button_Click();
            app = ohxc.winform.App.WindownApplication.getInstance();
            //app.ObjCacheManager.ConnectionInfoUpdate += ObjCacheManager_ConnectionInfo_update;
        }
        private void ObjCacheManager_ConnectionInfo_update(object sender, EventArgs e)
        {
            //uc_TSCStatus.Name = app.ObjCacheManager.GetLine().SCStats.ToString();
            //uc_TSCStatus.SetConnSignal(app.ObjCacheManager.GetLine().SCStats.ToString(), true);
            //refresh
        }
        //初始化UI
        private void initUI()
        {
            try
            {
                /*System Control*/
                //uc_CommunicationStatus.SetConnSignal("Off", false);
                //uc_ControlStatus.SetConnSignal("Offline", false);
                //uc_TSCStatus.SetConnSignal("Pause", false);

                /*Control Status*/
                uc_ControlStatus1.SetConnStatus("Current port states", true);
                uc_ControlStatus2.SetConnStatus("Current state", true);
                uc_ControlStatus3.SetConnStatus("Enhanced vehicles", true);
                uc_ControlStatus4.SetConnStatus("TSC state", true);
                uc_ControlStatus5.SetConnStatus("Unit alarm state list", true);
                uc_ControlStatus6.SetConnStatus("Enhanced transfers", true);
                uc_ControlStatus7.SetConnStatus("Enhanced carriers", false);
                uc_ControlStatus8.SetConnStatus("Lane cut list", false);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_btn_on_Button_Click(object sender, EventArgs e)
        {
            try
            {
                on_Button_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void on_Button_Click()
        {
            try
            {
                uc_btn_on.Enabled = false;
                uc_btn_off.Enabled = true;
                //uc_StatusSignal_11.SetConnSignal("On", true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_btn_off_Button_Click(object sender, EventArgs e)
        {
            try
            {
                off_Button_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void off_Button_Click()
        {
            try
            {
                uc_btn_on.Enabled = true;
                uc_btn_off.Enabled = false;
                //uc_StatusSignal_11.SetConnSignal("Off", false);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_btn_offline_Click(object sender, EventArgs e)
        {
            try
            {
                offline_Button_Click();

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void offline_Button_Click()
        {
            try
            {
                uc_btn_onlineR.Enabled = true;
                uc_btn_onlineL.Enabled = false;
                uc_btn_offline.Enabled = false;
                //uc_ControlStatus.SetConnSignal("Offline", false);
                pause_Button_alloff_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_btn_onlineR_Button_Click(object sender, EventArgs e)
        {
            uc_btn_onlineR.Enabled = false;
            uc_btn_onlineL.Enabled = true;
            uc_btn_offline.Enabled = true;
            //uc_ControlStatus.SetConnSignal("Online Remote", true);
            uc_btn_auto.Enabled = true;
            if (uc_btn_pause.Enabled == false)
            {
                pause_Button_Click();
            }
            else
            {
                auto_Button_Click();
            }
        }

        private void uc_btn_onlineL_Button_Click(object sender, EventArgs e)
        {
            uc_btn_onlineR.Enabled = true;
            uc_btn_onlineL.Enabled = false;
            uc_btn_offline.Enabled = true;
            //uc_ControlStatus.SetConnSignal("Online Local", true);
            //uc_btn_auto.Enabled = false;
        }

        private void uc_btn_auto_Button_Click(object sender, EventArgs e)
        {
            try
            {
                auto_Button_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void auto_Button_Click()
        {
            try
            {
                uc_btn_auto.Enabled = false;
                uc_btn_pause.Enabled = true;
                //uc_TSCStatus.SetConnSignal("Auto", true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_btn_pause_Button_Click(object sender, EventArgs e)
        {
            try
            {
                pause_Button_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void pause_Button_Click()
        {
            try
            {
                uc_btn_auto.Enabled = true;
                uc_btn_pause.Enabled = false;
                //uc_TSCStatus.SetConnSignal("Pause", false);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void pause_Button_alloff_Click()
        {
            try
            {
                uc_btn_auto.Enabled = false;
                uc_btn_pause.Enabled = false;
                //uc_TSCStatus.SetConnSignal("Pause", false);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
